using System.Collections.Generic;
using System.IO;
using WindowsFormsApplication1.Binary;

namespace WindowsFormsApplication1.Readers
{
    class AreFileBinaryReader
    {
        List<AreActor> actors = new List<AreActor>();
        List<AreRegion> regions = new List<AreRegion>();
        List<AreSpawnPoint> spawns = new List<AreSpawnPoint>();
        List<AreEntrance> entrances = new List<AreEntrance>();
        List<AreContainer> containers = new List<AreContainer>();

        public void Read(string filename)  //todo 
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                //return Read(fs);
                Read(fs);
            }
        }

        public void Read(Stream s) //todo;
        {
            using (BinaryReader br = new BinaryReader(s))
            {
                var header = (AreHeader)Common.ReadStruct(br, typeof(AreHeader));

                //if (header.ftype.ToString() != "AREA")
                    //return new AreFile();
                
                br.BaseStream.Seek(header.ActorOffset, SeekOrigin.Begin);
                for (int i = 0; i < header.ActorCount; i++)
                {
                    var actor = (AreActor)Common.ReadStruct(br, typeof(AreActor));
                    actors.Add(actor);
                }
                
                br.BaseStream.Seek(header.RegionOffset, SeekOrigin.Begin);
                for (int i = 0; i < header.RegionCount; i++)
                {
                    var region = (AreRegion)Common.ReadStruct(br, typeof(AreRegion));
                    regions.Add(region);
                }
                
                br.BaseStream.Seek(header.SpawnPointOffset, SeekOrigin.Begin);
                for (int i = 0; i < header.SpawnPointCount; i++)
                {
                    var spawn = (AreSpawnPoint)Common.ReadStruct(br, typeof(AreSpawnPoint));
                    spawns.Add(spawn);
                }

                br.BaseStream.Seek(header.EntrancesOffset, SeekOrigin.Begin);
                for (int i = 0; i < header.EntrancesCount; i++)
                {
                    var spawn = (AreEntrance)Common.ReadStruct(br, typeof(AreEntrance));
                    entrances.Add(spawn);
                }

                br.BaseStream.Seek(header.ContainerOffset, SeekOrigin.Begin);
                for (int i = 0; i < header.ContainerCount; i++)
                {
                    var container = (AreContainer)Common.ReadStruct(br, typeof(AreContainer));
                    containers.Add(container);
                }

                /*
                br.BaseStream.Seek(header.EffectOffset, SeekOrigin.Begin);
                for (int i = 0; i < header.EffectCount; i++)
                {
                    if (header.EffVersion == 0)
                    {
                        var creEffect = (Eff1Binary)Common.ReadStruct(br, typeof(Eff1Binary));
                        creEffects1.Add(creEffect);
                    }
                    else
                    {
                        var creEffect = (EmbeddedEff)Common.ReadStruct(br, typeof(EmbeddedEff));
                        creEffects2.Add(creEffect);
                    }
                }

                br.BaseStream.Seek(header.ItemOffset, SeekOrigin.Begin);
                for (int i = 0; i < header.ItemCount; i++)
                {
                    var creItem = (CreItem)Common.ReadStruct(br, typeof(CreItem));
                    creItems.Add(creItem);
                }

                br.BaseStream.Seek(header.ItemSlotOffset, SeekOrigin.Begin);
                for (int i = 0; i < 40; i++)
                {
                    var creItemSlot = (short)Common.ReadStruct(br, typeof(short));
                    creItemSlots.Add(creItemSlot);
                }

                var creFile = new CreFile();
                creFile.Flags.ShowLongname = (header.Flags & Common.Bit0) != 0;
                creFile.Flags.NoCorpse = (header.Flags & Common.Bit1) != 0;
                creFile.Flags.KeepCorpse = (header.Flags & Common.Bit2) != 0;
                creFile.Flags.OriginalFighter = (header.Flags & Common.Bit3) != 0;
                creFile.Flags.OriginalMage = (header.Flags & Common.Bit4) != 0;
                creFile.Flags.OriginalCleric = (header.Flags & Common.Bit5) != 0;
                creFile.Flags.OriginalThief = (header.Flags & Common.Bit6) != 0;
                creFile.Flags.OriginalDruid = (header.Flags & Common.Bit7) != 0;


                return creFile;*/
            }
        }
    }
}