using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsApplication1.Binary;
using WindowsFormsApplication1.Files;
using System.IO;

namespace WindowsFormsApplication1.Writers
{
    class CreBinaryWriter
    {
        const int HeaderSize = 724;
        const int Eff1Size = 48;
        const int Eff2Size = 272 - 8;
        const int ItemSize = 20;
        const int ItemSlotSize = 40 * 2;
        const int KnownSpellSize = 12;
        const int memorisedspellinfoSize = 16;
        const int memorisedspellSize = 12;

        List<CreKnownSpell> creKnownSpells = new List<CreKnownSpell>();
        List<CreSpellMemorisationInfo> creSpellMemorisationInfo = new List<CreSpellMemorisationInfo>();
        List<CreMemorisedSpell> creMemorisedSpells = new List<CreMemorisedSpell>();
        List<Eff1Binary> creEffects1 = new List<Eff1Binary>();
        List<EmbeddedEff> creEffects2 = new List<EmbeddedEff>();
        List<CreItem> creItems = new List<CreItem>();
        List<short> creItemSlots = new List<short>();

        public void Write(string filename, CreFile creFile)
        {
            foreach (var featureBlock in creFile.Effects1)
            {
                Eff1Binary featureBlockBinary = new Eff1Binary();
                featureBlockBinary.DiceSides = featureBlock.DiceSides;
                featureBlockBinary.DiceThrown = featureBlock.DiceThrown;
                featureBlockBinary.DispelResistance = featureBlock.DispelResistance;
                featureBlockBinary.Duration = featureBlock.Duration;
                featureBlockBinary.Opcode = featureBlock.Opcode;
                featureBlockBinary.Parameter1 = featureBlock.Parameter1;
                featureBlockBinary.Parameter2 = featureBlock.Parameter2;
                featureBlockBinary.Power = featureBlock.Power;
                featureBlockBinary.Probability1 = featureBlock.Probability1;
                featureBlockBinary.Probability2 = featureBlock.Probability2;
                featureBlockBinary.Resource = new array8(featureBlock.resource);
                featureBlockBinary.SavingThrowBonus = featureBlock.SavingThrowBonus;
                featureBlockBinary.SavingThrowType = featureBlock.SavingThrowType;
                featureBlockBinary.TargetType = featureBlock.TargetType;
                featureBlockBinary.TimingMode = featureBlock.TimingMode;
                featureBlockBinary.Unknown = featureBlock.Unknown;
                creEffects1.Add(featureBlockBinary);
            }

            foreach (var featureBlock in creFile.Effects2)
            {
                EmbeddedEff featureBlockBinary = new EmbeddedEff();
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

            if (!String.IsNullOrEmpty(creFile.Items.Helmet.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Helmet.Charges1;
                itemBinary.Charges2 = creFile.Items.Helmet.Charges2;
                itemBinary.Charges3 = creFile.Items.Helmet.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Helmet.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Helmet.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Helmet.Filename);
                itemBinary.Flags = creFile.Items.Helmet.Flags;

                creItems.Add(itemBinary);
                creItemSlots[0] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.Armor.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Armor.Charges1;
                itemBinary.Charges2 = creFile.Items.Armor.Charges2;
                itemBinary.Charges3 = creFile.Items.Armor.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Armor.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Armor.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Armor.Filename);
                itemBinary.Flags = creFile.Items.Armor.Flags;

                creItems.Add(itemBinary);
                creItemSlots[1] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.Shield.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Shield.Charges1;
                itemBinary.Charges2 = creFile.Items.Shield.Charges2;
                itemBinary.Charges3 = creFile.Items.Shield.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Shield.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Shield.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Shield.Filename);
                itemBinary.Flags = creFile.Items.Shield.Flags;

                creItems.Add(itemBinary);
                creItemSlots[2] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.Gloves.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Gloves.Charges1;
                itemBinary.Charges2 = creFile.Items.Gloves.Charges2;
                itemBinary.Charges3 = creFile.Items.Gloves.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Gloves.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Gloves.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Gloves.Filename);
                itemBinary.Flags = creFile.Items.Gloves.Flags;

                creItems.Add(itemBinary);
                creItemSlots[3] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.RingLeft.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.RingLeft.Charges1;
                itemBinary.Charges2 = creFile.Items.RingLeft.Charges2;
                itemBinary.Charges3 = creFile.Items.RingLeft.Charges3;
                itemBinary.ExpiryHour = creFile.Items.RingLeft.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.RingLeft.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.RingLeft.Filename);
                itemBinary.Flags = creFile.Items.RingLeft.Flags;

                creItems.Add(itemBinary);
                creItemSlots[4] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.RingRight.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.RingRight.Charges1;
                itemBinary.Charges2 = creFile.Items.RingRight.Charges2;
                itemBinary.Charges3 = creFile.Items.RingRight.Charges3;
                itemBinary.ExpiryHour = creFile.Items.RingRight.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.RingRight.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.RingRight.Filename);
                itemBinary.Flags = creFile.Items.RingRight.Flags;

                creItems.Add(itemBinary);
                creItemSlots[5] = itemCount;
                itemCount++;
            }


            if (!String.IsNullOrEmpty(creFile.Items.Amulet.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Amulet.Charges1;
                itemBinary.Charges2 = creFile.Items.Amulet.Charges2;
                itemBinary.Charges3 = creFile.Items.Amulet.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Amulet.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Amulet.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Amulet.Filename);
                itemBinary.Flags = creFile.Items.Amulet.Flags;

                creItems.Add(itemBinary);
                creItemSlots[6] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.Belt.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Belt.Charges1;
                itemBinary.Charges2 = creFile.Items.Belt.Charges2;
                itemBinary.Charges3 = creFile.Items.Belt.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Belt.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Belt.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Belt.Filename);
                itemBinary.Flags = creFile.Items.Amulet.Flags;

                creItems.Add(itemBinary);
                creItemSlots[7] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.Boots.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Boots.Charges1;
                itemBinary.Charges2 = creFile.Items.Boots.Charges2;
                itemBinary.Charges3 = creFile.Items.Boots.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Boots.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Boots.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Boots.Filename);
                itemBinary.Flags = creFile.Items.Boots.Flags;

                creItems.Add(itemBinary);
                creItemSlots[8] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.Weapon1.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Weapon1.Charges1;
                itemBinary.Charges2 = creFile.Items.Weapon1.Charges2;
                itemBinary.Charges3 = creFile.Items.Weapon1.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Weapon1.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Weapon1.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Weapon1.Filename);
                itemBinary.Flags = creFile.Items.Weapon1.Flags;

                creItems.Add(itemBinary);
                creItemSlots[9] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.Weapon2.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Weapon2.Charges1;
                itemBinary.Charges2 = creFile.Items.Weapon2.Charges2;
                itemBinary.Charges3 = creFile.Items.Weapon2.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Weapon2.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Weapon2.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Weapon2.Filename);
                itemBinary.Flags = creFile.Items.Weapon2.Flags;

                creItems.Add(itemBinary);
                creItemSlots[10] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.Weapon3.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Weapon3.Charges1;
                itemBinary.Charges2 = creFile.Items.Weapon3.Charges2;
                itemBinary.Charges3 = creFile.Items.Weapon3.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Weapon3.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Weapon3.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Weapon3.Filename);
                itemBinary.Flags = creFile.Items.Weapon3.Flags;

                creItems.Add(itemBinary);
                creItemSlots[11] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.Weapon4.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Weapon4.Charges1;
                itemBinary.Charges2 = creFile.Items.Weapon4.Charges2;
                itemBinary.Charges3 = creFile.Items.Weapon4.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Weapon4.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Weapon4.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Weapon4.Filename);
                itemBinary.Flags = creFile.Items.Weapon4.Flags;

                creItems.Add(itemBinary);
                creItemSlots[12] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.Quiver1.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Quiver1.Charges1;
                itemBinary.Charges2 = creFile.Items.Quiver1.Charges2;
                itemBinary.Charges3 = creFile.Items.Quiver1.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Quiver1.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Quiver1.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Quiver1.Filename);
                itemBinary.Flags = creFile.Items.Quiver1.Flags;

                creItems.Add(itemBinary);
                creItemSlots[13] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.Quiver2.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Quiver2.Charges1;
                itemBinary.Charges2 = creFile.Items.Quiver2.Charges2;
                itemBinary.Charges3 = creFile.Items.Quiver2.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Quiver2.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Quiver2.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Quiver2.Filename);
                itemBinary.Flags = creFile.Items.Quiver2.Flags;

                creItems.Add(itemBinary);
                creItemSlots[14] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.Quiver3.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Quiver3.Charges1;
                itemBinary.Charges2 = creFile.Items.Quiver3.Charges2;
                itemBinary.Charges3 = creFile.Items.Quiver3.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Quiver3.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Quiver3.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Quiver3.Filename);
                itemBinary.Flags = creFile.Items.Quiver3.Flags;

                creItems.Add(itemBinary);
                creItemSlots[15] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.Unknown.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Unknown.Charges1;
                itemBinary.Charges2 = creFile.Items.Unknown.Charges2;
                itemBinary.Charges3 = creFile.Items.Unknown.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Unknown.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Unknown.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Unknown.Filename);
                itemBinary.Flags = creFile.Items.Unknown.Flags;

                creItems.Add(itemBinary);
                creItemSlots[16] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.Cloak.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Cloak.Charges1;
                itemBinary.Charges2 = creFile.Items.Cloak.Charges2;
                itemBinary.Charges3 = creFile.Items.Cloak.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Cloak.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Cloak.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Cloak.Filename);
                itemBinary.Flags = creFile.Items.Cloak.Flags;

                creItems.Add(itemBinary);
                creItemSlots[17] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.QuickItem1.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Amulet.Charges1;
                itemBinary.Charges2 = creFile.Items.Amulet.Charges2;
                itemBinary.Charges3 = creFile.Items.Amulet.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Amulet.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Amulet.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Amulet.Filename);
                itemBinary.Flags = creFile.Items.Amulet.Flags;

                creItems.Add(itemBinary);
                creItemSlots[18] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.QuickItem2.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Amulet.Charges1;
                itemBinary.Charges2 = creFile.Items.Amulet.Charges2;
                itemBinary.Charges3 = creFile.Items.Amulet.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Amulet.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Amulet.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Amulet.Filename);
                itemBinary.Flags = creFile.Items.Amulet.Flags;

                creItems.Add(itemBinary);
                creItemSlots[19] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.QuickItem3.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.Amulet.Charges1;
                itemBinary.Charges2 = creFile.Items.Amulet.Charges2;
                itemBinary.Charges3 = creFile.Items.Amulet.Charges3;
                itemBinary.ExpiryHour = creFile.Items.Amulet.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.Amulet.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.Amulet.Filename);
                itemBinary.Flags = creFile.Items.Amulet.Flags;

                creItems.Add(itemBinary);
                creItemSlots[20] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.InventoryItem1.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.InventoryItem1.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem1.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem1.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem1.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem1.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.InventoryItem1.Filename);
                itemBinary.Flags = creFile.Items.InventoryItem1.Flags;

                creItems.Add(itemBinary);
                creItemSlots[21] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.InventoryItem2.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.InventoryItem2.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem2.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem2.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem2.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem2.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.InventoryItem2.Filename);
                itemBinary.Flags = creFile.Items.InventoryItem2.Flags;

                creItems.Add(itemBinary);
                creItemSlots[22] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.InventoryItem3.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.InventoryItem3.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem3.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem3.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem3.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem3.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.InventoryItem3.Filename);
                itemBinary.Flags = creFile.Items.InventoryItem3.Flags;

                creItems.Add(itemBinary);
                creItemSlots[23] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.InventoryItem4.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.InventoryItem4.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem4.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem4.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem4.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem4.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.InventoryItem4.Filename);
                itemBinary.Flags = creFile.Items.InventoryItem4.Flags;

                creItems.Add(itemBinary);
                creItemSlots[24] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.InventoryItem5.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.InventoryItem5.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem5.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem5.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem5.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem5.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.InventoryItem5.Filename);
                itemBinary.Flags = creFile.Items.InventoryItem5.Flags;

                creItems.Add(itemBinary);
                creItemSlots[25] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.InventoryItem6.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.InventoryItem6.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem6.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem6.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem6.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem6.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.InventoryItem6.Filename);
                itemBinary.Flags = creFile.Items.InventoryItem6.Flags;

                creItems.Add(itemBinary);
                creItemSlots[26] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.InventoryItem7.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.InventoryItem7.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem7.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem7.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem7.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem7.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.InventoryItem7.Filename);
                itemBinary.Flags = creFile.Items.InventoryItem7.Flags;

                creItems.Add(itemBinary);
                creItemSlots[27] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.InventoryItem8.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.InventoryItem8.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem8.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem8.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem8.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem8.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.InventoryItem8.Filename);
                itemBinary.Flags = creFile.Items.InventoryItem8.Flags;

                creItems.Add(itemBinary);
                creItemSlots[28] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.InventoryItem9.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.InventoryItem9.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem9.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem9.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem9.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem9.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.InventoryItem9.Filename);
                itemBinary.Flags = creFile.Items.InventoryItem9.Flags;

                creItems.Add(itemBinary);
                creItemSlots[29] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.InventoryItem10.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.InventoryItem10.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem10.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem10.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem10.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem10.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.InventoryItem10.Filename);
                itemBinary.Flags = creFile.Items.InventoryItem10.Flags;

                creItems.Add(itemBinary);
                creItemSlots[30] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.InventoryItem11.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.InventoryItem11.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem11.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem11.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem11.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem11.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.InventoryItem11.Filename);
                itemBinary.Flags = creFile.Items.InventoryItem11.Flags;

                creItems.Add(itemBinary);
                creItemSlots[31] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.InventoryItem12.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.InventoryItem12.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem12.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem12.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem12.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem12.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.InventoryItem12.Filename);
                itemBinary.Flags = creFile.Items.InventoryItem12.Flags;

                creItems.Add(itemBinary);
                creItemSlots[32] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.InventoryItem13.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.InventoryItem13.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem13.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem13.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem13.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem13.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.InventoryItem13.Filename);
                itemBinary.Flags = creFile.Items.InventoryItem13.Flags;

                creItems.Add(itemBinary);
                creItemSlots[33] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.InventoryItem14.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.InventoryItem14.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem14.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem14.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem14.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem14.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.InventoryItem14.Filename);
                itemBinary.Flags = creFile.Items.InventoryItem14.Flags;

                creItems.Add(itemBinary);
                creItemSlots[34] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.InventoryItem15.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.InventoryItem15.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem15.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem15.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem15.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem15.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.InventoryItem15.Filename);
                itemBinary.Flags = creFile.Items.InventoryItem15.Flags;

                creItems.Add(itemBinary);
                creItemSlots[35] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.InventoryItem16.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.InventoryItem16.Charges1;
                itemBinary.Charges2 = creFile.Items.InventoryItem16.Charges2;
                itemBinary.Charges3 = creFile.Items.InventoryItem16.Charges3;
                itemBinary.ExpiryHour = creFile.Items.InventoryItem16.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.InventoryItem16.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.InventoryItem16.Filename);
                itemBinary.Flags = creFile.Items.InventoryItem16.Flags;

                creItems.Add(itemBinary);
                creItemSlots[36] = itemCount;
                itemCount++;
            }

            if (!String.IsNullOrEmpty(creFile.Items.MagicWeapon.Filename))
            {
                var itemBinary = new CreItem();
                itemBinary.Charges1 = creFile.Items.MagicWeapon.Charges1;
                itemBinary.Charges2 = creFile.Items.MagicWeapon.Charges2;
                itemBinary.Charges3 = creFile.Items.MagicWeapon.Charges3;
                itemBinary.ExpiryHour = creFile.Items.MagicWeapon.ExpiryHour;
                itemBinary.ExpiryValue = creFile.Items.MagicWeapon.ExpiryValue;
                itemBinary.Filename = new array8(creFile.Items.MagicWeapon.Filename);
                itemBinary.Flags = creFile.Items.MagicWeapon.Flags;

                creItems.Add(itemBinary);
                creItemSlots[37] = itemCount;
                itemCount++;
            }

            creItemSlots[38] = creFile.Items.SelectedWeapon;
            creItemSlots[39] = creFile.Items.SelectedWeaponAbility;

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel1)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel2)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel3)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel4)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel5)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel6)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel7)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel8)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.MageLevel9)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.PriestLevel1)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.PriestLevel2)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.PriestLevel3)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.PriestLevel4)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.PriestLevel5)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.PriestLevel6)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.PriestLevel7)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            foreach (var memorisedSpell in creFile.MemorisedSpells.Innate)
            {
                var memorisedSpellBinary = new CreMemorisedSpell();
                memorisedSpellBinary.Filename = new array8(memorisedSpell.Filename);
                memorisedSpellBinary.Memorised = Convert.ToInt32(memorisedSpell.IsMemorised);
                creMemorisedSpells.Add(memorisedSpellBinary);
            }

            var creSpellmemorisationInfo = new CreSpellMemorisationInfo();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel1.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel1.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel1.Count);
            creSpellmemorisationInfo.SpellLevel = 0;
            creSpellmemorisationInfo.SpellOffset = 0;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfo();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel2.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel2.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel2.Count);
            creSpellmemorisationInfo.SpellLevel = 1;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfo();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel3.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel3.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel3.Count);
            creSpellmemorisationInfo.SpellLevel = 2;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfo();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel4.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel4.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel4.Count);
            creSpellmemorisationInfo.SpellLevel = 3;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfo();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel5.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel5.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel5.Count);
            creSpellmemorisationInfo.SpellLevel = 4;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfo();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel6.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel6.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel6.Count);
            creSpellmemorisationInfo.SpellLevel = 5;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfo();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel7.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel7.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel7.Count);
            creSpellmemorisationInfo.SpellLevel = 6;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count + creFile.MemorisedSpells.MageLevel6.Count;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfo();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel8.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel8.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel8.Count);
            creSpellmemorisationInfo.SpellLevel = 7;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count + creFile.MemorisedSpells.MageLevel6.Count +
                                                   creFile.MemorisedSpells.MageLevel7.Count;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfo();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel9.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel9.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.MageLevel9.Count);
            creSpellmemorisationInfo.SpellLevel = 8;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count + creFile.MemorisedSpells.MageLevel6.Count +
                                                   creFile.MemorisedSpells.MageLevel7.Count + creFile.MemorisedSpells.MageLevel8.Count;
            creSpellmemorisationInfo.SpellType = 0;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);


            creSpellmemorisationInfo = new CreSpellMemorisationInfo();
            creSpellmemorisationInfo.CurrentSlotCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel1.Count);
            creSpellmemorisationInfo.SlotCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel1.Count);
            creSpellmemorisationInfo.SpellCount = Convert.ToInt16(creFile.MemorisedSpells.PriestLevel1.Count);
            creSpellmemorisationInfo.SpellLevel = 0;
            creSpellmemorisationInfo.SpellOffset = creFile.MemorisedSpells.MageLevel1.Count + creFile.MemorisedSpells.MageLevel2.Count + creFile.MemorisedSpells.MageLevel3.Count +
                                                   creFile.MemorisedSpells.MageLevel4.Count + creFile.MemorisedSpells.MageLevel5.Count + creFile.MemorisedSpells.MageLevel6.Count +
                                                   creFile.MemorisedSpells.MageLevel7.Count + creFile.MemorisedSpells.MageLevel8.Count + creFile.MemorisedSpells.MageLevel9.Count;
            creSpellmemorisationInfo.SpellType = 1;
            creSpellMemorisationInfo.Add(creSpellmemorisationInfo);

            creSpellmemorisationInfo = new CreSpellMemorisationInfo();
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

            creSpellmemorisationInfo = new CreSpellMemorisationInfo();
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

            creSpellmemorisationInfo = new CreSpellMemorisationInfo();
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

            creSpellmemorisationInfo = new CreSpellMemorisationInfo();
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

            creSpellmemorisationInfo = new CreSpellMemorisationInfo();
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

            creSpellmemorisationInfo = new CreSpellMemorisationInfo();
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

            creSpellmemorisationInfo = new CreSpellMemorisationInfo();
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
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 0;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.MageLevel2)
            {
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 1;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.MageLevel3)
            {
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 2;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.MageLevel4)
            {
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 3;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.MageLevel5)
            {
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 4;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.MageLevel6)
            {
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 5;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.MageLevel7)
            {
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 6;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.MageLevel8)
            {
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 7;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.MageLevel9)
            {
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 8;
                knownSpellBinary.SpellType = 1;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.PriestLevel1)
            {
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 0;
                knownSpellBinary.SpellType = 0;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.PriestLevel2)
            {
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 1;
                knownSpellBinary.SpellType = 0;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.PriestLevel3)
            {
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 2;
                knownSpellBinary.SpellType = 0;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.PriestLevel4)
            {
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 3;
                knownSpellBinary.SpellType = 0;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.PriestLevel5)
            {
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 4;
                knownSpellBinary.SpellType = 0;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.PriestLevel6)
            {
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 5;
                knownSpellBinary.SpellType = 0;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.PriestLevel7)
            {
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 6;
                knownSpellBinary.SpellType = 0;
                creKnownSpells.Add(knownSpellBinary);
            }

            foreach (var knownSpell in creFile.KnownSpells.Innate)
            {
                var knownSpellBinary = new CreKnownSpell();
                knownSpellBinary.Filename = new array8(knownSpell.Filename);
                knownSpellBinary.SpellLevel = 0;
                knownSpellBinary.SpellType = 2;
                creKnownSpells.Add(knownSpellBinary);
            }

            CreHeader header = new CreHeader();

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
            header.Flags = creFile.Flags.CanActivateTriggers ? header.Flags | Common.Bit14 : header.Flags;
            header.Flags = creFile.Flags.BeenInParty ? header.Flags | Common.Bit15 : header.Flags;

            header.Flags = creFile.Flags.RestoreItem ? header.Flags | Common.Bit16 : header.Flags;
            header.Flags = creFile.Flags.ClearRestoreItem ? header.Flags | Common.Bit17 : header.Flags;
            // unknown...?
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
            header.Kit = creFile.Kit;
            header.LargePortrait = creFile.LargePortrait;
            header.LeatherColourIndex = creFile.LeatherColourIndex;
            header.Level1 = creFile.Level1;
            header.Level2 = creFile.Level2;
            header.Level3 = creFile.Level3;
            header.LocalActorEnumeration = creFile.LocalActorEnumeration;
            header.LockPicking = creFile.LockPicking;
            header.LongName = creFile.LongName;
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
            header.SaveVsWanrds = creFile.SaveVsWanrds;
            header.ScriptClass = creFile.ScriptClass;
            header.ScriptDefault = creFile.ScriptDefault;
            header.ScriptGeneral = creFile.ScriptGeneral;
            header.ScriptOverride = creFile.ScriptOverride;
            header.ScriptRace = creFile.ScriptRace;
            header.SetTraps = creFile.SetTraps;
            header.Sex = creFile.Sex;
            header.ShortName = creFile.ShortName;
            header.SkinColourIndex = creFile.SkinColourIndex;
            header.SlashingResistance = creFile.SlashingResistance;
            header.SmallPortrait = creFile.SmallPortrait;
            header.Specific = creFile.Specific;
            header.StatusFlags = creFile.StatusFlags;
            header.Stealth = creFile.Stealth;
            header.Strength = creFile.Strength;
            header.StrengthBonus = creFile.StrengthBonus;
            header.Strref1 = creFile.Strref1;
            header.Strref2 = creFile.Strref2;
            header.Strref3 = creFile.Strref3;
            header.Strref4 = creFile.Strref4;
            header.Strref5 = creFile.Strref5;
            header.Strref6 = creFile.Strref6;
            header.Strref7 = creFile.Strref7;
            header.Strref8 = creFile.Strref8;
            header.Strref9 = creFile.Strref9;
            header.Strref10 = creFile.Strref10;
            header.Strref11 = creFile.Strref11;
            header.Strref12 = creFile.Strref12;
            header.Strref13 = creFile.Strref13;
            header.Strref14 = creFile.Strref14;
            header.Strref15 = creFile.Strref15;
            header.Strref16 = creFile.Strref16;
            header.Strref17 = creFile.Strref17;
            header.Strref18 = creFile.Strref18;
            header.Strref19 = creFile.Strref19;
            header.Strref20 = creFile.Strref20;
            header.Strref21 = creFile.Strref21;
            header.Strref22 = creFile.Strref22;
            header.Strref23 = creFile.Strref23;
            header.Strref24 = creFile.Strref24;
            header.Strref25 = creFile.Strref25;
            header.Strref26 = creFile.Strref26;
            header.Strref27 = creFile.Strref27;
            header.Strref28 = creFile.Strref28;
            header.Strref29 = creFile.Strref29;
            header.Strref30 = creFile.Strref30;
            header.Strref31 = creFile.Strref31;
            header.Strref32 = creFile.Strref32;
            header.Strref33 = creFile.Strref33;
            header.Strref34 = creFile.Strref34;
            header.Strref35 = creFile.Strref35;
            header.Strref36 = creFile.Strref36;
            header.Strref37 = creFile.Strref37;
            header.Strref38 = creFile.Strref38;
            header.Strref39 = creFile.Strref39;
            header.Strref40 = creFile.Strref40;
            header.Strref41 = creFile.Strref41;
            header.Strref42 = creFile.Strref42;
            header.Strref43 = creFile.Strref43;
            header.Strref44 = creFile.Strref44;
            header.Strref45 = creFile.Strref45;
            header.Strref46 = creFile.Strref46;
            header.Strref47 = creFile.Strref47;
            header.Strref48 = creFile.Strref48;
            header.Strref49 = creFile.Strref49;
            header.Strref50 = creFile.Strref50;
            header.Strref51 = creFile.Strref51;
            header.Strref52 = creFile.Strref52;
            header.Strref53 = creFile.Strref53;
            header.Strref54 = creFile.Strref54;
            header.Strref55 = creFile.Strref55;
            header.Strref56 = creFile.Strref56;
            header.Strref57 = creFile.Strref57;
            header.Strref58 = creFile.Strref58;
            header.Strref59 = creFile.Strref59;
            header.Strref60 = creFile.Strref60;
            header.Strref61 = creFile.Strref61;
            header.Strref62 = creFile.Strref62;
            header.Strref63 = creFile.Strref63;
            header.Strref64 = creFile.Strref64;
            header.Strref65 = creFile.Strref65;
            header.Strref66 = creFile.Strref66;
            header.Strref67 = creFile.Strref67;
            header.Strref68 = creFile.Strref68;
            header.Strref69 = creFile.Strref69;
            header.Strref70 = creFile.Strref70;
            header.Strref71 = creFile.Strref71;
            header.Strref72 = creFile.Strref72;
            header.Strref73 = creFile.Strref73;
            header.Strref74 = creFile.Strref74;
            header.Strref75 = creFile.Strref75;
            header.Strref76 = creFile.Strref76;
            header.Strref77 = creFile.Strref77;
            header.Strref78 = creFile.Strref78;
            header.Strref79 = creFile.Strref79;
            header.Strref80 = creFile.Strref80;
            header.Strref81 = creFile.Strref81;
            header.Strref82 = creFile.Strref82;
            header.Strref83 = creFile.Strref83;
            header.Strref84 = creFile.Strref84;
            header.Strref85 = creFile.Strref85;
            header.Strref86 = creFile.Strref86;
            header.Strref87 = creFile.Strref87;
            header.Strref88 = creFile.Strref88;
            header.Strref89 = creFile.Strref89;
            header.Strref90 = creFile.Strref90;
            header.Strref91 = creFile.Strref91;
            header.Strref92 = creFile.Strref92;
            header.Strref93 = creFile.Strref93;
            header.Strref94 = creFile.Strref94;
            header.Strref95 = creFile.Strref95;
            header.Strref96 = creFile.Strref96;
            header.Strref97 = creFile.Strref97;
            header.Strref98 = creFile.Strref98;
            header.Strref99 = creFile.Strref99;
            header.Strref100 = creFile.Strref100;
            header.Thac0 = creFile.Thac0;
            header.Tracking = creFile.Tracking;
            header.Unknown = creFile.Unknown;
            header.Unknownx1 = creFile.Unknownx1;
            header.Unknownx2 = creFile.Unknownx2;
            header.Unknownx3 = creFile.Unknownx3;
            header.Unknownx4 = creFile.Unknownx4;
            header.Unknownx5 = creFile.Unknownx5;
            header.UnusedAxe = creFile.UnusedAxe;
            header.UnusedBlunt = creFile.UnusedBlunt;
            header.UnusedBows = creFile.UnusedBows;
            header.UnusedLargeSwords = creFile.UnusedLargeSwords;
            header.UnusedMissile = creFile.UnusedMissile;
            header.UnusedSmallSwords = creFile.UnusedSmallSwords;
            header.UnusedSpears = creFile.UnusedSpears;
            header.UnusedSpiked = creFile.UnusedSpiked;
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

            using (MemoryStream s = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(s))
                {
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

                    using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    {
                        bw.BaseStream.Position = 0;
                        bw.BaseStream.CopyTo(fs);
                        fs.Flush(flushToDisk: true);
                    }
                }
            }
        }
    }
}