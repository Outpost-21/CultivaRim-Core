﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace CultivaRim
{
    public class CropColumnWorker_StatColdResist : CropColumnWorker_StatBase
	{
		public override TextAnchor Anchor => TextAnchor.MiddleCenter;

		public override int GetMinWidth(CropTable table)
		{
			return Mathf.Max(base.GetMinWidth(table), 50);
		}

		public override int Compare(ThingDef a, ThingDef b)
		{
			return a.CropData().coldBoosts.CompareTo(b.CropData().coldBoosts);
		}

		public override string GetTextFor(ThingDef crop)
		{
			return crop.CropData().StatColdResist;
		}

        public override void IncrementValue(ThingDef crop)
        {
			crop.CropData().coldBoosts++;
        }
    }
}
