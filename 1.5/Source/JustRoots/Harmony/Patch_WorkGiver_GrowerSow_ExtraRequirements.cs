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
    [HarmonyPatch(typeof(WorkGiver_GrowerSow), "ExtraRequirements")]
    public static class Patch_WorkGiver_GrowerSow_ExtraRequirements
    {
        [HarmonyPostfix]
        public static void Postfix(Pawn pawn, IPlantToGrowSettable settable, ref bool __result)
		{
			IntVec3 c;
			if (settable is Zone_Growing zone_Growing)
			{
				if (!zone_Growing.allowSow)
				{
					__result = false;
					return;
				}
				c = zone_Growing.Cells[0];
			}
			else { c = ((Thing)settable).Position; }
			ThingDef plantDef = WorkGiver_Grower.CalculateWantedPlantDef(c, pawn.Map);
			if (!plantDef.CropData().known)
            {
				__result = false;
            }
        }
    }
}
