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
    [StaticConstructorOnStartup]
    public static class CultivaRimStartup
    {
        static CultivaRimStartup()
        {
            AddMissingPlantsToDictionary();
        }

        public static void AddMissingPlantsToDictionary()
        {
            CultivaRimSettings settings = CultivaRimMod.settings;
            List<string> enabledByDefault = new List<string>() { "Plant_Haygrass", "Plant_Potato" };
            if (settings.defaultUnlockedPlants.NullOrEmpty())
            {
                settings.defaultUnlockedPlants = new Dictionary<string, bool>();
            }
            foreach(ThingDef plant in DefDatabase<ThingDef>.AllDefs.Where(p => p.IsPlant))
            {
                if (!settings.defaultUnlockedPlants.ContainsKey(plant.defName) && plant.researchPrerequisites.NullOrEmpty() && plant.plant.Harvestable && plant.plant.Sowable)
                {
                    CompProperties_CropInfo cropInfo = plant.GetCompProperties<CompProperties_CropInfo>();
                    if (cropInfo == null || cropInfo.unlockedByProduct)
                    {
                        settings.defaultUnlockedPlants.Add(plant.defName, enabledByDefault.Contains(plant.defName));
                    }
                }
            }
        }
    }
}
