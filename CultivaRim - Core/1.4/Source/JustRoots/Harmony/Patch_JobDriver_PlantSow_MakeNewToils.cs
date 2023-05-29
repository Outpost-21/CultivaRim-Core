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
    [HarmonyPatch(typeof(JobDriver_PlantSow), "MakeNewToils")]
    public static class Patch_JobDriver_PlantSow_MakeNewToils
    {
        [HarmonyPrefix]
        public static void Prefix(JobDriver_PlantSow __instance)
        {
            if(__instance.Plant != null && __instance.sowWorkDone >= __instance.Plant.def.plant.sowWork)
            {
                GameCompUtil.gameComp_cropData.UnlockCrop(__instance.Plant.def);
                GameCompUtil.gameComp_cropData.GetCropData(__instance.Plant.def).AddExperience(2f, __instance.pawn);
            }
        }
    }
}
