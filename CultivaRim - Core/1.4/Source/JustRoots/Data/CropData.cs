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
        public bool known = false;
        public bool wild = false;
        public int experience = 0;
        public int maxLevel = 10;
        public int lastKnownLevel = 0;

        public int speedBoosts = 0;
        public int yieldBoosts = 0;
        public int heatBoosts = 0;
        public int coldBoosts = 0;
        public int rainBoosts = 0;

        public string StatGrowthSpeed => "+" + (speedBoosts * 0.05f).ToStringPercent();
        public string StatProductYield => "+" + (yieldBoosts * 0.05f).ToStringPercent();
        public string StatHeatResist => "+" + (heatBoosts * 0.5f).ToStringTemperature();
        public string StatColdResist => "+" + (coldBoosts * 0.5f).ToStringTemperature();
        public string StatRainGrowth => "+" + (rainBoosts * 0.05f).ToStringPercent();

        public bool hydroponics = false;
        public bool blightFree = false;
        public bool autoReplant = false;
        public bool lightIgnored = false;

        public int CurLevel => Mathf.FloorToInt(experience / 1000);

        public float CurLevelPercentage => (CurLevel > 0) ? ((experience - (CurLevel - 1f * 1000f)) / 1000f) : (experience / 1000f);

        public int UnspentPoints => CurLevel - (speedBoosts + yieldBoosts + heatBoosts + coldBoosts + rainBoosts);

        public void AddExperience(float exp, Pawn pawn, bool intelligenceMatters = false)
        {
            if (pawn != null && pawn.skills != null)
            {
                float result = exp;
                result += exp * (pawn.skills.GetSkill(SkillDefOf.Plants).Level / 20);
                if (intelligenceMatters)
                {
                    result += exp * (pawn.skills.GetSkill(SkillDefOf.Intellectual).Level / 20);
                }
                experience += Mathf.FloorToInt(result);
            }
            if(CurLevel > lastKnownLevel)
            {
                LevelUp();
            }
        }

        public void LevelUp()
        {
            lastKnownLevel = CurLevel;
            //if(CurLevel % 2 == 0)
            //{
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
            //}
            Messages.Message("JustRoots.CropKnowledgeLevelUp".Translate(plantDef.LabelCap, CurLevel), MessageTypeDefOf.TaskCompletion);
        }

        public void ExposeData()
        {
            Scribe_Defs.Look(ref plantDef, "plantDef");

            Scribe_Values.Look(ref known, "known");
            Scribe_Values.Look(ref wild, "wild");
            Scribe_Values.Look(ref experience, "experience");
            Scribe_Values.Look(ref maxLevel, "maxLevel");
            Scribe_Values.Look(ref lastKnownLevel, "lastKnownLevel");

            Scribe_Values.Look(ref speedBoosts, "speedBoosts");
            Scribe_Values.Look(ref yieldBoosts, "yieldBoosts");
            Scribe_Values.Look(ref heatBoosts, "heatBoosts");
            Scribe_Values.Look(ref coldBoosts, "coldBoosts");
            Scribe_Values.Look(ref rainBoosts, "rainBoosts");

            Scribe_Values.Look(ref hydroponics, "hydroponics");
            Scribe_Values.Look(ref blightFree, "blightFree");
            Scribe_Values.Look(ref autoReplant, "autoReplant");
            Scribe_Values.Look(ref lightIgnored, "lightIgnored");
        }
    }
}
