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
    [StaticConstructorOnStartup]
    public class CropColumnWorker_Experience : CropColumnWorker
	{
		private const int Width = 120;

		private const int BarPadding = 4;

		public static readonly Texture2D EnergyBarTex = SolidColorMaterials.NewSolidColorTexture(new Color32(252, byte.MaxValue, byte.MaxValue, 65));

		public override void DoCell(Rect rect, ThingDef crop, CropTable table)
		{
			CropData data = crop.CropData();
			Widgets.FillableBar(rect.ContractedBy(4f), data.CurLevelPercentage, EnergyBarTex, BaseContent.ClearTex, doBorder: false);
			Text.Font = GameFont.Small;
			Text.Anchor = TextAnchor.MiddleCenter;
			Widgets.Label(rect, data.CurLevelPercentage.ToStringPercent());
			Text.Anchor = TextAnchor.UpperLeft;
			Text.Font = GameFont.Small;
		}

		public override int GetMinWidth(CropTable table)
		{
			return Mathf.Max(base.GetMinWidth(table), 120);
		}

		public override int GetMaxWidth(CropTable table)
		{
			return Mathf.Min(base.GetMaxWidth(table), GetMinWidth(table));
		}
	}
}
