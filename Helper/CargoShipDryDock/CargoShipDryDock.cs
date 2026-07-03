using Mafi.Core;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Animations;
using Mafi.Core.Entities.Static;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Maintenance;
using Mafi.Core.Population;
using Mafi.Core.Prototypes;
using Mafi.Core.Vehicles;
using Mafi.Serialization;
using System;

namespace IndustrialExpansion.Helper.CargoShipDryDock
{
    public class CargoShipDryDock : Machine
    {
        protected override bool UsesVirtualLimitedBuffers
        {
            get
            {
                return true;
            }
        }

        public CargoShipDryDock(EntityId id, CargoShipDryDockProto proto, TileTransform transform, EntityContext context, VirtualBuffersMap virtualBuffersMap, IUnityConsumerFactory unityConsumerFactory, UnlockedProtosDb unlockedProtosDb, IVehicleBuffersRegistry vehicleBuffersRegistry, IEntityMaintenanceProvidersFactory maintenanceProvidersFactory, IAnimationStateFactory animationStateFactory)
            : base(id, proto, transform, context, virtualBuffersMap, unlockedProtosDb, vehicleBuffersRegistry, maintenanceProvidersFactory, animationStateFactory)
        {
        }

        public static void Serialize(CargoShipDryDock value, BlobWriter writer)
        {
            bool flag = writer.TryStartClassSerialization<CargoShipDryDock>(value);
            if (flag)
            {
                writer.EnqueueDataSerialization(value, CargoShipDryDock.s_serializeDataDelayedAction);
            }
        }

        public new static CargoShipDryDock Deserialize(BlobReader reader)
        {
            CargoShipDryDock CargoShipDryDock;
            bool flag = reader.TryStartClassDeserialization<CargoShipDryDock>(out CargoShipDryDock, null, null, false);
            if (flag)
            {
                reader.EnqueueDataDeserialization(CargoShipDryDock, CargoShipDryDock.s_deserializeDataDelayedAction, null);
            }
            return CargoShipDryDock;
        }

        private static readonly Action<object, BlobWriter> s_serializeDataDelayedAction = delegate (object obj, BlobWriter writer)
        {
            ((CargoShipDryDock)obj).SerializeData(writer);
        };

        private static readonly Action<object, BlobReader> s_deserializeDataDelayedAction = delegate (object obj, BlobReader reader)
        {
            ((CargoShipDryDock)obj).DeserializeData(reader);
        };
    }
}
