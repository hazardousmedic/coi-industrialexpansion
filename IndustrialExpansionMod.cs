using HarmonyLib;
using IndustrialExpansion.Data;
using IndustrialExpansion.Research;
using Mafi;
using Mafi.Collections;
using Mafi.Core.Mods;

namespace IndustrialExpansion
{
    public sealed class IndustrialExpansionMod : DataOnlyMod
    {
        private Harmony _harmony;

        public IndustrialExpansionMod(ModManifest manifest) : base(manifest)
        {
            Log.Info("[IndustrialExpansion] Constructed");
        }

        public override void RegisterPrototypes(ProtoRegistrator registrator)
        {

            ModTranslation.Initialize(this.Manifest.RootDirectoryPath);

            registrator.RegisterAllProducts();

            // Logistics Registration
            registrator.RegisterData<LogisticsData>();
            registrator.RegisterData<LogisticsResearchData>();

            // Surfaces Registration
            registrator.RegisterData<SurfaceData>();
            registrator.RegisterData<SurfaceResearchData>();

            // Cargoships Registration
            registrator.RegisterData<CargoDepotsData>();
            registrator.RegisterData<ShipsData>();
            registrator.RegisterData<ShipResearchData>();

            // Retaining Wall Registration
            registrator.RegisterData<RetainingWallData>();
            registrator.RegisterData<RetaingWallResearchData>();

            // Register Cargo Ship Drydock
            registrator.RegisterData<CargoShipVirtualProductData>();
            registrator.RegisterData<CargoShipDryDockData>();
            registrator.RegisterData<CargoShipDryDockResearchData>();
        }

        public override void EarlyInit(DependencyResolver resolver)
        {
            base.EarlyInit(resolver);
            _harmony = new Harmony("IndustrialExpansion");
            _harmony.PatchCategory("LiftThroughputPatch");
            _harmony.PatchCategory("SorterThroughputPatch");
            _harmony.PatchCategory("ContractsTabPatch");

            Log.Info("[IndustrialExpansion] Harmony patches applied");
        }

        public override void MigrateJsonConfig(VersionSlim savedVersion, Dict<string, object> savedValues)
        {
            base.MigrateJsonConfig(savedVersion, savedValues);
        }
    }
}