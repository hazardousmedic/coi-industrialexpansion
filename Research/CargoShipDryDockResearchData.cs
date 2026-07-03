using Mafi.Core.Mods;
using Mafi;
using Mafi.Base;
using Mafi.Core.Research;
using Mafi.Base.Prototypes.Research;
using MafiBuilder = Mafi.Core.Research.ResearchNodeProtoBuilderExtensions;


namespace IndustrialExpansion.Research
{
    internal class CargoShipDryDockResearchData : IModData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {
            Log.Info("[IndustrialExpansion] CargoShipDryDockResearchData : Registering Cargo Ship Drydock research.");

            CargoShipDryDockResearchData.RegisterCargoShipDryDock(registrator);
        }
        private static void RegisterCargoShipDryDock(ProtoRegistrator registrator)
        {
            var drydock = registrator.ResearchNodeProtoBuilder
                .Start(ModTranslation.Get("Cargo Ship Drydock"), ModIds.Research.BuildCargoShip, ResearchCosts.CargoDepot * 2, "title of a research node in the research tree").Description(ModTranslation.Get("Cargo Ship Drydock research enables construction of dedicated cargo ships, expanding your fleet’s capacity for resource transport to support naval operations. It unlocks specialized drydocks, streamlining shipbuilding for logistical efficiency. This upgrade bolsters your factory’s maritime supply chain."), "description of a research node in the research tree").SetGridPosition(new Vector2i(44, 15))
                .AddParents(new ResearchNodeProto[] { registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.CargoDepot) });
                MafiBuilder.AddMachineToUnlock(drydock, ModIds.Machines.CargoShipDryDock, false, false);
                drydock.BuildAndAdd();

            Log.Info("[IndustrialExpansion] CargoShipDryDockResearchData : Cargo Ship Drydock research registered.");
        }
    }
}