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
    public class CropTraitDef : Def
    {
        public List<string> cropTagsWhitelist = new List<string>();

        public List<string> cropTagsBlacklist = new List<string>();

        public bool repeatable = false;

        public float growthRateOffset = 0f;
        public float productYieldOffset = 0f;
        public float coldResistOffset = 0f;
        public float heatResistOffset = 0f;
        public float rainGrowthOffset = 0f;
    }
}
