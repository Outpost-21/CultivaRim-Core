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
    public class CropTable_JustCrops : CropTable
	{
		protected override IEnumerable<ThingDef> LabelSortFunction(IEnumerable<ThingDef> input)
		{
			return from p in input
				   where p.CropData().known
				   orderby p.label
				   select p;
		}

		public CropTable_JustCrops(CropTableDef def, Func<IEnumerable<ThingDef>> cropsGetter, int uiWidth, int uiHeight)
			: base(def, cropsGetter, uiWidth, uiHeight)
		{
		}
	}
}
