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
	public class MangleStrafe : Strafe
	{

		public override Ability Ability
		{
			get
			{
				return MangleStrafe.ability;
			}
		}

		protected override IEnumerator PostSuccessfulMoveSequence(CardSlot oldSlot)
		{
			bool flag = oldSlot.Card == null;
			if (flag)
			{	
				yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName("FiveNightsatInscryption_WirePile"), oldSlot, 0.1f, true);
			}
			yield break;
		}
		public static Ability ability;
	}
}
```
