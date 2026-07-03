using Mafi;
using Mafi.Base;
using Mafi.Base.Prototypes.Buildings;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Localization;
using System.Collections.Generic;

namespace IndustrialExpansion.Data
{
    internal class RetainingWallData : IModData
    {

        public void RegisterData(ProtoRegistrator registrator)
        {
            Log.Info("[IndustrialExpansion] RetainingWallData : Registering extra retaining wall prototype.");

            this.RegisterRetainingWallMedium(registrator);
        }
        private void RegisterRetainingWallMedium(ProtoRegistrator registrator)
        {
            ProtosDb prototypesDb = registrator.PrototypesDb;

            RetainingWallProto shortWallProto = prototypesDb.GetOrThrow<RetainingWallProto>(Ids.Buildings.RetainingWallStraight1);
            RetainingWallProto longWallProto = prototypesDb.GetOrThrow<RetainingWallProto>(Ids.Buildings.RetainingWallStraight4);

            ImmutableArray<ToolbarEntryData> categoriesProtos = registrator.GetCategoriesProtos(new Proto.ID[] { IdsCore.ToolbarCategories.Terraforming });

            LocStr1 descLocStr = Loc.Str1(ModIds.Buildings.RetainingWallMedium.Value + "__desc", ModTranslation.Get("Prevents terrain from collapsing. Walls can be placed below the surface to prevent terrain collapse during mining, or above terrain to aid with dumping operations. The placement elevation is adjustable. Walls will collapse if they hold more than {0} units of height or if they are overfilled."), "description of retaining wall");
            LocStr formattedDescLocStr = LocalizationManager.CreateAlreadyLocalizedStr(ModIds.Buildings.RetainingWallMedium.Value + "_formatted", descLocStr.Format(RetainingWallData.RETAINED_HEIGHT_TILES.ToString()).Value);

            EntityLayout entityLayout = this.CreateWallLayout(registrator, 2, 1, new string[] { "...", "###", "..." });
            EntityCosts entityCosts = Costs.Buildings.RetainingWall2.MapToEntityCosts(registrator);

            string prefabPath = "Assets/Terrain/RetainingWall_4M/RetainingWall_4M.prefab";
            string iconPath = "Assets/Terrain/RetainingWall_4M/RetainingWall_4M.png";
            ImmutableArray<ToolbarEntryData>? categoriesOpt = new ImmutableArray<ToolbarEntryData>?(categoriesProtos);

            Proto.Str str = Proto.CreateStrFromLocalized(ModIds.Buildings.RetainingWallMedium, Loc.Str(ModIds.Buildings.RetainingWallMedium.Value + "__name", ModTranslation.Get("Retaining wall (medium)"), "name of retaining wall"), formattedDescLocStr);

            RetainingWallProto mediumWallProto = prototypesDb.Add<RetainingWallProto>(
                new RetainingWallProto(
                    ModIds.Buildings.RetainingWallMedium,
                    str,
                    entityLayout,
                    entityCosts,
                    new LayoutEntityProto.Gfx(prefabPath, default(RelTile3f), iconPath, default(ColorRgba), false, null, categoriesOpt, true, false, null, null, default(ImmutableArray<string>), null, int.MaxValue, false, false, null, false)
                ),
                false
            );

            shortWallProto.SetNextTierIndirect(mediumWallProto, true);
            mediumWallProto.SetNextTierIndirect(longWallProto, true);

            Log.Info("[IndustrialExpansion] RetainingWallData : Retaining Wall Medium prototype registered.");
        }

        private EntityLayout CreateWallLayout(ProtoRegistrator registrator, int wallLengthTiles, int collapseThreshold, string[] retainingVerticesLayout)
        {
            string tileToken = "(W)".RepeatString(wallLengthTiles);
            IEntityLayoutParser layoutParser = registrator.LayoutParser;

            CustomLayoutToken[] tokens = new CustomLayoutToken[]
            {
        new CustomLayoutToken("(W)", delegate
        {
            return new LayoutTokenSpec(-6, 1, LayoutTileConstraint.Ground | LayoutTileConstraint.NoRubbleAfterCollapse, null, new int?(-5), new int?(0), null, null, null, false, false, 0);
        })
            };

            return layoutParser.ParseLayoutOrThrow(
                new EntityLayoutParams(
                    null,
                    tokens,
                    false,
                    null,
                    retainingVerticesLayout,
                    (TerrainVertexRel v, char c) => (c != '#') ? v : v.WithExtraConstraint(LayoutTileConstraint.DisableTerrainPhysics),
                    null,
                    new int?(collapseThreshold),
                    null,
                    default(Option<IEnumerable<KeyValuePair<char, int>>>),
                    false,
                    null,
                    null
                ),
                new string[] { tileToken, tileToken }
            );
        }

        public const int RETAINED_HEIGHT_TILES = 5;
    }
}