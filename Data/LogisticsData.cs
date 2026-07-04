using Mafi;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory.Transports;
using Mafi.Core.Factory.Zippers;
using Mafi.Core.Mods;
using Mafi.Core.Ports.Io;
using Mafi.Core.Prototypes;
using Mafi.Localization;
using System.Collections.Generic;

namespace IndustrialExpansion.Data
{
    internal class LogisticsData : IModData
    {
        // Replace '?' with the actual layout character for the chosen port shape.
        private static string[] SetPortChar(char PortChar, string[] rows)
        {
            string[] result = new string[rows.Length];
            for (int i = 0; i < rows.Length; i++)
                result[i] = rows[i].Replace('?', PortChar);
            return result;
        }

        // Small Balancer Layout
        private static readonly string[] smallbalancerlayout =
        {
            "   ?C+   ",
            "?D+|1|?B+",
            "   ?A+   ",
        };

        // Medium Balancer Layout
        private static readonly string[] mediumbalancerlayout =
        {
            "   ?A+?B+?C+   ",
            "?G+|1||1||1|?I+",
            "?H+|1||1||1|?J+",
            "   ?D+?E+?F+   ",
        };

        // Large Balancer Layout
        private static readonly string[] largebalancerlayout =
        {
            "   ?J+?K+?L+   ",
            "?G+|1||1||1|?C+",
            "?H+|1||1||1|?D+",
            "?I+|1||1||1|?E+",
            "   ?A+?B+?F+   ",
        };

        // Long Balancer Layout
        private static readonly string[] longbalancerlayout =
        {
            "   ?K+?L+   ",
            "?H+|1||1|?A+",
            "?G+|1||1|?B+",
            "?F+|1||1|?C+",
            "?E+|1||1|?D+",
            "   ?J+?I+   ",
        };

        // Prototype Registration
        public void RegisterData(ProtoRegistrator registrator)
        {
            Log.Info("[IndustrialExpansion] LogisticsData : Registering Tier IV Conveyor prototypes.");

            if (!ExtraTransportsCompat.ExtraTransportsHasTier4Conveyors(registrator))
            {
                this.RegisterFlatConveyorT4(registrator);
                this.RegisterLooseMaterialConveyorT4(registrator);
            }
            else
            {
                Log.Info("[IndustrialExpansion] LogisticsData : Extra Transports Tier IV Conveyor prototypes detected, skipping IndustrialExpansion tier IV Conveyor prototypes.");
            }

            // Register Flat Balancer
            IoPortShapeProto shapeFlat = registrator.PrototypesDb
                .GetOrThrow<IoPortShapeProto>(Ids.IoPortShapes.FlatConveyor);

            Log.Info("[IndustrialExpansion] LogisticsData : Registering More Balancers prototypes.");

            this.RegisterBalancer(registrator, ModIds.Balancers.SmallFlatBalancer,
                ModTranslation.Get("Small Flat Balancer"), shapeFlat,
                ModCosts.SmallFlatBalancer, smallbalancerlayout,
                "Assets/Base/MiniZippers/ConnectorFlat.prefab",
                Option<string>.Some("Assets/Logistics/Balancers/Icons/Small_FlatBalancer.png"),
                Ids.ToolbarCategories.Transports_Flat);

            this.RegisterBalancer(registrator, ModIds.Balancers.MediumFlatBalancer,
                ModTranslation.Get("Medium Flat Balancer"), shapeFlat,
                ModCosts.MediumFlatBalancer, mediumbalancerlayout,
                "Assets/Logistics/Balancers/Prefabs/Medium_FlatBalancer.prefab",
                Option<string>.Some("Assets/Logistics/Balancers/Icons/Medium_FlatBalancer.png"),
                Ids.ToolbarCategories.Transports_Flat);

            this.RegisterBalancer(registrator, ModIds.Balancers.LargeFlatBalancer,
                ModTranslation.Get("Large Flat Balancer"), shapeFlat,
                ModCosts.LargeFlatBalancer, largebalancerlayout,
                "Assets/Logistics/Balancers/Prefabs/Large_FlatBalancer.prefab",
                Option<string>.Some("Assets/Logistics/Balancers/Icons/Large_FlatBalancer.png"),
                Ids.ToolbarCategories.Transports_Flat);

            this.RegisterBalancer(registrator, ModIds.Balancers.LongFlatBalancer,
                ModTranslation.Get("Long Flat Balancer"), shapeFlat,
                ModCosts.LongFlatBalancer, longbalancerlayout,
                "Assets/Logistics/Balancers/Prefabs/Long_FlatBalancer.prefab",
                Option<string>.Some("Assets/Logistics/Balancers/Icons/Long_FlatBalancer.png"),
                Ids.ToolbarCategories.Transports_Flat);

            // Register U-shape Balancer
            IoPortShapeProto shapeLoose = registrator.PrototypesDb
                .GetOrThrow<IoPortShapeProto>(Ids.IoPortShapes.LooseMaterialConveyor);

            this.RegisterBalancer(registrator, ModIds.Balancers.SmallUShapeBalancer,
                ModTranslation.Get("Small U-shape Balancer"), shapeLoose,
                ModCosts.SmallUShapeBalancer, smallbalancerlayout,
                "Assets/Base/MiniZippers/ConnectorUShape.prefab",
                Option<string>.Some("Assets/Logistics/Balancers/Icons/Small_UShapeBalancer.png"),
                Ids.ToolbarCategories.Transports_Loose);

            this.RegisterBalancer(registrator, ModIds.Balancers.MediumUShapeBalancer,
                ModTranslation.Get("Medium U-shape Balancer"), shapeLoose,
                ModCosts.MediumUShapeBalancer, mediumbalancerlayout,
                "Assets/Logistics/Balancers/Prefabs/Medium_UShapeBalancer.prefab",
                Option<string>.Some("Assets/Logistics/Balancers/Icons/Medium_UShapeBalancer.png"),
                Ids.ToolbarCategories.Transports_Loose);

            this.RegisterBalancer(registrator, ModIds.Balancers.LargeUShapeBalancer,
                ModTranslation.Get("Large U-shape Balancer"), shapeLoose,
                ModCosts.LargeUShapeBalancer, largebalancerlayout,
                "Assets/Logistics/Balancers/Prefabs/Large_UShapeBalancer.prefab",
                Option<string>.Some("Assets/Logistics/Balancers/Icons/Large_UShapeBalancer.png"),
                Ids.ToolbarCategories.Transports_Loose);

            this.RegisterBalancer(registrator, ModIds.Balancers.LongUShapeBalancer,
                ModTranslation.Get("Longe U-shape Balancer"), shapeLoose,
                ModCosts.LongUShapeBalancer, longbalancerlayout,
                "Assets/Logistics/Balancers/Prefabs/Long_UShapeBalancer.prefab",
                Option<string>.Some("Assets/Logistics/Balancers/Icons/Long_UShapeBalancer.png"),
                Ids.ToolbarCategories.Transports_Loose);

            //Register Pipe Balancer
            IoPortShapeProto shapePipe = registrator.PrototypesDb
                .GetOrThrow<IoPortShapeProto>(Ids.IoPortShapes.Pipe);

            this.RegisterBalancer(registrator, ModIds.Balancers.SmallPipeBalancer,
                ModTranslation.Get("Small Pipe Balancer"), shapePipe,
                ModCosts.SmallPipeBalancer, smallbalancerlayout,
                "Assets/Base/MiniZippers/ConnectorFluid.Prefab",
                Option<string>.Some("Assets/Logistics/Balancers/Icons/Small_PipeBalancer.png"),
                Ids.ToolbarCategories.Transports_Fluid);

            this.RegisterBalancer(registrator, ModIds.Balancers.MediumPipeBalancer,
                ModTranslation.Get("Medium Pipe Balancer"), shapePipe,
                ModCosts.MediumPipeBalancer, mediumbalancerlayout,
                "Assets/Logistics/Balancers/Prefabs/Medium_PipeBalancer.prefab",
                Option<string>.Some("Assets/Logistics/Balancers/Icons/Medium_PipeBalancer.png"),
                Ids.ToolbarCategories.Transports_Fluid);

            this.RegisterBalancer(registrator, ModIds.Balancers.LargePipeBalancer,
                ModTranslation.Get("Large Pipe Balancer"), shapePipe,
                ModCosts.LargePipeBalancer, largebalancerlayout,
                "Assets/Logistics/Balancers/Prefabs/Large_PipeBalancer.prefab",
                Option<string>.Some("Assets/Logistics/Balancers/Icons/Large_PipeBalancer.png"),
                Ids.ToolbarCategories.Transports_Fluid);

            this.RegisterBalancer(registrator, ModIds.Balancers.LongPipeBalancer,
                ModTranslation.Get("Long Pipe Balancer"), shapePipe,
                ModCosts.LongPipeBalancer, longbalancerlayout,
                "Assets/Logistics/Balancers/Prefabs/Long_PipeBalancer.prefab",
                Option<string>.Some("Assets/Logistics/Balancers/Icons/Long_PipeBalancer.png"),
                Ids.ToolbarCategories.Transports_Fluid);

        }

        // Tier 4 Conveyor Prototype
        private void RegisterFlatConveyorT4(ProtoRegistrator registrator)
        {
            var cost = ModCosts.FlatConveyorT4.MapToEntityCosts(registrator);

            ProtosDb prototypesDb = registrator.PrototypesDb;

            TransportProto t3Proto = prototypesDb.GetOrThrow<TransportProto>(Ids.Transports.FlatConveyorT3);
            IoPortShapeProto portShape = prototypesDb.GetOrThrow<IoPortShapeProto>(Ids.IoPortShapes.FlatConveyor);

            ImmutableArray<ToolbarEntryData> categoriesProtos = registrator.GetCategoriesProtos(new Proto.ID[] { Ids.ToolbarCategories.Transports_Flat });
            LocStr locStr = Loc.Str(ModIds.Transports.FlatConveyorT4.Value + "__name", ModTranslation.Get("Flat Conveyor IV"), "unit product conveyor belt");
            LocStr locStr2 = Loc.Str(ModIds.Transports.FlatConveyorT4.Value + "__desc", ModTranslation.Get("Transpots unit or packaged products. Can carry multiple product types at the same time."), "description of flat conveyor belt");
            TransportProto transportProto = new TransportProto(ModIds.Transports.FlatConveyorT4, Proto.CreateStrFromLocalized(ModIds.Transports.FlatConveyorT4, locStr, locStr2), t3Proto.SurfaceRelativeHeight, 3.Quantity(), 0.58.Tiles(), RelTile1f.FromTilesPerSecond(2.9), t3Proto.ZStepLength, t3Proto.NeedsPillarsAtGround, t3Proto.CanBeBuried, t3Proto.TileSurfaceWhenOnGround, t3Proto.MaxPillarSupportRadius, portShape, new Electricity(((float)Costs.Transports.ConveyorT3ElectricityPer100.Value * 1.5f).ToFix32().ToIntCeiled()), t3Proto.CornersSharpnessPercent, t3Proto.AllowMixedProducts, t3Proto.IsBuildable, cost, t3Proto.ConstructionDurationPerProduct, this.CreateFlatConveyorT4Gfx(t3Proto, categoriesProtos));
            prototypesDb.Add<TransportProto>(transportProto, false);
            t3Proto.SetNextTier(transportProto, true);
            Log.Info("[IndustrialExpansion] LogisticsData : Flat Conveyor IV prototype registered.");
        }
        private void RegisterLooseMaterialConveyorT4(ProtoRegistrator registrator)
        {
            var cost = ModCosts.LooseMaterialConveyorT4.MapToEntityCosts(registrator);

            ProtosDb prototypesDb = registrator.PrototypesDb;

            TransportProto t3Proto = prototypesDb.GetOrThrow<TransportProto>(Ids.Transports.LooseMaterialConveyorT3);
            IoPortShapeProto portShape = prototypesDb.GetOrThrow<IoPortShapeProto>(Ids.IoPortShapes.LooseMaterialConveyor);

            ImmutableArray<ToolbarEntryData> categoriesProtos = registrator.GetCategoriesProtos(new Proto.ID[] { Ids.ToolbarCategories.Transports_Loose });
            LocStr locStr = Loc.Str(ModIds.Transports.LooseMaterialConveyorT4.Value + "__name", ModTranslation.Get("U-shape Conveyor IV"), "loose material conveyor belt");
            LocStr locStr2 = Loc.Str(ModIds.Transports.LooseMaterialConveyorT4.Value + "__desc", ModTranslation.Get("Transports bulk materials. Can carry multiple product types at the same time."), "description of u-shaped conveyor belt");
            TransportProto transportProto = new TransportProto(ModIds.Transports.LooseMaterialConveyorT4, Proto.CreateStrFromLocalized(ModIds.Transports.LooseMaterialConveyorT4, locStr, locStr2), t3Proto.SurfaceRelativeHeight, 3.Quantity(), 0.58.Tiles(), RelTile1f.FromTilesPerSecond(2.9), t3Proto.ZStepLength, t3Proto.NeedsPillarsAtGround, t3Proto.CanBeBuried, t3Proto.TileSurfaceWhenOnGround, t3Proto.MaxPillarSupportRadius, portShape, new Electricity(((float)Costs.Transports.ConveyorT3ElectricityPer100.Value * 1.5f).ToFix32().ToIntCeiled()), t3Proto.CornersSharpnessPercent, t3Proto.AllowMixedProducts, t3Proto.IsBuildable, cost, t3Proto.ConstructionDurationPerProduct, this.CreateLooseMaterialConveyorT4Gfx(t3Proto, categoriesProtos));
            prototypesDb.Add<TransportProto>(transportProto, false);
            t3Proto.SetNextTier(transportProto, true);
            Log.Info("[IndustrialExpansion] LogisticsData : U-shape Conveyor IV prototype registered");
        }

        // Conveyor Renderer
        private TransportProto.Gfx CreateFlatConveyorT4Gfx(TransportProto source, ImmutableArray<ToolbarEntryData> categories)
        {
            return new TransportProto.Gfx(
                source.Graphics.CrossSectionLods,
                source.Graphics.RenderProducts,
                "Assets/Logistics/Conveyors/Tier 4/Materials/FlatConveyorT4.mat",
                source.Graphics.TransportUvLength,
                source.Graphics.RenderTransportedProducts,
                source.Graphics.SoundOnBuildPrefabPath,
                source.Graphics.FlowIndicator,
                source.Graphics.VerticalConnectorPrefabPath,
                source.Graphics.PillarAttachments,
                source.Graphics.UvShiftY,
                new TransportProto.Gfx.TransportInstancedRenderingData(),  // ← new instance!
                source.Graphics.CrossSectionRadius,
                source.Graphics.CrossSectionScale,
                source.Graphics.UsePerProductColoring,
                Option<string>.Some("Assets/Logistics/Conveyors/Tier 4/Icons/FlatConveyorT4.png"),
                true,
                source.Graphics.MaxRenderedLod,
                new ImmutableArray<ToolbarEntryData>?(categories),
                false);
        }
        private TransportProto.Gfx CreateLooseMaterialConveyorT4Gfx(TransportProto source, ImmutableArray<ToolbarEntryData> categories)
        {
            return new TransportProto.Gfx(
                source.Graphics.CrossSectionLods,
                source.Graphics.RenderProducts,
                "Assets/Logistics/Conveyors/Tier 4/Materials/LooseMaterialConveyorT4.mat",
                source.Graphics.TransportUvLength,
                source.Graphics.RenderTransportedProducts,
                source.Graphics.SoundOnBuildPrefabPath,
                source.Graphics.FlowIndicator,
                source.Graphics.VerticalConnectorPrefabPath,
                source.Graphics.PillarAttachments,
                source.Graphics.UvShiftY,
                new TransportProto.Gfx.TransportInstancedRenderingData(),  // ← new instance!
                source.Graphics.CrossSectionRadius,
                source.Graphics.CrossSectionScale,
                source.Graphics.UsePerProductColoring,
                Option<string>.Some("Assets/Logistics/Conveyors/Tier 4/Icons/LooseMaterialConveyorT4.png"),
                true,
                source.Graphics.MaxRenderedLod,
                new ImmutableArray<ToolbarEntryData>?(categories),
                false);
        }

        // More Balancer Prototype 
        private void RegisterBalancer(
            ProtoRegistrator registrator,
            StaticEntityProto.ID id,
            string name,
            IoPortShapeProto portShape,
            EntityCostsTpl costs,
            string[] layoutRows,
            string prefabPath,
            Option<string> iconPath,
            Proto.ID toolbarCategory)
        {
            ProtosDb prototypesDb = registrator.PrototypesDb;

            Proto.Str strings = Proto.CreateStr(
                id, name,
                ModTranslation.Get("Distributes products between connected conveyor belts or pipes, with optional prioritization."),
                "machine that allows splitting and merging of transports"
                );

            Electricity requiredPower = registrator.DisableAllProtoCosts
                ? Electricity.Zero
                : 20.Kw();

            CustomLayoutToken[] customTokens = new CustomLayoutToken[]
            {
                new CustomLayoutToken("|0|", (EntityLayoutParams p, int h) =>
                {
                    return new LayoutTokenSpec(
                        0, h,
                        LayoutTileConstraint.UsingPillar,
                        null,
                        new int?(0),
                        new int?(0),
                        null, null, null, false, false, 0
                    );
                })
            };

            ThicknessIRange? pillarRange = new ThicknessIRange?(
                new ThicknessIRange(0, TransportPillarProto.MAX_PILLAR_HEIGHT.Value - 1)
            );

            var layoutParams = new EntityLayoutParams(
                null,
                customTokens,
                true,
                null, null, null, null, null,
                pillarRange,
                default(Option<IEnumerable<KeyValuePair<char, int>>>),
                false, null, null
            );

            string[] finalRows = SetPortChar(portShape.LayoutChar, layoutRows);
            EntityLayout layout = registrator.LayoutParser.ParseLayoutOrThrow(layoutParams, finalRows);

            ImmutableArray<ToolbarEntryData> categories =
                registrator.GetCategoriesProtos(new Proto.ID[] { toolbarCategory });

            var balancer = new ZipperProto(
                id, strings, layout,
                costs.MapToEntityCosts(registrator),
                requiredPower,
                true,
                new LayoutEntityProto.Gfx(
                    prefabPath,
                    default(RelTile3f),
                    iconPath,
                    ColorRgba.White,
                    hideBlockedPortsIcon: true,
                    null,
                    new ImmutableArray<ToolbarEntryData>?(categories),
                    useInstancedRendering: true,
                    false, null, null,
                    default(ImmutableArray<string>),
                    null, 5, false, false, null, false, false
                )
            );

            prototypesDb.Add(balancer);

            Log.Info($"[IndustrialExpansion] LogisticsData : {id.Value} prototype registered.");
        }

    }
}