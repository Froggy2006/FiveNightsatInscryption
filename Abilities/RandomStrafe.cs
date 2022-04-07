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
	public class RandomStrafe : AbilityBehaviour
	{
		public override Ability Ability
		{
			get
			{
				return RandomStrafe.ability;
			}
		}

		public override bool RespondsToDrawn()
		{
			return true;
		}

		public override IEnumerator OnDrawn()
		{
			(Singleton<PlayerHand>.Instance as PlayerHand3D).MoveCardAboveHand(base.Card);
			yield return base.Card.FlipInHand(new Action(this.AddMod));
			yield return base.LearnAbility(0.5f);
			yield break;
		}

		private void AddMod()
		{
			base.Card.Status.hiddenAbilities.Add(this.Ability);
			CardModificationInfo cardModificationInfo = new CardModificationInfo(this.ChooseAbility());
			CardModificationInfo cardModificationInfo2 = base.Card.TemporaryMods.Find((CardModificationInfo x) => x.HasAbility(this.Ability));
			bool flag = cardModificationInfo2 == null;
			bool flag2 = flag;
			if (flag2)
			{
				cardModificationInfo2 = base.Card.Info.Mods.Find((CardModificationInfo x) => x.HasAbility(this.Ability));
			}
			bool flag3 = cardModificationInfo2 != null;
			bool flag4 = flag3;
			if (flag4)
			{
				cardModificationInfo.fromTotem = cardModificationInfo2.fromTotem;
				cardModificationInfo.fromCardMerge = cardModificationInfo2.fromCardMerge;
			}
			base.Card.AddTemporaryMod(cardModificationInfo);
		}
		private Ability ChooseAbility()
		{
			List<Ability> list = new List<Ability>();
			list.Add(Ability.Strafe);
			list.Add(Ability.StrafePush);
			list.Add(Ability.SquirrelStrafe);
			list.Add(Ability.MoveBeside);
			list.Add(MangleStrafe.ability);
			list.RemoveAll((Ability x) => x == Ability.RandomAbility || base.Card.HasAbility(x));
			bool flag = list.Count > 0;
			bool flag2 = flag;
			Ability result;
			if (flag2)
			{
				result = list[SeededRandom.Range(0, list.Count, base.GetRandomSeed())];
			}
			else
			{
				result = Ability.Sharp;
			}
			return result;
		}
		public static Ability ability;
	}
}
```
