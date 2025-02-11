using System;
using System.Collections.Generic;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class StoFile : IEFile
    {
        public List<StoSaleItem2> ItemsSoldByStore = [];
        public List<StoDrinkItem2> stoDrinkItems = [];
        public List<StoCure2> stoCures = [];
        public List<ItemType> ItemTypesBoughtByStore = [];

        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private readonly IEFileType fileType = IEFileType.Sto;
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
        public RoomFlags Rooms;
        public Int32 PeasantRoomPrice;
        public Int32 NobleRoomPrice;
        public Int32 MerchantRoomPrice;
        public Int32 RoyalRoomPrice;
        public array36 Unknown78;
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
        public bool Bit7 { get; set; }
        public bool Bit8 { get; set; }
        public bool Quality1 { get; set; }
        public bool Quality2 { get; set; }
        public bool Bit11 { get; set; }
        public bool BuyFencedGoods { get; set; }
        public bool ReputationDoesNotAffectPrices { get; set; }
        public bool ToggleItemRecharge { get; set; }
        public bool CanSellCritialItemns { get; set; }
    }

    [Serializable]
    public struct RoomFlags
    {
        public bool Peasant { get; set; }
        public bool Merchant { get; set; }
        public bool Noble { get; set; }
        public bool Royal { get; set; }
        public bool Bit4 { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
        public bool Bit8 { get; set; }
        public bool Bit9 { get; set; }
        public bool Bit10 { get; set; }
        public bool Bit11 { get; set; }
        public bool Bit12 { get; set; }
        public bool Bit13 { get; set; }
        public bool Bit14 { get; set; }
        public bool Bit15 { get; set; }
        public bool Bit16 { get; set; }
        public bool Bit17 { get; set; }
        public bool Bit18 { get; set; }
        public bool Bit19 { get; set; }
        public bool Bit20 { get; set; }
        public bool Bit21 { get; set; }
        public bool Bit22 { get; set; }
        public bool Bit23 { get; set; }
        public bool Bit24 { get; set; }
        public bool Bit25 { get; set; }
        public bool Bit26 { get; set; }
        public bool Bit27 { get; set; }
        public bool Bit28 { get; set; }
        public bool Bit29 { get; set; }
        public bool Bit30 { get; set; }
        public bool Bit31 { get; set; }
    }

    [Serializable]
    public struct SaleItemFlags
    {
        public bool Identified { get; set; }
        public bool Unstealable { get; set; }
        public bool Stolen { get; set; }
        public bool Undroppable { get; set; }
        public bool Bit4 { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
        public bool Bit8 { get; set; }
        public bool Bit9 { get; set; }
        public bool Bit10 { get; set; }
        public bool Bit11 { get; set; }
        public bool Bit12 { get; set; }
        public bool Bit13 { get; set; }
        public bool Bit14 { get; set; }
        public bool Bit15 { get; set; }
        public bool Bit16 { get; set; }
        public bool Bit17 { get; set; }
        public bool Bit18 { get; set; }
        public bool Bit19 { get; set; }
        public bool Bit20 { get; set; }
        public bool Bit21 { get; set; }
        public bool Bit22 { get; set; }
        public bool Bit23 { get; set; }
        public bool Bit24 { get; set; }
        public bool Bit25 { get; set; }
        public bool Bit26 { get; set; }
        public bool Bit27 { get; set; }
        public bool Bit28 { get; set; }
        public bool Bit29 { get; set; }
        public bool Bit30 { get; set; }
        public bool Bit31 { get; set; }
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