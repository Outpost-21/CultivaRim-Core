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
    [DefOf]
    public static class CultivaRimDefOf
    {
        static CultivaRimDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(CultivaRimDefOf));
        }

        public static ResearchProjectDef CultivaRim_CropEngineering;

        public static CropTableDef CultivaRim_Crops;

        public static ShaderTypeDef CutoutPlant;
    }
}
