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
    public class CultivaRimSettings : ModSettings
    {
        public bool verboseLogging = false;

        public bool newCropVisuals = true;

        // Levelling settings
        public float lvlConstant = 0.07f;
        public float lvlPower = 2;

        // Stat Starts
        public float stat_growthSpeedDebuff = 0.5f;
        public float stat_productYieldDebuff = 0.5f;
        public float stat_coldResistDebuff = 0.5f;
        public float stat_heatResistDebuff = 0.5f;
        public float stat_rainGrowthDebuff = 0.5f;

        // Stat Constants
        public float stat_growthSpeed = 0.05f;
        public float stat_productYield = 0.05f;
        public float stat_coldResist = 0.05f;
        public float stat_heatResist = 0.05f;
        public float stat_rainGrowth = 0.05f;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref newCropVisuals, "newCropVisuals");

            Scribe_Values.Look(ref lvlConstant, "lvlConstant");
            Scribe_Values.Look(ref lvlPower, "lvlPower");

            Scribe_Values.Look(ref stat_growthSpeedDebuff, "stat_growthSpeedDebuff");
            Scribe_Values.Look(ref stat_productYieldDebuff, "stat_productYieldDebuff");
            Scribe_Values.Look(ref stat_coldResistDebuff, "stat_coldResistDebuff");
            Scribe_Values.Look(ref stat_heatResistDebuff, "stat_heatResistDebuff");
            Scribe_Values.Look(ref stat_rainGrowthDebuff, "stat_rainGrowthDebuff");

            Scribe_Values.Look(ref stat_growthSpeed, "stat_growthSpeed");
            Scribe_Values.Look(ref stat_productYield, "stat_productYield");
            Scribe_Values.Look(ref stat_coldResist, "stat_coldResist");
            Scribe_Values.Look(ref stat_heatResist, "stat_heatResist");
            Scribe_Values.Look(ref stat_rainGrowth, "stat_rainGrowth");
        }

        public bool IsValidSetting(string input)
        {
            if (GetType().GetFields().Where(p => p.FieldType == typeof(bool)).Any(i => i.Name == input))
            {
                return true;
            }

            return false;
        }

        public IEnumerable<string> GetEnabledSettings
        {
            get
            {
                return GetType().GetFields().Where(p => p.FieldType == typeof(bool) && (bool)p.GetValue(this)).Select(p => p.Name);
            }
        }
    }
}
