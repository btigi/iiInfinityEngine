using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public array8 AreaWED;
        public Int32 LastSaved;
        public Int32 AreaFlags;
        public array8 AreaToTheNorth;
        public Int32 AreaToTheNorthFlags;
        public array8 AreaToTheEast;
        public Int32 AreaToTheEastFlags;
        public array8 AreaToTheSouth;
        public Int32 AreaToTheSouthFlags;
        public array8 AreaToTheWest;
        public Int32 AreaToTheWestFlags;
        public ushort AreaTypeFlags;
        public Int16 WeatherProbabilityRain;
        public Int16 WeatherProbabilitySnow;
        public Int16 WeatherProbabilityFog;
        public Int16 WeatherProbabilityLightning;
        public Int16 OverlayTransparency;
        public Int32 ActorOffset;
        public Int16 ActorCount;
        public Int16 RegionCount;
        public Int32 RegionOffset;
        public Int32 SpawnPointOffset;
        public Int32 SpawnPointCount;
        public Int32 EntrancesOffset;
        public Int32 EntrancesCount;
        public Int32 ContainerOffset;
        public Int16 ContainerCount;
        public Int16 ItemCount;
        public Int32 ItemOffset;
        public Int32 VertexOffset;
        public Int16 VertexCount;
        public Int16 AmbientCount;
        public Int32 AmbientOffset;
        public Int32 VariableOffset;
        public Int32 VariableCount;
        public Int16 TiledObjectFlagOffset;
        public Int16 TiledObjectFlagCount;
        public array8 AreaScript;
        public Int32 ExploredBitmaskSize;
        public Int32 ExploredBitmaskOffset;
        public Int32 DoorCount;
        public Int32 DoorOffset;
        public Int32 AnimationCount;
        public Int32 AnimationOffset;
        public Int32 TiledObjectCount;
        public Int32 TiledObjectOffset;
        public Int32 SongEntryOffset;
        public Int32 RestInterruptionOffset;
        public Int32 AutomatOffset;
        public Int32 AutomatCount;
        public Int32 ProjectileOffset;
        public Int32 ProjectileCount;
        public array8 RestMovieDay;
        public array8 RestMovieNight;
        public array56 Unused;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreActorBinary
    {
        public array32 Name;
        public Int16 CurrentXCoordinate;
        public Int16 CurrentYCoordinate;
        public Int16 DestinationXCoordinate;
        public Int16 DestinationYCoordinate;
        public Int32 Flags;
        public Int16 HasBeenSpawned;
        public byte FilenameInitialCharacter;
        public byte Unknown;
        public Int32 ActorAnimation;
        public Int32 ActorOrientation;
        public Int32 ActorRemovalTimer;
        public Int16 MovementRestrictionDistance;
        public Int16 MovementRestrictionDistanceMoveToObject;
        public Int32 ActorAppearenceSchedule;
        public Int32 NumTimesTalkedTo;
        public array8 Dialog;
        public array8 ScriptOverride;
        public array8 ScriptGeneral;
        public array8 ScriptClass;
        public array8 ScriptRace;
        public array8 ScriptDefault;
        public array8 ScriptSpecific;
        public array8 CREFile;
        public Int32 CreOffset;
        public Int32 CreSize;
        public array128 Unknown2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreRegionBinary
    {
        public array32 Name;
        public Int16 RegionType;
        public Int16 BoundingBoxLeft;
        public Int16 BoundingBoxTop;
        public Int16 BoundingBoxRight;
        public Int16 BoundingBoxBottom;
        public Int16 VertexCount;
        public Int32 VertexIndex;
        public Int32 TriggerValue;
        public Int32 Cursor;
        public array8 DestinationArea;
        public array32 DestinationEntrance;
        public Int32 Flags;
        public Int32 InformationText;
        public Int16 TrapDetectionDifficulty;
        public Int16 TrapRemovalDifficulty;
        public Int16 IsTrap;
        public Int16 TrapDetected;
        public Int16 TrapLaunchXCoordinate;
        public Int16 TrapLaunchYCoordinate;
        public array8 KeyItem;
        public array8 RegionScript;
        public Int16 AlternativeUsePointXCoordinate;
        public Int16 AlternativeUsePointYCoordinate;
        public Int32 Unknown2;
        public array32 Unknown3;
        public array8 Sound;
        public Int16 TalkLocationXCoordinate;
        public Int16 TalkLocationYCoordinate;
        public Int32 DialogName;
        public array8 DialogFile;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreSpawnPointBinary
    {
        public array32 Name;
        public Int16 XCoordinate;
        public Int16 YCoordinate;
        public array8 Resref1;
        public array8 Resref2;
        public array8 Resref3;
        public array8 Resref4;
        public array8 Resref5;
        public array8 Resref6;
        public array8 Resref7;
        public array8 Resref8;
        public array8 Resref9;
        public array8 Resref10;
        public Int16 CreatureSpawnCount;
        public Int16 BaseCreatureNumberToSpawn;
        public Int16 Frequency;
        public Int16 SpawnMethod;
        public Int32 ActorRemovalTime;
        public Int16 MovementRestrictionDistance;
        public Int16 MovementRestrictionDistanceToObject;
        public Int16 MaximumCreaturesToSpawn;
        public Int16 Enabled;
        public Int32 SpawnPointAppearenceSchedule;
        public Int16 ProbabilityDay;
        public Int16 ProbabilityNight;
        public Int32 SpawnFrequency;
        public Int32 Countdown;
        public byte SpawnWeight1;
        public byte SpawnWeight2;
        public byte SpawnWeight3;
        public byte SpawnWeight4;
        public byte SpawnWeight5;
        public byte SpawnWeight6;
        public byte SpawnWeight7;
        public byte SpawnWeight8;
        public byte SpawnWeight9;
        public byte SpawnWeight10;
        public array38 Unknown;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreEntranceBinary
    {
        public array32 Name;
        public Int16 XCoordinate;
        public Int16 YCoordinate;
        public Int16 Orientation;
        public array66 Unknown;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreContainerBinary
    {
        public array32 Name;
        public Int16 XCoordinate;
        public Int16 YCoordinate;
        public Int16 ContainerType;
        public Int16 LockDifficulty;
        public Int32 Flags;
        public Int16 TrapDetectionDifficulty;
        public Int16 TrapRemovalDifficulty;
        public Int16 IsTrap;
        public Int16 TrapDetected;
        public Int16 TrapLaunchXCoordinate;
        public Int16 TrapLaunchYCoordinate;
        public Int16 BoundingBoxLeft;
        public Int16 BoundingBoxTop;
        public Int16 BoundingBoxRight;
        public Int16 BoundingBoxBottom;
        public Int32 ItemIndex;
        public Int32 ItemCount;
        public array8 TrapScript;
        public Int32 VertexIndex;
        public Int16 VertexCount;
        public Int16 TriggerRange;
        public array32 Owner;
        public array8 KeyItem;
        public Int32 BreakDifficulty;
        public Int32 LockpickString;
        public array56 Unknown3;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreItemBinary
    {
        public array8 ItemResref;
        public Int16 ExpirationTime;
        public Int16 Charges1;
        public Int16 Charges2;
        public Int16 Charges3;
        public Int32 Flags;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreAmbientBinary
    {
        public array32 Name;
        public Int16 XCoordinate;
        public Int16 YCoordinate;
        public Int16 Radius;
        public Int16 Height;
        public Int32 PitchVariance;
        public Int16 VolumeVariance;
        public Int16 Volume;
        public array8 Resref1;
        public array8 Resref2;
        public array8 Resref3;
        public array8 Resref4;
        public array8 Resref5;
        public array8 Resref6;
        public array8 Resref7;
        public array8 Resref8;
        public array8 Resref9;
        public array8 Resref10;
        public Int16 ResRefCount;
        public Int16 Unknown2;
        public Int32 FrequencyBase;
        public Int32 FrequencyVariation;
        public Int32 AmbientAppearenceSchedule;
        public Int32 Flags;
        public array64 Unknown3;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreVariableBinary
    {
        public array32 Name;
        public Int16 Type;
        public Int16 ResourceType;
        public Int32 ValueDword;
        public Int32 ValueInt;
        public double ValueDouble;
        public array32 ScriptName;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreDoorBinary
    {
        public array32 Name;
        public array8 DoorId;
        public Int32 Flags;
        public Int32 OpenVertexIndex;
        public Int16 OpenVertexCount;
        public Int16 ClosedVertexCount;
        public Int32 ClosedVertexIndex;
        public Int16 OpenBoundingBoxLeft;
        public Int16 OpenBoundingBoxTop;
        public Int16 OpenBoundingBoxRight;
        public Int16 OpenBoundingBoxBottom;
        public Int16 ClosedBoundingBoxLeft;
        public Int16 ClosedBoundingBoxTop;
        public Int16 ClosedBoundingBoxRight;
        public Int16 ClosedBoundingBoxBottom;
        public Int32 OpenVertexBlockIndex;
        public Int16 OpenVertexBlockCount;
        public Int16 ClosedVertexBlockCount;
        public Int32 ClosedVertexBlockIndex;
        public Int16 Hitpoints;
        public Int16 ArmourClass;
        public array8 DoorOpenSound;
        public array8 DoorCloseSound;
        public Int32 Cursor;
        public Int16 TrapDetectionDifficulty;
        public Int16 TrapRemovalDifficulty;
        public Int16 IsTrap;
        public Int16 TrapDetected;
        public Int16 TrapLaunchXCoordinate;
        public Int16 TrapLaunchYCoordinate;
        public array8 KeyItem;
        public array8 DoorScript;
        public Int32 SecretDoorDetectionDifficulty;
        public Int32 LockDifficulty;
        public Int16 DoorState1X;
        public Int16 DoorState1Y;
        public Int16 DoorState2X;
        public Int16 DoorState2Y;
        public Int32 LockpickString;
        public array24 TravelTriggerName;
        public Int32 DialogName;
        public array8 DialogResref;
        public array8 Unknown;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreAnimationBinary
    {
        public array32 Name;
        public Int16 XCoordinate;
        public Int16 YCoordinate;
        public Int32 AnimationAppearenceSchedule;
        public array8 BamAnimation;
        public Int16 BamSequence;
        public Int16 BamFrame;
        public Int32 Flags;
        public Int16 Height;
        public Int16 Transparency;
        public Int16 StartFrame;
        public byte LoopChance;
        public byte SkipCycles;
        public array8 Palette;
        public Int32 Unknown; //TODO
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreNoteBinary
    {
        public Int16 XCoordinate;
        public Int16 YCoordinate;
        public Int32 Text;
        public Int16 Location;
        public Int16 Colour;
        public Int32 NoteCountPlus;
        public array36 Unknown;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreTiledObjectBinary
    {
        public array32 Name;
        public array32 Unknown1;
        public Int32 Unknown2;
        public Int32 OpenSearchOffset;
        public Int32 OpenSearchCount;
        public Int32 ClosedSearchOffset;
        public Int32 ClosedSearchCount;
        public array48 Unknown3;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreProjectileBinary
    {
        public array8 Resref;
        public Int32 EffectOffset;
        public Int16 EffectSize;
        public Int16 MissileId; // missile.ids or projectile.ids - 1
        public Int16 TickUntilTriggerCheck;
        public Int16 TriggersRemaining;
        public Int16 XCoordinate;
        public Int16 YCoordinate;
        public Int16 ZCoordinate;
        public byte EATarget;
        public byte PartyOwnerIndex;
    }
    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreSongBinary
    {
        public Int32 DaySong;
        public Int32 NightSong;
        public Int32 WinSong;
        public Int32 BattleSong;
        public Int32 LoseSong;
        public Int32 Unknown1;
        public Int32 Unknown2;
        public Int32 Unknown3;
        public Int32 Unknown4;
        public Int32 Unknown5;
        public array8 DayAmbient1Wav;
        public array8 DayAmbient2Wav;
        public Int32 DayAmbientVolume;
        public array8 NightAmbient1Wav;
        public array8 NightAmbient2Wav;
        public Int32 NightAmbientVolume;
        public Int32 Reverb;
        public array60 Unknown6;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct AreInterruptionBinary
    {
        public array32 Name;
        public Int32 Text1;
        public Int32 Text2;
        public Int32 Text3;
        public Int32 Text4;
        public Int32 Text5;
        public Int32 Text6;
        public Int32 Text7;
        public Int32 Text8;
        public Int32 Text9;
        public Int32 Text10;
        public array8 ResRef1;
        public array8 ResRef2;
        public array8 ResRef3;
        public array8 ResRef4;
        public array8 ResRef5;
        public array8 ResRef6;
        public array8 ResRef7;
        public array8 ResRef8;
        public array8 ResRef9;
        public array8 ResRef10;
        public Int16 CreatureCount;
        public Int16 Difficulty;
        public Int32 RemovalTime;
        public Int16 RestrictionDistance;
        public Int16 RestrictionDistanceToObject;
        public Int16 MaximumCreaturesToSpawn;
        public Int16 Enabled;
        public Int16 DayProbability;
        public Int16 NightProbability;
        public array56 Unknown;
    }
}