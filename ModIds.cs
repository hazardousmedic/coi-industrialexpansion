using Mafi.Core.Entities.Static;
using Mafi.Core.Research;
using Mafi.Core.Products;
using Mafi.Core.Buildings.Cargo;
using Mafi.Core.Buildings.Cargo.Ships;
using Mafi.Core.Factory.Machines;

namespace IndustrialExpansion
{
    public static class ModIds
    {
        public static class Transports
        {
            // U-shape Transports
            public static readonly StaticEntityProto.ID LooseMaterialConveyorT4 = new StaticEntityProto.ID("LooseMaterialConveyorT4");

            // Flat Transports
            public static readonly StaticEntityProto.ID FlatConveyorT4 = new StaticEntityProto.ID("FlatConveyorT4");
        }

        public static class Balancers
        {
            // U-shape Balancers
            public static readonly StaticEntityProto.ID SmallUShapeBalancer = new StaticEntityProto.ID("SmallUShapeBalancer");

            public static readonly StaticEntityProto.ID LargeUShapeBalancer = new StaticEntityProto.ID("LargeUShapeBalancer");

            public static readonly StaticEntityProto.ID MediumUShapeBalancer = new StaticEntityProto.ID("MediumUShapeBalancer");

            public static readonly StaticEntityProto.ID LongUShapeBalancer = new StaticEntityProto.ID("LongUShapeBalancer");

            // Flat Balancers
            public static readonly StaticEntityProto.ID SmallFlatBalancer = new StaticEntityProto.ID("SmallFlatBalancer");

            public static readonly StaticEntityProto.ID LargeFlatBalancer = new StaticEntityProto.ID("LargeFlatBalancer");

            public static readonly StaticEntityProto.ID MediumFlatBalancer = new StaticEntityProto.ID("MediumFlatBalancer");

            public static readonly StaticEntityProto.ID LongFlatBalancer = new StaticEntityProto.ID("LongFlatBalancer");

            //Pipe Balancer
            public static readonly StaticEntityProto.ID SmallPipeBalancer = new StaticEntityProto.ID("SmallPipeBalancer");

            public static readonly StaticEntityProto.ID LargePipeBalancer = new StaticEntityProto.ID("LargePipeBalancer");

            public static readonly StaticEntityProto.ID MediumPipeBalancer = new StaticEntityProto.ID("MediumPipeBalancer");

            public static readonly StaticEntityProto.ID LongPipeBalancer = new StaticEntityProto.ID("LongPipeBalancer");
        }

        public static class Buildings
        {
            // Tier 4 and 5 Cargodocks
            public static readonly CargoDepotProto.ID CargoDepotT5 = new CargoDepotProto.ID("CargoDepotT5");

            public static readonly CargoDepotProto.ID CargoDepotT6 = new CargoDepotProto.ID("CargoDepotT6");

            public static readonly StaticEntityProto.ID RetainingWallMedium = new StaticEntityProto.ID("RetainingWallMedium");
        }

        public static class Ships
        {
            // Tier 4 and 5 Cargoships
            public static readonly CargoShipProto.ID CargoShipT5 = new CargoShipProto.ID("CargoShipT5");

            public static readonly CargoShipProto.ID CargoShipT6 = new CargoShipProto.ID("CargoShipT6");

        }

        public static class TerrainMaterials
        {
            // Darker Concrete Tiles
            public static readonly TerrainMaterialProto.ID DarkConcrete =
                new TerrainMaterialProto.ID("DarkConcrete");

            // Darker Reinforced Concrete Tiles
            public static readonly TerrainMaterialProto.ID DarkReinforcedConcrete =
                new TerrainMaterialProto.ID("DarkReinforcedConcrete");
        }

        public static class Machines
        {
            public static readonly MachineProto.ID CargoShipDryDock = new MachineProto.ID("CargoShipDryDock");
        }

        public static class Products
        {
            // Cargo Ship Product ID
            public static readonly ProductProto.ID CargoShipsQueue = new ProductProto.ID("Product_Virtual_CargoShip");
        }

        public static class Research
        {
            // Tier 4 Conveyor Research
            public static readonly ResearchNodeProto.ID ConveyorBeltsT4 =
                new ResearchNodeProto.ID("ConveyorBeltsT4");

            // More Balancer Research
            public static readonly ResearchNodeProto.ID MoreBalancer =
                new ResearchNodeProto.ID("MoreBalancer");

            // Extra Surfaces Research
            public static readonly ResearchNodeProto.ID Darker =
                new ResearchNodeProto.ID("Darker");

            public static readonly ResearchNodeProto.ID Darkerto =
                new ResearchNodeProto.ID("Darkerto");

            // Tier 4 and 5 Cargoships
            public static readonly ResearchNodeProto.ID MoreShips =
                new ResearchNodeProto.ID("MoreShips");

            // More Retaining Wall
            public static readonly ResearchNodeProto.ID MoreRetainingWall =
                new ResearchNodeProto.ID("MoreRetainingWall");

            // Cargo Ship Dry Dock
            public static readonly ResearchNodeProto.ID BuildCargoShip =
                new ResearchNodeProto.ID("BuildCargoShip");
        }
    }
}