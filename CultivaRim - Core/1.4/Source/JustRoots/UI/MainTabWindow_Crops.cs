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
    public class MainTabWindow_Crops : MainTabWindow_CropTable
    {
        protected override CropTableDef CropTableDef => CultivaRimDefOf.JustRoots_Crops;
    }
}
