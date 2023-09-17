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
        public CompProperties_CropInfo compProps;
        public CompProperties_CropInfo CompProps
        {
            get
            {
                if (compProps == null)
                {
                    CompProperties_CropInfo modExtInt = plantDef.GetCompProperties<CompProperties_CropInfo>();
                    if (modExtInt == null)
                    {
                        compProps = new CompProperties_CropInfo();
                    }
                    else
                    {
                        compProps = modExtInt;
                    }
                }
                return compProps;
            }
        }

        public bool known = false;
        public float exp = 0f;
        public int curLevel = 0;
        public int maxLevel = 10;

        public int speedBoosts = 0;
        public int yieldBoosts = 0;
        public int rainBoosts = 0;

        public float StatGrowthSpeedRaw => speedBoosts * CultivaRimMod.settings.stat_growthSpeed;
        public string StatGrowthSpeed => "+" + (StatGrowthSpeedRaw).ToStringPercent();
        public float StatProductYieldRaw => speedBoosts * CultivaRimMod.settings.stat_productYield;
        public string StatProductYield => "+" + (StatProductYieldRaw).ToStringPercent();
        public float StatRainGrowthRaw => speedBoosts * CultivaRimMod.settings.stat_rainGrowth;
        public string StatRainGrowth => "+" + (StatRainGrowthRaw).ToStringPercent();

        public List<CropTrait> cropTraits = new List<CropTrait>();

        public void AddTrait(CropTrait trait)
        {
            if(cropTraits.Any(t => t.def == trait.def))
            {
                LogUtil.LogError("Attempted to give a crop a trait it already has.");
            }
            cropTraits.Add(trait);
            nextTraitDefSelection = null;
        }

        public bool CanUpgrade => CurLevel >= maxLevel;

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

        public int UnspentPoints => CurLevel - (speedBoosts + yieldBoosts + rainBoosts);

        public int TraitUnlocks => Mathf.FloorToInt(CurLevel / 10) - cropTraits.Count;

        public bool Cultivated => CompProps.cultivatedLevel < CurLevel;

        public void AddExperience(float exp, Pawn pawn = null, bool intelligenceMatters = false)
        {
            if (CurLevel >= maxLevel)
            {
                return;
            }
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
            bool cropEngineeringCompleted = CultivaRimDefOf.CultivaRim_CropEngineering.IsFinished;
            if (!cropEngineeringCompleted && CurLevel % 2 == 0)
            {
                int num = Rand.RangeInclusive(1, 3);
                switch (num)
                {
                    case 1:
                        speedBoosts += 1;
                        break;
                    case 2:
                        yieldBoosts += 1;
                        break;
                    default:
                        rainBoosts += 1;
                        break;
                }
            }
            Messages.Message("CultivaRim.CropKnowledgeLevelUp".Translate(plantDef.LabelCap, CurLevel), MessageTypeDefOf.TaskCompletion);
        }

        public List<CropTraitDef> nextTraitDefSelection = new List<CropTraitDef>();

        public List<CropTraitDef> NextTraitDefSelection
        {
            get
            {
                if (nextTraitDefSelection.NullOrEmpty())
                {
                    List<CropTraitDef> allViable = DefDatabase<CropTraitDef>.AllDefs.Where(t => !cropTraits.Any(ct => ct.def == t)).ToList();

                    if (!allViable.NullOrEmpty())
                    {
                        nextTraitDefSelection = PickRandomTraits(allViable, allViable.Count > 3 ? 3 : allViable.Count);
                    }
                    else
                    {
                        return new List<CropTraitDef>();
                    }
                }
                return nextTraitDefSelection;
            }
        }

        public bool anyTraitsLeftToSelect => NextTraitDefSelection.Count > 0;

        public List<CropTraitDef> PickRandomTraits(List<CropTraitDef> input, int quantity)
        {
            if (input.Count == quantity)
            {
                return input;
            }
            List<CropTraitDef> output = new List<CropTraitDef>();
            while (output.Count < 3)
            {
                CropTraitDef randomTrait = input.RandomElement();
                if (!output.Contains(randomTrait))
                {
                    output.Add(randomTrait);
                }
            }
            return output;
        }

        public void ExposeData()
        {
            Scribe_Defs.Look(ref plantDef, "plantDef");

            Scribe_Values.Look(ref known, "known");
            Scribe_Values.Look(ref exp, "exp");
            Scribe_Values.Look(ref curLevel, "curLevel");
            Scribe_Values.Look(ref maxLevel, "maxLevel");

            Scribe_Values.Look(ref speedBoosts, "speedBoosts");
            Scribe_Values.Look(ref yieldBoosts, "yieldBoosts");
            Scribe_Values.Look(ref rainBoosts, "rainBoosts");

            Scribe_Collections.Look(ref cropTraits, "cropTraits");
            Scribe_Collections.Look(ref nextTraitDefSelection, "nextTraitDefSelection");
        }
    }
}
