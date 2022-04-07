```cs
using BepInEx;
using BepInEx.Logging;
using BepInEx.Bootstrap;
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
using FiveNightsatInscryption.abilities;
using FiveNightsatInscryption.specialabilities;
using FiveNightsatInscryption.Encounters.Forest;
using Infiniscryption;
using Infiniscryption.PackManagement;
using Infiniscryption.Core;
using DiskCardGame;

namespace FiveNightsAtInscryption
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        private const string PluginGuid = "Cevin_2006.Inscryption.Fnai";
        private const string PluginName = "Fnai";
        private const string PluginVersion = "0.0.1";
        public static string staticpath;
        public static string Directory;
        public static List<Sprite> art_sprites;
        internal static ManualLogSource Log;


        private void Awake()
        {

                base.Logger.LogInfo("Loaded Five Nights at Inscryption! - made by Cevin_2006");
            Plugin.Log = base.Logger;

            art_sprites = new List<Sprite>();
            Sprite OriginalStarterDeck = TextureHelper.GetImageAsSprite("OriginalsDeck.png", TextureHelper.SpriteType.StarterDeckIcon);
            OriginalStarterDeck.name = "Originals";
            art_sprites.Add(OriginalStarterDeck);

            Harmony harmony = new Harmony("Cevin_2006.Inscryption.Fnai");
            harmony.PatchAll();
            //Abilities
            this.AddEnnardFusion();
            this.AddEnnardHeadFusion();
            this.AddMangleStrafe();
            this.AddPuppetsGift();
            this.AddRandomStrafe();
            this.AddStatic();
            this.AddAftonAbility();
            this.AddDamagedAbility();

            //Cards

            //Fnaf 1
            this.AddEndoskeleton();
            this.AddFreddyFazbear();
            this.AddBonnieTheBunny();
            this.AddChicaTheChicken();

            //Fnaf 2
            this.AddToyFreddy();
            this.AddToyBonnie();
            this.AddToyChica();
            this.AddWirePile();
            this.AddTheMangle();
            this.AddPuppet();
            this.AddWitheredFreddy();
            this.AddWitheredBonnie();
            this.AddWitheredChica();
            this.AddWitheredFoxy();

            //Fnaf 3
            this.AddPhantomFreddy();
            this.AddPhantomChica();
            this.AddPhantomFoxy();
            this.AddPhantomMangle();
            this.AddPhantomPuppet();

            //Fnaf 4
            this.AddNightmareFreddy();
            this.AddNightmareBonnie();
            this.AddNightmareChica();
            this.AddNightmareFoxy();
            this.AddNightmareFredbear();
            this.AddNightmare();

            //Fnaf 5/SL
            this.AddFuntimeFreddy();
            this.AddBaby();
            this.AddBallora();
            this.AddFuntimeFoxy();
            this.AddMoltenFreddy();

            //Fnaf 6/Pizzeria Simulator
            this.AddScrapBaby();
            this.AddLefty();
            this.AddEnnard();

            //Aftons Forms
            this.AddBurntrap();
            this.AddGlitchtrap();
            this.AddScraptrap();
            this.AddSpringtrap();

            //boss cards
            this.AddMalhare();

            //StarterDecks
            this.AddOriginalsDeck();
            this.AddToysDeck();
            this.AddKillersDeck();
            this.AddNightmarishDeck();
            this.AddEnigmaticDuoDeck();
            this.AddSLDeck();
            this.AddScrapDeck();
        }


        private void Start()
        {
            if (Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.packmanager"))
                CreatePack();

            //Encounters

            FreddysPizzeria.AddEncounter();
        }
        public static CardInfo GetCardByName(string CardName)
        {
            return ScriptableObjectLoader<CardInfo>.AllData.Find((CardInfo info) => info.name == CardName);
        }

        public Texture2D GetTexture(string path)
        {
            byte[] data = File.ReadAllBytes(Path.Combine(base.Info.Location.Replace("FiveNightsatInscryption.dll", ""), "Artwork/" + path));
            Texture2D texture2D = new Texture2D(2, 2);
            texture2D.LoadImage(data);
            texture2D.filterMode = FilterMode.Point;
            return texture2D;
        }

        public void AddEnnardFusion()
        {
            AbilityInfo abilityInfo = AbilityManager.New("Cevin_2006.Inscryption.Fnai", "Union", "NOT ADDED.",
                typeof(EnnardFusion),
                this.GetTexture("UnionSigil.png"));
            abilityInfo.SetPixelAbilityIcon(this.GetTexture("UnionSigilPixel.png"), null);
            abilityInfo.powerLevel = 3;
            abilityInfo.metaCategories = new List<AbilityMetaCategory>
            {
                AbilityMetaCategory.Part1Rulebook,
                AbilityMetaCategory.Part1Modular
            };
            abilityInfo.canStack = false;
            abilityInfo.opponentUsable = true;
            EnnardFusion.ability = abilityInfo.ability;
        }
        public void AddEnnardHeadFusion()
        {
            AbilityInfo abilityInfo = AbilityManager.New("Cevin_2006.Inscryption.Fnai", "Head of the Union", "NOT ADDED.",
                typeof(EnnardHeadFusion),
                this.GetTexture("UnionSigil.png"));
            abilityInfo.SetPixelAbilityIcon(this.GetTexture("UnionSigilPixel.png"), null);
            abilityInfo.powerLevel = 3;
            abilityInfo.metaCategories = new List<AbilityMetaCategory>
            {
                AbilityMetaCategory.Part1Rulebook,
                AbilityMetaCategory.Part1Modular
            };
            abilityInfo.canStack = false;
            abilityInfo.opponentUsable = true;
            EnnardHeadFusion.ability = abilityInfo.ability;
        }

        public void AddStatic()
        {
            AbilityInfo abilityInfo = AbilityManager.New("Cevin_2006.Inscryption.Fnai", "Static", "[creature] Irritates the Opponent(-2/-1).",
                typeof(StaticAbility),
                this.GetTexture("StaticSigil.png"));
            abilityInfo.SetPixelAbilityIcon(this.GetTexture("StaticSigilPixel.png"), null);
            abilityInfo.powerLevel = 2;
            abilityInfo.metaCategories = new List<AbilityMetaCategory>
            {
                AbilityMetaCategory.Part1Rulebook,
                AbilityMetaCategory.Part1Modular
            };
            abilityInfo.canStack = true;
            abilityInfo.opponentUsable = true;
            StaticAbility.ability = abilityInfo.ability;
        }
        public void AddMangleStrafe()
        {
            AbilityInfo abilityInfo = AbilityManager.New("Cevin_2006.Inscryption.Fnai", "Mangle Strafe", "when [creature] strafes it leaves behind piles of Wire.",
                typeof(MangleStrafe),
                this.GetTexture("MangleStrafeSigil.png"));
            abilityInfo.SetPixelAbilityIcon(this.GetTexture("MangleStrafeSigilPixel.png"), null);
            abilityInfo.powerLevel = 1;
            abilityInfo.metaCategories = new List<AbilityMetaCategory>
            {
                AbilityMetaCategory.Part1Rulebook,
                AbilityMetaCategory.Part1Modular
            };
            abilityInfo.canStack = false;
            abilityInfo.opponentUsable = true;
            MangleStrafe.ability = abilityInfo.ability;
        }
        public void AddRandomStrafe()
        {
            AbilityInfo abilityInfo = AbilityManager.New("Cevin_2006.Inscryption.Fnai", "Choreography", "[creature] Chooses a random Strafe Ability as part of the Choreography.",
                typeof(RandomStrafe),
                this.GetTexture("RandomStrafeSigil.png"));
            abilityInfo.SetPixelAbilityIcon(this.GetTexture("RandomStrafeSigilPixel.png"), null);
            abilityInfo.powerLevel = 2;
            abilityInfo.metaCategories = new List<AbilityMetaCategory>
            {
                AbilityMetaCategory.Part1Rulebook,
                AbilityMetaCategory.Part1Modular
            };
            abilityInfo.canStack = false;
            abilityInfo.opponentUsable = true;
            RandomStrafe.ability = abilityInfo.ability;
        }
        public void AddPuppetsGift()
        {
            AbilityInfo abilityInfo = AbilityManager.New("Cevin_2006.Inscryption.Fnai", "Puppets Gift", "if [creature] is placed it unbrittles your brittle.",
                typeof(PuppetsGift),
                this.GetTexture("PuppetsGiftSigil.png"));
            abilityInfo.SetPixelAbilityIcon(this.GetTexture("PuppetsGiftSigilPixel.png"), null);
            abilityInfo.powerLevel = 3;
            abilityInfo.metaCategories = new List<AbilityMetaCategory>
            {
                AbilityMetaCategory.Part1Rulebook,
                AbilityMetaCategory.Part1Modular
            };
            abilityInfo.canStack = false;
            abilityInfo.opponentUsable = false;
            PuppetsGift.ability = abilityInfo.ability;
        }
        public void AddAftonAbility()
        {
            AbilityInfo abilityInfo = AbilityManager.New("Cevin_2006.Inscryption.Fnai", "Incarnation", "if [creature] dies it gets reborn as a different form.",
                typeof(AftonAbility),
                this.GetTexture("AftonSigil.png"));
            abilityInfo.SetPixelAbilityIcon(this.GetTexture("AftonSigilPixel.png"), null);
            abilityInfo.powerLevel = 3;
            abilityInfo.metaCategories = new List<AbilityMetaCategory>
            {
                AbilityMetaCategory.Part1Rulebook,
                AbilityMetaCategory.Part1Modular
            };
            abilityInfo.canStack = false;
            abilityInfo.opponentUsable = false;
            AftonAbility.ability = abilityInfo.ability;
        }
        public void AddDamagedAbility()
        {
            AbilityInfo abilityInfo = AbilityManager.New("Cevin_2006.Inscryption.Fnai", "Damaged", "[creature] will retaliate, if [creature] is attacked.",
                typeof(DamagedAbility),
                this.GetTexture("DamagedSigil.png"));
            abilityInfo.SetPixelAbilityIcon(this.GetTexture("DamagedSigilPixel.png"), null);
            abilityInfo.powerLevel = 1;
            abilityInfo.metaCategories = new List<AbilityMetaCategory>
            {
                AbilityMetaCategory.Part1Rulebook,
                AbilityMetaCategory.Part1Modular
            };
            abilityInfo.canStack = true;
            abilityInfo.opponentUsable = true;
            DamagedAbility.ability = abilityInfo.ability;
        }
        private void AddEndoskeleton()
        {
            CardInfo card = CardManager.BaseGameCards.CardByName("Squirrel");
            card.SetPortrait("Endoskeleton.png");
            card.SetAltPortrait("Endoskeleton.png");
            card.SetPixelPortrait("EndoskeletonPixel.png");
            card.SetCost(energyCost: 1);
            card.SetBasic("Endoskeleton", 0, 2, "Skeleton of the Animatronics");

        }
        private void AddPuppet()
        {
            CardInfo card = CardManager.BaseGameCards.CardByName("PackRat");
            card.SetPortrait("Puppet.png");
            card.SetAltPortrait("Puppet.png");
            card.SetPixelPortrait("PuppetPixel.png");
            card.SetCost(energyCost: 1);
            card.SetBasic("The Puppet", 0, 2, "revives Spirits inside of the animatronics.");

        }
        private void AddFreddyFazbear()
        {
            CardInfo Freddy = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "FreddyFazbear",
                // Card display name.
                "Freddy Fazbear",
                // Attack.
                1,
                // Health.
                2,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 3)
            .AddAbilities(Ability.DoubleStrike)
            .SetPortrait("Freddy.png")
            .SetPixelPortrait("FreddyPixel.png")
            .SetEmissivePortrait("Freddy_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Freddy);
        }
        private void AddBonnieTheBunny()
        {
            CardInfo Bonnie = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "BonnieTheBunny",
                // Card display name.
                "Bonnie the Bunny",
                // Attack.
                1,
                // Health.
                2,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 2)
            .AddAbilities(Ability.Reach)
            .SetPortrait("Bonnie.png")
            .SetPixelPortrait("BonniePixel.png")
            .SetEmissivePortrait("Bonnie_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Bonnie);
        }
        private void AddChicaTheChicken()
        {
            CardInfo Chica = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "ChicaTheChicken",
                // Card display name.
                "Chica the Chicken",
                // Attack.
                1,
                // Health.
                2,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 3)
            .AddAbilities(Ability.MadeOfStone)
            .SetPortrait("Chica.png")
            .SetPixelPortrait("ChicaPixel.png")
            .SetEmissivePortrait("Chica_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Chica);
        }
        private void AddFoxyThePirateFox()
        {
            CardInfo Foxy = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "FoxyThePirateFox",
                // Card display name.
                "Foxy the Pirate",
                // Attack.
                2,
                // Health.
                1,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 3)
            .AddAbilities(Ability.Strafe)
            .SetPortrait("foxy.png")
            .SetPixelPortrait("foxyPixel.png")
            .SetEmissivePortrait("foxy_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Foxy);
        }
        private void AddToyFreddy()
        {
            CardInfo ToyFreddy = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "ToyFreddy",
                // Card display name.
                "Toy Freddy",
                // Attack.
                1,
                // Health.
                1,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 2)
            .AddAbilities(Ability.ExplodeOnDeath)
            .SetPortrait("ToyFreddy.png")
            .SetPixelPortrait("ToyFreddyPixel.png")
            .SetEmissivePortrait("ToyFreddy_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", ToyFreddy);
        }
        private void AddToyBonnie()
        {
            CardInfo ToyBonnie = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "ToyBonnie",
                // Card display name.
                "Toy Bonnie",
                // Attack.
                0,
                // Health.
                3,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 3)
            .AddAbilities(Ability.ActivatedStatsUpEnergy)
            .SetPortrait("ToyBonnie.png")
            .SetPixelPortrait("ToyBonniePixel.png")
            .SetEmissivePortrait("ToyBonnie_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", ToyBonnie);
        }
        private void AddToyChica()
        {
            CardInfo ToyChica = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "ToyChica",
                // Card display name.
                "ToyChica",
                // Attack.
                0,
                // Health.
                1,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 3)
            .AddAbilities(Ability.GainBattery)
            .SetPortrait("ToyChica.png")
            .SetPixelPortrait("ToyChicaPixel.png")
            .SetEmissivePortrait("ToyChica_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", ToyChica);
        }
        private void AddTheMangle()
        {
            CardInfo Mangle = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "TheMangle",
                // Card display name.
                "The Mangle",
                // Attack.
                1,
                // Health.
                2,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 2)
            .AddAbilities(MangleStrafe.ability)
            .SetPortrait("Mangle.png")
            .SetPixelPortrait("ManglePixel.png")
            .SetEmissivePortrait("Mangle_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Mangle);
        }
        private void AddWirePile()
        {
            CardInfo WirePile = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "WirePile",
                // Card display name.
                "Wire Pile",
                // Attack.
                0,
                // Health.
                1,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .AddAbilities(Ability.SteelTrap)
            .SetPortrait("WirePile.png")
            .SetTerrain()
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", WirePile);
        }
        private void AddWitheredFreddy()
        {
            CardInfo Freddy = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "WitheredFreddy",
                // Card display name.
                "Withered Freddy",
                // Attack.
                0,
                // Health.
                2,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 1)
            .AddAbilities(DamagedAbility.ability)
            .SetPortrait("WitheredFreddy.png")
            .SetEmissivePortrait("WitheredFreddy_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Freddy);
        }
        private void AddWitheredBonnie()
        {
            CardInfo Bonnie = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "WitheredBonnie",
                // Card display name.
                "Withered Bonnie",
                // Attack.
                0,
                // Health.
                3,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 2)
            .AddAbilities(DamagedAbility.ability, Ability.Reach)
            .SetPortrait("WitheredBonnie.png")
            .SetEmissivePortrait("WitheredBonnie_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Bonnie);
        }
        private void AddWitheredChica()
        {
            CardInfo Chica = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "WitheredChica",
                // Card display name.
                "Withered Chica",
                // Attack.
                0,
                // Health.
                3,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 3)
            .AddAbilities(DamagedAbility.ability, Ability.Reach)
            .SetPortrait("WitheredChica.png")
            .SetEmissivePortrait("WitheredChica_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Chica);
        }
        private void AddWitheredFoxy()
        {
            CardInfo Foxy = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "WitheredFoxy",
                // Card display name.
                "Withered Foxy",
                // Attack.
                0,
                // Health.
                3,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 3)
            .AddAbilities(DamagedAbility.ability, Ability.Strafe)
            .SetPortrait("WitheredFoxy.png")
            .SetEmissivePortrait("WitheredFoxy_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Foxy);
        }
        private void AddPhantomFreddy()
        {
            CardInfo PhantomFreddy = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "PhantomFreddy",
                // Card display name.
                "PhantomFreddy",
                // Attack.
                1,
                // Health.
                1,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 2)
            .AddAbilities(Ability.Brittle, Ability.Sentry)
            .SetPortrait("PhantomFreddy.png")
            .SetPixelPortrait("PhantomFreddyPixel.png")
            .SetEmissivePortrait("PhantomFreddy_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", PhantomFreddy);
        }
        private void AddPhantomChica()
        {
            CardInfo PhantomChica = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "PhantomChica",
                // Card display name.
                "Phantom Chica",
                // Attack.
                1,
                // Health.
                3,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 1)
            .AddAbilities(Ability.Brittle)
            .SetPortrait("PhantomChica.png")
            .SetPixelPortrait("PhantomChicaPixel.png")
            .SetEmissivePortrait("PhantomChica_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", PhantomChica);
        }
        private void AddPhantomFoxy()
        {
            CardInfo PhantomFoxy = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "PhantomFoxy",
                // Card display name.
                "Phantom Foxy",
                // Attack.
                1,
                // Health.
                2,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 2)
            .AddAbilities(Ability.Strafe, RandomStrafe.ability)
            .SetPortrait("PhantomFoxy.png")
            .SetPixelPortrait("PhantomFoxyPixel.png")
            .SetEmissivePortrait("PhantomFoxy_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", PhantomFoxy);
        }
        private void AddPhantomMangle()
        {
            CardInfo PhantomMangle = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "PhantomMangle",
                // Card display name.
                "Phantom Mangle",
                // Attack.
                1,
                // Health.
                2,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 2)
            .AddAbilities(Ability.Brittle, RandomStrafe.ability)
            .SetPortrait("PhantomMangle.png")
            .SetPixelPortrait("PhantomManglePixel.png")
            .SetEmissivePortrait("PhantomMangle_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", PhantomMangle);
        }
        private void AddPhantomPuppet()
        {
            CardInfo PhantomPuppet = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "PhantomPuppet",
                // Card display name.
                "Phantom Puppet",
                // Attack.
                0,
                // Health.
                2,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 2)
            .AddAbilities(DamagedAbility.ability)
            .SetPortrait("PhantomPuppet.png")
            .SetPixelPortrait("PhantomPuppetPixel.png")
            .SetEmissivePortrait("PhantomPuppet_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", PhantomPuppet);
        }
        private void AddSpringtrap()
        {
            CardInfo Springtrap = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "Springtrap",
                // Card display name.
                "Springtrap",
                // Attack.
                1,
                // Health.
                1,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 2)
            .AddAbilities(AftonAbility.ability)
            .SetIceCube("FiveNightsatInscryption_Scraptrap")
            .SetPortrait("Springtrap.png")
            .SetPixelPortrait("SpringtrapPixel.png")
            .SetEmissivePortrait("Springtrap_e.png")
            .AddMetaCategories(CardMetaCategory.Rare)
            .SetRare()
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Springtrap);
        }
        private void AddNightmareFreddy()
        {
            CardInfo NightmareFreddy = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "NightmareFreddy",
                // Card display name.
                "Nightmare Freddy",
                // Attack.
                1,
                // Health.
                1,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 3)
            .AddAbilities(Ability.DrawRabbits)
            .SetPortrait("NightmareFreddy.png")
            .SetPixelPortrait("NightmareFreddyPixel.png")
            .SetEmissivePortrait("NightmareFreddy_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", NightmareFreddy);
        }
        private void AddNightmareBonnie()
        {
            CardInfo NightmareBonnie = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "NightmareBonnie",
                // Card display name.
                "Nightmare Bonnie",
                // Attack.
                1,
                // Health.
                1,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 2)
            .AddAbilities(Ability.SplitStrike)
            .SetPortrait("NightmareBonnie.png")
            .SetPixelPortrait("NightmareBonniePixel.png")
            .SetEmissivePortrait("NightmareBonnie_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", NightmareBonnie);
        }
        private void AddNightmareChica()
        {
            CardInfo NightmareChica = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "NightmareChica",
                // Card display name.
                "Nightmare Chica",
                // Attack.
                1,
                // Health.
                2,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 3)
            .AddAbilities(Ability.GuardDog, Ability.Sentry)
            .SetPortrait("NightmareChica.png")
            .SetPixelPortrait("NightmareChicaPixel.png")
            .SetEmissivePortrait("NightmareChica_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", NightmareChica);
        }
        private void AddNightmareFoxy()
        {
            CardInfo NightmareFoxy = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "NightmareFoxy",
                // Card display name.
                "Nightmare Foxy",
                // Attack.
                2,
                // Health.
                1,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 4)
            .AddAbilities(Ability.Strafe)
            .SetPortrait("NightmareFoxy.png")
            .SetPixelPortrait("NightmareFoxyPixel.png")
            .SetEmissivePortrait("NightmareFoxy_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", NightmareFoxy);
        }
        private void AddNightmareFredbear()
        {
            CardInfo nightmareFredbear = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "NightmareFredbear",
                // Card display name.
                "Nightmare Fredbear",
                // Attack.
                0,
                // Health.
                1,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 3)
            .AddSpecialAbilities(SpecialTriggeredAbility.BellProximity)
            .SetStatIcon(SpecialStatIcon.Bell)
            .SetPortrait("NightmareFredbear.png")
            .SetPixelPortrait("FredbearPixel.png")
            .SetEmissivePortrait("NightmareFredbear_e.png")
            .AddMetaCategories(CardMetaCategory.Rare)
            .SetRare()
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", nightmareFredbear);
        }
        private void AddNightmare()
        {
            CardInfo Nightmare = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "Nightmare",
                // Card display name.
                "Nightmare",
                // Attack.
                0,
                // Health.
                1,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 3)
            .AddSpecialAbilities(SpecialTriggeredAbility.CardsInHand)
            .SetStatIcon(SpecialStatIcon.CardsInHand)
            .SetPortrait("Nightmare.png")
            .SetPixelPortrait("NightmarePixel.png")
            .SetEmissivePortrait("Nightmare_e.png")
            .AddMetaCategories(CardMetaCategory.Rare)
            .SetRare()
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Nightmare);
        }
        private void AddFuntimeFreddy()
        {
            CardInfo FuntimeFreddy = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "FuntimeFreddy",
                // Card display name.
                "Funtime Freddy",
                // Attack.
                1,
                // Health.
                2,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 4)
            .AddAbilities(EnnardFusion.ability, Ability.DoubleStrike)
            .SetPortrait("FuntimeFreddy.png")
            .SetPixelPortrait("FuntimeFreddyPixel.png")
            .SetEmissivePortrait("FuntimeFreddy_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", FuntimeFreddy);
        }
        private void AddBaby()
        {
            CardInfo Baby = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "FuntimeBaby",
                // Card display name.
                "Baby",
                // Attack.
                1,
                // Health.
                2,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 3)
            .AddAbilities(EnnardHeadFusion.ability, Ability.SteelTrap)
            .SetPortrait("Baby.png")
            .SetPixelPortrait("BabyPixel.png")
            .SetEmissivePortrait("Baby_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Baby);
        }
        private void AddBallora()
        {
            CardInfo Ballora = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "Ballora",
                // Card display name.
                "Ballora",
                // Attack.
                1,
                // Health.
                2,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 2)
            .AddAbilities(EnnardFusion.ability, RandomStrafe.ability)
            .SetPortrait("Ballora.png")
            .SetPixelPortrait("BalloraPixel.png")
            .SetEmissivePortrait("Ballora_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Ballora);
        }
        private void AddFuntimeFoxy()
        {
            CardInfo FuntimeFoxy = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "FuntimeFoxy",
                // Card display name.
                "Funtime Foxy",
                // Attack.
                2,
                // Health.
                1,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 4)
            .AddAbilities(EnnardFusion.ability, Ability.StrafeSwap)
            .SetPortrait("FuntimeFoxy.png")
            .SetPixelPortrait("FuntimeFoxyPixel.png")
            .SetEmissivePortrait("FuntimeFoxy_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", FuntimeFoxy);
        }
        private void AddEnnard()
        {
            CardInfo Ennard = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "FuntimeEnnard",
                // Card display name.
                "Ennard",
                // Attack.
                5,
                // Health.
                4,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 6, bonesCost: 5)
            .AddAbilities(Ability.TailOnHit, Ability.SquirrelStrafe)
            .SetTail("FiveNightsatInscryption_ScrapBaby", "MoltenFreddy.png")
            .SetPortrait("Ennard.png")
            .SetPixelPortrait("EnnardPixel.png")
            .SetEmissivePortrait("Ennard_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Ennard);
        }
        private void AddMoltenFreddy()
        {
            CardInfo Ennard = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "MoltenFreddy",
                // Card display name.
                "Molten Freddy",
                // Attack.
                4,
                // Health.
                3,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 5, bonesCost: 4)
            .AddAbilities(Ability.AllStrike, DamagedAbility.ability)
            .SetPortrait("MoltenFreddy.png")
            .SetPixelPortrait("MoltenFreddyPixel.png")
            .SetEmissivePortrait("MoltenFreddy_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Ennard);
        }
        private void AddScrapBaby()
        {
            CardInfo Baby = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "ScrapBaby",
                // Card display name.
                "Scrap Baby",
                // Attack.
                2,
                // Health.
                2,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 4)
            .AddAbilities(DamagedAbility.ability, Ability.SteelTrap)
            .SetPortrait("ScrapBaby.png")
            .SetPixelPortrait("ScrapBabyPixel.png")
            .SetEmissivePortrait("ScrapBaby_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Baby);
        }
        private void AddLefty()
        {
            CardInfo Lefty = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "Lefty",
                // Card display name.
                "Lefty",
                // Attack.
                1,
                // Health.
                3,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 4)
            .AddAbilities(Ability.IceCube)
            .SetIceCube("PackRat")
            .SetPortrait("Lefty.png")
            .SetPixelPortrait("LeftyPixel.png")
            .SetEmissivePortrait("Lefty_e.png")
            .AddMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer)
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Lefty);
        }
        private void AddScraptrap()
        {
            CardInfo Springtrap = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "Scraptrap",
                // Card display name.
                "Scraptrap",
                // Attack.
                2,
                // Health.
                2,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 3)
            .AddAbilities(AftonAbility.ability, DamagedAbility.ability)
            .SetIceCube("FiveNightsatInscryption_Glitchtrap")
            .SetPortrait("Scraptrap.png")
            .SetPixelPortrait("ScraptrapPixel.png")
            .SetEmissivePortrait("Scraptrap_e.png")
            .AddMetaCategories(CardMetaCategory.Rare)
            .SetRare()
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Springtrap);
        }
        private void AddGlitchtrap()
        {
            CardInfo Springtrap = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "Glitchtrap",
                // Card display name.
                "Glitchtrap",
                // Attack.
                3,
                // Health.
                3,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 4)
            .AddAbilities(AftonAbility.ability)
            .SetIceCube("FiveNightsatInscryption_Burntrap")
            .SetPortrait("Glitchtrap.png")
            .SetPixelPortrait("GlitchtrapPixel.png")
            .SetEmissivePortrait("Glitchtrap_e.png")
            .AddMetaCategories(CardMetaCategory.Rare)
            .SetRare()
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Springtrap);
        }
        private void AddBurntrap()
        {
            CardInfo Springtrap = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "Burntrap",
                // Card display name.
                "Burntrap",
                // Attack.
                3,
                // Health.
                3,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 6)
            .AddAbilities(AftonAbility.ability)
            .SetIceCube("FiveNightsatInscryption_Burntrap")
            .AddSpecialAbilities(SpecialTriggeredAbility.Ouroboros)
            .SetPortrait("Burntrap.png")
            .SetPixelPortrait("BurntrapPixel.png")
            .SetEmissivePortrait("Burntrap_e.png")
            .AddMetaCategories(CardMetaCategory.Rare)
            .SetRare()
            .SetDefaultPart1Card();

            ;
            CardManager.Add("FiveNightsatInscryption", Springtrap);
        }
        private void AddMalhare()
        {
            CardInfo Malhare = CardManager.New(

                // Card ID Prefix
                modPrefix: "FiveNightsatInscryption",

                // Card internal name.
                "!GIANTCARD_MALHARE",
                // Card display name.
                "Malhare",
                // Attack.
                2,
                // Health.
                60,
                // Descryption.
                description: "Kill this abomination, please."
            )
            .SetCost(energyCost: 6)
            .SetPortrait("Malhare.png")
            .AddAppearances(CardAppearanceBehaviour.Appearance.GiantAnimatedPortrait)
            .AddSpecialAbilities(SpecialTriggeredAbility.GiantCard)
            .AddTraits(Trait.Giant, Trait.Uncuttable)
            .AddAbilities(Ability.MadeOfStone, Ability.AllStrike)
            ;
            CardManager.Add("FiveNightsatInscryption", Malhare);
        }
        private void AddOriginalsDeck()
        {
            StarterDeckInfo originalsDeck = ScriptableObject.CreateInstance<StarterDeckInfo>();
            originalsDeck.title = "OriginalsDeck";
            originalsDeck.iconSprite = TextureHelper.GetImageAsSprite("OriginalsDeck.png", TextureHelper.SpriteType.StarterDeckIcon);
            originalsDeck.cards = new List<CardInfo>()
            {
                CardLoader.GetCardByName("FiveNightsatInscryption_FreddyFazbear"),
                CardLoader.GetCardByName("FiveNightsatInscryption_BonnieTheBunny"),
                CardLoader.GetCardByName("FiveNightsatInscryption_ChicaTheChicken")
            };
            StarterDeckManager.Add("Cevin_2006.Inscryption.Fnai", originalsDeck);
        }
        private void AddToysDeck()
        {
            StarterDeckInfo toysDeck = ScriptableObject.CreateInstance<StarterDeckInfo>();
            toysDeck.title = "ToyDeck";
            toysDeck.iconSprite = TextureHelper.GetImageAsSprite("ToyDeck.png", TextureHelper.SpriteType.StarterDeckIcon);
            toysDeck.cards = new List<CardInfo>()
            {
                CardLoader.GetCardByName("FiveNightsatInscryption_ToyFreddy"),
                CardLoader.GetCardByName("FiveNightsatInscryption_ToyBonnie"),
                CardLoader.GetCardByName("FiveNightsatInscryption_ToyChica")
            };
            StarterDeckManager.Add("Cevin_2006.Inscryption.Fnai", toysDeck);
        }
        private void AddKillersDeck()
        {
            StarterDeckInfo killersDeck = ScriptableObject.CreateInstance<StarterDeckInfo>();
            killersDeck.title = "KillersDeck";
            killersDeck.iconSprite = TextureHelper.GetImageAsSprite("GhostlyDeck.png", TextureHelper.SpriteType.StarterDeckIcon);
            killersDeck.cards = new List<CardInfo>()
            {
                CardLoader.GetCardByName("FiveNightsatInscryption_Springtrap"),
                CardLoader.GetCardByName("FiveNightsatInscryption_PhantomFreddy"),
                CardLoader.GetCardByName("FiveNightsatInscryption_PhantomChica")
            };
            StarterDeckManager.Add("Cevin_2006.Inscryption.Fnai", killersDeck);
        }
        private void AddNightmarishDeck()
        {
            StarterDeckInfo nightmareDeck = ScriptableObject.CreateInstance<StarterDeckInfo>();
            nightmareDeck.title = "KillersDeck";
            nightmareDeck.iconSprite = TextureHelper.GetImageAsSprite("NightmarishDeck.png", TextureHelper.SpriteType.StarterDeckIcon);
            nightmareDeck.cards = new List<CardInfo>()
            {
                CardLoader.GetCardByName("FiveNightsatInscryption_NightmareFreddy"),
                CardLoader.GetCardByName("FiveNightsatInscryption_NightmareBonnie"),
                CardLoader.GetCardByName("FiveNightsatInscryption_NightmareChica")
            };
            StarterDeckManager.Add("Cevin_2006.Inscryption.Fnai", nightmareDeck);
        }
        private void AddEnigmaticDuoDeck()
        {
            StarterDeckInfo enigmaticDeck = ScriptableObject.CreateInstance<StarterDeckInfo>();
            enigmaticDeck.title = "enigmaticDeck";
            enigmaticDeck.iconSprite = TextureHelper.GetImageAsSprite("NightmareDuoDeck.png", TextureHelper.SpriteType.StarterDeckIcon);
            enigmaticDeck.cards = new List<CardInfo>()
            {
                CardLoader.GetCardByName("FiveNightsatInscryption_Nightmare"),
                CardLoader.GetCardByName("FiveNightsatInscryption_NightmareFredbear"),
                CardLoader.GetCardByName("PeltHare")
            };
            StarterDeckManager.Add("Cevin_2006.Inscryption.Fnai", enigmaticDeck);
        }
        private void AddSLDeck()
        {
            StarterDeckInfo slDeck = ScriptableObject.CreateInstance<StarterDeckInfo>();
            slDeck.title = "IceCreamDeck";
            slDeck.iconSprite = TextureHelper.GetImageAsSprite("SisterLocationDeck.png", TextureHelper.SpriteType.StarterDeckIcon);
            slDeck.cards = new List<CardInfo>()
            {
                CardLoader.GetCardByName("FiveNightsatInscryption_FuntimeFreddy"),
                CardLoader.GetCardByName("FiveNightsatInscryption_FuntimeFoxy"),
                CardLoader.GetCardByName("FiveNightsatInscryption_Ballora")
            };
            StarterDeckManager.Add("Cevin_2006.Inscryption.Fnai", slDeck);
        }
        private void AddScrapDeck()
        {
            StarterDeckInfo scrapDeck = ScriptableObject.CreateInstance<StarterDeckInfo>();
            scrapDeck.title = "ScrappyDeck";
            scrapDeck.iconSprite = TextureHelper.GetImageAsSprite("ScrapDeck.png", TextureHelper.SpriteType.StarterDeckIcon);
            scrapDeck.cards = new List<CardInfo>()
            {
                CardLoader.GetCardByName("FiveNightsatInscryption_ScrapBaby"),
                CardLoader.GetCardByName("FiveNightsatInscryption_Lefty"),
                CardLoader.GetCardByName("FiveNightsatInscryption_Scraptrap")
            };
            StarterDeckManager.Add("Cevin_2006.Inscryption.Fnai", scrapDeck);
        }
        public static void CreatePack()
        {
            PackInfo incrediPack = PackManager.GetPackInfo("FiveNightsatInscryption");
            incrediPack.Title = "Five Nights at Inscryption";
            incrediPack.SetTexture(TextureHelper.GetImageAsTexture("Artwork/FNAI_pack.png"));
            incrediPack.Description = "This card pack contains a bunch of FNAF themed Cards, from Freddy Fazbear to Springtrap.";
            incrediPack.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
        }
    }
}
```
