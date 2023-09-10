using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Ruyu.PredatorAnimalsPatch
{

    [HarmonyPatch(typeof(JobDriver_PredatorHunt))]
    [HarmonyPatch("MakeNewToils")]
    public static class PatchHitAction
    {
        private static void Postfix(JobDriver_PredatorHunt __instance)
        {
            // Check if the predator successfully hunted its prey
            if (__instance.firstHit)
            {
                Pawn predator = __instance.pawn;
                HediffDef reducedBleedingHediffDef = DefDatabase<HediffDef>.GetNamed("ruyuHediff_ReducedBleeding"); // Replace with your hediff definition name
                Hediff reducedBleedingHediff = HediffMaker.MakeHediff(reducedBleedingHediffDef, predator);
                predator.health.AddHediff(reducedBleedingHediff);
            }
        }

        [StaticConstructorOnStartup]
        public static class PredatorAnimalsPatch
        {
            static PredatorAnimalsPatch()
            {
                Harmony harmony = new Harmony("Ruyu.PredatorAnimalsPatch");
                harmony.PatchAll();
            }
        }
    }
}