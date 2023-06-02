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
            if(__instance.LifeStage == PlantLifeStage.Sowing || __instance.PositionHeld.IsPolluted(__instance.MapHeld)) { return; }
            CropData cropData = GameCompUtil.gameComp_cropData.GetCropData(__instance.def);
            if (cropData != null && !cropData.Cultivated)
            {
                // Get mature immature graphic if available
                if (cropData.wildCropImmatureGraphic != null && !__instance.HarvestableNow)
                { __result = cropData.wildCropImmatureGraphic; return; }
                if (cropData.wildCropGraphic != null)
                { __result = cropData.wildCropGraphic; return; }
            }
        }
    }
}
