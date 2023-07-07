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
    public class CropTrait : IExposable
    {
        public CropTraitDef def;

        public int count = 1;

        public void ExposeData()
        {
            Scribe_Values.Look(ref def, "def");
            Scribe_Values.Look(ref count, "count");
        }
    }
}
