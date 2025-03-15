using System;
using System.Runtime.InteropServices;

namespace ii.InfinityEngine.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct GamBinaryHeader
    {
        public array4 ftype;
        public array4 fversion;
        public int GameTime;
        public Int16 SelectedFormation;
        public Int16 FormationButton1;
        public Int16 FormationButton2;
        public Int16 FormationButton3;
        public Int16 FormationButton4;
        public Int16 FormationButton5;
        public int Gold;
        public Int16 UseActiveAreaFromPartyMember;
        public Int16 Weather;
        public int NpcStructPartyOffset;
        public int NpcStructPartyCount;
        public int PartyInventoryOffset;
        public int PartyInventoryCount;
        public int NpcStructNonPartyOffset;
        public int NpcStructNonPartyCount;
        public int GlobalVariableOffset;
        public int GlobalVariableCount;
        public array8 MainArea;
        public int CurrentLink;
        public int JournalEntryCount;
        public int JournalEntryOffset;
        public int PartyReputation;
        public array8 CurrentArea;
        public int GuiFlags;
        public int LoadingProgress;
        public int FamiliarInfoOffset;
        public int StoredLocationOffset;
        public int StoredLocationCount;
        public int GameTimeReal;
        public int PocketPlaneLocationOffset;
        public int PocketPlaneLocationCount;
        public int ZoomPercentage;
        public array8 RandomEncounterArea;
        public array8 CurrentWorldmap;
        public array8 CurrentCampaign;
        public int FamiliarOwner;
        public array20 RandomEncountryEntry;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct GamNpcStructBinary
    {
        public Int16 Selection;
        public Int16 PartyOrder;
        public int OffsetToCre;
        public int SizeOfCre;
        public array8 CreResref;
        public int Orientation;
        public array8 Area;
        public Int16 XCoordinate;
        public Int16 YCoordinate;
        public Int16 XCoordinateView;
        public Int16 YCoordinateView;
        public Int16 ModalAction;
        public Int16 Happiness;
        public int NumTimesInteractedNpcCount1;
        public int NumTimesInteractedNpcCount2;
        public int NumTimesInteractedNpcCount3;
        public int NumTimesInteractedNpcCount4;
        public int NumTimesInteractedNpcCount5;
        public int NumTimesInteractedNpcCount6;
        public int NumTimesInteractedNpcCount7;
        public int NumTimesInteractedNpcCount8;
        public int NumTimesInteractedNpcCount9;
        public int NumTimesInteractedNpcCount10;
        public int NumTimesInteractedNpcCount11;
        public int NumTimesInteractedNpcCount12;
        public int NumTimesInteractedNpcCount13;
        public int NumTimesInteractedNpcCount14;
        public int NumTimesInteractedNpcCount15;
        public int NumTimesInteractedNpcCount16;
        public int NumTimesInteractedNpcCount17;
        public int NumTimesInteractedNpcCount18;
        public int NumTimesInteractedNpcCount19;
        public int NumTimesInteractedNpcCount20;
        public int NumTimesInteractedNpcCount21;
        public int NumTimesInteractedNpcCount22;
        public int NumTimesInteractedNpcCount23;
        public int NumTimesInteractedNpcCount24;
        public Int16 QuickWeaponSlot1;
        public Int16 QuickWeaponSlot2;
        public Int16 QuickWeaponSlot3;
        public Int16 QuickWeaponSlot4;
        public Int16 QuickWeaponSlot1Ability;
        public Int16 QuickWeaponSlot2Ability;
        public Int16 QuickWeaponSlot3Ability;
        public Int16 QuickWeaponSlot4Ability;
        public array8 QuickSpell1Resource;
        public array8 QuickSpell2Resource;
        public array8 QuickSpell3Resource;
        public Int16 QuickItemSlot1;
        public Int16 QuickItemSlot2;
        public Int16 QuickItemSlot3;
        public Int16 QuickItemSlot1Ability;
        public Int16 QuickItemSlot2Ability;
        public Int16 QuickItemSlot3Ability;
        public array32 Name;
        public int TalkCount;
        public GamCharacterBinary CharacterStats;
        public array8 VoiceSet;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct GamCharacterBinary
    {
        public int MostPowerfulVanquishedName;
        public int MostPowerfulVanquishedXP;
        public int TimeInParty;
        public int TimeJoinedParty;
        public byte PartyMember;
        public Int16 Unused11;
        public char FirstLetterofCreResref;
        public int KillsXPGainedChapter;
        public int KillsNumberChapter;
        public int KillsXPGainedGame;
        public int KillsNumberGame;
        public array8 FavouriteSpell1;
        public array8 FavouriteSpell2;
        public array8 FavouriteSpell3;
        public array8 FavouriteSpell4;
        public Int16 FavouriteSpell1Count;
        public Int16 FavouriteSpell2Count;
        public Int16 FavouriteSpell3Count;
        public Int16 FavouriteSpell4Count;
        public array8 FavouriteWeapon1;
        public array8 FavouriteWeapon2;
        public array8 FavouriteWeapon3;
        public array8 FavouriteWeapon4;
        public Int16 FavouriteWeapon1Time;
        public Int16 FavouriteWeapon2Time;
        public Int16 FavouriteWeapon3Time;
        public Int16 FavouriteWeapon4Time;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct GamVariableBinary
    {
        public array32 Name { get; set; }
        public Int16 Type { get; set; }
        public Int16 ResourceType { get; set; }
        public Int32 ValueDword { get; set; }
        public Int32 ValueInt { get; set; }
        public double ValueDouble { get; set; }
        public array32 ScriptName { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct GamJournalBinary
    {
        public int JournalText { get; set; }
        public int Time { get; set; }
        public byte Chapter { get; set; }
        public byte ReadBy { get; set; }
        public byte JournalSection { get; set; }
        public byte LocationFlag { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct GamFamiliarInfoBinary
    {
        public array8 LawfulGoodFamiliar;
        public array8 LawfulNeutralFamiliar;
        public array8 LawfulEvilFamiliar;
        public array8 NeutralGoodFamiliar;
        public array8 NeutralFamiliar;
        public array8 NeutralEvilFamiliar;
        public array8 ChaoticGoodFamiliar;
        public array8 ChaoticNeutralFamiliar;
        public array8 ChaoticEvilFamiliar;
        public int OffsetToFamiliarResources;
        public int LGLevel1FamiliarCount;
        public int LGLevel2FamiliarCount;
        public int LGLevel3FamiliarCount;
        public int LGLevel4FamiliarCount;
        public int LGLevel5FamiliarCount;
        public int LGLevel6FamiliarCount;
        public int LGLevel7FamiliarCount;
        public int LGLevel8FamiliarCount;
        public int LGLevel9FamiliarCount;
        public int LNLevel1FamiliarCount;
        public int LNLevel2FamiliarCount;
        public int LNLevel3FamiliarCount;
        public int LNLevel4FamiliarCount;
        public int LNLevel5FamiliarCount;
        public int LNLevel6FamiliarCount;
        public int LNLevel7FamiliarCount;
        public int LNLevel8FamiliarCount;
        public int LNLevel9FamiliarCount;
        public int CGLevel1FamiliarCount;
        public int CGLevel2FamiliarCount;
        public int CGLevel3FamiliarCount;
        public int CGLevel4FamiliarCount;
        public int CGLevel5FamiliarCount;
        public int CGLevel6FamiliarCount;
        public int CGLevel7FamiliarCount;
        public int CGLevel8FamiliarCount;
        public int CGLevel9FamiliarCount;
        public int NGLevel1FamiliarCount;
        public int NGLevel2FamiliarCount;
        public int NGLevel3FamiliarCount;
        public int NGLevel4FamiliarCount;
        public int NGLevel5FamiliarCount;
        public int NGLevel6FamiliarCount;
        public int NGLevel7FamiliarCount;
        public int NGLevel8FamiliarCount;
        public int NGLevel9FamiliarCount;
        public int TNLevel1FamiliarCount;
        public int TNLevel2FamiliarCount;
        public int TNLevel3FamiliarCount;
        public int TNLevel4FamiliarCount;
        public int TNLevel5FamiliarCount;
        public int TNLevel6FamiliarCount;
        public int TNLevel7FamiliarCount;
        public int TNLevel8FamiliarCount;
        public int TNLevel9FamiliarCount;
        public int NELevel1FamiliarCount;
        public int NELevel2FamiliarCount;
        public int NELevel3FamiliarCount;
        public int NELevel4FamiliarCount;
        public int NELevel5FamiliarCount;
        public int NELevel6FamiliarCount;
        public int NELevel7FamiliarCount;
        public int NELevel8FamiliarCount;
        public int NELevel9FamiliarCount;
        public int LELevel1FamiliarCount;
        public int LELevel2FamiliarCount;
        public int LELevel3FamiliarCount;
        public int LELevel4FamiliarCount;
        public int LELevel5FamiliarCount;
        public int LELevel6FamiliarCount;
        public int LELevel7FamiliarCount;
        public int LELevel8FamiliarCount;
        public int LELevel9FamiliarCount;
        public int CNLevel1FamiliarCount;
        public int CNLevel2FamiliarCount;
        public int CNLevel3FamiliarCount;
        public int CNLevel4FamiliarCount;
        public int CNLevel5FamiliarCount;
        public int CNLevel6FamiliarCount;
        public int CNLevel7FamiliarCount;
        public int CNLevel8FamiliarCount;
        public int CNLevel9FamiliarCount;
        public int CELevel1FamiliarCount;
        public int CELevel2FamiliarCount;
        public int CELevel3FamiliarCount;
        public int CELevel4FamiliarCount;
        public int CELevel5FamiliarCount;
        public int CELevel6FamiliarCount;
        public int CELevel7FamiliarCount;
        public int CELevel8FamiliarCount;
        public int CELevel9FamiliarCount;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct GamStoredLocationBinary
    {
        public array8 Area;
        public Int16 XCoordinate;
        public Int16 YCoordinate;
    }
}