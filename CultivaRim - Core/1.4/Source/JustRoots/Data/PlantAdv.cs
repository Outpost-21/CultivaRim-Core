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
        public DefModExt_CropInfo modExt;
        public DefModExt_CropInfo ModExt
        {
            get
            {
                if (modExt == null)
                {
                    try
                    {
                        modExt = def.GetModExtension<DefModExt_CropInfo>();
                    }
                    catch (Exception ex)
                    {
                        LogUtil.LogError($"No DefModExt_CropInfo found on def {def} with class PlantAdv, the extension is required for functionality of that class.\n\nException:\n" + ex.Message);
                    }
                }
                return modExt;
            }
        }

        public Graphic wildCropGraphic;
        public Graphic wildCropImmatureGraphic;
        public Graphic toxWildCropGraphic;
        public Graphic toxWildCropImmatureGraphic;

        public override Graphic Graphic
        {
            get
            {
                if (LifeStage == PlantLifeStage.Sowing) { return base.Graphic; }
                CropData cropData = def.CropData();
                if (cropData != null && !cropData.Cultivated)
                {
                    if (PositionHeld.IsPolluted(MapHeld))
                    {
                        if (toxWildCropImmatureGraphic != null && !HarvestableNow)
                        { return toxWildCropImmatureGraphic; }
                        if (toxWildCropGraphic != null)
                        { return toxWildCropGraphic; }
                    }
                    else
                    {
                        if (wildCropImmatureGraphic != null && !HarvestableNow)
                        { return wildCropImmatureGraphic; }
                        if (wildCropGraphic != null)
                        { return wildCropGraphic; }
                    }
                }
                return base.Graphic;
            }
        }

        public PlantAdv()
        {
            InitWildCropGraphic();
            InitWildCropImmatureGraphic();
        }

        public void InitWildCropGraphic()
        {
            if (wildCropGraphic == null && (!ModExt?.wildCropPath?.NullOrEmpty() ?? false))
            {
                wildCropGraphic = GraphicDatabase.Get(def.graphicData.graphicClass, ModExt.wildCropPath, def.graphic.Shader, def.graphicData.drawSize, def.graphicData.color, def.graphicData.colorTwo);
            }
        }

        public void InitWildCropImmatureGraphic()
        {
            if (wildCropImmatureGraphic == null && (!ModExt?.wildCropImmaturePath?.NullOrEmpty() ?? false))
            {
                wildCropImmatureGraphic = GraphicDatabase.Get(def.graphicData.graphicClass, ModExt.wildCropImmaturePath, def.graphic.Shader, def.graphicData.drawSize, def.graphicData.color, def.graphicData.colorTwo);
            }
        }

        public void InitToxWildCropGraphic()
        {
            if (toxWildCropGraphic == null && (!ModExt?.toxWildCropPath?.NullOrEmpty() ?? false))
            {
                toxWildCropGraphic = GraphicDatabase.Get(def.graphicData.graphicClass, ModExt.toxWildCropPath, def.graphic.Shader, def.graphicData.drawSize, def.graphicData.color, def.graphicData.colorTwo);
            }
        }

        public void InitToxWildCropImmatureGraphic()
        {
            if (toxWildCropImmatureGraphic == null && (!ModExt?.toxWildCropImmaturePath?.NullOrEmpty() ?? false))
            {
                toxWildCropImmatureGraphic = GraphicDatabase.Get(def.graphicData.graphicClass, ModExt.toxWildCropImmaturePath, def.graphic.Shader, def.graphicData.drawSize, def.graphicData.color, def.graphicData.colorTwo);
            }
        }
    }
}
