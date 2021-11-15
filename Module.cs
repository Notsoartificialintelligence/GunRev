using ItemAPI;
using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace GunRev
{
    public class Module : ETGModule
    {
        public static readonly string MOD_NAME = "Gundustrial Revolution";
        public static readonly string VERSION = "1.1.4";
        public static readonly string TEXT_COLOR = "#677e9e";
        public static string ZipFilePath;
        public static string FilePath;
        public static AdvancedStringDB Strings;
        public override void Start()
        {
            try
            {
                HashSet<string> _LockedNamespaces = OMITBReflectionHelpers.ReflectGetField<HashSet<string>>(typeof(IDPool<AIActor>), "_LockedNamespaces", Gungeon.Game.Enemies);
                _LockedNamespaces.Remove("gungeon");

                EnemyAPI.Hooks.Init();
                EnemyAPI.EnemyTools.Init();
                EnemyAPI.EnemyBuilder.Init();
                ItemBuilder.Init();
                Module.Strings = new AdvancedStringDB();
                GungeonAPI.Tools.Init();
                CustomClipAmmoTypeToolbox.Init();

                Hook foyerCallbacksHook = new Hook(
                    typeof(Foyer).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance),
                    typeof(Module).GetMethod(nameof(Module.FoyerAwake))
                );

                Hook quickRestartHook = new Hook(
                    typeof(GameManager).GetMethod("DelayedQuickRestart", BindingFlags.Public | BindingFlags.Instance),
                    typeof(Module).GetMethod(nameof(Module.OnQuickRestart))
                );

                //version 1.0.0
                Vintage.Add();
                Atomic.Add();
                Graph.Register();
                //HoloPlush.Register();
                BoltBlaster.Add();
                FamiliarPhone.Register();
                Keyboard.Add();
                MegaCannon.Add();
                BarrelBullets.Register();
                BeamTurret.Init();
                HoloBullet.Init();
                RadCan.Register();
                //RefreshKey.Register();
                WhileGunShoot.Add();
                OilJar.Register();
                //Guntry.Add();
                MagnesiumGuonStone.Init();
                Catalyst.Add();
                SingleUseGun.Add();
                TableTechPause.Register();

                //version 1.1.0
                Gunbatus.Add();
                Trigunometry.Add();
                SilverCursor.Register();
                Button.Register();

                //version 1.1.1
                SlimePendant.Init();
                ACERifle.Add();

                //version 1.1.2
                Dematerialiser.Register();
                GungeonGun.Add();
                Chair.Add();

                //version 1.1.3
                Robullets.Register();
                //BinaryBoxOn.Add();
                //Shockgun.Add();
                Cryptocurrency.Init();
                GildedInfusion.Register();

                //version 1.1.4
                Piston.Add();
                TNTItem.Init();
                //StackOfDirt.Init();
                LaserShotgun.Add();
                DiscouragementBeam.Add();

                //cheeky form transformations
                //Guntry2.Add();
                //Guntry3.Add();
                //Guntry4.Add();
                //Guntry5.Add();

                ZipFilePath = this.Metadata.Archive;
                FilePath = this.Metadata.Directory;
                AudioResourceLoader.InitAudio();

                var syn = CustomSynergies.Add("Meltdown Averted", new List<string> { "ai:reactor_core", "ice_cube" }, null, false);
                syn.ActiveWhenGunUnequipped = false;
                syn.statModifiers = new List<StatModifier>(0)
                {
                StatModifier.Create(PlayerStats.StatType.RateOfFire,StatModifier.ModifyMethod.ADDITIVE, 1f)
                };
                syn.bonusSynergies = new List<CustomSynergyType>();

                var syn1 = CustomSynergies.Add("Battery Buddies", new List<string> { "ai:vintage", "thunderclap" }, null, true);
                syn1.ActiveWhenGunUnequipped = false;
                syn1.bonusSynergies = new List<CustomSynergyType>();

                var syn2 = CustomSynergies.Add("Explosiver Guon Stone", new List<string> { "ai:magnesium_guon_stone", "bomb" }, null, true);
                syn2.bonusSynergies = new List<CustomSynergyType>();

                var syn3 = CustomSynergies.Add("2 Kool 4 U", new List<string> { "ai:rad_can", "rad_gun" }, null, false);
                syn3.ActiveWhenGunUnequipped = false;
                syn3.statModifiers = new List<StatModifier>(0)
                {
                    StatModifier.Create(PlayerStats.StatType.ReloadSpeed,StatModifier.ModifyMethod.ADDITIVE, -0.3f),
                    StatModifier.Create(PlayerStats.StatType.Coolness,StatModifier.ModifyMethod.ADDITIVE, 2f)
                };
                syn3.bonusSynergies = new List<CustomSynergyType>();

                var syn4 = CustomSynergies.Add("All That Glitters Is Gold", new List<string> { "au_gun" }, new List<string> { "gold_junk", "gold_ammolet", "gilded_bullets", }, false);
                syn4.ActiveWhenGunUnequipped = false;
                syn4.statModifiers = new List<StatModifier>(0)
                {
                    StatModifier.Create(PlayerStats.StatType.MoneyMultiplierFromEnemies,StatModifier.ModifyMethod.ADDITIVE,1f),
                    StatModifier.Create(PlayerStats.StatType.Damage,StatModifier.ModifyMethod.ADDITIVE,1f),
                };
                syn4.bonusSynergies = new List<CustomSynergyType>();

                var syn5 = CustomSynergies.Add("Ultimate Automation", new List<string> { "nanomachines", "bionic_leg" }, null, true);
                syn5.statModifiers = new List<StatModifier>(0)
                {
                    StatModifier.Create(PlayerStats.StatType.Curse,StatModifier.ModifyMethod.ADDITIVE,1f),
                    StatModifier.Create(PlayerStats.StatType.Accuracy,StatModifier.ModifyMethod.ADDITIVE,-3f),
                    StatModifier.Create(PlayerStats.StatType.ReloadSpeed,StatModifier.ModifyMethod.MULTIPLICATIVE,0.75f),
                };
                syn5.bonusSynergies = new List<CustomSynergyType>();

                var syn6 = CustomSynergies.Add("Knife To A Gunfight", new List<string> { "ai:enter_the_gungeon", "ai:slime_pendant" }, null, false);
                syn6.bonusSynergies = new List<CustomSynergyType>();

                var syn7 = CustomSynergies.Add("Children Of Kaliber", new List<string> { "ai:enter_the_gungeon", "high_kaliber" }, null, true);
                syn7.bonusSynergies = new List<CustomSynergyType>();

                var syn8 = CustomSynergies.Add("Sentry Mode Activated", new List<string> { "ai:robullets", "portable_turret" }, null, true);
                syn8.bonusSynergies = new List<CustomSynergyType>();

                var syn9 = CustomSynergies.Add("Isn't It Iron Pick", new List<string> { "ai:piston", "big_iron" }, null, true);
                syn9.bonusSynergies = new List<CustomSynergyType>();

                if (PickupObjectDatabase.GetByEncounterName("Dispenser") != null)
                {
                    var syn10 = CustomSynergies.Add("Redstone Engineering", new List<string> { "ai:piston", "cel:dispenser" }, null, true);
                    syn10.bonusSynergies = new List<CustomSynergyType>();
                }

                //var syn11 = CustomSynergies.Add("Cobblestone", new List<string> { "ai:stack_of_dirt" }, new List<string> { "pink_guon_stone", "red_guon_stone", "white_guon_stone", "orange_guon_stone", "clear_guon_stone", "blue_guon_stone", "green_guon_stone", "glass_guon_stone" }, true);
                //syn11.bonusSynergies = new List<CustomSynergyType>();

                //var syn12 = CustomSynergies.Add("Brick Block", new List<string> { "ai:stack_of_dirt" }, new List<string> { "brick_breaker", "brick_of_cash" }, true);
                //syn12.bonusSynergies = new List<CustomSynergyType>();

                var syn13 = CustomSynergies.Add("Aperture Science", new List<string> { "ai:thermal_discouragement_beam", "science_cannon" }, null, true);
                syn13.bonusSynergies = new List<CustomSynergyType>();

                var syn14 = CustomSynergies.Add("Upgrades People, Upgrades!", new List<string> { "ai:laser_shotgun", "laser_sight" }, null, false);
                syn14.ActiveWhenGunUnequipped = false;
                syn14.statModifiers = new List<StatModifier>(0)
                {
                    StatModifier.Create(PlayerStats.StatType.RangeMultiplier,StatModifier.ModifyMethod.ADDITIVE,0.5f),
                };
                syn14.bonusSynergies = new List<CustomSynergyType>();

                SynergyForms.AddSynergyForms();

                Log($"{MOD_NAME} v{VERSION} started successfully.", TEXT_COLOR);

                List<string> Quotes = new List<string>
                {
                    "*microwave noises*",
                    "System.NullReferenceException",
                    "50% of this code is from OMITB...",
                    "hmmmmmmmmmmmmmmmmmmm",
                    "Rad Cans: Now with 10% more rad!",
                    "It's not a bug, it's a feature",
                    ":trolleybus:",
                    "okay i pull up",
                    "my goodness, the flame went out!",
                    "Compound Rifle is never coming out, cry about it",
                    "So you call these things guns? Instead of...",
                    "O I L .",
                    "THAT'S THE WAR FACE!",
                    "hi, you're on a rock floating in space. pretty cool huh?",
                    "jumpscare_001.png",
                    "No memes in console, please.",
                    "Gungeon: 2077 coming soon",
                    "Trans Rights!",
                    "guys i got another null what do i do",
                    "me when i when the when you when me i",
                    "hooks are the bane of my existence",
                    "Also try Prismatism!",
                    "Also try Planetside of Gunymede!",
                    "Also try OMITB!",
                    "Also try Children Of Kaliber!",
                    "Also try Cutting Room Floor!",
                    "Also try A Bleaker Item Pack!",
                    "Also try King's Items!",
                    "Also try Knife To A Gunfight!"
                };
                System.Random randomselector = new System.Random();
                int index = randomselector.Next(Quotes.Count);
                string idkwhattocallthis = Quotes[index];
                Log(idkwhattocallthis, TEXT_COLOR);
                Gungeon.Game.Enemies.LockNamespace("gungeon");
            }

            catch (Exception e)
            {
                ETGModConsole.Log($"<color={TEXT_COLOR}>{MOD_NAME}: {e.Message}</color>");
                ETGModConsole.Log(e.StackTrace);
                Log(e.Message);
                Log("\t" + e.StackTrace);
                Log("The gears in the Gundustry seized up!", TEXT_COLOR);
                Log("Message me on Discord to report the error.", TEXT_COLOR);
            }
        }
        public static List<AGDEnemyReplacementTier> m_cachedReplacementTiers = GameManager.Instance.EnemyReplacementTiers;
        public static void OnQuickRestart(Action<GameManager, float, QuickRestartOptions> orig, GameManager self, float duration, QuickRestartOptions options)
        {
            orig(self, duration, options);
            EnemyReplace.RunReplace(m_cachedReplacementTiers);
        }
        public static void FoyerAwake(Action<Foyer> orig, Foyer self)
        {
            orig(self);
            EnemyReplace.RunReplace(m_cachedReplacementTiers);
        }
        public static void Log(string text, string color = "#FFFFFF")
        {
            ETGModConsole.Log($"<color={color}>{text}</color>");
        }

        public override void Exit() { }
        public override void Init() { }
    }
}