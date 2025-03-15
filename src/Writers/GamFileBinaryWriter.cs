using ii.InfinityEngine.Binary;
using ii.InfinityEngine.Files;
using ii.InfinityEngine.Writers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace ii.InfinityEngine.Writers
{
    public class GamFileBinaryWriter : IGamFileWriter
    {
        const int HeaderSize = 180;
        const int GamNpcStructSize = 352;
        const int GamVariableSize = 84;
        const int GamJournalEntrySize = 12;
        const int GamFamiliarInfoSize = 400;
        const int GamStoredLocationSize = 12;
        const int GamPocketLocationSize = 12;
        const int GamPartyInventorySize = 20;

        public TlkFile TlkFile { get; set; }
        private CreFileBinaryWriter CreWriter = new();
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            int partyinventorycount = 0;

            if (file is not GamFile)
                throw new ArgumentException("File is not a valid gam file");

            var gamFile = file as GamFile;
            CreWriter.TlkFile = TlkFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(gamFile) == gamFile.Checksum))
                return false;

            var binaryPartyMembers = new List<(GamNpcStructBinary data, byte[] cre)>();
            var binaryNonPartyMembers = new List<(GamNpcStructBinary data, byte[] cre)>();
            var binaryVariables = new List<GamVariableBinary>();
            var binaryJournals = new List<GamJournalBinary>();
            var binaryStoredLocations = new List<GamStoredLocationBinary>();
            var binaryPocketPlaneLocations = new List<GamStoredLocationBinary>();

            foreach (var variable in gamFile.Variables)
            {
                var variableBinary = new GamVariableBinary();
                variableBinary.ValueDouble = variable.ValueDouble;
                variableBinary.ValueInt = variable.ValueInt;
                variableBinary.ValueDword = variable.ValueDword;
                variableBinary.Name = variable.Name;
                variableBinary.ResourceType = variable.ResourceType;
                variableBinary.ScriptName = variable.ScriptName;
                variableBinary.Type = variable.Type;
                binaryVariables.Add(variableBinary);
            }

            foreach (var journal in gamFile.JournalEntries)
            {
                var journalBinary = new GamJournalBinary();
                journalBinary.JournalText = Common.WriteString(journal.JournalText, TlkFile);
                journalBinary.Time = journal.Time;
                journalBinary.Chapter = journal.Chapter;
                journalBinary.ReadBy = journal.ReadBy;
                journalBinary.JournalSection = journal.JournalSection;
                journalBinary.LocationFlag = journal.LocationFlag;
                binaryJournals.Add(journalBinary);
            }

            foreach (var storedLocation in gamFile.StoredLocations)
            {
                var storedLocationBinary = new GamStoredLocationBinary();
                storedLocationBinary.XCoordinate = storedLocation.XCoordinate;
                storedLocationBinary.YCoordinate = storedLocation.YCoordinate;
                storedLocationBinary.Area = storedLocation.Area;
                binaryStoredLocations.Add(storedLocationBinary);
            }

            foreach (var pocketPlaneLocation in gamFile.PocketPlaneLocations)
            {
                var pocketPlaneLocationBinary = new GamStoredLocationBinary();
                pocketPlaneLocationBinary.XCoordinate = pocketPlaneLocation.XCoordinate;
                pocketPlaneLocationBinary.YCoordinate = pocketPlaneLocation.YCoordinate;
                pocketPlaneLocationBinary.Area = pocketPlaneLocation.Area;
                binaryPocketPlaneLocations.Add(pocketPlaneLocationBinary);
            }

            var creOffset = 0;
            foreach (var character in gamFile.PartyMembers)
            {
                var characterBinary = new GamNpcStructBinary();
                characterBinary.Selection = character.Selection;
                characterBinary.PartyOrder = character.PartyOrder;
                characterBinary.Name = character.Name;
                characterBinary.Orientation = character.Orientation;
                characterBinary.Area = character.Area;
                characterBinary.XCoordinate = character.XCoordinate;
                characterBinary.YCoordinate = character.YCoordinate;
                characterBinary.XCoordinateView = character.XCoordinateView;
                characterBinary.YCoordinateView = character.YCoordinateView;
                characterBinary.ModalAction = character.ModalAction;
                characterBinary.Happiness = character.Happiness;
                characterBinary.NumTimesInteractedNpcCount1 = character.NumTimesInteractedNpcCount1;
                characterBinary.NumTimesInteractedNpcCount2 = character.NumTimesInteractedNpcCount2;
                characterBinary.NumTimesInteractedNpcCount3 = character.NumTimesInteractedNpcCount3;
                characterBinary.NumTimesInteractedNpcCount4 = character.NumTimesInteractedNpcCount4;
                characterBinary.NumTimesInteractedNpcCount5 = character.NumTimesInteractedNpcCount5;
                characterBinary.NumTimesInteractedNpcCount6 = character.NumTimesInteractedNpcCount6;
                characterBinary.NumTimesInteractedNpcCount7 = character.NumTimesInteractedNpcCount7;
                characterBinary.NumTimesInteractedNpcCount8 = character.NumTimesInteractedNpcCount8;
                characterBinary.NumTimesInteractedNpcCount9 = character.NumTimesInteractedNpcCount9;
                characterBinary.NumTimesInteractedNpcCount10 = character.NumTimesInteractedNpcCount10;
                characterBinary.NumTimesInteractedNpcCount11 = character.NumTimesInteractedNpcCount11;
                characterBinary.NumTimesInteractedNpcCount12 = character.NumTimesInteractedNpcCount12;
                characterBinary.NumTimesInteractedNpcCount13 = character.NumTimesInteractedNpcCount13;
                characterBinary.NumTimesInteractedNpcCount14 = character.NumTimesInteractedNpcCount14;
                characterBinary.NumTimesInteractedNpcCount15 = character.NumTimesInteractedNpcCount15;
                characterBinary.NumTimesInteractedNpcCount16 = character.NumTimesInteractedNpcCount16;
                characterBinary.NumTimesInteractedNpcCount17 = character.NumTimesInteractedNpcCount17;
                characterBinary.NumTimesInteractedNpcCount18 = character.NumTimesInteractedNpcCount18;
                characterBinary.NumTimesInteractedNpcCount19 = character.NumTimesInteractedNpcCount19;
                characterBinary.NumTimesInteractedNpcCount20 = character.NumTimesInteractedNpcCount20;
                characterBinary.NumTimesInteractedNpcCount21 = character.NumTimesInteractedNpcCount21;
                characterBinary.NumTimesInteractedNpcCount22 = character.NumTimesInteractedNpcCount22;
                characterBinary.NumTimesInteractedNpcCount23 = character.NumTimesInteractedNpcCount23;
                characterBinary.NumTimesInteractedNpcCount24 = character.NumTimesInteractedNpcCount24;
                characterBinary.QuickWeaponSlot1 = character.QuickWeaponSlot1;
                characterBinary.QuickWeaponSlot2 = character.QuickWeaponSlot2;
                characterBinary.QuickWeaponSlot3 = character.QuickWeaponSlot3;
                characterBinary.QuickWeaponSlot4 = character.QuickWeaponSlot4;
                characterBinary.QuickWeaponSlot1Ability = character.QuickWeaponSlot1Ability;
                characterBinary.QuickWeaponSlot2Ability = character.QuickWeaponSlot2Ability;
                characterBinary.QuickWeaponSlot3Ability = character.QuickWeaponSlot3Ability;
                characterBinary.QuickWeaponSlot4Ability = character.QuickWeaponSlot4Ability;
                characterBinary.QuickSpell1Resource = character.QuickSpell1Resource;
                characterBinary.QuickSpell2Resource = character.QuickSpell2Resource;
                characterBinary.QuickSpell3Resource = character.QuickSpell3Resource;
                characterBinary.QuickItemSlot1 = character.QuickItemSlot1;
                characterBinary.QuickItemSlot2 = character.QuickItemSlot2;
                characterBinary.QuickItemSlot3 = character.QuickItemSlot3;
                characterBinary.QuickItemSlot1Ability = character.QuickItemSlot1Ability;
                characterBinary.QuickItemSlot2Ability = character.QuickItemSlot2Ability;
                characterBinary.QuickItemSlot3Ability = character.QuickItemSlot3Ability;
                characterBinary.Name = character.Name;
                characterBinary.TalkCount = character.TalkCount;
                characterBinary.CharacterStats = new GamCharacterBinary();
                characterBinary.CharacterStats.MostPowerfulVanquishedName = character.CharacterStats.MostPowerfulVanquishedName;
                characterBinary.CharacterStats.MostPowerfulVanquishedXP = character.CharacterStats.MostPowerfulVanquishedXP;
                characterBinary.CharacterStats.TimeInParty = character.CharacterStats.TimeInParty;
                characterBinary.CharacterStats.TimeJoinedParty = character.CharacterStats.TimeJoinedParty;
                characterBinary.CharacterStats.PartyMember = character.CharacterStats.PartyMember;
                characterBinary.CharacterStats.Unused11 = character.CharacterStats.Unused11;
                characterBinary.CharacterStats.FirstLetterofCreResref = character.CharacterStats.FirstLetterofCreResref;
                characterBinary.CharacterStats.KillsXPGainedChapter = character.CharacterStats.KillsXPGainedChapter;
                characterBinary.CharacterStats.KillsNumberChapter = character.CharacterStats.KillsNumberChapter;
                characterBinary.CharacterStats.KillsXPGainedGame = character.CharacterStats.KillsXPGainedGame;
                characterBinary.CharacterStats.KillsNumberGame = character.CharacterStats.KillsNumberGame;
                characterBinary.CharacterStats.FavouriteSpell1 = character.CharacterStats.FavouriteSpell1;
                characterBinary.CharacterStats.FavouriteSpell2 = character.CharacterStats.FavouriteSpell2;
                characterBinary.CharacterStats.FavouriteSpell3 = character.CharacterStats.FavouriteSpell3;
                characterBinary.CharacterStats.FavouriteSpell4 = character.CharacterStats.FavouriteSpell4;
                characterBinary.CharacterStats.FavouriteSpell1Count = character.CharacterStats.FavouriteSpell1Count;
                characterBinary.CharacterStats.FavouriteSpell2Count = character.CharacterStats.FavouriteSpell2Count;
                characterBinary.CharacterStats.FavouriteSpell3Count = character.CharacterStats.FavouriteSpell3Count;
                characterBinary.CharacterStats.FavouriteSpell4Count = character.CharacterStats.FavouriteSpell4Count;
                characterBinary.CharacterStats.FavouriteWeapon1 = character.CharacterStats.FavouriteWeapon1;
                characterBinary.CharacterStats.FavouriteWeapon2 = character.CharacterStats.FavouriteWeapon2;
                characterBinary.CharacterStats.FavouriteWeapon3 = character.CharacterStats.FavouriteWeapon3;
                characterBinary.CharacterStats.FavouriteWeapon4 = character.CharacterStats.FavouriteWeapon4;
                characterBinary.CharacterStats.FavouriteWeapon1Time = character.CharacterStats.FavouriteWeapon1Time;
                characterBinary.CharacterStats.FavouriteWeapon2Time = character.CharacterStats.FavouriteWeapon2Time;
                characterBinary.CharacterStats.FavouriteWeapon3Time = character.CharacterStats.FavouriteWeapon3Time;
                characterBinary.CharacterStats.FavouriteWeapon4Time = character.CharacterStats.FavouriteWeapon4Time;
                characterBinary.VoiceSet = character.VoiceSet;

                var creBytes = CreWriter.Write(character.CreFile);
                characterBinary.SizeOfCre = creBytes.Length;
                characterBinary.OffsetToCre = HeaderSize + (gamFile.PartyMembers.Count * GamNpcStructSize) + (partyinventorycount * GamPartyInventorySize) + (gamFile.NonPartyMembers.Count * GamNpcStructSize) + (binaryVariables.Count * GamVariableSize) + (binaryJournals.Count * GamJournalEntrySize) + GamFamiliarInfoSize + (binaryStoredLocations.Count * GamStoredLocationSize) + (binaryPocketPlaneLocations.Count * GamStoredLocationSize) + creOffset;

                creOffset += creBytes.Length;
                binaryPartyMembers.Add((characterBinary, creBytes));
            }

            foreach (var character in gamFile.NonPartyMembers)
            {
                var characterBinary = new GamNpcStructBinary();
                characterBinary.Selection = character.Selection;
                characterBinary.PartyOrder = character.PartyOrder;
                characterBinary.Name = character.Name;
                characterBinary.Orientation = character.Orientation;
                characterBinary.Area = character.Area;
                characterBinary.XCoordinate = character.XCoordinate;
                characterBinary.YCoordinate = character.YCoordinate;
                characterBinary.XCoordinateView = character.XCoordinateView;
                characterBinary.YCoordinateView = character.YCoordinateView;
                characterBinary.ModalAction = character.ModalAction;
                characterBinary.Happiness = character.Happiness;
                characterBinary.NumTimesInteractedNpcCount1 = character.NumTimesInteractedNpcCount1;
                characterBinary.NumTimesInteractedNpcCount2 = character.NumTimesInteractedNpcCount2;
                characterBinary.NumTimesInteractedNpcCount3 = character.NumTimesInteractedNpcCount3;
                characterBinary.NumTimesInteractedNpcCount4 = character.NumTimesInteractedNpcCount4;
                characterBinary.NumTimesInteractedNpcCount5 = character.NumTimesInteractedNpcCount5;
                characterBinary.NumTimesInteractedNpcCount6 = character.NumTimesInteractedNpcCount6;
                characterBinary.NumTimesInteractedNpcCount7 = character.NumTimesInteractedNpcCount7;
                characterBinary.NumTimesInteractedNpcCount8 = character.NumTimesInteractedNpcCount8;
                characterBinary.NumTimesInteractedNpcCount9 = character.NumTimesInteractedNpcCount9;
                characterBinary.NumTimesInteractedNpcCount10 = character.NumTimesInteractedNpcCount10;
                characterBinary.NumTimesInteractedNpcCount11 = character.NumTimesInteractedNpcCount11;
                characterBinary.NumTimesInteractedNpcCount12 = character.NumTimesInteractedNpcCount12;
                characterBinary.NumTimesInteractedNpcCount13 = character.NumTimesInteractedNpcCount13;
                characterBinary.NumTimesInteractedNpcCount14 = character.NumTimesInteractedNpcCount14;
                characterBinary.NumTimesInteractedNpcCount15 = character.NumTimesInteractedNpcCount15;
                characterBinary.NumTimesInteractedNpcCount16 = character.NumTimesInteractedNpcCount16;
                characterBinary.NumTimesInteractedNpcCount17 = character.NumTimesInteractedNpcCount17;
                characterBinary.NumTimesInteractedNpcCount18 = character.NumTimesInteractedNpcCount18;
                characterBinary.NumTimesInteractedNpcCount19 = character.NumTimesInteractedNpcCount19;
                characterBinary.NumTimesInteractedNpcCount20 = character.NumTimesInteractedNpcCount20;
                characterBinary.NumTimesInteractedNpcCount21 = character.NumTimesInteractedNpcCount21;
                characterBinary.NumTimesInteractedNpcCount22 = character.NumTimesInteractedNpcCount22;
                characterBinary.NumTimesInteractedNpcCount23 = character.NumTimesInteractedNpcCount23;
                characterBinary.NumTimesInteractedNpcCount24 = character.NumTimesInteractedNpcCount24;
                characterBinary.QuickWeaponSlot1 = character.QuickWeaponSlot1;
                characterBinary.QuickWeaponSlot2 = character.QuickWeaponSlot2;
                characterBinary.QuickWeaponSlot3 = character.QuickWeaponSlot3;
                characterBinary.QuickWeaponSlot4 = character.QuickWeaponSlot4;
                characterBinary.QuickWeaponSlot1Ability = character.QuickWeaponSlot1Ability;
                characterBinary.QuickWeaponSlot2Ability = character.QuickWeaponSlot2Ability;
                characterBinary.QuickWeaponSlot3Ability = character.QuickWeaponSlot3Ability;
                characterBinary.QuickWeaponSlot4Ability = character.QuickWeaponSlot4Ability;
                characterBinary.QuickSpell1Resource = character.QuickSpell1Resource;
                characterBinary.QuickSpell2Resource = character.QuickSpell2Resource;
                characterBinary.QuickSpell3Resource = character.QuickSpell3Resource;
                characterBinary.QuickItemSlot1 = character.QuickItemSlot1;
                characterBinary.QuickItemSlot2 = character.QuickItemSlot2;
                characterBinary.QuickItemSlot3 = character.QuickItemSlot3;
                characterBinary.QuickItemSlot1Ability = character.QuickItemSlot1Ability;
                characterBinary.QuickItemSlot2Ability = character.QuickItemSlot2Ability;
                characterBinary.QuickItemSlot3Ability = character.QuickItemSlot3Ability;
                characterBinary.Name = character.Name;
                characterBinary.TalkCount = character.TalkCount;
                characterBinary.CharacterStats = new GamCharacterBinary();
                characterBinary.CharacterStats.MostPowerfulVanquishedName = character.CharacterStats.MostPowerfulVanquishedName;
                characterBinary.CharacterStats.MostPowerfulVanquishedXP = character.CharacterStats.MostPowerfulVanquishedXP;
                characterBinary.CharacterStats.TimeInParty = character.CharacterStats.TimeInParty;
                characterBinary.CharacterStats.TimeJoinedParty = character.CharacterStats.TimeJoinedParty;
                characterBinary.CharacterStats.PartyMember = character.CharacterStats.PartyMember;
                characterBinary.CharacterStats.Unused11 = character.CharacterStats.Unused11;
                characterBinary.CharacterStats.FirstLetterofCreResref = character.CharacterStats.FirstLetterofCreResref;
                characterBinary.CharacterStats.KillsXPGainedChapter = character.CharacterStats.KillsXPGainedChapter;
                characterBinary.CharacterStats.KillsNumberChapter = character.CharacterStats.KillsNumberChapter;
                characterBinary.CharacterStats.KillsXPGainedGame = character.CharacterStats.KillsXPGainedGame;
                characterBinary.CharacterStats.KillsNumberGame = character.CharacterStats.KillsNumberGame;
                characterBinary.CharacterStats.FavouriteSpell1 = character.CharacterStats.FavouriteSpell1;
                characterBinary.CharacterStats.FavouriteSpell2 = character.CharacterStats.FavouriteSpell2;
                characterBinary.CharacterStats.FavouriteSpell3 = character.CharacterStats.FavouriteSpell3;
                characterBinary.CharacterStats.FavouriteSpell4 = character.CharacterStats.FavouriteSpell4;
                characterBinary.CharacterStats.FavouriteSpell1Count = character.CharacterStats.FavouriteSpell1Count;
                characterBinary.CharacterStats.FavouriteSpell2Count = character.CharacterStats.FavouriteSpell2Count;
                characterBinary.CharacterStats.FavouriteSpell3Count = character.CharacterStats.FavouriteSpell3Count;
                characterBinary.CharacterStats.FavouriteSpell4Count = character.CharacterStats.FavouriteSpell4Count;
                characterBinary.CharacterStats.FavouriteWeapon1 = character.CharacterStats.FavouriteWeapon1;
                characterBinary.CharacterStats.FavouriteWeapon2 = character.CharacterStats.FavouriteWeapon2;
                characterBinary.CharacterStats.FavouriteWeapon3 = character.CharacterStats.FavouriteWeapon3;
                characterBinary.CharacterStats.FavouriteWeapon4 = character.CharacterStats.FavouriteWeapon4;
                characterBinary.CharacterStats.FavouriteWeapon1Time = character.CharacterStats.FavouriteWeapon1Time;
                characterBinary.CharacterStats.FavouriteWeapon2Time = character.CharacterStats.FavouriteWeapon2Time;
                characterBinary.CharacterStats.FavouriteWeapon3Time = character.CharacterStats.FavouriteWeapon3Time;
                characterBinary.CharacterStats.FavouriteWeapon4Time = character.CharacterStats.FavouriteWeapon4Time;
                characterBinary.VoiceSet = character.VoiceSet;

                var creBytes = CreWriter.Write(character.CreFile);
                characterBinary.SizeOfCre = creBytes.Length;
                characterBinary.OffsetToCre = HeaderSize + (gamFile.PartyMembers.Count * GamNpcStructSize) + (partyinventorycount * GamPartyInventorySize) + (gamFile.NonPartyMembers.Count * GamNpcStructSize) + (binaryVariables.Count * GamVariableSize) + (binaryJournals.Count * GamJournalEntrySize) + GamFamiliarInfoSize + (binaryStoredLocations.Count * GamStoredLocationSize) + (binaryPocketPlaneLocations.Count * GamStoredLocationSize) + creOffset;
                creOffset += creBytes.Length;
                binaryNonPartyMembers.Add((characterBinary, creBytes));
            }

            var headerBinary = new GamBinaryHeader();
            headerBinary.ftype = new array4() { character1 = 'G', character2 = 'A', character3 = 'M', character4 = 'E' };
            headerBinary.fversion = new array4() { character1 = 'V', character2 = '2', character3 = '.', character4 = '0' };
            headerBinary.GameTime = gamFile.GameTime;
            headerBinary.SelectedFormation = gamFile.SelectedFormation;
            headerBinary.FormationButton1 = gamFile.FormationButton1;
            headerBinary.FormationButton2 = gamFile.FormationButton2;
            headerBinary.FormationButton3 = gamFile.FormationButton3;
            headerBinary.FormationButton4 = gamFile.FormationButton4;
            headerBinary.FormationButton5 = gamFile.FormationButton5;
            headerBinary.Gold = gamFile.Gold;
            headerBinary.UseActiveAreaFromPartyMember = gamFile.UseActiveAreaFromPartyMember;
            headerBinary.Weather = gamFile.Weather;
            headerBinary.NpcStructPartyOffset = HeaderSize;
            headerBinary.NpcStructPartyCount = binaryPartyMembers.Count;
            headerBinary.PartyInventoryOffset = 0;// HeaderSize + (binaryPartyMembers.Count * GamNpcStructSize);
            headerBinary.PartyInventoryCount = 0; //TODO: We need a binary struct, a gamFile class, to update the reader and to update the writer (we write 20 blank bytes at the moment)
            headerBinary.NpcStructNonPartyOffset = HeaderSize + (binaryPartyMembers.Count * GamNpcStructSize) + (headerBinary.PartyInventoryCount * GamPartyInventorySize);
            headerBinary.NpcStructNonPartyCount = binaryNonPartyMembers.Count;
            headerBinary.GlobalVariableOffset = HeaderSize + (binaryPartyMembers.Count * GamNpcStructSize) + (headerBinary.PartyInventoryCount * GamPartyInventorySize) + (binaryNonPartyMembers.Count * GamNpcStructSize);
            headerBinary.GlobalVariableCount = binaryVariables.Count;
            headerBinary.MainArea = gamFile.MainArea;
            headerBinary.CurrentLink = gamFile.CurrentLink;
            headerBinary.JournalEntryCount = binaryJournals.Count;
            headerBinary.JournalEntryOffset = HeaderSize + (binaryPartyMembers.Count * GamNpcStructSize) + (headerBinary.PartyInventoryCount * GamPartyInventorySize) + (binaryNonPartyMembers.Count * GamNpcStructSize) + (binaryVariables.Count * GamVariableSize);
            headerBinary.PartyReputation = gamFile.PartyReputation;
            headerBinary.CurrentArea = gamFile.CurrentArea;
            headerBinary.GuiFlags = gamFile.GuiFlags;
            headerBinary.LoadingProgress = gamFile.LoadingProgress;
            headerBinary.FamiliarInfoOffset = HeaderSize + (binaryPartyMembers.Count * GamNpcStructSize) + (headerBinary.PartyInventoryCount * GamPartyInventorySize) + (binaryNonPartyMembers.Count * GamNpcStructSize) + (binaryVariables.Count * GamVariableSize) + (binaryJournals.Count * GamJournalEntrySize);
            headerBinary.StoredLocationOffset = HeaderSize + (binaryPartyMembers.Count * GamNpcStructSize) + (headerBinary.PartyInventoryCount * GamPartyInventorySize) + (binaryNonPartyMembers.Count * GamNpcStructSize) + (binaryVariables.Count * GamVariableSize) + (binaryJournals.Count * GamJournalEntrySize) + GamFamiliarInfoSize;
            headerBinary.GameTimeReal = gamFile.GameTimeReal;
            headerBinary.PocketPlaneLocationOffset = HeaderSize + (binaryPartyMembers.Count * GamNpcStructSize) + (headerBinary.PartyInventoryCount * GamPartyInventorySize) + (binaryNonPartyMembers.Count * GamNpcStructSize) + (binaryVariables.Count * GamVariableSize) + (binaryJournals.Count * GamJournalEntrySize) + GamFamiliarInfoSize + (binaryStoredLocations.Count * GamStoredLocationSize);
            headerBinary.PocketPlaneLocationCount = binaryPocketPlaneLocations.Count;
            headerBinary.ZoomPercentage = gamFile.ZoomPercentage;
            headerBinary.RandomEncounterArea = gamFile.RandomEncounterArea;
            headerBinary.CurrentWorldmap = gamFile.CurrentWorldmap;
            headerBinary.CurrentCampaign = gamFile.CurrentCampaign;
            headerBinary.FamiliarOwner = gamFile.FamiliarOwner;
            headerBinary.RandomEncountryEntry = gamFile.RandomEncountryEntry;

            using var s = new MemoryStream();
            using var bw = new BinaryWriter(s);
            var headerAsBytes = Common.WriteStruct(headerBinary);
            bw.Write(headerAsBytes);

            foreach (var character in binaryPartyMembers)
            {
                var characterAsBytes = Common.WriteStruct(character.data);
                bw.Write(characterAsBytes);
            }

            for (int i = 0; i < headerBinary.PartyInventoryCount; i++)
            {
                bw.Write(new byte[20]);
            }

            foreach (var character in binaryNonPartyMembers)
            {
                var characterAsBytes = Common.WriteStruct(character.data);
                bw.Write(characterAsBytes);
            }

            foreach (var variable in binaryVariables)
            {
                var variableAsBytes = Common.WriteStruct(variable);
                bw.Write(variableAsBytes);
            }

            foreach (var journal in binaryJournals)
            {
                var journalAsBytes = Common.WriteStruct(journal);
                bw.Write(journalAsBytes);
            }

            bw.Write(Common.WriteStruct(gamFile.FamiliarInfo.LawfulGoodFamiliar));
            bw.Write(Common.WriteStruct(gamFile.FamiliarInfo.LawfulNeutralFamiliar));
            bw.Write(Common.WriteStruct(gamFile.FamiliarInfo.LawfulEvilFamiliar));
            bw.Write(Common.WriteStruct(gamFile.FamiliarInfo.NeutralGoodFamiliar));
            bw.Write(Common.WriteStruct(gamFile.FamiliarInfo.NeutralFamiliar));
            bw.Write(Common.WriteStruct(gamFile.FamiliarInfo.NeutralEvilFamiliar));
            bw.Write(Common.WriteStruct(gamFile.FamiliarInfo.ChaoticGoodFamiliar));
            bw.Write(Common.WriteStruct(gamFile.FamiliarInfo.ChaoticNeutralFamiliar));
            bw.Write(Common.WriteStruct(gamFile.FamiliarInfo.ChaoticEvilFamiliar));
            var offset = HeaderSize + (binaryPartyMembers.Count * GamNpcStructSize) + (binaryNonPartyMembers.Count * GamNpcStructSize) + (binaryVariables.Count * GamVariableSize) + (binaryJournals.Count * GamJournalEntrySize) + GamFamiliarInfoSize + (binaryStoredLocations.Count * GamStoredLocationSize) + (binaryPocketPlaneLocations.Count * GamPocketLocationSize);
            bw.Write(offset);
            bw.Write(gamFile.FamiliarInfo.LGLevel1FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LGLevel2FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LGLevel3FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LGLevel4FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LGLevel5FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LGLevel6FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LGLevel7FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LGLevel8FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LGLevel9FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LNLevel1FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LNLevel2FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LNLevel3FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LNLevel4FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LNLevel5FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LNLevel6FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LNLevel7FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LNLevel8FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LNLevel9FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CGLevel1FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CGLevel2FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CGLevel3FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CGLevel4FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CGLevel5FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CGLevel6FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CGLevel7FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CGLevel8FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CGLevel9FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NGLevel1FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NGLevel2FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NGLevel3FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NGLevel4FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NGLevel5FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NGLevel6FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NGLevel7FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NGLevel8FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NGLevel9FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.TNLevel1FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.TNLevel2FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.TNLevel3FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.TNLevel4FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.TNLevel5FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.TNLevel6FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.TNLevel7FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.TNLevel8FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.TNLevel9FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NELevel1FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NELevel2FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NELevel3FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NELevel4FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NELevel5FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NELevel6FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NELevel7FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NELevel8FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.NELevel9FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LELevel1FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LELevel2FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LELevel3FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LELevel4FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LELevel5FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LELevel6FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LELevel7FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LELevel8FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.LELevel9FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CNLevel1FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CNLevel2FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CNLevel3FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CNLevel4FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CNLevel5FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CNLevel6FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CNLevel7FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CNLevel8FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CNLevel9FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CELevel1FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CELevel2FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CELevel3FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CELevel4FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CELevel5FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CELevel6FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CELevel7FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CELevel8FamiliarCount);
            bw.Write(gamFile.FamiliarInfo.CELevel9FamiliarCount);

            foreach (var storedLocation in binaryStoredLocations)
            {
                var storedLocationAsBytes = Common.WriteStruct(storedLocation);
                bw.Write(storedLocationAsBytes);
            }

            foreach (var pocketPlaneLocation in binaryPocketPlaneLocations)
            {
                var pocketPlaneLocationAsBytes = Common.WriteStruct(pocketPlaneLocation);
                bw.Write(pocketPlaneLocationAsBytes);
            }

            var familiars = new List<IEnumerable<array8>> {
                    gamFile.LGLevel1Familiar, gamFile.LGLevel2Familiar, gamFile.LGLevel3Familiar, gamFile.LGLevel4Familiar, gamFile.LGLevel5Familiar, gamFile.LGLevel6Familiar, gamFile.LGLevel7Familiar, gamFile.LGLevel8Familiar, gamFile.LGLevel9Familiar,
                    gamFile.LNLevel1Familiar, gamFile.LNLevel2Familiar, gamFile.LNLevel3Familiar, gamFile.LNLevel4Familiar, gamFile.LNLevel5Familiar, gamFile.LNLevel6Familiar, gamFile.LNLevel7Familiar, gamFile.LNLevel8Familiar, gamFile.LNLevel9Familiar,
                    gamFile.CGLevel1Familiar, gamFile.CGLevel2Familiar, gamFile.CGLevel3Familiar, gamFile.CGLevel4Familiar, gamFile.CGLevel5Familiar, gamFile.CGLevel6Familiar, gamFile.CGLevel7Familiar, gamFile.CGLevel8Familiar, gamFile.CGLevel9Familiar,
                    gamFile.NGLevel1Familiar, gamFile.NGLevel2Familiar, gamFile.NGLevel3Familiar, gamFile.NGLevel4Familiar, gamFile.NGLevel5Familiar, gamFile.NGLevel6Familiar, gamFile.NGLevel7Familiar, gamFile.NGLevel8Familiar, gamFile.NGLevel9Familiar,
                    gamFile.TNLevel1Familiar, gamFile.TNLevel2Familiar, gamFile.TNLevel3Familiar, gamFile.TNLevel4Familiar, gamFile.TNLevel5Familiar, gamFile.TNLevel6Familiar, gamFile.TNLevel7Familiar, gamFile.TNLevel8Familiar, gamFile.TNLevel9Familiar,
                    gamFile.NELevel1Familiar, gamFile.NELevel2Familiar, gamFile.NELevel3Familiar, gamFile.NELevel4Familiar, gamFile.NELevel5Familiar, gamFile.NELevel6Familiar, gamFile.NELevel7Familiar, gamFile.NELevel8Familiar, gamFile.NELevel9Familiar,
                    gamFile.LELevel1Familiar, gamFile.LELevel2Familiar, gamFile.LELevel3Familiar, gamFile.LELevel4Familiar, gamFile.LELevel5Familiar, gamFile.LELevel6Familiar, gamFile.LELevel7Familiar, gamFile.LELevel8Familiar, gamFile.LELevel9Familiar,
                    gamFile.CNLevel1Familiar, gamFile.CNLevel2Familiar, gamFile.CNLevel3Familiar, gamFile.CNLevel4Familiar, gamFile.CNLevel5Familiar, gamFile.CNLevel6Familiar, gamFile.CNLevel7Familiar, gamFile.CNLevel8Familiar, gamFile.CNLevel9Familiar,
                    gamFile.CELevel1Familiar, gamFile.CELevel2Familiar, gamFile.CELevel3Familiar, gamFile.CELevel4Familiar, gamFile.CELevel5Familiar, gamFile.CELevel6Familiar, gamFile.CELevel7Familiar, gamFile.CELevel8Familiar, gamFile.CELevel9Familiar,
                };

            foreach (var familiarList in familiars)
            {
                foreach (var familiar in familiarList)
                {
                    bw.Write(Common.WriteStruct(familiar));
                }
            }

            foreach (var character in binaryPartyMembers)
            {
                if (character.cre != null && character.cre.Length > 0)
                {
                    bw.Write(character.cre);
                }
            }

            foreach (var character in binaryNonPartyMembers)
            {
                if (character.cre != null && character.cre.Length > 0)
                {
                    bw.Write(character.cre);
                }
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