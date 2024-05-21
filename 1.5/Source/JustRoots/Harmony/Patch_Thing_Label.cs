using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using HarmonyLib;

namespace CultivaRim
{
    [HarmonyPatch(typeof(Thing), "Label", MethodType.Getter)]
    public static class Patch_Thing_Label
    {
        [HarmonyPostfix]
        public static void Postfix(Thing __instance, ref string __result)
        {
            if (__instance.def.IsPlant && __instance.def.plant.harvestedThingDef != null)
            {
                __result = __instance.def.CropData().Cultivated ? __instance.LabelNoCount : "CultivaRim.WildPrefix".Translate(__instance.LabelNoCount);
            }
        }
    }
}
