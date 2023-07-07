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
    public class PlantAdv : Plant
    {
        public Comp_CropInfo cropComp;
        public Comp_CropInfo CropComp
        {
            get
            {
                if (cropComp == null)
                {
                    try
                    {
                        cropComp = this.TryGetComp<Comp_CropInfo>();
                    }
                    catch (Exception ex)
                    {
                        LogUtil.LogWarning($"No Comp_CropInfo found on def {def.defName} with class PlantAdv, the comp is required for functionality of that class.\n\nException:\n" + ex.Message);
                    }
                }
                return cropComp;
            }
        }

        public override Graphic Graphic
        {
            get
            {
                //if (LifeStage == PlantLifeStage.Sowing) { return base.Graphic; }
                CropData cropData = def.CropData();
                if (cropData != null && !cropData.Cultivated && CropComp != null)
                {
                    if (PositionHeld.IsPolluted(MapHeld))
                    {
                        if (CropComp?.toxWildCropImmatureGraphic != null && !HarvestableNow)
                        { return CropComp?.toxWildCropImmatureGraphic; }
                        if (CropComp?.toxWildCropGraphic != null)
                        { return CropComp?.toxWildCropGraphic; }
                    }
                    else
                    {
                        if (CropComp?.wildCropImmatureGraphic != null && !HarvestableNow)
                        { return CropComp?.wildCropImmatureGraphic; }
                        if (CropComp?.wildCropGraphic != null)
                        { return CropComp?.wildCropGraphic; }
                    }
                }
                return base.Graphic;
            }
        }

        public override string Label => def.CropData().Cultivated ? base.Label : "CultivaRim.WildPrefix".Translate(base.Label);

        public PlantAdv()
        {

        }
    }
}
