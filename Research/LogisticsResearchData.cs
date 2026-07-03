using Mafi;
using Mafi.Base.Prototypes.Research;
using Mafi.Base;
using Mafi.Core.Mods;
using Mafi.Core.Research;
using MafiBuilder = Mafi.Core.Research.ResearchNodeProtoBuilderExtensions;

namespace IndustrialExpansion.Research
{
    internal class LogisticsResearchData : IModData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {
            LogisticsResearchData.RegisterNewLogistics(registrator);
        }

        private static void RegisterNewLogistics(ProtoRegistrator registrator)
        {
            // Extra Transports T4 Check
            if (!ExtraTransportsCompat.ExtraTransportsHasT4Research(registrator))
            {
                Log.Info("[IndustrialExpansion] LogisticsResearchData : Registering Tier IV Conveyor research.");

                var t4c = registrator.ResearchNodeProtoBuilder
                    .Start(ModTranslation.Get("Conveyor Belts IV"), ModIds.Research.ConveyorBeltsT4, ResearchCosts.ConveyorBeltsT3 * 2, "title of a research node in the research tree")
                    .Description(ModTranslation.Get("Conveyor belts with an increase throughput."), "description of a research node in the research tree")
                    .SetGridPosition(new Vector2i(124, 11))
                    .AddParents(new ResearchNodeProto[] { registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.ConveyorBeltsT3) });
                MafiBuilder.AddTransportToUnlock(t4c, ModIds.Transports.LooseMaterialConveyorT4);
                MafiBuilder.AddTransportToUnlock(t4c, ModIds.Transports.FlatConveyorT4);
                t4c.BuildAndAdd();

                Log.Info("[IndustrialExpansion] LogisticsResearchData : Tier IV Conveyor research registered.");
            }
            else
            {
                Log.Info("[IndustrialExpansion] LogisticsResearchData : Detected Tier IV Conveyor research from ExtraTransports, skipping Industrial Expansion Tier IV conveyor research.");
            }

            Log.Info("[IndustrialExpansion] LogisticsResearchData : Registering More Balancers research.");

            var morebalancer = registrator.ResearchNodeProtoBuilder
                .Start(ModTranslation.Get("More Balancers"), ModIds.Research.MoreBalancer, ResearchCosts.TransportsBalancing * 2, "title of a research node in the research tree")
                .Description(ModTranslation.Get("Unlocks advanced logistics components, including 4-Way, 10-Way, and 12-Way Balancers for improved distribution and throughput management."), "description of a research node in the research tree")
                .SetGridPosition(new Vector2i(36, 11))
                .AddParents(new ResearchNodeProto[] { registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.TransportsBalancing) });

            MafiBuilder.AddLayoutEntityToUnlock(morebalancer, ModIds.Balancers.SmallFlatBalancer);
            MafiBuilder.AddLayoutEntityToUnlock(morebalancer, ModIds.Balancers.LargeFlatBalancer);
            MafiBuilder.AddLayoutEntityToUnlock(morebalancer, ModIds.Balancers.MediumFlatBalancer);
            MafiBuilder.AddLayoutEntityToUnlock(morebalancer, ModIds.Balancers.LongFlatBalancer);

            MafiBuilder.AddLayoutEntityToUnlock(morebalancer, ModIds.Balancers.SmallUShapeBalancer);
            MafiBuilder.AddLayoutEntityToUnlock(morebalancer, ModIds.Balancers.LargeUShapeBalancer);
            MafiBuilder.AddLayoutEntityToUnlock(morebalancer, ModIds.Balancers.MediumUShapeBalancer);
            MafiBuilder.AddLayoutEntityToUnlock(morebalancer, ModIds.Balancers.LongUShapeBalancer);

            MafiBuilder.AddLayoutEntityToUnlock(morebalancer, ModIds.Balancers.SmallPipeBalancer);
            MafiBuilder.AddLayoutEntityToUnlock(morebalancer, ModIds.Balancers.LargePipeBalancer);
            MafiBuilder.AddLayoutEntityToUnlock(morebalancer, ModIds.Balancers.MediumPipeBalancer);
            MafiBuilder.AddLayoutEntityToUnlock(morebalancer, ModIds.Balancers.LongPipeBalancer);

            morebalancer.BuildAndAdd();

            Log.Info("[IndustrialExpansion] LogisticsResearchData: More Balancers Research registered.");
        }
    }
}