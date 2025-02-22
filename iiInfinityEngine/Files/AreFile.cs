using System;
using System.Collections.Generic;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class AreFile : IEFile
    {
        public List<AreActor2> actors = [];
        public List<AreRegion2> regions = [];
        public List<AreSpawnPoint2> spawns = [];
        public List<AreEntrance2> entrances = [];
        public List<AreContainer2> containers = [];
        public List<AreItem2> items = [];
        public List<AreAmbient2> ambients = [];
        public List<AreVariable2> variables = [];
        public List<AreDoor2> doors = [];
        public List<AreAnimation2> animations = [];
        public List<AreNote2> notes = [];
        public List<AreTiledObject2> tiledObjects = [];
        public List<AreProjectile2> projectiles = [];
        public List<AreSong2> songs = [];
        public List<AreInterruption2> interruptions = [];
        public List<bool> exploration = [];
        public List<Int32> vertices = [];

        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private readonly IEFileType fileType = IEFileType.Are;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public string AreaWED { get; set; }
        public Int32 LastSaved { get; set; }
        public AreaFlags AreaFlags;
        public string AreaToTheNorth { get; set; }
        public AreaLinkFlags AreaToTheNorthFlags;
        public string AreaToTheEast { get; set; }
        public AreaLinkFlags AreaToTheEastFlags;
        public string AreaToTheSouth { get; set; }
        public AreaLinkFlags AreaToTheSouthFlags;
        public string AreaToTheWest { get; set; }
        public AreaLinkFlags AreaToTheWestFlags;
        public AreaTypeFlags AreaTypeFlags;
        public Int16 WeatherProbabilityRain { get; set; }
        public Int16 WeatherProbabilitySnow { get; set; }
        public Int16 WeatherProbabilityFog { get; set; }// - not implemented 
        public Int16 WeatherProbabilityLightning { get; set; }
        public Int16 OverlayTransparency { get; set; }
        public Int32 VertexOffset { get; set; }
        public Int16 VertexCount { get; set; }
        public Int16 TiledObjectFlagOffset { get; set; }
        public Int16 TiledObjectFlagCount { get; set; }
        public string AreaScript { get; set; }
        public Int32 ExploredBitmaskSize { get; set; }
        public Int32 ExploredBitmaskOffset { get; set; }
        public string RestMovieDay { get; set; }
        public string RestMovieNight { get; set; }
        public array56 Unknown7 { get; set; }
    }

    [Serializable]
    public struct AreActor2
    {
        public string Name { get; set; }
        public Int16 CurrentXCoordinate { get; set; }
        public Int16 CurrentYCoordinate { get; set; }
        public Int16 DestinationXCoordinate { get; set; }
        public Int16 DestinationYCoordinate { get; set; }
        public ActorFlags ActorFlags;
        public Int16 HasBeenSpawned { get; set; }
        public byte FilenameInitialCharacter { get; set; }
        public byte Unknown { get; set; }
        public Int32 ActorAnimation { get; set; }
        public Int32 ActorOrientation { get; set; }
        public Int32 ActorRemovalTimer { get; set; }
        public Int16 MovementRestrictionDistance { get; set; }
        public Int16 MovementRestrictionDistanceMoveToObject { get; set; }
        public Int32 ActorAppearenceSchedule { get; set; }
        //bit 23 = 23:30 to 00:29
        //bit 22 = 22:30 to 23:29
        //bit 21 = 21:30 to 22:29 (Night)
        //bit 20 = 20:30 to 21:29 (Dusk)
        //bit 19 = 19:30 to 20:29
        //bit 18 = 18:30 to 19:29
        //bit 17 = 17:30 to 18:29
        //bit 16 = 16:30 to 17:29
        //bit 15 = 15:30 to 16:29
        //bit 14 = 14:30 to 15:29
        //bit 13 = 13:30 to 14:29
        //bit 12 = 12:30 to 13:29
        //bit 11 = 11:30 to 12:29
        //bit 10 = 10:30 to 11:29
        //bit 9 = 09:30 to 10:29
        //bit 8 = 08:30 to 09:29
        //bit 7 = 07:30 to 08:29
        //bit 6 = 06:30 to 07:29 (Day)
        //bit 5 = 05:30 to 06:29 (Dawn)
        //bit 4 = 04:30 to 05:29
        //bit 3 = 03:30 to 04:29
        //bit 2 = 02:30 to 03:29
        //bit 1 = 01:30 to 02:29
        //bit 0 = 00:30 to 01:29 
        public Int32 NumTimesTalkedTo { get; set; }
        public string Dialog { get; set; }
        public string ScriptOverride { get; set; }
        public string ScriptGeneral { get; set; }
        public string ScriptClass { get; set; }
        public string ScriptRace { get; set; }
        public string ScriptDefault { get; set; }
        public string ScriptSpecific { get; set; }
        public string CREFile { get; set; }
        public Int32 CreOffset { get; set; }
        public Int32 CreSize { get; set; }
        public array128 Unknown2 { get; set; }
    }

    [Serializable]
    public struct AreRegion2
    {
        public string Name { get; set; }
        public RegionType RegionType { get; set; }
        public Int16 BoundingBoxLeft { get; set; }
        public Int16 BoundingBoxTop { get; set; }
        public Int16 BoundingBoxRight { get; set; }
        public Int16 BoundingBoxBottom { get; set; }
        public Int16 VertexCount { get; set; }
        public Int32 VertexIndex { get; set; }
        public Int32 TriggerValue { get; set; }
        public Int32 Cursor { get; set; }
        public string DestinationArea { get; set; }
        public string DestinationEntrance { get; set; }
        public RegionFlags Flags;
        public IEString InformationText { get; set; }
        public Int16 TrapDetectionDifficulty { get; set; }
        public Int16 TrapRemovalDifficulty { get; set; }
        public Int16 IsTrap { get; set; }
        public Int16 TrapDetected { get; set; }
        public Int16 TrapLaunchXCoordinate { get; set; }
        public Int16 TrapLaunchYCoordinate { get; set; }
        public string KeyItem { get; set; }
        public string RegionScript { get; set; }
        public Int16 AlternativeUsePointXCoordinate { get; set; }
        public Int16 AlternativeUsePointYCoordinate { get; set; }
        public Int32 Unknown2 { get; set; }
        public array32 Unknown3 { get; set; }
        public string Sound { get; set; }
        public Int16 TalkLocationXCoordinate { get; set; }
        public Int16 TalkLocationYCoordinate { get; set; }
        public IEString DialogName { get; set; }
        public string DialogFile { get; set; }
    }

    [Serializable]
    public struct AreSpawnPoint2
    {
        public string Name { get; set; }
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public string Resref1 { get; set; }
        public string Resref2 { get; set; }
        public string Resref3 { get; set; }
        public string Resref4 { get; set; }
        public string Resref5 { get; set; }
        public string Resref6 { get; set; }
        public string Resref7 { get; set; }
        public string Resref8 { get; set; }
        public string Resref9 { get; set; }
        public string Resref10 { get; set; }
        public Int16 CreatureSpawnCount { get; set; }
        public Int16 BaseCreatureNumberToSpawn { get; set; }
        public Int16 Frequency { get; set; }
        public Int16 SpawnMethod { get; set; }
        //Bit 0: If Bit 2 is set, don't spawn
        //Bit 1: One-time-spawnpoint (checked for after the spawning, and disables the spawn point if set)
        //Bit 2: Used internally to disable the spawn point temporarily
        //Set after a spawning and cleared in two situations:
        //(1) after the CompressTime() method for the spawn point is called with a time amount of at least 16 hours
        //(2) if Bit 0 is not set and there are no living spawns in the area 
        public Int32 ActorRemovalTime { get; set; }
        public Int16 MovementRestrictionDistance { get; set; }
        public Int16 MovementRestrictionDistanceToObject { get; set; }
        public Int16 MaximumCreaturesToSpawn { get; set; }
        public Int16 Enabled { get; set; }
        public Int32 SpawnPointAppearenceSchedule { get; set; }
        //bit 23 = 23:30 to 00:29
        //bit 22 = 22:30 to 23:29
        //bit 21 = 21:30 to 22:29 (Night)
        //bit 20 = 20:30 to 21:29 (Dusk)
        //bit 19 = 19:30 to 20:29
        //bit 18 = 18:30 to 19:29
        //bit 17 = 17:30 to 18:29
        //bit 16 = 16:30 to 17:29
        //bit 15 = 15:30 to 16:29
        //bit 14 = 14:30 to 15:29
        //bit 13 = 13:30 to 14:29
        //bit 12 = 12:30 to 13:29
        //bit 11 = 11:30 to 12:29
        //bit 10 = 10:30 to 11:29
        //bit 9 = 09:30 to 10:29
        //bit 8 = 08:30 to 09:29
        //bit 7 = 07:30 to 08:29
        //bit 6 = 06:30 to 07:29 (Day)
        //bit 5 = 05:30 to 06:29 (Dawn)
        //bit 4 = 04:30 to 05:29
        //bit 3 = 03:30 to 04:29
        //bit 2 = 02:30 to 03:29
        //bit 1 = 01:30 to 02:29
        //bit 0 = 00:30 to 01:29 
        public Int16 ProbabilityDay { get; set; }
        public Int16 ProbabilityNight { get; set; }
        public Int32 SpawnFrequency { get; set; }
        public Int32 Countdown { get; set; }
        public byte SpawnWeight1 { get; set; }
        public byte SpawnWeight2 { get; set; }
        public byte SpawnWeight3 { get; set; }
        public byte SpawnWeight4 { get; set; }
        public byte SpawnWeight5 { get; set; }
        public byte SpawnWeight6 { get; set; }
        public byte SpawnWeight7 { get; set; }
        public byte SpawnWeight8 { get; set; }
        public byte SpawnWeight9 { get; set; }
        public byte SpawnWeight10 { get; set; }
        public array38 Unknown { get; set; }
    }

    [Serializable]
    public struct AreEntrance2
    {
        public string Name { get; set; }
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public Int16 Orientation { get; set; }
        public array66 Unknown { get; set; }
    }

    [Serializable]
    public class AreContainer2
    {
        public List<AreItem2> items = new List<AreItem2>();

        public string Name { get; set; }
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public ContainerType ContainerType { get; set; }
        public Int16 LockDifficulty { get; set; }
        public ContainerFlags Flags;
        public Int16 TrapDetectionDifficulty { get; set; }
        public Int16 TrapRemovalDifficulty { get; set; }
        public Int16 IsTrap { get; set; }
        public Int16 TrapDetected { get; set; }
        public Int16 TrapLaunchXCoordinate { get; set; }
        public Int16 TrapLaunchYCoordinate { get; set; }
        public Int16 BoundingBoxLeft { get; set; }
        public Int16 BoundingBoxTop { get; set; }
        public Int16 BoundingBoxRight { get; set; }
        public Int16 BoundingBoxBottom { get; set; }
        public string TrapScript { get; set; }
        public Int32 VertexIndex { get; set; }
        public Int16 VertexCount { get; set; }
        public Int16 TriggerRange { get; set; }
        public array32 Owner { get; set; }
        public string KeyItem { get; set; }
        public Int32 BreakDifficulty { get; set; }
        public IEString LockpickString { get; set; }
        public array56 Unknown3 { get; set; }
    }

    [Serializable]
    public struct AreItem2
    {
        public string ItemResref { get; set; }
        public Int16 ExpirationTime { get; set; }
        public Int16 Charges1 { get; set; }
        public Int16 Charges2 { get; set; }
        public Int16 Charges3 { get; set; }
        public AreaItemFlags Flags;
    }

    [Serializable]
    public struct AreAmbient2
    {
        public string Name { get; set; }
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public Int16 Radius { get; set; }
        public Int16 Height { get; set; }
        public Int32 PitchVariance { get; set; }
        public Int16 VolumeVariance { get; set; }
        public Int16 Volume { get; set; }
        public string Resref1 { get; set; }
        public string Resref2 { get; set; }
        public string Resref3 { get; set; }
        public string Resref4 { get; set; }
        public string Resref5 { get; set; }
        public string Resref6 { get; set; }
        public string Resref7 { get; set; }
        public string Resref8 { get; set; }
        public string Resref9 { get; set; }
        public string Resref10 { get; set; }
        public Int16 ResRefCount { get; set; }
        public Int16 Unknown2 { get; set; }
        public Int32 FrequencyBase { get; set; }
        public Int32 FrequencyVariation { get; set; }
        public Int32 AmbientAppearenceSchedule { get; set; }
        public Int32 Flags { get; set; }
        public array64 Unknown3 { get; set; }
    }

    [Serializable]
    public struct AreVariable2
    {
        public string Name { get; set; }
        public Int16 Type { get; set; }
        public Int16 ResourceType { get; set; }
        public Int32 ValueDword { get; set; }
        public Int32 ValueInt { get; set; }
        public double ValueDouble { get; set; }
        public string ScriptName { get; set; }
    }

    [Serializable]
    public struct AreDoor2
    {
        public string Name { get; set; }
        public string DoorId { get; set; }
        public Int32 Flags { get; set; }
        //bit 0: Door open
        //bit 1: Door locked
        //bit 2: Reset trap
        //bit 3: Trap detectable
        //bit 4: Door has been bashed
        //bit 5: Can't close
        //bit 6: Linked
        //bit 7: Door Hidden
        //bit 8: Door Found (if hidden)
        //bit 9: Don't block line of sight
        //bit 10: Remove Key (BG2 only)
        //bit 11: Slide
        public Int32 OpenVertexIndex { get; set; }
        public Int16 OpenVertexCount { get; set; }
        public Int16 ClosedVertexCount { get; set; }
        public Int32 ClosedVertexIndex { get; set; }
        public Int16 OpenBoundingBoxLeft { get; set; }
        public Int16 OpenBoundingBoxTop { get; set; }
        public Int16 OpenBoundingBoxRight { get; set; }
        public Int16 OpenBoundingBoxBottom { get; set; }
        public Int16 ClosedBoundingBoxLeft { get; set; }
        public Int16 ClosedBoundingBoxTop { get; set; }
        public Int16 ClosedBoundingBoxRight { get; set; }
        public Int16 ClosedBoundingBoxBottom { get; set; }
        public Int32 OpenVertexBlockIndex { get; set; }
        public Int16 OpenVertexBlockCount { get; set; }
        public Int16 ClosedVertexBlockCount { get; set; }
        public Int32 ClosedVertexBlockIndex { get; set; }
        public Int16 Hitpoints { get; set; }
        public Int16 ArmourClass { get; set; }
        public string DoorOpenSound { get; set; }
        public string DoorCloseSound { get; set; }
        public Int32 Cursor { get; set; }
        public Int16 TrapDetectionDifficulty { get; set; }
        public Int16 TrapRemovalDifficulty { get; set; }
        public Int16 IsTrap { get; set; }
        public Int16 TrapDetected { get; set; }
        public Int16 TrapLaunchXCoordinate { get; set; }
        public Int16 TrapLaunchYCoordinate { get; set; }
        public string KeyItem { get; set; }
        public string DoorScript { get; set; }
        public Int32 SecretDoorDetectionDifficulty { get; set; }
        public Int32 LockDifficulty { get; set; }
        public Int16 DoorState1X { get; set; }
        public Int16 DoorState1Y { get; set; }
        public Int16 DoorState2X { get; set; }
        public Int16 DoorState2Y { get; set; }
        public IEString LockpickString { get; set; }
        public string TravelTriggerName { get; set; }
        public IEString DialogName { get; set; }
        public string DialogResref { get; set; }
        public array8 Unknown { get; set; }
    }

    [Serializable]
    public struct AreAnimation2
    {
        public string Name { get; set; }
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public Int32 AnimationAppearenceSchedule { get; set; }
        public string BamAnimation { get; set; }
        public Int16 BamSequence { get; set; }
        public Int16 BamFrame { get; set; }
        public Int32 Flags { get; set; }
        public Int16 Height { get; set; }
        public Int16 Transparency { get; set; }
        public Int16 StartFrame { get; set; }
        public byte LoopChance { get; set; }
        public byte SkipCycles { get; set; }
        public string Palette { get; set; }
        public Int16 WidthPVRZ { get; set; }
        public Int16 HeightPVRZ { get; set; }
    }

    [Serializable]
    public struct AreNote2
    {
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public IEString Text { get; set; }
        public Int16 Location { get; set; }
        public Int16 Colour { get; set; }
        public array36 Unknown { get; set; }
    }

    [Serializable]
    public struct AreTiledObject2
    {
        public string Name { get; set; }
        public array8 TileId { get; set; }
        public Int32 Flags { get; set; }
        public Int32 OpenSearchOffset { get; set; }
        public Int16 OpenSearchCount { get; set; }
        public Int16 ClosedSearchCount { get; set; }
        public Int32 ClosedSearchOffset { get; set; }
        public array48 Unknown { get; set; }
    }

    [Serializable]
    public struct AreProjectile2
    {
        public string Resref { get; set; }
        public Int32 EffectOffset { get; set; }
        public Int16 EffectSize { get; set; }
        public Int16 MissileId { get; set; } // missile.ids or projectile.ids - 1
        public Int16 TickUntilTriggerCheck { get; set; }
        public Int16 TriggersRemaining { get; set; }
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public Int16 ZCoordinate { get; set; }
        public byte EATarget { get; set; }
        public byte PartyOwnerIndex { get; set; }
    }

    [Serializable]
    public struct AreSong2
    {
        public Int32 DaySong { get; set; }
        public Int32 NightSong { get; set; }
        public Int32 WinSong { get; set; }
        public Int32 BattleSong { get; set; }
        public Int32 LoseSong { get; set; }
        public Int32 AltMusic1 { get; set; }
        public Int32 AltMusic2 { get; set; }
        public Int32 AltMusic3 { get; set; }
        public Int32 AltMusic4 { get; set; }
        public Int32 AltMusic5 { get; set; }
        public string DayAmbient1Wav { get; set; }
        public string DayAmbient2Wav { get; set; }
        public Int32 DayAmbientVolume { get; set; }
        public string NightAmbient1Wav { get; set; }
        public string NightAmbient2Wav { get; set; }
        public Int32 NightAmbientVolume { get; set; }
        public Int32 Reverb { get; set; }
        public array60 Unknown { get; set; }
    }

    [Serializable]
    public struct AreInterruption2
    {
        public string Name { get; set; }
        public Int32 Text1 { get; set; }
        public Int32 Text2 { get; set; }
        public Int32 Text3 { get; set; }
        public Int32 Text4 { get; set; }
        public Int32 Text5 { get; set; }
        public Int32 Text6 { get; set; }
        public Int32 Text7 { get; set; }
        public Int32 Text8 { get; set; }
        public Int32 Text9 { get; set; }
        public Int32 Text10 { get; set; }
        public string ResRef1 { get; set; }
        public string ResRef2 { get; set; }
        public string ResRef3 { get; set; }
        public string ResRef4 { get; set; }
        public string ResRef5 { get; set; }
        public string ResRef6 { get; set; }
        public string ResRef7 { get; set; }
        public string ResRef8 { get; set; }
        public string ResRef9 { get; set; }
        public string ResRef10 { get; set; }
        public Int16 CreatureCount { get; set; }
        public Int16 Difficulty { get; set; }
        public Int32 RemovalTime { get; set; }
        public Int16 RestrictionDistance { get; set; }
        public Int16 RestrictionDistanceToObject { get; set; }
        public Int16 MaximumCreaturesToSpawn { get; set; }
        public Int16 Enabled { get; set; }
        public Int16 DayProbability { get; set; }
        public Int16 NightProbability { get; set; }
        public array56 Unknown { get; set; }
    }

    [Serializable]
    public struct AreaFlags
    {
        public bool SaveAllowed { get; set; }
        public bool TutorialArea { get; set; }
        public bool DeadMagicZone { get; set; }
        public bool Dream { get; set; }
        public bool Bit04 { get; set; }
        public bool Bit05 { get; set; }
        public bool Bit06 { get; set; }
        public bool Bit07 { get; set; }
        public bool Bit08 { get; set; }
        public bool Bit09 { get; set; }
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
    public struct AreaLinkFlags
    {
        public bool PartyRequired { get; set; }
        public bool PartyEnabled { get; set; }
        public bool Bit02 { get; set; }
        public bool Bit03 { get; set; }
        public bool Bit04 { get; set; }
        public bool Bit05 { get; set; }
        public bool Bit06 { get; set; }
        public bool Bit07 { get; set; }
        public bool Bit08 { get; set; }
        public bool Bit09 { get; set; }
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
    public struct AreaTypeFlags
    {
        public bool Outdoor { get; set; }
        public bool DayNight { get; set; }
        public bool Weather { get; set; }
        public bool City { get; set; }
        public bool Forest { get; set; }
        public bool Dungeon { get; set; }
        public bool ExtendedNight { get; set; }
        public bool CanRestIndoors { get; set; }
        public bool Bit08 { get; set; }
        public bool Bit09 { get; set; }
        public bool Bit10 { get; set; }
        public bool Bit11 { get; set; }
        public bool Bit12 { get; set; }
        public bool Bit13 { get; set; }
        public bool Bit14 { get; set; }
        public bool Bit15 { get; set; }
    }

    [Serializable]
    public struct ActorFlags
    {
        public bool CreAttached { get; set; }
        public bool Bit01 { get; set; }
        public bool Bit02 { get; set; }
        public bool OverrideScriptName { get; set; }
        public bool Bit04 { get; set; }
        public bool Bit05 { get; set; }
        public bool Bit06 { get; set; }
        public bool Bit07 { get; set; }
        public bool Bit08 { get; set; }
        public bool Bit09 { get; set; }
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
    public struct RegionFlags
    {
        public bool InvisibleTrap { get; set; }
        public bool ResetTrap { get; set; }
        public bool PartyRequired { get; set; }
        public bool Detectable { get; set; }
        public bool Bit04 { get; set; }
        public bool Bit05 { get; set; }
        public bool NPCCanTrigger { get; set; }
        public bool Bit07 { get; set; }
        public bool Deactivated { get; set; }
        public bool NPCCannotPass { get; set; }
        public bool AlternativePoint { get; set; }
        public bool UsedByDoor { get; set; }
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
    public struct AreaItemFlags
    {
        public bool Identified { get; set; }
        public bool Unstealable { get; set; }
        public bool Stolen { get; set; }
        public bool Undroppable { get; set; }
        public bool Bit04 { get; set; }
        public bool Bit05 { get; set; }
        public bool Bit06 { get; set; }
        public bool Bit07 { get; set; }
        public bool Bit08 { get; set; }
        public bool Bit09 { get; set; }
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
    public struct ContainerFlags
    {
        public bool Locked { get; set; }
        public bool Bit01 { get; set; }
        public bool Bit02 { get; set; }
        public bool TrapResets { get; set; }
        public bool Bit04 { get; set; }
        public bool Disabled { get; set; }
        public bool Bit06 { get; set; }
        public bool Bit07 { get; set; }
        public bool Bit08 { get; set; }
        public bool Bit09 { get; set; }
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
    public enum RegionType
    {
        ProximityTrigger = 0,
        InfoPoint = 1,
        TravelRegion = 2
    }

    [Serializable]
    public enum ContainerType
    {
        None,
        Bag,
        Chest,
        Drawer,
        Pile,
        Table,
        Shelf,
        Altar,
        Nonvisible,
        Spellbook,
        Body,
        Barrel,
        Crate
    }
}