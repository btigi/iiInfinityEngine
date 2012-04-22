using System.Collections.Generic;
using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using System;

namespace iiInfinityEngine.Core.Readers
{
    public class AreFileBinaryReader : IAreFileReader
    {
        public TlkFile TlkFile { get; set; }

        public AreFile Read(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var f = Read(fs);
                f.Filename = Path.GetFileName(filename);
                return f;
            }
        }

        public AreFile Read(Stream s)
        {
            using (BinaryReader br = new BinaryReader(s))
            {
                var areFile = ParseFile(br);
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                areFile.OriginalFile = ParseFile(br);
                return areFile;
            }
        }

        private AreFile ParseFile(BinaryReader br)
        {
            var header = (AreHeaderBinary)Common.ReadStruct(br, typeof(AreHeaderBinary));

            if (header.ftype.ToString() != "AREA")
                return new AreFile();

            List<AreActorBinary> actors = new List<AreActorBinary>();
            List<AreRegionBinary> regions = new List<AreRegionBinary>();
            List<AreSpawnPointBinary> spawns = new List<AreSpawnPointBinary>();
            List<AreEntranceBinary> entrances = new List<AreEntranceBinary>();
            List<AreContainerBinary> containers = new List<AreContainerBinary>();
            List<AreItemBinary> items = new List<AreItemBinary>();
            List<AreAmbientBinary> ambients = new List<AreAmbientBinary>();
            List<AreVariableBinary> variables = new List<AreVariableBinary>();
            List<AreDoorBinary> doors = new List<AreDoorBinary>();
            List<AreAnimationBinary> animations = new List<AreAnimationBinary>();
            List<AreNoteBinary> notes = new List<AreNoteBinary>();
            List<AreTiledObjectBinary> tiledObjects = new List<AreTiledObjectBinary>();
            List<AreProjectileBinary> projectiles = new List<AreProjectileBinary>();
            List<AreSongBinary> songs = new List<AreSongBinary>();
            List<AreInterruptionBinary> interruptions = new List<AreInterruptionBinary>();
            List<bool> exploredArea = new List<bool>();
            List<Int32> vertices = new List<Int32>();

            br.BaseStream.Seek(header.ActorOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.ActorCount; i++)
            {
                var actor = (AreActorBinary)Common.ReadStruct(br, typeof(AreActorBinary));
                actors.Add(actor);
            }

            br.BaseStream.Seek(header.RegionOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.RegionCount; i++)
            {
                var region = (AreRegionBinary)Common.ReadStruct(br, typeof(AreRegionBinary));
                regions.Add(region);
            }

            br.BaseStream.Seek(header.SpawnPointOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.SpawnPointCount; i++)
            {
                var spawn = (AreSpawnPointBinary)Common.ReadStruct(br, typeof(AreSpawnPointBinary));
                spawns.Add(spawn);
            }

            br.BaseStream.Seek(header.EntrancesOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.EntrancesCount; i++)
            {
                var entrance = (AreEntranceBinary)Common.ReadStruct(br, typeof(AreEntranceBinary));
                entrances.Add(entrance);
            }

            br.BaseStream.Seek(header.ContainerOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.ContainerCount; i++)
            {
                var container = (AreContainerBinary)Common.ReadStruct(br, typeof(AreContainerBinary));
                containers.Add(container);
            }

            br.BaseStream.Seek(header.ItemOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.ItemCount; i++)
            {
                var item = (AreItemBinary)Common.ReadStruct(br, typeof(AreItemBinary));
                items.Add(item);
            }

            br.BaseStream.Seek(header.AmbientOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.AmbientCount; i++)
            {
                var ambient = (AreAmbientBinary)Common.ReadStruct(br, typeof(AreAmbientBinary));
                ambients.Add(ambient);
            }

            br.BaseStream.Seek(header.VariableOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.VariableCount; i++)
            {
                var variable = (AreVariableBinary)Common.ReadStruct(br, typeof(AreVariableBinary));
                variables.Add(variable);
            }

            br.BaseStream.Seek(header.DoorOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.DoorCount; i++)
            {
                var door = (AreDoorBinary)Common.ReadStruct(br, typeof(AreDoorBinary));
                doors.Add(door);
            }

            br.BaseStream.Seek(header.AnimationOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.AnimationCount; i++)
            {
                var animation = (AreAnimationBinary)Common.ReadStruct(br, typeof(AreAnimationBinary));
                animations.Add(animation);
            }

            br.BaseStream.Seek(header.AutomatOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.AutomatCount; i++)
            {
                var note = (AreNoteBinary)Common.ReadStruct(br, typeof(AreNoteBinary));
                notes.Add(note);
            }

            br.BaseStream.Seek(header.TiledObjectOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.TiledObjectCount; i++)
            {
                var tiledObject = (AreTiledObjectBinary)Common.ReadStruct(br, typeof(AreTiledObjectBinary));
                tiledObjects.Add(tiledObject);
            }

            br.BaseStream.Seek(header.ProjectileOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.ProjectileCount; i++)
            {
                var projectile = (AreProjectileBinary)Common.ReadStruct(br, typeof(AreProjectileBinary));
                projectiles.Add(projectile);
            }

            br.BaseStream.Seek(header.SongEntryOffset, SeekOrigin.Begin);
            for (int i = 0; i < 1; i++) // ?
            {
                var song = (AreSongBinary)Common.ReadStruct(br, typeof(AreSongBinary));
                songs.Add(song);
            }

            br.BaseStream.Seek(header.RestInterruptionOffset, SeekOrigin.Begin);
            for (int i = 0; i < 1; i++) // ?
            {
                var interruption = (AreInterruptionBinary)Common.ReadStruct(br, typeof(AreInterruptionBinary));
                interruptions.Add(interruption);
            }

            // Assume that we'll always have a multiple of a byte
            br.BaseStream.Seek(header.ExploredBitmaskOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.ExploredBitmaskSize / 8; i++) // ?
            {
                var exploration = (byte)Common.ReadStruct(br, typeof(byte));
                bool explored = false;
                if ((exploration & Common.Bit0) != 0)
                    explored = true;
                exploredArea.Add(explored);

                if ((exploration & Common.Bit1) != 0)
                    explored = true;
                exploredArea.Add(explored);

                if ((exploration & Common.Bit2) != 0)
                    explored = true;
                exploredArea.Add(explored);

                if ((exploration & Common.Bit3) != 0)
                    explored = true;
                exploredArea.Add(explored);

                if ((exploration & Common.Bit4) != 0)
                    explored = true;
                exploredArea.Add(explored);

                if ((exploration & Common.Bit5) != 0)
                    explored = true;
                exploredArea.Add(explored);

                if ((exploration & Common.Bit6) != 0)
                    explored = true;
                exploredArea.Add(explored);

                if ((exploration & Common.Bit7) != 0)
                    explored = true;
                exploredArea.Add(explored);
            }

            br.BaseStream.Seek(header.VertexOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.VertexCount; i++)
            {
                var vertex = (Int32)Common.ReadStruct(br, typeof(Int32));
                vertices.Add(vertex);
            }

            var areFile = new AreFile();
            areFile.AreaFlags.SaveAllowed = (header.AreaFlags & Common.Bit0) != 0;
            areFile.AreaFlags.TutorialArea = (header.AreaFlags & Common.Bit1) != 0;
            areFile.AreaFlags.DeadMagicZone = (header.AreaFlags & Common.Bit2) != 0;
            areFile.AreaFlags.Dream = (header.AreaFlags & Common.Bit3) != 0;
            areFile.AreaFlags.Bit04 = (header.AreaFlags & Common.Bit4) != 0;
            areFile.AreaFlags.Bit05 = (header.AreaFlags & Common.Bit5) != 0;
            areFile.AreaFlags.Bit06 = (header.AreaFlags & Common.Bit6) != 0;
            areFile.AreaFlags.Bit07 = (header.AreaFlags & Common.Bit7) != 0;
            areFile.AreaFlags.Bit08 = (header.AreaFlags & Common.Bit8) != 0;
            areFile.AreaFlags.Bit09 = (header.AreaFlags & Common.Bit9) != 0;
            areFile.AreaFlags.Bit10 = (header.AreaFlags & Common.Bit10) != 0;
            areFile.AreaFlags.Bit11 = (header.AreaFlags & Common.Bit11) != 0;
            areFile.AreaFlags.Bit12 = (header.AreaFlags & Common.Bit12) != 0;
            areFile.AreaFlags.Bit13 = (header.AreaFlags & Common.Bit13) != 0;
            areFile.AreaFlags.Bit14 = (header.AreaFlags & Common.Bit14) != 0;
            areFile.AreaFlags.Bit15 = (header.AreaFlags & Common.Bit15) != 0;
            areFile.AreaFlags.Bit16 = (header.AreaFlags & Common.Bit16) != 0;
            areFile.AreaFlags.Bit17 = (header.AreaFlags & Common.Bit17) != 0;
            areFile.AreaFlags.Bit18 = (header.AreaFlags & Common.Bit18) != 0;
            areFile.AreaFlags.Bit19 = (header.AreaFlags & Common.Bit19) != 0;
            areFile.AreaFlags.Bit20 = (header.AreaFlags & Common.Bit20) != 0;
            areFile.AreaFlags.Bit21 = (header.AreaFlags & Common.Bit21) != 0;
            areFile.AreaFlags.Bit22 = (header.AreaFlags & Common.Bit22) != 0;
            areFile.AreaFlags.Bit23 = (header.AreaFlags & Common.Bit23) != 0;
            areFile.AreaFlags.Bit24 = (header.AreaFlags & Common.Bit24) != 0;
            areFile.AreaFlags.Bit25 = (header.AreaFlags & Common.Bit25) != 0;
            areFile.AreaFlags.Bit26 = (header.AreaFlags & Common.Bit26) != 0;
            areFile.AreaFlags.Bit27 = (header.AreaFlags & Common.Bit27) != 0;
            areFile.AreaFlags.Bit28 = (header.AreaFlags & Common.Bit28) != 0;
            areFile.AreaFlags.Bit29 = (header.AreaFlags & Common.Bit29) != 0;
            areFile.AreaFlags.Bit30 = (header.AreaFlags & Common.Bit30) != 0;
            areFile.AreaFlags.Bit31 = (header.AreaFlags & Common.Bit31) != 0;

            areFile.AreaTypeFlags.Outdoor = (header.AreaTypeFlags & Common.Bit0) != 0;
            areFile.AreaTypeFlags.DayNight = (header.AreaTypeFlags & Common.Bit1) != 0;
            areFile.AreaTypeFlags.Weather = (header.AreaTypeFlags & Common.Bit2) != 0;
            areFile.AreaTypeFlags.City = (header.AreaTypeFlags & Common.Bit3) != 0;
            areFile.AreaTypeFlags.Forest = (header.AreaTypeFlags & Common.Bit4) != 0;
            areFile.AreaTypeFlags.Dungeon = (header.AreaTypeFlags & Common.Bit5) != 0;
            areFile.AreaTypeFlags.ExtendedNight = (header.AreaTypeFlags & Common.Bit6) != 0;
            areFile.AreaTypeFlags.CanRestIndoors = (header.AreaTypeFlags & Common.Bit7) != 0;
            areFile.AreaTypeFlags.Bit08 = (header.AreaTypeFlags & Common.Bit8) != 0;
            areFile.AreaTypeFlags.Bit09 = (header.AreaTypeFlags & Common.Bit9) != 0;
            areFile.AreaTypeFlags.Bit10 = (header.AreaTypeFlags & Common.Bit10) != 0;
            areFile.AreaTypeFlags.Bit11 = (header.AreaTypeFlags & Common.Bit11) != 0;
            areFile.AreaTypeFlags.Bit12 = (header.AreaTypeFlags & Common.Bit12) != 0;
            areFile.AreaTypeFlags.Bit13 = (header.AreaTypeFlags & Common.Bit13) != 0;
            areFile.AreaTypeFlags.Bit14 = (header.AreaTypeFlags & Common.Bit14) != 0;
            areFile.AreaTypeFlags.Bit15 = (header.AreaTypeFlags & Common.Bit15) != 0;

            areFile.AreaScript = header.AreaScript.ToString();
            areFile.AreaToTheEast = header.AreaToTheEast.ToString();
            areFile.AreaToTheNorth = header.AreaToTheNorth.ToString();
            areFile.AreaToTheSouth = header.AreaToTheSouth.ToString();
            areFile.AreaToTheWest = header.AreaToTheWest.ToString();
            areFile.AreaWED = header.AreaWED.ToString();
            areFile.LastSaved = header.LastSaved;
            areFile.RestMovieDay = header.RestMovieDay.ToString();
            areFile.RestMovieNight = header.RestMovieNight.ToString();
            areFile.Unknown1 = header.Unknown1;
            areFile.Unknown2 = header.Unknown2;
            areFile.Unknown3 = header.Unknown3;
            areFile.Unknown4 = header.Unknown4;
            areFile.Unknown5 = header.Unknown5;
            areFile.Unknown6 = header.Unknown6;
            areFile.Unknown7 = header.Unknown7;
            areFile.WeatherProbabilityFog = header.WeatherProbabilityFog;
            areFile.WeatherProbabilityLightning = header.WeatherProbabilityLightning;
            areFile.WeatherProbabilityRain = header.WeatherProbabilityRain;
            areFile.WeatherProbabilitySnow = header.WeatherProbabilitySnow;

            foreach (var actor in actors)
            {
                var areActor2 = new AreActor2();
                areActor2.ActorFlags.CreAttached = (actor.Flags & Common.Bit0) != 0;
                areActor2.ActorFlags.Bit01 = (actor.Flags & Common.Bit1) != 0;
                areActor2.ActorFlags.Bit02 = (actor.Flags & Common.Bit2) != 0;
                areActor2.ActorFlags.OverrideScriptName = (actor.Flags & Common.Bit3) != 0;
                areActor2.ActorFlags.Bit04 = (actor.Flags & Common.Bit4) != 0;
                areActor2.ActorFlags.Bit05 = (actor.Flags & Common.Bit5) != 0;
                areActor2.ActorFlags.Bit06 = (actor.Flags & Common.Bit6) != 0;
                areActor2.ActorFlags.Bit07 = (actor.Flags & Common.Bit7) != 0;
                areActor2.ActorFlags.Bit08 = (actor.Flags & Common.Bit8) != 0;
                areActor2.ActorFlags.Bit09 = (actor.Flags & Common.Bit9) != 0;
                areActor2.ActorFlags.Bit10 = (actor.Flags & Common.Bit10) != 0;
                areActor2.ActorFlags.Bit11 = (actor.Flags & Common.Bit11) != 0;
                areActor2.ActorFlags.Bit12 = (actor.Flags & Common.Bit12) != 0;
                areActor2.ActorFlags.Bit13 = (actor.Flags & Common.Bit13) != 0;
                areActor2.ActorFlags.Bit14 = (actor.Flags & Common.Bit14) != 0;
                areActor2.ActorFlags.Bit15 = (actor.Flags & Common.Bit15) != 0;
                areActor2.ActorFlags.Bit16 = (actor.Flags & Common.Bit16) != 0;
                areActor2.ActorFlags.Bit17 = (actor.Flags & Common.Bit17) != 0;
                areActor2.ActorFlags.Bit18 = (actor.Flags & Common.Bit18) != 0;
                areActor2.ActorFlags.Bit19 = (actor.Flags & Common.Bit19) != 0;
                areActor2.ActorFlags.Bit20 = (actor.Flags & Common.Bit20) != 0;
                areActor2.ActorFlags.Bit21 = (actor.Flags & Common.Bit21) != 0;
                areActor2.ActorFlags.Bit22 = (actor.Flags & Common.Bit22) != 0;
                areActor2.ActorFlags.Bit23 = (actor.Flags & Common.Bit23) != 0;
                areActor2.ActorFlags.Bit24 = (actor.Flags & Common.Bit24) != 0;
                areActor2.ActorFlags.Bit25 = (actor.Flags & Common.Bit25) != 0;
                areActor2.ActorFlags.Bit26 = (actor.Flags & Common.Bit26) != 0;
                areActor2.ActorFlags.Bit27 = (actor.Flags & Common.Bit27) != 0;
                areActor2.ActorFlags.Bit28 = (actor.Flags & Common.Bit28) != 0;
                areActor2.ActorFlags.Bit29 = (actor.Flags & Common.Bit29) != 0;
                areActor2.ActorFlags.Bit30 = (actor.Flags & Common.Bit30) != 0;
                areActor2.ActorFlags.Bit31 = (actor.Flags & Common.Bit31) != 0;

                areActor2.ActorAnimation = actor.ActorAnimation;
                areActor2.ActorAppearenceSchedule = actor.ActorAppearenceSchedule;
                areActor2.ActorOrientation = actor.ActorOrientation;
                areActor2.ActorRemovalTimer = actor.ActorRemovalTimer;
                areActor2.CREFile = actor.CREFile.ToString();
                areActor2.CreOffset = actor.CreOffset;
                areActor2.CreSize = actor.CreSize;
                areActor2.CurrentXCoordinate = actor.CurrentXCoordinate;
                areActor2.CurrentYCoordinate = actor.CurrentYCoordinate;
                areActor2.DestinationXCoordinate = actor.DestinationXCoordinate;
                areActor2.DestinationYCoordinate = actor.DestinationYCoordinate;
                areActor2.FilenameInitialCharacter = actor.FilenameInitialCharacter;
                areActor2.HasBeenSpawned = actor.HasBeenSpawned;
                areActor2.MovementRestrictionDistance = actor.MovementRestrictionDistance;
                areActor2.MovementRestrictionDistanceMoveToObject = actor.MovementRestrictionDistanceMoveToObject;
                areActor2.Name = actor.Name.ToString();
                areActor2.NumTimesTalkedTo = actor.NumTimesTalkedTo;
                areActor2.ScriptClass = actor.ScriptClass.ToString();
                areActor2.ScriptDefault = actor.ScriptDefault.ToString();
                areActor2.ScriptGeneral = actor.ScriptGeneral.ToString();
                areActor2.ScriptOverride = actor.ScriptOverride.ToString();
                areActor2.ScriptRace = actor.ScriptRace.ToString();
                areActor2.ScriptSpecific = actor.ScriptSpecific.ToString();
                areActor2.Unknown = actor.Unknown;
                areActor2.Unknown2 = actor.Unknown2;
                areFile.actors.Add(areActor2);
            }

            foreach (var region in regions)
            {
                var region2 = new AreRegion2();
                region2.Flags.InvisibleTrap = (region.Flags & Common.Bit0) != 0;
                region2.Flags.ResetTrap = (region.Flags & Common.Bit1) != 0;
                region2.Flags.PartyRequired = (region.Flags & Common.Bit2) != 0;
                region2.Flags.Detectable = (region.Flags & Common.Bit3) != 0;
                region2.Flags.Bit04 = (region.Flags & Common.Bit4) != 0;
                region2.Flags.Bit05 = (region.Flags & Common.Bit5) != 0;
                region2.Flags.NPCCanTrigger = (region.Flags & Common.Bit6) != 0;
                region2.Flags.Bit07 = (region.Flags & Common.Bit7) != 0;
                region2.Flags.Deactivated = (region.Flags & Common.Bit8) != 0;
                region2.Flags.NPCCannotPass = (region.Flags & Common.Bit9) != 0;
                region2.Flags.AlternativePoint = (region.Flags & Common.Bit10) != 0;
                region2.Flags.UsedByDoor = (region.Flags & Common.Bit11) != 0;
                region2.Flags.Bit12 = (region.Flags & Common.Bit12) != 0;
                region2.Flags.Bit13 = (region.Flags & Common.Bit13) != 0;
                region2.Flags.Bit14 = (region.Flags & Common.Bit14) != 0;
                region2.Flags.Bit15 = (region.Flags & Common.Bit15) != 0;
                region2.Flags.Bit16 = (region.Flags & Common.Bit16) != 0;
                region2.Flags.Bit17 = (region.Flags & Common.Bit17) != 0;
                region2.Flags.Bit18 = (region.Flags & Common.Bit18) != 0;
                region2.Flags.Bit19 = (region.Flags & Common.Bit19) != 0;
                region2.Flags.Bit20 = (region.Flags & Common.Bit20) != 0;
                region2.Flags.Bit21 = (region.Flags & Common.Bit21) != 0;
                region2.Flags.Bit22 = (region.Flags & Common.Bit22) != 0;
                region2.Flags.Bit23 = (region.Flags & Common.Bit23) != 0;
                region2.Flags.Bit24 = (region.Flags & Common.Bit24) != 0;
                region2.Flags.Bit25 = (region.Flags & Common.Bit25) != 0;
                region2.Flags.Bit26 = (region.Flags & Common.Bit26) != 0;
                region2.Flags.Bit27 = (region.Flags & Common.Bit27) != 0;
                region2.Flags.Bit28 = (region.Flags & Common.Bit28) != 0;
                region2.Flags.Bit29 = (region.Flags & Common.Bit29) != 0;
                region2.Flags.Bit30 = (region.Flags & Common.Bit30) != 0;
                region2.Flags.Bit31 = (region.Flags & Common.Bit31) != 0;

                region2.AlternativeUsePointXCoordinate = region.AlternativeUsePointXCoordinate;
                region2.AlternativeUsePointYCoordinate = region.AlternativeUsePointYCoordinate;
                region2.BoundingBoxBottom = region.BoundingBoxBottom;
                region2.BoundingBoxLeft = region.BoundingBoxLeft;
                region2.BoundingBoxRight = region.BoundingBoxRight;
                region2.BoundingBoxTop = region.BoundingBoxTop;
                region2.Cursor = region.Cursor;
                region2.DestinationArea = region.DestinationArea.ToString();
                region2.DestinationEntrance = region.DestinationEntrance.ToString();
                region2.DialogFile = region.DialogFile.ToString();
                region2.DialogName = Common.ReadString(region.DialogName, TlkFile);
                region2.InformationText = Common.ReadString(region.InformationText, TlkFile);
                region2.IsTrap = region.IsTrap;
                region2.KeyItem = region.KeyItem.ToString();
                region2.Name = region.Name.ToString();
                region2.RegionScript = region.RegionScript.ToString();
                region2.RegionType = (RegionType)region.RegionType;
                region2.Sound = region.Sound.ToString();
                region2.TalkLocationXCoordinate = region.TalkLocationXCoordinate;
                region2.TalkLocationYCoordinate = region.TalkLocationYCoordinate;
                region2.TrapDetected = region.TrapDetected;
                region2.TrapDetectionDifficulty = region.TrapDetectionDifficulty;
                region2.TrapLaunchXCoordinate = region.TrapLaunchXCoordinate;
                region2.TrapLaunchYCoordinate = region.TrapLaunchYCoordinate;
                region2.TrapRemovalDifficulty = region.TrapRemovalDifficulty;
                region2.Unknown1 = region.Unknown1;
                region2.Unknown2 = region.Unknown2;
                region2.Unknown3 = region.Unknown3;
                region2.VertexCount = region.VertexCount;//xx
                region2.VertexIndex = region.VertexIndex;//xx
                areFile.regions.Add(region2);
            }

            foreach (var spawn in spawns)
            {
                var spawn2 = new AreSpawnPoint2();
                spawn2.ActorRemovalTime = spawn.ActorRemovalTime;
                spawn2.BaseCreatureNumberToSpawn = spawn.BaseCreatureNumberToSpawn;
                spawn2.CreatureSpawnCount = spawn.CreatureSpawnCount;
                spawn2.Enabled = spawn.Enabled;
                spawn2.Frequency = spawn.Frequency;
                spawn2.MaximumCreaturesToSpawn = spawn.MaximumCreaturesToSpawn;
                spawn2.MovementRestrictionDistance = spawn.MovementRestrictionDistance;
                spawn2.MovementRestrictionDistanceToObject = spawn.MovementRestrictionDistanceToObject;
                spawn2.Name = spawn.Name.ToString();
                spawn2.ProbabilityDay = spawn.ProbabilityDay;
                spawn2.ProbabilityNight = spawn.ProbabilityNight;
                spawn2.Resref1 = spawn.Resref1.ToString();
                spawn2.Resref2 = spawn.Resref2.ToString();
                spawn2.Resref3 = spawn.Resref3.ToString();
                spawn2.Resref4 = spawn.Resref4.ToString();
                spawn2.Resref5 = spawn.Resref5.ToString();
                spawn2.Resref6 = spawn.Resref6.ToString();
                spawn2.Resref7 = spawn.Resref7.ToString();
                spawn2.Resref8 = spawn.Resref8.ToString();
                spawn2.Resref9 = spawn.Resref9.ToString();
                spawn2.Resref10 = spawn.Resref10.ToString();
                spawn2.SpawnMethod = spawn.SpawnMethod;
                spawn2.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule;
                spawn2.Unknown = spawn.Unknown;
                spawn2.XCoordinate = spawn.XCoordinate;
                spawn2.YCoordinate = spawn.YCoordinate;
                areFile.spawns.Add(spawn2);
            }

            foreach (var entrance in entrances)
            {
                var entrance2 = new AreEntrance2();
                entrance2.Name = entrance.Name.ToString();
                entrance2.Orientation = entrance.Orientation;
                entrance2.Unknown = entrance.Unknown;
                entrance2.XCoordinate = entrance.XCoordinate;
                entrance2.YCoordinate = entrance.YCoordinate;
                areFile.entrances.Add(entrance2);
            }

            foreach (var container in containers)
            {
                var container2 = new AreContainer2();

                container2.Flags.Locked = (container.Flags & Common.Bit0) != 0;
                container2.Flags.Bit01 = (container.Flags & Common.Bit1) != 0;
                container2.Flags.Bit02 = (container.Flags & Common.Bit2) != 0;
                container2.Flags.TrapResets = (container.Flags & Common.Bit3) != 0;
                container2.Flags.Bit04 = (container.Flags & Common.Bit4) != 0;
                container2.Flags.Disabled = (container.Flags & Common.Bit5) != 0;
                container2.Flags.Bit06 = (container.Flags & Common.Bit6) != 0;
                container2.Flags.Bit07 = (container.Flags & Common.Bit7) != 0;
                container2.Flags.Bit08 = (container.Flags & Common.Bit8) != 0;
                container2.Flags.Bit09 = (container.Flags & Common.Bit9) != 0;
                container2.Flags.Bit10 = (container.Flags & Common.Bit10) != 0;
                container2.Flags.Bit11 = (container.Flags & Common.Bit11) != 0;
                container2.Flags.Bit12 = (container.Flags & Common.Bit12) != 0;
                container2.Flags.Bit13 = (container.Flags & Common.Bit13) != 0;
                container2.Flags.Bit14 = (container.Flags & Common.Bit14) != 0;
                container2.Flags.Bit15 = (container.Flags & Common.Bit15) != 0;
                container2.Flags.Bit16 = (container.Flags & Common.Bit16) != 0;
                container2.Flags.Bit17 = (container.Flags & Common.Bit17) != 0;
                container2.Flags.Bit18 = (container.Flags & Common.Bit18) != 0;
                container2.Flags.Bit19 = (container.Flags & Common.Bit19) != 0;
                container2.Flags.Bit20 = (container.Flags & Common.Bit20) != 0;
                container2.Flags.Bit21 = (container.Flags & Common.Bit21) != 0;
                container2.Flags.Bit22 = (container.Flags & Common.Bit22) != 0;
                container2.Flags.Bit23 = (container.Flags & Common.Bit23) != 0;
                container2.Flags.Bit24 = (container.Flags & Common.Bit24) != 0;
                container2.Flags.Bit25 = (container.Flags & Common.Bit25) != 0;
                container2.Flags.Bit26 = (container.Flags & Common.Bit26) != 0;
                container2.Flags.Bit27 = (container.Flags & Common.Bit27) != 0;
                container2.Flags.Bit28 = (container.Flags & Common.Bit28) != 0;
                container2.Flags.Bit29 = (container.Flags & Common.Bit29) != 0;
                container2.Flags.Bit30 = (container.Flags & Common.Bit30) != 0;
                container2.Flags.Bit31 = (container.Flags & Common.Bit31) != 0;

                container2.BoundingBoxBottom = container.BoundingBoxBottom;
                container2.BoundingBoxLeft = container.BoundingBoxLeft;
                container2.BoundingBoxRight = container.BoundingBoxRight;
                container2.BoundingBoxTop = container.BoundingBoxTop;
                container2.ContainerType = (ContainerType)container.ContainerType;
                container2.IsTrap = container.IsTrap;
                container2.KeyItem = container.KeyItem.ToString();
                container2.LockDifficulty = container.LockDifficulty;
                container2.LockpickString = Common.ReadString(container.LockpickString, TlkFile);
                container2.Name = container.Name.ToString();
                container2.TrapDetected = container.TrapDetected;
                container2.TrapDetectionDifficulty = container.TrapDetectionDifficulty;
                container2.TrapLaunchXCoordinate = container.TrapLaunchXCoordinate;
                container2.TrapLaunchYCoordinate = container.TrapLaunchYCoordinate;
                container2.TrapRemovalDifficulty = container.TrapRemovalDifficulty;
                container2.TrapScript = container.TrapScript.ToString();
                container2.Unknown1 = container.Unknown1;
                container2.Unknown2 = container.Unknown2;
                container2.Unknown3 = container.Unknown3;
                container2.VertexCount = container.VertexCount;//xx
                container2.VertexIndex = container.VertexIndex;//xx
                container2.XCoordinate = container.XCoordinate;
                container2.YCoordinate = container.YCoordinate;

                for (int i = 0; i < container.ItemCount; i++)
                {
                    var item = new AreItem2();
                    item.Charges1 = items[container.ItemIndex + 0].Charges1;
                    item.Charges2 = items[container.ItemIndex + 0].Charges2;
                    item.Charges3 = items[container.ItemIndex + 0].Charges3;
                    item.ExpirationTime = items[container.ItemIndex + 0].ExpirationTime;
                    item.Flags.Identified = (items[container.ItemIndex + 0].Flags & Common.Bit0) != 0;
                    item.Flags.Unstealable = (items[container.ItemIndex + 0].Flags & Common.Bit1) != 0;
                    item.Flags.Stolen = (items[container.ItemIndex + 0].Flags & Common.Bit2) != 0;
                    item.Flags.Unstealable = (items[container.ItemIndex + 0].Flags & Common.Bit3) != 0;
                    item.Flags.Bit04 = (items[container.ItemIndex + 0].Flags & Common.Bit4) != 0;
                    item.Flags.Bit05 = (items[container.ItemIndex + 0].Flags & Common.Bit5) != 0;
                    item.Flags.Bit06 = (items[container.ItemIndex + 0].Flags & Common.Bit6) != 0;
                    item.Flags.Bit07 = (items[container.ItemIndex + 0].Flags & Common.Bit7) != 0;
                    item.Flags.Bit08 = (items[container.ItemIndex + 0].Flags & Common.Bit8) != 0;
                    item.Flags.Bit09 = (items[container.ItemIndex + 0].Flags & Common.Bit9) != 0;
                    item.Flags.Bit10 = (items[container.ItemIndex + 0].Flags & Common.Bit10) != 0;
                    item.Flags.Bit11 = (items[container.ItemIndex + 0].Flags & Common.Bit11) != 0;
                    item.Flags.Bit12 = (items[container.ItemIndex + 0].Flags & Common.Bit12) != 0;
                    item.Flags.Bit13 = (items[container.ItemIndex + 0].Flags & Common.Bit13) != 0;
                    item.Flags.Bit14 = (items[container.ItemIndex + 0].Flags & Common.Bit14) != 0;
                    item.Flags.Bit15 = (items[container.ItemIndex + 0].Flags & Common.Bit15) != 0;
                    item.Flags.Bit16 = (items[container.ItemIndex + 0].Flags & Common.Bit16) != 0;
                    item.Flags.Bit17 = (items[container.ItemIndex + 0].Flags & Common.Bit17) != 0;
                    item.Flags.Bit18 = (items[container.ItemIndex + 0].Flags & Common.Bit18) != 0;
                    item.Flags.Bit19 = (items[container.ItemIndex + 0].Flags & Common.Bit19) != 0;
                    item.Flags.Bit20 = (items[container.ItemIndex + 0].Flags & Common.Bit20) != 0;
                    item.Flags.Bit21 = (items[container.ItemIndex + 0].Flags & Common.Bit21) != 0;
                    item.Flags.Bit22 = (items[container.ItemIndex + 0].Flags & Common.Bit22) != 0;
                    item.Flags.Bit23 = (items[container.ItemIndex + 0].Flags & Common.Bit23) != 0;
                    item.Flags.Bit24 = (items[container.ItemIndex + 0].Flags & Common.Bit24) != 0;
                    item.Flags.Bit25 = (items[container.ItemIndex + 0].Flags & Common.Bit25) != 0;
                    item.Flags.Bit26 = (items[container.ItemIndex + 0].Flags & Common.Bit26) != 0;
                    item.Flags.Bit27 = (items[container.ItemIndex + 0].Flags & Common.Bit27) != 0;
                    item.Flags.Bit28 = (items[container.ItemIndex + 0].Flags & Common.Bit28) != 0;
                    item.Flags.Bit29 = (items[container.ItemIndex + 0].Flags & Common.Bit29) != 0;
                    item.Flags.Bit30 = (items[container.ItemIndex + 0].Flags & Common.Bit30) != 0;
                    item.Flags.Bit31 = (items[container.ItemIndex + 0].Flags & Common.Bit31) != 0;
                    item.ItemResref = items[container.ItemIndex + 0].ItemResref.ToString();
                    container2.items.Add(item);
                }

                areFile.containers.Add(container2);
            }
            /*
            foreach (var item in items)
            {
                var item2 = new AreItem2();
                item2.Charges1 = item.Charges1;
                item2.Charges2 = item.Charges2;
                item2.Charges3 = item.Charges3;
                item2.ExpirationTime = item.ExpirationTime;
                item2.Flags = item.Flags;//xx AreaItemFlags
                item2.ItemResref = item.ItemResref.ToString();
                areFile.items.Add(item2);
            }
            */
            foreach (var ambient in ambients)
            {
                var ambient2 = new AreAmbient2();
                ambient2.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule;//xx
                ambient2.Flags = ambient.Flags;//xx
                ambient2.FrequencyBase = ambient.FrequencyBase;
                ambient2.FrequencyVariation = ambient.FrequencyVariation;
                ambient2.Height = ambient.Height;
                ambient2.Name = ambient.Name.ToString();
                ambient2.Radius = ambient.Radius;
                ambient2.Resref1 = ambient.Resref1.ToString();
                ambient2.Resref2 = ambient.Resref2.ToString();
                ambient2.Resref3 = ambient.Resref3.ToString();
                ambient2.Resref4 = ambient.Resref4.ToString();
                ambient2.Resref5 = ambient.Resref5.ToString();
                ambient2.Resref6 = ambient.Resref6.ToString();
                ambient2.Resref7 = ambient.Resref7.ToString();
                ambient2.Resref8 = ambient.Resref8.ToString();
                ambient2.Resref9 = ambient.Resref9.ToString();
                ambient2.Resref10 = ambient.Resref10.ToString();
                //ambient2.ResRefCount = ambient.ResRefCount;//xx
                ambient2.Unknown1 = ambient.Unknown1;
                ambient2.Unknown2 = ambient.Unknown2;
                ambient2.Unknown3 = ambient.Unknown3;
                ambient2.Volume = ambient.Volume;
                ambient2.XCoordinate = ambient.XCoordinate;
                ambient2.YCoordinate = ambient.YCoordinate;
                areFile.ambients.Add(ambient2);
            }

            foreach (var variable in variables)
            {
                var variable2 = new AreVariable2();
                variable2.Name = variable.Name.ToString();
                variable2.Unknown1 = variable.Unknown1;
                variable2.Unknown2 = variable.Unknown2;
                variable2.Value = variable.Value;
                areFile.variables.Add(variable2);
            }

            foreach (var door in doors)
            {
                var door2 = new AreDoor2();
                door2.ClosedBoundingBoxBottom = door.ClosedBoundingBoxBottom;
                door2.ClosedBoundingBoxLeft = door.ClosedBoundingBoxLeft;
                door2.ClosedBoundingBoxRight = door.ClosedBoundingBoxRight;
                door2.ClosedBoundingBoxTop = door.ClosedBoundingBoxTop;
                door2.ClosedVertexBlockCount = door.ClosedVertexBlockCount;//xx
                door2.ClosedVertexBlockIndex = door.ClosedVertexBlockIndex;//xx
                door2.ClosedVertexCount = door.ClosedVertexCount;//xx
                door2.ClosedVertexIndex = door.ClosedVertexIndex;//xx
                door2.Cursor = door.Cursor;
                door2.DialogName = Common.ReadString(door.DialogName, TlkFile);
                door2.DialogResref = door.DialogResref.ToString();
                door2.DoorCloseSound = door.DoorCloseSound.ToString();
                door2.DoorId = door.DoorId.ToString();
                door2.DoorOpenSound = door.DoorOpenSound.ToString();
                door2.DoorScript = door.DoorScript.ToString();
                door2.DoorState1X = door.DoorState1X;
                door2.DoorState1Y = door.DoorState1Y;
                door2.DoorState2X = door.DoorState2X;
                door2.DoorState2Y = door.DoorState2Y;
                door2.Flags = door.Flags;//xx
                door2.IsTrap = door.IsTrap;
                door2.KeyItem = door.KeyItem.ToString();
                door2.LockDifficulty = door.LockDifficulty;
                door2.LockpickString = Common.ReadString(door.LockpickString, TlkFile);
                door2.Name = door.Name.ToString();
                door2.OpenBoundingBoxBottom = door.OpenBoundingBoxBottom;
                door2.OpenBoundingBoxLeft = door.OpenBoundingBoxLeft;
                door2.OpenBoundingBoxRight = door.OpenBoundingBoxRight;
                door2.OpenBoundingBoxTop = door.OpenBoundingBoxTop;
                door2.OpenVertexBlockCount = door.OpenVertexBlockCount;//xx
                door2.OpenVertexBlockIndex = door.OpenVertexBlockIndex;//xx
                door2.OpenVertexCount = door.OpenVertexCount;//xx
                door2.OpenVertexIndex = door.OpenVertexIndex;//xx
                door2.SecretDoorDetectionDifficulty = door.SecretDoorDetectionDifficulty;
                door2.TrapDetected = door.TrapDetected;
                door2.TrapDetectionDifficulty = door.TrapDetectionDifficulty;
                door2.TrapDetected = door.TrapLaunchXCoordinate;
                door2.TrapLaunchXCoordinate = door.TrapLaunchYCoordinate;
                door2.TrapLaunchYCoordinate = door.TrapRemovalDifficulty;
                door2.TravelTriggerName = door.TravelTriggerName.ToString();
                door2.Unknown = door.Unknown;
                door2.Unused = door.Unused;
                areFile.doors.Add(door2);
            }

            foreach (var animation in animations)
            {
                var animation2 = new AreAnimation2();
                animation2.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule; //xx
                animation2.BamAnimation = animation.BamAnimation.ToString();
                animation2.BamFrame = animation.BamFrame;
                animation2.BamSequence = animation.BamSequence;
                animation2.Flags = animation.Flags; //xx
                animation2.Height = animation.Height;
                animation2.LoopChance = animation.LoopChance;
                animation2.Name = animation.Name.ToString();
                animation2.Palette = animation.Palette.ToString();
                animation2.SkipCycles = animation.SkipCycles;
                animation2.StartFrame = animation.StartFrame;
                animation2.Transparency = animation.Transparency;
                animation2.Unknown = animation.Unknown;
                animation2.XCoordinate = animation.XCoordinate;
                animation2.YCoordinate = animation.YCoordinate;
                areFile.animations.Add(animation2);
            }

            foreach (var note in notes)
            {
                var note2 = new AreNote2();
                note2.Colour = note.Colour;
                note2.Location = note.Location;
                note2.Text = Common.ReadString(note.Text, TlkFile);
                note2.Unknown = note.Unknown;
                note2.XCoordinate = note.XCoordinate;
                note2.YCoordinate = note.YCoordinate;
                areFile.notes.Add(note2);
            }

            foreach (var tiledObject in tiledObjects)
            {
                var tiledObject2 = new AreTiledObject2();
                tiledObject2.ClosedSearchCount = tiledObject.ClosedSearchCount;
                tiledObject2.ClosedSearchOffset = tiledObject.ClosedSearchOffset;
                tiledObject2.Name = tiledObject.Name.ToString();
                tiledObject2.OpenSearchCount = tiledObject.OpenSearchCount;
                tiledObject2.OpenSearchOffset = tiledObject.OpenSearchOffset;
                tiledObject2.Unknown1 = tiledObject.Unknown1;
                tiledObject2.Unknown2 = tiledObject.Unknown2;
                tiledObject2.Unknown3 = tiledObject.Unknown3;
                areFile.tiledObjects.Add(tiledObject2);
            }

            foreach (var projectile in projectiles)
            {
                var projectile2 = new AreProjectile2();
                projectile2.EATarget = projectile.EATarget;
                projectile2.EffectOffset = projectile.EffectOffset;
                projectile2.EffectSize = projectile.EffectSize;
                projectile2.MissileId = projectile.MissileId;
                projectile2.PartyOwnerIndex = projectile.PartyOwnerIndex;
                projectile2.Resref = projectile.Resref.ToString();
                projectile2.TickUntilTriggerCheck = projectile.TickUntilTriggerCheck;
                projectile2.TriggersRemaining = projectile.TriggersRemaining;
                projectile2.XCoordinate = projectile.XCoordinate;
                projectile2.YCoordinate = projectile.YCoordinate;
                projectile2.ZCoordinate = projectile.ZCoordinate;
                areFile.projectiles.Add(projectile2);
            }

            foreach (var song in songs)
            {
                var song2 = new AreSong2();
                song2.BattleSong = song.BattleSong;
                song2.DayAmbient1Wav = song.DayAmbient1Wav.ToString();
                song2.DayAmbient2Wav = song.DayAmbient2Wav.ToString();
                song2.DayAmbientVolume = song.DayAmbientVolume;
                song2.DaySong = song.DaySong;
                song2.LoseSong = song.LoseSong;
                song2.NightAmbient1Wav = song.NightAmbient1Wav.ToString();
                song2.NightAmbient2Wav = song.NightAmbient2Wav.ToString();
                song2.NightAmbientVolume = song.NightAmbientVolume;
                song2.NightSong = song.NightSong;
                song2.NightSong = song.Reverb;
                song2.Unknown1 = song.Unknown1;
                song2.Unknown2 = song.Unknown2;
                song2.Unknown3 = song.Unknown3;
                song2.Unknown4 = song.Unknown4;
                song2.Unknown5 = song.Unknown5;
                song2.Unknown6 = song.Unknown6;
                song2.WinSong = song.WinSong;
                areFile.songs.Add(song2);
            }

            foreach (var interruption in interruptions)
            {
                var interruption2 = new AreInterruption2();
                interruption2.CreatureCount = interruption.CreatureCount;
                interruption2.DayProbability = interruption.DayProbability;
                interruption2.Difficulty = interruption.Difficulty;
                interruption2.Enabled = interruption.Enabled;
                interruption2.MaximumCreaturesToSpawn = interruption.MaximumCreaturesToSpawn;
                interruption2.Name = interruption.Name.ToString();
                interruption2.NightProbability = interruption.NightProbability;
                interruption2.RemovalTime = interruption.RemovalTime;
                interruption2.ResRef1 = interruption.ResRef1.ToString();
                interruption2.ResRef2 = interruption.ResRef2.ToString();
                interruption2.ResRef3 = interruption.ResRef3.ToString();
                interruption2.ResRef4 = interruption.ResRef4.ToString();
                interruption2.ResRef5 = interruption.ResRef5.ToString();
                interruption2.ResRef6 = interruption.ResRef6.ToString();
                interruption2.ResRef7 = interruption.ResRef7.ToString();
                interruption2.ResRef8 = interruption.ResRef8.ToString();
                interruption2.ResRef9 = interruption.ResRef9.ToString();
                interruption2.ResRef10 = interruption.ResRef10.ToString();
                interruption2.RestrictionDistance = interruption.RestrictionDistance;
                interruption2.RestrictionDistanceToObject = interruption.RestrictionDistanceToObject;
                interruption2.Text1 = interruption.Text1;
                interruption2.Text2 = interruption.Text2;
                interruption2.Text3 = interruption.Text3;
                interruption2.Text4 = interruption.Text4;
                interruption2.Text5 = interruption.Text5;
                interruption2.Text6 = interruption.Text6;
                interruption2.Text7 = interruption.Text7;
                interruption2.Text8 = interruption.Text8;
                interruption2.Text9 = interruption.Text9;
                interruption2.Text10 = interruption.Text10;
                interruption2.Unknown = interruption.Unknown;
                areFile.interruptions.Add(interruption2);
            }

            foreach (var exploration in exploredArea)
            {
                areFile.exploration.Add(exploration);
            }

            foreach (var vertex in vertices)
            {
                areFile.vertices.Add(vertex);
            }

            areFile.Checksum = MD5HashGenerator.GenerateKey(areFile);
            return areFile;
        }
    }
}