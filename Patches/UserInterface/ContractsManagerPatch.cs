using System;
using System.Reflection;
using HarmonyLib;
using Mafi;
using Mafi.Collections;
using Mafi.Core.Products;
using Mafi.Core.World.Contracts;

namespace IndustrialExpansion.Patches.UserInterface
{
    [HarmonyPatch(typeof(ContractsManager), "GetTradeEstimateForShipSize")]
    [HarmonyPatchCategory("ContractsTabPatch")]
    public static class ContractsManagerPatch
    {
        // Only ships with 10 or 12 modules use the extended cargo dock capacity logic.
        private static readonly int[] SupportedModuleCounts = { 10, 12 };

        [HarmonyPrefix]
        public static bool Prefix(
            ContractsManager __instance,
            ContractProto contract,
            int modulesCount,
            out Quantity maxToImport,
            out Quantity maxToExport)
        {
            maxToImport = Quantity.Zero;
            maxToExport = Quantity.Zero;

            if (Array.IndexOf(SupportedModuleCounts, modulesCount) < 0)
                return true; // Run original method for unsupported ship sizes.

            Quantity importCapacity = GetModuleCapacity(__instance, contract.ProductToBuy.Type, fallback: 500.Quantity());
            Quantity exportCapacity = GetModuleCapacity(__instance, contract.ProductToPayWith.Type, fallback: 500.Quantity());

            // Scale capacities by 200% for extended docks.
            Percent scaleFactor = 200.Percent();
            importCapacity = importCapacity.ScaledBy(scaleFactor);
            exportCapacity = exportCapacity.ScaledBy(scaleFactor);

            ComputeEstimate(__instance, contract, modulesCount, importCapacity, exportCapacity, out maxToImport, out maxToExport);
            TryInsertIntoCache(__instance, contract, modulesCount, maxToImport, maxToExport);

            return false; // Skip original method.
        }

        private static Quantity GetModuleCapacity(ContractsManager instance, ProductType productType, Quantity fallback)
        {
            try
            {
                FieldInfo field = typeof(ContractsManager)
                    .GetField("m_modulesCapacities", BindingFlags.Instance | BindingFlags.NonPublic);

                if (field == null) return fallback;

                object dict = field.GetValue(instance);
                if (dict == null) return fallback;

                // Locate TryGetValue(ProductType, out TValue) via reflection.
                Type valueType = field.FieldType.GetGenericArguments()[1];
                MethodInfo tryGetValue = dict.GetType().GetMethod(
                    "TryGetValue",
                    new[] { typeof(ProductType), valueType.MakeByRefType() });

                if (tryGetValue == null) return fallback;

                object[] args = { productType, null };
                bool found = (bool)tryGetValue.Invoke(dict, args);

                if (found && args[1] is Quantity capacity)
                    return capacity;
            }
            catch (Exception ex)
            {
                Log.Warning($"[IndustrialExpansion] GetModuleCapacity failed for {productType}: {ex.Message}");
            }

            return fallback;
        }

        private static void ComputeEstimate(
            ContractsManager instance,
            ContractProto contract,
            int modulesCount,
            Quantity perImportModule,
            Quantity perExportModule,
            out Quantity maxToImport,
            out Quantity maxToExport)
        {
            int remainingModules = modulesCount;
            Quantity surplusImport = Quantity.Zero;
            int importModulesUsed = 0;

            while (remainingModules > 0)
            {
                remainingModules--;

                Quantity toBuy = instance.GetToBuy(contract);
                Quantity exportNeeded = new Quantity(perExportModule.Value * toBuy.Value / contract.QuantityToPayWith.Value);
                Quantity exportRemainder = exportNeeded;

                while (exportRemainder.IsPositive && (surplusImport.IsPositive || remainingModules > 0))
                {
                    if (surplusImport.IsPositive)
                    {
                        Quantity consumed = exportRemainder.Min(surplusImport);
                        surplusImport -= consumed;
                        exportRemainder -= consumed;
                    }
                    else
                    {
                        importModulesUsed++;
                        remainingModules--;

                        Quantity allocated = perImportModule.Min(exportRemainder);
                        exportRemainder -= allocated;
                        surplusImport += perImportModule - allocated;
                    }
                }
            }

            Quantity totalImportCapacity = importModulesUsed * perImportModule;
            Quantity totalExportCapacity = (modulesCount - importModulesUsed) * perExportModule;

            // GetContractStatsForCapacities: first out = export estimate, second out = import estimate.
            instance.GetContractStatsForCapacities(
                contract,
                totalImportCapacity,
                totalExportCapacity,
                out maxToExport,
                out maxToImport);
        }

        private static void TryInsertIntoCache(
            ContractsManager instance,
            ContractProto contract,
            int modulesCount,
            Quantity maxToImport,
            Quantity maxToExport)
        {
            try
            {
                Type managerType = typeof(ContractsManager);

                FieldInfo cacheField = managerType
                    .GetField("m_contractsEstimatesCache", BindingFlags.Instance | BindingFlags.NonPublic);
                if (cacheField == null) return;

                object cache = cacheField.GetValue(instance);
                if (cache == null) return;

                Type cacheType = cache.GetType();

                // Resolve estimate type first — needed to build the correct TryGetValue signature.
                Type estimateDataType = managerType
                    .GetNestedType("ContractEstimateData", BindingFlags.NonPublic);
                if (estimateDataType == null) return;

                Type listType = typeof(Lyst<>).MakeGenericType(estimateDataType);

                // Now resolve TryGetValue with explicit parameter types to avoid AmbiguousMatchException.
                MethodInfo tryGetValue = cacheType.GetMethod(
                    "TryGetValue",
                    BindingFlags.Instance | BindingFlags.Public,
                    null,
                    new[] { typeof(ContractProto), listType.MakeByRefType() },
                    null);
                if (tryGetValue == null) return;

                // Retrieve or create the per-contract estimate list.
                object[] getArgs = { contract, null };
                bool existingList = (bool)tryGetValue.Invoke(cache, getArgs);

                object estimateList;
                if (existingList && getArgs[1] != null)
                {
                    estimateList = getArgs[1];
                }
                else
                {
                    estimateList = Activator.CreateInstance(listType);

                    MethodInfo addToCache = cacheType
                        .GetMethod("Add", BindingFlags.Instance | BindingFlags.Public,
                            null,
                            new[] { typeof(ContractProto), listType },
                            null);
                    addToCache?.Invoke(cache, new[] { contract, estimateList });
                }

                // Append the new estimate entry.
                object newEntry = Activator.CreateInstance(
                    estimateDataType,
                    new object[] { modulesCount, maxToImport, maxToExport });

                MethodInfo addToList = estimateList.GetType()
                    .GetMethod("Add", BindingFlags.Instance | BindingFlags.Public,
                        null,
                        new[] { estimateDataType },
                        null);
                addToList?.Invoke(estimateList, new[] { newEntry });
            }
            catch (Exception ex)
            {
                Log.Warning($"[IndustrialExpansion] TryInsertIntoCache failed: {ex.Message}");
            }
        }
    }
}