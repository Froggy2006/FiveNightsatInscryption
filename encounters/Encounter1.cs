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
using FiveNightsatInscryption;
using DiskCardGame;
using FiveNightsAtInscryption;

namespace FiveNightsatInscryption.Encounters.Forest
{
	internal class FreddysPizzeria
	{
		public static void AddEncounter()
		{
			string name = "FreddysPizzeria";
			string regionName = "Forest";
			List<Tribe> tribes = new List<Tribe>
			{
				Tribe.Hooved
			};
			List<Ability> redundant = new List<Ability>
			{
				Ability.Strafe
			};
			bool regionLocked = true;
			List<CardInfo> randomReplacementCards = EncounterHelper.AddRandomCards("Squirrel", "none", "none", "none", "none", "none", "none", "none", "none");
			List<List<EncounterBlueprintData.CardBlueprint>> list = new List<List<EncounterBlueprintData.CardBlueprint>>();
			List<EncounterBlueprintData.CardBlueprint> list2 = new List<EncounterBlueprintData.CardBlueprint>();
			list2.Add(new EncounterBlueprintData.CardBlueprint
			{
				card = CardLoader.GetCardByName("Squirrel")
			});
			list2.Add(new EncounterBlueprintData.CardBlueprint
			{
				card = CardLoader.GetCardByName("FiveNightsatInscryption_BonnieTheBunny")
			});
			List<EncounterBlueprintData.CardBlueprint> list3 = new List<EncounterBlueprintData.CardBlueprint>();
			list3.Add(new EncounterBlueprintData.CardBlueprint
			{
				card = CardLoader.GetCardByName("FiveNightsatInscryption_ChicaTheChicken"),
				randomReplaceChance = 50
			});
			List<EncounterBlueprintData.CardBlueprint> list4 = new List<EncounterBlueprintData.CardBlueprint>();
			list4.Add(new EncounterBlueprintData.CardBlueprint
			{
				card = CardLoader.GetCardByName("Squirrel")
			});
			List<EncounterBlueprintData.CardBlueprint> item = new List<EncounterBlueprintData.CardBlueprint>();
			List<EncounterBlueprintData.CardBlueprint> list5 = new List<EncounterBlueprintData.CardBlueprint>();
			list5.Add(new EncounterBlueprintData.CardBlueprint
			{
				card = CardLoader.GetCardByName("FiveNightsatInscryption_FreddyFazbear")
			});
			List<EncounterBlueprintData.CardBlueprint> item2 = new List<EncounterBlueprintData.CardBlueprint>();
			List<EncounterBlueprintData.CardBlueprint> list6 = new List<EncounterBlueprintData.CardBlueprint>();
			list6.Add(new EncounterBlueprintData.CardBlueprint
			{
				card = CardLoader.GetCardByName("FiveNightsatInscryption_BonnieTheBunny"),
				randomReplaceChance = 50
			});
			list.Add(list2);
			list.Add(list3);
			list.Add(list4);
			list.Add(item);
			list.Add(list5);
			list.Add(item2);
			list.Add(list6);
			EncounterBlueprintData encounterBlueprintData = EncounterHelper.BuildBlueprint(name, tribes, redundant, regionLocked, 0, 30, randomReplacementCards, list);
			EncounterManager.Add(encounterBlueprintData);
			RegionExtensions.AddEncounters(EncounterHelper.GetRegionData(regionName), new EncounterBlueprintData[]
			{
				encounterBlueprintData
			});
		}
	}
}

