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

        public static List<ThingDef> CalculateCropFromHarvestable(ThingDef harvested)
        {
            if (cropDropDict.NullOrEmpty())
            {
                foreach(ThingDef thing in DefDatabase<ThingDef>.AllDefs.Where(t => t.IsPlant && t.plant.harvestedThingDef != null))
                {
                    DefModExt_CropInfo modExt = thing.GetModExtension<DefModExt_CropInfo>();
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
    }
}
