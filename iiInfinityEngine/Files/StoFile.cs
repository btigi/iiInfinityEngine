using System;
using System.Collections.Generic;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class StoFile : IEFile
    {
        public List<StoSaleItem2> ItemsSoldByStore = new List<StoSaleItem2>();
        public List<StoDrinkItem2> stoDrinkItems = new List<StoDrinkItem2>();
        public List<StoCure2> stoCures = new List<StoCure2>();
        public List<ItemType> ItemTypesBoughtByStore = new List<ItemType>();

        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private IEFileType fileType = IEFileType.Sto;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public StoreType StoreType;
        public IEString Name;
        public StoreFlags Flags;
        public Int32 SellMarkup;
        public Int32 BuyMarkup;
        public Int32 DepreciationRate;
        public Int16 StealFailurePercentage;
        public Int16 Capacity;
        public array8 Unknown0024;
        public Int32 Lore;
        public Int32 CostToIdentifyItem;
        public array8 RumoursTavern;
        public array8 RumoursTemple;
        public bool PeasantRoom;
        public bool MerchantRoom;
        public bool NobleRoom;
        public bool RoyalRoom;
        public Int32 PeasantRoomPrice;
        public Int32 NobleRoomPrice;
        public Int32 MerchantRoomPrice;
        public Int32 RoyalRoomPrice;
        public array8 Unknown2;
        public array8 Unknown3;
        public array8 Unknown4;
        public array8 Unknown5;
        public array4 Unknown6;
    }

    [Serializable]
    public class StoSaleItem2
    {
        public array8 Filename;
        public Int16 ItemExpirationTime;
        public Int16 Quantity1;
        public Int16 Quantity2;
        public Int16 Quantity3;
        public SaleItemFlags Flags;
        public Int32 Amount;
        public Int32 IsInfinite;
    }

    [Serializable]
    public class StoDrinkItem2
    {
        public array8 Rumours;
        public IEString Name;
        public Int32 Price;
        public Int32 Strength;
    }

    [Serializable]
    public class StoCure2
    {
        public array8 Filename;
        public Int32 Price;
    }

    [Serializable]
    public class StoBuyItem2
    {
        public Int32 ItemType;
    }

    [Serializable]
    public struct StoreFlags
    {
        public bool CanBuyFromPlayer { get; set; }
        public bool AllowedToSell { get; set; }
        public bool AllowedToIdentify { get; set; }
        public bool AllowedToSteal { get; set; }
        public bool AllowedToDonate { get; set; }
        public bool AllowedToPurchaseCure { get; set; }
        public bool AllowedToPurchaseDrinks { get; set; }
        public bool FlagUnknown1 { get; set; }
        public bool FlagUnknown2 { get; set; }
        public bool Quality1 { get; set; }
        public bool Quality2 { get; set; }
        public bool FlagUnknown3 { get; set; }
        public bool BuyFencedGoods { get; set; }
    }

    [Serializable]
    public struct SaleItemFlags
    {
        public bool Identified { get; set; }
        public bool Unstealable { get; set; }
        public bool Stolen { get; set; }
        public bool Undroppable { get; set; }
    }

    public enum StoreType
    {
        Store = 0,
        Tavern = 1,
        Inn = 2,
        Temple = 3,
        Container = 5
    }
}