using Mafi.Core.Mods;
using Mafi;
using Mafi.Base;
using Mafi.Core.Research;
using MafiBuilder = Mafi.Core.Research.ResearchNodeProtoBuilderExtensions;

namespace IndustrialExpansion.Research
{
    internal class ShipResearchData : IModData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {
            Log.Info("[IndustrialExpansion] ShipResearchData : Registering Cargo Depot V & VI research.");

            ShipResearchData.RegisterNewShip(registrator);
        }

        // Ship Research Registration

        public static void RegisterNewShip(ProtoRegistrator registrator)
        {
            var newships = registrator.ResearchNodeProtoBuilder
                .Start(ModTranslation.Get("Cargo Depot V & VI"), ModIds.Research.MoreShips, 250, "title of a research node in the research tree")
                .Description("", "description of a research node in the research tree")
                .SetGridPosition(new Vector2i(148, 22))
                .AddParents(new ResearchNodeProto[] { registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.ResearchLab4) });

            MafiBuilder.AddLayoutEntityToUnlock(newships, ModIds.Buildings.CargoDepotT5, false);
            MafiBuilder.AddLayoutEntityToUnlock(newships, ModIds.Buildings.CargoDepotT6, false);

            ResearchNodeProto newshipsnode = newships.BuildAndAdd();

            Log.Info("[IndustrialExpansion] ShipResearchData : Cargo Depot V & VI research registered.");

        }
    }
}
