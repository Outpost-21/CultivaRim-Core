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
    public class CropTableDef : Def
    {
        public List<CropColumnDef> columns;

        public Type workerClass = typeof(CropTable);

        public int minWidth = 998;
    }
}
