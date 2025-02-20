using System;
using System.Collections.Generic;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using System.IO;
using iiInfinityEngine.Core.Writers.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace iiInfinityEngine.Core.Writers
{
    public class AreFileBinaryWriter : IAreFileWriter
    {
        const int HeaderSize = 284;
        const int ActorSize = 272;
        const int RegionSize = 196;
        const int Spawnsize = 200;
        const int EntranceSize = 104;
        const int ContainerSize = 192;
        const int ItemSize = 20;
        const int AmbientSize = 212;
        const int VariableSize = 84;
        const int DoorSize = 200;
        const int AnimationSize = 76;
        const int NoteSize = 52;
        const int TiledObjectSize = 108;
        const int ProjectileSize = 28;
        const int SongSize = 144;
        const int InterruptionSize = 228;

        public TlkFile TlkFile { get; set; }
        public BackupManager BackupManger { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (!(file is AreFile))
                throw new ArgumentException("File is not a valid are file");

            var areFile = file as AreFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(areFile) == areFile.Checksum))
                return false;

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
            List<byte> areaExplorations = new List<byte>();
            List<Int32> vertices = new List<Int32>();

            foreach (var interruption in areFile.interruptions)
            {
                AreInterruptionBinary interruptionBinary = new AreInterruptionBinary();
                interruptionBinary.CreatureCount = interruption.CreatureCount;
                interruptionBinary.DayProbability = interruption.DayProbability;
                interruptionBinary.Difficulty = interruption.Difficulty;
                interruptionBinary.Enabled = interruption.Enabled;
                interruptionBinary.MaximumCreaturesToSpawn = interruption.MaximumCreaturesToSpawn;
                interruptionBinary.Name = new array32(interruption.Name);
                interruptionBinary.NightProbability = interruption.NightProbability;
                interruptionBinary.RemovalTime = interruption.RemovalTime;
                interruptionBinary.ResRef1 = new array8(interruption.ResRef1);
                interruptionBinary.ResRef2 = new array8(interruption.ResRef2);
                interruptionBinary.ResRef3 = new array8(interruption.ResRef3);
                interruptionBinary.ResRef4 = new array8(interruption.ResRef4);
                interruptionBinary.ResRef5 = new array8(interruption.ResRef5);
                interruptionBinary.ResRef6 = new array8(interruption.ResRef6);
                interruptionBinary.ResRef7 = new array8(interruption.ResRef7);
                interruptionBinary.ResRef8 = new array8(interruption.ResRef8);
                interruptionBinary.ResRef9 = new array8(interruption.ResRef9);
                interruptionBinary.ResRef10 = new array8(interruption.ResRef10);
                interruptionBinary.RestrictionDistance = interruption.RestrictionDistance;
                interruptionBinary.Text1 = interruption.Text1;
                interruptionBinary.Text2 = interruption.Text2;
                interruptionBinary.Text3 = interruption.Text3;
                interruptionBinary.Text4 = interruption.Text4;
                interruptionBinary.Text5 = interruption.Text5;
                interruptionBinary.Text6 = interruption.Text6;
                interruptionBinary.Text7 = interruption.Text7;
                interruptionBinary.Text8 = interruption.Text8;
                interruptionBinary.Text9 = interruption.Text9;
                interruptionBinary.Text10 = interruption.Text10;
                interruptionBinary.Unknown = interruption.Unknown;
                interruptions.Add(interruptionBinary);
            }

            foreach (var song in areFile.songs)
            {
                AreSongBinary songBinary = new AreSongBinary();
                songBinary.BattleSong = song.BattleSong;
                songBinary.DayAmbient1Wav = new array8(song.DayAmbient1Wav);
                songBinary.DayAmbient2Wav = new array8(song.DayAmbient2Wav);
                songBinary.DayAmbientVolume = song.DayAmbientVolume;
                songBinary.DaySong = song.DaySong;
                songBinary.LoseSong = song.LoseSong;
                songBinary.NightAmbient1Wav = new array8(song.NightAmbient1Wav);
                songBinary.NightAmbient2Wav = new array8(song.NightAmbient2Wav);
                songBinary.NightAmbientVolume = song.NightAmbientVolume;
                songBinary.NightSong = song.NightSong;
                songBinary.Reverb = song.Reverb;
                songBinary.AltMusic1 = song.AltMusic1;
                songBinary.AltMusic2 = song.AltMusic2;
                songBinary.AltMusic3 = song.AltMusic3;
                songBinary.AltMusic4 = song.AltMusic4;
                songBinary.AltMusic5 = song.AltMusic5;
                songBinary.Unknown = song.Unknown;
                songBinary.WinSong = song.WinSong;
                songs.Add(songBinary);
            }

            foreach (var projectile in areFile.projectiles)
            {
                AreProjectileBinary projectileBinary = new AreProjectileBinary();
                projectileBinary.EATarget = projectile.EATarget;
                projectileBinary.EffectOffset = projectile.EffectOffset;
                projectileBinary.EffectSize = projectile.EffectSize;
                projectileBinary.MissileId = projectile.MissileId;
                projectileBinary.PartyOwnerIndex = projectile.PartyOwnerIndex;
                projectileBinary.Resref = new array8(projectile.Resref);
                projectileBinary.TickUntilTriggerCheck = projectile.TickUntilTriggerCheck;
                projectileBinary.TriggersRemaining = projectile.TriggersRemaining;
                projectileBinary.XCoordinate = projectile.XCoordinate;
                projectileBinary.YCoordinate = projectile.YCoordinate;
                projectileBinary.ZCoordinate = projectile.ZCoordinate;
                projectiles.Add(projectileBinary);
            }

            foreach (var tiledObject in areFile.tiledObjects)
            {
                AreTiledObjectBinary tiledObjectBinary = new AreTiledObjectBinary();
                tiledObjectBinary.ClosedSearchCount = tiledObject.ClosedSearchCount;
                tiledObjectBinary.ClosedSearchOffset = tiledObject.ClosedSearchOffset;
                tiledObjectBinary.Name = new array32(tiledObject.Name);
                tiledObjectBinary.OpenSearchCount = tiledObject.OpenSearchCount;
                tiledObjectBinary.OpenSearchOffset = tiledObject.OpenSearchOffset;
                tiledObjectBinary.Unknown = tiledObject.Unknown;
                tiledObjectBinary.Flags = tiledObject.Flags;
                tiledObjectBinary.TileId = tiledObject.TileId;
                tiledObjects.Add(tiledObjectBinary);
            }

            foreach (var note in areFile.notes)
            {
                AreNoteBinary noteBinary = new AreNoteBinary();
                noteBinary.Colour = note.Colour;
                noteBinary.Location = note.Location;
                //noteBinary.NoteCountPlus = note.NoteCountPlus;
                noteBinary.Text = Common.WriteString(note.Text, TlkFile);
                noteBinary.XCoordinate = note.XCoordinate;
                noteBinary.YCoordinate = note.YCoordinate;
                notes.Add(noteBinary);
            }

            foreach (var animation in areFile.animations)
            {
                AreAnimationBinary animationBinary = new AreAnimationBinary();
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule;
                animationBinary.BamAnimation = new array8(animation.BamAnimation);
                animationBinary.BamFrame = animation.BamFrame;
                animationBinary.BamSequence = animation.BamSequence;
                animationBinary.Flags = animation.Flags;
                animationBinary.Height = animation.Height;
                animationBinary.LoopChance = animation.LoopChance;
                animationBinary.Name = new array32(animation.Name);
                animationBinary.Palette = new array8(animation.Palette);
                animationBinary.SkipCycles = animation.SkipCycles;
                animationBinary.StartFrame = animation.StartFrame;
                animationBinary.Transparency = animation.Transparency;
                animationBinary.XCoordinate = animation.XCoordinate;
                animationBinary.YCoordinate = animation.YCoordinate;
                animationBinary.WidthPVRZ = animation.WidthPVRZ;
                animationBinary.HeightPVRZ = animation.HeightPVRZ;
                animations.Add(animationBinary);
            }

            foreach (var door in areFile.doors)
            {
                AreDoorBinary doorBinary = new AreDoorBinary();
                doorBinary.ClosedBoundingBoxBottom = door.ClosedBoundingBoxBottom;
                doorBinary.ClosedBoundingBoxLeft = door.ClosedBoundingBoxLeft;
                doorBinary.ClosedBoundingBoxRight = door.ClosedBoundingBoxRight;
                doorBinary.ClosedBoundingBoxTop = door.ClosedBoundingBoxTop;
                doorBinary.ClosedVertexBlockCount = door.ClosedVertexBlockCount;
                doorBinary.ClosedVertexBlockIndex = door.ClosedVertexBlockIndex;
                doorBinary.ClosedVertexCount = door.ClosedVertexCount;
                doorBinary.ClosedVertexIndex = door.ClosedVertexIndex;
                doorBinary.Cursor = door.Cursor;
                doorBinary.DialogName = Common.WriteString(door.DialogName, TlkFile);
                doorBinary.DialogResref = new array8(door.DialogResref);
                doorBinary.DoorCloseSound = new array8(door.DoorCloseSound);
                doorBinary.DoorId = new array8(door.DoorId);
                doorBinary.DoorOpenSound = new array8(door.DoorOpenSound);
                doorBinary.DoorScript = new array8(door.DoorScript);
                doorBinary.DoorState1X = door.DoorState1X;
                doorBinary.DoorState1Y = door.DoorState1Y;
                doorBinary.DoorState2X = door.DoorState2X;
                doorBinary.DoorState2Y = door.DoorState2Y;
                doorBinary.Flags = door.Flags;
                doorBinary.IsTrap = door.IsTrap;
                doorBinary.KeyItem = new array8(door.KeyItem);
                doorBinary.LockDifficulty = door.LockDifficulty;
                doorBinary.LockpickString = Common.WriteString(door.LockpickString, TlkFile);
                doorBinary.Name = new array32(door.Name);
                doorBinary.OpenBoundingBoxBottom = door.OpenBoundingBoxBottom;
                doorBinary.OpenBoundingBoxLeft = door.OpenBoundingBoxLeft;
                doorBinary.OpenBoundingBoxRight = door.OpenBoundingBoxRight;
                doorBinary.OpenBoundingBoxTop = door.OpenBoundingBoxTop;
                doorBinary.OpenVertexBlockCount = door.OpenVertexBlockCount;
                doorBinary.OpenVertexBlockIndex = door.OpenVertexBlockIndex;
                doorBinary.OpenVertexCount = door.OpenVertexCount;
                doorBinary.OpenVertexIndex = door.OpenVertexIndex;
                doorBinary.SecretDoorDetectionDifficulty = door.SecretDoorDetectionDifficulty;
                doorBinary.TrapDetected = door.TrapDetected;
                doorBinary.TrapDetectionDifficulty = door.TrapDetectionDifficulty;
                doorBinary.TrapLaunchXCoordinate = door.TrapLaunchXCoordinate;
                doorBinary.TrapLaunchYCoordinate = door.TrapLaunchYCoordinate;
                doorBinary.TrapRemovalDifficulty = door.TrapRemovalDifficulty;
                //doorBinary.TravelTriggerName = new array24(door.TravelTriggerName);
                doorBinary.Unknown = door.Unknown;
                doorBinary.Hitpoints = door.Hitpoints;
                doorBinary.ArmourClass = door.ArmourClass;
                doors.Add(doorBinary);
            }

            foreach (var variable in areFile.variables)
            {
                AreVariableBinary variableBinary = new AreVariableBinary();

                variableBinary.Name = new array32(variable.Name);
                variableBinary.Type = variable.Type;
                variableBinary.ResourceType = variable.ResourceType;
                variableBinary.ValueDword = variable.ValueDword;
                variableBinary.ValueInt = variable.ValueInt;
                variableBinary.ValueDouble = variable.ValueDouble;
                variableBinary.ScriptName = new array32(variable.ScriptName);
                variables.Add(variableBinary);
            }

            foreach (var ambient in areFile.ambients)
            {
                AreAmbientBinary ambientBinary = new AreAmbientBinary();
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule;
                ambientBinary.Flags = ambient.Flags;
                ambientBinary.FrequencyBase = ambient.FrequencyBase;
                ambientBinary.FrequencyVariation = ambient.FrequencyVariation;
                ambientBinary.Height = ambient.Height;
                ambientBinary.Name = new array32(ambient.Name);
                ambientBinary.Radius = ambient.Radius;
                ambientBinary.Resref1 = new array8(ambient.Resref1);
                ambientBinary.Resref2 = new array8(ambient.Resref2);
                ambientBinary.Resref3 = new array8(ambient.Resref3);
                ambientBinary.Resref4 = new array8(ambient.Resref4);
                ambientBinary.Resref5 = new array8(ambient.Resref5);
                ambientBinary.Resref6 = new array8(ambient.Resref6);
                ambientBinary.Resref7 = new array8(ambient.Resref7);
                ambientBinary.Resref8 = new array8(ambient.Resref8);
                ambientBinary.Resref9 = new array8(ambient.Resref9);
                ambientBinary.Resref10 = new array8(ambient.Resref10);
                ambientBinary.ResRefCount = ambient.ResRefCount; //xx
                ambientBinary.XCoordinate = ambient.XCoordinate;
                ambientBinary.YCoordinate = ambient.YCoordinate;
                ambientBinary.PitchVariance = ambient.PitchVariance;
                ambientBinary.VolumeVariance = ambient.VolumeVariance;
                ambientBinary.Unknown2 = ambient.Unknown2;
                ambientBinary.Unknown3 = ambient.Unknown3;
                ambientBinary.Volume = ambient.Volume;
                ambients.Add(ambientBinary);
            }

            foreach (var container in areFile.containers)
            {
                AreContainerBinary containerBinary = new AreContainerBinary();
                containerBinary.BoundingBoxBottom = container.BoundingBoxBottom;
                containerBinary.BoundingBoxLeft = container.BoundingBoxLeft;
                containerBinary.BoundingBoxRight = container.BoundingBoxRight;
                containerBinary.BoundingBoxTop = container.BoundingBoxTop;
                containerBinary.ContainerType = (Int16)container.ContainerType;
                containerBinary.Flags = container.Flags.Locked ? containerBinary.Flags | Common.Bit0 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit01 ? containerBinary.Flags | Common.Bit1 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit02 ? containerBinary.Flags | Common.Bit2 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.TrapResets ? containerBinary.Flags | Common.Bit3 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit04 ? containerBinary.Flags | Common.Bit4 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Disabled ? containerBinary.Flags | Common.Bit5 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit06 ? containerBinary.Flags | Common.Bit6 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit07 ? containerBinary.Flags | Common.Bit7 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit08 ? containerBinary.Flags | Common.Bit8 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit09 ? containerBinary.Flags | Common.Bit9 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit10 ? containerBinary.Flags | Common.Bit10 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit11 ? containerBinary.Flags | Common.Bit11 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit12 ? containerBinary.Flags | Common.Bit12 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit13 ? containerBinary.Flags | Common.Bit13 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit14 ? containerBinary.Flags | Common.Bit14 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit15 ? containerBinary.Flags | Common.Bit15 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit16 ? containerBinary.Flags | Common.Bit16 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit17 ? containerBinary.Flags | Common.Bit17 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit18 ? containerBinary.Flags | Common.Bit18 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit19 ? containerBinary.Flags | Common.Bit19 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit20 ? containerBinary.Flags | Common.Bit20 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit21 ? containerBinary.Flags | Common.Bit21 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit22 ? containerBinary.Flags | Common.Bit22 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit23 ? containerBinary.Flags | Common.Bit23 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit24 ? containerBinary.Flags | Common.Bit24 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit25 ? containerBinary.Flags | Common.Bit25 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit26 ? containerBinary.Flags | Common.Bit26 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit27 ? containerBinary.Flags | Common.Bit27 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit28 ? containerBinary.Flags | Common.Bit28 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit29 ? containerBinary.Flags | Common.Bit29 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit30 ? containerBinary.Flags | Common.Bit30 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Bit31 ? containerBinary.Flags | Common.Bit31 : containerBinary.Flags;
                containerBinary.IsTrap = container.IsTrap;
                containerBinary.ItemCount = container.items.Count;
                containerBinary.ItemIndex = items.Count;

                for (int j = 0; j < container.items.Count; j++)
                {
                    AreItemBinary itemBinary = new AreItemBinary();
                    itemBinary.Charges1 = container.items[j].Charges1;
                    itemBinary.Charges2 = container.items[j].Charges2;
                    itemBinary.Charges3 = container.items[j].Charges3;
                    itemBinary.ExpirationTime = container.items[j].ExpirationTime;
                    itemBinary.Flags = container.items[j].Flags.Identified ? itemBinary.Flags | Common.Bit0 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Unstealable ? itemBinary.Flags | Common.Bit1 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Stolen ? itemBinary.Flags | Common.Bit2 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Undroppable ? itemBinary.Flags | Common.Bit3 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit04 ? itemBinary.Flags | Common.Bit4 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit05 ? itemBinary.Flags | Common.Bit5 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit06 ? itemBinary.Flags | Common.Bit6 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit07 ? itemBinary.Flags | Common.Bit7 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit08 ? itemBinary.Flags | Common.Bit8 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit09 ? itemBinary.Flags | Common.Bit9 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit10 ? itemBinary.Flags | Common.Bit10 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit11 ? itemBinary.Flags | Common.Bit11 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit12 ? itemBinary.Flags | Common.Bit12 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit13 ? itemBinary.Flags | Common.Bit13 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit14 ? itemBinary.Flags | Common.Bit14 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit15 ? itemBinary.Flags | Common.Bit15 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit16 ? itemBinary.Flags | Common.Bit16 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit17 ? itemBinary.Flags | Common.Bit17 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit18 ? itemBinary.Flags | Common.Bit18 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit19 ? itemBinary.Flags | Common.Bit19 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit20 ? itemBinary.Flags | Common.Bit20 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit21 ? itemBinary.Flags | Common.Bit21 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit22 ? itemBinary.Flags | Common.Bit22 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit23 ? itemBinary.Flags | Common.Bit23 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit24 ? itemBinary.Flags | Common.Bit24 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit25 ? itemBinary.Flags | Common.Bit25 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit26 ? itemBinary.Flags | Common.Bit26 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit27 ? itemBinary.Flags | Common.Bit27 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit28 ? itemBinary.Flags | Common.Bit28 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit29 ? itemBinary.Flags | Common.Bit29 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit30 ? itemBinary.Flags | Common.Bit30 : itemBinary.Flags;
                    itemBinary.Flags = container.items[j].Flags.Bit31 ? itemBinary.Flags | Common.Bit31 : itemBinary.Flags;
                    itemBinary.ItemResref = new array8(container.items[j].ItemResref);
                    items.Add(itemBinary);
                }

                containerBinary.KeyItem = new array8(container.KeyItem);
                containerBinary.LockDifficulty = container.LockDifficulty;
                containerBinary.LockpickString = Common.WriteString(container.LockpickString, TlkFile);
                containerBinary.Name = new array32(container.Name);
                containerBinary.TrapDetected = container.TrapDetected;
                containerBinary.TrapDetectionDifficulty = container.TrapDetectionDifficulty;
                containerBinary.TrapLaunchXCoordinate = container.TrapLaunchXCoordinate;
                containerBinary.TrapLaunchYCoordinate = container.TrapLaunchYCoordinate;
                containerBinary.TrapRemovalDifficulty = container.TrapRemovalDifficulty;
                containerBinary.TrapScript = new array8(container.TrapScript);
                containerBinary.TriggerRange = container.TriggerRange;
                containerBinary.Owner = container.Owner;
                containerBinary.BreakDifficulty = container.BreakDifficulty;
                containerBinary.VertexCount = container.VertexCount;
                containerBinary.VertexIndex = container.VertexIndex;
                containerBinary.XCoordinate = container.XCoordinate;
                containerBinary.YCoordinate = container.YCoordinate;
                containers.Add(containerBinary);
            }

            foreach (var entrance in areFile.entrances)
            {
                AreEntranceBinary entranceBinary = new AreEntranceBinary();
                entranceBinary.Name = new array32(entrance.Name);
                entranceBinary.Orientation = entrance.Orientation;
                entranceBinary.Unknown = entrance.Unknown;
                entranceBinary.XCoordinate = entrance.XCoordinate;
                entranceBinary.YCoordinate = entrance.YCoordinate;
                entrances.Add(entranceBinary);
            }

            foreach (var spawn in areFile.spawns)
            {
                AreSpawnPointBinary spawnBinary = new AreSpawnPointBinary();
                spawnBinary.ActorRemovalTime = spawn.ActorRemovalTime;
                spawnBinary.BaseCreatureNumberToSpawn = spawn.BaseCreatureNumberToSpawn;
                spawnBinary.CreatureSpawnCount = spawn.CreatureSpawnCount;
                spawnBinary.Enabled = spawn.Enabled;
                spawnBinary.Frequency = spawn.Frequency;
                spawnBinary.MaximumCreaturesToSpawn = spawn.MaximumCreaturesToSpawn;
                spawnBinary.MovementRestrictionDistance = spawn.MovementRestrictionDistance;
                spawnBinary.MovementRestrictionDistanceToObject = spawn.MovementRestrictionDistanceToObject;
                spawnBinary.Name = new array32(spawn.Name);
                spawnBinary.ProbabilityDay = spawn.ProbabilityDay;
                spawnBinary.ProbabilityNight = spawn.ProbabilityNight;
                spawnBinary.Resref1 = new array8(spawn.Resref1);
                spawnBinary.Resref2 = new array8(spawn.Resref2);
                spawnBinary.Resref3 = new array8(spawn.Resref3);
                spawnBinary.Resref4 = new array8(spawn.Resref4);
                spawnBinary.Resref5 = new array8(spawn.Resref5);
                spawnBinary.Resref6 = new array8(spawn.Resref6);
                spawnBinary.Resref7 = new array8(spawn.Resref7);
                spawnBinary.Resref8 = new array8(spawn.Resref8);
                spawnBinary.Resref9 = new array8(spawn.Resref9);
                spawnBinary.Resref10 = new array8(spawn.Resref10);
                spawnBinary.SpawnMethod = spawn.SpawnMethod;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule;
                spawnBinary.Unknown = spawn.Unknown;
                spawnBinary.XCoordinate = spawn.XCoordinate;
                spawnBinary.YCoordinate = spawn.YCoordinate;
                spawnBinary.SpawnFrequency = spawn.SpawnFrequency;
                spawnBinary.Countdown = spawn.Countdown;
                spawnBinary.SpawnWeight1 = spawn.SpawnWeight1;
                spawnBinary.SpawnWeight2 = spawn.SpawnWeight2;
                spawnBinary.SpawnWeight3 = spawn.SpawnWeight3;
                spawnBinary.SpawnWeight4 = spawn.SpawnWeight4;
                spawnBinary.SpawnWeight5 = spawn.SpawnWeight5;
                spawnBinary.SpawnWeight6 = spawn.SpawnWeight6;
                spawnBinary.SpawnWeight7 = spawn.SpawnWeight7;
                spawnBinary.SpawnWeight8 = spawn.SpawnWeight8;
                spawnBinary.SpawnWeight9 = spawn.SpawnWeight9;
                spawnBinary.SpawnWeight10 = spawn.SpawnWeight10;
                spawnBinary.Unknown = spawn.Unknown;
                spawns.Add(spawnBinary);
            }

            foreach (var region in areFile.regions)
            {
                AreRegionBinary regionBinary = new AreRegionBinary();
                regionBinary.AlternativeUsePointXCoordinate = region.AlternativeUsePointXCoordinate;
                regionBinary.AlternativeUsePointYCoordinate = region.AlternativeUsePointYCoordinate;
                regionBinary.BoundingBoxBottom = region.BoundingBoxBottom;
                regionBinary.BoundingBoxLeft = region.BoundingBoxLeft;
                regionBinary.BoundingBoxRight = region.BoundingBoxRight;
                regionBinary.BoundingBoxTop = region.BoundingBoxTop;
                regionBinary.Cursor = region.Cursor;
                regionBinary.DestinationArea = new array8(region.DestinationArea);
                regionBinary.DestinationEntrance = new array32(region.DestinationEntrance);
                regionBinary.DialogFile = new array8(region.DialogFile);
                regionBinary.DialogName = Common.WriteString(region.DialogName, TlkFile);
                regionBinary.Flags = region.Flags.InvisibleTrap ? regionBinary.Flags | Common.Bit0 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.ResetTrap ? regionBinary.Flags | Common.Bit1 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.PartyRequired ? regionBinary.Flags | Common.Bit2 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Detectable ? regionBinary.Flags | Common.Bit3 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit04 ? regionBinary.Flags | Common.Bit4 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit05 ? regionBinary.Flags | Common.Bit5 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.NPCCanTrigger ? regionBinary.Flags | Common.Bit6 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit07 ? regionBinary.Flags | Common.Bit7 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Deactivated ? regionBinary.Flags | Common.Bit8 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.NPCCannotPass ? regionBinary.Flags | Common.Bit9 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.AlternativePoint ? regionBinary.Flags | Common.Bit10 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.UsedByDoor ? regionBinary.Flags | Common.Bit11 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit12 ? regionBinary.Flags | Common.Bit12 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit13 ? regionBinary.Flags | Common.Bit13 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit14 ? regionBinary.Flags | Common.Bit14 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit15 ? regionBinary.Flags | Common.Bit15 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit16 ? regionBinary.Flags | Common.Bit16 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit17 ? regionBinary.Flags | Common.Bit17 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit18 ? regionBinary.Flags | Common.Bit18 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit19 ? regionBinary.Flags | Common.Bit19 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit20 ? regionBinary.Flags | Common.Bit20 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit21 ? regionBinary.Flags | Common.Bit21 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit22 ? regionBinary.Flags | Common.Bit22 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit23 ? regionBinary.Flags | Common.Bit23 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit24 ? regionBinary.Flags | Common.Bit24 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit25 ? regionBinary.Flags | Common.Bit25 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit26 ? regionBinary.Flags | Common.Bit26 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit27 ? regionBinary.Flags | Common.Bit27 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit28 ? regionBinary.Flags | Common.Bit28 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit29 ? regionBinary.Flags | Common.Bit29 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit30 ? regionBinary.Flags | Common.Bit30 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Bit31 ? regionBinary.Flags | Common.Bit31 : regionBinary.Flags;
                regionBinary.InformationText = Common.WriteString(region.InformationText, TlkFile);
                regionBinary.IsTrap = region.IsTrap;
                regionBinary.KeyItem = new array8(region.KeyItem);
                regionBinary.Name = new array32(region.Name);
                regionBinary.RegionScript = new array8(region.RegionScript);
                regionBinary.RegionType = (Int16)region.RegionType;
                regionBinary.Sound = new array8(region.Sound);
                regionBinary.TalkLocationXCoordinate = region.TalkLocationXCoordinate;
                regionBinary.TalkLocationYCoordinate = region.TalkLocationYCoordinate;
                regionBinary.TrapDetected = region.TrapDetected;
                regionBinary.TrapDetectionDifficulty = region.TrapDetectionDifficulty;
                regionBinary.TrapLaunchXCoordinate = region.TrapLaunchXCoordinate;
                regionBinary.TrapLaunchYCoordinate = region.TrapLaunchYCoordinate;
                regionBinary.TrapRemovalDifficulty = region.TrapRemovalDifficulty;
                regionBinary.TriggerValue = region.TriggerValue;
                regionBinary.Unknown2 = region.Unknown2;
                regionBinary.Unknown3 = region.Unknown3;
                regionBinary.VertexCount = region.VertexCount;
                regionBinary.VertexIndex = region.VertexIndex;
                regions.Add(regionBinary);
            }

            foreach (var actor in areFile.actors)
            {
                AreActorBinary actorBinary = new AreActorBinary();
                actorBinary.ActorAnimation = actor.ActorAnimation;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule;
                actorBinary.Flags = actor.ActorFlags.CreAttached ? actorBinary.Flags | Common.Bit0 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit01 ? actorBinary.Flags | Common.Bit1 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit02 ? actorBinary.Flags | Common.Bit2 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.OverrideScriptName ? actorBinary.Flags | Common.Bit3 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit04 ? actorBinary.Flags | Common.Bit4 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit05 ? actorBinary.Flags | Common.Bit5 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit06 ? actorBinary.Flags | Common.Bit6 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit07 ? actorBinary.Flags | Common.Bit7 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit08 ? actorBinary.Flags | Common.Bit8 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit09 ? actorBinary.Flags | Common.Bit9 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit10 ? actorBinary.Flags | Common.Bit10 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit11 ? actorBinary.Flags | Common.Bit11 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit12 ? actorBinary.Flags | Common.Bit12 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit13 ? actorBinary.Flags | Common.Bit13 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit14 ? actorBinary.Flags | Common.Bit14 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit15 ? actorBinary.Flags | Common.Bit15 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit16 ? actorBinary.Flags | Common.Bit16 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit17 ? actorBinary.Flags | Common.Bit17 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit18 ? actorBinary.Flags | Common.Bit18 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit19 ? actorBinary.Flags | Common.Bit19 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit20 ? actorBinary.Flags | Common.Bit20 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit21 ? actorBinary.Flags | Common.Bit21 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit22 ? actorBinary.Flags | Common.Bit22 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit23 ? actorBinary.Flags | Common.Bit23 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit24 ? actorBinary.Flags | Common.Bit24 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit25 ? actorBinary.Flags | Common.Bit25 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit26 ? actorBinary.Flags | Common.Bit26 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit27 ? actorBinary.Flags | Common.Bit27 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit28 ? actorBinary.Flags | Common.Bit28 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit29 ? actorBinary.Flags | Common.Bit29 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit30 ? actorBinary.Flags | Common.Bit30 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Bit31 ? actorBinary.Flags | Common.Bit31 : actorBinary.Flags;
                actorBinary.ActorOrientation = actor.ActorOrientation;
                actorBinary.ActorRemovalTimer = actor.ActorRemovalTimer;
                actorBinary.CREFile = new array8(actor.CREFile);
                actorBinary.CreOffset = actor.CreOffset;
                actorBinary.CreSize = actor.CreSize;
                actorBinary.CurrentXCoordinate = actor.CurrentXCoordinate;
                actorBinary.CurrentYCoordinate = actor.CurrentYCoordinate;
                actorBinary.DestinationXCoordinate = actor.DestinationXCoordinate;
                actorBinary.DestinationYCoordinate = actor.DestinationYCoordinate;
                actorBinary.Dialog = new array8(actor.Dialog);
                actorBinary.FilenameInitialCharacter = actor.FilenameInitialCharacter;
                actorBinary.HasBeenSpawned = actor.HasBeenSpawned;
                actorBinary.MovementRestrictionDistance = actor.MovementRestrictionDistance;
                actorBinary.MovementRestrictionDistanceMoveToObject = actor.MovementRestrictionDistanceMoveToObject;
                actorBinary.Name = new array32(actor.Name);
                actorBinary.NumTimesTalkedTo = actor.NumTimesTalkedTo;
                actorBinary.ScriptClass = new array8(actor.ScriptClass);
                actorBinary.ScriptDefault = new array8(actor.ScriptDefault);
                actorBinary.ScriptGeneral = new array8(actor.ScriptGeneral);
                actorBinary.ScriptOverride = new array8(actor.ScriptOverride);
                actorBinary.ScriptRace = new array8(actor.ScriptRace);
                actorBinary.ScriptSpecific = new array8(actor.ScriptSpecific);
                actorBinary.Unknown = actor.Unknown;
                actorBinary.Unknown2 = actor.Unknown2;
                actors.Add(actorBinary);
            }

            if (areFile.exploration.Count % 8 != 0)
            {
                for (int i = 0; i < areFile.exploration.Count + 1; i++)
                {
                    areaExplorations.Add(new byte());
                }
            }
            else
            {
                for (int i = 0; i < areFile.exploration.Count; i++)
                {
                    areaExplorations.Add(new byte());
                }
            }

            int bitCount = 0;
            byte currentByte = 0;
            foreach (var exploration in areFile.exploration)
            {
                switch (bitCount)
                {
                    case 0:
                        currentByte = exploration ? Convert.ToByte(currentByte | (byte)Common.Bit0) : currentByte;
                        break;
                    case 1:
                        currentByte = exploration ? Convert.ToByte(currentByte | (byte)Common.Bit1) : currentByte;
                        break;
                    case 2:
                        currentByte = exploration ? Convert.ToByte(currentByte | (byte)Common.Bit2) : currentByte;
                        break;
                    case 3:
                        currentByte = exploration ? Convert.ToByte(currentByte | (byte)Common.Bit3) : currentByte;
                        break;
                    case 4:
                        currentByte = exploration ? Convert.ToByte(currentByte | (byte)Common.Bit4) : currentByte;
                        break;
                    case 5:
                        currentByte = exploration ? Convert.ToByte(currentByte | (byte)Common.Bit5) : currentByte;
                        break;
                    case 6:
                        currentByte = exploration ? Convert.ToByte(currentByte | (byte)Common.Bit6) : currentByte;
                        break;
                    case 7:
                        currentByte = exploration ? Convert.ToByte(currentByte | (byte)Common.Bit7) : currentByte;
                        break;
                }

                if (bitCount == 7)
                {
                    areaExplorations.Add(currentByte);
                    currentByte = new byte();
                }
            }

            foreach (var vertex in areFile.vertices)
            {
                vertices.Add(vertex);
            }

            AreHeaderBinary header = new AreHeaderBinary();
            header.AreaFlags = areFile.AreaFlags.SaveAllowed ? header.AreaFlags | Common.Bit0 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.TutorialArea ? header.AreaFlags | Common.Bit1 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.DeadMagicZone ? header.AreaFlags | Common.Bit2 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Dream ? header.AreaFlags | Common.Bit3 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit04 ? header.AreaFlags | Common.Bit4 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit05 ? header.AreaFlags | Common.Bit5 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit06 ? header.AreaFlags | Common.Bit6 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit07 ? header.AreaFlags | Common.Bit7 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit08 ? header.AreaFlags | Common.Bit8 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit09 ? header.AreaFlags | Common.Bit9 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit10 ? header.AreaFlags | Common.Bit10 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit11 ? header.AreaFlags | Common.Bit11 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit12 ? header.AreaFlags | Common.Bit12 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit13 ? header.AreaFlags | Common.Bit13 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit14 ? header.AreaFlags | Common.Bit14 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit15 ? header.AreaFlags | Common.Bit15 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit16 ? header.AreaFlags | Common.Bit16 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit17 ? header.AreaFlags | Common.Bit17 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit18 ? header.AreaFlags | Common.Bit18 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit19 ? header.AreaFlags | Common.Bit19 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit20 ? header.AreaFlags | Common.Bit20 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit21 ? header.AreaFlags | Common.Bit21 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit22 ? header.AreaFlags | Common.Bit22 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit23 ? header.AreaFlags | Common.Bit23 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit24 ? header.AreaFlags | Common.Bit24 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit25 ? header.AreaFlags | Common.Bit25 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit26 ? header.AreaFlags | Common.Bit26 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit27 ? header.AreaFlags | Common.Bit27 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit28 ? header.AreaFlags | Common.Bit28 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit29 ? header.AreaFlags | Common.Bit29 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit30 ? header.AreaFlags | Common.Bit30 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Bit31 ? header.AreaFlags | Common.Bit31 : header.AreaFlags;

            header.AreaTypeFlags = areFile.AreaTypeFlags.Outdoor ? Convert.ToUInt16(header.AreaTypeFlags | Common.Bit0) : header.AreaTypeFlags;
            header.AreaTypeFlags = areFile.AreaTypeFlags.DayNight ? Convert.ToUInt16(header.AreaTypeFlags | Common.Bit1) : header.AreaTypeFlags;
            header.AreaTypeFlags = areFile.AreaTypeFlags.Weather ? Convert.ToUInt16(header.AreaTypeFlags | Common.Bit2) : header.AreaTypeFlags;
            header.AreaTypeFlags = areFile.AreaTypeFlags.City ? Convert.ToUInt16(header.AreaTypeFlags | Common.Bit3) : header.AreaTypeFlags;
            header.AreaTypeFlags = areFile.AreaTypeFlags.Forest ? Convert.ToUInt16(header.AreaTypeFlags | Common.Bit4) : header.AreaTypeFlags;
            header.AreaTypeFlags = areFile.AreaTypeFlags.Dungeon ? Convert.ToUInt16(header.AreaTypeFlags | Common.Bit5) : header.AreaTypeFlags;
            header.AreaTypeFlags = areFile.AreaTypeFlags.ExtendedNight ? Convert.ToUInt16(header.AreaTypeFlags | Common.Bit6) : header.AreaTypeFlags;
            header.AreaTypeFlags = areFile.AreaTypeFlags.CanRestIndoors ? Convert.ToUInt16(header.AreaTypeFlags | Common.Bit7) : header.AreaTypeFlags;
            header.AreaTypeFlags = areFile.AreaTypeFlags.Bit08 ? Convert.ToUInt16(header.AreaTypeFlags | Common.Bit8) : header.AreaTypeFlags;
            header.AreaTypeFlags = areFile.AreaTypeFlags.Bit09 ? Convert.ToUInt16(header.AreaTypeFlags | Common.Bit9) : header.AreaTypeFlags;
            header.AreaTypeFlags = areFile.AreaTypeFlags.Bit10 ? Convert.ToUInt16(header.AreaTypeFlags | Common.Bit10) : header.AreaTypeFlags;
            header.AreaTypeFlags = areFile.AreaTypeFlags.Bit11 ? Convert.ToUInt16(header.AreaTypeFlags | Common.Bit11) : header.AreaTypeFlags;
            header.AreaTypeFlags = areFile.AreaTypeFlags.Bit12 ? Convert.ToUInt16(header.AreaTypeFlags | Common.Bit12) : header.AreaTypeFlags;
            header.AreaTypeFlags = areFile.AreaTypeFlags.Bit13 ? Convert.ToUInt16(header.AreaTypeFlags | Common.Bit13) : header.AreaTypeFlags;
            header.AreaTypeFlags = areFile.AreaTypeFlags.Bit14 ? Convert.ToUInt16(header.AreaTypeFlags | Common.Bit14) : header.AreaTypeFlags;
            header.AreaTypeFlags = areFile.AreaTypeFlags.Bit15 ? Convert.ToUInt16(header.AreaTypeFlags | Common.Bit15) : header.AreaTypeFlags;

            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.PartyRequired ? header.AreaToTheNorthFlags | Common.Bit0 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.PartyEnabled ? header.AreaToTheNorthFlags | Common.Bit1 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit02 ? header.AreaToTheNorthFlags | Common.Bit2 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit03 ? header.AreaToTheNorthFlags | Common.Bit3 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit04 ? header.AreaToTheNorthFlags | Common.Bit4 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit05 ? header.AreaToTheNorthFlags | Common.Bit5 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit06 ? header.AreaToTheNorthFlags | Common.Bit6 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit07 ? header.AreaToTheNorthFlags | Common.Bit7 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit08 ? header.AreaToTheNorthFlags | Common.Bit8 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit09 ? header.AreaToTheNorthFlags | Common.Bit9 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit10 ? header.AreaToTheNorthFlags | Common.Bit10 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit11 ? header.AreaToTheNorthFlags | Common.Bit11 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit12 ? header.AreaToTheNorthFlags | Common.Bit12 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit13 ? header.AreaToTheNorthFlags | Common.Bit13 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit14 ? header.AreaToTheNorthFlags | Common.Bit14 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit15 ? header.AreaToTheNorthFlags | Common.Bit15 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit16 ? header.AreaToTheNorthFlags | Common.Bit16 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit17 ? header.AreaToTheNorthFlags | Common.Bit17 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit18 ? header.AreaToTheNorthFlags | Common.Bit18 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit19 ? header.AreaToTheNorthFlags | Common.Bit19 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit20 ? header.AreaToTheNorthFlags | Common.Bit20 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit21 ? header.AreaToTheNorthFlags | Common.Bit21 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit22 ? header.AreaToTheNorthFlags | Common.Bit22 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit23 ? header.AreaToTheNorthFlags | Common.Bit23 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit24 ? header.AreaToTheNorthFlags | Common.Bit24 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit25 ? header.AreaToTheNorthFlags | Common.Bit25 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit26 ? header.AreaToTheNorthFlags | Common.Bit26 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit27 ? header.AreaToTheNorthFlags | Common.Bit27 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit28 ? header.AreaToTheNorthFlags | Common.Bit28 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit29 ? header.AreaToTheNorthFlags | Common.Bit29 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit30 ? header.AreaToTheNorthFlags | Common.Bit30 : header.AreaToTheNorthFlags;
            header.AreaToTheNorthFlags = areFile.AreaToTheNorthFlags.Bit31 ? header.AreaToTheNorthFlags | Common.Bit31 : header.AreaToTheNorthFlags;

            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.PartyRequired ? header.AreaToTheEastFlags | Common.Bit0 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.PartyEnabled ? header.AreaToTheEastFlags | Common.Bit1 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit02 ? header.AreaToTheEastFlags | Common.Bit2 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit03 ? header.AreaToTheEastFlags | Common.Bit3 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit04 ? header.AreaToTheEastFlags | Common.Bit4 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit05 ? header.AreaToTheEastFlags | Common.Bit5 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit06 ? header.AreaToTheEastFlags | Common.Bit6 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit07 ? header.AreaToTheEastFlags | Common.Bit7 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit08 ? header.AreaToTheEastFlags | Common.Bit8 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit09 ? header.AreaToTheEastFlags | Common.Bit9 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit10 ? header.AreaToTheEastFlags | Common.Bit10 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit11 ? header.AreaToTheEastFlags | Common.Bit11 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit12 ? header.AreaToTheEastFlags | Common.Bit12 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit13 ? header.AreaToTheEastFlags | Common.Bit13 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit14 ? header.AreaToTheEastFlags | Common.Bit14 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit15 ? header.AreaToTheEastFlags | Common.Bit15 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit16 ? header.AreaToTheEastFlags | Common.Bit16 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit17 ? header.AreaToTheEastFlags | Common.Bit17 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit18 ? header.AreaToTheEastFlags | Common.Bit18 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit19 ? header.AreaToTheEastFlags | Common.Bit19 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit20 ? header.AreaToTheEastFlags | Common.Bit20 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit21 ? header.AreaToTheEastFlags | Common.Bit21 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit22 ? header.AreaToTheEastFlags | Common.Bit22 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit23 ? header.AreaToTheEastFlags | Common.Bit23 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit24 ? header.AreaToTheEastFlags | Common.Bit24 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit25 ? header.AreaToTheEastFlags | Common.Bit25 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit26 ? header.AreaToTheEastFlags | Common.Bit26 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit27 ? header.AreaToTheEastFlags | Common.Bit27 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit28 ? header.AreaToTheEastFlags | Common.Bit28 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit29 ? header.AreaToTheEastFlags | Common.Bit29 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit30 ? header.AreaToTheEastFlags | Common.Bit30 : header.AreaToTheEastFlags;
            header.AreaToTheEastFlags = areFile.AreaToTheEastFlags.Bit31 ? header.AreaToTheEastFlags | Common.Bit31 : header.AreaToTheEastFlags;

            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.PartyRequired ? header.AreaToTheSouthFlags | Common.Bit0 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.PartyEnabled ? header.AreaToTheSouthFlags | Common.Bit1 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit02 ? header.AreaToTheSouthFlags | Common.Bit2 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit03 ? header.AreaToTheSouthFlags | Common.Bit3 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit04 ? header.AreaToTheSouthFlags | Common.Bit4 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit05 ? header.AreaToTheSouthFlags | Common.Bit5 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit06 ? header.AreaToTheSouthFlags | Common.Bit6 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit07 ? header.AreaToTheSouthFlags | Common.Bit7 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit08 ? header.AreaToTheSouthFlags | Common.Bit8 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit09 ? header.AreaToTheSouthFlags | Common.Bit9 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit10 ? header.AreaToTheSouthFlags | Common.Bit10 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit11 ? header.AreaToTheSouthFlags | Common.Bit11 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit12 ? header.AreaToTheSouthFlags | Common.Bit12 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit13 ? header.AreaToTheSouthFlags | Common.Bit13 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit14 ? header.AreaToTheSouthFlags | Common.Bit14 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit15 ? header.AreaToTheSouthFlags | Common.Bit15 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit16 ? header.AreaToTheSouthFlags | Common.Bit16 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit17 ? header.AreaToTheSouthFlags | Common.Bit17 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit18 ? header.AreaToTheSouthFlags | Common.Bit18 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit19 ? header.AreaToTheSouthFlags | Common.Bit19 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit20 ? header.AreaToTheSouthFlags | Common.Bit20 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit21 ? header.AreaToTheSouthFlags | Common.Bit21 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit22 ? header.AreaToTheSouthFlags | Common.Bit22 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit23 ? header.AreaToTheSouthFlags | Common.Bit23 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit24 ? header.AreaToTheSouthFlags | Common.Bit24 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit25 ? header.AreaToTheSouthFlags | Common.Bit25 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit26 ? header.AreaToTheSouthFlags | Common.Bit26 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit27 ? header.AreaToTheSouthFlags | Common.Bit27 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit28 ? header.AreaToTheSouthFlags | Common.Bit28 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit29 ? header.AreaToTheSouthFlags | Common.Bit29 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit30 ? header.AreaToTheSouthFlags | Common.Bit30 : header.AreaToTheSouthFlags;
            header.AreaToTheSouthFlags = areFile.AreaToTheSouthFlags.Bit31 ? header.AreaToTheSouthFlags | Common.Bit31 : header.AreaToTheSouthFlags;

            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.PartyRequired ? header.AreaToTheWestFlags | Common.Bit0 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.PartyEnabled ? header.AreaToTheWestFlags | Common.Bit1 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit02 ? header.AreaToTheWestFlags | Common.Bit2 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit03 ? header.AreaToTheWestFlags | Common.Bit3 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit04 ? header.AreaToTheWestFlags | Common.Bit4 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit05 ? header.AreaToTheWestFlags | Common.Bit5 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit06 ? header.AreaToTheWestFlags | Common.Bit6 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit07 ? header.AreaToTheWestFlags | Common.Bit7 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit08 ? header.AreaToTheWestFlags | Common.Bit8 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit09 ? header.AreaToTheWestFlags | Common.Bit9 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit10 ? header.AreaToTheWestFlags | Common.Bit10 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit11 ? header.AreaToTheWestFlags | Common.Bit11 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit12 ? header.AreaToTheWestFlags | Common.Bit12 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit13 ? header.AreaToTheWestFlags | Common.Bit13 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit14 ? header.AreaToTheWestFlags | Common.Bit14 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit15 ? header.AreaToTheWestFlags | Common.Bit15 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit16 ? header.AreaToTheWestFlags | Common.Bit16 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit17 ? header.AreaToTheWestFlags | Common.Bit17 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit18 ? header.AreaToTheWestFlags | Common.Bit18 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit19 ? header.AreaToTheWestFlags | Common.Bit19 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit20 ? header.AreaToTheWestFlags | Common.Bit20 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit21 ? header.AreaToTheWestFlags | Common.Bit21 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit22 ? header.AreaToTheWestFlags | Common.Bit22 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit23 ? header.AreaToTheWestFlags | Common.Bit23 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit24 ? header.AreaToTheWestFlags | Common.Bit24 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit25 ? header.AreaToTheWestFlags | Common.Bit25 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit26 ? header.AreaToTheWestFlags | Common.Bit26 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit27 ? header.AreaToTheWestFlags | Common.Bit27 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit28 ? header.AreaToTheWestFlags | Common.Bit28 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit29 ? header.AreaToTheWestFlags | Common.Bit29 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit30 ? header.AreaToTheWestFlags | Common.Bit30 : header.AreaToTheWestFlags;
            header.AreaToTheWestFlags = areFile.AreaToTheWestFlags.Bit31 ? header.AreaToTheWestFlags | Common.Bit31 : header.AreaToTheWestFlags;

            header.ftype = new array4() { character1 = 'A', character2 = 'R', character3 = 'E', character4 = 'A' };
            header.fversion = new array4() { character1 = 'V', character2 = '1', character3 = '.', character4 = '0' };
            header.AreaScript = new array8(areFile.AreaScript);
            header.AreaToTheEast = new array8(areFile.AreaToTheEast);
            header.AreaToTheNorth = new array8(areFile.AreaToTheNorth);
            header.AreaToTheSouth = new array8(areFile.AreaToTheSouth);
            header.AreaToTheWest = new array8(areFile.AreaToTheWest);
            header.AreaWED = new array8(areFile.AreaWED);
            header.LastSaved = areFile.LastSaved;
            header.RestMovieDay = new array8(areFile.RestMovieDay);
            header.RestMovieNight = new array8(areFile.RestMovieNight);
            header.OverlayTransparency = areFile.OverlayTransparency;
            header.TiledObjectFlagOffset = areFile.TiledObjectFlagOffset;
            header.TiledObjectFlagCount= areFile.TiledObjectFlagCount;
            header.WeatherProbabilityFog = areFile.WeatherProbabilityFog;
            header.WeatherProbabilityLightning = areFile.WeatherProbabilityLightning;
            header.WeatherProbabilityRain = areFile.WeatherProbabilityRain;
            header.WeatherProbabilitySnow = areFile.WeatherProbabilitySnow;

            header.ActorCount = Convert.ToInt16(actors.Count);
            header.ActorOffset = HeaderSize;
            header.RegionCount = Convert.ToInt16(regions.Count);
            header.RegionOffset = HeaderSize + (ActorSize * actors.Count);
            header.SpawnPointCount = Convert.ToInt16(spawns.Count);
            header.SpawnPointOffset = HeaderSize + (ActorSize * actors.Count) + (RegionSize * regions.Count);
            header.EntrancesCount = Convert.ToInt16(entrances.Count);
            header.EntrancesOffset = HeaderSize + (ActorSize * actors.Count) + (RegionSize * regions.Count) + (Spawnsize * spawns.Count);
            header.ContainerCount = Convert.ToInt16(containers.Count);
            header.ContainerOffset = HeaderSize + (ActorSize * actors.Count) + (RegionSize * regions.Count) + (Spawnsize * spawns.Count) + (EntranceSize * entrances.Count);
            header.ItemCount = Convert.ToInt16(items.Count);
            header.ItemOffset = HeaderSize + (ActorSize * actors.Count) + (RegionSize * regions.Count) + (Spawnsize * spawns.Count) + (EntranceSize * entrances.Count) + (ContainerSize * containers.Count);
            header.AmbientCount = Convert.ToInt16(ambients.Count);
            header.AmbientOffset = HeaderSize + (ActorSize * actors.Count) + (RegionSize * regions.Count) + (Spawnsize * spawns.Count) + (EntranceSize * entrances.Count) + (ContainerSize * containers.Count) + (ItemSize * items.Count);
            header.VariableCount = Convert.ToInt16(variables.Count);
            header.VariableOffset = HeaderSize + (ActorSize * actors.Count) + (RegionSize * regions.Count) + (Spawnsize * spawns.Count) + (EntranceSize * entrances.Count) + (ContainerSize * containers.Count) + (ItemSize * items.Count) + (AmbientSize * ambients.Count);
            header.ExploredBitmaskOffset = HeaderSize + (ActorSize * actors.Count) + (RegionSize * regions.Count) + (Spawnsize * spawns.Count) + (EntranceSize * entrances.Count) + (ContainerSize * containers.Count) + (ItemSize * items.Count) + (AmbientSize * ambients.Count) + (VariableSize * variables.Count);
            header.ExploredBitmaskSize = areaExplorations.Count;
            header.DoorCount = doors.Count;
            header.DoorOffset = HeaderSize + (ActorSize * actors.Count) + (RegionSize * regions.Count) + (Spawnsize * spawns.Count) + (EntranceSize * entrances.Count) + (ContainerSize * containers.Count) + (ItemSize * items.Count) + (AmbientSize * ambients.Count) + (VariableSize * variables.Count) + (areaExplorations.Count);
            header.TiledObjectCount = Convert.ToInt16(tiledObjects.Count);
            header.TiledObjectOffset = HeaderSize + (ActorSize * actors.Count) + (RegionSize * regions.Count) + (Spawnsize * spawns.Count) + (EntranceSize * entrances.Count) + (ContainerSize * containers.Count) + (ItemSize * items.Count) + (AmbientSize * ambients.Count) + (VariableSize * variables.Count) + (DoorSize * doors.Count) + (areaExplorations.Count);
            header.VertexCount = Convert.ToInt16(vertices.Count);
            header.VertexOffset = HeaderSize + (ActorSize * actors.Count) + (RegionSize * regions.Count) + (Spawnsize * spawns.Count) + (EntranceSize * entrances.Count) + (ContainerSize * containers.Count) + (ItemSize * items.Count) + (AmbientSize * ambients.Count) + (VariableSize * variables.Count) + (DoorSize * doors.Count) + (areaExplorations.Count) + (TiledObjectSize * tiledObjects.Count);
            header.AnimationCount = Convert.ToInt16(animations.Count);
            header.AnimationOffset = HeaderSize + (ActorSize * actors.Count) + (RegionSize * regions.Count) + (Spawnsize * spawns.Count) + (EntranceSize * entrances.Count) + (ContainerSize * containers.Count) + (ItemSize * items.Count) + (AmbientSize * ambients.Count) + (VariableSize * variables.Count) + (DoorSize * doors.Count) + (4 * vertices.Count) + (areaExplorations.Count) + (TiledObjectSize * tiledObjects.Count);
            header.SongEntryOffset = HeaderSize + (ActorSize * actors.Count) + (RegionSize * regions.Count) + (Spawnsize * spawns.Count) + (EntranceSize * entrances.Count) + (ContainerSize * containers.Count) + (ItemSize * items.Count) + (AmbientSize * ambients.Count) + (VariableSize * variables.Count) + (DoorSize * doors.Count) + (4 * vertices.Count) + (AnimationSize * animations.Count) + (areaExplorations.Count) + (TiledObjectSize * tiledObjects.Count);
            header.RestInterruptionOffset = HeaderSize + (ActorSize * actors.Count) + (RegionSize * regions.Count) + (Spawnsize * spawns.Count) + (EntranceSize * entrances.Count) + (ContainerSize * containers.Count) + (ItemSize * items.Count) + (AmbientSize * ambients.Count) + (VariableSize * variables.Count) + (DoorSize * doors.Count) + (4 * vertices.Count) + (AnimationSize * animations.Count) + (areaExplorations.Count) + (TiledObjectSize * tiledObjects.Count) + (SongSize * songs.Count);
            header.AutomatCount = Convert.ToInt16(notes.Count);
            header.AutomatOffset = HeaderSize + (ActorSize * actors.Count) + (RegionSize * regions.Count) + (Spawnsize * spawns.Count) + (EntranceSize * entrances.Count) + (ContainerSize * containers.Count) + (ItemSize * items.Count) + (AmbientSize * ambients.Count) + (VariableSize * variables.Count) + (DoorSize * doors.Count) + (4 * vertices.Count) + (AnimationSize * animations.Count) + (areaExplorations.Count) + (TiledObjectSize * tiledObjects.Count) + (InterruptionSize * interruptions.Count) + (SongSize * songs.Count);
            header.ProjectileCount = Convert.ToInt16(projectiles.Count);
            header.ProjectileOffset = HeaderSize + (ActorSize * actors.Count) + (RegionSize * regions.Count) + (Spawnsize * spawns.Count) + (EntranceSize * entrances.Count) + (ContainerSize * containers.Count) + (ItemSize * items.Count) + (AmbientSize * ambients.Count) + (VariableSize * variables.Count) + (DoorSize * doors.Count) + (4 * vertices.Count) + (AnimationSize * animations.Count) + (areaExplorations.Count) + (NoteSize * notes.Count) + (TiledObjectSize * tiledObjects.Count);

            using (MemoryStream s = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(s))
                {
                    var headerAsBytes = Common.WriteStruct(header);

                    bw.Write(headerAsBytes);

                    foreach (var actor in actors)
                    {
                        var actorAsBytes = Common.WriteStruct(actor);
                        bw.Write(actorAsBytes);
                    }

                    foreach (var region in regions)
                    {
                        var regionAsBytes = Common.WriteStruct(region);
                        bw.Write(regionAsBytes);
                    }

                    foreach (var spawn in spawns)
                    {
                        var spawnAsBytes = Common.WriteStruct(spawn);
                        bw.Write(spawnAsBytes);
                    }

                    foreach (var entrance in entrances)
                    {
                        var entranceAsBytes = Common.WriteStruct(entrance);
                        bw.Write(entranceAsBytes);
                    }

                    foreach (var container in containers)
                    {
                        var containerAsBytes = Common.WriteStruct(container);
                        bw.Write(containerAsBytes);
                    }

                    foreach (var item in items)
                    {
                        var itemAsBytes = Common.WriteStruct(item);
                        bw.Write(itemAsBytes);
                    }

                    foreach (var ambient in ambients)
                    {
                        var ambientAsBytes = Common.WriteStruct(ambient);
                        bw.Write(ambientAsBytes);
                    }

                    foreach (var variable in variables)
                    {
                        var variableAsBytes = Common.WriteStruct(variable);
                        bw.Write(variableAsBytes);
                    }

                    foreach (var exploration in areaExplorations)
                    {
                        var explorationAsBytes = Common.WriteStruct(exploration);
                        bw.Write(explorationAsBytes);
                    }

                    foreach (var door in doors)
                    {
                        var doorAsBytes = Common.WriteStruct(door);
                        bw.Write(doorAsBytes);
                    }

                    foreach (var tiledObject in tiledObjects)
                    {
                        var tiledObjectAsBytes = Common.WriteStruct(tiledObject);
                        bw.Write(tiledObjectAsBytes);
                    }

                    foreach (var vertex in vertices)
                    {
                        var vertexAsBytes = Common.WriteStruct(vertex);
                        bw.Write(vertexAsBytes);
                    }

                    foreach (var animation in animations)
                    {
                        var animationAsBytes = Common.WriteStruct(animation);
                        bw.Write(animationAsBytes);
                    }

                    foreach (var song in songs)
                    {
                        var songAsBytes = Common.WriteStruct(song);
                        bw.Write(songAsBytes);
                    }

                    foreach (var interruption in interruptions)
                    {
                        var interruptionAsBytes = Common.WriteStruct(interruption);
                        bw.Write(interruptionAsBytes);
                    }

                    foreach (var note in notes)
                    {
                        var noteAsBytes = Common.WriteStruct(note);
                        bw.Write(noteAsBytes);
                    }

                    foreach (var projectile in projectiles)
                    {
                        var projectileAsBytes = Common.WriteStruct(projectile);
                        bw.Write(projectileAsBytes);
                    }

                    if (BackupManger != null)
                    {
                        BackupManger.BackupFile(file, file.Filename, file.FileType, this);
                    }

                    using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    {
                        bw.BaseStream.Position = 0;
                        bw.BaseStream.CopyTo(fs);
                        fs.Flush(flushToDisk: true);
                    }
                }
            }
            return true;
        }
    }
}