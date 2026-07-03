using HarmonyLib;
using Mafi.Unity.UiToolkit.Library;
using System;
using System.Linq;

namespace IndustrialExpansion.Patches.UserInterface
{
    [HarmonyPatch(typeof(Dropdown<int>), "SetOptions", new Type[] { typeof(int[]) })]
    [HarmonyPatchCategory("ContractsTabPatch")]
    public static class DropdownSetOptionsPatch
    {
        [HarmonyPrefix]
        public static void SetOptionsPrefix(ref int[] options)
        {
            bool flag = options != null && options.Length == 4 && options.SequenceEqual(new int[] { 2, 4, 6, 8 });
            if (flag)
            {
                options = new int[] { 2, 4, 6, 8, 10, 12 };
            }
        }
    }
}