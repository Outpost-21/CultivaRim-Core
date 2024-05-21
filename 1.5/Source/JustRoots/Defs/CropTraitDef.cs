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
        public string texPath;

        public Texture cachedTex;

        public List<string> cropTagsWhitelist = new List<string>();

        public List<string> cropTagsBlacklist = new List<string>();

        public bool repeatable = false;

        public float growthRateOffset = 0f;
        public float productYieldOffset = 0f;
        public float rainGrowthOffset = 0f;

        public override IEnumerable<string> ConfigErrors()
        {
            if (texPath.NullOrEmpty())
            {
                yield return defName + ": texPath is null or empty";
            }
        }

        public override void PostLoad()
        {
            if (!string.IsNullOrEmpty(texPath))
            {
                LongEventHandler.ExecuteWhenFinished(delegate
                {
                    cachedTex = ContentFinder<Texture2D>.Get(texPath);
                });
            }
        }
    }
}
