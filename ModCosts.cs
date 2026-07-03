using Mafi.Base;
using Mafi.Core.Prototypes;

namespace IndustrialExpansion
{
    internal class ModCosts
    {
        // Loose Material Conveyor T4 Cost
        public static EntityCostsTpl LooseMaterialConveyorT4
        {
            get
            {
                return EntityCostsTpl.Build.CP4(60).Product(102, Ids.Products.Rubber).MaintenanceT1(IndustrialExpansion.ModCosts.ConveyorMaintenancePer100T4, IndustrialExpansion.ModCosts.EXTRA_BUFFER_MONTHS)
                    .Priority(4);
            }
        }

        // Flat Conveyor T4 Cost
        public static EntityCostsTpl FlatConveyorT4
        {
            get
            {
                return EntityCostsTpl.Build.CP4(60).Product(102, Ids.Products.Rubber).MaintenanceT1(IndustrialExpansion.ModCosts.ConveyorMaintenancePer100T4, IndustrialExpansion.ModCosts.EXTRA_BUFFER_MONTHS)
                    .Priority(4);
            }
        }

        // U-shape Balancer Cost
        public static EntityCostsTpl SmallUShapeBalancer
        {
            get
            {
                return EntityCostsTpl.Build.CP2(4).Priority(0);
            }
        }
        public static EntityCostsTpl LargeUShapeBalancer
        {
            get
            {
                return EntityCostsTpl.Build.CP2(12).Priority(0);
            }
        }
        public static EntityCostsTpl MediumUShapeBalancer
        {
            get
            {
                return EntityCostsTpl.Build.CP2(10).Priority(0);
            }
        }
        public static EntityCostsTpl LongUShapeBalancer
        {
            get
            {
                return EntityCostsTpl.Build.CP2(12).Priority(0);
            }
        }

        // Flat Balancer Cost
        public static EntityCostsTpl SmallFlatBalancer
        {
            get
            {
                return EntityCostsTpl.Build.CP2(4).Priority(0);
            }
        }
        public static EntityCostsTpl LargeFlatBalancer
        {
            get
            {
                return EntityCostsTpl.Build.CP2(12).Priority(0);
            }
        }
        public static EntityCostsTpl MediumFlatBalancer
        {
            get
            {
                return EntityCostsTpl.Build.CP2(10).Priority(0);
            }
        }
        public static EntityCostsTpl LongFlatBalancer
        {
            get
            {
                return EntityCostsTpl.Build.CP2(12).Priority(0);
            }
        }
        // Pipe Balancer Cost
        public static EntityCostsTpl SmallPipeBalancer
        {
            get
            {
                return EntityCostsTpl.Build.CP2(4).Priority(0);
            }
        }
        public static EntityCostsTpl LargePipeBalancer
        {
            get
            {
                return EntityCostsTpl.Build.CP2(12).Priority(0);
            }
        }
        public static EntityCostsTpl MediumPipeBalancer
        {
            get
            {
                return EntityCostsTpl.Build.CP2(10).Priority(0);
            }
        }
        public static EntityCostsTpl LongPipeBalancer
        {
            get
            {
                return EntityCostsTpl.Build.CP2(12).Priority(0);
            }
        }

        public static class Buildings
        {
            // Cargodocks Tier 4 and 5 Cargodocks
            public static readonly EntityCostsTpl CargoDepotT5 = EntityCostsTpl.Build.CP2(400).Concrete(700);

            public static readonly EntityCostsTpl CargoDepotT6 = EntityCostsTpl.Build.CP2(500).Concrete(800);

        }

        public static class Machines
        {
            // DryDocks Operation Cost
            public static EntityCostsTpl CargoShipDryDock
            {
                get
                {
                    return EntityCostsTpl.Build.CP(180).Concrete(250).Workers(20)
                        .MaintenanceT1(15);
                }
            }
        }

        public static class CargoShip
        {
            // Cargoship Worker Costs
            public static readonly EntityCostsTpl CargoShipT5 = EntityCostsTpl.Build.Workers(40).Priority(0);

            public static readonly EntityCostsTpl CargoShipT6 = EntityCostsTpl.Build.Workers(46).Priority(0);
        }

        //Maintenance Cost

        public static readonly double ConveyorMaintenancePer100T4 = 2.0;

        public static readonly int EXTRA_BUFFER_MONTHS = 32;
    }
}
