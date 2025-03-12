using ii.InfinityEngine.Binary;
using ii.InfinityEngine.Files;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ii.InfinityEngine.Readers
{
    public class GamFileBinaryReader : IGamFileReader
    {
        public TlkFile TlkFile { get; set; }
        private CreFileBinaryReader CreReader = new CreFileBinaryReader();

        public GamFile Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var f = Read(fs);
            f.Filename = Path.GetFileName(filename);
            return f;
        }

        public GamFile Read(Stream s)
        {
            using var br = new BinaryReader(s);
            var gamFile = ParseFile(br);
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            gamFile.OriginalFile = ParseFile(br);
            return gamFile;
        }

        private GamFile ParseFile(BinaryReader br)
        {
            var header = (GamBinaryHeader)Common.ReadStruct(br, typeof(GamBinaryHeader));

            if (header.ftype.ToString() != "GAME" || header.fversion.ToString() != "V2.0")
                return new GamFile();

            var gamFile = new GamFile();
            var npcStructPartyMembers = new List<(GamNpcStructBinary data, CreFile cre)>();
            var npcStructNonParty = new List<(GamNpcStructBinary data, CreFile cre)>();
            var variables = new List<GamVariableBinary>();
            var journals = new List<GamJournalBinary>();
            var storedLocations = new List<GamStoredLocationBinary>();
            var pocketPlaneLocations = new List<GamStoredLocationBinary>();

            br.BaseStream.Seek(header.NpcStructPartyOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.NpcStructPartyCount; i++)
            {
                var npcStruct = (GamNpcStructBinary)Common.ReadStruct(br, typeof(GamNpcStructBinary));
                npcStructPartyMembers.Add((npcStruct, null));
            }

            for (int i = 0; i < npcStructPartyMembers.Count; i++)
            {
                var npc = npcStructPartyMembers.ElementAt(i);
                br.BaseStream.Seek(npc.data.OffsetToCre, SeekOrigin.Begin);
                var npcStruct = br.ReadBytes(npc.data.SizeOfCre);
                var ms = new MemoryStream(npcStruct);
                var cre = CreReader.Read(ms);
                npc.cre = cre;
                npcStructPartyMembers[i] = npc;
            }

            br.BaseStream.Seek(header.NpcStructNonPartyOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.NpcStructNonPartyCount; i++)
            {
                var npcStruct = (GamNpcStructBinary)Common.ReadStruct(br, typeof(GamNpcStructBinary));
                npcStructNonParty.Add((npcStruct, null));
            }

            for (int i = 0; i < npcStructNonParty.Count; i++)
            {
                var npc = npcStructNonParty.ElementAt(i);
                br.BaseStream.Seek(npc.data.OffsetToCre, SeekOrigin.Begin);
                var npcStruct = br.ReadBytes(npc.data.SizeOfCre);
                var ms = new MemoryStream(npcStruct);
                var cre = CreReader.Read(ms);
                npc.cre = cre;
                npcStructNonParty[i] = npc;
            }

            //Note: We ignore 'Party Inventory' as there's no definition of the structure - it's likely not used

            br.BaseStream.Seek(header.GlobalVariableOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.GlobalVariableCount; i++)
            {
                var variable = (GamVariableBinary)Common.ReadStruct(br, typeof(GamVariableBinary));
                variables.Add(variable);
            }

            br.BaseStream.Seek(header.FamiliarInfoOffset, SeekOrigin.Begin);
            var familiarInfo = (GamFamiliarInfoBinary)Common.ReadStruct(br, typeof(GamFamiliarInfoBinary));

            // This also seems unused
            if (familiarInfo.OffsetToFamiliarResources < br.BaseStream.Length)
            {
                br.BaseStream.Seek(familiarInfo.OffsetToFamiliarResources, SeekOrigin.Begin);
                ReadFamiliarResRefs(br, familiarInfo.LGLevel1FamiliarCount, gamFile.LGLevel1Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LGLevel2FamiliarCount, gamFile.LGLevel2Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LGLevel3FamiliarCount, gamFile.LGLevel3Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LGLevel4FamiliarCount, gamFile.LGLevel4Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LGLevel5FamiliarCount, gamFile.LGLevel5Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LGLevel6FamiliarCount, gamFile.LGLevel6Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LGLevel7FamiliarCount, gamFile.LGLevel7Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LGLevel8FamiliarCount, gamFile.LGLevel8Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LGLevel9FamiliarCount, gamFile.LGLevel9Familiar);

                ReadFamiliarResRefs(br, familiarInfo.LNLevel1FamiliarCount, gamFile.LNLevel1Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LNLevel2FamiliarCount, gamFile.LNLevel2Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LNLevel3FamiliarCount, gamFile.LNLevel3Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LNLevel4FamiliarCount, gamFile.LNLevel4Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LNLevel5FamiliarCount, gamFile.LNLevel5Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LNLevel6FamiliarCount, gamFile.LNLevel6Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LNLevel7FamiliarCount, gamFile.LNLevel7Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LNLevel8FamiliarCount, gamFile.LNLevel8Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LNLevel9FamiliarCount, gamFile.LNLevel9Familiar);

                ReadFamiliarResRefs(br, familiarInfo.CGLevel1FamiliarCount, gamFile.CGLevel1Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CGLevel2FamiliarCount, gamFile.CGLevel2Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CGLevel3FamiliarCount, gamFile.CGLevel3Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CGLevel4FamiliarCount, gamFile.CGLevel4Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CGLevel5FamiliarCount, gamFile.CGLevel5Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CGLevel6FamiliarCount, gamFile.CGLevel6Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CGLevel7FamiliarCount, gamFile.CGLevel7Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CGLevel8FamiliarCount, gamFile.CGLevel8Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CGLevel9FamiliarCount, gamFile.CGLevel9Familiar);

                ReadFamiliarResRefs(br, familiarInfo.NGLevel1FamiliarCount, gamFile.NGLevel1Familiar);
                ReadFamiliarResRefs(br, familiarInfo.NGLevel2FamiliarCount, gamFile.NGLevel2Familiar);
                ReadFamiliarResRefs(br, familiarInfo.NGLevel3FamiliarCount, gamFile.NGLevel3Familiar);
                ReadFamiliarResRefs(br, familiarInfo.NGLevel4FamiliarCount, gamFile.NGLevel4Familiar);
                ReadFamiliarResRefs(br, familiarInfo.NGLevel5FamiliarCount, gamFile.NGLevel5Familiar);
                ReadFamiliarResRefs(br, familiarInfo.NGLevel6FamiliarCount, gamFile.NGLevel6Familiar);
                ReadFamiliarResRefs(br, familiarInfo.NGLevel7FamiliarCount, gamFile.NGLevel7Familiar);
                ReadFamiliarResRefs(br, familiarInfo.NGLevel8FamiliarCount, gamFile.NGLevel8Familiar);
                ReadFamiliarResRefs(br, familiarInfo.NGLevel9FamiliarCount, gamFile.NGLevel9Familiar);

                ReadFamiliarResRefs(br, familiarInfo.TNLevel1FamiliarCount, gamFile.TNLevel1Familiar);
                ReadFamiliarResRefs(br, familiarInfo.TNLevel2FamiliarCount, gamFile.TNLevel2Familiar);
                ReadFamiliarResRefs(br, familiarInfo.TNLevel3FamiliarCount, gamFile.TNLevel3Familiar);
                ReadFamiliarResRefs(br, familiarInfo.TNLevel4FamiliarCount, gamFile.TNLevel4Familiar);
                ReadFamiliarResRefs(br, familiarInfo.TNLevel5FamiliarCount, gamFile.TNLevel5Familiar);
                ReadFamiliarResRefs(br, familiarInfo.TNLevel6FamiliarCount, gamFile.TNLevel6Familiar);
                ReadFamiliarResRefs(br, familiarInfo.TNLevel7FamiliarCount, gamFile.TNLevel7Familiar);
                ReadFamiliarResRefs(br, familiarInfo.TNLevel8FamiliarCount, gamFile.TNLevel8Familiar);
                ReadFamiliarResRefs(br, familiarInfo.TNLevel9FamiliarCount, gamFile.TNLevel9Familiar);

                ReadFamiliarResRefs(br, familiarInfo.NELevel1FamiliarCount, gamFile.NELevel1Familiar);
                ReadFamiliarResRefs(br, familiarInfo.NELevel2FamiliarCount, gamFile.NELevel2Familiar);
                ReadFamiliarResRefs(br, familiarInfo.NELevel3FamiliarCount, gamFile.NELevel3Familiar);
                ReadFamiliarResRefs(br, familiarInfo.NELevel4FamiliarCount, gamFile.NELevel4Familiar);
                ReadFamiliarResRefs(br, familiarInfo.NELevel5FamiliarCount, gamFile.NELevel5Familiar);
                ReadFamiliarResRefs(br, familiarInfo.NELevel6FamiliarCount, gamFile.NELevel6Familiar);
                ReadFamiliarResRefs(br, familiarInfo.NELevel7FamiliarCount, gamFile.NELevel7Familiar);
                ReadFamiliarResRefs(br, familiarInfo.NELevel8FamiliarCount, gamFile.NELevel8Familiar);
                ReadFamiliarResRefs(br, familiarInfo.NELevel9FamiliarCount, gamFile.NELevel9Familiar);

                ReadFamiliarResRefs(br, familiarInfo.LELevel1FamiliarCount, gamFile.LELevel1Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LELevel2FamiliarCount, gamFile.LELevel2Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LELevel3FamiliarCount, gamFile.LELevel3Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LELevel4FamiliarCount, gamFile.LELevel4Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LELevel5FamiliarCount, gamFile.LELevel5Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LELevel6FamiliarCount, gamFile.LELevel6Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LELevel7FamiliarCount, gamFile.LELevel7Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LELevel8FamiliarCount, gamFile.LELevel8Familiar);
                ReadFamiliarResRefs(br, familiarInfo.LELevel9FamiliarCount, gamFile.LELevel9Familiar);

                ReadFamiliarResRefs(br, familiarInfo.CNLevel1FamiliarCount, gamFile.CNLevel1Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CNLevel2FamiliarCount, gamFile.CNLevel2Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CNLevel3FamiliarCount, gamFile.CNLevel3Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CNLevel4FamiliarCount, gamFile.CNLevel4Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CNLevel5FamiliarCount, gamFile.CNLevel5Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CNLevel6FamiliarCount, gamFile.CNLevel6Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CNLevel7FamiliarCount, gamFile.CNLevel7Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CNLevel8FamiliarCount, gamFile.CNLevel8Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CNLevel9FamiliarCount, gamFile.CNLevel9Familiar);

                ReadFamiliarResRefs(br, familiarInfo.CELevel1FamiliarCount, gamFile.CELevel1Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CELevel2FamiliarCount, gamFile.CELevel2Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CELevel3FamiliarCount, gamFile.CELevel3Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CELevel4FamiliarCount, gamFile.CELevel4Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CELevel5FamiliarCount, gamFile.CELevel5Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CELevel6FamiliarCount, gamFile.CELevel6Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CELevel7FamiliarCount, gamFile.CELevel7Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CELevel8FamiliarCount, gamFile.CELevel8Familiar);
                ReadFamiliarResRefs(br, familiarInfo.CELevel9FamiliarCount, gamFile.CELevel9Familiar);
            }

            br.BaseStream.Seek(header.JournalEntryOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.JournalEntryCount; i++)
            {
                var journal = (GamJournalBinary)Common.ReadStruct(br, typeof(GamJournalBinary));
                journals.Add(journal);
            }

            //Looks like we never need/use header.FamiliarExtraOffset ?

            br.BaseStream.Seek(header.StoredLocationOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.StoredLocationCount; i++)
            {
                var storedLocation = (GamStoredLocationBinary)Common.ReadStruct(br, typeof(GamStoredLocationBinary));
                storedLocations.Add(storedLocation);
            }

            br.BaseStream.Seek(header.PocketPlaneLocationOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.PocketPlaneLocationCount; i++)
            {
                var storedLocation = (GamStoredLocationBinary)Common.ReadStruct(br, typeof(GamStoredLocationBinary));
                pocketPlaneLocations.Add(storedLocation);
            }

            gamFile.GameTime = header.GameTime;
            gamFile.SelectedFormation = header.SelectedFormation;
            gamFile.FormationButton1 = header.FormationButton1;
            gamFile.FormationButton2 = header.FormationButton2;
            gamFile.FormationButton3 = header.FormationButton3;
            gamFile.FormationButton4 = header.FormationButton4;
            gamFile.FormationButton5 = header.FormationButton5;
            gamFile.Gold = header.Gold;
            gamFile.UseActiveAreaFromPartyMember = header.UseActiveAreaFromPartyMember;
            gamFile.Weather = header.Weather;
            gamFile.MainArea = header.MainArea;
            gamFile.PartyReputation = header.PartyReputation;
            gamFile.CurrentArea = header.CurrentArea;
            gamFile.GuiFlags = header.GuiFlags;
            gamFile.LoadingProgress = header.LoadingProgress;
            gamFile.GameTimeReal = header.GameTimeReal;
            gamFile.ZoomPercentage = header.ZoomPercentage;
            gamFile.RandomEncounterArea = header.RandomEncounterArea;
            gamFile.CurrentWorldmap = header.CurrentWorldmap;
            gamFile.CurrentCampaign = header.CurrentCampaign;
            gamFile.FamiliarOwner = header.FamiliarOwner;
            gamFile.RandomEncountryEntry = header.RandomEncountryEntry;

            gamFile.FamiliarInfo = new GamFamiliarInfo();
            gamFile.FamiliarInfo.LawfulGoodFamiliar = familiarInfo.LawfulGoodFamiliar;
            gamFile.FamiliarInfo.LawfulNeutralFamiliar = familiarInfo.LawfulNeutralFamiliar;
            gamFile.FamiliarInfo.LawfulEvilFamiliar = familiarInfo.LawfulEvilFamiliar;
            gamFile.FamiliarInfo.NeutralGoodFamiliar = familiarInfo.NeutralGoodFamiliar;
            gamFile.FamiliarInfo.NeutralFamiliar = familiarInfo.NeutralFamiliar;
            gamFile.FamiliarInfo.NeutralEvilFamiliar = familiarInfo.NeutralEvilFamiliar;
            gamFile.FamiliarInfo.ChaoticGoodFamiliar = familiarInfo.ChaoticGoodFamiliar;
            gamFile.FamiliarInfo.ChaoticNeutralFamiliar = familiarInfo.ChaoticNeutralFamiliar;
            gamFile.FamiliarInfo.ChaoticEvilFamiliar = familiarInfo.ChaoticEvilFamiliar;
            gamFile.FamiliarInfo.LGLevel1FamiliarCount = familiarInfo.LGLevel1FamiliarCount;
            gamFile.FamiliarInfo.LGLevel2FamiliarCount = familiarInfo.LGLevel2FamiliarCount;
            gamFile.FamiliarInfo.LGLevel3FamiliarCount = familiarInfo.LGLevel3FamiliarCount;
            gamFile.FamiliarInfo.LGLevel4FamiliarCount = familiarInfo.LGLevel4FamiliarCount;
            gamFile.FamiliarInfo.LGLevel5FamiliarCount = familiarInfo.LGLevel5FamiliarCount;
            gamFile.FamiliarInfo.LGLevel6FamiliarCount = familiarInfo.LGLevel6FamiliarCount;
            gamFile.FamiliarInfo.LGLevel7FamiliarCount = familiarInfo.LGLevel7FamiliarCount;
            gamFile.FamiliarInfo.LGLevel8FamiliarCount = familiarInfo.LGLevel8FamiliarCount;
            gamFile.FamiliarInfo.LGLevel9FamiliarCount = familiarInfo.LGLevel9FamiliarCount;
            gamFile.FamiliarInfo.LNLevel1FamiliarCount = familiarInfo.LNLevel1FamiliarCount;
            gamFile.FamiliarInfo.LNLevel2FamiliarCount = familiarInfo.LNLevel2FamiliarCount;
            gamFile.FamiliarInfo.LNLevel3FamiliarCount = familiarInfo.LNLevel3FamiliarCount;
            gamFile.FamiliarInfo.LNLevel4FamiliarCount = familiarInfo.LNLevel4FamiliarCount;
            gamFile.FamiliarInfo.LNLevel5FamiliarCount = familiarInfo.LNLevel5FamiliarCount;
            gamFile.FamiliarInfo.LNLevel6FamiliarCount = familiarInfo.LNLevel6FamiliarCount;
            gamFile.FamiliarInfo.LNLevel7FamiliarCount = familiarInfo.LNLevel7FamiliarCount;
            gamFile.FamiliarInfo.LNLevel8FamiliarCount = familiarInfo.LNLevel8FamiliarCount;
            gamFile.FamiliarInfo.LNLevel9FamiliarCount = familiarInfo.LNLevel9FamiliarCount;
            gamFile.FamiliarInfo.CGLevel1FamiliarCount = familiarInfo.CGLevel1FamiliarCount;
            gamFile.FamiliarInfo.CGLevel2FamiliarCount = familiarInfo.CGLevel2FamiliarCount;
            gamFile.FamiliarInfo.CGLevel3FamiliarCount = familiarInfo.CGLevel3FamiliarCount;
            gamFile.FamiliarInfo.CGLevel4FamiliarCount = familiarInfo.CGLevel4FamiliarCount;
            gamFile.FamiliarInfo.CGLevel5FamiliarCount = familiarInfo.CGLevel5FamiliarCount;
            gamFile.FamiliarInfo.CGLevel6FamiliarCount = familiarInfo.CGLevel6FamiliarCount;
            gamFile.FamiliarInfo.CGLevel7FamiliarCount = familiarInfo.CGLevel7FamiliarCount;
            gamFile.FamiliarInfo.CGLevel8FamiliarCount = familiarInfo.CGLevel8FamiliarCount;
            gamFile.FamiliarInfo.CGLevel9FamiliarCount = familiarInfo.CGLevel9FamiliarCount;
            gamFile.FamiliarInfo.NGLevel1FamiliarCount = familiarInfo.NGLevel1FamiliarCount;
            gamFile.FamiliarInfo.NGLevel2FamiliarCount = familiarInfo.NGLevel2FamiliarCount;
            gamFile.FamiliarInfo.NGLevel3FamiliarCount = familiarInfo.NGLevel3FamiliarCount;
            gamFile.FamiliarInfo.NGLevel4FamiliarCount = familiarInfo.NGLevel4FamiliarCount;
            gamFile.FamiliarInfo.NGLevel5FamiliarCount = familiarInfo.NGLevel5FamiliarCount;
            gamFile.FamiliarInfo.NGLevel6FamiliarCount = familiarInfo.NGLevel6FamiliarCount;
            gamFile.FamiliarInfo.NGLevel7FamiliarCount = familiarInfo.NGLevel7FamiliarCount;
            gamFile.FamiliarInfo.NGLevel8FamiliarCount = familiarInfo.NGLevel8FamiliarCount;
            gamFile.FamiliarInfo.NGLevel9FamiliarCount = familiarInfo.NGLevel9FamiliarCount;
            gamFile.FamiliarInfo.TNLevel1FamiliarCount = familiarInfo.TNLevel1FamiliarCount;
            gamFile.FamiliarInfo.TNLevel2FamiliarCount = familiarInfo.TNLevel2FamiliarCount;
            gamFile.FamiliarInfo.TNLevel3FamiliarCount = familiarInfo.TNLevel3FamiliarCount;
            gamFile.FamiliarInfo.TNLevel4FamiliarCount = familiarInfo.TNLevel4FamiliarCount;
            gamFile.FamiliarInfo.TNLevel5FamiliarCount = familiarInfo.TNLevel5FamiliarCount;
            gamFile.FamiliarInfo.TNLevel6FamiliarCount = familiarInfo.TNLevel6FamiliarCount;
            gamFile.FamiliarInfo.TNLevel7FamiliarCount = familiarInfo.TNLevel7FamiliarCount;
            gamFile.FamiliarInfo.TNLevel8FamiliarCount = familiarInfo.TNLevel8FamiliarCount;
            gamFile.FamiliarInfo.TNLevel9FamiliarCount = familiarInfo.TNLevel9FamiliarCount;
            gamFile.FamiliarInfo.NELevel1FamiliarCount = familiarInfo.NELevel1FamiliarCount;
            gamFile.FamiliarInfo.NELevel2FamiliarCount = familiarInfo.NELevel2FamiliarCount;
            gamFile.FamiliarInfo.NELevel3FamiliarCount = familiarInfo.NELevel3FamiliarCount;
            gamFile.FamiliarInfo.NELevel4FamiliarCount = familiarInfo.NELevel4FamiliarCount;
            gamFile.FamiliarInfo.NELevel5FamiliarCount = familiarInfo.NELevel5FamiliarCount;
            gamFile.FamiliarInfo.NELevel6FamiliarCount = familiarInfo.NELevel6FamiliarCount;
            gamFile.FamiliarInfo.NELevel7FamiliarCount = familiarInfo.NELevel7FamiliarCount;
            gamFile.FamiliarInfo.NELevel8FamiliarCount = familiarInfo.NELevel8FamiliarCount;
            gamFile.FamiliarInfo.NELevel9FamiliarCount = familiarInfo.NELevel9FamiliarCount;
            gamFile.FamiliarInfo.LELevel1FamiliarCount = familiarInfo.LELevel1FamiliarCount;
            gamFile.FamiliarInfo.LELevel2FamiliarCount = familiarInfo.LELevel2FamiliarCount;
            gamFile.FamiliarInfo.LELevel3FamiliarCount = familiarInfo.LELevel3FamiliarCount;
            gamFile.FamiliarInfo.LELevel4FamiliarCount = familiarInfo.LELevel4FamiliarCount;
            gamFile.FamiliarInfo.LELevel5FamiliarCount = familiarInfo.LELevel5FamiliarCount;
            gamFile.FamiliarInfo.LELevel6FamiliarCount = familiarInfo.LELevel6FamiliarCount;
            gamFile.FamiliarInfo.LELevel7FamiliarCount = familiarInfo.LELevel7FamiliarCount;
            gamFile.FamiliarInfo.LELevel8FamiliarCount = familiarInfo.LELevel8FamiliarCount;
            gamFile.FamiliarInfo.LELevel9FamiliarCount = familiarInfo.LELevel9FamiliarCount;
            gamFile.FamiliarInfo.CNLevel1FamiliarCount = familiarInfo.CNLevel1FamiliarCount;
            gamFile.FamiliarInfo.CNLevel2FamiliarCount = familiarInfo.CNLevel2FamiliarCount;
            gamFile.FamiliarInfo.CNLevel3FamiliarCount = familiarInfo.CNLevel3FamiliarCount;
            gamFile.FamiliarInfo.CNLevel4FamiliarCount = familiarInfo.CNLevel4FamiliarCount;
            gamFile.FamiliarInfo.CNLevel5FamiliarCount = familiarInfo.CNLevel5FamiliarCount;
            gamFile.FamiliarInfo.CNLevel6FamiliarCount = familiarInfo.CNLevel6FamiliarCount;
            gamFile.FamiliarInfo.CNLevel7FamiliarCount = familiarInfo.CNLevel7FamiliarCount;
            gamFile.FamiliarInfo.CNLevel8FamiliarCount = familiarInfo.CNLevel8FamiliarCount;
            gamFile.FamiliarInfo.CNLevel9FamiliarCount = familiarInfo.CNLevel9FamiliarCount;
            gamFile.FamiliarInfo.CELevel1FamiliarCount = familiarInfo.CELevel1FamiliarCount;
            gamFile.FamiliarInfo.CELevel2FamiliarCount = familiarInfo.CELevel2FamiliarCount;
            gamFile.FamiliarInfo.CELevel3FamiliarCount = familiarInfo.CELevel3FamiliarCount;
            gamFile.FamiliarInfo.CELevel4FamiliarCount = familiarInfo.CELevel4FamiliarCount;
            gamFile.FamiliarInfo.CELevel5FamiliarCount = familiarInfo.CELevel5FamiliarCount;
            gamFile.FamiliarInfo.CELevel6FamiliarCount = familiarInfo.CELevel6FamiliarCount;
            gamFile.FamiliarInfo.CELevel7FamiliarCount = familiarInfo.CELevel7FamiliarCount;
            gamFile.FamiliarInfo.CELevel8FamiliarCount = familiarInfo.CELevel8FamiliarCount;
            gamFile.FamiliarInfo.CELevel9FamiliarCount = familiarInfo.CELevel9FamiliarCount;

            foreach (var npcStructParty in npcStructPartyMembers)
            {
                var gamNpcStruct = new GamNpcStruct();
                gamNpcStruct.Selection = npcStructParty.data.Selection;
                gamNpcStruct.PartyOrder = npcStructParty.data.PartyOrder;
                gamNpcStruct.CreFile = npcStructParty.cre;
                gamNpcStruct.CreResref = npcStructParty.data.CreResref;
                gamNpcStruct.Orientation = npcStructParty.data.Orientation;
                gamNpcStruct.Area = npcStructParty.data.Area;
                gamNpcStruct.XCoordinate = npcStructParty.data.XCoordinate;
                gamNpcStruct.YCoordinate = npcStructParty.data.YCoordinate;
                gamNpcStruct.XCoordinateView = npcStructParty.data.XCoordinateView;
                gamNpcStruct.YCoordinateView = npcStructParty.data.YCoordinateView;
                gamNpcStruct.ModalAction = npcStructParty.data.ModalAction;
                gamNpcStruct.Happiness = npcStructParty.data.Happiness;
                gamNpcStruct.NumTimesInteractedNpcCount1 = npcStructParty.data.NumTimesInteractedNpcCount1;
                gamNpcStruct.NumTimesInteractedNpcCount2 = npcStructParty.data.NumTimesInteractedNpcCount2;
                gamNpcStruct.NumTimesInteractedNpcCount3 = npcStructParty.data.NumTimesInteractedNpcCount3;
                gamNpcStruct.NumTimesInteractedNpcCount4 = npcStructParty.data.NumTimesInteractedNpcCount4;
                gamNpcStruct.NumTimesInteractedNpcCount5 = npcStructParty.data.NumTimesInteractedNpcCount5;
                gamNpcStruct.NumTimesInteractedNpcCount6 = npcStructParty.data.NumTimesInteractedNpcCount6;
                gamNpcStruct.NumTimesInteractedNpcCount7 = npcStructParty.data.NumTimesInteractedNpcCount7;
                gamNpcStruct.NumTimesInteractedNpcCount8 = npcStructParty.data.NumTimesInteractedNpcCount8;
                gamNpcStruct.NumTimesInteractedNpcCount9 = npcStructParty.data.NumTimesInteractedNpcCount9;
                gamNpcStruct.NumTimesInteractedNpcCount10 = npcStructParty.data.NumTimesInteractedNpcCount10;
                gamNpcStruct.NumTimesInteractedNpcCount11 = npcStructParty.data.NumTimesInteractedNpcCount11;
                gamNpcStruct.NumTimesInteractedNpcCount12 = npcStructParty.data.NumTimesInteractedNpcCount12;
                gamNpcStruct.NumTimesInteractedNpcCount13 = npcStructParty.data.NumTimesInteractedNpcCount13;
                gamNpcStruct.NumTimesInteractedNpcCount14 = npcStructParty.data.NumTimesInteractedNpcCount14;
                gamNpcStruct.NumTimesInteractedNpcCount15 = npcStructParty.data.NumTimesInteractedNpcCount15;
                gamNpcStruct.NumTimesInteractedNpcCount16 = npcStructParty.data.NumTimesInteractedNpcCount16;
                gamNpcStruct.NumTimesInteractedNpcCount17 = npcStructParty.data.NumTimesInteractedNpcCount17;
                gamNpcStruct.NumTimesInteractedNpcCount18 = npcStructParty.data.NumTimesInteractedNpcCount18;
                gamNpcStruct.NumTimesInteractedNpcCount19 = npcStructParty.data.NumTimesInteractedNpcCount19;
                gamNpcStruct.NumTimesInteractedNpcCount20 = npcStructParty.data.NumTimesInteractedNpcCount20;
                gamNpcStruct.NumTimesInteractedNpcCount21 = npcStructParty.data.NumTimesInteractedNpcCount21;
                gamNpcStruct.NumTimesInteractedNpcCount22 = npcStructParty.data.NumTimesInteractedNpcCount22;
                gamNpcStruct.NumTimesInteractedNpcCount23 = npcStructParty.data.NumTimesInteractedNpcCount23;
                gamNpcStruct.NumTimesInteractedNpcCount24 = npcStructParty.data.NumTimesInteractedNpcCount24;
                gamNpcStruct.QuickWeaponSlot1 = npcStructParty.data.QuickWeaponSlot1;
                gamNpcStruct.QuickWeaponSlot2 = npcStructParty.data.QuickWeaponSlot2;
                gamNpcStruct.QuickWeaponSlot3 = npcStructParty.data.QuickWeaponSlot3;
                gamNpcStruct.QuickWeaponSlot4 = npcStructParty.data.QuickWeaponSlot4;
                gamNpcStruct.QuickWeaponSlot1Ability = npcStructParty.data.QuickWeaponSlot1Ability;
                gamNpcStruct.QuickWeaponSlot2Ability = npcStructParty.data.QuickWeaponSlot2Ability;
                gamNpcStruct.QuickWeaponSlot3Ability = npcStructParty.data.QuickWeaponSlot3Ability;
                gamNpcStruct.QuickWeaponSlot4Ability = npcStructParty.data.QuickWeaponSlot4Ability;
                gamNpcStruct.QuickSpell1Resource = npcStructParty.data.QuickSpell1Resource;
                gamNpcStruct.QuickSpell2Resource = npcStructParty.data.QuickSpell2Resource;
                gamNpcStruct.QuickSpell3Resource = npcStructParty.data.QuickSpell3Resource;
                gamNpcStruct.QuickItemSlot1 = npcStructParty.data.QuickItemSlot1;
                gamNpcStruct.QuickItemSlot2 = npcStructParty.data.QuickItemSlot2;
                gamNpcStruct.QuickItemSlot3 = npcStructParty.data.QuickItemSlot3;
                gamNpcStruct.QuickItemSlot1Ability = npcStructParty.data.QuickItemSlot1Ability;
                gamNpcStruct.QuickItemSlot2Ability = npcStructParty.data.QuickItemSlot2Ability;
                gamNpcStruct.QuickItemSlot3Ability = npcStructParty.data.QuickItemSlot3Ability;
                gamNpcStruct.Name = npcStructParty.data.Name;
                gamNpcStruct.TalkCount = npcStructParty.data.TalkCount;
                gamNpcStruct.CharacterStats = new GamCharacter();
                gamNpcStruct.CharacterStats.MostPowerfulVanquishedName = npcStructParty.data.CharacterStats.MostPowerfulVanquishedName;
                gamNpcStruct.CharacterStats.MostPowerfulVanquishedXP = npcStructParty.data.CharacterStats.MostPowerfulVanquishedXP;
                gamNpcStruct.CharacterStats.TimeInParty = npcStructParty.data.CharacterStats.TimeInParty;
                gamNpcStruct.CharacterStats.TimeJoinedParty = npcStructParty.data.CharacterStats.TimeJoinedParty;
                gamNpcStruct.CharacterStats.PartyMember = npcStructParty.data.CharacterStats.PartyMember;
                gamNpcStruct.CharacterStats.Unused11 = npcStructParty.data.CharacterStats.Unused11;
                gamNpcStruct.CharacterStats.FirstLetterofCreResref = npcStructParty.data.CharacterStats.FirstLetterofCreResref;
                gamNpcStruct.CharacterStats.KillsXPGainedChapter = npcStructParty.data.CharacterStats.KillsXPGainedChapter;
                gamNpcStruct.CharacterStats.KillsNumberChapter = npcStructParty.data.CharacterStats.KillsNumberChapter;
                gamNpcStruct.CharacterStats.KillsXPGainedGame = npcStructParty.data.CharacterStats.KillsXPGainedGame;
                gamNpcStruct.CharacterStats.KillsNumberGame = npcStructParty.data.CharacterStats.KillsNumberGame;
                gamNpcStruct.CharacterStats.FavouriteSpell1 = npcStructParty.data.CharacterStats.FavouriteSpell1;
                gamNpcStruct.CharacterStats.FavouriteSpell2 = npcStructParty.data.CharacterStats.FavouriteSpell2;
                gamNpcStruct.CharacterStats.FavouriteSpell3 = npcStructParty.data.CharacterStats.FavouriteSpell3;
                gamNpcStruct.CharacterStats.FavouriteSpell4 = npcStructParty.data.CharacterStats.FavouriteSpell4;
                gamNpcStruct.CharacterStats.FavouriteSpell1Count = npcStructParty.data.CharacterStats.FavouriteSpell1Count;
                gamNpcStruct.CharacterStats.FavouriteSpell2Count = npcStructParty.data.CharacterStats.FavouriteSpell2Count;
                gamNpcStruct.CharacterStats.FavouriteSpell3Count = npcStructParty.data.CharacterStats.FavouriteSpell3Count;
                gamNpcStruct.CharacterStats.FavouriteSpell4Count = npcStructParty.data.CharacterStats.FavouriteSpell4Count;
                gamNpcStruct.CharacterStats.FavouriteWeapon1 = npcStructParty.data.CharacterStats.FavouriteWeapon1;
                gamNpcStruct.CharacterStats.FavouriteWeapon2 = npcStructParty.data.CharacterStats.FavouriteWeapon2;
                gamNpcStruct.CharacterStats.FavouriteWeapon3 = npcStructParty.data.CharacterStats.FavouriteWeapon3;
                gamNpcStruct.CharacterStats.FavouriteWeapon4 = npcStructParty.data.CharacterStats.FavouriteWeapon4;
                gamNpcStruct.CharacterStats.FavouriteWeapon1Time = npcStructParty.data.CharacterStats.FavouriteWeapon1Time;
                gamNpcStruct.CharacterStats.FavouriteWeapon2Time = npcStructParty.data.CharacterStats.FavouriteWeapon2Time;
                gamNpcStruct.CharacterStats.FavouriteWeapon3Time = npcStructParty.data.CharacterStats.FavouriteWeapon3Time;
                gamNpcStruct.CharacterStats.FavouriteWeapon4Time = npcStructParty.data.CharacterStats.FavouriteWeapon4Time;
                gamNpcStruct.VoiceSet = npcStructParty.data.VoiceSet;
                gamFile.PartyMembers.Add(gamNpcStruct);
            }

            foreach (var npcStructParty in npcStructNonParty)
            {
                var gamNpcStruct = new GamNpcStruct();
                gamNpcStruct.Selection = npcStructParty.data.Selection;
                gamNpcStruct.PartyOrder = npcStructParty.data.PartyOrder;
                gamNpcStruct.CreFile = npcStructParty.cre;
                gamNpcStruct.CreResref = npcStructParty.data.CreResref;
                gamNpcStruct.Orientation = npcStructParty.data.Orientation;
                gamNpcStruct.Area = npcStructParty.data.Area;
                gamNpcStruct.XCoordinate = npcStructParty.data.XCoordinate;
                gamNpcStruct.YCoordinate = npcStructParty.data.YCoordinate;
                gamNpcStruct.XCoordinateView = npcStructParty.data.XCoordinateView;
                gamNpcStruct.YCoordinateView = npcStructParty.data.YCoordinateView;
                gamNpcStruct.ModalAction = npcStructParty.data.ModalAction;
                gamNpcStruct.Happiness = npcStructParty.data.Happiness;
                gamNpcStruct.NumTimesInteractedNpcCount1 = npcStructParty.data.NumTimesInteractedNpcCount1;
                gamNpcStruct.NumTimesInteractedNpcCount2 = npcStructParty.data.NumTimesInteractedNpcCount2;
                gamNpcStruct.NumTimesInteractedNpcCount3 = npcStructParty.data.NumTimesInteractedNpcCount3;
                gamNpcStruct.NumTimesInteractedNpcCount4 = npcStructParty.data.NumTimesInteractedNpcCount4;
                gamNpcStruct.NumTimesInteractedNpcCount5 = npcStructParty.data.NumTimesInteractedNpcCount5;
                gamNpcStruct.NumTimesInteractedNpcCount6 = npcStructParty.data.NumTimesInteractedNpcCount6;
                gamNpcStruct.NumTimesInteractedNpcCount7 = npcStructParty.data.NumTimesInteractedNpcCount7;
                gamNpcStruct.NumTimesInteractedNpcCount8 = npcStructParty.data.NumTimesInteractedNpcCount8;
                gamNpcStruct.NumTimesInteractedNpcCount9 = npcStructParty.data.NumTimesInteractedNpcCount9;
                gamNpcStruct.NumTimesInteractedNpcCount10 = npcStructParty.data.NumTimesInteractedNpcCount10;
                gamNpcStruct.NumTimesInteractedNpcCount11 = npcStructParty.data.NumTimesInteractedNpcCount11;
                gamNpcStruct.NumTimesInteractedNpcCount12 = npcStructParty.data.NumTimesInteractedNpcCount12;
                gamNpcStruct.NumTimesInteractedNpcCount13 = npcStructParty.data.NumTimesInteractedNpcCount13;
                gamNpcStruct.NumTimesInteractedNpcCount14 = npcStructParty.data.NumTimesInteractedNpcCount14;
                gamNpcStruct.NumTimesInteractedNpcCount15 = npcStructParty.data.NumTimesInteractedNpcCount15;
                gamNpcStruct.NumTimesInteractedNpcCount16 = npcStructParty.data.NumTimesInteractedNpcCount16;
                gamNpcStruct.NumTimesInteractedNpcCount17 = npcStructParty.data.NumTimesInteractedNpcCount17;
                gamNpcStruct.NumTimesInteractedNpcCount18 = npcStructParty.data.NumTimesInteractedNpcCount18;
                gamNpcStruct.NumTimesInteractedNpcCount19 = npcStructParty.data.NumTimesInteractedNpcCount19;
                gamNpcStruct.NumTimesInteractedNpcCount20 = npcStructParty.data.NumTimesInteractedNpcCount20;
                gamNpcStruct.NumTimesInteractedNpcCount21 = npcStructParty.data.NumTimesInteractedNpcCount21;
                gamNpcStruct.NumTimesInteractedNpcCount22 = npcStructParty.data.NumTimesInteractedNpcCount22;
                gamNpcStruct.NumTimesInteractedNpcCount23 = npcStructParty.data.NumTimesInteractedNpcCount23;
                gamNpcStruct.NumTimesInteractedNpcCount24 = npcStructParty.data.NumTimesInteractedNpcCount24;
                gamNpcStruct.QuickWeaponSlot1 = npcStructParty.data.QuickWeaponSlot1;
                gamNpcStruct.QuickWeaponSlot2 = npcStructParty.data.QuickWeaponSlot2;
                gamNpcStruct.QuickWeaponSlot3 = npcStructParty.data.QuickWeaponSlot3;
                gamNpcStruct.QuickWeaponSlot4 = npcStructParty.data.QuickWeaponSlot4;
                gamNpcStruct.QuickWeaponSlot1Ability = npcStructParty.data.QuickWeaponSlot1Ability;
                gamNpcStruct.QuickWeaponSlot2Ability = npcStructParty.data.QuickWeaponSlot2Ability;
                gamNpcStruct.QuickWeaponSlot3Ability = npcStructParty.data.QuickWeaponSlot3Ability;
                gamNpcStruct.QuickWeaponSlot4Ability = npcStructParty.data.QuickWeaponSlot4Ability;
                gamNpcStruct.QuickSpell1Resource = npcStructParty.data.QuickSpell1Resource;
                gamNpcStruct.QuickSpell2Resource = npcStructParty.data.QuickSpell2Resource;
                gamNpcStruct.QuickSpell3Resource = npcStructParty.data.QuickSpell3Resource;
                gamNpcStruct.QuickItemSlot1 = npcStructParty.data.QuickItemSlot1;
                gamNpcStruct.QuickItemSlot2 = npcStructParty.data.QuickItemSlot2;
                gamNpcStruct.QuickItemSlot3 = npcStructParty.data.QuickItemSlot3;
                gamNpcStruct.QuickItemSlot1Ability = npcStructParty.data.QuickItemSlot1Ability;
                gamNpcStruct.QuickItemSlot2Ability = npcStructParty.data.QuickItemSlot2Ability;
                gamNpcStruct.QuickItemSlot3Ability = npcStructParty.data.QuickItemSlot3Ability;
                gamNpcStruct.Name = npcStructParty.data.Name;
                gamNpcStruct.TalkCount = npcStructParty.data.TalkCount;
                gamNpcStruct.CharacterStats = new GamCharacter();
                gamNpcStruct.CharacterStats.MostPowerfulVanquishedName = npcStructParty.data.CharacterStats.MostPowerfulVanquishedName;
                gamNpcStruct.CharacterStats.MostPowerfulVanquishedXP = npcStructParty.data.CharacterStats.MostPowerfulVanquishedXP;
                gamNpcStruct.CharacterStats.TimeInParty = npcStructParty.data.CharacterStats.TimeInParty;
                gamNpcStruct.CharacterStats.TimeJoinedParty = npcStructParty.data.CharacterStats.TimeJoinedParty;
                gamNpcStruct.CharacterStats.PartyMember = npcStructParty.data.CharacterStats.PartyMember;
                gamNpcStruct.CharacterStats.Unused11 = npcStructParty.data.CharacterStats.Unused11;
                gamNpcStruct.CharacterStats.FirstLetterofCreResref = npcStructParty.data.CharacterStats.FirstLetterofCreResref;
                gamNpcStruct.CharacterStats.KillsXPGainedChapter = npcStructParty.data.CharacterStats.KillsXPGainedChapter;
                gamNpcStruct.CharacterStats.KillsNumberChapter = npcStructParty.data.CharacterStats.KillsNumberChapter;
                gamNpcStruct.CharacterStats.KillsXPGainedGame = npcStructParty.data.CharacterStats.KillsXPGainedGame;
                gamNpcStruct.CharacterStats.KillsNumberGame = npcStructParty.data.CharacterStats.KillsNumberGame;
                gamNpcStruct.CharacterStats.FavouriteSpell1 = npcStructParty.data.CharacterStats.FavouriteSpell1;
                gamNpcStruct.CharacterStats.FavouriteSpell2 = npcStructParty.data.CharacterStats.FavouriteSpell2;
                gamNpcStruct.CharacterStats.FavouriteSpell3 = npcStructParty.data.CharacterStats.FavouriteSpell3;
                gamNpcStruct.CharacterStats.FavouriteSpell4 = npcStructParty.data.CharacterStats.FavouriteSpell4;
                gamNpcStruct.CharacterStats.FavouriteSpell1Count = npcStructParty.data.CharacterStats.FavouriteSpell1Count;
                gamNpcStruct.CharacterStats.FavouriteSpell2Count = npcStructParty.data.CharacterStats.FavouriteSpell2Count;
                gamNpcStruct.CharacterStats.FavouriteSpell3Count = npcStructParty.data.CharacterStats.FavouriteSpell3Count;
                gamNpcStruct.CharacterStats.FavouriteSpell4Count = npcStructParty.data.CharacterStats.FavouriteSpell4Count;
                gamNpcStruct.CharacterStats.FavouriteWeapon1 = npcStructParty.data.CharacterStats.FavouriteWeapon1;
                gamNpcStruct.CharacterStats.FavouriteWeapon2 = npcStructParty.data.CharacterStats.FavouriteWeapon2;
                gamNpcStruct.CharacterStats.FavouriteWeapon3 = npcStructParty.data.CharacterStats.FavouriteWeapon3;
                gamNpcStruct.CharacterStats.FavouriteWeapon4 = npcStructParty.data.CharacterStats.FavouriteWeapon4;
                gamNpcStruct.CharacterStats.FavouriteWeapon1Time = npcStructParty.data.CharacterStats.FavouriteWeapon1Time;
                gamNpcStruct.CharacterStats.FavouriteWeapon2Time = npcStructParty.data.CharacterStats.FavouriteWeapon2Time;
                gamNpcStruct.CharacterStats.FavouriteWeapon3Time = npcStructParty.data.CharacterStats.FavouriteWeapon3Time;
                gamNpcStruct.CharacterStats.FavouriteWeapon4Time = npcStructParty.data.CharacterStats.FavouriteWeapon4Time;
                gamNpcStruct.VoiceSet = npcStructParty.data.VoiceSet;
                gamFile.NonPartyMembers.Add(gamNpcStruct);
            }

            foreach (var variable in variables)
            {
                var gamVariable = new GamVariable();
                gamVariable.Name = variable.Name;
                gamVariable.Type = variable.Type;
                gamVariable.ResourceType = variable.ResourceType;
                gamVariable.ValueDword = variable.ValueDword;
                gamVariable.ValueInt = variable.ValueInt;
                gamVariable.ValueDouble = variable.ValueDouble;
                gamVariable.ScriptName = variable.ScriptName;
                gamFile.Variables.Add(gamVariable);
            }

            foreach (var journal in journals)
            {
                var gamJournal = new GamJournal();
                gamJournal.JournalText = Common.ReadString(journal.JournalText, TlkFile);
                gamJournal.Time = journal.Time;
                gamJournal.Chapter = journal.Chapter;
                gamJournal.ReadBy = journal.ReadBy;
                gamJournal.JournalSection = journal.JournalSection;
                gamJournal.LocationFlag = journal.LocationFlag;
                gamFile.JournalEntries.Add(gamJournal);
            }

            foreach (var storedLocation in storedLocations)
            {
                var gamStoredLocation = new GamStoredLocation();
                gamStoredLocation.Area = storedLocation.Area;
                gamStoredLocation.XCoordinate = storedLocation.XCoordinate;
                gamStoredLocation.YCoordinate = storedLocation.YCoordinate;
                gamFile.StoredLocations.Add(gamStoredLocation);
            }

            foreach (var pocketPlaneLocation in pocketPlaneLocations)
            {
                var gamPocketPlaneLocation = new GamStoredLocation();
                gamPocketPlaneLocation.Area = pocketPlaneLocation.Area;
                gamPocketPlaneLocation.XCoordinate = pocketPlaneLocation.XCoordinate;
                gamPocketPlaneLocation.YCoordinate = pocketPlaneLocation.YCoordinate;
                gamFile.PocketPlaneLocations.Add(gamPocketPlaneLocation);
            }

            gamFile.Checksum = HashGenerator.GenerateKey(gamFile);
            return gamFile;
        }

        private void ReadFamiliarResRefs(BinaryReader br, int count, List<array8> list)
        {
            for (int i = 0; i < count; i++)
            {
                var resref = br.ReadChars(8);
                list.Add(new array8()
                {
                    character1 = resref[0],
                    character2 = resref[1],
                    character3 = resref[2],
                    character4 = resref[3],
                    character5 = resref[4],
                    character6 = resref[5],
                    character7 = resref[6],
                });
            }
        }
    }
}