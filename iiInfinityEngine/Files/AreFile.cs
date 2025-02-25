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


        public AreFile()
        {
            AreaToTheNorthFlags = new AreaLinkFlags();
            AreaToTheEastFlags = new AreaLinkFlags();
            AreaToTheSouthFlags = new AreaLinkFlags();
            AreaToTheWestFlags = new AreaLinkFlags();
            AreaTypeFlags = new AreaTypeFlags();
        }

        public array8 AreaWed { get; set; }
        public Int32 LastSaved { get; set; }
        public AreaFlags AreaFlags;
        public array8 AreaToTheNorth { get; set; }
        public AreaLinkFlags AreaToTheNorthFlags { get; set; }
        public array8 AreaToTheEast { get; set; }
        public AreaLinkFlags AreaToTheEastFlags { get; set; }
        public array8 AreaToTheSouth { get; set; }
        public AreaLinkFlags AreaToTheSouthFlags { get; set; }
        public array8 AreaToTheWest { get; set; }
        public AreaLinkFlags AreaToTheWestFlags { get; set; }
        public AreaTypeFlags AreaTypeFlags { get; set; }
        public Int16 WeatherProbabilityRain { get; set; }
        public Int16 WeatherProbabilitySnow { get; set; }
        public Int16 WeatherProbabilityFog { get; set; }
        public Int16 WeatherProbabilityLightning { get; set; }
        public Int16 OverlayTransparency { get; set; }
        public Int32 VertexOffset { get; set; }
        public Int16 VertexCount { get; set; }
        public Int16 TiledObjectFlagOffset { get; set; }
        public Int16 TiledObjectFlagCount { get; set; }
        public array8 AreaScript { get; set; }
        public Int32 ExploredBitmaskSize { get; set; }
        public Int32 ExploredBitmaskOffset { get; set; }
        public array8 RestMovieDay { get; set; }
        public array8 RestMovieNight { get; set; }
        public array56 Unknowne4 { get; set; }
    }

    [Serializable]
    public struct AreActor2
    {
        public AreActor2()
        {
            ActorFlags = new ActorFlags();
            ActorAppearenceSchedule = new AppearenceSchedule();
        }

        public array32 Name { get; set; }
        public Int16 CurrentXCoordinate { get; set; }
        public Int16 CurrentYCoordinate { get; set; }
        public Int16 DestinationXCoordinate { get; set; }
        public Int16 DestinationYCoordinate { get; set; }
        public ActorFlags ActorFlags { get; set; }
        public Int16 HasBeenSpawned { get; set; }
        public byte FilenameInitialCharacter { get; set; }
        public byte Unknownef { get; set; }
        public Int32 ActorAnimation { get; set; }
        public Int16 ActorOrientation { get; set; }
        public Int16 Unknown36 { get; set; }
        public Int32 ActorRemovalTimer { get; set; }
        public Int16 MovementRestrictionDistance { get; set; }
        public Int16 MovementRestrictionDistanceMoveToObject { get; set; }
        public AppearenceSchedule ActorAppearenceSchedule { get; set; }
        public Int32 NumTimesTalkedTo { get; set; }
        public array8 Dialog { get; set; }
        public array8 ScriptOverride { get; set; }
        public array8 ScriptGeneral { get; set; }
        public array8 ScriptClass { get; set; }
        public array8 ScriptRace { get; set; }
        public array8 ScriptDefault { get; set; }
        public array8 ScriptSpecific { get; set; }
        public array8 CREFile { get; set; }
        public Int32 CreOffset { get; set; }
        public Int32 CreSize { get; set; }
        public array128 Unknown90 { get; set; }
    }

    [Serializable]
    public struct AreRegion2
    {
        public AreRegion2()
        {
            Flags = new RegionFlags();
        }

        public array32 Name { get; set; }
        public RegionType RegionType { get; set; }
        public Int16 BoundingBoxLeft { get; set; }
        public Int16 BoundingBoxTop { get; set; }
        public Int16 BoundingBoxRight { get; set; }
        public Int16 BoundingBoxBottom { get; set; }
        public Int16 VertexCount { get; set; }
        public Int32 VertexIndex { get; set; }
        public Int32 TriggerValue { get; set; }
        public Int32 Cursor { get; set; }
        public array8 DestinationArea { get; set; }
        public array32 DestinationEntrance { get; set; }
        public RegionFlags Flags { get; set; }
        public IEString InformationText { get; set; }
        public Int16 TrapDetectionDifficulty { get; set; }
        public Int16 TrapRemovalDifficulty { get; set; }
        public Int16 IsTrap { get; set; }
        public Int16 TrapDetected { get; set; }
        public Int16 TrapLaunchXCoordinate { get; set; }
        public Int16 TrapLaunchYCoordinate { get; set; }
        public array8 KeyItem { get; set; }
        public array8 RegionScript { get; set; }
        public Int16 AlternativeUsePointXCoordinate { get; set; }
        public Int16 AlternativeUsePointYCoordinate { get; set; }
        public Int32 Unknown88 { get; set; }
        public array32 Unknown8c { get; set; }
        public array8 Sound { get; set; }
        public Int16 TalkLocationXCoordinate { get; set; }
        public Int16 TalkLocationYCoordinate { get; set; }
        public IEString DialogName { get; set; }
        public array8 DialogFile { get; set; }
    }

    [Serializable]
    public class AreSpawnPoint2
    {
        public AreSpawnPoint2()
        {
            SpawnPointAppearenceSchedule = new AppearenceSchedule();
            SpawnMethod = new SpawnMethod();
        }

        public array32 Name { get; set; }
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public array8 Resref1 { get; set; }
        public array8 Resref2 { get; set; }
        public array8 Resref3 { get; set; }
        public array8 Resref4 { get; set; }
        public array8 Resref5 { get; set; }
        public array8 Resref6 { get; set; }
        public array8 Resref7 { get; set; }
        public array8 Resref8 { get; set; }
        public array8 Resref9 { get; set; }
        public array8 Resref10 { get; set; }
        public Int16 CreatureSpawnCount { get; set; }
        public Int16 BaseCreatureNumberToSpawn { get; set; }
        public Int16 Frequency { get; set; }
        public SpawnMethod SpawnMethod { get; set; }
        public Int32 ActorRemovalTime { get; set; }
        public Int16 MovementRestrictionDistance { get; set; }
        public Int16 MovementRestrictionDistanceToObject { get; set; }
        public Int16 MaximumCreaturesToSpawn { get; set; }
        public Int16 Enabled { get; set; }
        public AppearenceSchedule SpawnPointAppearenceSchedule { get; set; }
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
        public array38 Unknowna2 { get; set; }
    }

    [Serializable]
    public struct AreEntrance2
    {
        public array32 Name { get; set; }
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public Int16 Orientation { get; set; }
        public array66 Unknown26 { get; set; }
    }

    [Serializable]
    public class AreContainer2
    {
        public List<AreItem2> items = [];

        public AreContainer2()
        {
            Flags = new ContainerFlags();
        }

        public array32 Name { get; set; }
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public ContainerType ContainerType { get; set; }
        public Int16 LockDifficulty { get; set; }
        public ContainerFlags Flags { get; set; }
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
        public array8 TrapScript { get; set; }
        public Int32 VertexIndex { get; set; }
        public Int16 VertexCount { get; set; }
        public Int16 TriggerRange { get; set; }
        public array32 Owner { get; set; }
        public array8 KeyItem { get; set; }
        public Int32 BreakDifficulty { get; set; }
        public IEString LockpickString { get; set; }
        public array56 Unknown88 { get; set; }
    }

    [Serializable]
    public struct AreItem2
    {
        public AreItem2()
        {
            Flags = new AreaItemFlags();
        }

        public array8 ItemResref { get; set; }
        public Int16 ExpirationTime { get; set; }
        public Int16 Charges1 { get; set; }
        public Int16 Charges2 { get; set; }
        public Int16 Charges3 { get; set; }
        public AreaItemFlags Flags { get; set; }
    }

    [Serializable]
    public class AreAmbient2
    {
        public AreAmbient2()
        {
            AmbientAppearenceSchedule = new AppearenceSchedule();
            Flags = new AmbientFlags();
        }

        public array32 Name { get; set; }
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public Int16 Radius { get; set; }
        public Int16 Height { get; set; }
        public Int32 PitchVariance { get; set; }
        public Int16 VolumeVariance { get; set; }
        public Int16 Volume { get; set; }
        public array8 Resref1 { get; set; }
        public array8 Resref2 { get; set; }
        public array8 Resref3 { get; set; }
        public array8 Resref4 { get; set; }
        public array8 Resref5 { get; set; }
        public array8 Resref6 { get; set; }
        public array8 Resref7 { get; set; }
        public array8 Resref8 { get; set; }
        public array8 Resref9 { get; set; }
        public array8 Resref10 { get; set; }
        public Int16 ResRefCount { get; set; }
        public Int16 Unknown82 { get; set; }
        public Int32 FrequencyBase { get; set; }
        public Int32 FrequencyVariation { get; set; }
        public AppearenceSchedule AmbientAppearenceSchedule { get; set; }
        public AmbientFlags Flags { get; set; }
        public array64 Unknown94 { get; set; }
    }

    [Serializable]
    public struct AreVariable2
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
    public struct AreDoor2
    {
        public AreDoor2()
        {
            Flags = new DoorFlags();
        }

        public array32 Name { get; set; }
        public array8 DoorId { get; set; }
        public DoorFlags Flags { get; set; }
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
        public array8 DoorOpenSound { get; set; }
        public array8 DoorCloseSound { get; set; }
        public Int32 Cursor { get; set; }
        public Int16 TrapDetectionDifficulty { get; set; }
        public Int16 TrapRemovalDifficulty { get; set; }
        public Int16 IsTrap { get; set; }
        public Int16 TrapDetected { get; set; }
        public Int16 TrapLaunchXCoordinate { get; set; }
        public Int16 TrapLaunchYCoordinate { get; set; }
        public array8 KeyItem { get; set; }
        public array8 DoorScript { get; set; }
        public Int32 SecretDoorDetectionDifficulty { get; set; }
        public Int32 LockDifficulty { get; set; }
        public Int16 DoorState1X { get; set; }
        public Int16 DoorState1Y { get; set; }
        public Int16 DoorState2X { get; set; }
        public Int16 DoorState2Y { get; set; }
        public IEString LockpickString { get; set; }
        public array24 TravelTriggerName { get; set; }
        public IEString DialogName { get; set; }
        public array8 DialogResref { get; set; }
        public array8 Unknownc0 { get; set; }
    }

    [Serializable]
    public struct AreAnimation2
    {
        public AreAnimation2()
        {
            AnimationAppearenceSchedule = new AppearenceSchedule();
            Flags = new AnimationFlags();
        }

        public array32 Name { get; set; }
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public AppearenceSchedule AnimationAppearenceSchedule { get; set; }
        public array8 BamAnimation { get; set; }
        public Int16 BamSequence { get; set; }
        public Int16 BamFrame { get; set; }
        public AnimationFlags Flags { get; set; }
        public Int16 Height { get; set; }
        public Int16 Transparency { get; set; }
        public Int16 StartFrame { get; set; }
        public byte LoopChance { get; set; }
        public byte SkipCycles { get; set; }
        public array8 Palette { get; set; }
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
        public NoteColor Colour { get; set; }
        public array36 Unknown10 { get; set; }
    }

    [Serializable]
    public struct AreTiledObject2
    {
        public array32 Name { get; set; }
        public array8 TileId { get; set; }
        public TiledObjectFlags Flags { get; set; }
        public Int32 OpenSearchOffset { get; set; }
        public Int16 OpenSearchCount { get; set; }
        public Int16 ClosedSearchCount { get; set; }
        public Int32 ClosedSearchOffset { get; set; }
        public array48 Unknown3c { get; set; }
    }

    [Serializable]
    public struct AreProjectile2
    {
        public array8 Resref { get; set; }
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
        public array8 DayAmbient1Wav { get; set; }
        public array8 DayAmbient2Wav { get; set; }
        public Int32 DayAmbientVolume { get; set; }
        public array8 NightAmbient1Wav { get; set; }
        public array8 NightAmbient2Wav { get; set; }
        public Int32 NightAmbientVolume { get; set; }
        public Int32 Reverb { get; set; }
        public array60 Unknown54 { get; set; }
    }

    [Serializable]
    public struct AreInterruption2
    {
        public array32 Name { get; set; }
        public IEString Text1 { get; set; }
        public IEString Text2 { get; set; }
        public IEString Text3 { get; set; }
        public IEString Text4 { get; set; }
        public IEString Text5 { get; set; }
        public IEString Text6 { get; set; }
        public IEString Text7 { get; set; }
        public IEString Text8 { get; set; }
        public IEString Text9 { get; set; }
        public IEString Text10 { get; set; }
        public array8 ResRef1 { get; set; }
        public array8 ResRef2 { get; set; }
        public array8 ResRef3 { get; set; }
        public array8 ResRef4 { get; set; }
        public array8 ResRef5 { get; set; }
        public array8 ResRef6 { get; set; }
        public array8 ResRef7 { get; set; }
        public array8 ResRef8 { get; set; }
        public array8 ResRef9 { get; set; }
        public array8 ResRef10 { get; set; }
        public Int16 CreatureCount { get; set; }
        public Int16 Difficulty { get; set; }
        public Int32 RemovalTime { get; set; }
        public Int16 RestrictionDistance { get; set; }
        public Int16 RestrictionDistanceToObject { get; set; }
        public Int16 MaximumCreaturesToSpawn { get; set; }
        public Int16 Enabled { get; set; }
        public Int16 DayProbability { get; set; }
        public Int16 NightProbability { get; set; }
        public array56 Unknownac { get; set; }
    }

    [Serializable]
    public struct AreaFlags
    {
        public bool SaveAllowed { get; set; }
        public bool TutorialArea { get; set; }
        public bool DeadMagicZone { get; set; }
        public bool Dream { get; set; }
        public bool Player1DeathDoesNotEndGame { get; set; }
        public bool RestingNotAllowed { get; set; }
        public bool TravelNotAllowed { get; set; }
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
    public class AreaLinkFlags
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
    public class AreaTypeFlags
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
    public class ActorFlags
    {
        public bool CreAttached { get; set; }
        public bool HasSeenParty { get; set; }
        public bool Invulnerable { get; set; }
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
    public class RegionFlags
    {
        public bool KeyRequired { get; set; }
        public bool ResetTrap { get; set; }
        public bool PartyRequired { get; set; }
        public bool Detectable { get; set; }
        public bool EnemiesActivates { get; set; }
        public bool TutorialTrigger { get; set; }
        public bool NpcActivates { get; set; }
        public bool SilentTrigger { get; set; }
        public bool Deactivated { get; set; }
        public bool NPCCannotPass { get; set; }
        public bool AlternativePoint { get; set; }
        public bool DoorClosed { get; set; }
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
    public class AreaItemFlags
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
    public class ContainerFlags
    {
        public bool Locked { get; set; }
        public bool DisabledIfNoOwner { get; set; }
        public bool MagicalLock { get; set; }
        public bool TrapResets { get; set; }
        public bool RemoveOnly { get; set; }
        public bool Disabled { get; set; }
        public bool DoNotClear { get; set; }
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
    public class AppearenceSchedule
    {
        public bool Am1 { get; set; }
        public bool Am2 { get; set; }
        public bool Am3 { get; set; }
        public bool Am4 { get; set; }
        public bool Am5 { get; set; }
        public bool Am6 { get; set; }
        public bool Am7 { get; set; }
        public bool Am8 { get; set; }
        public bool Am9 { get; set; }
        public bool Am10 { get; set; }
        public bool Am11 { get; set; }
        public bool Am12 { get; set; }
        public bool Pm1 { get; set; }
        public bool Pm2 { get; set; }
        public bool Pm3 { get; set; }
        public bool Pm4 { get; set; }
        public bool Pm5 { get; set; }
        public bool Pm6 { get; set; }
        public bool Pm7 { get; set; }
        public bool Pm8 { get; set; }
        public bool Pm9 { get; set; }
        public bool Pm10 { get; set; }
        public bool Pm11 { get; set; }
        public bool Pm12 { get; set; }
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
    public class AmbientFlags
    {
        public bool Enabled { get; set; }
        public bool DisableEnvironmentalEffects { get; set; }
        public bool IgnoreRadius { get; set; }
        public bool PlayInRandomOrder { get; set; }
        public bool HighMemoryAmbient { get; set; }
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
    public class TiledObjectFlags
    {
        public bool InSecondaryState { get; set; }
        public bool CanBeSeenThrough { get; set; }
        public bool Bit2 { get; set; }
        public bool Bit3 { get; set; }
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
    public class DoorFlags
    {
        public bool DoorOpen { get; set; }
        public bool DoorLocked { get; set; }
        public bool ResetTrap { get; set; }
        public bool DetectableTrap { get; set; }
        public bool DoorForced { get; set; }
        public bool CannotClose { get; set; }
        public bool Linked { get; set; }
        public bool DoorHidden { get; set; }
        public bool DoorFound { get; set; }
        public bool DoNotBlockLos { get; set; }
        public bool RemoveKey { get; set; }
        public bool IgnoreObstaclesWhenClosing { get; set; }
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
    public class AnimationFlags
    {
        public bool Enabled { get; set; }
        public bool TransparentBlack { get; set; }
        public bool NotLightSource { get; set; }
        public bool PartialAnimation { get; set; }
        public bool SynchronizedDraw { get; set; }
        public bool RandomStartFrame { get; set; }
        public bool NotCoveredByWall { get; set; }
        public bool DisableOnSlowMachines { get; set; }
        public bool DrawAsBackground { get; set; }
        public bool PlayAllFrames { get; set; }
        public bool UsePaletteBitmap { get; set; }
        public bool MirrorYAxis { get; set; }
        public bool DoNotRemoveInCombat { get; set; }
        public bool WbmResref { get; set; }
        public bool DrawStenciled { get; set; }
        public bool PvrzResref { get; set; }
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
    public class SpawnMethod
    {
        public bool SpawnUntilPaused { get; set; }
        public bool DisableAfterSpawn { get; set; }
        public bool SpawnPaused { get; set; }
        public bool Bit3 { get; set; }
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

    [Serializable]
    public enum NoteColor : Int16
    {
        Gray = 0,
        Violet = 1,
        Green = 2,
        Orange = 3,
        Red = 4,
        Blue = 5,
        DarkBlue = 6,
        LightGray = 7
    }
}