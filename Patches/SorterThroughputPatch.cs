using IndustrialExpansion.Extensions;
using HarmonyLib;
using Mafi;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Factory.Sorters;
using Mafi.Core.Ports.Io;
using System;

namespace IndustrialExpansion.Patches

{
    [HarmonyPatch]
    [HarmonyPatchCategory("SorterThroughputPatch")]
    public static class SorterThroughputPatch
    {
        [HarmonyPatch(typeof(Sorter), "recomputeBufferSizeAndDelay")]
        [HarmonyPrefix]
        public static bool RecomputeBufferSizeAndDelayPrefix(
            Sorter __instance,
            ref Duration ___m_delay)
        {
            ImmutableArray<IoPort> ports = __instance.Ports;
            PartialQuantity partialQuantity = PartialQuantity.Zero;
            foreach (IoPort ioPort in ports)
            {
                if (ioPort.IsConnected)
                {
                    partialQuantity = partialQuantity.Max(ioPort.GetMaxThroughputPerTick());
                }
            }

            if (partialQuantity.Value <= 0.5.ToFix32())
                ___m_delay = Sorter.MAX_DELAY;
            else if (partialQuantity.Value <= 1.0.ToFix32())
                ___m_delay = Sorter.MEDIUM_DELAY;
            else if (partialQuantity.Value <= 2.0.ToFix32())
                ___m_delay = Sorter.MIN_DELAY;
            else if (partialQuantity.Value <= 3.0.ToFix32())
                ___m_delay = 3.Ticks();
            else
                ___m_delay = 2.Ticks();

            Fix32 fix = partialQuantity.Value.Min(3.0.ToFix32());
            Quantity quantity = new Quantity(___m_delay.Ticks * fix.ScaledBy(Sorter.BUFFER_SCALE).ToIntCeiled());
            __instance.SetPrivateProperty("MaxBufferSize", quantity);
            return false;
        }

        [HarmonyPatch(typeof(IoPortData), "SendAsMuchAs", new Type[] { typeof(ProductQuantity) })]
        [HarmonyPrefix]
        public static void SendAsMuchAsPrefix(IoPortData __instance, ref ProductQuantity pq, out Quantity __state)
        {
            __state = Quantity.Zero;
            if (!__instance.ConnectedTo.HasValue) return;

            Sorter sorter = __instance.ConnectedTo.Value as Sorter;
            if (sorter == null) return;

            Quantity quantity = sorter.MaxBufferSize - sorter.QuantityInOutputBuffer;
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