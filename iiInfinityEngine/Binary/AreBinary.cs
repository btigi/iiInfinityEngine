using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreHeaderBinary
    {
        public array4 ftype { get; set; }
        public array4 fversion { get; set; }
        public array8 AreaWED { get; set; }
        public Int32 LastSaved { get; set; }
        public Int32 AreaFlags { get; set; }
        public array8 AreaToTheNorth { get; set; }
        public Int32 AreaToTheNorthFlags { get; set; }
        public array8 AreaToTheEast { get; set; }
        public Int32 AreaToTheEastFlags { get; set; }
        public array8 AreaToTheSouth { get; set; }
        public Int32 AreaToTheSouthFlags { get; set; }
        public array8 AreaToTheWest { get; set; }
        public Int32 AreaToTheWestFlags { get; set; }
        public ushort AreaTypeFlags { get; set; }
        public Int16 WeatherProbabilityRain { get; set; }
        public Int16 WeatherProbabilitySnow { get; set; }
        public Int16 WeatherProbabilityFog { get; set; }
        public Int16 WeatherProbabilityLightning { get; set; }
        public Int16 OverlayTransparency { get; set; }
        public Int32 ActorOffset { get; set; }
        public Int16 ActorCount { get; set; }
        public Int16 RegionCount { get; set; }
        public Int32 RegionOffset { get; set; }
        public Int32 SpawnPointOffset { get; set; }
        public Int32 SpawnPointCount { get; set; }
        public Int32 EntrancesOffset { get; set; }
        public Int32 EntrancesCount { get; set; }
        public Int32 ContainerOffset { get; set; }
        public Int16 ContainerCount { get; set; }
        public Int16 ItemCount { get; set; }
        public Int32 ItemOffset { get; set; }
        public Int32 VertexOffset { get; set; }
        public Int16 VertexCount { get; set; }
        public Int16 AmbientCount { get; set; }
        public Int32 AmbientOffset { get; set; }
        public Int32 VariableOffset { get; set; }
        public Int32 VariableCount { get; set; }
        public Int16 TiledObjectFlagOffset { get; set; }
        public Int16 TiledObjectFlagCount { get; set; }
        public array8 AreaScript { get; set; }
        public Int32 ExploredBitmaskSize { get; set; }
        public Int32 ExploredBitmaskOffset { get; set; }
        public Int32 DoorCount { get; set; }
        public Int32 DoorOffset { get; set; }
        public Int32 AnimationCount { get; set; }
        public Int32 AnimationOffset { get; set; }
        public Int32 TiledObjectCount { get; set; }
        public Int32 TiledObjectOffset { get; set; }
        public Int32 SongEntryOffset { get; set; }
        public Int32 RestInterruptionOffset { get; set; }
        public Int32 AutomatOffset { get; set; }
        public Int32 AutomatCount { get; set; }
        public Int32 ProjectileOffset { get; set; }
        public Int32 ProjectileCount { get; set; }
        public array8 RestMovieDay { get; set; }
        public array8 RestMovieNight { get; set; }
        public array56 Unknowne4 { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreActorBinary
    {
        public array32 Name { get; set; }
        public Int16 CurrentXCoordinate { get; set; }
        public Int16 CurrentYCoordinate { get; set; }
        public Int16 DestinationXCoordinate { get; set; }
        public Int16 DestinationYCoordinate { get; set; }
        public Int32 Flags { get; set; }
        public Int16 HasBeenSpawned { get; set; }
        public byte FilenameInitialCharacter { get; set; }
        public byte Unknownef { get; set; }
        public Int32 ActorAnimation { get; set; }
        public Int32 ActorOrientation { get; set; }
        public Int32 ActorRemovalTimer { get; set; }
        public Int16 MovementRestrictionDistance { get; set; }
        public Int16 MovementRestrictionDistanceMoveToObject { get; set; }
        public Int32 ActorAppearenceSchedule { get; set; }
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

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreRegionBinary
    {
        public array32 Name { get; set; }
        public Int16 RegionType { get; set; }
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
        public Int32 Flags { get; set; }
        public Int32 InformationText { get; set; }
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
        public Int32 Unknown2 { get; set; }
        public array32 Unknown3 { get; set; }
        public array8 Sound { get; set; }
        public Int16 TalkLocationXCoordinate { get; set; }
        public Int16 TalkLocationYCoordinate { get; set; }
        public Int32 DialogName { get; set; }
        public array8 DialogFile { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreSpawnPointBinary
    {
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
        public Int16 SpawnMethod { get; set; }
        public Int32 ActorRemovalTime { get; set; }
        public Int16 MovementRestrictionDistance { get; set; }
        public Int16 MovementRestrictionDistanceToObject { get; set; }
        public Int16 MaximumCreaturesToSpawn { get; set; }
        public Int16 Enabled { get; set; }
        public Int32 SpawnPointAppearenceSchedule { get; set; }
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

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreEntranceBinary
    {
        public array32 Name { get; set; }
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public Int16 Orientation { get; set; }
        public array66 Unknown { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreContainerBinary
    {
        public array32 Name { get; set; }
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public Int16 ContainerType { get; set; }
        public Int16 LockDifficulty { get; set; }
        public Int32 Flags { get; set; }
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
        public Int32 ItemIndex { get; set; }
        public Int32 ItemCount { get; set; }
        public array8 TrapScript { get; set; }
        public Int32 VertexIndex { get; set; }
        public Int16 VertexCount { get; set; }
        public Int16 TriggerRange { get; set; }
        public array32 Owner { get; set; }
        public array8 KeyItem { get; set; }
        public Int32 BreakDifficulty { get; set; }
        public Int32 LockpickString { get; set; }
        public array56 Unknown3 { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreItemBinary
    {
        public array8 ItemResref { get; set; }
        public Int16 ExpirationTime { get; set; }
        public Int16 Charges1 { get; set; }
        public Int16 Charges2 { get; set; }
        public Int16 Charges3 { get; set; }
        public Int32 Flags { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreAmbientBinary
    {
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
        public Int16 Unknown2 { get; set; }
        public Int32 FrequencyBase { get; set; }
        public Int32 FrequencyVariation { get; set; }
        public Int32 AmbientAppearenceSchedule { get; set; }
        public Int32 Flags { get; set; }
        public array64 Unknown3 { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreVariableBinary
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
    struct AreDoorBinary
    {
        public array32 Name { get; set; }
        public array8 DoorId { get; set; }
        public Int32 Flags { get; set; }
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
        public Int32 LockpickString { get; set; }
        public array24 TravelTriggerName { get; set; }
        public Int32 DialogName { get; set; }
        public array8 DialogResref { get; set; }
        public array8 Unknown { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreAnimationBinary
    {
        public array32 Name { get; set; }
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public Int32 AnimationAppearenceSchedule { get; set; }
        public array8 BamAnimation { get; set; }
        public Int16 BamSequence { get; set; }
        public Int16 BamFrame { get; set; }
        public Int32 Flags { get; set; }
        public Int16 Height { get; set; }
        public Int16 Transparency { get; set; }
        public Int16 StartFrame { get; set; }
        public byte LoopChance { get; set; }
        public byte SkipCycles { get; set; }
        public array8 Palette { get; set; }
        public Int16 WidthPVRZ { get; set; }
        public Int16 HeightPVRZ { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreNoteBinary
    {
        public Int16 XCoordinate { get; set; }
        public Int16 YCoordinate { get; set; }
        public Int32 Text { get; set; }
        public Int16 Location { get; set; }
        public Int16 Colour { get; set; }
        public Int32 NoteCountPlus { get; set; }
        public array36 Unknown { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreTiledObjectBinary //TODO:
    {
        public array32 Name { get; set; }
        public array8 TileId { get; set; }
        public Int32 Flags { get; set; }
        public Int32 OpenSearchOffset { get; set; }
        public Int16 OpenSearchCount { get; set; }
        public Int16 ClosedSearchCount { get; set; }
        public Int32 ClosedSearchOffset { get; set; }
        public array48 Unknown { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreProjectileBinary
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
    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreSongBinary
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
        public array60 Unknown { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreInterruptionBinary
    {
        public array32 Name { get; set; }
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
        public array56 Unknown { get; set; }
    }
}