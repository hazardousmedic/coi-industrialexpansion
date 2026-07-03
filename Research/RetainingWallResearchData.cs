using Mafi;
using Mafi.Base;
using Mafi.Base.Prototypes.Research;
using Mafi.Core.Mods;
using Mafi.Core.Research;
using MafiBuilder = Mafi.Core.Research.ResearchNodeProtoBuilderExtensions;


namespace IndustrialExpansion.Research
{
    internal class RetaingWallResearchData : IModData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {
            Log.Info("[IndustrialExpansion] CargoShipDryDockResearchData : Registering More Retaining Wall research.");

            RetaingWallResearchData.RegisterNewRetainingWall(registrator);
        }

        // Retaining Wall Research Registration

        public static void RegisterNewRetainingWall(ProtoRegistrator registrator)
        {
            var newretainingwall = registrator.ResearchNodeProtoBuilder
                .Start(ModTranslation.Get("More Retaining Wall"), ModIds.Research.MoreRetainingWall, ResearchCosts.RetainingWalls, "title of a research node in the research tree")
                .Description("", "description of a research node in the research tree")
                .SetGridPosition(new Vector2i(36, 7))
                .AddParents(new ResearchNodeProto[] { registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.RetainingWalls) });

            MafiBuilder.AddLayoutEntityToUnlock(newretainingwall, ModIds.Buildings.RetainingWallMedium, false);

            ResearchNodeProto newretainingwallnode = newretainingwall.BuildAndAdd();

            Log.Info("[IndustrialExpansion] CargoShipDryDockResearchData : More Retaining Wall Research registered.");

        }
    }
}
