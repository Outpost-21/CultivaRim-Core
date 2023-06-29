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
    public static class CultivaRimDebug
    {
        [DebugAction("CultivaRim", "Unlock All Crops", actionType = DebugActionType.Action, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void DebugCommand_UnlockAllCrops()
        {
            foreach(ThingDef crop in DefDatabase<ThingDef>.AllDefs.Where(td => td.IsPlant && td.plant.Harvestable))
            {
                crop.CropUnlock();
            }
        }
        [DebugAction("CultivaRim", "Unlock Crop...", actionType = DebugActionType.Action, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void DebugCommand_UnlockSpecificCrop()
        {
            Find.WindowStack.Add(new Dialog_DebugOptionListLister(DebugOptions_GetAllCropsForUnlocking()));
        }

        public static List<DebugMenuOption> DebugOptions_GetAllCropsForUnlocking()
        {
            List<DebugMenuOption> list = new List<DebugMenuOption>();
            foreach (ThingDef crop in DefDatabase<ThingDef>.AllDefs.Where(td => td.IsPlant && td.plant.Harvestable && !td.CropData().known))
            {
                list.Add(new DebugMenuOption(crop.LabelCap, DebugMenuOptionMode.Action, delegate
                {
                    crop.CropUnlock();
                }));
            }
            return list;
        }

        [DebugAction("CultivaRim", "Increment Crop Level...", actionType = DebugActionType.Action, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void DebugCommand_IncrementCropLevel()
        {
            Find.WindowStack.Add(new Dialog_DebugOptionListLister(DebugOptions_GetAllCropsForLevelling()));
        }

        public static List<DebugMenuOption> DebugOptions_GetAllCropsForLevelling()
        {
            List<DebugMenuOption> list = new List<DebugMenuOption>();
            for (int i = 0; i < GameCompUtil.gameComp_cropData.cropData.Count; i++)
            {
                CropData crop = GameCompUtil.gameComp_cropData.cropData.ElementAt(i).Value;
                if (crop.known)
                {
                    list.Add(new DebugMenuOption(crop.plantDef.LabelCap, DebugMenuOptionMode.Action, delegate
                    {
                        Find.WindowStack.Add(new Dialog_DebugOptionListLister(DebugOptions_LevelIncrementsForCrops(crop)));
                    }));
                }
            }
            return list;
        }

        public static List<DebugMenuOption> DebugOptions_LevelIncrementsForCrops(CropData crop)
        {
            List<DebugMenuOption> list = new List<DebugMenuOption>();
            list.Add(new DebugMenuOption("Add 1", DebugMenuOptionMode.Action, delegate
            {
                LevelCropByAmount(crop, 1);
            }));
            list.Add(new DebugMenuOption("Add 5", DebugMenuOptionMode.Action, delegate
            {
                LevelCropByAmount(crop, 5);
            }));
            list.Add(new DebugMenuOption("Add 10", DebugMenuOptionMode.Action, delegate
            {
                LevelCropByAmount(crop, 10);
            }));
            return list;
        }

        public static void LevelCropByAmount(CropData crop, int increment)
        {
            while(crop.maxLevel < (crop.CurLevel + increment))
            {
                crop.maxLevel += 5;
            }
            for (int i = 0; i < increment; i++)
            {
                crop.AddExperience(crop.ExpForNextLevel);
            }
        }
    }
}
