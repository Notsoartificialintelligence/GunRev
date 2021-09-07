using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GundustrialRevolution
{
    internal class SynergyForms
    {
        public static void AddSynergyForms()
        {
            AdvancedDualWieldSynergyProcessor advancedDualWieldSynergyProcessor = (PickupObjectDatabase.GetById(Vintage.BatteryBuddiesID) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
            advancedDualWieldSynergyProcessor.PartnerGunID = 13;
            advancedDualWieldSynergyProcessor.SynergyNameToCheck = "Battery Buddies";
            AdvancedDualWieldSynergyProcessor advancedDualWieldSynergyProcessor1 = (PickupObjectDatabase.GetById(13) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
            advancedDualWieldSynergyProcessor1.PartnerGunID = Vintage.BatteryBuddiesID;
            advancedDualWieldSynergyProcessor1.SynergyNameToCheck = "Battery Buddies";
        }
    }
}
