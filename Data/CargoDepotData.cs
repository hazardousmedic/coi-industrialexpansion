using Mafi;
using Mafi.Base;
using Mafi.Base.Prototypes.Buildings;
using Mafi.Core.Buildings.Cargo;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IndustrialExpansion.Data
{
    public sealed class CargoDepotsData : IModData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {
            Log.Info("[IndustrialExpansion] CargoDepotData : Registering Tier V and VI Cargo Depot prototypes.");

            Predicate<LayoutTile> predicate = (LayoutTile x) => x.Constraint == LayoutTileConstraint.None || x.Constraint.HasAnyConstraints(LayoutTileConstraint.Ocean);
            CustomLayoutToken[] array = new CustomLayoutToken[3];
            array[0] = new CustomLayoutToken("~0!", (EntityLayoutParams p, int h) => new LayoutTokenSpec(-10, h, LayoutTileConstraint.Ocean, null, null, null, null, null, null, false, false, 0));
            array[1] = new CustomLayoutToken("{0!", delegate (EntityLayoutParams p, int h)
            {
                int? num = new int?(0);
                return new LayoutTokenSpec(-10, h, LayoutTileConstraint.None, null, null, num, null, null, null, false, false, 0);
            });
            array[2] = new CustomLayoutToken("~~~", (EntityLayoutParams p, int h) => new LayoutTokenSpec(-12, -10, LayoutTileConstraint.Ocean, null, null, null, null, null, null, false, false, 0));
            EntityLayoutParams entityLayoutParams = new EntityLayoutParams(predicate, new ArrayListHelper<CustomLayoutToken>(array), false, null, null, null, null, null, null, default(Option<IEnumerable<KeyValuePair<char, int>>>), false, null, null);
            string depotDescription = ModTranslation.Get("Once built, a repaired cargo ship can dock here and transfer its cargo via attached cargo depot modules.");
            string[] array2 = Enumerable.Repeat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ", 2).Concat(new ElementListHelper<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)<@A")).Concat(Enumerable.Repeat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ", 2))
                .ToArray<string>();
            RelTile1f relTile1f = new RelTile1f(10);
            Duration duration = 18.Seconds();
            Duration duration2 = 3.Seconds();
            CargoDepotProto cargoDepotProto = registrator.CargoDepotProtoBuilder.Start(ModTranslation.Get("Cargo depot (12)"), ModIds.Buildings.CargoDepotT6).SetCost(ModCosts.Buildings.CargoDepotT6, false).Description(LocalizationManager.CreateAlreadyLocalizedStr(ModIds.Buildings.CargoDepotT6.ToString() + "__desc", depotDescription))
                .SetSlots(CargoDepotsData.MODULE_ORIGIN, 5, 9, 12)
                .SetInterfaceRange(relTile1f + new RelTile1f(50))
                .SetArrivalDepartDurations(duration + 5 * duration2, duration + 5 * duration2)
                .SetCargoShipProtoId(ModIds.Ships.CargoShipT6)
                .SetCategories(new Proto.ID[] { Ids.ToolbarCategories.CargoShips })
                .SetLayout(entityLayoutParams, ShipyardData.CreatePadding(16, "~~~", "                                                   ", false).Concat(Enumerable.Repeat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ", 60)).Concat(array2)
                    .Concat(ShipyardData.CreatePadding(16, "~~~", "                                                   ", true))
                    .ToArray<string>())
                .SetCustomIconPath("Assets/CargoDepot/Icons/CargoDepotT6.png")
                .SetPrefabPath("Assets/CargoDepot/Prefabs/CargoDepotT6.prefab")
                .BuildAndAdd();
            CargoDepotProto cargoDepotProto2 = registrator.CargoDepotProtoBuilder.Start(ModTranslation.Get("Cargo depot (10)"), ModIds.Buildings.CargoDepotT5).SetCost(ModCosts.Buildings.CargoDepotT5, false).Description(LocalizationManager.CreateAlreadyLocalizedStr(ModIds.Buildings.CargoDepotT5.ToString() + "__desc", depotDescription))
                .SetSlots(CargoDepotsData.MODULE_ORIGIN, 5, 9, 10)
                .SetInterfaceRange(relTile1f + new RelTile1f(45))
                .SetArrivalDepartDurations(duration + 4 * duration2, duration + 4 * duration2)
                .SetPrefabOffset(new RelTile3f(0, 5, 0))
                .SetCargoShipProtoId(ModIds.Ships.CargoShipT5)
                .SetCategories(new Proto.ID[] { Ids.ToolbarCategories.CargoShips })
                .SetCustomIconPath("Assets/CargoDepot/Icons/CargoDepotT5.png")
                .SetLayout(entityLayoutParams, ShipyardData.CreatePadding(16, "~~~", "                                                   ", false).Concat(Enumerable.Repeat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ", 60)).Concat(array2)
                    .Concat(ShipyardData.CreatePadding(16, "~~~", "                                                   ", true))
                    .ToArray<string>())
                .SetPrefabPath("Assets/CargoDepot/Prefabs/CargoDepotT5.prefab")
                .BuildAndAdd();
            CargoDepotProto orThrow = registrator.PrototypesDb.GetOrThrow<CargoDepotProto>(Ids.Buildings.CargoDepotT4);
            orThrow.SetNextTier(cargoDepotProto2, false);
            cargoDepotProto2.SetNextTier(cargoDepotProto, false);
            cargoDepotProto2.AddParam(new DrawArrowWileBuildingProtoParam(6f, 0f));
            cargoDepotProto.AddParam(new DrawArrowWileBuildingProtoParam(6f, 0f));
            cargoDepotProto2.Upgrade.SetCannotDowngrade();
            cargoDepotProto.Upgrade.SetCannotDowngrade();
            cargoDepotProto2.Upgrade.SetCannotMove();
            cargoDepotProto.Upgrade.SetCannotMove();

            Log.Info("[IndustrialExpansion] CargoDepotData : Tier V and VI Cargo Depot prototypes registered.");

        }

        private static readonly RelTile2i MODULE_ORIGIN = new RelTile2i("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Length / 3 - 1, 13);
    }
}
