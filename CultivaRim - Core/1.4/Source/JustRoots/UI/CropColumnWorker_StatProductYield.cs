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
    public class CropColumnWorker_StatProductYield : CropColumnWorker_StatBase
	{
		public override TextAnchor Anchor => TextAnchor.MiddleCenter;

		public override int GetMinWidth(CropTable table)
		{
			return Mathf.Max(base.GetMinWidth(table), 50);
		}

		public override int Compare(ThingDef a, ThingDef b)
		{
			return a.CropData().yieldBoosts.CompareTo(b.CropData().yieldBoosts);
		}

		public override string GetTextFor(ThingDef crop)
		{
			return crop.CropData().StatProductYield;
		}

		public override void IncrementValue(ThingDef crop)
		{
			crop.CropData().yieldBoosts++;
		}
	}
}
