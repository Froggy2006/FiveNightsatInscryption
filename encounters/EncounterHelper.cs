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

namespace FiveNightsatInscryption.Encounters
{
	public class EncounterHelper
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000023B0 File Offset: 0x000005B0
		public static EncounterBlueprintData BuildBlueprint(string name, List<Tribe> tribes, List<Ability> redundant, bool regionLocked, int minDifficulty, int maxDifficulty, List<CardInfo> randomReplacementCards, List<List<EncounterBlueprintData.CardBlueprint>> blueprintTurns)
		{
			EncounterBlueprintData encounterBlueprintData = ScriptableObject.CreateInstance<EncounterBlueprintData>();
			encounterBlueprintData.name = name;
			encounterBlueprintData.regionSpecific = regionLocked;
			EncounterExtensions.SetDifficulty<EncounterBlueprintData>(encounterBlueprintData, minDifficulty, maxDifficulty);
			encounterBlueprintData.dominantTribes = tribes;
			encounterBlueprintData.redundantAbilities = redundant;
			encounterBlueprintData.randomReplacementCards = randomReplacementCards;
			encounterBlueprintData.turns = blueprintTurns;
			return encounterBlueprintData;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002404 File Offset: 0x00000604
		public static CardInfo GetCardInfo(string self)
		{
			return CardLoader.GetCardByName(self);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000241C File Offset: 0x0000061C
		public static RegionData GetRegionData(string RegionName)
		{
			RegionData result = RegionManager.BaseGameRegions[0];
			for (int i = 0; i < RegionManager.BaseGameRegions.Count; i++)
			{
				bool flag = RegionManager.BaseGameRegions[i].name == RegionName;
				if (flag)
				{
					result = RegionManager.BaseGameRegions[i];
				}
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002480 File Offset: 0x00000680
		public static EncounterBlueprintData GetBlueprintData(string EncounterName)
		{
			EncounterBlueprintData result = EncounterManager.BaseGameEncounters[0];
			for (int i = 0; i < EncounterManager.BaseGameEncounters.Count; i++)
			{
				bool flag = EncounterManager.BaseGameEncounters[i].name == EncounterName;
				if (flag)
				{
					result = EncounterManager.BaseGameEncounters[i];
				}
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024E4 File Offset: 0x000006E4
		public static List<CardInfo> AddRandomCards(string turn1 = "none", string turn2 = "none", string turn3 = "none", string turn4 = "none", string turn5 = "none", string turn6 = "none", string turn7 = "none", string turn8 = "none", string turn9 = "none")
		{
			List<CardInfo> list = new List<CardInfo>();
			bool flag = turn1 != "none";
			List<CardInfo> result;
			if (flag)
			{
				list.Add(CardLoader.GetCardByName(turn1));
				bool flag2 = turn2 != "none";
				if (flag2)
				{
					list.Add(CardLoader.GetCardByName(turn2));
					bool flag3 = turn3 != "none";
					if (flag3)
					{
						list.Add(CardLoader.GetCardByName(turn3));
						bool flag4 = turn4 != "none";
						if (flag4)
						{
							list.Add(CardLoader.GetCardByName(turn4));
							bool flag5 = turn5 != "none";
							if (flag5)
							{
								list.Add(CardLoader.GetCardByName(turn5));
								bool flag6 = turn6 != "none";
								if (flag6)
								{
									list.Add(CardLoader.GetCardByName(turn6));
									bool flag7 = turn7 != "none";
									if (flag7)
									{
										list.Add(CardLoader.GetCardByName(turn7));
										bool flag8 = turn8 != "none";
										if (flag8)
										{
											list.Add(CardLoader.GetCardByName(turn8));
											bool flag9 = turn9 != "none";
											if (flag9)
											{
												list.Add(CardLoader.GetCardByName(turn9));
												result = list;
											}
											else
											{
												result = list;
											}
										}
										else
										{
											result = list;
										}
									}
									else
									{
										result = list;
									}
								}
								else
								{
									result = list;
								}
							}
							else
							{
								result = list;
							}
						}
						else
						{
							result = list;
						}
					}
					else
					{
						result = list;
					}
				}
				else
				{
					result = list;
				}
			}
			else
			{
				result = list;
			}
			return result;
		}
	}
}

```
