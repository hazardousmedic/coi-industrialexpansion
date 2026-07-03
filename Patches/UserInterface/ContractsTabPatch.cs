using System;
using System.Linq;
using HarmonyLib;
using Mafi.Core.Economy;
using Mafi.Core.World.Contracts;
using Mafi.Unity.Ui;
using Mafi.Unity.Ui.Trading;
using Mafi.Unity.Ui.World;

namespace IndustrialExpansion.Patches.UserInterface
{
    [HarmonyPatch(typeof(ContractsTab))]
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyPatch(new Type[]
{
    typeof(UiContext),
    typeof(ContractsManager),
    typeof(SourceProductsAnalyzer),
    typeof(MessagesWindow.Controller),
    typeof(WorldMapWindow.Controller)
})]
    [HarmonyPatchCategory("ContractsTabPatch")]
    public static class ContractsTabPatch
    {
        [HarmonyPostfix]
        public static void ConstructorPostfix(ref int ___m_shipSize)
        {
            bool flag = !new int[] { 2, 4, 6, 8, 10, 12 }.Contains(___m_shipSize);
            if (flag)
            {
                ___m_shipSize = 4;
            }
        }
    }
}
