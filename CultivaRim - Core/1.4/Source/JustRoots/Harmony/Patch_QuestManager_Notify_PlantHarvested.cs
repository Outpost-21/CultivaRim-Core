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
    [HarmonyPatch(typeof(QuestManager), "Notify_PlantHarvested")]
    public static class Patch_QuestManager_Notify_PlantHarvested
    {
        [HarmonyPostfix]
        public static void Postfix(Pawn worker, Thing harvested)
        {
            List<ThingDef> crops = CropUtil.CalculateCropFromHarvestable(harvested.def);
            for (int i = 0; i < crops.Count; i++)
            {
                GameCompUtil.gameComp_cropData.UnlockCrop(crops[i]);
                GameCompUtil.gameComp_cropData.GetCropData(crops[i]).AddExperience(2f, worker);
            }
        }
    }
}
