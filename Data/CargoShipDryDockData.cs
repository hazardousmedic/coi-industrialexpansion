using IndustrialExpansion.Helper.CargoShipDryDock;
using Mafi;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Factory.Recipes;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using System;
using System.Linq;
using System.Collections.Generic;

namespace IndustrialExpansion.Data
{
    internal class CargoShipDryDockData : IModData
    {
        public static string[] CreatePadding(int size, string token, string layoutBase, bool isReversed)
        {
            string[] array = new string[size];
            for (int i = 0; i < size; i++)
            {
                int num = (isReversed ? (size - i) : (i + 1));
                bool flag = num <= 3;
                if (flag)
                {
                    num = 0;
                }
                array[i] = token.RepeatString(num) + "   ".RepeatString(size - num) + layoutBase;
            }
            return array;
        }

        public void RegisterData(ProtoRegistrator reg)
        {
            Log.Info("[IndustrialExpansion] CargoShipDryDockData : Registering Drydock prototype.");

            EntityLayoutParser entityLayoutParser = new EntityLayoutParser(reg.PrototypesDb);
            Predicate<LayoutTile> predicate = (LayoutTile x) => x.Constraint == LayoutTileConstraint.None || x.Constraint.HasAnyConstraints(LayoutTileConstraint.Ocean);
            CustomLayoutToken[] array = new CustomLayoutToken[3];
            array[0] = new CustomLayoutToken("~0!", (EntityLayoutParams p, int i) => new LayoutTokenSpec(-10, i, LayoutTileConstraint.Ocean, null, null, null, null, null, null, false, false, 0));
            array[1] = new CustomLayoutToken("{0!", delegate (EntityLayoutParams p, int i)
            {
                int? num = new int?(0);
                return new LayoutTokenSpec(-10, i, LayoutTileConstraint.None, null, null, num, null, null, null, false, false, 0);
            });
            array[2] = new CustomLayoutToken("~~~", (EntityLayoutParams p, int i) => new LayoutTokenSpec(-12, -10, LayoutTileConstraint.Ocean | LayoutTileConstraint.NoRubbleAfterCollapse, null, null, null, null, null, null, false, false, 0));
            EntityLayout entityLayout = entityLayoutParser.ParseLayoutOrThrow(new EntityLayoutParams(predicate, array, false, null, null, null, null, null, null, default(Option<IEnumerable<KeyValuePair<char, int>>>), false, null, null), CargoShipDryDockData.CreatePadding(16, "~~~", "                                                            ", false).Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(11)).Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{9!{9!{9!{9!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(1))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(11))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{9!{9!{9!{9!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(1))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{9!{9!{9!{9!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(1))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{9!{9!{9!{9!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(1))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(21))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(3))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(1))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(1))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)<#A".Repeat(1))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(1))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)<#B".Repeat(1))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{9!{9!{9!{9!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(1))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{9!{9!{9!{9!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(1))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{9!{9!{9!{9!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)<#C".Repeat(1))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{9!{9!{9!{9!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(1))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)<#D".Repeat(1))
                .Concat<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!{4!(4)(4)   ".Repeat(3))
                .Concat<string>(CargoShipDryDockData.CreatePadding(16, "~~~", "                                                            ", true))
                .ToArray<string>());
            Proto.Str str = Proto.CreateStr(ModIds.Machines.CargoShipDryDock, ModTranslation.Get("Cargo Ship Drydock"), ModTranslation.Get("Constructs a dedicated Cargo Ship Drydock, enabling the assembly and deployment of cargo ships to efficiently transport large volumes of resources across the seas."), "place where you build cargo ships");
            string text = "Assets/DryDock/CargoShipDryDock.prefab";
            RelTile3f relTile3f = new RelTile3f(8.5.ToFix32(), 0, 0);
            Option<string> option = "Assets/DryDock/CargoShipDryDock.png";
            ImmutableArray<EmissionParams> empty = ImmutableArray<EmissionParams>.Empty;
            ImmutableArray<ParticlesParams> empty2 = ImmutableArray<ParticlesParams>.Empty;
            Option<string> option2 = "Assets/Base/Buildings/Maintenance/T1/MaintenanceDepotT1Sound.prefab";
            ColorRgba empty3 = ColorRgba.Empty;
            LayoutEntityProto.VisualizedLayers? visualizedLayers = new LayoutEntityProto.VisualizedLayers?(LayoutEntityProto.VisualizedLayers.Empty);
            MachineProto.Gfx gfx = new MachineProto.Gfx(text, reg.GetCategoriesProtos(new Proto.ID[] { Ids.ToolbarCategories.CargoShips }), relTile3f, option, empty2, empty, option2, false, true, default(ImmutableArray<string>), null, false, null, empty3, visualizedLayers);
            CargoShipDryDockProto cargoShipDrydockProto = reg.PrototypesDb.Add<CargoShipDryDockProto>(new CargoShipDryDockProto(ModIds.Machines.CargoShipDryDock, str, entityLayout, 1.Mw(), ModCosts.Machines.CargoShipDryDock.MapToEntityCosts(reg), gfx, null), false);
            RecipeProto recipeProto = reg.RecipeProtoBuilder.Start(ModTranslation.Get("Build Cargo Ship"), new RecipeProto.ID("CargoShipRecipe"), cargoShipDrydockProto)
                .AddInput<RecipeProtoBuilder.State>(250, Ids.Products.Steel, "A", false)
                .AddInput<RecipeProtoBuilder.State>(100, Ids.Products.ConstructionParts2, "B", false)
                .AddInput<RecipeProtoBuilder.State>(150, Ids.Products.MechanicalParts, "C", false)
                .AddInput<RecipeProtoBuilder.State>(25, Ids.Products.Electronics2, "D", false)
                .AddOutput<RecipeProtoBuilder.State>(1, ModIds.Products.CargoShipsQueue, "*", false, false)
                .EnablePartialExecution(5.Percent())
                .SetDurationSeconds(900)
                .BuildAndAdd();

            Log.Info("[IndustrialExpansion] CargoShipDryDockData : Drydock prototype registered.");
        }
    }
}