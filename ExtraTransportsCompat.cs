using Mafi.Core.Factory.Transports;
using Mafi.Core.Mods;
using Mafi.Core.Research;
using System.Linq;

namespace IndustrialExpansion
{
    internal static class ExtraTransportsCompat
    {
        private static readonly string[] ExtraTransportsModIds = new[]
        {
        "LooseMaterialConveyorT4",
        "FlatConveyorT4",
        "ExtraTransports_ConveyorT5_Loose",
        "ExtraTransports_ConveyorT5_Flat",
        };

        private static readonly string[] ExtraTransportsResearchIds = new[]
        {
            "ConveyorBeltsT4"
        };

        public static bool ExtraTransportsHasTier4Conveyors(ProtoRegistrator registrator)
        {
            // Get all registered transport protos, check if any ID matches
            var allTransports = registrator.PrototypesDb.All<TransportProto>();
            return allTransports.Any(t => ExtraTransportsModIds.Contains(t.Id.Value));
        }

        public static bool ExtraTransportsHasT4Research(ProtoRegistrator registrator)
        {
            // Get all registered research protos, check if any ID matches
            var allResearch = registrator.PrototypesDb.All<ResearchNodeProto>();
            return allResearch.Any(r => ExtraTransportsResearchIds.Contains(r.Id.Value));
        }

    }
}