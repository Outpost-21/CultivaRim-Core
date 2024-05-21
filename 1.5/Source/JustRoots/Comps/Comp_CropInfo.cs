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
    public class Comp_CropInfo : ThingComp
    {
        public CompProperties_CropInfo Props => (CompProperties_CropInfo)props;

        public Graphic wildCropGraphic;

        public Graphic WildCropGraphic
        {
            get
            {
                if (wildCropGraphic == null && (!Props.wildCropPath?.NullOrEmpty() ?? false))
                {
                    wildCropGraphic = GraphicDatabase.Get(typeof(Graphic_Random), Props.wildCropPath, CultivaRimDefOf.CutoutPlant.Shader, new Vector2(1, 1), Color.white, Color.white);
                }
                return wildCropGraphic;
            }
        }

        public Graphic wildCropImmatureGraphic;

        public Graphic WildCropImmatureGraphic
        {
            get
            {
                if (wildCropImmatureGraphic == null && (!Props.wildCropImmaturePath?.NullOrEmpty() ?? false))
                {
                    wildCropImmatureGraphic = GraphicDatabase.Get(typeof(Graphic_Random), Props.wildCropImmaturePath, CultivaRimDefOf.CutoutPlant.Shader, new Vector2(1, 1), Color.white, Color.white);
                }
                return wildCropImmatureGraphic;
            }
        }

        public Graphic toxWildCropGraphic;

        public Graphic ToxWildCropGraphic
        {
            get
            {
                if (toxWildCropGraphic == null && (!Props.toxWildCropPath?.NullOrEmpty() ?? false))
                {
                    toxWildCropGraphic = GraphicDatabase.Get(typeof(Graphic_Random), Props.toxWildCropPath, CultivaRimDefOf.CutoutPlant.Shader, new Vector2(1, 1), Color.white, Color.white);
                }
                return toxWildCropGraphic;
            }
        }

        public Graphic toxWildCropImmatureGraphic;

        public Graphic ToxWildCropImmatureGraphic
        {
            get
            {
                if (toxWildCropImmatureGraphic == null && (!Props.toxWildCropImmaturePath?.NullOrEmpty() ?? false))
                {
                    toxWildCropImmatureGraphic = GraphicDatabase.Get(typeof(Graphic_Random), Props.toxWildCropImmaturePath, CultivaRimDefOf.CutoutPlant.Shader, new Vector2(1, 1), Color.white, Color.white);
                }
                return toxWildCropImmatureGraphic;
            }
        }
    }
}
