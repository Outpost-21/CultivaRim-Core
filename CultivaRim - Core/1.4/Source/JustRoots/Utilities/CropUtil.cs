using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace CultivaRim
{
    public static class CropUtil
    {
        public static Dictionary<ThingDef, List<ThingDef>> cropDropDict = new Dictionary<ThingDef, List<ThingDef>>();

        public static List<ThingDef> CalculateCropFromHarvestable(this ThingDef harvested)
        {
            if (cropDropDict.NullOrEmpty())
            {
                foreach(ThingDef thing in DefDatabase<ThingDef>.AllDefs.Where(t => t.IsPlant && t.plant.harvestedThingDef != null))
                {
                    CompProperties_CropInfo modExt = thing.GetCompProperties<CompProperties_CropInfo>();
                    if (modExt == null || modExt.unlockedByProduct)
                    {
                        if (!cropDropDict.ContainsKey(thing.plant.harvestedThingDef))
                        {
                            cropDropDict.Add(thing.plant.harvestedThingDef, new List<ThingDef>() { thing });
                        }
                        else
                        {
                            cropDropDict[thing.plant.harvestedThingDef].Add(thing);
                        }
                    }
                }
            }

            return cropDropDict[harvested];
        }

        public static CropData CropData(this ThingDef crop)
        {
            return GameCompUtil.gameComp_cropData.GetCropData(crop);
        }

        public static void CropUnlock(this ThingDef crop)
        {
            crop.CropData().known = true;
        }

        public static float CropGrowthBonus(this ThingDef crop, bool raining = false)
        {
            CropData data = crop.CropData();
            float result = data.StatGrowthSpeedRaw;
            if (raining)
            {
                result += data.StatRainGrowthRaw;
            }
            return result;
        }

        public static float CropYieldBonus(this ThingDef crop)
        {
            CropData data = crop.CropData();
            float result = data.StatProductYieldRaw;
            return result;
        }

        public static float CropHeatResistBonus(this ThingDef crop)
        {
            CropData data = crop.CropData();
            float result = data.StatHeatResistRaw;
            return result;
        }

        public static float CropColdResistBonus(this ThingDef crop)
        {
            CropData data = crop.CropData();
            float result = data.StatColdResistRaw;
            return result;
        }
    }
}
