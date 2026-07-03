using IndustrialExpansion.Extensions;
using HarmonyLib;
using Mafi;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Factory.Lifts;
using Mafi.Core.Ports.Io;
using System;

namespace IndustrialExpansion.Patches

{
    [HarmonyPatch]
    [HarmonyPatchCategory("LiftThroughputPatch")]
    public static class LiftThroughputPatch
    {
        [HarmonyPatch(typeof(Lift), "recomputeBufferSizeAndDelay")]
        [HarmonyPrefix]
        public static bool RecomputeBufferSizeAndDelayPrefix(
            Lift __instance,
            LiftProto ___m_proto,
            ref Percent ___m_animationSpeed,
            ref Duration ___m_delay)
        {
            ImmutableArray<IoPort> ports = __instance.Ports;
            PartialQuantity partialQuantity = PartialQuantity.MaxValue;
            foreach (IoPort ioPort in ports)
            {
                if (ioPort.IsConnected)
                {
                    partialQuantity = partialQuantity.Min(ioPort.GetMaxThroughputPerTick());
                }
            }
            if (partialQuantity == PartialQuantity.MaxValue)
            {
                partialQuantity = PartialQuantity.Zero;
            }

            int num;
            if (partialQuantity.Value < 0.2.ToFix32())
                num = 1;
            else if (partialQuantity.Value < 0.6.ToFix32())
                num = 2;
            else if (partialQuantity.Value < 1.5.ToFix32())
                num = 3;
            else if (partialQuantity.Value < 2.5.ToFix32())
                num = 4;
            else
                num = 5;

            ___m_animationSpeed = Percent.Hundred * num;
            if (___m_proto.HeightDelta.IsNegative)
            {
                ___m_animationSpeed = -___m_animationSpeed;
            }
            ___m_delay = (Lift.DELAY_BASE + Lift.DELAY_PER_HEIGHT * ___m_proto.HeightDelta.Value.Abs()) / num;

            Fix32 fix = partialQuantity.Value.Min(3.0.ToFix32());
            Quantity quantity = new Quantity(___m_delay.Ticks * fix.ScaledBy(Lift.BUFFER_SCALE).ToIntCeiled());
            __instance.SetPrivateProperty("MaxBufferSize", quantity);
            return false;
        }

        [HarmonyPatch(typeof(IoPortData), "SendAsMuchAs", new Type[] { typeof(ProductQuantity) })]
        [HarmonyPrefix]
        public static void SendAsMuchAsPrefix(IoPortData __instance, ref ProductQuantity pq, out Quantity __state)
        {
            __state = Quantity.Zero;
            if (!__instance.ConnectedTo.HasValue) return;

            Lift lift = __instance.ConnectedTo.Value as Lift;
            if (lift == null) return;

            Quantity quantity = lift.MaxBufferSize - lift.QuantityInOutputBuffer;
            if (quantity.Value <= 0)
            {
                __state = pq.Quantity;
                pq = pq.WithNewQuantity(Quantity.Zero);
            }
            else if (pq.Quantity > quantity)
            {
                __state = pq.Quantity - quantity;
                pq = pq.WithNewQuantity(quantity);
            }
        }

        [HarmonyPatch(typeof(IoPortData), "SendAsMuchAs", new Type[] { typeof(ProductQuantity) })]
        [HarmonyPostfix]
        public static void SendAsMuchAsPostfix(Quantity __state, ref Quantity __result)
        {
            if (__state.IsPositive)
            {
                __result += __state;
            }
        }
    }
}