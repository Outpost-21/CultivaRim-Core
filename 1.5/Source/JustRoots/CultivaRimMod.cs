using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace CultivaRim
{
    public class CultivaRimMod : Mod
    {
        public static CultivaRimMod mod;
        public static CultivaRimSettings settings;

        public Vector2 optionsScrollPosition;
        public float optionsViewRectHeight;

        public List<ThingDef> unlockablePlantsCached = new List<ThingDef>();
        public List<ThingDef> UnlockablePlants
        {
            get
            {
                if (unlockablePlantsCached.NullOrEmpty())
                {
                    unlockablePlantsCached = new List<ThingDef>();
                    foreach(ThingDef plant in DefDatabase<ThingDef>.AllDefs.Where(p => p.IsPlant))
                    {
                        if (!unlockablePlantsCached.Contains(plant) && plant.researchPrerequisites.NullOrEmpty() && plant.plant.Harvestable && plant.plant.Sowable)
                        {
                            CompProperties_CropInfo cropInfo = plant.GetCompProperties<CompProperties_CropInfo>();
                            if (cropInfo == null || cropInfo.unlockedByProduct)
                            {
                                unlockablePlantsCached.Add(plant);
                            }
                        }
                    }
                }
                return unlockablePlantsCached;
            }
        }

        internal static string VersionDir => Path.Combine(mod.Content.ModMetaData.RootDir.FullName, "Version.txt");
        public static string CurrentVersion { get; private set; }

        public CultivaRimMod(ModContentPack content) : base(content)
        {
            mod = this;
            settings = GetSettings<CultivaRimSettings>();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            CurrentVersion = $"{version.Major}.{version.Minor}.{version.Build}";

            LogUtil.LogMessage($"{CurrentVersion} ::");

            if (Prefs.DevMode)
            {
                File.WriteAllText(VersionDir, CurrentVersion);
            }

            Harmony harmony = new Harmony("Neronix17.CultivaRim.RimWorld");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override string SettingsCategory() => "CultivaRim";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            bool flag = optionsViewRectHeight > inRect.height;
            Rect viewRect = new Rect(inRect.x, inRect.y, inRect.width - (flag ? 26f : 0f), optionsViewRectHeight);
            Widgets.BeginScrollView(inRect, ref optionsScrollPosition, viewRect);
            Listing_Standard listing = new Listing_Standard();
            Rect rect = new Rect(viewRect.x, viewRect.y, viewRect.width, 999999f);
            listing.Begin(rect);
            // ============================ CONTENTS ================================
            DoOptionsCategoryContents(listing);
            // ======================================================================
            optionsViewRectHeight = listing.CurHeight;
            listing.End();
            Widgets.EndScrollView();
        }

        public void DoOptionsCategoryContents(Listing_Standard listing)
        {
            if(listing.ButtonTextLabeled("Reset to Defaults", "Reset"))
            {
                settings.newCropVisuals = true;
                
                settings.lvlConstant = 0.07f;
                settings.lvlPower = 2f;
                settings.traitInterval = 10f;
                
                settings.stat_growthSpeed = 0.1f;
                settings.stat_productYield = 0.1f;
                settings.stat_coldResist = 1f;
                settings.stat_heatResist = 1f;
                settings.stat_rainGrowth = 0.1f;

                settings.stat_growthSpeedDebuff = 0.5f;
                settings.stat_productYieldDebuff = 0.5f;

                settings.defaultUnlockedPlants.Clear();
                CultivaRimStartup.AddMissingPlantsToDictionary();
            }
            {
                listing.LabelBacked("Trait Interval", Color.white);
                listing.Note("This is the number of levels a crop can develop before aquiring a trait and increasing the level cap.", GameFont.Tiny);
                listing.AddLabeledSlider("Trait Interval: " + settings.traitInterval.ToString(), ref settings.traitInterval, 5f, 30f, roundTo: 1f);
            }
            {
                listing.LabelBacked("Stat Debuffs", Color.white);
                listing.Note("These are universal offsets applied to start plants off less useful than they usually are in vanilla.", GameFont.Tiny);
                listing.AddLabeledSlider("Growth Speed: -" + settings.stat_growthSpeedDebuff.ToStringPercent(), ref settings.stat_growthSpeedDebuff, 0f, 0.5f, "-0%", "-50%");
                listing.AddLabeledSlider("Product Yield: -" + settings.stat_productYieldDebuff.ToStringPercent(), ref settings.stat_productYieldDebuff, 0f, 0.5f, "-0%", "-50%");
            }
            {
                listing.LabelBacked("Stat Buffs", Color.white);
                listing.Note("These are the value increments added when you add a point to any given crop.", GameFont.Tiny);
                listing.AddLabeledSlider("Growth Speed: +" + settings.stat_growthSpeed.ToStringPercent(), ref settings.stat_growthSpeed, 0.01f, 10.0f, "+1%", "+100%");
                listing.AddLabeledSlider("Product Yield: +" + settings.stat_productYield.ToStringPercent(), ref settings.stat_productYield, 0.01f, 10.0f, "+1%", "+100%");
                listing.AddLabeledSlider("Rain Growth: +" + settings.stat_rainGrowth.ToStringPercent(), ref settings.stat_rainGrowth, 0.01f, 10.0f, "+1%", "+100%");
                listing.AddLabeledSlider("Cold Resist: -" + settings.stat_coldResist.ToStringTemperature(), ref settings.stat_coldResist, 0.5f, 10.0f, roundTo: 0.5f);
                listing.AddLabeledSlider("Heat Resist: +" + settings.stat_heatResist.ToStringTemperature(), ref settings.stat_heatResist, 0.5f, 10.0f, roundTo: 0.5f);
            }
            {
                listing.LabelBacked("Default Unlocked Plants", Color.white);
                listing.Note("Ticked plants are unlocked at the start of a new game regardless of if they have been found or not, this doesn't affect plants unlocked through research or other means, only ones you typically unlock through discovery.");
                foreach(ThingDef plant in UnlockablePlants)
                {
                    bool plantDictValue = settings.defaultUnlockedPlants[plant.defName];
                    listing.CheckboxLabeled(plant.LabelCap, ref plantDictValue);
                    settings.defaultUnlockedPlants[plant.defName] = plantDictValue;
                }
            }
        }
    }
}
