using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct StoHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public Int32 StoreType;
        public Int32 Name;
        public Int32 Flags;
        public Int32 SellMarkup;
        public Int32 BuyMarkup;
        public Int32 DepreciationRate;
        public Int16 StealFailurePercentage;
        public Int16 Capacity;
        public array8 Unknown;
        public Int32 PurchaseOffset;
        public Int32 PurchaseCount;
        public Int32 SaleOffset;
        public Int32 SaleCount;
        public Int32 Lore;
        public Int32 CostToIdentifyItem;
        public array8 RumoursTavern;
        public Int32 DrinksOffset;
        public Int32 DrinksCount;
        public array8 RumoursTemple;
        public Int32 Rooms;
        public Int32 PeasantRoomPrice;
        public Int32 NobleRoomPrice;
        public Int32 MerchantRoomPrice;
        public Int32 RoyalRoomPrice;
        public Int32 CureOffset;
        public Int32 CureCount;
        public array8 Unknown2;
        public array8 Unknown3;
        public array8 Unknown4;
        public array8 Unknown5;
        public array4 Unknown6;
    }

    struct StoSaleItemBinary
    {
        public array8 Filename;
        public Int16 ItemExpirationTime;
        public Int16 Quantity1;
        public Int16 Quantity2;
        public Int16 Quantity3;
        public Int32 Flags;
        public Int32 Amount;
        public Int32 IsInfinite;
    }

    struct StoDrinkItemBinary
    {
        public array8 Rumours;
        public Int32 Name;
        public Int32 Price;
        public Int32 Strength;
    }

    struct StoCureBinary
    {
        public array8 Filename;
        public Int32 Price;
    }

    struct StoBuyItemBinary
    {
        // Quick hack to avoid a compiler warning about un-assigned variables
        public Int32 ItemType { get; set; }
    }
}