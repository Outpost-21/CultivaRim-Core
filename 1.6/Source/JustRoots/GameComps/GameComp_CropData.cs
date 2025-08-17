﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace CultivaRim
{
    public class GameComp_CropData : GameComponent
    {
        public Dictionary<string, CropData> cropData = new Dictionary<string, CropData>();

        public Dictionary<string, int> upgradeProgress = new Dictionary<string, int>();

        public Dictionary<string, int> respecProgress = new Dictionary<string, int>();

        public GameComp_CropData(Game game)
        {
            GameCompUtil.gameComp_cropData = this;
            NullCheck();
        }

        public override void StartedNewGame()
        {
            base.StartedNewGame();
            NullCheck();
        }

        public override void GameComponentTick()
        {
            base.GameComponentTick();
        }

        public CropData GetCropData(ThingDef crop)
        {
            if (!cropData.ContainsKey(crop.defName))
            {
                cropData.Add(crop.defName, new CropData { plantDef = crop });
            }
            return cropData[crop.defName];
        }

        public void StartRespec(ThingDef crop)
        {
            respecProgress.Add(crop.defName, GetCropData(crop).CurLevel * 1000);
        }

        public void EndRespec(ThingDef crop, bool dev = false)
        {
            if (respecProgress.ContainsKey(crop.defName))
            {
                respecProgress.Remove(crop.defName);
                CropData data = GetCropData(crop);
                data.speedBoosts = 0;
                data.yieldBoosts = 0;
                data.rainBoosts = 0;
                data.cropTraits = new List<CropTrait>();
            }
            else if (dev)
            {
                CropData data = GetCropData(crop);
                data.speedBoosts = 0;
                data.yieldBoosts = 0;
                data.rainBoosts = 0;
                data.cropTraits = new List<CropTrait>();
            }
        }

        public void StartUpgrade(ThingDef crop)
        {
            upgradeProgress.Add(crop.defName, GetCropData(crop).CurLevel * 1000);
        }

        public void EndUpgrade(ThingDef crop, bool dev = false)
        {
            if (upgradeProgress.ContainsKey(crop.defName))
            {
                upgradeProgress.Remove(crop.defName);
                CropData data = GetCropData(crop);
                data.maxLevel += 10;
            }
            else if (dev)
            {
                CropData data = GetCropData(crop);
                data.maxLevel += 10;
            }
        }

        public void NullCheck()
        {
            if (cropData.NullOrEmpty())
            {
                cropData = new Dictionary<string, CropData>();
            }
            if (respecProgress.NullOrEmpty())
            {
                respecProgress = new Dictionary<string, int>();
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref cropData, "cropData");
            Scribe_Collections.Look(ref respecProgress, "respecProgress");
        }
    }
}
