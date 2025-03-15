using System;
using System.Collections.Generic;

namespace ii.InfinityEngine.Files
{
    [Serializable]
    public class GamFile : IEFile
    {
        public List<GamNpcStruct> PartyMembers = [];
        public List<GamNpcStruct> NonPartyMembers = [];
        public List<GamVariable> Variables = [];
        public List<GamPartyInventory> PartyInventories = [];
        public List<GamJournal> JournalEntries = [];
        public List<GamStoredLocation> StoredLocations = [];
        public List<GamStoredLocation> PocketPlaneLocations = [];
        public GamFamiliarInfo FamiliarInfo = new();

        public List<array8> LGLevel1Familiar = new();
        public List<array8> LGLevel2Familiar = new();
        public List<array8> LGLevel3Familiar = new();
        public List<array8> LGLevel4Familiar = new();
        public List<array8> LGLevel5Familiar = new();
        public List<array8> LGLevel6Familiar = new();
        public List<array8> LGLevel7Familiar = new();
        public List<array8> LGLevel8Familiar = new();
        public List<array8> LGLevel9Familiar = new();

        public List<array8> LNLevel1Familiar = new();
        public List<array8> LNLevel2Familiar = new();
        public List<array8> LNLevel3Familiar = new();
        public List<array8> LNLevel4Familiar = new();
        public List<array8> LNLevel5Familiar = new();
        public List<array8> LNLevel6Familiar = new();
        public List<array8> LNLevel7Familiar = new();
        public List<array8> LNLevel8Familiar = new();
        public List<array8> LNLevel9Familiar = new();

        public List<array8> CGLevel1Familiar = new();
        public List<array8> CGLevel2Familiar = new();
        public List<array8> CGLevel3Familiar = new();
        public List<array8> CGLevel4Familiar = new();
        public List<array8> CGLevel5Familiar = new();
        public List<array8> CGLevel6Familiar = new();
        public List<array8> CGLevel7Familiar = new();
        public List<array8> CGLevel8Familiar = new();
        public List<array8> CGLevel9Familiar = new();

        public List<array8> NGLevel1Familiar = new();
        public List<array8> NGLevel2Familiar = new();
        public List<array8> NGLevel3Familiar = new();
        public List<array8> NGLevel4Familiar = new();
        public List<array8> NGLevel5Familiar = new();
        public List<array8> NGLevel6Familiar = new();
        public List<array8> NGLevel7Familiar = new();
        public List<array8> NGLevel8Familiar = new();
        public List<array8> NGLevel9Familiar = new();

        public List<array8> TNLevel1Familiar = new();
        public List<array8> TNLevel2Familiar = new();
        public List<array8> TNLevel3Familiar = new();
        public List<array8> TNLevel4Familiar = new();
        public List<array8> TNLevel5Familiar = new();
        public List<array8> TNLevel6Familiar = new();
        public List<array8> TNLevel7Familiar = new();
        public List<array8> TNLevel8Familiar = new();
        public List<array8> TNLevel9Familiar = new();

        public List<array8> NELevel1Familiar = new();
        public List<array8> NELevel2Familiar = new();
        public List<array8> NELevel3Familiar = new();
        public List<array8> NELevel4Familiar = new();
        public List<array8> NELevel5Familiar = new();
        public List<array8> NELevel6Familiar = new();
        public List<array8> NELevel7Familiar = new();
        public List<array8> NELevel8Familiar = new();
        public List<array8> NELevel9Familiar = new();

        public List<array8> LELevel1Familiar = new();
        public List<array8> LELevel2Familiar = new();
        public List<array8> LELevel3Familiar = new();
        public List<array8> LELevel4Familiar = new();
        public List<array8> LELevel5Familiar = new();
        public List<array8> LELevel6Familiar = new();
        public List<array8> LELevel7Familiar = new();
        public List<array8> LELevel8Familiar = new();
        public List<array8> LELevel9Familiar = new();

        public List<array8> CNLevel1Familiar = new();
        public List<array8> CNLevel2Familiar = new();
        public List<array8> CNLevel3Familiar = new();
        public List<array8> CNLevel4Familiar = new();
        public List<array8> CNLevel5Familiar = new();
        public List<array8> CNLevel6Familiar = new();
        public List<array8> CNLevel7Familiar = new();
        public List<array8> CNLevel8Familiar = new();
        public List<array8> CNLevel9Familiar = new();

        public List<array8> CELevel1Familiar = new();
        public List<array8> CELevel2Familiar = new();
        public List<array8> CELevel3Familiar = new();
        public List<array8> CELevel4Familiar = new();
        public List<array8> CELevel5Familiar = new();
        public List<array8> CELevel6Familiar = new();
        public List<array8> CELevel7Familiar = new();
        public List<array8> CELevel8Familiar = new();
        public List<array8> CELevel9Familiar = new();

        public GamFile()
        {
            Weather = new();
            GuiFlags = new();
        }

        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private readonly IEFileType fileType = IEFileType.Gam;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public int GameTime { get; set; }
        public Int16 SelectedFormation { get; set; }
        public Int16 FormationButton1 { get; set; }
        public Int16 FormationButton2 { get; set; }
        public Int16 FormationButton3 { get; set; }
        public Int16 FormationButton4 { get; set; }
        public Int16 FormationButton5 { get; set; }
        public int Gold { get; set; }
        public Int16 UseActiveAreaFromPartyMember { get; set; }
        public WeatherFlags Weather { get; set; }
        public array8 MainArea { get; set; }
        public int CurrentLink { get; set; }
        public int PartyReputation { get; set; }
        public array8 CurrentArea { get; set; }
        public GuiFlags GuiFlags { get; set; }
        public LoadingProgress LoadingProgress { get; set; }
        public int GameTimeReal { get; set; }
        public int ZoomPercentage { get; set; }
        public array8 RandomEncounterArea { get; set; }
        public array8 CurrentWorldmap { get; set; }
        public array8 CurrentCampaign { get; set; }
        public int FamiliarOwner { get; set; }
        public array20 RandomEncountryEntry { get; set; }
    }

    [Serializable]
    public class GamNpcStruct
    {
        public CreFile CreFile { get; set; }
        public Int16 Selection { get; set; }
        public Int16 PartyOrder { get; set; }
        public array8 CreResref { get; set; }
        public int Orientation { get; set; }
        public array8 Area { get; set; }
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public Int16 XCoordinateView { get; set; }
        public Int16 YCoordinateView { get; set; }
        public Int16 ModalAction { get; set; }
        public Int16 Happiness { get; set; }
        public int NumTimesInteractedNpcCount1 { get; set; }
        public int NumTimesInteractedNpcCount2 { get; set; }
        public int NumTimesInteractedNpcCount3 { get; set; }
        public int NumTimesInteractedNpcCount4 { get; set; }
        public int NumTimesInteractedNpcCount5 { get; set; }
        public int NumTimesInteractedNpcCount6 { get; set; }
        public int NumTimesInteractedNpcCount7 { get; set; }
        public int NumTimesInteractedNpcCount8 { get; set; }
        public int NumTimesInteractedNpcCount9 { get; set; }
        public int NumTimesInteractedNpcCount10 { get; set; }
        public int NumTimesInteractedNpcCount11 { get; set; }
        public int NumTimesInteractedNpcCount12 { get; set; }
        public int NumTimesInteractedNpcCount13 { get; set; }
        public int NumTimesInteractedNpcCount14 { get; set; }
        public int NumTimesInteractedNpcCount15 { get; set; }
        public int NumTimesInteractedNpcCount16 { get; set; }
        public int NumTimesInteractedNpcCount17 { get; set; }
        public int NumTimesInteractedNpcCount18 { get; set; }
        public int NumTimesInteractedNpcCount19 { get; set; }
        public int NumTimesInteractedNpcCount20 { get; set; }
        public int NumTimesInteractedNpcCount21 { get; set; }
        public int NumTimesInteractedNpcCount22 { get; set; }
        public int NumTimesInteractedNpcCount23 { get; set; }
        public int NumTimesInteractedNpcCount24 { get; set; }
        public Int16 QuickWeaponSlot1 { get; set; }
        public Int16 QuickWeaponSlot2 { get; set; }
        public Int16 QuickWeaponSlot3 { get; set; }
        public Int16 QuickWeaponSlot4 { get; set; }
        public Int16 QuickWeaponSlot1Ability { get; set; }
        public Int16 QuickWeaponSlot2Ability { get; set; }
        public Int16 QuickWeaponSlot3Ability { get; set; }
        public Int16 QuickWeaponSlot4Ability { get; set; }
        public array8 QuickSpell1Resource { get; set; }
        public array8 QuickSpell2Resource { get; set; }
        public array8 QuickSpell3Resource { get; set; }
        public Int16 QuickItemSlot1 { get; set; }
        public Int16 QuickItemSlot2 { get; set; }
        public Int16 QuickItemSlot3 { get; set; }
        public Int16 QuickItemSlot1Ability { get; set; }
        public Int16 QuickItemSlot2Ability { get; set; }
        public Int16 QuickItemSlot3Ability { get; set; }
        public array32 Name { get; set; }
        public int TalkCount { get; set; }
        public GamCharacter CharacterStats { get; set; } = new();
        public array8 VoiceSet { get; set; }
    }

    [Serializable]
    public class GamCharacter
    {
        public int MostPowerfulVanquishedName { get; set; }
        public int MostPowerfulVanquishedXP { get; set; }
        public int TimeInParty { get; set; }
        public int TimeJoinedParty { get; set; }
        public byte PartyMember { get; set; }
        public Int16 Unused11 { get; set; }
        public char FirstLetterofCreResref { get; set; }
        public int KillsXPGainedChapter { get; set; }
        public int KillsNumberChapter { get; set; }
        public int KillsXPGainedGame { get; set; }
        public int KillsNumberGame { get; set; }
        public array8 FavouriteSpell1 { get; set; }
        public array8 FavouriteSpell2 { get; set; }
        public array8 FavouriteSpell3 { get; set; }
        public array8 FavouriteSpell4 { get; set; }
        public Int16 FavouriteSpell1Count { get; set; }
        public Int16 FavouriteSpell2Count { get; set; }
        public Int16 FavouriteSpell3Count { get; set; }
        public Int16 FavouriteSpell4Count { get; set; }
        public array8 FavouriteWeapon1 { get; set; }
        public array8 FavouriteWeapon2 { get; set; }
        public array8 FavouriteWeapon3 { get; set; }
        public array8 FavouriteWeapon4 { get; set; }
        public Int16 FavouriteWeapon1Time { get; set; }
        public Int16 FavouriteWeapon2Time { get; set; }
        public Int16 FavouriteWeapon3Time { get; set; }
        public Int16 FavouriteWeapon4Time { get; set; }
    }


    [Serializable]
    public class GamJournal
    {
        public IEString JournalText { get; set; }
        public int Time { get; set; }
        public byte Chapter { get; set; }
        public byte ReadBy { get; set; }
        public byte JournalSection { get; set; } //TODO:GAM
        public byte LocationFlag { get; set; }
    }

    [Serializable]
    public class GamFamiliarInfo
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

    [Serializable]
    public class GamStoredLocation
    {
        public array8 Area;
        public Int16 XCoordinate;
        public Int16 YCoordinate;
    }

    [Serializable]
    public class GamVariable
    {
        public array32 Name { get; set; }
        public Int16 Type { get; set; }
        public Int16 ResourceType { get; set; }
        public Int32 ValueDword { get; set; }
        public Int32 ValueInt { get; set; }
        public double ValueDouble { get; set; }
        public array32 ScriptName { get; set; }
    }

    [Serializable]
    public class GamPartyInventory
    {
        public array20 Unknown0;
    }

    [Serializable]
    public class WeatherFlags
    {
        public bool Rain { get; set; }
        public bool Snow { get; set; }
        public bool LightRain { get; set; }
        public bool MediumRain { get; set; }
        public bool LightWind { get; set; }
        public bool MediumWind { get; set; }
        public bool RareLightning { get; set; }
        public bool Lightning { get; set; }
        public bool StormIncreasing { get; set; }
        public bool Bit9 { get; set; }
        public bool Bit10 { get; set; }
        public bool Bit11 { get; set; }
        public bool Bit12 { get; set; }
        public bool Bit13 { get; set; }
        public bool Bit14 { get; set; }
        public bool Bit15 { get; set; }
    }

    [Serializable]
    public class GuiFlags
    {
        public bool AiEnabled { get; set; }
        public bool TextWindowSize1 { get; set; }
        public bool TextWindowSize2 { get; set; }
        public bool Bit3 { get; set; }
        public bool HideGui { get; set; }
        public bool HideOptionsPanel { get; set; }
        public bool HidePortraitPanel { get; set; }
        public bool ShowAutomapNotes { get; set; }
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
    public enum LoadingProgress
    {
        BG1,
        Totsc,
        Soa,
        XNewAreaPending,
        XNewAreaComplete,
        ToB
    }    
}