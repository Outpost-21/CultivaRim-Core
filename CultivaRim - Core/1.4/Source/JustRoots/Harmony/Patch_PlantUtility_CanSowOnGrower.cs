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
    [HarmonyPatch(typeof(PlantUtility), "CanSowOnGrower")]
    public static class Patch_PlantUtility_CanSowOnGrower
    {
        [HarmonyPostfix]
        public static void Postfix(ThingDef plantDef, object obj, ref bool __result)
        {
            if (!GameCompUtil.gameComp_cropData.GetCropData(plantDef).known)
            {
                __result = false;
            }
        }
    }
}
