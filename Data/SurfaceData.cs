using Mafi;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Terrain;
using Mafi.Core.Prototypes;

namespace IndustrialExpansion.Data
{ 
    internal class SurfaceData : IModData
    {
        // Dark Concrete Surface Paths
        private static readonly string[] VanillaConcreteNormalPaths = new[]
        {
            "Assets/base/terrain/surfaces/concrete/concreteBlock1a-256-normals.png",
            "Assets/base/terrain/surfaces/concrete/concreteBlock1b-256-normals.png",
            "Assets/base/terrain/surfaces/concrete/concreteBlock1c-256-normals.png",
            "Assets/base/terrain/surfaces/concrete/concreteBlock1d-256-normals.png",
            "Assets/base/terrain/surfaces/concrete/concreteBlock1e-256-normals.png",
            "Assets/base/terrain/surfaces/concrete/concreteBlock1f-256-normals.png",
            "Assets/base/terrain/surfaces/concrete/concreteBlock1g-256-normals.png",
            "Assets/base/terrain/surfaces/concrete/concreteBlock1h-256-normals.png",
        };

        private static readonly string[] VanillaConcreteSmoothmetal = new[]
        {
            "Assets/base/terrain/surfaces/concrete/concreteBlock1a-256-smoothmetal.png",
            "Assets/base/terrain/surfaces/concrete/concreteBlock1b-256-smoothmetal.png",
            "Assets/base/terrain/surfaces/concrete/concreteBlock1c-256-smoothmetal.png",
            "Assets/base/terrain/surfaces/concrete/concreteBlock1d-256-smoothmetal.png",
            "Assets/base/terrain/surfaces/concrete/concreteBlock1e-256-smoothmetal.png",
            "Assets/base/terrain/surfaces/concrete/concreteBlock1f-256-smoothmetal.png",
            "Assets/base/terrain/surfaces/concrete/concreteBlock1g-256-smoothmetal.png",
            "Assets/base/terrain/surfaces/concrete/concreteBlock1h-256-smoothmetal.png",
        };

        private static readonly string[] DarkConcreteAlbedoPaths = new[]
        {
            "Assets/Surfaces/DarkConcrete/Textures/Main/DarkConcrete_0.png",
            "Assets/Surfaces/DarkConcrete/Textures/Main/DarkConcrete_1.png",
            "Assets/Surfaces/DarkConcrete/Textures/Main/DarkConcrete_2.png",
            "Assets/Surfaces/DarkConcrete/Textures/Main/DarkConcrete_3.png",
            "Assets/Surfaces/DarkConcrete/Textures/Main/DarkConcrete_4.png",
            "Assets/Surfaces/DarkConcrete/Textures/Main/DarkConcrete_5.png",
            "Assets/Surfaces/DarkConcrete/Textures/Main/DarkConcrete_6.png",
            "Assets/Surfaces/DarkConcrete/Textures/Main/DarkConcrete_7.png",
        };

        private const string DarkConcreteIconPath =
            "Assets/Surfaces/DarkConcrete/Icons/DarkConcreteIcon.png";

        private static readonly TileSurfacesEdgesSpec DarkConcreteEdgesSpec = new TileSurfacesEdgesSpec(
            "Assets/Surfaces/DarkConcrete/Textures/Edges/A-albedo.png",
            "Assets/base/terrain/surfaces/concreteedges/A-normals.png",
            "Assets/base/terrain/surfaces/concreteedges/A-smoothmetal.png",
            "Assets/Surfaces/DarkConcrete/Textures/Edges/B-albedo.png",
            "Assets/base/terrain/surfaces/concreteedges/B-normals.png",
            "Assets/base/terrain/surfaces/concreteedges/B-smoothmetal.png",
            "Assets/Surfaces/DarkConcrete/Textures/Edges/C-albedo.png",
            "Assets/base/terrain/surfaces/concreteedges/C-normals.png",
            "Assets/base/terrain/surfaces/concreteedges/C-smoothmetal.png",
            "Assets/Surfaces/DarkConcrete/Textures/Edges/D-albedo.png",
            "Assets/base/terrain/surfaces/concreteedges/D-normals.png",
            "Assets/base/terrain/surfaces/concreteedges/D-smoothmetal.png"
        );

        // Dark Reinforced Concrete Paths
        private static readonly string[] VanillaReinforcedConcreteNormalPaths = new[]
        {
            "Assets/base/terrain/surfaces/concretereinforced/ReinforcedConcrete1a-256-normals.png",
            "Assets/base/terrain/surfaces/concretereinforced/ReinforcedConcrete1b-256-normals.png",
            "Assets/base/terrain/surfaces/concretereinforced/ReinforcedConcrete1c-256-normals.png",
            "Assets/base/terrain/surfaces/concretereinforced/ReinforcedConcrete1d-256-normals.png",
            "Assets/base/terrain/surfaces/concretereinforced/ReinforcedConcrete1e-256-normals.png",
            "Assets/base/terrain/surfaces/concretereinforced/ReinforcedConcrete1f-256-normals.png",
            "Assets/base/terrain/surfaces/concretereinforced/ReinforcedConcrete1g-256-normals.png",
            "Assets/base/terrain/surfaces/concretereinforced/ReinforcedConcrete1h-256-normals.png",
        };

        private static readonly string[] VanillaReinforcedConcreteSmoothMetalPaths = new[]
        {
            "Assets/base/terrain/surfaces/concretereinforced/ReinforcedConcrete1a-256-smoothmetal.png",
            "Assets/base/terrain/surfaces/concretereinforced/ReinforcedConcrete1b-256-smoothmetal.png",
            "Assets/base/terrain/surfaces/concretereinforced/ReinforcedConcrete1c-256-smoothmetal.png",
            "Assets/base/terrain/surfaces/concretereinforced/ReinforcedConcrete1d-256-smoothmetal.png",
            "Assets/base/terrain/surfaces/concretereinforced/ReinforcedConcrete1e-256-smoothmetal.png",
            "Assets/base/terrain/surfaces/concretereinforced/ReinforcedConcrete1f-256-smoothmetal.png",
            "Assets/base/terrain/surfaces/concretereinforced/ReinforcedConcrete1g-256-smoothmetal.png",
            "Assets/base/terrain/surfaces/concretereinforced/ReinforcedConcrete1h-256-smoothmetal.png",
        };

        private static readonly string[] DarkReinforcedConcreteAlbedoPaths = new[]
        {
            "Assets/Surfaces/DarkReinforcedConcrete/Textures/Main/DarkReinforcedConcrete_0.png",
            "Assets/Surfaces/DarkReinforcedConcrete/Textures/Main/DarkReinforcedConcrete_1.png",
            "Assets/Surfaces/DarkReinforcedConcrete/Textures/Main/DarkReinforcedConcrete_2.png",
            "Assets/Surfaces/DarkReinforcedConcrete/Textures/Main/DarkReinforcedConcrete_3.png",
            "Assets/Surfaces/DarkReinforcedConcrete/Textures/Main/DarkReinforcedConcrete_4.png",
            "Assets/Surfaces/DarkReinforcedConcrete/Textures/Main/DarkReinforcedConcrete_5.png",
            "Assets/Surfaces/DarkReinforcedConcrete/Textures/Main/DarkReinforcedConcrete_6.png",
            "Assets/Surfaces/DarkReinforcedConcrete/Textures/Main/DarkReinforcedConcrete_7.png",
        };

        private const string DarkReinforcedConcreteIconPath =
            "Assets/Surfaces/DarkReinforcedConcrete/Icons/DarkReinforcedConcreteIcon.png";

        private static readonly TileSurfacesEdgesSpec DarkReinforcedConcreteEdgesSpec = new TileSurfacesEdgesSpec(
            "Assets/Surfaces/DarkReinforcedConcrete/Textures/Edges/A-albedo.png",
            "Assets/base/terrain/surfaces/concretereinforcededges/A_normals.png",
            "Assets/base/terrain/surfaces/concretereinforcededges/A_smoothmetal.png",
            "Assets/Surfaces/DarkReinforcedConcrete/Textures/Edges/B-albedo.png",
            "Assets/base/terrain/surfaces/concretereinforcededges/B_normals.png",
            "Assets/base/terrain/surfaces/concretereinforcededges/B_smoothmetal.png",
            "Assets/Surfaces/DarkReinforcedConcrete/Textures/Edges/C-albedo.png",
            "Assets/base/terrain/surfaces/concretereinforcededges/C_normals.png",
            "Assets/base/terrain/surfaces/concretereinforcededges/C_smoothmetal.png",
            "Assets/Surfaces/DarkReinforcedConcrete/Textures/Edges/D-albedo.png",
            "Assets/base/terrain/surfaces/concretereinforcededges/D_normals.png",
            "Assets/base/terrain/surfaces/concretereinforcededges/D_smoothmetal.png"
        );

        public void RegisterData(ProtoRegistrator registrator)
        {
            Log.Info("[IndustrialExpansion] SurfacesData : Registering extra surfaces data.");

            RegisterDarkConcrete(registrator);
            RegisterDarkReinforcedConcrete(registrator);
        }

        private void RegisterDarkConcrete(ProtoRegistrator registrator)
        {
            ProtosDb prototypesDb = registrator.PrototypesDb;

            ProductProto concreteSlab = prototypesDb.GetOrThrow<ProductProto>(Ids.Products.ConcreteSlab);

            var textureSpec = new TileSurfaceTextureSpec(
                DarkConcreteAlbedoPaths.ToImmutableArray(),
                VanillaConcreteNormalPaths.ToImmutableArray(),
                VanillaConcreteSmoothmetal.ToImmutableArray(),
                false);

            var gfx = new TerrainTileSurfaceProto.Gfx(
                textureSpec,
                DarkConcreteEdgesSpec,
                0.25f,
                null,
                DarkConcreteIconPath);

            var compatibleNeighbours = ImmutableArray.Create<Proto.ID>();

            prototypesDb.Add(new TerrainTileSurfaceProto(
                ModIds.TerrainMaterials.DarkConcrete,
                Proto.CreateStr(
                    ModIds.TerrainMaterials.DarkConcrete,
                    ModTranslation.Get("Concrete surface (Darker)"),
                    null,
                    ModTranslation.Get("A darker variant of the standard concrete surface. Same durability and cost.")),
                Percent.FromFloat(0.8f),
                concreteSlab.WithQuantity(1),
                true,
                compatibleNeighbours,
                gfx));

            Log.Info("[IndustrialExpansion] SurfacesData : Dark concrete surface registered.");
        }

        private void RegisterDarkReinforcedConcrete(ProtoRegistrator registrator)
        {
            ProtosDb prototypesDb = registrator.PrototypesDb;

            ProductProto concreteSlab = prototypesDb.GetOrThrow<ProductProto>(Ids.Products.ConcreteSlab);

            var textureSpec = new TileSurfaceTextureSpec(
                DarkReinforcedConcreteAlbedoPaths.ToImmutableArray(),
                VanillaReinforcedConcreteNormalPaths.ToImmutableArray(),
                VanillaReinforcedConcreteSmoothMetalPaths.ToImmutableArray(),
                false);

            var gfx = new TerrainTileSurfaceProto.Gfx(
                textureSpec,
                DarkReinforcedConcreteEdgesSpec,
                0.25f,
                null,
                DarkReinforcedConcreteIconPath);

            var compatibleNeighbours = ImmutableArray.Create<Proto.ID>();

            prototypesDb.Add(new TerrainTileSurfaceProto(
                ModIds.TerrainMaterials.DarkReinforcedConcrete,
                Proto.CreateStr(
                    ModIds.TerrainMaterials.DarkReinforcedConcrete,
                    ModTranslation.Get("Reinforced surface (Darker)"),
                    null,
                    ModTranslation.Get("A darker variant of the standard reinforced surface. Same durability and cost.")),
                Percent.FromFloat(0.8f),
                concreteSlab.WithQuantity(1),
                true,
                compatibleNeighbours,
                gfx));

            Log.Info("[IndustrialExpansion] SurfacesData : Dark reinforced surface registered.");
        }
    }
}