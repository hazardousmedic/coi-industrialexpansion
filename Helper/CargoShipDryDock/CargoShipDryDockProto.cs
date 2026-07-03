using System;
using System.Collections.Generic;
using Mafi;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Prototypes;

namespace IndustrialExpansion.Helper.CargoShipDryDock
{
    public class CargoShipDryDockProto : MachineProto
    {
        public override Type EntityType
        {
            get
            {
                return typeof(CargoShipDryDock);
            }
        }
        public CargoShipDryDockProto(MachineProto.ID id, Proto.Str strings, EntityLayout layout, Electricity electricityConsumed, EntityCosts costs, MachineProto.Gfx graphics, IEnumerable<Tag> tags = null)
            : base(id, strings, layout, costs, electricityConsumed, Computing.Zero, new int?(10), true, ImmutableArray<global::Mafi.Core.Entities.Animations.AnimationParams>.Empty, graphics, null, false, true, new Upoints?(2.Upoints()), tags)
        {
        }
    }
}
