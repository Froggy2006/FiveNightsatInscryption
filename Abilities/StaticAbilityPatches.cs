```cs
using BepInEx;
using BepInEx.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using UnityEngine;
using Pixelplacement;
using HarmonyLib;
using InscryptionAPI;
using InscryptionAPI.Ascension;
using InscryptionAPI.Boons;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Guid;
using InscryptionAPI.Helpers;
using InscryptionAPI.Regions;
using InscryptionAPI.Saves;
using InscryptionCommunityPatch;
using DiskCardGame;

namespace FiveNightsatInscryption.abilities
{
    [HarmonyPatch(typeof(PlayableCard), "GetPassiveHealthBuffs")]
    public class PassiveHealthBuffs_patch
    {
        [HarmonyPostfix]
        public static void Postfix(ref int __result, ref PlayableCard __instance)
        {
            if (__instance.OnBoard)
            {
                CardSlot opposingslot = Singleton<BoardManager>.Instance.OpponentSlotsCopy[__instance.Slot.Index];
                if (opposingslot.Card != null)
                {
                    if (opposingslot.Card.HasAbility(StaticAbility.ability))
                    {
                        __result -= 1;
                    }
                }
            }
        }
    }
    [HarmonyPatch(typeof(PlayableCard), "GetPassiveAttackBuffs")]
    public class PassiveAttackBuffs_patch
    {
        [HarmonyPostfix]
        public static void Postfix(ref int __result, ref PlayableCard __instance)
        {
            if (__instance.OnBoard)
            {
                CardSlot opposingslot = Singleton<BoardManager>.Instance.OpponentSlotsCopy[__instance.Slot.Index];
                if (opposingslot.Card != null)
                {
                    if (opposingslot.Card.HasAbility(StaticAbility.ability))
                    {
                        __result -= 2;
                    }
                }
            }
        }
    }
}
```
