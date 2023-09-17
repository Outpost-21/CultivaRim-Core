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
    public class CropColumnWorker_Traitselect : CropColumnWorker
    {
        public override int GetMinWidth(CropTable table)
        {
            return 80;
        }

        public override void DoCell(Rect rect, ThingDef crop, CropTable table)
        {
            bool isActive = crop.CropData().TraitUnlocks > 0 && crop.CropData().anyTraitsLeftToSelect;
            if (isActive)
            {
                if (Widgets.ButtonText(rect, "CultivaRim.TraitButtonLabel".Translate()))
                {
                    Find.WindowStack.Add(new Window_TraitSelection() { cropData = crop.CropData() });
                }
            }
        }
    }
}
