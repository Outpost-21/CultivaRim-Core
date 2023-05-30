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
        public CropTraitWorker cropTraitWorker;

        public List<string> cropTagsWhitelist = new List<string>();

        public List<string> cropTagsBlacklist = new List<string>();
    }
}
