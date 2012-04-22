using System;
using System.Collections.Generic;
using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace iiInfinityEngine.Core.Writers
{
    public class StoFileBinaryWriter : IStoFileWriter
    {
        const int HeaderSize = 156;
        const int PurchaseSize = 4;
        const int SaleSize = 28;
        const int CureSize = 12;
        const int DrinkSize = 20;

        public TlkFile TlkFile { get; set; }
        public BackupManager BackupManger { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (!(file is StoFile))
                throw new ArgumentException("File is not a valid creature file");

            var stoFile = file as StoFile;

            if (!(forceSave) && (MD5HashGenerator.GenerateKey(stoFile) == stoFile.Checksum))
                return false;

            List<StoSaleItemBinary> stoSaleItems = new List<StoSaleItemBinary>();
            List<StoDrinkItemBinary> stoDrinkItems = new List<StoDrinkItemBinary>();
            List<StoCureBinary> stoCures = new List<StoCureBinary>();
            List<Int32> stoBuyItems = new List<Int32>();

            foreach (var saleItem in stoFile.ItemsSoldByStore)
            {
                StoSaleItemBinary saleItemBinary = new StoSaleItemBinary();
                saleItemBinary.Amount = saleItem.Amount;
                saleItemBinary.Filename = saleItem.Filename;
                saleItemBinary.Flags = saleItem.Flags.Identified ? saleItemBinary.Flags | Common.Bit0 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Unstealable ? saleItemBinary.Flags | Common.Bit1 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Stolen ? saleItemBinary.Flags | Common.Bit2 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Undroppable ? saleItemBinary.Flags | Common.Bit3 : saleItemBinary.Flags;
                saleItemBinary.IsInfinite = saleItem.IsInfinite;
                saleItemBinary.ItemExpirationTime = saleItem.ItemExpirationTime;
                saleItemBinary.Quantity1 = saleItem.Quantity1;
                saleItemBinary.Quantity2 = saleItem.Quantity2;
                saleItemBinary.Quantity3 = saleItem.Quantity3;
                stoSaleItems.Add(saleItemBinary);
            }

            foreach (var drink in stoFile.stoDrinkItems)
            {
                StoDrinkItemBinary drinkBinary = new StoDrinkItemBinary();
                drinkBinary.Name = Common.WriteString(drink.Name, TlkFile);
                drinkBinary.Price = drink.Price;
                drinkBinary.Rumours = drink.Rumours;
                drinkBinary.Strength = drink.Strength;
                stoDrinkItems.Add(drinkBinary);
            }

            foreach (var cure in stoFile.stoCures)
            {
                StoCureBinary cureBinary = new StoCureBinary();
                cureBinary.Filename = cure.Filename;
                cureBinary.Price = cure.Price;
                stoCures.Add(cureBinary);
            }

            foreach (var buyItem in stoFile.ItemTypesBoughtByStore)
            {
                stoBuyItems.Add((Int32)buyItem);
            }

            StoHeaderBinary header = new StoHeaderBinary();

            header.Flags = stoFile.Flags.CanBuyFromPlayer ? header.Flags | Common.Bit0 : header.Flags;
            header.Flags = stoFile.Flags.AllowedToSell ? header.Flags | Common.Bit1 : header.Flags;
            header.Flags = stoFile.Flags.AllowedToIdentify ? header.Flags | Common.Bit2 : header.Flags;
            header.Flags = stoFile.Flags.AllowedToSteal ? header.Flags | Common.Bit3 : header.Flags;
            header.Flags = stoFile.Flags.AllowedToDonate ? header.Flags | Common.Bit4 : header.Flags;
            header.Flags = stoFile.Flags.AllowedToPurchaseCure ? header.Flags | Common.Bit5 : header.Flags;
            header.Flags = stoFile.Flags.AllowedToPurchaseDrinks ? header.Flags | Common.Bit6 : header.Flags;
            header.Flags = stoFile.Flags.FlagUnknown1 ? header.Flags | Common.Bit7 : header.Flags;
            header.Flags = stoFile.Flags.FlagUnknown2 ? header.Flags | Common.Bit8 : header.Flags;
            header.Flags = stoFile.Flags.Quality1 ? header.Flags | Common.Bit9 : header.Flags;
            header.Flags = stoFile.Flags.Quality2 ? header.Flags | Common.Bit10 : header.Flags;
            header.Flags = stoFile.Flags.FlagUnknown3 ? header.Flags | Common.Bit11 : header.Flags;
            header.Flags = stoFile.Flags.BuyFencedGoods ? header.Flags | Common.Bit12 : header.Flags;

            header.ftype = new array4() { character1 = 'S', character2 = 'T', character3 = 'O', character4 = 'R' };
            header.fversion = new array4() { character1 = 'V', character2 = '1', character3 = '.', character4 = '0' };
            header.Name = Common.WriteString(stoFile.Name, TlkFile);
            header.StoreType = (Int32)stoFile.StoreType;
            header.SellMarkup = stoFile.SellMarkup;
            header.BuyMarkup = stoFile.BuyMarkup;
            header.DepreciationRate = stoFile.DepreciationRate;
            header.StealFailurePercentage = stoFile.StealFailurePercentage;
            header.Capacity = stoFile.Capacity;
            header.Unknown = stoFile.Unknown0024;
            header.PurchaseOffset = HeaderSize;
            header.PurchaseCount = stoBuyItems.Count;
            header.SaleOffset = HeaderSize + (PurchaseSize * stoBuyItems.Count);
            header.SaleCount = stoSaleItems.Count;
            header.Lore = stoFile.Lore;
            header.CostToIdentifyItem = stoFile.CostToIdentifyItem;
            header.RumoursTavern = stoFile.RumoursTavern;
            header.DrinksOffset = HeaderSize + (PurchaseSize * stoBuyItems.Count) + (SaleSize * stoSaleItems.Count);
            header.DrinksCount = stoDrinkItems.Count;
            header.RumoursTemple = stoFile.RumoursTemple;
            header.Rooms = stoFile.PeasantRoom ? header.Rooms | Common.Bit0 : header.Rooms;
            header.Rooms = stoFile.MerchantRoom ? header.Rooms | Common.Bit1 : header.Rooms;
            header.Rooms = stoFile.NobleRoom ? header.Rooms | Common.Bit2 : header.Rooms;
            header.Rooms = stoFile.RoyalRoom ? header.Rooms | Common.Bit3 : header.Rooms;
            header.PeasantRoomPrice = stoFile.PeasantRoomPrice;
            header.NobleRoomPrice = stoFile.NobleRoomPrice;
            header.MerchantRoomPrice = stoFile.MerchantRoomPrice;
            header.RoyalRoomPrice = stoFile.RoyalRoomPrice;
            header.CureOffset = HeaderSize + (PurchaseSize * stoBuyItems.Count) + (SaleSize * stoSaleItems.Count) + (DrinkSize * stoDrinkItems.Count);
            header.CureCount = stoCures.Count;
            header.Unknown2 = stoFile.Unknown2;
            header.Unknown3 = stoFile.Unknown3;
            header.Unknown4 = stoFile.Unknown4;
            header.Unknown5 = stoFile.Unknown5;
            header.Unknown6 = stoFile.Unknown6;

            using (MemoryStream s = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(s))
                {
                    var headerAsBytes = Common.WriteStruct(header);

                    bw.Write(headerAsBytes);

                    foreach (var buyItem in stoBuyItems)
                    {
                        var stoBuyItemAsBytes = Common.WriteStruct(buyItem);
                        bw.Write(stoBuyItemAsBytes);
                    }

                    foreach (var saleItem in stoSaleItems)
                    {
                        var stoSaleItemAsBytes = Common.WriteStruct(saleItem);
                        bw.Write(stoSaleItemAsBytes);
                    }

                    foreach (var drinkItem in stoDrinkItems)
                    {
                        var stoDrinkItemAsBytes = Common.WriteStruct(drinkItem);
                        bw.Write(stoDrinkItemAsBytes);
                    }

                    foreach (var cureItem in stoCures)
                    {
                        var stoCureItemAsBytes = Common.WriteStruct(cureItem);
                        bw.Write(stoCureItemAsBytes);
                    }

                    if (BackupManger != null)
                    {
                        BackupManger.BackupFile(file, file.Filename, file.FileType, this);
                    }

                    using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    {
                        bw.BaseStream.Position = 0;
                        bw.BaseStream.CopyTo(fs);
                        fs.Flush(flushToDisk: true);
                    }
                }
            }
            return true;
        }
    }
}