using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace CultivaRim
{
	public abstract class CropColumnWorker_Text : CropColumnWorker
	{
		private static NumericStringComparer comparer = new NumericStringComparer();

		protected virtual int Width => def.width;

		protected virtual TextAnchor Anchor => TextAnchor.MiddleLeft;

		public override void DoHeader(Rect rect, CropTable table)
		{
			base.DoHeader(rect, table);
			MouseoverSounds.DoRegion(rect);
		}

		public override void DoCell(Rect rect, ThingDef crop, CropTable table)
		{
			Rect rect2 = new Rect(rect.x, rect.y, rect.width, Mathf.Min(rect.height, 30f));
			string textFor = GetTextFor(crop);
			if (textFor == null)
			{
				return;
			}
			Text.Font = GameFont.Small;
			Text.Anchor = Anchor;
			Text.WordWrap = false;
			Widgets.Label(rect2, textFor);
			Text.WordWrap = true;
			Text.Anchor = TextAnchor.UpperLeft;
			if (Mouse.IsOver(rect2))
			{
				string tip = GetTip(crop);
				if (!tip.NullOrEmpty())
				{
					TooltipHandler.TipRegion(rect2, tip);
				}
			}
		}

		public override int GetMinWidth(CropTable table)
		{
			return Mathf.Max(base.GetMinWidth(table), Width);
		}

		public override int Compare(ThingDef a, ThingDef b)
		{
			return comparer.Compare(GetTextFor(a), GetTextFor(b));
		}

		protected abstract string GetTextFor(ThingDef pawn);

		protected virtual string GetTip(ThingDef pawn)
		{
			return null;
		}
	}
}
