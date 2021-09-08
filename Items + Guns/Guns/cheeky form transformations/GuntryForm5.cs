using System;
using System.Collections;
using Gungeon;
using MonoMod;
using UnityEngine;
using ItemAPI;

namespace GunRev
{

    public class Guntry5 : GunBehaviour
    {
        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Guntry4", "rocketgun5");
            Game.Items.Rename("outdated_gun_mods:guntry4", "ai:shushyourenotsupposedtoseethis4");
            gun.gameObject.AddComponent<Guntry5>();
            gun.SetShortDescription("Humanity's Mistake");
            gun.SetLongDescription("Shoots several stages that have different functions.\n\nSomeone had the hairbrained idea of combining the unreliability of early rocket flight with the power of a rocket launcher. Needless to say, this was not a good idea.");
            gun.SetupSprite(null, "rocketgun5_idle_001", 8);
            Gun other = PickupObjectDatabase.GetById(38) as Gun;
            gun.AddProjectileModuleFrom(other, true, false);
            gun.DefaultModule.ammoCost = 99999;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 3;
            gun.DefaultModule.cooldownTime = 1.6f;
            gun.DefaultModule.numberOfShotsInClip = 0;
            gun.SetBaseMaxAmmo(0);
            gun.quality = PickupObject.ItemQuality.EXCLUDED;
            gun.encounterTrackable.EncounterGuid = "Back to Earth it is then.";
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