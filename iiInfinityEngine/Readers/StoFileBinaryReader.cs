using System.Collections.Generic;
using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Readers.Interfaces;

namespace iiInfinityEngine.Core.Readers
{
    public class StoFileBinaryReader : IStoFileReader
    {
        public TlkFile TlkFile { get; set; }

        public StoFile Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var f = Read(fs);
            f.Filename = Path.GetFileName(filename);
            return f;
        }

        public StoFile Read(Stream s)
        {
            using var br = new BinaryReader(s);
            var stoFile = ParseFile(br);
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            stoFile.OriginalFile = ParseFile(br);
            return stoFile;
        }

        private StoFile ParseFile(BinaryReader br)
        {
            var header = (StoHeaderBinary)Common.ReadStruct(br, typeof(StoHeaderBinary));

            if (header.ftype.ToString() != "STOR")
                return new StoFile();

            var stoSaleItems = new List<StoSaleItemBinary>();
            var stoDrinkItems = new List<StoDrinkItemBinary>();
            var stoCures = new List<StoCureBinary>();
            var stoBuyItems = new List<StoBuyItemBinary>();

            br.BaseStream.Seek(header.SaleOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.SaleCount; i++)
            {
                var stoSaleItem = (StoSaleItemBinary)Common.ReadStruct(br, typeof(StoSaleItemBinary));
                stoSaleItems.Add(stoSaleItem);
            }

            br.BaseStream.Seek(header.DrinksOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.DrinksCount; i++)
            {
                var stoDrinkItem = (StoDrinkItemBinary)Common.ReadStruct(br, typeof(StoDrinkItemBinary));
                stoDrinkItems.Add(stoDrinkItem);
            }

            br.BaseStream.Seek(header.CureOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.CureCount; i++)
            {
                var stoCure = (StoCureBinary)Common.ReadStruct(br, typeof(StoCureBinary));
                stoCures.Add(stoCure);
            }

            br.BaseStream.Seek(header.PurchaseOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.PurchaseCount; i++)
            {
                var stoBuyItem = (StoBuyItemBinary)Common.ReadStruct(br, typeof(StoBuyItemBinary));
                stoBuyItems.Add(stoBuyItem);
            }

            var stoFile = new StoFile();
            stoFile.Flags.CanBuyFromPlayer = (header.Flags & Common.Bit0) != 0;
            stoFile.Flags.AllowedToSell = (header.Flags & Common.Bit1) != 0;
            stoFile.Flags.AllowedToIdentify = (header.Flags & Common.Bit2) != 0;
            stoFile.Flags.AllowedToSteal = (header.Flags & Common.Bit3) != 0;
            stoFile.Flags.AllowedToDonate = (header.Flags & Common.Bit4) != 0;
            stoFile.Flags.AllowedToPurchaseCure = (header.Flags & Common.Bit5) != 0;
            stoFile.Flags.AllowedToPurchaseDrinks = (header.Flags & Common.Bit6) != 0;
            stoFile.Flags.Bit7 = (header.Flags & Common.Bit7) != 0;
            stoFile.Flags.Bit8 = (header.Flags & Common.Bit8) != 0;
            stoFile.Flags.Quality1 = (header.Flags & Common.Bit9) != 0;
            stoFile.Flags.Quality2 = (header.Flags & Common.Bit10) != 0;
            stoFile.Flags.Bit11 = (header.Flags & Common.Bit11) != 0;
            stoFile.Flags.BuyFencedGoods = (header.Flags & Common.Bit12) != 0;
            stoFile.Flags.ReputationDoesNotAffectPrices = (header.Flags & Common.Bit13) != 0;
            stoFile.Flags.ToggleItemRecharge = (header.Flags & Common.Bit14) != 0;
            stoFile.Flags.CanSellCritialItemns = (header.Flags & Common.Bit15) != 0;
            stoFile.StoreType = (StoreType)header.StoreType;
            stoFile.Name = Common.ReadString(header.Name, TlkFile);
            stoFile.SellMarkup = header.SellMarkup;
            stoFile.BuyMarkup = header.BuyMarkup;
            stoFile.DepreciationRate = header.DepreciationRate;
            stoFile.StealFailurePercentage = header.StealFailurePercentage;
            stoFile.Capacity = header.Capacity;
            stoFile.Unknown0024 = header.Unknown24;
            stoFile.Lore = header.Lore;
            stoFile.CostToIdentifyItem = header.CostToIdentifyItem;
            stoFile.RumoursTavern = header.RumoursTavern;
            stoFile.RumoursTemple = header.RumoursTemple;
            stoFile.Rooms.Peasant = (header.Rooms & Common.Bit0) != 0;
            stoFile.Rooms.Merchant = (header.Rooms & Common.Bit1) != 0;
            stoFile.Rooms.Noble = (header.Rooms & Common.Bit2) != 0;
            stoFile.Rooms.Royal = (header.Rooms & Common.Bit3) != 0;
            stoFile.Rooms.Bit4 = (header.Rooms & Common.Bit4) != 0;
            stoFile.Rooms.Bit5 = (header.Rooms & Common.Bit5) != 0;
            stoFile.Rooms.Bit6 = (header.Rooms & Common.Bit6) != 0;
            stoFile.Rooms.Bit7 = (header.Rooms & Common.Bit7) != 0;
            stoFile.Rooms.Bit8 = (header.Rooms & Common.Bit8) != 0;
            stoFile.Rooms.Bit9 = (header.Rooms & Common.Bit9) != 0;
            stoFile.Rooms.Bit10 = (header.Rooms & Common.Bit10) != 0;
            stoFile.Rooms.Bit11 = (header.Rooms & Common.Bit11) != 0;
            stoFile.Rooms.Bit12 = (header.Rooms & Common.Bit12) != 0;
            stoFile.Rooms.Bit13 = (header.Rooms & Common.Bit13) != 0;
            stoFile.Rooms.Bit14 = (header.Rooms & Common.Bit14) != 0;
            stoFile.Rooms.Bit15 = (header.Rooms & Common.Bit15) != 0;
            stoFile.Rooms.Bit16 = (header.Rooms & Common.Bit16) != 0;
            stoFile.Rooms.Bit17 = (header.Rooms & Common.Bit17) != 0;
            stoFile.Rooms.Bit18 = (header.Rooms & Common.Bit18) != 0;
            stoFile.Rooms.Bit19 = (header.Rooms & Common.Bit19) != 0;
            stoFile.Rooms.Bit20 = (header.Rooms & Common.Bit20) != 0;
            stoFile.Rooms.Bit21 = (header.Rooms & Common.Bit21) != 0;
            stoFile.Rooms.Bit22 = (header.Rooms & Common.Bit22) != 0;
            stoFile.Rooms.Bit23 = (header.Rooms & Common.Bit23) != 0;
            stoFile.Rooms.Bit24 = (header.Rooms & Common.Bit24) != 0;
            stoFile.Rooms.Bit25 = (header.Rooms & Common.Bit25) != 0;
            stoFile.Rooms.Bit26 = (header.Rooms & Common.Bit26) != 0;
            stoFile.Rooms.Bit27 = (header.Rooms & Common.Bit27) != 0;
            stoFile.Rooms.Bit28 = (header.Rooms & Common.Bit28) != 0;
            stoFile.Rooms.Bit29 = (header.Rooms & Common.Bit29) != 0;
            stoFile.Rooms.Bit30 = (header.Rooms & Common.Bit30) != 0;
            stoFile.Rooms.Bit31 = (header.Rooms & Common.Bit31) != 0;
            stoFile.PeasantRoomPrice = header.PeasantRoomPrice;
            stoFile.NobleRoomPrice = header.NobleRoomPrice;
            stoFile.MerchantRoomPrice = header.MerchantRoomPrice;
            stoFile.RoyalRoomPrice = header.RoyalRoomPrice;
            stoFile.Unknown78 = header.Unknown78;

            foreach (var saleItem in stoSaleItems)
            {
                var saleItem2 = new StoSaleItem2();
                saleItem2.Amount = saleItem.Amount;
                saleItem2.Filename = saleItem.Filename;
                saleItem2.Flags.Identified = (saleItem.Flags & Common.Bit0) != 0;
                saleItem2.Flags.Unstealable = (saleItem.Flags & Common.Bit1) != 0;
                saleItem2.Flags.Stolen = (saleItem.Flags & Common.Bit2) != 0;
                saleItem2.Flags.Undroppable = (saleItem.Flags & Common.Bit3) != 0;
                saleItem2.Flags.Bit4 = (saleItem.Flags & Common.Bit4) != 0;
                saleItem2.Flags.Bit5 = (saleItem.Flags & Common.Bit5) != 0;
                saleItem2.Flags.Bit6 = (saleItem.Flags & Common.Bit6) != 0;
                saleItem2.Flags.Bit7 = (saleItem.Flags & Common.Bit7) != 0;
                saleItem2.Flags.Bit8 = (saleItem.Flags & Common.Bit8) != 0;
                saleItem2.Flags.Bit9 = (saleItem.Flags & Common.Bit9) != 0;
                saleItem2.Flags.Bit10 = (saleItem.Flags & Common.Bit10) != 0;
                saleItem2.Flags.Bit11 = (saleItem.Flags & Common.Bit11) != 0;
                saleItem2.Flags.Bit12 = (saleItem.Flags & Common.Bit12) != 0;
                saleItem2.Flags.Bit13 = (saleItem.Flags & Common.Bit13) != 0;
                saleItem2.Flags.Bit14 = (saleItem.Flags & Common.Bit14) != 0;
                saleItem2.Flags.Bit15 = (saleItem.Flags & Common.Bit15) != 0;
                saleItem2.Flags.Bit16 = (saleItem.Flags & Common.Bit16) != 0;
                saleItem2.Flags.Bit17 = (saleItem.Flags & Common.Bit17) != 0;
                saleItem2.Flags.Bit18 = (saleItem.Flags & Common.Bit18) != 0;
                saleItem2.Flags.Bit19 = (saleItem.Flags & Common.Bit19) != 0;
                saleItem2.Flags.Bit20 = (saleItem.Flags & Common.Bit20) != 0;
                saleItem2.Flags.Bit21 = (saleItem.Flags & Common.Bit21) != 0;
                saleItem2.Flags.Bit22 = (saleItem.Flags & Common.Bit22) != 0;
                saleItem2.Flags.Bit23 = (saleItem.Flags & Common.Bit23) != 0;
                saleItem2.Flags.Bit24 = (saleItem.Flags & Common.Bit24) != 0;
                saleItem2.Flags.Bit25 = (saleItem.Flags & Common.Bit25) != 0;
                saleItem2.Flags.Bit26 = (saleItem.Flags & Common.Bit26) != 0;
                saleItem2.Flags.Bit27 = (saleItem.Flags & Common.Bit27) != 0;
                saleItem2.Flags.Bit28 = (saleItem.Flags & Common.Bit28) != 0;
                saleItem2.Flags.Bit29 = (saleItem.Flags & Common.Bit29) != 0;
                saleItem2.Flags.Bit30 = (saleItem.Flags & Common.Bit30) != 0;
                saleItem2.Flags.Bit31 = (saleItem.Flags & Common.Bit31) != 0;
                saleItem2.IsInfinite = saleItem.IsInfinite;
                saleItem2.ItemExpirationTime = saleItem.ItemExpirationTime;
                saleItem2.Quantity1 = saleItem.Quantity1;
                saleItem2.Quantity2 = saleItem.Quantity2;
                saleItem2.Quantity3 = saleItem.Quantity3;
                stoFile.ItemsSoldByStore.Add(saleItem2);
            }

            foreach (var drink in stoDrinkItems)
            {
                var drink2 = new StoDrinkItem2();
                drink2.Name = Common.ReadString(drink.Name, TlkFile);
                drink2.Price = drink.Price;
                drink2.Rumours = drink.Rumours;
                drink2.Strength = drink.Strength;
                stoFile.stoDrinkItems.Add(drink2);
            }

            foreach (var cure in stoCures)
            {
                var cure2 = new StoCure2();
                cure2.Filename = cure.Filename;
                cure2.Price = cure.Price;
                stoFile.stoCures.Add(cure2);
            }

            foreach (var buyItem in stoBuyItems)
            {
                stoFile.ItemTypesBoughtByStore.Add((ItemType)buyItem.ItemType);
            }

            stoFile.Checksum = HashGenerator.GenerateKey(stoFile);
            return stoFile;
        }
    }
}