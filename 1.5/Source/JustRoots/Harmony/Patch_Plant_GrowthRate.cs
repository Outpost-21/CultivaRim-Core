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
    [HarmonyPatch(typeof(Plant), "GrowthRate", MethodType.Getter)]
    public static class Patch_Plant_GrowthRate
    {
        [HarmonyPostfix]
        public static void Postfix(Plant __instance, ref float __result)
        {
            float outcome = __result;
            if (outcome > 0f && __instance.IsCrop)
            {
                WeatherDef curWeather = __instance.Map.weatherManager.CurWeatherLerped;
                bool isWetWeather = curWeather.weatherThought != null && curWeather.weatherThought == ThoughtDefOf.SoakingWet;
                outcome *= 1f + __instance.def.CropGrowthBonus(isWetWeather) - CultivaRimMod.settings.stat_growthSpeedDebuff;
            }
            __result =  outcome;
        }
    }
}
