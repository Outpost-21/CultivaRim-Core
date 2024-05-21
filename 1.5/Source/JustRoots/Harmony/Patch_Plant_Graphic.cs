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
    [HarmonyPatch(typeof(Plant), "Graphic", MethodType.Getter)]
    public static class Patch_Plant_Graphic
    {
        [HarmonyPostfix]
        public static void Postfix(Plant __instance, ref Graphic __result)
        {
            CropData cropData = __instance.def.CropData();
            Comp_CropInfo cropComp = __instance.TryGetComp<Comp_CropInfo>();
            if (cropData != null && !cropData.Cultivated && cropComp != null)
            {
                if (__instance.PositionHeld.IsPolluted(__instance.MapHeld))
                {
                    if (cropComp?.ToxWildCropImmatureGraphic != null && !__instance.HarvestableNow)
                    { __result = cropComp?.ToxWildCropImmatureGraphic; return; }
                    if (cropComp?.ToxWildCropGraphic != null && __instance.HarvestableNow)
                    { __result = cropComp?.ToxWildCropGraphic; return; }
                }
                else
                {
                    if (cropComp?.WildCropImmatureGraphic != null && !__instance.HarvestableNow)
                    { __result = cropComp?.WildCropImmatureGraphic; return; }
                    if (cropComp?.WildCropGraphic != null && __instance.HarvestableNow)
                    { __result = cropComp?.WildCropGraphic; return; }
                }
            }
        }
    }
}
