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
    public class Window_TraitSelection : Window
    {
        public CropData cropData;

        public override Vector2 InitialSize => new Vector2(680f, 500f);

        public override void DoWindowContents(Rect inRect)
        {
            doCloseX = true;
            float thirdOf = inRect.width / 3f;

            List<CropTraitDef> list = cropData.NextTraitDefSelection;

            int curTraitNum = 0;

            LogUtil.LogMessage(list.ToArray().ToString());

            foreach(CropTraitDef def in list)
            {
                Rect rect = new Rect(inRect.x + curTraitNum * thirdOf, inRect.y, thirdOf, inRect.height).ContractedBy(4f);
                DoTraitSelection(rect, def);
                curTraitNum++;
            }
        }

        public void DoTraitSelection(Rect rect, CropTraitDef trait)
        {
            Rect iconRect = new Rect(rect.x, rect.y, rect.width, rect.width).ContractedBy(24f);
            DrawTraitIcon(iconRect, trait.cachedTex);
            Rect labelRect = new Rect(rect.x, rect.y + rect.width, rect.width, 32f);
            Widgets.DrawBoxSolid(labelRect, new Color(0.3f, 0.3f, 0.3f, 0.2f));
            Text.Font = GameFont.Medium;
            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(labelRect, trait.LabelCap);
            Rect descRect = new Rect(rect.x, rect.y + rect.width + 32f, rect.width, rect.height - rect.width - 32f - 24f);
            Text.Font = GameFont.Tiny;
            Text.Anchor = TextAnchor.UpperLeft;
            Widgets.Label(descRect, trait.description);
            Text.Font = GameFont.Small;
            Rect buttonRect = new Rect(rect.x, rect.height - 24f, rect.width, 24f);
            if (Widgets.ButtonText(buttonRect, "CultivaRim.SelectTrait".Translate()))
            {
                cropData.AddTrait(new CropTrait() { def = trait });
                GameCompUtil.gameComp_cropData.EndUpgrade(cropData.plantDef, true);
            }
        }

        public void DrawTraitIcon(Rect rect, Texture iconTex)
        {
            Widgets.DrawTextureFitted(rect, iconTex, 1f);
        }
    }
}
