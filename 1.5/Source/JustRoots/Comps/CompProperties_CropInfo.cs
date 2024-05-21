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
    public class CompProperties_CropInfo : CompProperties
    {
        public string wildCropPath;
        public string wildCropImmaturePath;
        public string toxWildCropPath;
        public string toxWildCropImmaturePath;

        public int cultivatedLevel = 5;

        public bool unlockedByProduct = true;

        public bool unlockedByResearch = true;

        public bool incidentSpawned = true;

        public List<BiomeDef> biomeWhitelist = new List<BiomeDef>();
        public List<BiomeDef> biomeBlacklist = new List<BiomeDef>();

        public bool biomeLockedPlanting = false;

        public List<ThingDef> secondaryProducts = new List<ThingDef>();

        public CompProperties_CropInfo()
        {
            compClass = typeof(Comp_CropInfo);
        }
    }
}