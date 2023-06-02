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

        // Levelling settings
        public float lvlConstant = 0.07f;
        public float lvlPower = 2;

        // Stat Constants
        public float stat_growthSpeed = 0.05f;
        public float stat_productYield = 0.05f;
        public float stat_coldResist = 0.05f;
        public float stat_heatResist = 0.05f;
        public float stat_rainGrowth = 0.05f;

        public override void ExposeData()
        {
            base.ExposeData();
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
