﻿using ii.InfinityEngine.Binary;
using ii.InfinityEngine.Files;
using ii.InfinityEngine.Writers.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Numerics;

namespace ii.InfinityEngine.Writers
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

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not AreFile)
                throw new ArgumentException("File is not a valid are file");

            var areFile = file as AreFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(areFile) == areFile.Checksum))
                return false;

            List<AreActorBinary> actors = [];
            List<AreRegionBinary> regions = [];
            List<AreSpawnPointBinary> spawns = [];
            List<AreEntranceBinary> entrances = [];
            List<AreContainerBinary> containers = [];
            List<AreItemBinary> items = [];
            List<AreAmbientBinary> ambients = [];
            List<AreVariableBinary> variables = [];
            List<AreDoorBinary> doors = [];
            List<AreAnimationBinary> animations = [];
            List<AreNoteBinary> notes = [];
            List<AreTiledObjectBinary> tiledObjects = [];
            List<AreProjectileBinary> projectiles = [];
            List<AreSongBinary> songs = [];
            List<AreInterruptionBinary> interruptions = [];
            List<byte> areaExplorations = new List<byte>();
            List<Int32> vertices = [];

            foreach (var interruption in areFile.interruptions)
            {
                var interruptionBinary = new AreInterruptionBinary();
                interruptionBinary.CreatureCount = interruption.CreatureCount;
                interruptionBinary.DayProbability = interruption.DayProbability;
                interruptionBinary.Difficulty = interruption.Difficulty;
                interruptionBinary.Enabled = interruption.Enabled;
                interruptionBinary.MaximumCreaturesToSpawn = interruption.MaximumCreaturesToSpawn;
                interruptionBinary.Name = interruption.Name;
                interruptionBinary.NightProbability = interruption.NightProbability;
                interruptionBinary.RemovalTime = interruption.RemovalTime;
                interruptionBinary.ResRef1 = interruption.ResRef1;
                interruptionBinary.ResRef2 = interruption.ResRef2;
                interruptionBinary.ResRef3 = interruption.ResRef3;
                interruptionBinary.ResRef4 = interruption.ResRef4;
                interruptionBinary.ResRef5 = interruption.ResRef5;
                interruptionBinary.ResRef6 = interruption.ResRef6;
                interruptionBinary.ResRef7 = interruption.ResRef7;
                interruptionBinary.ResRef8 = interruption.ResRef8;
                interruptionBinary.ResRef9 = interruption.ResRef9;
                interruptionBinary.ResRef10 = interruption.ResRef10;
                interruptionBinary.RestrictionDistance = interruption.RestrictionDistance;                
                interruptionBinary.Text1 = Common.WriteString(interruption.Text1, TlkFile);
                interruptionBinary.Text2 = Common.WriteString(interruption.Text2, TlkFile);
                interruptionBinary.Text3 = Common.WriteString(interruption.Text3, TlkFile);
                interruptionBinary.Text4 = Common.WriteString(interruption.Text4, TlkFile);
                interruptionBinary.Text5 = Common.WriteString(interruption.Text5, TlkFile);
                interruptionBinary.Text6 = Common.WriteString(interruption.Text6, TlkFile);
                interruptionBinary.Text7 = Common.WriteString(interruption.Text7, TlkFile);
                interruptionBinary.Text8 = Common.WriteString(interruption.Text8, TlkFile);
                interruptionBinary.Text9 = Common.WriteString(interruption.Text9, TlkFile);
                interruptionBinary.Text10 = Common.WriteString(interruption.Text10, TlkFile);
                interruptionBinary.Unknownac = interruption.Unknownac;
                interruptions.Add(interruptionBinary);
            }

            foreach (var song in areFile.songs)
            {
                var songBinary = new AreSongBinary();
                songBinary.BattleSong = song.BattleSong;
                songBinary.DayAmbient1Wav = song.DayAmbient1Wav;
                songBinary.DayAmbient2Wav = song.DayAmbient2Wav;
                songBinary.DayAmbientVolume = song.DayAmbientVolume;
                songBinary.DaySong = song.DaySong;
                songBinary.LoseSong = song.LoseSong;
                songBinary.NightAmbient1Wav = song.NightAmbient1Wav;
                songBinary.NightAmbient2Wav = song.NightAmbient2Wav;
                songBinary.NightAmbientVolume = song.NightAmbientVolume;
                songBinary.NightSong = song.NightSong;
                songBinary.Reverb = song.Reverb;
                songBinary.AltMusic1 = song.AltMusic1;
                songBinary.AltMusic2 = song.AltMusic2;
                songBinary.AltMusic3 = song.AltMusic3;
                songBinary.AltMusic4 = song.AltMusic4;
                songBinary.AltMusic5 = song.AltMusic5;
                songBinary.Unknown54 = song.Unknown54;
                songBinary.WinSong = song.WinSong;
                songs.Add(songBinary);
            }

            foreach (var projectile in areFile.projectiles)
            {
                var projectileBinary = new AreProjectileBinary();
                projectileBinary.EATarget = projectile.EATarget;
                projectileBinary.EffectOffset = projectile.EffectOffset;
                projectileBinary.EffectSize = projectile.EffectSize;
                projectileBinary.MissileId = projectile.MissileId;
                projectileBinary.PartyOwnerIndex = projectile.PartyOwnerIndex;
                projectileBinary.Resref = projectile.Resref;
                projectileBinary.TickUntilTriggerCheck = projectile.TickUntilTriggerCheck;
                projectileBinary.TriggersRemaining = projectile.TriggersRemaining;
                projectileBinary.XCoordinate = projectile.XCoordinate;
                projectileBinary.YCoordinate = projectile.YCoordinate;
                projectileBinary.ZCoordinate = projectile.ZCoordinate;
                projectiles.Add(projectileBinary);
            }

            foreach (var tiledObject in areFile.tiledObjects)
            {
                var tiledObjectBinary = new AreTiledObjectBinary();
                tiledObjectBinary.ClosedSearchCount = tiledObject.ClosedSearchCount;
                tiledObjectBinary.ClosedSearchOffset = tiledObject.ClosedSearchOffset;
                tiledObjectBinary.Name = tiledObject.Name;
                tiledObjectBinary.OpenSearchCount = tiledObject.OpenSearchCount;
                tiledObjectBinary.OpenSearchOffset = tiledObject.OpenSearchOffset;
                tiledObjectBinary.Unknown3c = tiledObject.Unknown3c;
                tiledObjectBinary.Flags = tiledObject.Flags.InSecondaryState ? tiledObjectBinary.Flags | Common.Bit0 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.CanBeSeenThrough ? tiledObjectBinary.Flags | Common.Bit1 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit2 ? tiledObjectBinary.Flags | Common.Bit2 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit3 ? tiledObjectBinary.Flags | Common.Bit3 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit4 ? tiledObjectBinary.Flags | Common.Bit4 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit5 ? tiledObjectBinary.Flags | Common.Bit5 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit6 ? tiledObjectBinary.Flags | Common.Bit6 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit7 ? tiledObjectBinary.Flags | Common.Bit7 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit8 ? tiledObjectBinary.Flags | Common.Bit8 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit9 ? tiledObjectBinary.Flags | Common.Bit9 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit10 ? tiledObjectBinary.Flags | Common.Bit10 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit11 ? tiledObjectBinary.Flags | Common.Bit11 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit12 ? tiledObjectBinary.Flags | Common.Bit12 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit13 ? tiledObjectBinary.Flags | Common.Bit13 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit14 ? tiledObjectBinary.Flags | Common.Bit14 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit15 ? tiledObjectBinary.Flags | Common.Bit15 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit16 ? tiledObjectBinary.Flags | Common.Bit16 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit17 ? tiledObjectBinary.Flags | Common.Bit17 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit18 ? tiledObjectBinary.Flags | Common.Bit18 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit19 ? tiledObjectBinary.Flags | Common.Bit19 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit20 ? tiledObjectBinary.Flags | Common.Bit20 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit21 ? tiledObjectBinary.Flags | Common.Bit21 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit22 ? tiledObjectBinary.Flags | Common.Bit22 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit23 ? tiledObjectBinary.Flags | Common.Bit23 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit24 ? tiledObjectBinary.Flags | Common.Bit24 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit25 ? tiledObjectBinary.Flags | Common.Bit25 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit26 ? tiledObjectBinary.Flags | Common.Bit26 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit27 ? tiledObjectBinary.Flags | Common.Bit27 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit28 ? tiledObjectBinary.Flags | Common.Bit28 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit29 ? tiledObjectBinary.Flags | Common.Bit29 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit30 ? tiledObjectBinary.Flags | Common.Bit30 : tiledObjectBinary.Flags;
                tiledObjectBinary.Flags = tiledObject.Flags.Bit31 ? tiledObjectBinary.Flags | Common.Bit31 : tiledObjectBinary.Flags;
                tiledObjectBinary.TileId = tiledObject.TileId;
                tiledObjects.Add(tiledObjectBinary);
            }

            foreach (var note in areFile.notes)
            {
                var noteBinary = new AreNoteBinary();
                noteBinary.Colour = (Int16)note.Colour;
                noteBinary.Location = note.Location;
                //noteBinary.NoteCountPlus = note.NoteCountPlus;
                noteBinary.Text = Common.WriteString(note.Text, TlkFile);
                noteBinary.XCoordinate = note.XCoordinate;
                noteBinary.YCoordinate = note.YCoordinate;
                notes.Add(noteBinary);
            }

            foreach (var animation in areFile.animations)
            {
                var animationBinary = new AreAnimationBinary();
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Am1 ? animationBinary.AnimationAppearenceSchedule | Common.Bit0 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Am2 ? animationBinary.AnimationAppearenceSchedule | Common.Bit1 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Am3 ? animationBinary.AnimationAppearenceSchedule | Common.Bit2 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Am4 ? animationBinary.AnimationAppearenceSchedule | Common.Bit3 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Am5 ? animationBinary.AnimationAppearenceSchedule | Common.Bit4 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Am6 ? animationBinary.AnimationAppearenceSchedule | Common.Bit5 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Am7 ? animationBinary.AnimationAppearenceSchedule | Common.Bit6 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Am8 ? animationBinary.AnimationAppearenceSchedule | Common.Bit7 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Am9 ? animationBinary.AnimationAppearenceSchedule | Common.Bit8 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Am10 ? animationBinary.AnimationAppearenceSchedule | Common.Bit9 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Am11 ? animationBinary.AnimationAppearenceSchedule | Common.Bit10 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Am12 ? animationBinary.AnimationAppearenceSchedule | Common.Bit11 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Pm1 ? animationBinary.AnimationAppearenceSchedule | Common.Bit12 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Pm2 ? animationBinary.AnimationAppearenceSchedule | Common.Bit13 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Pm3 ? animationBinary.AnimationAppearenceSchedule | Common.Bit14 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Pm4 ? animationBinary.AnimationAppearenceSchedule | Common.Bit15 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Pm5 ? animationBinary.AnimationAppearenceSchedule | Common.Bit16 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Pm6 ? animationBinary.AnimationAppearenceSchedule | Common.Bit17 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Pm7 ? animationBinary.AnimationAppearenceSchedule | Common.Bit18 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Pm8 ? animationBinary.AnimationAppearenceSchedule | Common.Bit19 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Pm9 ? animationBinary.AnimationAppearenceSchedule | Common.Bit20 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Pm10 ? animationBinary.AnimationAppearenceSchedule | Common.Bit21 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Pm11 ? animationBinary.AnimationAppearenceSchedule | Common.Bit22 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Pm12 ? animationBinary.AnimationAppearenceSchedule | Common.Bit23 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Bit24 ? animationBinary.AnimationAppearenceSchedule | Common.Bit24 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Bit25 ? animationBinary.AnimationAppearenceSchedule | Common.Bit25 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Bit26 ? animationBinary.AnimationAppearenceSchedule | Common.Bit26 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Bit27 ? animationBinary.AnimationAppearenceSchedule | Common.Bit27 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Bit28 ? animationBinary.AnimationAppearenceSchedule | Common.Bit28 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Bit29 ? animationBinary.AnimationAppearenceSchedule | Common.Bit29 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Bit30 ? animationBinary.AnimationAppearenceSchedule | Common.Bit30 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.AnimationAppearenceSchedule = animation.AnimationAppearenceSchedule.Bit31 ? animationBinary.AnimationAppearenceSchedule | Common.Bit31 : animationBinary.AnimationAppearenceSchedule;
                animationBinary.BamAnimation = animation.BamAnimation;
                animationBinary.BamFrame = animation.BamFrame;
                animationBinary.BamSequence = animation.BamSequence;
                animationBinary.Flags = animation.Flags.Enabled ? animationBinary.Flags | Common.Bit0 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.TransparentBlack ? animationBinary.Flags | Common.Bit1 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.NotLightSource ? animationBinary.Flags | Common.Bit2 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.PartialAnimation ? animationBinary.Flags | Common.Bit3 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.SynchronizedDraw ? animationBinary.Flags | Common.Bit4 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.RandomStartFrame ? animationBinary.Flags | Common.Bit5 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.NotCoveredByWall ? animationBinary.Flags | Common.Bit6 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.DisableOnSlowMachines ? animationBinary.Flags | Common.Bit7 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.DrawAsBackground ? animationBinary.Flags | Common.Bit8 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.PlayAllFrames ? animationBinary.Flags | Common.Bit9 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.UsePaletteBitmap ? animationBinary.Flags | Common.Bit10 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.MirrorYAxis ? animationBinary.Flags | Common.Bit11 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.DoNotRemoveInCombat ? animationBinary.Flags | Common.Bit12 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.WbmResref ? animationBinary.Flags | Common.Bit13 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.DrawStenciled ? animationBinary.Flags | Common.Bit14 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.PvrzResref ? animationBinary.Flags | Common.Bit15 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.Bit16 ? animationBinary.Flags | Common.Bit16 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.Bit17 ? animationBinary.Flags | Common.Bit17 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.Bit18 ? animationBinary.Flags | Common.Bit18 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.Bit19 ? animationBinary.Flags | Common.Bit19 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.Bit20 ? animationBinary.Flags | Common.Bit20 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.Bit21 ? animationBinary.Flags | Common.Bit21 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.Bit22 ? animationBinary.Flags | Common.Bit22 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.Bit23 ? animationBinary.Flags | Common.Bit23 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.Bit24 ? animationBinary.Flags | Common.Bit24 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.Bit25 ? animationBinary.Flags | Common.Bit25 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.Bit26 ? animationBinary.Flags | Common.Bit26 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.Bit27 ? animationBinary.Flags | Common.Bit27 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.Bit28 ? animationBinary.Flags | Common.Bit28 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.Bit29 ? animationBinary.Flags | Common.Bit29 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.Bit30 ? animationBinary.Flags | Common.Bit30 : animationBinary.Flags;
                animationBinary.Flags = animation.Flags.Bit31 ? animationBinary.Flags | Common.Bit31 : animationBinary.Flags;
                animationBinary.Height = animation.Height;
                animationBinary.LoopChance = animation.LoopChance;
                animationBinary.Name = animation.Name;
                animationBinary.Palette = animation.Palette;
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
                var doorBinary = new AreDoorBinary();
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
                doorBinary.DialogResref = door.DialogResref;
                doorBinary.DoorCloseSound = door.DoorCloseSound;
                doorBinary.DoorId = door.DoorId;
                doorBinary.DoorOpenSound = door.DoorOpenSound;
                doorBinary.DoorScript = door.DoorScript;
                doorBinary.DoorState1X = door.DoorState1X;
                doorBinary.DoorState1Y = door.DoorState1Y;
                doorBinary.DoorState2X = door.DoorState2X;
                doorBinary.DoorState2Y = door.DoorState2Y;
                doorBinary.Flags = door.Flags.DoorOpen ? doorBinary.Flags | Common.Bit0 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.DoorLocked ? doorBinary.Flags | Common.Bit1 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.ResetTrap ? doorBinary.Flags | Common.Bit2 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.DetectableTrap ? doorBinary.Flags | Common.Bit3 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.DoorForced ? doorBinary.Flags | Common.Bit4 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.CannotClose ? doorBinary.Flags | Common.Bit5 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Linked ? doorBinary.Flags | Common.Bit6 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.DoorHidden ? doorBinary.Flags | Common.Bit7 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.DoorFound ? doorBinary.Flags | Common.Bit8 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.DoNotBlockLos ? doorBinary.Flags | Common.Bit9 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.RemoveKey ? doorBinary.Flags | Common.Bit10 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.IgnoreObstaclesWhenClosing ? doorBinary.Flags | Common.Bit11 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit12 ? doorBinary.Flags | Common.Bit12 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit13 ? doorBinary.Flags | Common.Bit13 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit14 ? doorBinary.Flags | Common.Bit14 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit15 ? doorBinary.Flags | Common.Bit15 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit16 ? doorBinary.Flags | Common.Bit16 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit17 ? doorBinary.Flags | Common.Bit17 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit18 ? doorBinary.Flags | Common.Bit18 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit19 ? doorBinary.Flags | Common.Bit19 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit20 ? doorBinary.Flags | Common.Bit20 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit21 ? doorBinary.Flags | Common.Bit21 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit22 ? doorBinary.Flags | Common.Bit22 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit23 ? doorBinary.Flags | Common.Bit23 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit24 ? doorBinary.Flags | Common.Bit24 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit25 ? doorBinary.Flags | Common.Bit25 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit26 ? doorBinary.Flags | Common.Bit26 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit27 ? doorBinary.Flags | Common.Bit27 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit28 ? doorBinary.Flags | Common.Bit28 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit29 ? doorBinary.Flags | Common.Bit29 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit30 ? doorBinary.Flags | Common.Bit30 : doorBinary.Flags;
                doorBinary.Flags = door.Flags.Bit31 ? doorBinary.Flags | Common.Bit31 : doorBinary.Flags;
                doorBinary.IsTrap = door.IsTrap;
                doorBinary.KeyItem = door.KeyItem;
                doorBinary.LockDifficulty = door.LockDifficulty;
                doorBinary.LockpickString = Common.WriteString(door.LockpickString, TlkFile);
                doorBinary.Name = door.Name;
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
                doorBinary.TravelTriggerName = door.TravelTriggerName;
                doorBinary.Unknownc0 = door.Unknownc0;
                doorBinary.Hitpoints = door.Hitpoints;
                doorBinary.ArmourClass = door.ArmourClass;
                doors.Add(doorBinary);
            }

            foreach (var variable in areFile.variables)
            {
                var variableBinary = new AreVariableBinary();
                variableBinary.Name = variable.Name;
                variableBinary.Type = variable.Type;
                variableBinary.ResourceType = variable.ResourceType;
                variableBinary.ValueDword = variable.ValueDword;
                variableBinary.ValueInt = variable.ValueInt;
                variableBinary.ValueDouble = variable.ValueDouble;
                variableBinary.ScriptName = variable.ScriptName;
                variables.Add(variableBinary);
            }

            foreach (var ambient in areFile.ambients)
            {
                var ambientBinary = new AreAmbientBinary();
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Am1 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit0 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Am2 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit1 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Am3 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit2 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Am4 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit3 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Am5 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit4 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Am6 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit5 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Am7 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit6 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Am8 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit7 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Am9 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit8 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Am10 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit9 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Am11 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit10 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Am12 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit11 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Pm1 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit12 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Pm2 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit13 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Pm3 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit14 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Pm4 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit15 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Pm5 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit16 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Pm6 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit17 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Pm7 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit18 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Pm8 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit19 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Pm9 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit20 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Pm10 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit21 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Pm11 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit22 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Pm12 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit23 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Bit24 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit24 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Bit25 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit25 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Bit26 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit26 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Bit27 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit27 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Bit28 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit28 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Bit29 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit29 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Bit30 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit30 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.AmbientAppearenceSchedule = ambient.AmbientAppearenceSchedule.Bit31 ? ambientBinary.AmbientAppearenceSchedule | Common.Bit31 : ambientBinary.AmbientAppearenceSchedule;
                ambientBinary.Flags = ambient.Flags.Enabled ? ambientBinary.Flags | Common.Bit0 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Looping ? ambientBinary.Flags | Common.Bit1 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.IgnoreRadius ? ambientBinary.Flags | Common.Bit2 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.PlayInRandomOrder ? ambientBinary.Flags | Common.Bit3 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.HighMemoryAmbient ? ambientBinary.Flags | Common.Bit4 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit5 ? ambientBinary.Flags | Common.Bit5 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit6 ? ambientBinary.Flags | Common.Bit6 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit7 ? ambientBinary.Flags | Common.Bit7 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit8 ? ambientBinary.Flags | Common.Bit8 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit9 ? ambientBinary.Flags | Common.Bit9 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit10 ? ambientBinary.Flags | Common.Bit10 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit11 ? ambientBinary.Flags | Common.Bit11 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit12 ? ambientBinary.Flags | Common.Bit12 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit13 ? ambientBinary.Flags | Common.Bit13 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit14 ? ambientBinary.Flags | Common.Bit14 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit15 ? ambientBinary.Flags | Common.Bit15 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit16 ? ambientBinary.Flags | Common.Bit16 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit17 ? ambientBinary.Flags | Common.Bit17 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit18 ? ambientBinary.Flags | Common.Bit18 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit19 ? ambientBinary.Flags | Common.Bit19 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit20 ? ambientBinary.Flags | Common.Bit20 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit21 ? ambientBinary.Flags | Common.Bit21 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit22 ? ambientBinary.Flags | Common.Bit22 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit23 ? ambientBinary.Flags | Common.Bit23 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit24 ? ambientBinary.Flags | Common.Bit24 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit25 ? ambientBinary.Flags | Common.Bit25 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit26 ? ambientBinary.Flags | Common.Bit26 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit27 ? ambientBinary.Flags | Common.Bit27 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit28 ? ambientBinary.Flags | Common.Bit28 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit29 ? ambientBinary.Flags | Common.Bit29 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit30 ? ambientBinary.Flags | Common.Bit30 : ambientBinary.Flags;
                ambientBinary.Flags = ambient.Flags.Bit31 ? ambientBinary.Flags | Common.Bit31 : ambientBinary.Flags;
                ambientBinary.FrequencyBase = ambient.FrequencyBase;
                ambientBinary.FrequencyVariation = ambient.FrequencyVariation;
                ambientBinary.Height = ambient.Height;
                ambientBinary.Name = ambient.Name;
                ambientBinary.Radius = ambient.Radius;
                ambientBinary.Resref1 = ambient.Resref1;
                ambientBinary.Resref2 = ambient.Resref2;
                ambientBinary.Resref3 = ambient.Resref3;
                ambientBinary.Resref4 = ambient.Resref4;
                ambientBinary.Resref5 = ambient.Resref5;
                ambientBinary.Resref6 = ambient.Resref6;
                ambientBinary.Resref7 = ambient.Resref7;
                ambientBinary.Resref8 = ambient.Resref8;
                ambientBinary.Resref9 = ambient.Resref9;
                ambientBinary.Resref10 = ambient.Resref10;
                ambientBinary.ResRefCount = ambient.ResRefCount; //xx
                ambientBinary.XCoordinate = ambient.XCoordinate;
                ambientBinary.YCoordinate = ambient.YCoordinate;
                ambientBinary.PitchVariance = ambient.PitchVariance;
                ambientBinary.VolumeVariance = ambient.VolumeVariance;
                ambientBinary.Unknown82 = ambient.Unknown82;
                ambientBinary.Unknownc0 = ambient.Unknown94;
                ambientBinary.Volume = ambient.Volume;
                ambients.Add(ambientBinary);
            }

            foreach (var container in areFile.containers)
            {
                var containerBinary = new AreContainerBinary();
                containerBinary.BoundingBoxBottom = container.BoundingBoxBottom;
                containerBinary.BoundingBoxLeft = container.BoundingBoxLeft;
                containerBinary.BoundingBoxRight = container.BoundingBoxRight;
                containerBinary.BoundingBoxTop = container.BoundingBoxTop;
                containerBinary.ContainerType = (Int16)container.ContainerType;
                containerBinary.Flags = container.Flags.Locked ? containerBinary.Flags | Common.Bit0 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.DisabledIfNoOwner ? containerBinary.Flags | Common.Bit1 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.MagicalLock ? containerBinary.Flags | Common.Bit2 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.TrapResets ? containerBinary.Flags | Common.Bit3 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.RemoveOnly ? containerBinary.Flags | Common.Bit4 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.Disabled ? containerBinary.Flags | Common.Bit5 : containerBinary.Flags;
                containerBinary.Flags = container.Flags.DoNotClear ? containerBinary.Flags | Common.Bit6 : containerBinary.Flags;
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
                    var itemBinary = new AreItemBinary();
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
                    itemBinary.ItemResref = container.items[j].ItemResref;
                    items.Add(itemBinary);
                }

                containerBinary.KeyItem = container.KeyItem;
                containerBinary.LockDifficulty = container.LockDifficulty;
                containerBinary.LockpickString = Common.WriteString(container.LockpickString, TlkFile);
                containerBinary.Name = container.Name;
                containerBinary.TrapDetected = container.TrapDetected;
                containerBinary.TrapDetectionDifficulty = container.TrapDetectionDifficulty;
                containerBinary.TrapLaunchXCoordinate = container.TrapLaunchXCoordinate;
                containerBinary.TrapLaunchYCoordinate = container.TrapLaunchYCoordinate;
                containerBinary.TrapRemovalDifficulty = container.TrapRemovalDifficulty;
                containerBinary.TrapScript = container.TrapScript;
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
                var entranceBinary = new AreEntranceBinary();
                entranceBinary.Name = entrance.Name;
                entranceBinary.Orientation = entrance.Orientation;
                entranceBinary.Unknown26 = entrance.Unknown26;
                entranceBinary.XCoordinate = entrance.XCoordinate;
                entranceBinary.YCoordinate = entrance.YCoordinate;
                entrances.Add(entranceBinary);
            }

            foreach (var spawn in areFile.spawns)
            {
                var spawnBinary = new AreSpawnPointBinary();
                spawnBinary.ActorRemovalTime = spawn.ActorRemovalTime;
                spawnBinary.BaseCreatureNumberToSpawn = spawn.BaseCreatureNumberToSpawn;
                spawnBinary.CreatureSpawnCount = spawn.CreatureSpawnCount;
                spawnBinary.Enabled = spawn.Enabled;
                spawnBinary.Frequency = spawn.Frequency;
                spawnBinary.MaximumCreaturesToSpawn = spawn.MaximumCreaturesToSpawn;
                spawnBinary.MovementRestrictionDistance = spawn.MovementRestrictionDistance;
                spawnBinary.MovementRestrictionDistanceToObject = spawn.MovementRestrictionDistanceToObject;
                spawnBinary.Name = spawn.Name;
                spawnBinary.ProbabilityDay = spawn.ProbabilityDay;
                spawnBinary.ProbabilityNight = spawn.ProbabilityNight;
                spawnBinary.Resref1 = spawn.Resref1;
                spawnBinary.Resref2 = spawn.Resref2;
                spawnBinary.Resref3 = spawn.Resref3;
                spawnBinary.Resref4 = spawn.Resref4;
                spawnBinary.Resref5 = spawn.Resref5;
                spawnBinary.Resref6 = spawn.Resref6;
                spawnBinary.Resref7 = spawn.Resref7;
                spawnBinary.Resref8 = spawn.Resref8;
                spawnBinary.Resref9 = spawn.Resref9;
                spawnBinary.Resref10 = spawn.Resref10;
                spawnBinary.SpawnMethod = (Int16)(spawn.SpawnMethod.SpawnUntilPaused ? spawnBinary.SpawnMethod | Common.Bit0 : spawnBinary.SpawnMethod);
                spawnBinary.SpawnMethod = (Int16)(spawn.SpawnMethod.DisableAfterSpawn ? spawnBinary.SpawnMethod | Common.Bit1 : spawnBinary.SpawnMethod);
                spawnBinary.SpawnMethod = (Int16)(spawn.SpawnMethod.SpawnPaused ? spawnBinary.SpawnMethod | Common.Bit2 : spawnBinary.SpawnMethod);
                spawnBinary.SpawnMethod = (Int16)(spawn.SpawnMethod.Bit3 ? spawnBinary.SpawnMethod | Common.Bit3 : spawnBinary.SpawnMethod);
                spawnBinary.SpawnMethod = (Int16)(spawn.SpawnMethod.Bit4 ? spawnBinary.SpawnMethod | Common.Bit4 : spawnBinary.SpawnMethod);
                spawnBinary.SpawnMethod = (Int16)(spawn.SpawnMethod.Bit5 ? spawnBinary.SpawnMethod | Common.Bit5 : spawnBinary.SpawnMethod);
                spawnBinary.SpawnMethod = (Int16)(spawn.SpawnMethod.Bit6 ? spawnBinary.SpawnMethod | Common.Bit6 : spawnBinary.SpawnMethod);
                spawnBinary.SpawnMethod = (Int16)(spawn.SpawnMethod.Bit7 ? spawnBinary.SpawnMethod | Common.Bit7 : spawnBinary.SpawnMethod);
                spawnBinary.SpawnMethod = (Int16)(spawn.SpawnMethod.Bit8 ? spawnBinary.SpawnMethod | Common.Bit8 : spawnBinary.SpawnMethod);
                spawnBinary.SpawnMethod = (Int16)(spawn.SpawnMethod.Bit9 ? spawnBinary.SpawnMethod | Common.Bit9 : spawnBinary.SpawnMethod);
                spawnBinary.SpawnMethod = (Int16)(spawn.SpawnMethod.Bit10 ? spawnBinary.SpawnMethod | Common.Bit10 : spawnBinary.SpawnMethod);
                spawnBinary.SpawnMethod = (Int16)(spawn.SpawnMethod.Bit11 ? spawnBinary.SpawnMethod | Common.Bit11 : spawnBinary.SpawnMethod);
                spawnBinary.SpawnMethod = (Int16)(spawn.SpawnMethod.Bit12 ? spawnBinary.SpawnMethod | Common.Bit12 : spawnBinary.SpawnMethod);
                spawnBinary.SpawnMethod = (Int16)(spawn.SpawnMethod.Bit13 ? spawnBinary.SpawnMethod | Common.Bit13 : spawnBinary.SpawnMethod);
                spawnBinary.SpawnMethod = (Int16)(spawn.SpawnMethod.Bit14 ? spawnBinary.SpawnMethod | Common.Bit14 : spawnBinary.SpawnMethod);
                spawnBinary.SpawnMethod = (Int16)(spawn.SpawnMethod.Bit15 ? spawnBinary.SpawnMethod | Common.Bit15 : spawnBinary.SpawnMethod);
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Am1 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit0 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Am2 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit1 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Am3 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit2 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Am4 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit3 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Am5 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit4 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Am6 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit5 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Am7 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit6 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Am8 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit7 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Am9 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit8 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Am10 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit9 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Am11 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit10 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Am12 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit11 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Pm1 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit12 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Pm2 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit13 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Pm3 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit14 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Pm4 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit15 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Pm5 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit16 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Pm6 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit17 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Pm7 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit18 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Pm8 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit19 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Pm9 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit20 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Pm10 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit21 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Pm11 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit22 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Pm12 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit23 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Bit24 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit24 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Bit25 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit25 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Bit26 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit26 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Bit27 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit27 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Bit28 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit28 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Bit29 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit29 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Bit30 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit30 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.SpawnPointAppearenceSchedule = spawn.SpawnPointAppearenceSchedule.Bit31 ? spawnBinary.SpawnPointAppearenceSchedule | Common.Bit31 : spawnBinary.SpawnPointAppearenceSchedule;
                spawnBinary.Unknowna2 = spawn.Unknowna2;
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
                spawnBinary.Unknowna2 = spawn.Unknowna2;
                spawns.Add(spawnBinary);
            }

            foreach (var region in areFile.regions)
            {
                var regionBinary = new AreRegionBinary();
                regionBinary.AlternativeUsePointXCoordinate = region.AlternativeUsePointXCoordinate;
                regionBinary.AlternativeUsePointYCoordinate = region.AlternativeUsePointYCoordinate;
                regionBinary.BoundingBoxBottom = region.BoundingBoxBottom;
                regionBinary.BoundingBoxLeft = region.BoundingBoxLeft;
                regionBinary.BoundingBoxRight = region.BoundingBoxRight;
                regionBinary.BoundingBoxTop = region.BoundingBoxTop;
                regionBinary.Cursor = region.Cursor;
                regionBinary.DestinationArea = region.DestinationArea;
                regionBinary.DestinationEntrance = region.DestinationEntrance;
                regionBinary.DialogFile = region.DialogFile;
                regionBinary.DialogName = Common.WriteString(region.DialogName, TlkFile);
                regionBinary.Flags = region.Flags.KeyRequired ? regionBinary.Flags | Common.Bit0 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.ResetTrap ? regionBinary.Flags | Common.Bit1 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.PartyRequired ? regionBinary.Flags | Common.Bit2 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Detectable ? regionBinary.Flags | Common.Bit3 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.EnemiesActivates ? regionBinary.Flags | Common.Bit4 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.TutorialTrigger ? regionBinary.Flags | Common.Bit5 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.NpcActivates ? regionBinary.Flags | Common.Bit6 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.SilentTrigger ? regionBinary.Flags | Common.Bit7 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.Deactivated ? regionBinary.Flags | Common.Bit8 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.NPCCannotPass ? regionBinary.Flags | Common.Bit9 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.AlternativePoint ? regionBinary.Flags | Common.Bit10 : regionBinary.Flags;
                regionBinary.Flags = region.Flags.DoorClosed ? regionBinary.Flags | Common.Bit11 : regionBinary.Flags;
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
                regionBinary.KeyItem = region.KeyItem;
                regionBinary.Name = region.Name;
                regionBinary.RegionScript = region.RegionScript;
                regionBinary.RegionType = (Int16)region.RegionType;
                regionBinary.Sound = region.Sound;
                regionBinary.TalkLocationXCoordinate = region.TalkLocationXCoordinate;
                regionBinary.TalkLocationYCoordinate = region.TalkLocationYCoordinate;
                regionBinary.TrapDetected = region.TrapDetected;
                regionBinary.TrapDetectionDifficulty = region.TrapDetectionDifficulty;
                regionBinary.TrapLaunchXCoordinate = region.TrapLaunchXCoordinate;
                regionBinary.TrapLaunchYCoordinate = region.TrapLaunchYCoordinate;
                regionBinary.TrapRemovalDifficulty = region.TrapRemovalDifficulty;
                regionBinary.TriggerValue = region.TriggerValue;
                regionBinary.Unknown88 = region.Unknown88;
                regionBinary.Unknown8c = region.Unknown8c;
                regionBinary.VertexCount = region.VertexCount;
                regionBinary.VertexIndex = region.VertexIndex;
                regions.Add(regionBinary);
            }

            foreach (var actor in areFile.actors)
            {
                var actorBinary = new AreActorBinary();
                actorBinary.ActorAnimation = actor.ActorAnimation;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Am1 ? actorBinary.ActorAppearenceSchedule | Common.Bit0 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Am2 ? actorBinary.ActorAppearenceSchedule | Common.Bit1 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Am3 ? actorBinary.ActorAppearenceSchedule | Common.Bit2 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Am4 ? actorBinary.ActorAppearenceSchedule | Common.Bit3 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Am5 ? actorBinary.ActorAppearenceSchedule | Common.Bit4 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Am6 ? actorBinary.ActorAppearenceSchedule | Common.Bit5 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Am7 ? actorBinary.ActorAppearenceSchedule | Common.Bit6 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Am8 ? actorBinary.ActorAppearenceSchedule | Common.Bit7 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Am9 ? actorBinary.ActorAppearenceSchedule | Common.Bit8 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Am10 ? actorBinary.ActorAppearenceSchedule | Common.Bit9 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Am11 ? actorBinary.ActorAppearenceSchedule | Common.Bit10 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Am12 ? actorBinary.ActorAppearenceSchedule | Common.Bit11 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Pm1 ? actorBinary.ActorAppearenceSchedule | Common.Bit12 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Pm2 ? actorBinary.ActorAppearenceSchedule | Common.Bit13 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Pm3 ? actorBinary.ActorAppearenceSchedule | Common.Bit14 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Pm4 ? actorBinary.ActorAppearenceSchedule | Common.Bit15 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Pm5 ? actorBinary.ActorAppearenceSchedule | Common.Bit16 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Pm6 ? actorBinary.ActorAppearenceSchedule | Common.Bit17 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Pm7 ? actorBinary.ActorAppearenceSchedule | Common.Bit18 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Pm8 ? actorBinary.ActorAppearenceSchedule | Common.Bit19 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Pm9 ? actorBinary.ActorAppearenceSchedule | Common.Bit20 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Pm10 ? actorBinary.ActorAppearenceSchedule | Common.Bit21 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Pm11 ? actorBinary.ActorAppearenceSchedule | Common.Bit22 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Pm12 ? actorBinary.ActorAppearenceSchedule | Common.Bit23 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Bit24 ? actorBinary.ActorAppearenceSchedule | Common.Bit24 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Bit25 ? actorBinary.ActorAppearenceSchedule | Common.Bit25 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Bit26 ? actorBinary.ActorAppearenceSchedule | Common.Bit26 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Bit27 ? actorBinary.ActorAppearenceSchedule | Common.Bit27 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Bit28 ? actorBinary.ActorAppearenceSchedule | Common.Bit28 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Bit29 ? actorBinary.ActorAppearenceSchedule | Common.Bit29 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Bit30 ? actorBinary.ActorAppearenceSchedule | Common.Bit30 : actorBinary.ActorAppearenceSchedule;
                actorBinary.ActorAppearenceSchedule = actor.ActorAppearenceSchedule.Bit31 ? actorBinary.ActorAppearenceSchedule | Common.Bit31 : actorBinary.ActorAppearenceSchedule;
                actorBinary.Flags = actor.ActorFlags.CreAttached ? actorBinary.Flags | Common.Bit0 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.HasSeenParty ? actorBinary.Flags | Common.Bit1 : actorBinary.Flags;
                actorBinary.Flags = actor.ActorFlags.Invulnerable ? actorBinary.Flags | Common.Bit2 : actorBinary.Flags;
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
                actorBinary.CREFile = actor.CREFile;
                actorBinary.CreOffset = actor.CreOffset;
                actorBinary.CreSize = actor.CreSize;
                actorBinary.CurrentXCoordinate = actor.CurrentXCoordinate;
                actorBinary.CurrentYCoordinate = actor.CurrentYCoordinate;
                actorBinary.DestinationXCoordinate = actor.DestinationXCoordinate;
                actorBinary.DestinationYCoordinate = actor.DestinationYCoordinate;
                actorBinary.Dialog = actor.Dialog;
                actorBinary.FilenameInitialCharacter = actor.FilenameInitialCharacter;
                actorBinary.HasBeenSpawned = actor.HasBeenSpawned;
                actorBinary.MovementRestrictionDistance = actor.MovementRestrictionDistance;
                actorBinary.MovementRestrictionDistanceMoveToObject = actor.MovementRestrictionDistanceMoveToObject;
                actorBinary.Name = actor.Name;
                actorBinary.NumTimesTalkedTo = actor.NumTimesTalkedTo;
                actorBinary.ScriptClass = actor.ScriptClass;
                actorBinary.ScriptDefault = actor.ScriptDefault;
                actorBinary.ScriptGeneral = actor.ScriptGeneral;
                actorBinary.ScriptOverride = actor.ScriptOverride;
                actorBinary.ScriptRace = actor.ScriptRace;
                actorBinary.ScriptSpecific = actor.ScriptSpecific;
                actorBinary.Unknownef = actor.Unknownef;
                actorBinary.Unknown90 = actor.Unknown90;
                actorBinary.Unknown36 = actor.Unknown36;
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

            var header = new AreHeaderBinary();
            header.AreaFlags = areFile.AreaFlags.SaveAllowed ? header.AreaFlags | Common.Bit0 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.TutorialArea ? header.AreaFlags | Common.Bit1 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.DeadMagicZone ? header.AreaFlags | Common.Bit2 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Dream ? header.AreaFlags | Common.Bit3 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.Player1DeathDoesNotEndGame ? header.AreaFlags | Common.Bit4 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.RestingNotAllowed ? header.AreaFlags | Common.Bit5 : header.AreaFlags;
            header.AreaFlags = areFile.AreaFlags.TravelNotAllowed ? header.AreaFlags | Common.Bit6 : header.AreaFlags;
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
            header.AreaScript = areFile.AreaScript;
            header.AreaToTheEast = areFile.AreaToTheEast;
            header.AreaToTheNorth = areFile.AreaToTheNorth;
            header.AreaToTheSouth = areFile.AreaToTheSouth;
            header.AreaToTheWest = areFile.AreaToTheWest;
            header.AreaWED = areFile.AreaWed;
            header.LastSaved = areFile.LastSaved;
            header.RestMovieDay = areFile.RestMovieDay;
            header.RestMovieNight = areFile.RestMovieNight;
            header.OverlayTransparency = areFile.OverlayTransparency;
            header.TiledObjectFlagOffset = areFile.TiledObjectFlagOffset;
            header.TiledObjectFlagCount = areFile.TiledObjectFlagCount;
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

            using var s = new MemoryStream();
            using var bw = new BinaryWriter(s);
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

            BackupManger?.BackupFile(file, file.Filename, file.FileType, this);

            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            bw.BaseStream.Position = 0;
            bw.BaseStream.CopyTo(fs);
            fs.Flush(flushToDisk: true);
            return true;
        }
    }
}