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
    public class CropData : IExposable
    {
        public ThingDef plantDef;
        public DefModExt_CropInfo modExt;
        public DefModExt_CropInfo ModExt
        {
            get
            {
                if (modExt == null)
                {
                    DefModExt_CropInfo modExtInt = plantDef.GetModExtension<DefModExt_CropInfo>();
                    if (modExtInt == null)
                    {
                        modExt = new DefModExt_CropInfo();
                    }
                    else
                    {
                        modExt = modExtInt;
                    }
                }
                return modExt;
            }
        }

        public bool known = false;
        public bool wild = false;
        public float exp = 0f;
        public int curLevel = 0;
        public int maxLevel = 10;

        public int speedBoosts = 0;
        public int yieldBoosts = 0;
        public int heatBoosts = 0;
        public int coldBoosts = 0;
        public int rainBoosts = 0;

        public float StatGrowthSpeedRaw => speedBoosts * CultivaRimMod.settings.stat_growthSpeed;
        public string StatGrowthSpeed => "+" + (StatGrowthSpeedRaw).ToStringPercent();
        public float StatProductYieldRaw => speedBoosts * CultivaRimMod.settings.stat_productYield;
        public string StatProductYield => "+" + (StatProductYieldRaw).ToStringPercent();
        public float StatHeatResistRaw => speedBoosts * CultivaRimMod.settings.stat_heatResist;
        public string StatHeatResist => "+" + (StatHeatResistRaw).ToStringTemperature();
        public float StatColdResistRaw => speedBoosts * CultivaRimMod.settings.stat_coldResist;
        public string StatColdResist => "+" + (StatColdResistRaw).ToStringTemperature();
        public float StatRainGrowthRaw => speedBoosts * CultivaRimMod.settings.stat_rainGrowth;
        public string StatRainGrowth => "+" + (StatRainGrowthRaw).ToStringPercent();

        //public bool hydroponics = false;
        //public bool blightFree = false;
        //public bool autoReplant = false;
        //public bool lightIgnored = false;

        public List<CropTrait> cropTraits = new List<CropTrait>();

        public float Exp 
        {
            get
            {
                return exp;
            } 
            set 
            {
                exp = value;

                while (exp >= ExpForNextLevel)
                {
                    LevelUp();
                }
            } 
        }

        public int CurLevel => curLevel;

        public float ExpForNextLevel => Mathf.Pow((CurLevel + 1) / CultivaRimMod.settings.lvlConstant, CultivaRimMod.settings.lvlPower);

        public float CurLevelPercentage => Exp / ExpForNextLevel;

        public int UnspentPoints => CurLevel - (speedBoosts + yieldBoosts + heatBoosts + coldBoosts + rainBoosts);

        public int TraitUnlocks => Mathf.FloorToInt(CurLevel / 5);

        public bool Cultivated => ModExt.cultivatedLevel < CurLevel;

        public void AddExperience(float exp, Pawn pawn = null, bool intelligenceMatters = false)
        {
            if (pawn != null && pawn.skills != null)
            {
                float result = exp;
                result += exp * (pawn.skills.GetSkill(SkillDefOf.Plants).Level / 20);
                if (intelligenceMatters)
                {
                    result += exp * (pawn.skills.GetSkill(SkillDefOf.Intellectual).Level / 20);
                }
                this.Exp += result;
            }
            else
            {
                this.Exp += exp;
            }
        }

        public void LevelUp()
        {
            Exp = Exp - ExpForNextLevel;
            curLevel++;
            bool cropEngineeringCompleted = DefDatabase<ResearchProjectDef>.GetNamed("CultivaRim_CropEngineering").IsFinished;
            if (!cropEngineeringCompleted && CurLevel % 2 == 0)
            {
                int num = Rand.RangeInclusive(1, 4);
                switch (num)
                {
                    case 1:
                        speedBoosts += 1;
                        break;
                    case 2:
                        yieldBoosts += 1;
                        break;
                    case 3:
                        coldBoosts += 1;
                        break;
                    default:
                        heatBoosts += 1;
                        break;
                }
            }
            Messages.Message("CultivaRim.CropKnowledgeLevelUp".Translate(plantDef.LabelCap, CurLevel), MessageTypeDefOf.TaskCompletion);
        }

        public void ExposeData()
        {
            Scribe_Defs.Look(ref plantDef, "plantDef");

            Scribe_Values.Look(ref known, "known");
            Scribe_Values.Look(ref wild, "wild");
            Scribe_Values.Look(ref exp, "exp");
            Scribe_Values.Look(ref curLevel, "curLevel");
            Scribe_Values.Look(ref maxLevel, "maxLevel");

            Scribe_Values.Look(ref speedBoosts, "speedBoosts");
            Scribe_Values.Look(ref yieldBoosts, "yieldBoosts");
            Scribe_Values.Look(ref heatBoosts, "heatBoosts");
            Scribe_Values.Look(ref coldBoosts, "coldBoosts");
            Scribe_Values.Look(ref rainBoosts, "rainBoosts");

            Scribe_Collections.Look(ref cropTraits, "cropTraits");
        }
    }
}
