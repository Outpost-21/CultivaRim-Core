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
	public class CropColumnWorker_Label : CropColumnWorker
	{
		private const int LeftMargin = 3;

		private const float PortraitCameraZoom = 1.2f;

		private static Dictionary<string, string> labelCache = new Dictionary<string, string>();

		private static float labelCacheForWidth = -1f;

		protected virtual TextAnchor LabelAlignment => TextAnchor.MiddleLeft;

		public override void DoCell(Rect rect, ThingDef crop, CropTable table)
		{
			Rect rect2 = new Rect(rect.x, rect.y, rect.width, Mathf.Min(rect.height, def.groupable ? rect.height : ((float)GetMinCellHeight(crop))));
			Rect rect3 = rect2;
			rect3.xMin += 3f;
			if (def.showIcon)
			{
				rect3.xMin += rect2.height;
				Widgets.ThingIcon(new Rect(rect2.x, rect2.y, rect2.height, rect2.height), crop);
			}
			if (Mouse.IsOver(rect2))
			{
				GUI.DrawTexture(rect2, TexUI.HighlightTex);
			}
			string text = crop.LabelCap;
			int unspentPoints = crop.CropData().UnspentPoints;
			if (unspentPoints > 0)
            {
				text += $" ({crop.CropData().UnspentPoints} Unspent Points)";

			}
			if (rect3.width != labelCacheForWidth)
			{
				labelCacheForWidth = rect3.width;
				labelCache.Clear();
			}
			if (Text.CalcSize(text.StripTags()).x > rect3.width)
			{
				text = text.StripTags().Truncate(rect3.width, labelCache);
			}
			Text.Font = GameFont.Small;
			Text.Anchor = LabelAlignment;
			Text.WordWrap = false;
			Widgets.Label(rect3, text);
			Text.WordWrap = true;
			Text.Anchor = TextAnchor.UpperLeft;
			if (Mouse.IsOver(rect2))
			{
				TipSignal tooltip = crop.description;
				TooltipHandler.TipRegion(rect2, tooltip);
			}
		}

		public override int GetMinWidth(CropTable table)
		{
			return Mathf.Max(base.GetMinWidth(table), 80);
		}

		public override int GetOptimalWidth(CropTable table)
		{
			return Mathf.Clamp(165, GetMinWidth(table), GetMaxWidth(table));
		}
	}
}
