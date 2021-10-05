/*using System;
using System.Collections.Generic;
using Gungeon;
using ItemAPI;
using UnityEngine;
using AnimationType = ItemAPI.EnemyBuilder.AnimationType;
using System.Collections;
using Dungeonator;
using System.Linq;
using Brave.BulletScript;
using GungeonAPI;
using SpriteBuilder = ItemAPI.SpriteBuilder;
using static DirectionalAnimation;
using EnemyAPI;

namespace GunRev
{
	class Omegear : AIActor
	{
		public static GameObject prefab;
		public static readonly string guid = "Omegear";
		public static GameObject shootpoint;
		public static GameObject LeftHand;
		public static GameObject RightHand;

		private static tk2dSpriteCollectionData OmegearCollection;


		public static List<int> spriteIds2 = new List<int>();

		private static Texture2D BossCardTexture = ItemAPI.ResourceExtractor.GetTextureFromResource("Planetside/Resources/BossCards/fungannon_bosscard.png");

		public static void Init()
		{
			Omegear.BuildPrefab();
		}
		public static void BuildPrefab()
		{
			bool flag = prefab != null || EnemyAPI.EnemyBuilder.Dictionary.ContainsKey(guid);

			bool flag2 = flag;
			if (!flag2)
			{
				prefab = BossBuilder.BuildPrefab("Fungannon", guid, "Planetside/Resources/Fungannon/Fungannon_idle_001.png", new IntVector2(0, 0), new IntVector2(0, 0), false, true);
				var enemy = prefab.AddComponent<EnemyBehavior>();
				OmegearController pain = prefab.AddComponent<OmegearController>();

				AIAnimator aiAnimator = enemy.aiAnimator;

				enemy.aiActor.knockbackDoer.weight = 20f;
				enemy.aiActor.MovementSpeed = 1f;
				enemy.aiActor.healthHaver.PreventAllDamage = false;
				enemy.aiActor.CollisionDamage = 1f;
				enemy.aiActor.HasShadow = false;
				enemy.aiActor.IgnoreForRoomClear = false;
				enemy.aiActor.aiAnimator.HitReactChance = 0f;
				enemy.aiActor.specRigidbody.CollideWithOthers = true;
				enemy.aiActor.specRigidbody.CollideWithTileMap = true;
				enemy.aiActor.PreventFallingInPitsEver = false;
				enemy.aiActor.healthHaver.ForceSetCurrentHealth(1500f);
				enemy.aiActor.CollisionKnockbackStrength = 10f;
				enemy.aiActor.CanTargetPlayers = true;
				enemy.aiActor.healthHaver.SetHealthMaximum(1500f, null, false);

				aiAnimator.IdleAnimation = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					Prefix = "idle",
					AnimNames = new string[1],
					Flipped = new DirectionalAnimation.FlipType[1]
				};

				aiAnimator.MoveAnimation = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
					Flipped = new DirectionalAnimation.FlipType[2],
					AnimNames = new string[]
					{
						"moveright", //Good
						"moveleft",//Good


	                }
				};

				//=====================================================================================
				DirectionalAnimation ArmsTell = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
					AnimNames = new string[]
					{
						"tell_arms",

					},
					Flipped = new DirectionalAnimation.FlipType[2]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "tell_arms",
						anim = ArmsTell
					}
				};
				//=====================================================================================
				DirectionalAnimation almostdone = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					Prefix = "intro",
					AnimNames = new string[1],
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "intro",
						anim = almostdone
					}
				};
				DirectionalAnimation done = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					Prefix = "death",
					AnimNames = new string[1],
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "death",
						anim = done
					}
				};


				bool flag3 = OmegearCollection == null;
				if (flag3)
				{
					OmegearCollection = SpriteBuilder.ConstructCollection(prefab, "GearCollection");
					UnityEngine.Object.DontDestroyOnLoad(OmegearCollection);
					for (int i = 0; i < spritePaths.Length; i++)
					{
						SpriteBuilder.AddSpriteToCollection(spritePaths[i], OmegearCollection);
					}
					SpriteBuilder.AddAnimation(enemy.spriteAnimator, OmegearCollection, new List<int>
					{

					0,
					1,
					2,
					3,
					4,
					5,
					6

					}, "idle", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 6f;

					SpriteBuilder.AddAnimation(enemy.spriteAnimator, OmegearCollection, new List<int>
					{

					7,
					8,
					9,
					10,
					11,
					12,
					13,
					14,
					15

					}, "moveleft", tk2dSpriteAnimationClip.WrapMode.Once).fps = 8f;
					SpriteBuilder.AddAnimation(enemy.spriteAnimator, OmegearCollection, new List<int>
					{

					16,
					17,
					18,
					19,
					20,
					21,
					22,
					23,
					24

					}, "moveright", tk2dSpriteAnimationClip.WrapMode.Once).fps = 8f;

					SpriteBuilder.AddAnimation(enemy.spriteAnimator, OmegearCollection, new List<int>
					{

					25,
					26,
					27,
					28,
					29,
					30,
					31,
					32,
					33,
					34,
					35,//
					36,
					37,

					35,//
					36,
					37,

					35,//
					36,
					37,
					35,//
					36,
					37,
					38,
					39,
					40

					}, "roar", tk2dSpriteAnimationClip.WrapMode.Once).fps = 7f;


					SpriteBuilder.AddAnimation(enemy.spriteAnimator, OmegearCollection, new List<int>
					{

					41,
					42,
					43,
					44,
					45,
					46,
					47,
					48,
					49,
					50,
					51,
					51,
					52,
					53,

					}, "jump", tk2dSpriteAnimationClip.WrapMode.Once).fps = 11f;


					SpriteBuilder.AddAnimation(enemy.spriteAnimator, OmegearCollection, new List<int>
					{

					53,
					54,
					55

					}, "jumpland", tk2dSpriteAnimationClip.WrapMode.Once).fps = 10f;



					SpriteBuilder.AddAnimation(enemy.spriteAnimator, OmegearCollection, new List<int>
					{

					0,
					1,
					2,
					3,
					4,
					5,
					6,
					0,
					1,
					2,
					3,
					4,
					5,
					6,
					0,
					1,
					2,
					3,
					4,
					5,
					6,
					25,
					26,
					27,
					28,
					29,
					30,
					31,
					32,
					33,
					34,
					35,//
					36,
					37,


					}, "intro", tk2dSpriteAnimationClip.WrapMode.Once).fps = 10f;
					SpriteBuilder.AddAnimation(enemy.spriteAnimator, OmegearCollection, new List<int>
					{
				66,
				67,
				68,
				69,
				70,
				71,
				72,
				73,
				74,
				75,
				76,
				77,
				78,
				76,
				77,
				78,
				76,
				77,
				78,
				76,
				77,
				78,
				76,
				77,
				78,
				76,
				77,
				78
					}, "death", tk2dSpriteAnimationClip.WrapMode.Once).fps = 10f;

					SpriteBuilder.AddAnimation(enemy.spriteAnimator, OmegearCollection, new List<int>
					{
					25,
					26,
					27,
					28,
					27,
					26,
					27,
					28,
					29,

					27,
					26,
					27,
					28,
					29,

					27,
					26,
					27,
					28,
					29,

					27,
					26,
					27,
					28,
					29,

					27,
					26,
					27,
					28,
					29,

					27,
					26,
					27,
					28,
					29,

					27,
					26,
					27,
					28,
					29,

					27,
					26,
					27,
					28,

					27,
					26,
					27,
					28,
					29,

					27,
					26,
					27,
					28,
					29,

					27,
					26,
					27,
					28,
					29,

					27,
					26,
					27,
					28,
					29,

					27,
					26,
					27,
					28,
					29,

					27,
					26,
					27,
					28,
					29,

					27,
					26,
					27,
					28,
					29,

					27,
					26,
					25
					}, "charge", tk2dSpriteAnimationClip.WrapMode.Once).fps = 8f;
				}

				enemy.aiActor.PreventBlackPhantom = false;

				shootpoint = new GameObject("CentreOfAllCannons");
				shootpoint.transform.parent = enemy.transform;
				shootpoint.transform.position = new Vector2(2.875f, 2.875f);

				var bs = prefab.GetComponent<BehaviorSpeculator>();
				BehaviorSpeculator behaviorSpeculator = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").behaviorSpeculator;

				bs.OverrideBehaviors = behaviorSpeculator.OverrideBehaviors;
				bs.OtherBehaviors = behaviorSpeculator.OtherBehaviors;
				bs.TargetBehaviors = new List<TargetBehaviorBase>
				{
					new TargetPlayerBehavior
					{
						Radius = 45f,
						LineOfSight = true,
						ObjectPermanence = true,
						SearchInterval = 0.25f,
						PauseOnTargetSwitch = false,
						PauseTime = 0.25f
					},

				};

				bs.AttackBehaviorGroup.AttackBehaviors = new List<AttackBehaviorGroup.AttackGroupItem>()
				{

					new AttackBehaviorGroup.AttackGroupItem()
						{
						Probability = 1.1f,
						Behavior = new ShootBehavior()
						{

							BulletScript = new CustomBulletScriptSelector(typeof(PrimaryCannonScript)),
							LeadAmount = 0,

							AttackCooldown = 1.5f,
							Cooldown = 6f,

							RequiresLineOfSight = true,
							ShootPoint = shootpoint,
							CooldownVariance = 0f,
							GlobalCooldown = 0,
							InitialCooldown = 1,
							InitialCooldownVariance = 0,
							GroupName = null,
							MinRange = 0,
							Range = 1000,
							MinWallDistance = 0,
							MaxEnemiesInRoom = -1,
							MinHealthThreshold = 0,
							MaxHealthThreshold = 1,
							HealthThresholds = new float[0],
							AccumulateHealthThresholds = true,
							targetAreaStyle = null,
							IsBlackPhantom = false,
							resetCooldownOnDamage = null,
							MaxUsages = 0,
							ImmobileDuringStop = false,
							ChargeAnimation = "jump",
							FireAnimation = "jumpland",
							StopDuring = ShootBehavior.StopType.Charge

						},
						NickName = "ffefefefe"
					},
					new AttackBehaviorGroup.AttackGroupItem()
						{
						Probability = 1f,
						Behavior = new ShootBehavior()
						{

							BulletScript = new CustomBulletScriptSelector(typeof()),
							LeadAmount = 0,

							AttackCooldown = 0.25f,
							Cooldown = 0.5f,

							RequiresLineOfSight = true,
							ShootPoint = shootpoint,
							CooldownVariance = 0f,
							GlobalCooldown = 0,
							InitialCooldown = 0,
							InitialCooldownVariance = 0,
							GroupName = null,
							MinRange = 0,
							Range = 1000,
							MinWallDistance = 0,
							MaxEnemiesInRoom = -1,
							MinHealthThreshold = 0,
							MaxHealthThreshold = 1,
							HealthThresholds = new float[0],
							AccumulateHealthThresholds = true,
							targetAreaStyle = null,
							IsBlackPhantom = false,
							resetCooldownOnDamage = null,
							MaxUsages = 0,

						},
						NickName = "dasddsasad"
					},
					new AttackBehaviorGroup.AttackGroupItem()
						{
						Probability = 1f,
						Behavior = new ShootBehavior()
						{
							StopDuring = ShootBehavior.StopType.Attack,
							BulletScript = new CustomBulletScriptSelector(typeof(RainingPoot)),
							LeadAmount = 0,

							AttackCooldown = 0f,
							Cooldown = 5f,

							RequiresLineOfSight = true,
							ShootPoint = shootpoint,
							CooldownVariance = 0f,
							GlobalCooldown = 0,
							InitialCooldown = 1,
							InitialCooldownVariance = 0,
							GroupName = null,
							MinRange = 0,
							Range = 1000,
							MinWallDistance = 0,
							MaxEnemiesInRoom = -1,
							MinHealthThreshold = 0,
							MaxHealthThreshold = 1,
							HealthThresholds = new float[0],
							AccumulateHealthThresholds = true,
							targetAreaStyle = null,
							IsBlackPhantom = false,
							resetCooldownOnDamage = null,
							MaxUsages = 0,
							FireAnimation = "roar",

						},
						NickName = "raor"

					},
					new AttackBehaviorGroup.AttackGroupItem()
						{
						Probability = 0.85f,
						Behavior = new ShootBehavior()
						{
							StopDuring = ShootBehavior.StopType.Attack,
							BulletScript = new CustomBulletScriptSelector(typeof(ShotgunExecutionerChain1)),
							LeadAmount = 0,

							AttackCooldown = 3f,
							Cooldown = 20f,

							RequiresLineOfSight = true,
							ShootPoint = shootpoint,
							CooldownVariance = 0f,
							GlobalCooldown = 0,
							InitialCooldown = 8,
							InitialCooldownVariance = 0,
							GroupName = null,
							MinRange = 0,
							Range = 1000,
							MinWallDistance = 0,
							MaxEnemiesInRoom = -1,
							MinHealthThreshold = 0,
							MaxHealthThreshold = 1,
							HealthThresholds = new float[0],
							AccumulateHealthThresholds = true,
							targetAreaStyle = null,
							IsBlackPhantom = false,
							resetCooldownOnDamage = null,
							MaxUsages = 0,
							FireAnimation = "charge",

						},
						NickName = "pissandshit"

					},
					new AttackBehaviorGroup.AttackGroupItem()
						{
						Probability = 1f,
						Behavior = new ShootBehavior()
						{

							BulletScript = new CustomBulletScriptSelector(typeof()),
							LeadAmount = 0,

							AttackCooldown = 0.5f,
							Cooldown = 0.1f,

							RequiresLineOfSight = true,
							ShootPoint = shootpoint,
							CooldownVariance = 0f,
							GlobalCooldown = 0,
							InitialCooldown = 2,
							InitialCooldownVariance = 0,
							GroupName = null,
							MinRange = 0,
							Range = 1000,
							MinWallDistance = 0,
							MaxEnemiesInRoom = -1,
							MinHealthThreshold = 0,
							MaxHealthThreshold = 1,
							HealthThresholds = new float[0],
							AccumulateHealthThresholds = true,
							targetAreaStyle = null,
							IsBlackPhantom = false,
							resetCooldownOnDamage = null,
							MaxUsages = 0,

						},
						NickName = "fastFire"
					},
				};
				bs.MovementBehaviors = new List<MovementBehaviorBase>() {
				new SeekTargetBehavior() {
					StopWhenInRange = false,
					CustomRange = 6,
					LineOfSight = true,
					ReturnToSpawn = true,
					SpawnTetherDistance = 0,
					PathInterval = 0.5f,
					SpecifyRange = false,
					MinActiveRange = -0.25f,
					MaxActiveRange = 0
				}
				};
				bs.InstantFirstTick = behaviorSpeculator.InstantFirstTick;
				bs.TickInterval = behaviorSpeculator.TickInterval;
				bs.PostAwakenDelay = behaviorSpeculator.PostAwakenDelay;
				bs.RemoveDelayOnReinforce = behaviorSpeculator.RemoveDelayOnReinforce;
				bs.OverrideStartingFacingDirection = behaviorSpeculator.OverrideStartingFacingDirection;
				bs.StartingFacingDirection = behaviorSpeculator.StartingFacingDirection;
				bs.SkipTimingDifferentiator = behaviorSpeculator.SkipTimingDifferentiator;

				Game.Enemies.Add("ai:omegear", enemy.aiActor);
				if (enemy.GetComponent<EncounterTrackable>() != null)
				{
					UnityEngine.Object.Destroy(enemy.GetComponent<EncounterTrackable>());
				}
				GenericIntroDoer miniBossIntroDoer = prefab.AddComponent<GenericIntroDoer>();
				prefab.AddComponent<OmegearIntroController>();

				miniBossIntroDoer.triggerType = GenericIntroDoer.TriggerType.PlayerEnteredRoom;
				miniBossIntroDoer.initialDelay = 0.15f;
				miniBossIntroDoer.cameraMoveSpeed = 14;
				miniBossIntroDoer.specifyIntroAiAnimator = null;
				miniBossIntroDoer.BossMusicEvent = "Play_MUS_Boss_Theme_Beholster";
				//miniBossIntroDoer.BossMusicEvent = "Play_MUS_Lich_Double_01";
				miniBossIntroDoer.PreventBossMusic = false;
				miniBossIntroDoer.InvisibleBeforeIntroAnim = false;
				miniBossIntroDoer.preIntroAnim = string.Empty;
				miniBossIntroDoer.preIntroDirectionalAnim = string.Empty;
				miniBossIntroDoer.introAnim = "intro";
				miniBossIntroDoer.introDirectionalAnim = string.Empty;
				miniBossIntroDoer.continueAnimDuringOutro = false;
				miniBossIntroDoer.cameraFocus = null;
				miniBossIntroDoer.roomPositionCameraFocus = Vector2.zero;
				miniBossIntroDoer.restrictPlayerMotionToRoom = false;
				miniBossIntroDoer.fusebombLock = false;
				miniBossIntroDoer.AdditionalHeightOffset = 0;
				GunRev.Module.Strings.Enemies.Set("#OMEGEAR_NAME", "OMEGEAR");
				GunRev.Module.Strings.Enemies.Set("#OMEGEAR_NAME_SMALL", "Omegear");

				GunRev.Module.Strings.Enemies.Set("BRASS_KICKER", "BRASS KICKER");
				GunRev.Module.Strings.Enemies.Set("#QUOTE", "");
				enemy.aiActor.OverrideDisplayName = "#OMEGEAR_NAME_SMALL";

				miniBossIntroDoer.portraitSlideSettings = new PortraitSlideSettings()
				{
					bossNameString = "#OMEGEAR_NAME",
					bossSubtitleString = "BRASS_KICKER",
					bossQuoteString = "#QUOTE",
					bossSpritePxOffset = IntVector2.Zero,
					topLeftTextPxOffset = IntVector2.Zero,
					bottomRightTextPxOffset = IntVector2.Zero,
					bgColor = Color.blue
				};
				if (BossCardTexture)
				{
					miniBossIntroDoer.portraitSlideSettings.bossArtSprite = BossCardTexture;
					miniBossIntroDoer.SkipBossCard = false;
					enemy.aiActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.MainBar;
				}
				else
				{
					miniBossIntroDoer.SkipBossCard = true;
					enemy.aiActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.MainBar;
				}

				SpriteBuilder.AddSpriteToCollection("Planetside/Resources/Ammocom/ammonimiconasdsadsa", SpriteBuilder.ammonomiconCollection);
				if (enemy.GetComponent<EncounterTrackable>() != null)
				{
					UnityEngine.Object.Destroy(enemy.GetComponent<EncounterTrackable>());
				}
				enemy.encounterTrackable = enemy.gameObject.AddComponent<EncounterTrackable>();
				enemy.encounterTrackable.journalData = new JournalEntry();
				enemy.encounterTrackable.EncounterGuid = "ai:omegear";
				enemy.encounterTrackable.prerequisites = new DungeonPrerequisite[0];
				enemy.encounterTrackable.journalData.SuppressKnownState = false;
				enemy.encounterTrackable.journalData.IsEnemy = true;
				enemy.encounterTrackable.journalData.SuppressInAmmonomicon = false;
				enemy.encounterTrackable.ProxyEncounterGuid = "";
				enemy.encounterTrackable.journalData.AmmonomiconSprite = "GunRev/Resources/omegear/omegearicon";
				enemy.encounterTrackable.journalData.enemyPortraitSprite = ItemAPI.ResourceExtractor.GetTextureFromResource("Planetside\\Resources\\Ammocom\\ammoentryshrrom.png");
				GunRev.Module.Strings.Enemies.Set("#OMEGEARAMMONOMICON", "Omegear");
				GunRev.Module.Strings.Enemies.Set("#OMEGEARAMMONOMICONSHORT", "Brass Kicker");
				GunRev.Module.Strings.Enemies.Set("#OMEGEARAMMONOMICONLONG", "A giant automaton, created by a cowardly group of Gundead many years ago. While it takes the resemblance of a gear, there is no machine large enough in the Gungeon it could fit into (or has since ceased to exist), and is speculated to be purely an aesthetic choice.");
				enemy.encounterTrackable.journalData.PrimaryDisplayName = "#OMEGEARAMMONOMICON";
				enemy.encounterTrackable.journalData.NotificationPanelDescription = "#OMEGEARAMMONOMICONSHORT";
				enemy.encounterTrackable.journalData.AmmonomiconFullEntry = "#OMEGEARAMMONOMICONLONG";
				EnemyAPI.EnemyBuilder.AddEnemyToDatabase(enemy.gameObject, "ai:omegear");
				EnemyDatabase.GetEntry("ai:omegear").ForcedPositionInAmmonomicon = 4;
				EnemyDatabase.GetEntry("ai:omegear").isInBossTab = true;
				EnemyDatabase.GetEntry("ai:omegear").isNormalEnemy = true;

				miniBossIntroDoer.SkipFinalizeAnimation = true;
				miniBossIntroDoer.RegenerateCache();

				//==================
				//Important for not breaking basegame stuff!
				StaticReferenceManager.AllHealthHavers.Remove(enemy.aiActor.healthHaver);
				//==================

			}
		}


		private static string[] spritePaths = new string[]
		{
			//Idle
			"Planetside/Resources/Fungannon/Fungannon_idle_001.png",
			"Planetside/Resources/Fungannon/Fungannon_idle_002.png",
			"Planetside/Resources/Fungannon/Fungannon_idle_003.png",
			"Planetside/Resources/Fungannon/Fungannon_idle_004.png",
			"Planetside/Resources/Fungannon/Fungannon_idle_005.png",
			"Planetside/Resources/Fungannon/Fungannon_idle_006.png",
			"Planetside/Resources/Fungannon/Fungannon_idle_007.png",
			//MoveLeft
			"Planetside/Resources/Fungannon/Fungannon_move_001.png",
			"Planetside/Resources/Fungannon/Fungannon_move_002.png",
			"Planetside/Resources/Fungannon/Fungannon_move_003.png",
			"Planetside/Resources/Fungannon/Fungannon_move_004.png",
			"Planetside/Resources/Fungannon/Fungannon_move_005.png",
			"Planetside/Resources/Fungannon/Fungannon_move_006.png",
			"Planetside/Resources/Fungannon/Fungannon_move_007.png",
			"Planetside/Resources/Fungannon/Fungannon_move_008.png",
			"Planetside/Resources/Fungannon/Fungannon_move_009.png",
			//MoveRight
			"Planetside/Resources/Fungannon/Fungannon_moveright_001.png",
			"Planetside/Resources/Fungannon/Fungannon_moveright_002.png",
			"Planetside/Resources/Fungannon/Fungannon_moveright_003.png",
			"Planetside/Resources/Fungannon/Fungannon_moveright_004.png",
			"Planetside/Resources/Fungannon/Fungannon_moveright_005.png",
			"Planetside/Resources/Fungannon/Fungannon_moveright_006.png",
			"Planetside/Resources/Fungannon/Fungannon_moveright_007.png",
			"Planetside/Resources/Fungannon/Fungannon_moveright_008.png",
			"Planetside/Resources/Fungannon/Fungannon_moveright_009.png",
			//Roar
			"Planetside/Resources/Fungannon/Fungannon_roar_001.png",
			"Planetside/Resources/Fungannon/Fungannon_roar_002.png",
			"Planetside/Resources/Fungannon/Fungannon_roar_003.png",
			"Planetside/Resources/Fungannon/Fungannon_roar_004.png",
			"Planetside/Resources/Fungannon/Fungannon_roar_005.png",
			"Planetside/Resources/Fungannon/Fungannon_roar_006.png",
			"Planetside/Resources/Fungannon/Fungannon_roar_007.png",
			"Planetside/Resources/Fungannon/Fungannon_roar_008.png",
			"Planetside/Resources/Fungannon/Fungannon_roar_009.png",
			"Planetside/Resources/Fungannon/Fungannon_roar_010.png",
			"Planetside/Resources/Fungannon/Fungannon_roar_011.png",
			"Planetside/Resources/Fungannon/Fungannon_roar_012.png",
			"Planetside/Resources/Fungannon/Fungannon_roar_013.png",
			"Planetside/Resources/Fungannon/Fungannon_roar_014.png",
			"Planetside/Resources/Fungannon/Fungannon_roar_015.png",
			"Planetside/Resources/Fungannon/Fungannon_roar_016.png",
			//Jump
			"Planetside/Resources/Fungannon/Fungannon_jump_001.png",
			"Planetside/Resources/Fungannon/Fungannon_jump_002.png",
			"Planetside/Resources/Fungannon/Fungannon_jump_003.png",
			"Planetside/Resources/Fungannon/Fungannon_jump_004.png",
			"Planetside/Resources/Fungannon/Fungannon_jump_005.png",
			"Planetside/Resources/Fungannon/Fungannon_jump_006.png",
			"Planetside/Resources/Fungannon/Fungannon_jump_007.png",
			"Planetside/Resources/Fungannon/Fungannon_jump_008.png",
			"Planetside/Resources/Fungannon/Fungannon_jump_009.png",
			"Planetside/Resources/Fungannon/Fungannon_jump_010.png",
			"Planetside/Resources/Fungannon/Fungannon_jump_011.png",
			"Planetside/Resources/Fungannon/Fungannon_jump_012.png",
			//JumpLand
			"Planetside/Resources/Fungannon/Fungannon_jumpland_001.png",
			"Planetside/Resources/Fungannon/Fungannon_jumpland_002.png",
			"Planetside/Resources/Fungannon/Fungannon_jumpland_003.png",
			//Intro
			"Planetside/Resources/Fungannon/Fungannon_spawn_001.png",
			"Planetside/Resources/Fungannon/Fungannon_spawn_002.png",
			"Planetside/Resources/Fungannon/Fungannon_spawn_003.png",
			"Planetside/Resources/Fungannon/Fungannon_spawn_004.png",
			"Planetside/Resources/Fungannon/Fungannon_spawn_005.png",
			"Planetside/Resources/Fungannon/Fungannon_spawn_006.png",
			"Planetside/Resources/Fungannon/Fungannon_spawn_007.png",
			"Planetside/Resources/Fungannon/Fungannon_spawn_008.png",
			"Planetside/Resources/Fungannon/Fungannon_spawn_009.png",
			"Planetside/Resources/Fungannon/Fungannon_spawn_010.png",
			//Death
			"Planetside/Resources/Fungannon/Fungannon_death_001.png",
			"Planetside/Resources/Fungannon/Fungannon_death_002.png",
			"Planetside/Resources/Fungannon/Fungannon_death_003.png",
			"Planetside/Resources/Fungannon/Fungannon_death_004.png",
			"Planetside/Resources/Fungannon/Fungannon_death_005.png",
			"Planetside/Resources/Fungannon/Fungannon_death_006.png",
			"Planetside/Resources/Fungannon/Fungannon_death_007.png",
			"Planetside/Resources/Fungannon/Fungannon_death_008.png",
			"Planetside/Resources/Fungannon/Fungannon_death_009.png",
			"Planetside/Resources/Fungannon/Fungannon_death_010.png",
			"Planetside/Resources/Fungannon/Fungannon_death_011.png",
			"Planetside/Resources/Fungannon/Fungannon_death_012.png",
			"Planetside/Resources/Fungannon/Fungannon_death_013.png",

		};
		public class EnemyBehavior : BraveBehaviour
		{
			private RoomHandler m_StartRoom;
			public void Update()
			{
				m_StartRoom = aiActor.GetAbsoluteParentRoom();
				if (!base.aiActor.HasBeenEngaged)
				{
					CheckPlayerRoom();
				}
			}
			private void CheckPlayerRoom()
			{
				if (GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != null && GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() == m_StartRoom && GameManager.Instance.PrimaryPlayer.IsInCombat == true)
				{

					GameManager.Instance.StartCoroutine(LateEngage());
				}
				else
				{
					base.aiActor.HasBeenEngaged = false;
				}
			}
			private IEnumerator LateEngage()
			{
				yield return new WaitForSeconds(0.5f);
				if (GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != null && GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() == m_StartRoom)
				{
					base.aiActor.HasBeenEngaged = true;
				}
				yield break;
			}

			public void Start()
			{
				this.aiActor.knockbackDoer.SetImmobile(false, "omegear");
				base.aiActor.bulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("f905765488874846b7ff257ff81d6d0c").bulletBank.GetBullet("spore2"));
				base.aiActor.bulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("f905765488874846b7ff257ff81d6d0c").bulletBank.GetBullet("spore1"));
				base.aiActor.bulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("383175a55879441d90933b5c4e60cf6f").bulletBank.GetBullet("bigBullet"));
				base.aiActor.bulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("41ee1c8538e8474a82a74c4aff99c712").bulletBank.GetBullet("big"));
				base.aiActor.HasBeenEngaged = false;
				//Important for not breaking basegame stuff!
				StaticReferenceManager.AllHealthHavers.Remove(base.aiActor.healthHaver);
			}

		}
		private static List<GameObject> m_reticles = new List<GameObject>();
		public class Cannonball : Bullet
		{
			public Cannonball() : base("bigBullet", false, false, false)
			{

			}

			protected override IEnumerator Top()
			{
				base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("f905765488874846b7ff257ff81d6d0c").bulletBank.GetBullet("spore2"));
				base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("f905765488874846b7ff257ff81d6d0c").bulletBank.GetBullet("spore1"));
				for (int i = 0; i < 600; i++)
				{
					string bankName = (UnityEngine.Random.value > 0.33f) ? "spore2" : "spore1";
					base.Fire(new Direction(UnityEngine.Random.Range(150, 210), Brave.BulletScript.DirectionType.Relative, -1f), new Speed(1f, SpeedType.Absolute), new PrimaryCannonScript.Spore(bankName, UnityEngine.Random.Range(15, 40)));
					yield return this.Wait(1f);

				}
				yield break;
			}
		}

		public class SporeSmall : Bullet
		{
			public SporeSmall(string bulletname, float Airtime) : base(bulletname, false, false, false)
			{
				this.BulletName = bulletname;
				this.AirTime = Airtime;
			}

			protected override IEnumerator Top()
			{
				if (this.BulletName == "spore2")
				{
					base.ChangeSpeed(new Speed(0, SpeedType.Absolute), 180);
				}
				else
				{
					base.ChangeSpeed(new Speed(1, SpeedType.Absolute), 120);
				}
				yield return this.Wait(AirTime);
				base.Vanish(false);
				yield break;
			}
			public string BulletName;
			public float AirTime;
		}
		public class Superball : Bullet
		{
			public Superball() : base("big", false, false, false)
			{
			}
			protected override IEnumerator Top()
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("f905765488874846b7ff257ff81d6d0c").bulletBank.GetBullet("spore2"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("f905765488874846b7ff257ff81d6d0c").bulletBank.GetBullet("spore1"));
				}
				for (int i = 0; i < 90; i++)
				{
					float Speed = base.Speed;
					base.ChangeSpeed(new Speed(Speed * 0.98f, SpeedType.Absolute), 0);

					float aim = this.GetAimDirection(5f, UnityEngine.Random.Range(6f, 6f));
					float delta = BraveMathCollege.ClampAngle180(aim - this.Direction);
					if (Mathf.Abs(delta) > 100f)
					{
						yield break;
					}
					this.Direction += Mathf.MoveTowards(0f, delta, 3f);
					yield return this.Wait(1);
				}

				base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 1);
				base.Vanish(true);
				yield break;
			}
			public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
			{
				if (!preventSpawningProjectiles)
				{
					base.PostWwiseEvent("Play_BOSS_Rat_Cheese_Burst_02", null);
					for (int i = 0; i < 14; i++)
					{
						string bankName = (UnityEngine.Random.value > 0.33f) ? "spore2" : "spore1";
						float speed = 1f;
						if (bankName == "spore2")
						{
							speed *= UnityEngine.Random.Range(1.5f, 2f);
						}
						else
						{
							speed *= UnityEngine.Random.Range(0.6f, 1.4f);

						}
						base.Fire(new Direction(UnityEngine.Random.Range(-180, 180), Brave.BulletScript.DirectionType.Absolute, -1f), new Speed(6f * speed, SpeedType.Absolute), new PrimaryCannonScript.Spore(bankName, 180));
					}
					return;
				}
			}
		}

		public class Spore : Bullet
		{
			public Spore(string bulletname) : base(bulletname, false, false, false)
			{
				this.BulletName = bulletname;
			}

			protected override IEnumerator Top()
			{
				if (this.BulletName == "spore2")
				{
					base.ChangeSpeed(new Speed(22, SpeedType.Absolute), 60);
				}
				else
				{
					base.ChangeSpeed(new Speed(18, SpeedType.Absolute), 60);
				}
				yield break;
			}
			public string BulletName;
		}
	}

	public class RainingPoot : Script
	{

		protected override IEnumerator Top()
		{

			CellArea area = base.BulletBank.aiActor.ParentRoom.area;
			AIActor aiActor = base.BulletBank.aiActor;
			aiActor.aiAnimator.PlayUntilFinished("roar", false, null, -1f, false);
			Vector2 roomLowerLeft = area.UnitBottomLeft;
			Vector2 roomUpperRight = area.UnitTopRight - new Vector2(0f, 3.125f);
			Vector2 roomCenter = area.UnitCenter - new Vector2(0f, 2.25f);
			for (int i = 0; i < 3; i++)
			{
				int fired;
				fired = UnityEngine.Random.Range(11, 18);
				for (int j = 0; j < fired; j++)
				{
					Vector2 vector = new Vector2(roomLowerLeft.x, base.SubdivideRange(roomLowerLeft.y, roomUpperRight.y, fired + 1, j, true));
					vector += new Vector2(UnityEngine.Random.Range(-0.25f, 0.25f), UnityEngine.Random.Range(-1, 1));
					vector.x -= 2.5f;
					this.FireWallBullet(0f, vector, roomCenter);
				}
				fired = UnityEngine.Random.Range(11, 18);
				for (int k = 0; k < fired; k++)
				{
					Vector2 vector2 = new Vector2(base.SubdivideRange(roomLowerLeft.x, roomUpperRight.x, fired + 1, k, true), roomUpperRight.y);
					vector2 += new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-0.25f, 0.25f));
					vector2.y += 6.5f;
					this.FireWallBullet(-90f, vector2, roomCenter);
				}
				fired = UnityEngine.Random.Range(11, 18);
				for (int l = 0; l < fired; l++)
				{
					Vector2 vector3 = new Vector2(roomUpperRight.x, base.SubdivideRange(roomLowerLeft.y, roomUpperRight.y, fired + 1, l, true));
					vector3 += new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-0.25f, 0.25f));
					vector3.x += 2.5f;
					this.FireWallBullet(180f, vector3, roomCenter);
				}
				fired = UnityEngine.Random.Range(11, 18);
				for (int m = 0; m < fired; m++)
				{
					Vector2 vector4 = new Vector2(base.SubdivideRange(roomLowerLeft.x, roomUpperRight.x, fired + 1, m, true), roomLowerLeft.y);
					vector4 += new Vector2(UnityEngine.Random.Range(-0.25f, 0.25f), UnityEngine.Random.Range(-1f, 1f));
					vector4.y -= 2.5f;
					this.FireWallBullet(90f, vector4, roomCenter);
				}
				yield return base.Wait(36);
			}
			yield return base.Wait(240);


			yield break;
		}
		private void FireWallBullet(float facingDir, Vector2 spawnPos, Vector2 roomCenter)
		{
			float angleDeg = (spawnPos - roomCenter).ToAngle();
			int num = Mathf.RoundToInt(BraveMathCollege.ClampAngle360(angleDeg) / 45f) % 8;
			float num2 = (float)num * 45f;
			base.Fire(Offset.OverridePosition(spawnPos), new Direction(facingDir, Brave.BulletScript.DirectionType.Absolute, -1f), new Speed(0f, SpeedType.Absolute), new RainingPoot.PAin(this, (UnityEngine.Random.value > 0.33f) ? "spore2" : "spore1"));
		}

		public class PAin : Bullet
		{
			public PAin(RainingPoot parent, string bulletName) : base(bulletName, true, false, false)
			{
				this.m_parent = parent;
			}
			protected override IEnumerator Top()
			{
				int travelTime = UnityEngine.Random.Range(120, 450);
				this.Projectile.IgnoreTileCollisionsFor(90f);
				this.Projectile.specRigidbody.AddCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.HighObstacle, CollisionLayer.LowObstacle));
				this.Projectile.sprite.ForceRotationRebuild();
				this.Projectile.sprite.UpdateZDepth();
				int r = UnityEngine.Random.Range(0, 20);

				Vector2 area = base.BulletBank.aiActor.sprite.WorldCenter;
				this.Direction = (area - base.Position).ToAngle();

				base.ChangeSpeed(new Speed(10f, SpeedType.Absolute), UnityEngine.Random.Range(150, 600));
				yield return base.Wait(travelTime);
				base.Vanish(false);
				yield break;
			}
			private RainingPoot m_parent;
		}
	}


	public class Cannonball : Bullet
	{
		public Cannonball() : base("bigBullet", false, false, false)
		{

		}

		protected override IEnumerator Top()
		{
			base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("f905765488874846b7ff257ff81d6d0c").bulletBank.GetBullet("spore2"));
			base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("f905765488874846b7ff257ff81d6d0c").bulletBank.GetBullet("spore1"));
			for (int i = 0; i < 600; i++)
			{
				string bankName = (UnityEngine.Random.value > 0.33f) ? "spore2" : "spore1";
				base.Fire(new Direction(UnityEngine.Random.Range(150, 210), Brave.BulletScript.DirectionType.Relative, -1f), new Speed(4, SpeedType.Absolute), new PrimaryCannonScript.Spore(bankName, UnityEngine.Random.Range(15, 90)));
				yield return this.Wait(2f);

			}
			yield break;
		}
	}
	public class Spore : Bullet
	{
		public Spore(string bulletname, float Airtime) : base(bulletname, false, false, false)
		{
			this.BulletName = bulletname;
			this.AirTime = Airtime;
		}

		protected override IEnumerator Top()
		{
			if (this.BulletName == "spore2")
			{
				base.ChangeSpeed(new Speed(0, SpeedType.Absolute), 180);
			}
			else
			{
				base.ChangeSpeed(new Speed(1, SpeedType.Absolute), 120);
			}
			yield return this.Wait(AirTime);
			base.Vanish(false);
			yield break;
		}
		public string BulletName;
		public float AirTime;
	}
}
*/