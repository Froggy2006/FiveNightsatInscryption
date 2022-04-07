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
	public class DamagedAbility : AbilityBehaviour
	{
		public override Ability Ability
		{
			get
			{
				return DamagedAbility.ability;
			}
		}
		public override bool RespondsToTakeDamage(PlayableCard source)
		{
			return source != null && source.Health > 0;
		}
		public override IEnumerator OnTakeDamage(PlayableCard source)
		{
			yield return base.PreSuccessfulTriggerSequence();
			base.Card.Anim.StrongNegationEffect();
			yield return new WaitForSeconds(0.55f);
			yield return source.TakeDamage(1, base.Card);
			yield return base.LearnAbility(0.4f);
			yield break;
		}
				public static Ability ability;
	}
}
``
