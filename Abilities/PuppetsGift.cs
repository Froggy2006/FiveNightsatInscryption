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
	public class PuppetsGift : AbilityBehaviour
	{
		public override Ability Ability
		{
			get
			{
				return PuppetsGift.ability;
			}
		}
		public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
		{
			return true;
		}

		public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
		{
			foreach (CardSlot cardslot in Singleton<BoardManager>.Instance.PlayerSlotsCopy)
			{
				bool flag = cardslot.Card != null;
				if (flag)
				{
					bool flag2 = cardslot.Card.TemporaryMods != null;
					if (flag2)
					{
						this.NegateBrittle(false, cardslot.Card);
					}
				}
				cardslot.Card = null;
			}
			List<CardSlot>.Enumerator enumerator = default(List<CardSlot>.Enumerator);
			yield break;
		}

		public override bool RespondsToResolveOnBoard()
		{
			return true;
		}

		public override IEnumerator OnResolveOnBoard()
		{
			foreach (CardSlot cardslot in Singleton<BoardManager>.Instance.PlayerSlotsCopy)
			{
				bool flag = cardslot.Card != null;
				if (flag)
				{
					bool flag2 = cardslot.Card.TemporaryMods != null;
					if (flag2)
					{
						this.NegateBrittle(true, cardslot.Card);
					}
				}
				cardslot.Card = null;
			}
			List<CardSlot>.Enumerator enumerator = default(List<CardSlot>.Enumerator);
			yield break;
		}

		public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard)
		{
			return base.Card.OnBoard && otherCard != base.Card && otherCard.Slot.IsPlayerSlot;
		}

		public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
		{
			bool flag = otherCard.TemporaryMods != null;
			if (flag)
			{
				this.NegateBrittle(true, otherCard);
			}
			yield break;
		}

		public void NegateBrittle(bool negate, Card card)
		{
			CardModificationInfo cardModificationInfo = new CardModificationInfo();
			cardModificationInfo.negateAbilities.Add(Ability.Brittle);
			CardInfo cardInfo = card.Info.Clone() as CardInfo;
			if (negate)
			{
				cardInfo.Mods.Add(cardModificationInfo);
			}
			else
			{
				cardInfo.Mods.Remove(cardModificationInfo);
			}
			card.SetInfo(cardInfo);
		}

		public static Ability ability;
	}
}
```
