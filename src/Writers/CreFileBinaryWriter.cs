﻿using ii.InfinityEngine.Binary;
using ii.InfinityEngine.Files;
using ii.InfinityEngine.Writers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace ii.InfinityEngine.Writers
{
    public class CreFileBinaryWriter : ICreFileWriter
    {
        const int HeaderSize = 724;
        const int Eff1Size = 48;
        const int Eff2Size = 272 - 8;
        const int ItemSize = 20;
        const int ItemSlotSize = 40 * 2;
        const int KnownSpellSize = 12;
        const int memorisedspellinfoSize = 16;
        const int memorisedspellSize = 12;

        public TlkFile TlkFile { get; set; }
        public BackupManager BackupManger { get; set; }

        internal byte[] Write(CreFile creFile)
        {
            List<CreKnownSpellBinary> creKnownSpells = [];
            List<CreSpellMemorisationInfoBinary> creSpellMemorisationInfo = [];
            List<CreMemorisedSpellBinary> creMemorisedSpells = [];
            List<Eff1BinaryBinary> creEffects1 = [];
            List<EmbeddedEffBinary> creEffects2 = [];
            List<CreItemBinary> creItems = [];
            List<short> creItemSlots = [];

            foreach (var featureBlock in creFile.Effects1)
            {
                var featureBlockBinary = new Eff1BinaryBinary();
                featureBlockBinary.DiceSides = featureBlock.DiceSides;
                featureBlockBinary.DiceThrown = featureBlock.DiceThrown;
                featureBlockBinary.Resistance = featureBlock.Resistance;
                featureBlockBinary.Duration = featureBlock.Duration;
                featureBlockBinary.Opcode = featureBlock.Opcode;
                featureBlockBinary.Parameter1 = featureBlock.Parameter1;
                featureBlockBinary.Parameter2 = featureBlock.Parameter2;
                featureBlockBinary.Power = featureBlock.Power;
                featureBlockBinary.Probability1 = featureBlock.Probability1;
                featureBlockBinary.Probability2 = featureBlock.Probability2;
                featureBlockBinary.Resource = featureBlock.Resource;
                featureBlockBinary.SavingThrowBonus = featureBlock.SavingThrowBonus;
                featureBlockBinary.SavingThrowType = featureBlock.SavingThrowType;
                featureBlockBinary.TargetType = featureBlock.TargetType;
                featureBlockBinary.TimingMode = (byte)featureBlock.TimingMode;
                featureBlockBinary.Unknown = featureBlock.Unknown;
                creEffects1.Add(featureBlockBinary);
            }

            foreach (var featureBlock in creFile.Effects2)
            {
                var featureBlockBinary = new EmbeddedEffBinary();
                featureBlockBinary.CasterLevel = featureBlock.CasterLevel;
                featureBlockBinary.CasterXCoordinate = featureBlock.CasterXCoordinate;
                featureBlockBinary.CasterYCoordinate = featureBlock.CasterYCoordinate;
                featureBlockBinary.DiceSides = featureBlock.DiceSides;
                featureBlockBinary.DiceThrown = featureBlock.DiceThrown;
                featureBlockBinary.Duration = featureBlock.Duration;
                featureBlockBinary.HighestAffectedLevelFromParent = featureBlock.HighestAffectedLevelFromParent;
                featureBlockBinary.LowestAffectedLevelFromParent = featureBlock.LowestAffectedLevelFromParent;
                featureBlockBinary.Opcode = featureBlock.Opcode;
                featureBlockBinary.Parameter1 = featureBlock.Parameter1;
                featureBlockBinary.Parameter2 = featureBlock.Parameter2;
                featureBlockBinary.Parameter3 = featureBlock.Parameter3;
                featureBlockBinary.Parameter4 = featureBlock.Parameter4;
                featureBlockBinary.ParentResource = featureBlock.ParentResource;
                featureBlockBinary.ParentResourceSlot = featureBlock.ParentResourceSlot;
                featureBlockBinary.Power = featureBlock.Power;
                featureBlockBinary.PrimaryType = featureBlock.PrimaryType;
                featureBlockBinary.Probability1 = featureBlock.Probability1;
                featureBlockBinary.Probability2 = featureBlock.Probability2;
                featureBlockBinary.Projectile = featureBlock.Projectile;
                featureBlockBinary.Resistance = featureBlock.Resistance;
                featureBlockBinary.Resource = featureBlock.Resource;
                featureBlockBinary.Resource2 = featureBlock.Resource2;
                featureBlockBinary.Resource3 = featureBlock.Resource3;
                featureBlockBinary.ResourceTypeFromParent = featureBlock.ResourceTypeFromParent;
                featureBlockBinary.SavingThrowBonus = featureBlock.SavingThrowBonus;
                featureBlockBinary.SavingThrowType = featureBlock.SavingThrowType;
                featureBlockBinary.SecondaryType = featureBlock.SecondaryType;
                featureBlockBinary.SetLocalVariableIfNonExistant = featureBlock.SetLocalVariableIfNonExistant;
                featureBlockBinary.TargetType = featureBlock.TargetType;
                featureBlockBinary.TargetXCoordinate = featureBlock.TargetXCoordinate;
                featureBlockBinary.TargetYCoordinate = featureBlock.TargetYCoordinate;
                featureBlockBinary.TimingMode = featureBlock.TimingMode;
                featureBlockBinary.Unknown = featureBlock.Unknown;
                featureBlockBinary.Unknown2 = featureBlock.Unknown2;
                featureBlockBinary.Unknown3 = featureBlock.Unknown3;
                featureBlockBinary.Unknown4 = featureBlock.Unknown4;
                featureBlockBinary.Unknown5 = featureBlock.Unknown5;
                featureBlockBinary.Unknownd4_1 = featureBlock.Unknownd4_1;
                featureBlockBinary.Unknownd4_2 = featureBlock.Unknownd4_2;
                featureBlockBinary.Unknownd4_3 = featureBlock.Unknownd4_3;
                featureBlockBinary.Unknownd4_4 = featureBlock.Unknownd4_4;
                featureBlockBinary.Unknownd4_5 = featureBlock.Unknownd4_5;
                featureBlockBinary.Unknownd4_6 = featureBlock.Unknownd4_6;
                featureBlockBinary.Unknownd4_7 = featureBlock.Unknownd4_7;
                featureBlockBinary.Unknownd4_8 = featureBlock.Unknownd4_8;
                featureBlockBinary.Unknownd4_9 = featureBlock.Unknownd4_9;
                featureBlockBinary.Unknownd4_10 = featureBlock.Unknownd4_10;
                featureBlockBinary.Unknownd4_11 = featureBlock.Unknownd4_11;
                featureBlockBinary.Unknownd4_12 = featureBlock.Unknownd4_12;
                featureBlockBinary.Unknownd4_13 = featureBlock.Unknownd4_13;
                featureBlockBinary.Unknownd4_14 = featureBlock.Unknownd4_14;
                featureBlockBinary.Unknownd4_15 = featureBlock.Unknownd4_15;
                featureBlockBinary.Variable = featureBlock.Variable;
                creEffects2.Add(featureBlockBinary);
            }

            // pre fill the item slot array with 'no item'
            for (int i = 0; i < 40; i++)
            {
                creItemSlots.Add(-1);
            }

            short itemCount = 0;

            if (!array8.IsNullOrEmpty(creFile.Items.Helmet?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.Helmet.Charges1;
                itemBinary.Charges2 = creFile.Items.Helmet.Charges2;
                itemBinary.Charges3 = creFile.Items.Helmet.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Helmet.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Helmet.ExpiryValue;
                itemBinary.Filename = creFile.Items.Helmet.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.Helmet.Flags);

                creItems.Add(itemBinary);
                creItemSlots[0] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.Armor?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.Armor.Charges1;
                itemBinary.Charges2 = creFile.Items.Armor.Charges2;
                itemBinary.Charges3 = creFile.Items.Armor.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Armor.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Armor.ExpiryValue;
                itemBinary.Filename = creFile.Items.Armor.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.Armor.Flags);

                creItems.Add(itemBinary);
                creItemSlots[1] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.Shield?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.Shield.Charges1;
                itemBinary.Charges2 = creFile.Items.Shield.Charges2;
                itemBinary.Charges3 = creFile.Items.Shield.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Shield.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Shield.ExpiryValue;
                itemBinary.Filename = creFile.Items.Shield.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.Shield.Flags);

                creItems.Add(itemBinary);
                creItemSlots[2] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.Gloves?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.Gloves.Charges1;
                itemBinary.Charges2 = creFile.Items.Gloves.Charges2;
                itemBinary.Charges3 = creFile.Items.Gloves.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Gloves.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Gloves.ExpiryValue;
                itemBinary.Filename = creFile.Items.Gloves.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.Gloves.Flags);

                creItems.Add(itemBinary);
                creItemSlots[3] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.RingLeft?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.RingLeft.Charges1;
                itemBinary.Charges2 = creFile.Items.RingLeft.Charges2;
                itemBinary.Charges3 = creFile.Items.RingLeft.Charges3;
                itemBinary.ExpiryHour = creFile.Items.RingLeft.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.RingLeft.ExpiryValue;
                itemBinary.Filename = creFile.Items.RingLeft.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.RingLeft.Flags);

                creItems.Add(itemBinary);
                creItemSlots[4] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.RingRight?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.RingRight.Charges1;
                itemBinary.Charges2 = creFile.Items.RingRight.Charges2;
                itemBinary.Charges3 = creFile.Items.RingRight.Charges3;
                itemBinary.ExpiryHour = creFile.Items.RingRight.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.RingRight.ExpiryValue;
                itemBinary.Filename = creFile.Items.RingRight.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.RingRight.Flags);

                creItems.Add(itemBinary);
                creItemSlots[5] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.Amulet?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.Amulet.Charges1;
                itemBinary.Charges2 = creFile.Items.Amulet.Charges2;
                itemBinary.Charges3 = creFile.Items.Amulet.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Amulet.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Amulet.ExpiryValue;
                itemBinary.Filename = creFile.Items.Amulet.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.Amulet.Flags);

                creItems.Add(itemBinary);
                creItemSlots[6] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.Belt?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.Belt.Charges1;
                itemBinary.Charges2 = creFile.Items.Belt.Charges2;
                itemBinary.Charges3 = creFile.Items.Belt.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Belt.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Belt.ExpiryValue;
                itemBinary.Filename = creFile.Items.Belt.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.Belt.Flags);

                creItems.Add(itemBinary);
                creItemSlots[7] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.Boots?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.Boots.Charges1;
                itemBinary.Charges2 = creFile.Items.Boots.Charges2;
                itemBinary.Charges3 = creFile.Items.Boots.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Boots.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Boots.ExpiryValue;
                itemBinary.Filename = creFile.Items.Boots.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.Boots.Flags);

                creItems.Add(itemBinary);
                creItemSlots[8] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.Weapon1?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.Weapon1.Charges1;
                itemBinary.Charges2 = creFile.Items.Weapon1.Charges2;
                itemBinary.Charges3 = creFile.Items.Weapon1.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Weapon1.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Weapon1.ExpiryValue;
                itemBinary.Filename = creFile.Items.Weapon1.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.Weapon1.Flags);

                creItems.Add(itemBinary);
                creItemSlots[9] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.Weapon2?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.Weapon2.Charges1;
                itemBinary.Charges2 = creFile.Items.Weapon2.Charges2;
                itemBinary.Charges3 = creFile.Items.Weapon2.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Weapon2.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Weapon2.ExpiryValue;
                itemBinary.Filename = creFile.Items.Weapon2.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.Weapon2.Flags);

                creItems.Add(itemBinary);
                creItemSlots[10] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.Weapon3?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.Weapon3.Charges1;
                itemBinary.Charges2 = creFile.Items.Weapon3.Charges2;
                itemBinary.Charges3 = creFile.Items.Weapon3.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Weapon3.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Weapon3.ExpiryValue;
                itemBinary.Filename = creFile.Items.Weapon3.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.Weapon3.Flags);

                creItems.Add(itemBinary);
                creItemSlots[11] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.Weapon4?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.Weapon4.Charges1;
                itemBinary.Charges2 = creFile.Items.Weapon4.Charges2;
                itemBinary.Charges3 = creFile.Items.Weapon4.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Weapon4.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Weapon4.ExpiryValue;
                itemBinary.Filename = creFile.Items.Weapon4.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.Weapon4.Flags);

                creItems.Add(itemBinary);
                creItemSlots[12] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.Quiver1?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.Quiver1.Charges1;
                itemBinary.Charges2 = creFile.Items.Quiver1.Charges2;
                itemBinary.Charges3 = creFile.Items.Quiver1.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Quiver1.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Quiver1.ExpiryValue;
                itemBinary.Filename = creFile.Items.Quiver1.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.Quiver1.Flags);

                creItems.Add(itemBinary);
                creItemSlots[13] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.Quiver2?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.Quiver2.Charges1;
                itemBinary.Charges2 = creFile.Items.Quiver2.Charges2;
                itemBinary.Charges3 = creFile.Items.Quiver2.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Quiver2.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Quiver2.ExpiryValue;
                itemBinary.Filename = creFile.Items.Quiver2.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.Quiver2.Flags);

                creItems.Add(itemBinary);
                creItemSlots[14] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.Quiver3?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.Quiver3.Charges1;
                itemBinary.Charges2 = creFile.Items.Quiver3.Charges2;
                itemBinary.Charges3 = creFile.Items.Quiver3.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Quiver3.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Quiver3.ExpiryValue;
                itemBinary.Filename = creFile.Items.Quiver3.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.Quiver3.Flags);

                creItems.Add(itemBinary);
                creItemSlots[15] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.Quiver4?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.Quiver4.Charges1;
                itemBinary.Charges2 = creFile.Items.Quiver4.Charges2;
                itemBinary.Charges3 = creFile.Items.Quiver4.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Quiver4.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Quiver4.ExpiryValue;
                itemBinary.Filename = creFile.Items.Quiver4.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.Quiver4.Flags);

                creItems.Add(itemBinary);
                creItemSlots[16] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.Cloak?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.Cloak.Charges1;
                itemBinary.Charges2 = creFile.Items.Cloak.Charges2;
                itemBinary.Charges3 = creFile.Items.Cloak.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Cloak.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Cloak.ExpiryValue;
                itemBinary.Filename = creFile.Items.Cloak.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.Cloak.Flags);

                creItems.Add(itemBinary);
                creItemSlots[17] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.QuickItem1?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.QuickItem1.Charges1;
                itemBinary.Charges2 = creFile.Items.QuickItem1.Charges2;
                itemBinary.Charges3 = creFile.Items.QuickItem1.Charges3;
                itemBinary.ExpiryHour = creFile.Items.QuickItem1.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.QuickItem1.ExpiryValue;
                itemBinary.Filename = creFile.Items.QuickItem1.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.QuickItem1.Flags);

                creItems.Add(itemBinary);
                creItemSlots[18] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.QuickItem2?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.QuickItem2.Charges1;
                itemBinary.Charges2 = creFile.Items.QuickItem2.Charges2;
                itemBinary.Charges3 = creFile.Items.QuickItem2.Charges3;
                itemBinary.ExpiryHour = creFile.Items.QuickItem2.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.QuickItem2.ExpiryValue;
                itemBinary.Filename = creFile.Items.QuickItem2.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.QuickItem2.Flags);

                creItems.Add(itemBinary);
                creItemSlots[19] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.QuickItem3?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.QuickItem3.Charges1;
                itemBinary.Charges2 = creFile.Items.QuickItem3.Charges2;
                itemBinary.Charges3 = creFile.Items.QuickItem3.Charges3;
                itemBinary.ExpiryHour = creFile.Items.QuickItem3.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.QuickItem3.ExpiryValue;
                itemBinary.Filename = creFile.Items.QuickItem3.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.QuickItem3.Flags);

                creItems.Add(itemBinary);
                creItemSlots[20] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.InventoryItem1?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.InventoryItem1.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem1.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem1.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem1.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem1.ExpiryValue;
                itemBinary.Filename = creFile.Items.InventoryItem1.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.InventoryItem1.Flags);

                creItems.Add(itemBinary);
                creItemSlots[21] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.InventoryItem2?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.InventoryItem2.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem2.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem2.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem2.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem2.ExpiryValue;
                itemBinary.Filename = creFile.Items.InventoryItem2.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.InventoryItem2.Flags);

                creItems.Add(itemBinary);
                creItemSlots[22] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.InventoryItem3?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.InventoryItem3.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem3.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem3.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem3.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem3.ExpiryValue;
                itemBinary.Filename = creFile.Items.InventoryItem3.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.InventoryItem3.Flags);

                creItems.Add(itemBinary);
                creItemSlots[23] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.InventoryItem4?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.InventoryItem4.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem4.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem4.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem4.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem4.ExpiryValue;
                itemBinary.Filename = creFile.Items.InventoryItem4.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.InventoryItem4.Flags);

                creItems.Add(itemBinary);
                creItemSlots[24] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.InventoryItem5?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.InventoryItem5.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem5.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem5.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem5.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem5.ExpiryValue;
                itemBinary.Filename = creFile.Items.InventoryItem5.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.InventoryItem5.Flags);

                creItems.Add(itemBinary);
                creItemSlots[25] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.InventoryItem6?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.InventoryItem6.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem6.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem6.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem6.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem6.ExpiryValue;
                itemBinary.Filename = creFile.Items.InventoryItem6.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.InventoryItem6.Flags);

                creItems.Add(itemBinary);
                creItemSlots[26] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.InventoryItem7?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.InventoryItem7.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem7.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem7.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem7.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem7.ExpiryValue;
                itemBinary.Filename = creFile.Items.InventoryItem7.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.InventoryItem7.Flags);

                creItems.Add(itemBinary);
                creItemSlots[27] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.InventoryItem8?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.InventoryItem8.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem8.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem8.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem8.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem8.ExpiryValue;
                itemBinary.Filename = creFile.Items.InventoryItem8.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.InventoryItem8.Flags);

                creItems.Add(itemBinary);
                creItemSlots[28] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.InventoryItem9?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.InventoryItem9.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem9.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem9.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem9.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem9.ExpiryValue;
                itemBinary.Filename = creFile.Items.InventoryItem9.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.InventoryItem9.Flags);

                creItems.Add(itemBinary);
                creItemSlots[29] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.InventoryItem10?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.InventoryItem10.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem10.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem10.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem10.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem10.ExpiryValue;
                itemBinary.Filename = creFile.Items.InventoryItem10.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.InventoryItem10.Flags);

                creItems.Add(itemBinary);
                creItemSlots[30] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.InventoryItem11?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.InventoryItem11.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem11.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem11.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem11.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem11.ExpiryValue;
                itemBinary.Filename = creFile.Items.InventoryItem11.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.InventoryItem11.Flags);

                creItems.Add(itemBinary);
                creItemSlots[31] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.InventoryItem12?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.InventoryItem12.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem12.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem12.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem12.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem12.ExpiryValue;
                itemBinary.Filename = creFile.Items.InventoryItem12.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.InventoryItem12.Flags);

                creItems.Add(itemBinary);
                creItemSlots[32] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.InventoryItem13?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.InventoryItem13.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem13.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem13.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem13.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem13.ExpiryValue;
                itemBinary.Filename = creFile.Items.InventoryItem13.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.InventoryItem13.Flags);

                creItems.Add(itemBinary);
                creItemSlots[33] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.InventoryItem14?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.InventoryItem14.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem14.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem14.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem14.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem14.ExpiryValue;
                itemBinary.Filename = creFile.Items.InventoryItem14.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.InventoryItem14.Flags);

                creItems.Add(itemBinary);
                creItemSlots[34] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.InventoryItem15?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.InventoryItem15.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem15.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem15.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem15.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem15.ExpiryValue;
                itemBinary.Filename = creFile.Items.InventoryItem15.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.InventoryItem15.Flags);

                creItems.Add(itemBinary);
                creItemSlots[35] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.InventoryItem16?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.InventoryItem16.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem16.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem16.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem16.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem16.ExpiryValue;
                itemBinary.Filename = creFile.Items.InventoryItem16.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.InventoryItem16.Flags);

                creItems.Add(itemBinary);
                creItemSlots[36] = itemCount;
                itemCount++;
            }

            if (!array8.IsNullOrEmpty(creFile.Items.MagicWeapon?.Filename))
            {
                var itemBinary = new CreItemBinary();
                itemBinary.Charges1 = creFile.Items.MagicWeapon.Charges1;
                itemBinary.Charges2 = creFile.Items.MagicWeapon.Charges2;
                itemBinary.Charges3 = creFile.Items.MagicWeapon.Charges3;
                itemBinary.ExpiryHour = creFile.Items.MagicWeapon.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.MagicWeapon.ExpiryValue;
                itemBinary.Filename = creFile.Items.MagicWeapon.Filename;
                itemBinary.Flags = ItemFlags(creFile.Items.MagicWeapon.Flags);

                creItems.Add(itemBinary);
                creItemSlots[37] = itemCount;
                itemCount++;
            }

            creItemSlots[38] = creFile.Items.SelectedWeapon;
            creItemSlots[39] = creFile.Items.SelectedWeaponAbility;

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel1)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel2)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel3)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel4)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel5)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel6)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel7)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel8)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel9)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.PriestLevel1)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.PriestLevel2)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.PriestLevel3)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.PriestLevel4)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.PriestLevel5)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.PriestLevel6)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.PriestLevel7)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.Innate)
            {
                var memorisedSpellBinary = new CreMemorisedSpellBinary();
                memorisedSpellBinary.Filename = memorisedSpell.Filename;
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            var creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel1.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel1.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel1.Count);
            creSpellmemorisationInfo.SpellLevel = 0;
            creSpellmemorisationInfo.SpellOffset = 0;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel2.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel2.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel2.Count);
            creSpellmemorisationInfo.SpellLevel = 1;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel3.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel3.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel3.Count);
            creSpellmemorisationInfo.SpellLevel = 2;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel4.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel4.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel4.Count);
            creSpellmemorisationInfo.SpellLevel = 3;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel5.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel5.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel5.Count);
            creSpellmemorisationInfo.SpellLevel = 4;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel6.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel6.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel6.Count);
            creSpellmemorisationInfo.SpellLevel = 5;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel7.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel7.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel7.Count);
            creSpellmemorisationInfo.SpellLevel = 6;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count + creFile.MemorisedSpells.MageLevel6.Count;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel8.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel8.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel8.Count);
            creSpellmemorisationInfo.SpellLevel = 7;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count + creFile.MemorisedSpells.MageLevel6.Count +
                                                   creFile.MemorisedSpells.MageLevel7.Count;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel9.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel9.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel9.Count);
            creSpellmemorisationInfo.SpellLevel = 8;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count + creFile.MemorisedSpells.MageLevel6.Count +
                                                   creFile.MemorisedSpells.MageLevel7.Count + creFile.MemorisedSpells.MageLevel8.Count;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);


            creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel1.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel1.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel1.Count);
            creSpellmemorisationInfo.SpellLevel = 0;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count + creFile.MemorisedSpells.MageLevel6.Count +
                                                   creFile.MemorisedSpells.MageLevel7.Count + creFile.MemorisedSpells.MageLevel8.Count + creFile.MemorisedSpells.MageLevel9.Count;
            creSpellmemorisationInfo.SpellType = 1;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel2.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel2.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel2.Count);
            creSpellmemorisationInfo.SpellLevel = 1;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count + creFile.MemorisedSpells.MageLevel6.Count +
                                                   creFile.MemorisedSpells.MageLevel7.Count + creFile.MemorisedSpells.MageLevel8.Count + creFile.MemorisedSpells.MageLevel9.Count +
                                                   creFile.MemorisedSpells.PriestLevel1.Count;
            creSpellmemorisationInfo.SpellType = 1;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel1.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel3.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel3.Count);
            creSpellmemorisationInfo.SpellLevel = 2;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count + creFile.MemorisedSpells.MageLevel6.Count +
                                                   creFile.MemorisedSpells.MageLevel7.Count + creFile.MemorisedSpells.MageLevel8.Count + creFile.MemorisedSpells.MageLevel9.Count +
                                                   creFile.MemorisedSpells.PriestLevel1.Count + creFile.MemorisedSpells.PriestLevel2.Count;
            creSpellmemorisationInfo.SpellType = 1;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel1.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel4.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel4.Count);
            creSpellmemorisationInfo.SpellLevel = 3;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count + creFile.MemorisedSpells.MageLevel6.Count +
                                                   creFile.MemorisedSpells.MageLevel7.Count + creFile.MemorisedSpells.MageLevel8.Count + creFile.MemorisedSpells.MageLevel9.Count +
                                                   creFile.MemorisedSpells.PriestLevel1.Count + creFile.MemorisedSpells.PriestLevel2.Count + creFile.MemorisedSpells.PriestLevel3.Count;
            creSpellmemorisationInfo.SpellType = 1;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel1.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel5.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel5.Count);
            creSpellmemorisationInfo.SpellLevel = 4;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count + creFile.MemorisedSpells.MageLevel6.Count +
                                                   creFile.MemorisedSpells.MageLevel7.Count + creFile.MemorisedSpells.MageLevel8.Count + creFile.MemorisedSpells.MageLevel9.Count +
                                                   creFile.MemorisedSpells.PriestLevel1.Count + creFile.MemorisedSpells.PriestLevel2.Count + creFile.MemorisedSpells.PriestLevel3.Count +
                                                   creFile.MemorisedSpells.PriestLevel4.Count;
            creSpellmemorisationInfo.SpellType = 1;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel1.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel6.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel6.Count);
            creSpellmemorisationInfo.SpellLevel = 5;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count + creFile.MemorisedSpells.MageLevel6.Count +
                                                   creFile.MemorisedSpells.MageLevel7.Count + creFile.MemorisedSpells.MageLevel8.Count + creFile.MemorisedSpells.MageLevel9.Count +
                                                   creFile.MemorisedSpells.PriestLevel1.Count + creFile.MemorisedSpells.PriestLevel2.Count + creFile.MemorisedSpells.PriestLevel2.Count +
                                                   creFile.MemorisedSpells.PriestLevel4.Count + creFile.MemorisedSpells.PriestLevel5.Count;
            creSpellmemorisationInfo.SpellType = 1;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel1.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel7.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel7.Count);
            creSpellmemorisationInfo.SpellLevel = 6;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count + creFile.MemorisedSpells.MageLevel6.Count +
                                                   creFile.MemorisedSpells.MageLevel7.Count + creFile.MemorisedSpells.MageLevel8.Count + creFile.MemorisedSpells.MageLevel9.Count +
                                                   creFile.MemorisedSpells.PriestLevel1.Count + creFile.MemorisedSpells.PriestLevel2.Count + creFile.MemorisedSpells.PriestLevel3.Count +
                                                   creFile.MemorisedSpells.PriestLevel4.Count + creFile.MemorisedSpells.PriestLevel5.Count + creFile.MemorisedSpells.PriestLevel6.Count;
            creSpellmemorisationInfo.SpellType = 1;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfoBinary();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.Innate.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.Innate.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.Innate.Count);
            creSpellmemorisationInfo.SpellLevel = 0;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count + creFile.MemorisedSpells.MageLevel6.Count +
                                                   creFile.MemorisedSpells.MageLevel7.Count + creFile.MemorisedSpells.MageLevel8.Count + creFile.MemorisedSpells.MageLevel9.Count +
                                                   creFile.MemorisedSpells.PriestLevel1.Count + creFile.MemorisedSpells.PriestLevel2.Count + creFile.MemorisedSpells.PriestLevel3.Count +
                                                   creFile.MemorisedSpells.PriestLevel4.Count + creFile.MemorisedSpells.PriestLevel5.Count + creFile.MemorisedSpells.PriestLevel6.Count +
                                                   creFile.MemorisedSpells.PriestLevel7.Count;
            creSpellmemorisationInfo.SpellType = 2;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            foreach (var knownSpell in creFile.KnownSpells.MageLevel1)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 0;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.MageLevel2)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 1;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.MageLevel3)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 2;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.MageLevel4)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 3;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.MageLevel5)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 4;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.MageLevel6)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 5;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.MageLevel7)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 6;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.MageLevel8)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 7;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.MageLevel9)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 8;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.PriestLevel1)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 0;
                knownSpellBinary.SpellType = 0;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.PriestLevel2)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 1;
                knownSpellBinary.SpellType = 0;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.PriestLevel3)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 2;
                knownSpellBinary.SpellType = 0;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.PriestLevel4)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 3;
                knownSpellBinary.SpellType = 0;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.PriestLevel5)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 4;
                knownSpellBinary.SpellType = 0;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.PriestLevel6)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 5;
                knownSpellBinary.SpellType = 0;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.PriestLevel7)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 6;
                knownSpellBinary.SpellType = 0;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.Innate)
            {
                var knownSpellBinary = new CreKnownSpellBinary();
                knownSpellBinary.Filename = knownSpell.Filename;
                knownSpellBinary.SpellLevel = 0;
                knownSpellBinary.SpellType = 2;
                creKnownSpells.Add(knownSpellBinary);
            }

            var header = new CreHeaderBinary();
            header.Flags = creFile.Flags.ShowLongname ? header.Flags | Common.Bit0 : header.Flags;
            header.Flags = creFile.Flags.NoCorpse ? header.Flags | Common.Bit1 : header.Flags;
            header.Flags = creFile.Flags.KeepCorpse ? header.Flags | Common.Bit2 : header.Flags;
            header.Flags = creFile.Flags.OriginalFighter ? header.Flags | Common.Bit3 : header.Flags;
            header.Flags = creFile.Flags.OriginalMage ? header.Flags | Common.Bit4 : header.Flags;
            header.Flags = creFile.Flags.OriginalCleric ? header.Flags | Common.Bit5 : header.Flags;
            header.Flags = creFile.Flags.OriginalThief ? header.Flags | Common.Bit6 : header.Flags;
            header.Flags = creFile.Flags.OriginalDruid ? header.Flags | Common.Bit7 : header.Flags;
            header.Flags = creFile.Flags.OriginalRanger ? header.Flags | Common.Bit8 : header.Flags;
            header.Flags = creFile.Flags.FallenPaladin ? header.Flags | Common.Bit9 : header.Flags;
            header.Flags = creFile.Flags.FallenRanger ? header.Flags | Common.Bit10 : header.Flags;
            header.Flags = creFile.Flags.Exportable ? header.Flags | Common.Bit11 : header.Flags;
            header.Flags = creFile.Flags.HideInjuryStatus ? header.Flags | Common.Bit12 : header.Flags;
            header.Flags = creFile.Flags.QuestCritical ? header.Flags | Common.Bit13 : header.Flags;
            header.Flags = creFile.Flags.MovingBetweenAreas ? header.Flags | Common.Bit14 : header.Flags;
            header.Flags = creFile.Flags.BeenInParty ? header.Flags | Common.Bit15 : header.Flags;
            header.Flags = creFile.Flags.RestoreItem ? header.Flags | Common.Bit16 : header.Flags;
            header.Flags = creFile.Flags.ClearRestoreItem ? header.Flags | Common.Bit17 : header.Flags;
            header.Flags = creFile.Flags.Bit18 ? header.Flags | Common.Bit18 : header.Flags;
            header.Flags = creFile.Flags.Bit19 ? header.Flags | Common.Bit19 : header.Flags;
            header.Flags = creFile.Flags.PreventExplodingDeath ? header.Flags | Common.Bit20 : header.Flags;
            header.Flags = creFile.Flags.Bit21 ? header.Flags | Common.Bit21 : header.Flags;
            header.Flags = creFile.Flags.DoNotApplyNightmareModeModifiers ? header.Flags | Common.Bit22 : header.Flags;
            header.Flags = creFile.Flags.NotTooltip ? header.Flags | Common.Bit23 : header.Flags;
            header.Flags = creFile.Flags.RandomWalkEa ? header.Flags | Common.Bit24 : header.Flags;
            header.Flags = creFile.Flags.RandomWalkGender ? header.Flags | Common.Bit25 : header.Flags;
            header.Flags = creFile.Flags.RandomWalkRace ? header.Flags | Common.Bit26 : header.Flags;
            header.Flags = creFile.Flags.RandomWalkClass ? header.Flags | Common.Bit27 : header.Flags;
            header.Flags = creFile.Flags.RandomWalkSpecific ? header.Flags | Common.Bit28 : header.Flags;
            header.Flags = creFile.Flags.RandomWalkGender ? header.Flags | Common.Bit29 : header.Flags;
            header.Flags = creFile.Flags.RandomWalkAlignment ? header.Flags | Common.Bit30 : header.Flags;
            header.Flags = creFile.Flags.UnInterruptable ? header.Flags | Common.Bit31 : header.Flags;
            header.ftype = new array4() { character1 = 'C', character2 = 'R', character3 = 'E', character4 = ' ' };
            header.fversion = new array4() { character1 = 'V', character2 = '1', character3 = '.', character4 = '0' };
            header.AcidResistance = creFile.AcidResistance;
            header.Alignment = creFile.Alignment;
            header.Animation = creFile.Animation;
            header.ArmorClassEffective = creFile.ArmorClassEffective;
            header.ArmorClassNatural = creFile.ArmorClassNatural;
            header.ArmorColourIndex = creFile.ArmorColourIndex;
            header.Charisma = creFile.Charisma;
            header.Class = creFile.Class;
            header.ColdResistance = creFile.ColdResistance;
            header.Constitution = creFile.Constitution;
            header.CrushingModifuer = creFile.CrushingModifuer;
            header.CrushingResistance = creFile.CrushingResistance;
            header.CurrentHP = creFile.CurrentHP;
            header.DeathVariable = creFile.DeathVariable;
            header.DetectIllusion = creFile.DetectIllusion;
            header.Dexterity = creFile.Dexterity;
            header.DialogFile = creFile.DialogFile;
            header.EffectOffset = HeaderSize;
            header.ElectricityResistance = creFile.ElectricityResistance;
            header.EnemyAlly = creFile.EnemyAlly;
            header.Fatigue = creFile.Fatigue;
            header.FindTraps = creFile.FindTraps;
            header.FireResistance = creFile.FireResistance;
            header.Gender = creFile.Gender;
            header.General = creFile.General;
            header.GlobalActorEnumeration = creFile.GlobalActorEnumeration;
            header.Gold = creFile.Gold;
            header.HairColourIndex = creFile.HairColourIndex;
            header.HideInShadows = creFile.HideInShadows;
            header.Intelligence = creFile.Intelligence;
            header.Intoxication = creFile.Intoxication;

            uint kit = 0;
            kit |= (creFile.Kit.None ? 1u : 0u) << 31;
            kit |= (creFile.Kit.Barbarian ? 1u : 0u) << 30;
            kit |= (creFile.Kit.TrueClass ? 1u : 0u) << 29;
            kit |= (creFile.Kit.Berserker ? 1u : 0u) << 28;
            kit |= (creFile.Kit.WizardSlayer ? 1u : 0u) << 27;
            kit |= (creFile.Kit.Kensai ? 1u : 0u) << 26;
            kit |= (creFile.Kit.Cavalier ? 1u : 0u) << 25;
            kit |= (creFile.Kit.Inquisitor ? 1u : 0u) << 24;
            kit |= (creFile.Kit.Undeadhunter ? 1u : 0u) << 23;
            kit |= (creFile.Kit.Archer ? 1u : 0u) << 22;
            kit |= (creFile.Kit.Stalker ? 1u : 0u) << 21;
            kit |= (creFile.Kit.Beastmaster ? 1u : 0u) << 20;
            kit |= (creFile.Kit.Assassin ? 1u : 0u) << 19;
            kit |= (creFile.Kit.Bountyhunter ? 1u : 0u) << 18;
            kit |= (creFile.Kit.Swashbuckler ? 1u : 0u) << 17;
            kit |= (creFile.Kit.Blade ? 1u : 0u) << 16;
            kit |= (creFile.Kit.Jester ? 1u : 0u) << 15;
            kit |= (creFile.Kit.Skald ? 1u : 0u) << 14;
            kit |= (creFile.Kit.Totemic ? 1u : 0u) << 13;
            kit |= (creFile.Kit.Shapeshifter ? 1u : 0u) << 12;
            kit |= (creFile.Kit.Avenger ? 1u : 0u) << 11;
            kit |= (creFile.Kit.ClericOfTalos ? 1u : 0u) << 10;
            kit |= (creFile.Kit.ClericOfHelm ? 1u : 0u) << 9;
            kit |= (creFile.Kit.ClericOfLathander ? 1u : 0u) << 8;
            kit |= (creFile.Kit.Abjurer ? 1u : 0u) << 7;
            kit |= (creFile.Kit.Conjurer ? 1u : 0u) << 6;
            kit |= (creFile.Kit.Diviner ? 1u : 0u) << 5;
            kit |= (creFile.Kit.Enchanter ? 1u : 0u) << 4;
            kit |= (creFile.Kit.Illusionist ? 1u : 0u) << 3;
            kit |= (creFile.Kit.Invoker ? 1u : 0u) << 2;
            kit |= (creFile.Kit.Necromancer ? 1u : 0u) << 1;
            kit |= (creFile.Kit.Transmuter ? 1u : 0u) << 0;
            header.Kit = kit;

            header.LargePortrait = creFile.LargePortrait;
            header.LeatherColourIndex = creFile.LeatherColourIndex;
            header.Level1 = creFile.Level1;
            header.Level2 = creFile.Level2;
            header.Level3 = creFile.Level3;
            header.LocalActorEnumeration = creFile.LocalActorEnumeration;
            header.LockPicking = creFile.LockPicking;
            header.LongName = Common.WriteString(creFile.LongName, TlkFile);
            header.Lore = creFile.Lore;
            header.Luck = creFile.Luck;
            header.MagicColdResistance = creFile.MagicColdResistance;
            header.MagicFireResistance = creFile.MagicFireResistance;
            header.MagicResistance = creFile.MagicResistance;
            header.MajorColourIndex = creFile.MajorColourIndex;
            header.MaximumHP = creFile.MaximumHP;
            header.MetalColourIndex = creFile.MetalColourIndex;
            header.MinorColourIndex = creFile.MinorColourIndex;
            header.MissileModifier = creFile.MissileModifier;
            header.MissileResistance = creFile.MissileResistance;
            header.Morale = creFile.Morale;
            header.MoraleBreak = creFile.MoraleBreak;
            header.MoraleRecoveryTime = creFile.MoraleRecoveryTime;
            header.NumberOfAttacks = creFile.NumberOfAttacks;
            header.ObjectIdRef1 = creFile.ObjectIdRef1;
            header.ObjectIdRef2 = creFile.ObjectIdRef2;
            header.ObjectIdRef3 = creFile.ObjectIdRef3;
            header.ObjectIdRef4 = creFile.ObjectIdRef4;
            header.ObjectIdRef5 = creFile.ObjectIdRef5;
            header.PickPockets = creFile.PickPockets;
            header.PiercingModifier = creFile.PiercingModifier;
            header.PiercingResistance = creFile.PiercingResistance;
            header.PowerLevel = creFile.PowerLevel;
            header.Race = creFile.Race;
            header.RacialEnemy = creFile.RacialEnemy;
            header.Reputation = creFile.Reputation;
            header.SaveVsBreath = creFile.SaveVsBreath;
            header.SaveVsDeath = creFile.SaveVsDeath;
            header.SaveVsPolymorph = creFile.SaveVsPolymorph;
            header.SaveVsSpells = creFile.SaveVsSpells;
            header.SaveVsWands = creFile.SaveVsWands;
            header.ScriptClass = creFile.ScriptClass;
            header.ScriptDefault = creFile.ScriptDefault;
            header.ScriptGeneral = creFile.ScriptGeneral;
            header.ScriptOverride = creFile.ScriptOverride;
            header.ScriptRace = creFile.ScriptRace;
            header.SetTraps = creFile.SetTraps;
            header.Sex = creFile.Sex;
            header.ShortName = Common.WriteString(creFile.ShortName, TlkFile);
            header.SkinColourIndex = creFile.SkinColourIndex;
            header.SlashingResistance = creFile.SlashingResistance;
            header.SmallPortrait = creFile.SmallPortrait;
            header.Specific = creFile.Specific;
            header.StatusFlags = creFile.StatusFlags.Sleeping ? header.StatusFlags | Common.Bit0 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Berserk ? header.StatusFlags | Common.Bit1 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Panic ? header.StatusFlags | Common.Bit2 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Stunned ? header.StatusFlags | Common.Bit3 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Invisible ? header.StatusFlags | Common.Bit4 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Helpless ? header.StatusFlags | Common.Bit5 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.FrozenDeath ? header.StatusFlags | Common.Bit6 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.StoneDeath ? header.StatusFlags | Common.Bit7 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.ExplodingDeath ? header.StatusFlags | Common.Bit8 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.FlameDeath ? header.StatusFlags | Common.Bit9 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.AcidDeath ? header.StatusFlags | Common.Bit10 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Dead ? header.StatusFlags | Common.Bit11 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Silenced ? header.StatusFlags | Common.Bit12 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Charmed ? header.StatusFlags | Common.Bit13 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Poisoned ? header.StatusFlags | Common.Bit14 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Hasted ? header.StatusFlags | Common.Bit15 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Slowed ? header.StatusFlags | Common.Bit16 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Infravision ? header.StatusFlags | Common.Bit17 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Blind ? header.StatusFlags | Common.Bit18 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Diseased ? header.StatusFlags | Common.Bit19 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Feebleminded ? header.StatusFlags | Common.Bit20 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Nondetection ? header.StatusFlags | Common.Bit21 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.ImprovedInvisibility ? header.StatusFlags | Common.Bit22 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Bless ? header.StatusFlags | Common.Bit23 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Chant ? header.StatusFlags | Common.Bit24 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.DrawUponHolyMight ? header.StatusFlags | Common.Bit25 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Luck ? header.StatusFlags | Common.Bit26 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Aid ? header.StatusFlags | Common.Bit27 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.ChantBad ? header.StatusFlags | Common.Bit28 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Bless ? header.StatusFlags | Common.Bit29 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.MirrorImage ? header.StatusFlags | Common.Bit30 : header.StatusFlags;
            header.StatusFlags = creFile.StatusFlags.Confused ? header.StatusFlags | Common.Bit31 : header.StatusFlags;
            header.Stealth = creFile.Stealth;
            header.Strength = creFile.Strength;
            header.StrengthBonus = creFile.StrengthBonus;
            header.Strref1 = Common.WriteString(creFile.Strref1, TlkFile);
            header.Strref2 = Common.WriteString(creFile.Strref2, TlkFile);
            header.Strref3 = Common.WriteString(creFile.Strref3, TlkFile);
            header.Strref4 = Common.WriteString(creFile.Strref4, TlkFile);
            header.Strref5 = Common.WriteString(creFile.Strref5, TlkFile);
            header.Strref6 = Common.WriteString(creFile.Strref6, TlkFile);
            header.Strref7 = Common.WriteString(creFile.Strref7, TlkFile);
            header.Strref8 = Common.WriteString(creFile.Strref8, TlkFile);
            header.Strref9 = Common.WriteString(creFile.Strref9, TlkFile);
            header.Strref10 = Common.WriteString(creFile.Strref10, TlkFile);
            header.Strref11 = Common.WriteString(creFile.Strref11, TlkFile);
            header.Strref12 = Common.WriteString(creFile.Strref12, TlkFile);
            header.Strref13 = Common.WriteString(creFile.Strref13, TlkFile);
            header.Strref14 = Common.WriteString(creFile.Strref14, TlkFile);
            header.Strref15 = Common.WriteString(creFile.Strref15, TlkFile);
            header.Strref16 = Common.WriteString(creFile.Strref16, TlkFile);
            header.Strref17 = Common.WriteString(creFile.Strref17, TlkFile);
            header.Strref18 = Common.WriteString(creFile.Strref18, TlkFile);
            header.Strref19 = Common.WriteString(creFile.Strref19, TlkFile);
            header.Strref20 = Common.WriteString(creFile.Strref20, TlkFile);
            header.Strref21 = Common.WriteString(creFile.Strref21, TlkFile);
            header.Strref22 = Common.WriteString(creFile.Strref22, TlkFile);
            header.Strref23 = Common.WriteString(creFile.Strref23, TlkFile);
            header.Strref24 = Common.WriteString(creFile.Strref24, TlkFile);
            header.Strref25 = Common.WriteString(creFile.Strref25, TlkFile);
            header.Strref26 = Common.WriteString(creFile.Strref26, TlkFile);
            header.Strref27 = Common.WriteString(creFile.Strref27, TlkFile);
            header.Strref28 = Common.WriteString(creFile.Strref28, TlkFile);
            header.Strref29 = Common.WriteString(creFile.Strref29, TlkFile);
            header.Strref30 = Common.WriteString(creFile.Strref30, TlkFile);
            header.Strref31 = Common.WriteString(creFile.Strref31, TlkFile);
            header.Strref32 = Common.WriteString(creFile.Strref32, TlkFile);
            header.Strref33 = Common.WriteString(creFile.Strref33, TlkFile);
            header.Strref34 = Common.WriteString(creFile.Strref34, TlkFile);
            header.Strref35 = Common.WriteString(creFile.Strref35, TlkFile);
            header.Strref36 = Common.WriteString(creFile.Strref36, TlkFile);
            header.Strref37 = Common.WriteString(creFile.Strref37, TlkFile);
            header.Strref38 = Common.WriteString(creFile.Strref38, TlkFile);
            header.Strref39 = Common.WriteString(creFile.Strref39, TlkFile);
            header.Strref40 = Common.WriteString(creFile.Strref40, TlkFile);
            header.Strref41 = Common.WriteString(creFile.Strref41, TlkFile);
            header.Strref42 = Common.WriteString(creFile.Strref42, TlkFile);
            header.Strref43 = Common.WriteString(creFile.Strref43, TlkFile);
            header.Strref44 = Common.WriteString(creFile.Strref44, TlkFile);
            header.Strref45 = Common.WriteString(creFile.Strref45, TlkFile);
            header.Strref46 = Common.WriteString(creFile.Strref46, TlkFile);
            header.Strref47 = Common.WriteString(creFile.Strref47, TlkFile);
            header.Strref48 = Common.WriteString(creFile.Strref48, TlkFile);
            header.Strref49 = Common.WriteString(creFile.Strref49, TlkFile);
            header.Strref50 = Common.WriteString(creFile.Strref50, TlkFile);
            header.Strref51 = Common.WriteString(creFile.Strref51, TlkFile);
            header.Strref52 = Common.WriteString(creFile.Strref52, TlkFile);
            header.Strref53 = Common.WriteString(creFile.Strref53, TlkFile);
            header.Strref54 = Common.WriteString(creFile.Strref54, TlkFile);
            header.Strref55 = Common.WriteString(creFile.Strref55, TlkFile);
            header.Strref56 = Common.WriteString(creFile.Strref56, TlkFile);
            header.Strref57 = Common.WriteString(creFile.Strref57, TlkFile);
            header.Strref58 = Common.WriteString(creFile.Strref58, TlkFile);
            header.Strref59 = Common.WriteString(creFile.Strref59, TlkFile);
            header.Strref60 = Common.WriteString(creFile.Strref60, TlkFile);
            header.Strref61 = Common.WriteString(creFile.Strref61, TlkFile);
            header.Strref62 = Common.WriteString(creFile.Strref62, TlkFile);
            header.Strref63 = Common.WriteString(creFile.Strref63, TlkFile);
            header.Strref64 = Common.WriteString(creFile.Strref64, TlkFile);
            header.Strref65 = Common.WriteString(creFile.Strref65, TlkFile);
            header.Strref66 = Common.WriteString(creFile.Strref66, TlkFile);
            header.Strref67 = Common.WriteString(creFile.Strref67, TlkFile);
            header.Strref68 = Common.WriteString(creFile.Strref68, TlkFile);
            header.Strref69 = Common.WriteString(creFile.Strref69, TlkFile);
            header.Strref70 = Common.WriteString(creFile.Strref70, TlkFile);
            header.Strref71 = Common.WriteString(creFile.Strref71, TlkFile);
            header.Strref72 = Common.WriteString(creFile.Strref72, TlkFile);
            header.Strref73 = Common.WriteString(creFile.Strref73, TlkFile);
            header.Strref74 = Common.WriteString(creFile.Strref74, TlkFile);
            header.Strref75 = Common.WriteString(creFile.Strref75, TlkFile);
            header.Strref76 = Common.WriteString(creFile.Strref76, TlkFile);
            header.Strref77 = Common.WriteString(creFile.Strref77, TlkFile);
            header.Strref78 = Common.WriteString(creFile.Strref78, TlkFile);
            header.Strref79 = Common.WriteString(creFile.Strref79, TlkFile);
            header.Strref80 = Common.WriteString(creFile.Strref80, TlkFile);
            header.Strref81 = Common.WriteString(creFile.Strref81, TlkFile);
            header.Strref82 = Common.WriteString(creFile.Strref82, TlkFile);
            header.Strref83 = Common.WriteString(creFile.Strref83, TlkFile);
            header.Strref84 = Common.WriteString(creFile.Strref84, TlkFile);
            header.Strref85 = Common.WriteString(creFile.Strref85, TlkFile);
            header.Strref86 = Common.WriteString(creFile.Strref86, TlkFile);
            header.Strref87 = Common.WriteString(creFile.Strref87, TlkFile);
            header.Strref88 = Common.WriteString(creFile.Strref88, TlkFile);
            header.Strref89 = Common.WriteString(creFile.Strref89, TlkFile);
            header.Strref90 = Common.WriteString(creFile.Strref90, TlkFile);
            header.Strref91 = Common.WriteString(creFile.Strref91, TlkFile);
            header.Strref92 = Common.WriteString(creFile.Strref92, TlkFile);
            header.Strref93 = Common.WriteString(creFile.Strref93, TlkFile);
            header.Strref94 = Common.WriteString(creFile.Strref94, TlkFile);
            header.Strref95 = Common.WriteString(creFile.Strref95, TlkFile);
            header.Strref96 = Common.WriteString(creFile.Strref96, TlkFile);
            header.Strref97 = Common.WriteString(creFile.Strref97, TlkFile);
            header.Strref98 = Common.WriteString(creFile.Strref98, TlkFile);
            header.Strref99 = Common.WriteString(creFile.Strref99, TlkFile);
            header.Strref100 = Common.WriteString(creFile.Strref100, TlkFile);
            header.Thac0 = creFile.Thac0;
            header.Tracking = creFile.Tracking;
            header.Unused1Proficiency = creFile.Unused1Proficiency;
            header.Unused2Proficiency = creFile.Unused2Proficiency;
            header.Unused3Proficiency = creFile.Unused3Proficiency;
            header.Unused4Proficiency = creFile.Unused4Proficiency;
            header.Unused5Proficiency = creFile.Unused5Proficiency;
            header.Unused6Proficiency = creFile.Unused6Proficiency;
            header.Unused7Proficiency = creFile.Unused7Proficiency;
            header.UnusedAxe = creFile.UnusedAxeProficiency;
            header.UnusedBlunt = creFile.UnusedBluntProficiency;
            header.UnusedBows = creFile.UnusedBowProficiency;
            header.UnusedLargeSwords = creFile.UnusedLargeSwordProficiency;
            header.UnusedMissile = creFile.UnusedMissileProficiency;
            header.UnusedSmallSwords = creFile.UnusedSmallSwordProficiency;
            header.UnusedSpears = creFile.UnusedSpearProficiency;
            header.UnusedSpiked = creFile.UnusedSpikedProficiency;
            header.Wisdom = creFile.Wisdom;
            header.XPReward = creFile.XPReward;
            header.KnownSpellsCount = creKnownSpells.Count;
            header.KnownSpellsoffset = HeaderSize;

            header.SpellMemorizationCount = creSpellMemorisationInfo.Count;
            header.SpellMemorizationOffset = HeaderSize + (creKnownSpells.Count * KnownSpellSize);

            header.MemorizedSpellsCount = creMemorisedSpells.Count;
            header.MemorizedSpellsOffset = HeaderSize + (creKnownSpells.Count * KnownSpellSize) + (creSpellMemorisationInfo.Count * memorisedspellinfoSize);

            int totalEffectBlockSize = 0;
            if (creEffects1.Count > 1)
            {
                header.EffVersion = 0;
                header.EffectCount = creEffects1.Count;
                header.EffectOffset = HeaderSize + (creKnownSpells.Count * KnownSpellSize) + (creSpellMemorisationInfo.Count * memorisedspellinfoSize) + (creMemorisedSpells.Count * memorisedspellSize);
                totalEffectBlockSize = (creEffects1.Count * Eff1Size);
            }
            if (creEffects2.Count > 0)
            {
                header.EffVersion = 1;
                header.EffectCount = creEffects2.Count;
                header.EffectOffset = HeaderSize + (creKnownSpells.Count * KnownSpellSize) + (creSpellMemorisationInfo.Count * memorisedspellinfoSize) + (creMemorisedSpells.Count * memorisedspellSize);
                totalEffectBlockSize = (creEffects2.Count * Eff2Size);
            }

            header.ItemCount = creItems.Count;
            header.ItemOffset = HeaderSize + (totalEffectBlockSize) + (creKnownSpells.Count * KnownSpellSize) + (creMemorisedSpells.Count * memorisedspellSize) + (creSpellMemorisationInfo.Count * memorisedspellinfoSize);

            header.ItemSlotOffset = HeaderSize + (totalEffectBlockSize) + (creKnownSpells.Count * KnownSpellSize) + (creMemorisedSpells.Count * memorisedspellSize) + (creSpellMemorisationInfo.Count * memorisedspellinfoSize) + (creItems.Count * ItemSize);

            using var s = new MemoryStream();
            using var bw = new BinaryWriter(s);
            var headerAsBytes = Common.WriteStruct(header);

            bw.Write(headerAsBytes);

            foreach (var spell in creKnownSpells)
            {
                var spellAsBytes = Common.WriteStruct(spell);
                bw.Write(spellAsBytes);
            }

            foreach (var spell in creSpellMemorisationInfo)
            {
                var spellAsBytes = Common.WriteStruct(spell);
                bw.Write(spellAsBytes);
            }


            foreach (var spell in creMemorisedSpells)
            {
                var spellAsBytes = Common.WriteStruct(spell);
                bw.Write(spellAsBytes);
            }

            if (creFile.EffVersion == 0)
            {
                foreach (var effect in creEffects1)
                {
                    var effectAsBytes = Common.WriteStruct(effect);
                    bw.Write(effectAsBytes);
                }
            }
            if (creFile.EffVersion == 1)
            {
                foreach (var effect in creEffects2)
                {
                    var effectAsBytes = Common.WriteStruct(effect);
                    bw.Write(effectAsBytes);
                }
            }

            foreach (var item in creItems)
            {
                var itemAsBytes = Common.WriteStruct(item);
                bw.Write(itemAsBytes);
            }

            foreach (var itemSlot in creItemSlots)
            {
                bw.Write(itemSlot);
            }


            var byteArray = s.ToArray();
            return byteArray;
        }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not CreFile)
                throw new ArgumentException("File is not a valid cre file");

            var creFile = file as CreFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(creFile) == creFile.Checksum))
                return false;

            var bytes = Write(creFile);

            BackupManger?.BackupFile(file, file.Filename, file.FileType, this);

            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            fs.Write(bytes);
            return true;
        }

        private int ItemFlags(CreatureItemFlags flags)
        {
            var result = 0;
            result = flags.IsIdentified ? result | Common.Bit0 : result;
            result = flags.IsStealable ? result | Common.Bit1 : result;
            result = flags.IsStealable ? result | Common.Bit2 : result;
            result = flags.IsDroppable ? result | Common.Bit3 : result;
            result = flags.Bit4 ? result | Common.Bit4 : result;
            result = flags.Bit5 ? result | Common.Bit5 : result;
            result = flags.Bit6 ? result | Common.Bit6 : result;
            result = flags.Bit7 ? result | Common.Bit7 : result;
            result = flags.Bit8 ? result | Common.Bit8 : result;
            result = flags.Bit9 ? result | Common.Bit9 : result;
            result = flags.Bit10 ? result | Common.Bit10 : result;
            result = flags.Bit11 ? result | Common.Bit11 : result;
            result = flags.Bit12 ? result | Common.Bit12 : result;
            result = flags.Bit13 ? result | Common.Bit13 : result;
            result = flags.Bit14 ? result | Common.Bit14 : result;
            result = flags.Bit15 ? result | Common.Bit15 : result;
            result = flags.Bit16 ? result | Common.Bit16 : result;
            result = flags.Bit17 ? result | Common.Bit17 : result;
            result = flags.Bit18 ? result | Common.Bit18 : result;
            result = flags.Bit19 ? result | Common.Bit19 : result;
            result = flags.Bit20 ? result | Common.Bit20 : result;
            result = flags.Bit21 ? result | Common.Bit21 : result;
            result = flags.Bit22 ? result | Common.Bit22 : result;
            result = flags.Bit23 ? result | Common.Bit23 : result;
            result = flags.Bit24 ? result | Common.Bit24 : result;
            result = flags.Bit25 ? result | Common.Bit25 : result;
            result = flags.Bit26 ? result | Common.Bit26 : result;
            result = flags.Bit27 ? result | Common.Bit27 : result;
            result = flags.Bit28 ? result | Common.Bit28 : result;
            result = flags.Bit29 ? result | Common.Bit29 : result;
            result = flags.Bit30 ? result | Common.Bit30 : result;
            result = flags.Bit31 ? result | Common.Bit31 : result;
            return result;
        }
    }
}