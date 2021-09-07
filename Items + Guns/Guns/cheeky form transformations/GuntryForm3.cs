using System;
using System.Collections;
using Gungeon;
using MonoMod;
using UnityEngine;
using ItemAPI;

namespace GundustrialRevolution
{

    public class Guntry3 : GunBehaviour
    {
        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Guntry2", "rocketgun3");
            Game.Items.Rename("outdated_gun_mods:guntry2", "ai:shushyourenotsupposedtoseethis2");
            gun.gameObject.AddComponent<Guntry3>();
            gun.SetShortDescription("Humanity's Mistake");
            gun.SetLongDescription("Shoots several stages that have different functions.\n\nSomeone had the hairbrained idea of combining the unreliability of early rocket flight with the power of a rocket launcher. Needless to say, this was not a good idea.");
            gun.SetupSprite(null, "rocketgun3_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 16);
            Gun other = PickupObjectDatabase.GetById(38) as Gun;
            gun.AddProjectileModuleFrom(other, true, false);
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 3;
            gun.DefaultModule.cooldownTime = 1.6f;
            gun.DefaultModule.numberOfShotsInClip = 2;
            gun.SetBaseMaxAmmo(32);
            gun.quality = PickupObject.ItemQuality.EXCLUDED;
            gun.encounterTrackable.EncounterGuid = "Off to Venus, I guess.";

            Projectile FuelTank2 = UnityEngine.Object.Instantiate<Projectile>((PickupObjectDatabase.GetById(56) as Gun).DefaultModule.projectiles[0]);
            FuelTank2.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(FuelTank2.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(FuelTank2);
            FuelTank2.SetProjectileSpriteRight("rocketgun_projectile_002", 6, 3, false, tk2dBaseSprite.Anchor.MiddleCenter, 6, 3);
            FuelTank2.shouldRotate = true;
            FuelTank2.baseData.damage = 32f;
            FuelTank2.baseData.speed = 16f;
            FuelTank2.baseData.range = 32f;
            ExplosiveModifier explosiveModifier = FuelTank2.gameObject.AddComponent<ExplosiveModifier>();
            explosiveModifier.doExplosion = true;
            explosiveModifier.explosionData = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
            FuelTank2.transform.parent = gun.barrelOffset;
            
            gun.DefaultModule.projectiles[0] = FuelTank2;
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.CUSTOM;
            gun.DefaultModule.customAmmoType = "rocket";
            ETGMod.Databases.Items.Add(gun, null, "ANY");
        }
        public override void OnPostFired(PlayerController player, Gun gun)
        {
            gun.PreventNormalFireAudio = true;
            AkSoundEngine.PostEvent("Play_WPN_comm4nd0_shot_01", gameObject);
        }
        private bool HasReloaded;
        protected void Update()
        {
            if (gun.CurrentOwner)
            {

                if (!gun.PreventNormalFireAudio)
                {
                    this.gun.PreventNormalFireAudio = true;
                }
                if (!gun.IsReloading && !HasReloaded)
                {
                    this.HasReloaded = true;
                }
            }
        }

        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
            if (gun.IsReloading && this.HasReloaded)
            {
                HasReloaded = false;
                AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
                base.OnReloadPressed(player, gun, bSOMETHING);
                AkSoundEngine.PostEvent("Play_WPN_comm4nd0_reload_01", base.gameObject);
            }
        }
    }
}