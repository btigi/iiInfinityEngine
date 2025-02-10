using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

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

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not StoFile)
                throw new ArgumentException("File is not a valid creature file");

            var stoFile = file as StoFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(stoFile) == stoFile.Checksum))
                return false;

            var stoSaleItems = new List<StoSaleItemBinary>();
            var stoDrinkItems = new List<StoDrinkItemBinary>();
            var stoCures = new List<StoCureBinary>();
            var stoBuyItems = new List<Int32>();

            foreach (var saleItem in stoFile.ItemsSoldByStore)
            {
                var saleItemBinary = new StoSaleItemBinary();
                saleItemBinary.Amount = saleItem.Amount;
                saleItemBinary.Filename = saleItem.Filename;
                saleItemBinary.Flags = saleItem.Flags.Identified ? saleItemBinary.Flags | Common.Bit0 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Unstealable ? saleItemBinary.Flags | Common.Bit1 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Stolen ? saleItemBinary.Flags | Common.Bit2 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Undroppable ? saleItemBinary.Flags | Common.Bit3 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit4 ? saleItemBinary.Flags | Common.Bit4 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit5 ? saleItemBinary.Flags | Common.Bit5 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit6 ? saleItemBinary.Flags | Common.Bit6 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit7 ? saleItemBinary.Flags | Common.Bit7 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit8 ? saleItemBinary.Flags | Common.Bit8 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit9 ? saleItemBinary.Flags | Common.Bit9 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit10 ? saleItemBinary.Flags | Common.Bit10 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit11 ? saleItemBinary.Flags | Common.Bit11 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit12 ? saleItemBinary.Flags | Common.Bit12 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit13 ? saleItemBinary.Flags | Common.Bit13 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit14 ? saleItemBinary.Flags | Common.Bit14 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit15 ? saleItemBinary.Flags | Common.Bit15 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit16 ? saleItemBinary.Flags | Common.Bit16 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit17 ? saleItemBinary.Flags | Common.Bit17 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit18 ? saleItemBinary.Flags | Common.Bit18 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit19 ? saleItemBinary.Flags | Common.Bit19 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit20 ? saleItemBinary.Flags | Common.Bit20 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit21 ? saleItemBinary.Flags | Common.Bit21 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit22 ? saleItemBinary.Flags | Common.Bit22 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit23 ? saleItemBinary.Flags | Common.Bit23 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit24 ? saleItemBinary.Flags | Common.Bit24 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit25 ? saleItemBinary.Flags | Common.Bit25 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit26 ? saleItemBinary.Flags | Common.Bit26 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit27 ? saleItemBinary.Flags | Common.Bit27 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit28 ? saleItemBinary.Flags | Common.Bit28 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit29 ? saleItemBinary.Flags | Common.Bit29 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit30 ? saleItemBinary.Flags | Common.Bit30 : saleItemBinary.Flags;
                saleItemBinary.Flags = saleItem.Flags.Bit31 ? saleItemBinary.Flags | Common.Bit31 : saleItemBinary.Flags;
                saleItemBinary.IsInfinite = saleItem.IsInfinite;
                saleItemBinary.ItemExpirationTime = saleItem.ItemExpirationTime;
                saleItemBinary.Quantity1 = saleItem.Quantity1;
                saleItemBinary.Quantity2 = saleItem.Quantity2;
                saleItemBinary.Quantity3 = saleItem.Quantity3;
                stoSaleItems.Add(saleItemBinary);
            }

            foreach (var drink in stoFile.stoDrinkItems)
            {
                var drinkBinary = new StoDrinkItemBinary();
                drinkBinary.Name = Common.WriteString(drink.Name, TlkFile);
                drinkBinary.Price = drink.Price;
                drinkBinary.Rumours = drink.Rumours;
                drinkBinary.Strength = drink.Strength;
                stoDrinkItems.Add(drinkBinary);
            }

            foreach (var cure in stoFile.stoCures)
            {
                var cureBinary = new StoCureBinary();
                cureBinary.Filename = cure.Filename;
                cureBinary.Price = cure.Price;
                stoCures.Add(cureBinary);
            }

            foreach (var buyItem in stoFile.ItemTypesBoughtByStore)
            {
                stoBuyItems.Add((Int32)buyItem);
            }

            var header = new StoHeaderBinary();
            header.Flags = stoFile.Flags.CanBuyFromPlayer ? header.Flags | Common.Bit0 : header.Flags;
            header.Flags = stoFile.Flags.AllowedToSell ? header.Flags | Common.Bit1 : header.Flags;
            header.Flags = stoFile.Flags.AllowedToIdentify ? header.Flags | Common.Bit2 : header.Flags;
            header.Flags = stoFile.Flags.AllowedToSteal ? header.Flags | Common.Bit3 : header.Flags;
            header.Flags = stoFile.Flags.AllowedToDonate ? header.Flags | Common.Bit4 : header.Flags;
            header.Flags = stoFile.Flags.AllowedToPurchaseCure ? header.Flags | Common.Bit5 : header.Flags;
            header.Flags = stoFile.Flags.AllowedToPurchaseDrinks ? header.Flags | Common.Bit6 : header.Flags;
            header.Flags = stoFile.Flags.Bit7 ? header.Flags | Common.Bit7 : header.Flags;
            header.Flags = stoFile.Flags.Bit8 ? header.Flags | Common.Bit8 : header.Flags;
            header.Flags = stoFile.Flags.Quality1 ? header.Flags | Common.Bit9 : header.Flags;
            header.Flags = stoFile.Flags.Quality2 ? header.Flags | Common.Bit10 : header.Flags;
            header.Flags = stoFile.Flags.Bit11 ? header.Flags | Common.Bit11 : header.Flags;
            header.Flags = stoFile.Flags.BuyFencedGoods ? header.Flags | Common.Bit12 : header.Flags;
            header.Flags = stoFile.Flags.ReputationDoesNotAffectPrices ? header.Flags | Common.Bit13 : header.Flags;
            header.Flags = stoFile.Flags.ToggleItemRecharge ? header.Flags | Common.Bit14 : header.Flags;
            header.Flags = stoFile.Flags.CanSellCritialItemns ? header.Flags | Common.Bit15 : header.Flags;
            header.ftype = new array4() { character1 = 'S', character2 = 'T', character3 = 'O', character4 = 'R' };
            header.fversion = new array4() { character1 = 'V', character2 = '1', character3 = '.', character4 = '0' };
            header.Name = Common.WriteString(stoFile.Name, TlkFile);
            header.StoreType = (Int32)stoFile.StoreType;
            header.SellMarkup = stoFile.SellMarkup;
            header.BuyMarkup = stoFile.BuyMarkup;
            header.DepreciationRate = stoFile.DepreciationRate;
            header.StealFailurePercentage = stoFile.StealFailurePercentage;
            header.Capacity = stoFile.Capacity;
            header.Unknown24 = stoFile.Unknown0024;
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
            header.Rooms = stoFile.Rooms.Peasant ? header.Rooms | Common.Bit0 : header.Rooms;
            header.Rooms = stoFile.Rooms.Merchant ? header.Rooms | Common.Bit1 : header.Rooms;
            header.Rooms = stoFile.Rooms.Noble ? header.Rooms | Common.Bit2 : header.Rooms;
            header.Rooms = stoFile.Rooms.Royal ? header.Rooms | Common.Bit3 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit4 ? header.Rooms | Common.Bit4 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit5 ? header.Rooms | Common.Bit5 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit6 ? header.Rooms | Common.Bit6 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit7 ? header.Rooms | Common.Bit7 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit8 ? header.Rooms | Common.Bit8 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit9 ? header.Rooms | Common.Bit9 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit10 ? header.Rooms | Common.Bit10 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit11 ? header.Rooms | Common.Bit11 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit12 ? header.Rooms | Common.Bit12 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit13 ? header.Rooms | Common.Bit13 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit14 ? header.Rooms | Common.Bit14 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit15 ? header.Rooms | Common.Bit15 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit16 ? header.Rooms | Common.Bit16 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit17 ? header.Rooms | Common.Bit17 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit18 ? header.Rooms | Common.Bit18 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit19 ? header.Rooms | Common.Bit19 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit20 ? header.Rooms | Common.Bit20 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit21 ? header.Rooms | Common.Bit21 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit22 ? header.Rooms | Common.Bit22 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit23 ? header.Rooms | Common.Bit23 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit24 ? header.Rooms | Common.Bit24 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit25 ? header.Rooms | Common.Bit25 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit26 ? header.Rooms | Common.Bit26 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit27 ? header.Rooms | Common.Bit27 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit28 ? header.Rooms | Common.Bit28 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit29 ? header.Rooms | Common.Bit29 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit30 ? header.Rooms | Common.Bit30 : header.Rooms;
            header.Rooms = stoFile.Rooms.Bit31 ? header.Rooms | Common.Bit31 : header.Rooms;
            header.PeasantRoomPrice = stoFile.PeasantRoomPrice;
            header.NobleRoomPrice = stoFile.NobleRoomPrice;
            header.MerchantRoomPrice = stoFile.MerchantRoomPrice;
            header.RoyalRoomPrice = stoFile.RoyalRoomPrice;
            header.CureOffset = HeaderSize + (PurchaseSize * stoBuyItems.Count) + (SaleSize * stoSaleItems.Count) + (DrinkSize * stoDrinkItems.Count);
            header.CureCount = stoCures.Count;
            header.Unknown78 = stoFile.Unknown78;

            using var s = new MemoryStream();
            using var bw = new BinaryWriter(s);
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

            BackupManger?.BackupFile(file, file.Filename, file.FileType, this);

            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            bw.BaseStream.Position = 0;
            bw.BaseStream.CopyTo(fs);
            fs.Flush(flushToDisk: true);
            return true;
        }
    }
}