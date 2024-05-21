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
    public class CropColumnWorker_Traits : CropColumnWorker
    {
        public override int GetMinWidth(CropTable table)
        {
            return 160;
        }

        public override void DoCell(Rect rect, ThingDef crop, CropTable table)
        {
            CropData cropData = crop.CropData();
            if (cropData.cropTraits.NullOrEmpty())
            {
                return;
            }
            float curX = rect.x;
            foreach (CropTrait trait in cropData.cropTraits)
            {
                Rect iconRect = new Rect(curX, rect.y, rect.height, rect.height);
                Widgets.DrawTextureFitted(iconRect, trait.def.cachedTex, 1f);
                curX += rect.height;
            }
        }
    }
}
