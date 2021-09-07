﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;

namespace GundustrialRevolution
{
    public class Graph : PassiveItem
    {
        public static void Register()
        {
            string itemName = "Graph";

            string resourceName = "GunRev/Resources/graph.png";

            GameObject obj = new GameObject(itemName);

            var item = obj.AddComponent<Graph>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            string shortDesc = "A Beautiful Line";
            string longDesc = "Improves accuracy.\n\nThese same tablets are used by Gundead scientists to predict their results. With a little modification, you can predict the accuracy of your bullets, too.";

            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ai");

            item.quality = PickupObject.ItemQuality.C;

            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Accuracy, 2, StatModifier.ModifyMethod.MULTIPLICATIVE);
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
        }

        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);

        }
    }
}