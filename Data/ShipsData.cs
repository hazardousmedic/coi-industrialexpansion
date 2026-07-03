using Mafi;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Buildings.Cargo.Ships;
using Mafi.Core.Buildings.Cargo.Ships.Modules;
using Mafi.Core.Economy;
using Mafi.Core.Entities.Dynamic;
using Mafi.Core.Mods;
using Mafi.Core.PathFinding;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;
using Mafi.Localization;

namespace IndustrialExpansion.Data
{
    internal class ShipsData : IModData
    {
        // Fuel Constant
        private static readonly Quantity FuelPerJourneyBase = 40.Quantity();
        private static readonly Quantity FuelPerJourneyPerModule = 25.Quantity();
        private static readonly Percent PollutionPercentDiesel = 100.Percent();
        private static readonly Percent PollutionPercentHydrogen = 0.Percent();

        // Hydrogen requires ~20% more fuel by energy density than diesel.
        private static readonly Percent HydrogenDieselEnergyRatio = 120.Percent();
        // Heavy oil is denser than diese: burn ~20% less fuel per journey but emtits ~30% more pollution than disesel
        private static readonly Percent HeavyOilDieselFuelRatio = 80.Percent();
        private static readonly Percent PollutionPercentHeavyOil = 130.Percent();

        public void RegisterData(ProtoRegistrator registrator)
        {
            ProtosDb prototypesDb = registrator.PrototypesDb;

            // Fuel products.
            ProductProto hydrogen = prototypesDb.GetOrThrow<ProductProto>(Ids.Products.Hydrogen);
            ProductProto diesel = prototypesDb.GetOrThrow<ProductProto>(Ids.Products.Diesel);
            ProductProto heavyoil = prototypesDb.GetOrThrow<ProductProto>(Ids.Products.HeavyOil);

            // Build-cost ingredient products.
            ProductProto electronics = prototypesDb.GetOrThrow<ProductProto>(Ids.Products.Electronics);
            ProductProto mechanicalParts = prototypesDb.GetOrThrow<ProductProto>(Ids.Products.MechanicalParts);
            ProductProto steel = prototypesDb.GetOrThrow<ProductProto>(Ids.Products.Steel);

            // Technologies that unlock each fuel variant.
            Option<Proto> cargoShipTech = prototypesDb.GetOrThrow<TechnologyProto>(Ids.Technology.CargoShip);
            Option<Proto> hydrogenShipTech = prototypesDb.GetOrThrow<TechnologyProto>(Ids.Technology.HydrogenCargoShip);

            AssetValue dieselShipCost = new AssetValue(
                mechanicalParts.WithQuantity(100),
                steel.WithQuantity(100)
            );

            AssetValue hydrogenShipCost = new AssetValue(
                electronics.WithQuantity(180),
                steel.WithQuantity(180)
            );

            // Shared graphics for the standard diesel ship.
            CargoShipProto.Gfx dieselGfx = new CargoShipProto.Gfx(
                "Assets/Base/Ships/CargoShip/CargoShip_Front.prefab",
                "Assets/Base/Ships/CargoShip/CargoShip_Rear.prefab",
                "Assets/Base/Ships/CargoShip/CargoShipModule_Empty.prefab",
                "Assets/Base/Ships/CargoShip/Audio/Engine.prefab",
                "Assets/Base/Ships/CargoShip/Audio/Arrival.prefab",
                "Assets/Base/Ships/CargoShip/Audio/Departure.prefab",
                new RelTile1i(5),
                RelTile3f.FromDimensionsInMeters(40.0, 22.0, 25.0),
                "Assets/Base/Icons/Technologies/CargoShipTechnology.png"
            );

            // Hydrogen ship reuses all shared assets except the front/rear hull prefabs.
            CargoShipProto.Gfx hydrogenGfx = new CargoShipProto.Gfx(
                "Assets/Base/Ships/CargoShip/HydrogenFront.prefab",
                "Assets/Base/Ships/CargoShip/HydrogenRear.prefab",
                dieselGfx.EmptyModulePrefabPath,
                dieselGfx.EngineSoundPath,
                dieselGfx.ArrivalSoundPath,
                dieselGfx.DepartureSoundPath,
                dieselGfx.ModuleSlotLength,
                dieselGfx.BasicBoxColliderSize,
                dieselGfx.IconPath
            );

            // Shared graphics for the standard heavy oil ship.
            CargoShipProto.Gfx heavyoilGfx = new CargoShipProto.Gfx(
                dieselGfx.FrontPrefabPath,
                dieselGfx.BackPrefabPath,
                dieselGfx.EmptyModulePrefabPath,
                dieselGfx.EngineSoundPath,
                dieselGfx.ArrivalSoundPath,
                dieselGfx.DepartureSoundPath,
                dieselGfx.ModuleSlotLength,
                dieselGfx.BasicBoxColliderSize,
                dieselGfx.IconPath
            );

            ImmutableArray<CargoShipModuleProto> shipModules = GetShipModules(registrator);

            // Cost for ship fuel conversions
            ImmutableArray<ProductProto> dieselConversions = ImmutableArray.Create<ProductProto>(heavyoil);
            ImmutableArray<ProductProto> heavyoilConversions = ImmutableArray.Create<ProductProto>(diesel);
            ImmutableArray<ProductProto> hydrogenConversions = ImmutableArray<ProductProto>.Empty;

            CargoShipProto.FuelData dieselFuelData = new CargoShipProto.FuelData(
                diesel,
                FuelPerJourneyBase,
                FuelPerJourneyPerModule,
                cargoShipTech,
                dieselConversions,
                dieselShipCost,
                PollutionPercentDiesel,
                Option<CargoShipProto.Gfx>.None
            );

            CargoShipProto.FuelData hydrogenFuelData = new CargoShipProto.FuelData(
                hydrogen,
                FuelPerJourneyBase.ScaledBy(HydrogenDieselEnergyRatio),
                FuelPerJourneyPerModule.ScaledBy(HydrogenDieselEnergyRatio),
                hydrogenShipTech,
                hydrogenConversions,
                hydrogenShipCost,
                PollutionPercentHydrogen,
                hydrogenGfx
            );

            CargoShipProto.FuelData heavyoilFuelData = new CargoShipProto.FuelData(
                heavyoil,
                FuelPerJourneyBase.ScaledBy(HeavyOilDieselFuelRatio),
                FuelPerJourneyPerModule.ScaledBy(HeavyOilDieselFuelRatio),
                cargoShipTech,
                heavyoilConversions,
                dieselShipCost,  // Heavy oil ship has same cost as diesel variant.
                PollutionPercentHeavyOil,
                heavyoilGfx
            );

            ImmutableArray<CargoShipProto.FuelData> fuelVariants = ImmutableArray.Create(
                dieselFuelData,
                hydrogenFuelData,
                heavyoilFuelData
            );

            // Both T5 and T6 share the same description and display name.
            LocStr sharedDescription = Loc.Str(
                ModIds.Ships.CargoShipT5.ToString() + "__desc",
                ModTranslation.Get("The cargo ship departs automatically when there is enough cargo to collect on the world map."),
                "description of a cargo ship"
            );

            // Both ships share identical driving physics; only size and module count differ.
            // Parameter order: maxSpeed, acceleration, reverseSpeedFraction, rotationSpeed,
            // rotationAcceleration, maxTurnAngle, turnAngleAcceleration, weightTons,
            // frontWheelOffset, rearWheelOffset, brakingFraction, stoppingDistance.
            DrivingData sharedDrivingData = new DrivingData(
                1.6.Tiles(),
                0.6.Tiles(),
                40.Percent(),
                0.012.Tiles(),
                0.01.Tiles(),
                4.Degrees(),
                0.1.Degrees(),
                2.5.ToFix32(),
                RelTile1f.Zero,
                RelTile1f.Zero,
                0.85.ToFix32(),
                0.4.Tiles()
            );

            // T5: 10-module ship.
            prototypesDb.Add<CargoShipProto>(
                new CargoShipProto(
                    ModIds.Ships.CargoShipT5,
                    Proto.CreateStr(ModIds.Ships.CargoShipT5, "Cargo Ship", sharedDescription, null),
                    ModCosts.CargoShip.CargoShipT5.MapToEntityCosts(registrator),
                    RelTile3f.FromDimensionsInMeters(141.0, 21.0, 5.0),
                    sharedDrivingData,
                    Option<FuelTankProto>.None,
                    new ShipsPathFindingParams(new RelTile2i(71, 23), ShipHeightClass.CargoShip),
                    80.Seconds(),  // loadingDuration
                    10,            // modulesCount
                    shipModules,
                    fuelVariants,
                    dieselGfx,
                    200.Percent()  // capacityPercent
                ),
                false
            );

            // T6: 12-module ship.
            prototypesDb.Add<CargoShipProto>(
                new CargoShipProto(
                    ModIds.Ships.CargoShipT6,
                    Proto.CreateStr(ModIds.Ships.CargoShipT6, ModTranslation.Get("Cargo Ship"), sharedDescription, null),
                    ModCosts.CargoShip.CargoShipT6.MapToEntityCosts(registrator),
                    RelTile3f.FromDimensionsInMeters(161.0, 21.0, 5.0),
                    sharedDrivingData,
                    Option<FuelTankProto>.None,
                    new ShipsPathFindingParams(new RelTile2i(81, 23), ShipHeightClass.CargoShip),
                    80.Seconds(),  // loadingDuration
                    12,            // modulesCount
                    shipModules,
                    fuelVariants,
                    dieselGfx,
                    200.Percent()  // capacityPercent
                ),
                false
            );
        }

        private static ImmutableArray<CargoShipModuleProto> GetShipModules(ProtoRegistrator registrator)
        {
            CargoShipModuleProto fluidModule =
                registrator.PrototypesDb.GetOrThrow<CargoShipModuleProto>(Ids.Ships.CargoShipFluidModule);
            CargoShipLooseModuleProto looseModule =
                registrator.PrototypesDb.GetOrThrow<CargoShipLooseModuleProto>(Ids.Ships.CargoShipLooseModule);
            CargoShipCountableModuleProto unitModule =
                registrator.PrototypesDb.GetOrThrow<CargoShipCountableModuleProto>(Ids.Ships.CargoShipUnitModule);

            return ImmutableArray.Create<CargoShipModuleProto>(fluidModule, looseModule, unitModule);
        }
    }
}