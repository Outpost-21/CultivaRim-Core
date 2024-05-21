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
    public abstract class MainTabWindow_CropTable : MainTabWindow
	{
		private CropTable table;

		protected virtual float ExtraBottomSpace => 53f;

		protected virtual float ExtraTopSpace => 0f;

		protected abstract CropTableDef CropTableDef { get; }

		public override float Margin => 6f;

		public override Vector2 RequestedTabSize
		{
			get
			{
				if (table == null)
				{
					return Vector2.zero;
				}
				return new Vector2(table.Size.x + Margin * 2f, table.Size.y + ExtraBottomSpace + ExtraTopSpace + Margin * 2f);
			}
		}

		protected virtual IEnumerable<ThingDef> Crops => DefDatabase<ThingDef>.AllDefs.Where(c => c.IsPlant && c.plant.Sowable);

		public override void PostOpen()
		{
			base.PostOpen();
			if (table == null)
			{
				table = CreateTable();
			}
			SetDirty();
		}

		public override void DoWindowContents(Rect rect)
		{
			table.CropTableOnGUI(new Vector2(rect.x, rect.y + ExtraTopSpace));
		}

		public void Notify_PawnsChanged()
		{
			SetDirty();
		}

		public override void Notify_ResolutionChanged()
		{
			table = CreateTable();
			base.Notify_ResolutionChanged();
		}

		private CropTable CreateTable()
		{
			return (CropTable)Activator.CreateInstance(CropTableDef.workerClass, CropTableDef, (Func<IEnumerable<ThingDef>>)(() => Crops), UI.screenWidth - (int)(Margin * 2f), (int)((float)(UI.screenHeight - 35) - ExtraBottomSpace - ExtraTopSpace - Margin * 2f));
		}

		protected void SetDirty()
		{
			table.SetDirty();
			SetInitialSizeAndPosition();
		}
	}
}
