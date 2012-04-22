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
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var f = Read(fs);
                f.Filename = Path.GetFileName(filename);
                return f;
            }
        }

        public StoFile Read(Stream s)
        {
            using (BinaryReader br = new BinaryReader(s))
            {
                var stoFile = ParseFile(br);
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                stoFile.OriginalFile = ParseFile(br);
                return stoFile;
            }
        }

        private StoFile ParseFile(BinaryReader br)
        {
            var header = (StoHeaderBinary)Common.ReadStruct(br, typeof(StoHeaderBinary));

            if (header.ftype.ToString() != "STOR")
                return new StoFile();

            List<StoSaleItemBinary> stoSaleItems = new List<StoSaleItemBinary>();
            List<StoDrinkItemBinary> stoDrinkItems = new List<StoDrinkItemBinary>();
            List<StoCureBinary> stoCures = new List<StoCureBinary>();
            List<StoBuyItemBinary> stoBuyItems = new List<StoBuyItemBinary>();

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


            StoFile stoFile = new StoFile();
            stoFile.Flags.CanBuyFromPlayer = (header.Flags & Common.Bit0) != 0;
            stoFile.Flags.AllowedToSell = (header.Flags & Common.Bit1) != 0;
            stoFile.Flags.AllowedToIdentify = (header.Flags & Common.Bit2) != 0;
            stoFile.Flags.AllowedToSteal = (header.Flags & Common.Bit3) != 0;
            stoFile.Flags.AllowedToDonate = (header.Flags & Common.Bit4) != 0;
            stoFile.Flags.AllowedToPurchaseCure = (header.Flags & Common.Bit5) != 0;
            stoFile.Flags.AllowedToPurchaseDrinks = (header.Flags & Common.Bit6) != 0;
            stoFile.Flags.FlagUnknown1 = (header.Flags & Common.Bit7) != 0;
            stoFile.Flags.FlagUnknown2 = (header.Flags & Common.Bit8) != 0;
            stoFile.Flags.Quality1 = (header.Flags & Common.Bit9) != 0;
            stoFile.Flags.Quality2 = (header.Flags & Common.Bit10) != 0;
            stoFile.Flags.FlagUnknown3 = (header.Flags & Common.Bit11) != 0;
            stoFile.Flags.BuyFencedGoods = (header.Flags & Common.Bit12) != 0;
            stoFile.StoreType = (StoreType)header.StoreType;
            stoFile.Name = Common.ReadString(header.Name, TlkFile);
            stoFile.SellMarkup = header.SellMarkup;
            stoFile.BuyMarkup = header.BuyMarkup;
            stoFile.DepreciationRate = header.DepreciationRate;
            stoFile.StealFailurePercentage = header.StealFailurePercentage;
            stoFile.Capacity = header.Capacity;
            stoFile.Unknown0024 = header.Unknown;
            stoFile.Lore = header.Lore;
            stoFile.CostToIdentifyItem = header.CostToIdentifyItem;
            stoFile.RumoursTavern = header.RumoursTavern;
            stoFile.RumoursTemple = header.RumoursTemple;
            stoFile.PeasantRoom = (header.Rooms & Common.Bit0) != 0;
            stoFile.MerchantRoom = (header.Rooms & Common.Bit1) != 0;
            stoFile.NobleRoom = (header.Rooms & Common.Bit2) != 0;
            stoFile.RoyalRoom = (header.Rooms & Common.Bit3) != 0;
            stoFile.PeasantRoomPrice = header.PeasantRoomPrice;
            stoFile.NobleRoomPrice = header.NobleRoomPrice;
            stoFile.MerchantRoomPrice = header.MerchantRoomPrice;
            stoFile.RoyalRoomPrice = header.RoyalRoomPrice;
            stoFile.Unknown2 = header.Unknown2;
            stoFile.Unknown3 = header.Unknown3;
            stoFile.Unknown4 = header.Unknown4;
            stoFile.Unknown5 = header.Unknown5;
            stoFile.Unknown6 = header.Unknown6;

            foreach (var saleItem in stoSaleItems)
            {
                StoSaleItem2 saleItem2 = new StoSaleItem2();
                saleItem2.Amount = saleItem.Amount;
                saleItem2.Filename = saleItem.Filename;
                saleItem2.Flags.Identified = (saleItem.Flags & Common.Bit0) != 0;
                saleItem2.Flags.Unstealable = (saleItem.Flags & Common.Bit1) != 0;
                saleItem2.Flags.Stolen = (saleItem.Flags & Common.Bit2) != 0;
                saleItem2.Flags.Undroppable = (saleItem.Flags & Common.Bit3) != 0;
                saleItem2.IsInfinite = saleItem.IsInfinite;
                saleItem2.ItemExpirationTime = saleItem.ItemExpirationTime;
                saleItem2.Quantity1 = saleItem.Quantity1;
                saleItem2.Quantity2 = saleItem.Quantity2;
                saleItem2.Quantity3 = saleItem.Quantity3;
                stoFile.ItemsSoldByStore.Add(saleItem2);
            }

            foreach (var drink in stoDrinkItems)
            {
                StoDrinkItem2 drink2 = new StoDrinkItem2();
                drink2.Name = Common.ReadString(drink.Name, TlkFile);
                drink2.Price = drink.Price;
                drink2.Rumours = drink.Rumours;
                drink2.Strength = drink.Strength;
                stoFile.stoDrinkItems.Add(drink2);
            }

            foreach (var cure in stoCures)
            {
                StoCure2 cure2 = new StoCure2();
                cure2.Filename = cure.Filename;
                cure2.Price = cure.Price;
                stoFile.stoCures.Add(cure2);
            }

            foreach (var buyItem in stoBuyItems)
            {
                stoFile.ItemTypesBoughtByStore.Add((ItemType)buyItem.ItemType);
            }

            stoFile.Checksum = MD5HashGenerator.GenerateKey(stoFile);
            return stoFile;
        }
    }
}