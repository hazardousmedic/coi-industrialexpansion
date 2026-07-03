using Mafi;
using Mafi.Base;
using Mafi.Base.Prototypes.Research;
using Mafi.Core.Mods;
using Mafi.Core.Research;
using MafiBuilder = Mafi.Core.Research.ResearchNodeProtoBuilderExtensions;

namespace IndustrialExpansion.Research
{
    internal class SurfaceResearchData :IModData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {
            Log.Info("[IndustrialExpansion] SurfaceResearchData : Registering extra surface research.");

            SurfaceResearchData.RegisterNewSurfaces(registrator);
        }

        // More Surfaces Research Registration
        public static void RegisterNewSurfaces(ProtoRegistrator registrator)
        {
            // More Surfaces Research Registration

            var darkconcrete = registrator.ResearchNodeProtoBuilder
                .Start(ModTranslation.Get("Darker!"), ModIds.Research.Darker, ResearchCosts.CustomSurfaces, "title of a research node in the research tree")
                .Description(ModTranslation.Get("Unlocks new darker variant of the concrete surfaces."), "description of a research node in the research tree")
                .SetGridPosition(new Vector2i(32, 3))
                .AddParents(new ResearchNodeProto[] { registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.CustomSurfaces) });

            MafiBuilder.AddSurfaceToUnlock(darkconcrete, ModIds.TerrainMaterials.DarkConcrete);

            ResearchNodeProto darkconcretenode = darkconcrete.BuildAndAdd();

            var darkreinforcedconcrete = registrator.ResearchNodeProtoBuilder
                .Start(ModTranslation.Get("Darker to!"), ModIds.Research.Darkerto, ResearchCosts.CustomSurfaces, "title of a research node in the research tree")
                .Description(ModTranslation.Get("Unlocks new darker variant of the reinforced surfaces."), "description of a research node in the research tree")
                .SetGridPosition(new Vector2i(132, 18))
                .AddParents(new ResearchNodeProto[] { registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.RocketAssemblyAndLaunch) });

            MafiBuilder.AddSurfaceToUnlock(darkreinforcedconcrete, ModIds.TerrainMaterials.DarkReinforcedConcrete);

            ResearchNodeProto darkreinforcedconcretenode = darkreinforcedconcrete.BuildAndAdd();

            Log.Info("[IndustrialExpansion] SurfaceResearchData : Extra surface research registered.");

        }
    }
}
