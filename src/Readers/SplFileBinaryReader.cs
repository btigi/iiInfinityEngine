using System.Collections.Generic;
using System.IO;
using ii.InfinityEngine.Binary;
using ii.InfinityEngine.Files;
using ii.InfinityEngine.Readers.Interfaces;

namespace ii.InfinityEngine.Readers
{
    public class SplFileBinaryReader : ISplFileReader
    {
        public TlkFile TlkFile { get; set; }

        public SplFile Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var f = Read(fs);
            f.Filename = Path.GetFileName(filename);
            return f;
        }

        public SplFile Read(Stream s)
        {
            using var br = new BinaryReader(s);
            var splFile = ParseFile(br);
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            splFile.OriginalFile = ParseFile(br);
            return splFile;
        }

        private SplFile ParseFile(BinaryReader br)
        {
            var header = (SplHeaderBinary)Common.ReadStruct(br, typeof(SplHeaderBinary));

            if (header.ftype.ToString() != "SPL ")
                return new SplFile();

            var splExtendedHeaders = new List<SplExtendedHeaderBinary>();
            var splFeatureBlocks = new List<SplFeatureBlockBinary>();

            br.BaseStream.Seek(header.ExtendedHeaderOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.ExtendedHeaderCount; i++)
            {
                var itmExtendedHeader = (SplExtendedHeaderBinary)Common.ReadStruct(br, typeof(SplExtendedHeaderBinary));
                splExtendedHeaders.Add(itmExtendedHeader);
            }

            br.BaseStream.Seek(header.FeatureBlockOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.FeatureBlockCastingCount; i++)
            {
                var splFeatureBlock = (SplFeatureBlockBinary)Common.ReadStruct(br, typeof(SplFeatureBlockBinary));
                splFeatureBlocks.Add(splFeatureBlock);
            }

            var splFile = new SplFile();
            //splFile.ftype 
            //splFile.fversion 
            splFile.UnidentifiedName = Common.ReadString(header.UnidentifiedName, TlkFile);
            splFile.IdentifiedName = Common.ReadString(header.IdentifiedName, TlkFile);
            splFile.CompletionSound = header.CompletionSound;
            splFile.Flags.Bit0 = (header.Flags & Common.Bit0) != 0;
            splFile.Flags.Bit1 = (header.Flags & Common.Bit1) != 0;
            splFile.Flags.Bit2 = (header.Flags & Common.Bit2) != 0;
            splFile.Flags.Bit3 = (header.Flags & Common.Bit3) != 0;
            splFile.Flags.Bit4 = (header.Flags & Common.Bit4) != 0;
            splFile.Flags.Bit5 = (header.Flags & Common.Bit5) != 0;
            splFile.Flags.Bit6 = (header.Flags & Common.Bit6) != 0;
            splFile.Flags.Bit7 = (header.Flags & Common.Bit7) != 0;
            splFile.Flags.Bit8 = (header.Flags & Common.Bit8) != 0;
            splFile.Flags.BreaksSanctuaryInvisibility = (header.Flags & Common.Bit9) != 0;
            splFile.Flags.Hostile = (header.Flags & Common.Bit10) != 0;
            splFile.Flags.NoLOSRequired = (header.Flags & Common.Bit11) != 0;
            splFile.Flags.AllowSpotting = (header.Flags & Common.Bit12) != 0;
            splFile.Flags.OutdoorsOnly = (header.Flags & Common.Bit13) != 0;
            splFile.Flags.IgnoreWildSurgeDeadMagic = (header.Flags & Common.Bit14) != 0;
            splFile.Flags.IgnoreWildSurge = (header.Flags & Common.Bit15) != 0;
            splFile.Flags.NonCombatAbility = (header.Flags & Common.Bit16) != 0;
            splFile.Flags.Bit17 = (header.Flags & Common.Bit17) != 0;
            splFile.Flags.Bit18 = (header.Flags & Common.Bit18) != 0;
            splFile.Flags.Bit19 = (header.Flags & Common.Bit19) != 0;
            splFile.Flags.Bit20 = (header.Flags & Common.Bit20) != 0;
            splFile.Flags.Bit21 = (header.Flags & Common.Bit21) != 0;
            splFile.Flags.Bit22 = (header.Flags & Common.Bit22) != 0;
            splFile.Flags.Bit23 = (header.Flags & Common.Bit23) != 0;
            splFile.Flags.CanTargetInvisible = (header.Flags & Common.Bit24) != 0;
            splFile.Flags.CastableWhenSilenced = (header.Flags & Common.Bit25) != 0;
            splFile.Flags.Bit26 = (header.Flags & Common.Bit26) != 0;
            splFile.Flags.Bit27 = (header.Flags & Common.Bit27) != 0;
            splFile.Flags.Bit28 = (header.Flags & Common.Bit28) != 0;
            splFile.Flags.Bit29 = (header.Flags & Common.Bit29) != 0;
            splFile.Flags.Bit30 = (header.Flags & Common.Bit30) != 0;
            splFile.Flags.Bit31 = (header.Flags & Common.Bit31) != 0;
            splFile.SpellType = (SpellType)header.SpellType;
            splFile.ExclusionFlags = header.ExclusionFlags;
            splFile.CastingGraphic = header.CastingGraphic;
            splFile.Unknown24 = header.Unused24;
            splFile.PrimaryType = header.PrimaryType;
            splFile.Unknown26 = header.Unused26;
            splFile.SecondaryType = header.SecondaryType;
            splFile.Unused28 = header.Unused28;
            splFile.Unused29 = header.Unused29;
            splFile.Unused2a = header.Unused2a;
            splFile.Unused2b = header.Unused2b;
            splFile.Unused2c = header.Unused2c;
            splFile.Unused2d = header.Unused2d;
            splFile.Unused2e = header.Unused2e;
            splFile.Unused2f = header.Unused2f;
            splFile.Unused30 = header.Unused30;
            splFile.Unused32 = header.Unused32;
            splFile.SpellLevel = header.SpellLevel;
            splFile.Unused38 = header.Unused38;
            splFile.SpellBookIcon = header.SpellBookIcon;
            splFile.Unused42 = header.Unused42;
            splFile.Unused44 = header.Unused44;
            splFile.Unused4c = header.Unused4c;
            splFile.UnidentifiedDescription = Common.ReadString(header.UnidentifiedDescription, TlkFile);
            splFile.IdentifiedDescription = Common.ReadString(header.IdentifiedDescription, TlkFile);
            splFile.Unused58 = header.Unused58;
            splFile.Unused60 = header.Unused60;

            var totalFeatureBlockCount = 0;

            foreach (var extendedHeader in splExtendedHeaders)
            {
                var extendedHeader2 = new SplExtendedHeader();
                extendedHeader2.CastingTime = extendedHeader.CastingTime;
                extendedHeader2.FeatureBlockCount = extendedHeader.FeatureBlockCount;
                extendedHeader2.FeatureBlockOffset = extendedHeader.FeatureBlockOffset;
                extendedHeader2.LevelRequired = extendedHeader.LevelRequired;
                extendedHeader2.Location = (SpellLocation)extendedHeader.Location;
                extendedHeader2.MemorisedIcon = extendedHeader.MemorisedIcon;
                extendedHeader2.ProjectileAnimation = extendedHeader.ProjectileAnimation;
                extendedHeader2.Range = extendedHeader.Range;
                extendedHeader2.SpellForm = (SpellForm)extendedHeader.SpellForm;
                extendedHeader2.TargetCount = extendedHeader.TargetCount;
                extendedHeader2.TargetType = (SpellTarget)extendedHeader.TargetType;
                extendedHeader2.TimesPerDay = extendedHeader.TimesPerDay;
                extendedHeader2.Unused1 = extendedHeader.Unused1;
                extendedHeader2.Unused16 = extendedHeader.Unused16;
                extendedHeader2.Unused18 = extendedHeader.Unused18;
                extendedHeader2.Unused1a = extendedHeader.Unused1a;
                extendedHeader2.Unused1c = extendedHeader.Unused1c;
                extendedHeader2.Unused22 = extendedHeader.Unused22;
                extendedHeader2.Unused24 = extendedHeader.Unused24;

                br.BaseStream.Seek(header.FeatureBlockOffset + (header.FeatureBlockCastingCount * 48) + (totalFeatureBlockCount * 48), SeekOrigin.Begin);
                for (int i = 0; i < extendedHeader.FeatureBlockCount; i++)
                {
                    var featureBlock = (SplFeatureBlockBinary)Common.ReadStruct(br, typeof(SplFeatureBlockBinary));

                    var splFeatureBlock2 = new SplFeatureBlock();
                    splFeatureBlock2.DiceSides = featureBlock.DiceSides;
                    splFeatureBlock2.DiceThrown = featureBlock.DiceThrown;
                    splFeatureBlock2.Duration = featureBlock.Duration;
                    splFeatureBlock2.Opcode = featureBlock.Opcode;
                    splFeatureBlock2.Parameter1 = featureBlock.Parameter1;
                    splFeatureBlock2.Parameter2 = featureBlock.Parameter2;
                    splFeatureBlock2.Power = featureBlock.Power;
                    splFeatureBlock2.Probability1 = featureBlock.Probability1;
                    splFeatureBlock2.Probability2 = featureBlock.Probability2;
                    splFeatureBlock2.Resistance.DispellableAffectedByMagicResistance = (featureBlock.Resistance & Common.Bit0) != 0;
                    splFeatureBlock2.Resistance.IgnoreMagicResistance = (featureBlock.Resistance & Common.Bit1) != 0;
                    splFeatureBlock2.Resistance.Bit2 = (featureBlock.Resistance & Common.Bit2) != 0;
                    splFeatureBlock2.Resistance.Bit3 = (featureBlock.Resistance & Common.Bit3) != 0;
                    splFeatureBlock2.Resistance.Bit4 = (featureBlock.Resistance & Common.Bit4) != 0;
                    splFeatureBlock2.Resistance.Bit5 = (featureBlock.Resistance & Common.Bit5) != 0;
                    splFeatureBlock2.Resistance.Bit6 = (featureBlock.Resistance & Common.Bit6) != 0;
                    splFeatureBlock2.Resistance.Bit7 = (featureBlock.Resistance & Common.Bit7) != 0;
                    splFeatureBlock2.Resource = featureBlock.Resource;
                    splFeatureBlock2.SavingThrowBonus = featureBlock.SavingThrowBonus;
                    splFeatureBlock2.SavingThrowType.Spells = (featureBlock.SavingThrowType & Common.Bit0) != 0;
                    splFeatureBlock2.SavingThrowType.Breath = (featureBlock.SavingThrowType & Common.Bit1) != 0;
                    splFeatureBlock2.SavingThrowType.ParalyzePoisonDeath = (featureBlock.SavingThrowType & Common.Bit2) != 0;
                    splFeatureBlock2.SavingThrowType.Wands = (featureBlock.SavingThrowType & Common.Bit3) != 0;
                    splFeatureBlock2.SavingThrowType.PetrifyPolymorph = (featureBlock.SavingThrowType & Common.Bit4) != 0;
                    splFeatureBlock2.SavingThrowType.Bit5 = (featureBlock.SavingThrowType & Common.Bit5) != 0;
                    splFeatureBlock2.SavingThrowType.Bit6 = (featureBlock.SavingThrowType & Common.Bit6) != 0;
                    splFeatureBlock2.SavingThrowType.Bit7 = (featureBlock.SavingThrowType & Common.Bit7) != 0;
                    splFeatureBlock2.SavingThrowType.Bit8 = (featureBlock.SavingThrowType & Common.Bit8) != 0;
                    splFeatureBlock2.SavingThrowType.Bit9 = (featureBlock.SavingThrowType & Common.Bit9) != 0;
                    splFeatureBlock2.SavingThrowType.IgnorePrimaryTarget = (featureBlock.SavingThrowType & Common.Bit10) != 0;
                    splFeatureBlock2.SavingThrowType.IgnoreSecondaryTarget = (featureBlock.SavingThrowType & Common.Bit11) != 0;
                    splFeatureBlock2.SavingThrowType.Bit12 = (featureBlock.SavingThrowType & Common.Bit12) != 0;
                    splFeatureBlock2.SavingThrowType.Bit13 = (featureBlock.SavingThrowType & Common.Bit13) != 0;
                    splFeatureBlock2.SavingThrowType.Bit14 = (featureBlock.SavingThrowType & Common.Bit14) != 0;
                    splFeatureBlock2.SavingThrowType.Bit15 = (featureBlock.SavingThrowType & Common.Bit15) != 0;
                    splFeatureBlock2.SavingThrowType.Bit16 = (featureBlock.SavingThrowType & Common.Bit16) != 0;
                    splFeatureBlock2.SavingThrowType.Bit17 = (featureBlock.SavingThrowType & Common.Bit17) != 0;
                    splFeatureBlock2.SavingThrowType.Bit18 = (featureBlock.SavingThrowType & Common.Bit18) != 0;
                    splFeatureBlock2.SavingThrowType.Bit19 = (featureBlock.SavingThrowType & Common.Bit19) != 0;
                    splFeatureBlock2.SavingThrowType.Bit20 = (featureBlock.SavingThrowType & Common.Bit20) != 0;
                    splFeatureBlock2.SavingThrowType.Bit21 = (featureBlock.SavingThrowType & Common.Bit21) != 0;
                    splFeatureBlock2.SavingThrowType.Bit22 = (featureBlock.SavingThrowType & Common.Bit22) != 0;
                    splFeatureBlock2.SavingThrowType.Bit23 = (featureBlock.SavingThrowType & Common.Bit23) != 0;
                    splFeatureBlock2.SavingThrowType.BypassMirrorImage = (featureBlock.SavingThrowType & Common.Bit24) != 0;
                    splFeatureBlock2.SavingThrowType.IgnoreDifficulty = (featureBlock.SavingThrowType & Common.Bit25) != 0;
                    splFeatureBlock2.SavingThrowType.Bit26 = (featureBlock.SavingThrowType & Common.Bit26) != 0;
                    splFeatureBlock2.SavingThrowType.Bit27 = (featureBlock.SavingThrowType & Common.Bit27) != 0;
                    splFeatureBlock2.SavingThrowType.Bit28 = (featureBlock.SavingThrowType & Common.Bit28) != 0;
                    splFeatureBlock2.SavingThrowType.Bit29 = (featureBlock.SavingThrowType & Common.Bit29) != 0;
                    splFeatureBlock2.SavingThrowType.Bit30 = (featureBlock.SavingThrowType & Common.Bit30) != 0;
                    splFeatureBlock2.SavingThrowType.Bit31 = (featureBlock.SavingThrowType & Common.Bit31) != 0;
                    splFeatureBlock2.TargetType = (SpellAbilityTarget)featureBlock.TargetType;
                    splFeatureBlock2.TimingMode = (TimingMode)featureBlock.TimingMode;
                    splFeatureBlock2.Unused2c = featureBlock.Unused2c;

                    extendedHeader2.splFeatureBlocks.Add(splFeatureBlock2);
                    totalFeatureBlockCount++;
                }

                splFile.splExtendedHeader.Add(extendedHeader2);
            }

            foreach (var featureBlock in splFeatureBlocks)
            {
                var splFeatureBlock2 = new SplFeatureBlock();
                splFeatureBlock2.DiceSides = featureBlock.DiceSides;
                splFeatureBlock2.DiceThrown = featureBlock.DiceThrown;
                splFeatureBlock2.Duration = featureBlock.Duration;
                splFeatureBlock2.Opcode = featureBlock.Opcode;
                splFeatureBlock2.Parameter1 = featureBlock.Parameter1;
                splFeatureBlock2.Parameter2 = featureBlock.Parameter2;
                splFeatureBlock2.Power = featureBlock.Power;
                splFeatureBlock2.Probability1 = featureBlock.Probability1;
                splFeatureBlock2.Probability2 = featureBlock.Probability2;
                splFeatureBlock2.Resistance.DispellableAffectedByMagicResistance = (featureBlock.Resistance & Common.Bit0) != 0;
                splFeatureBlock2.Resistance.IgnoreMagicResistance = (featureBlock.Resistance & Common.Bit1) != 0;
                splFeatureBlock2.Resistance.Bit2 = (featureBlock.Resistance & Common.Bit2) != 0;
                splFeatureBlock2.Resistance.Bit3 = (featureBlock.Resistance & Common.Bit3) != 0;
                splFeatureBlock2.Resistance.Bit4 = (featureBlock.Resistance & Common.Bit4) != 0;
                splFeatureBlock2.Resistance.Bit5 = (featureBlock.Resistance & Common.Bit5) != 0;
                splFeatureBlock2.Resistance.Bit6 = (featureBlock.Resistance & Common.Bit6) != 0;
                splFeatureBlock2.Resistance.Bit7 = (featureBlock.Resistance & Common.Bit7) != 0;
                splFeatureBlock2.Resource = featureBlock.Resource;
                splFeatureBlock2.SavingThrowBonus = featureBlock.SavingThrowBonus;
                splFeatureBlock2.SavingThrowType.Spells = (featureBlock.SavingThrowType & Common.Bit0) != 0;
                splFeatureBlock2.SavingThrowType.Breath = (featureBlock.SavingThrowType & Common.Bit1) != 0;
                splFeatureBlock2.SavingThrowType.ParalyzePoisonDeath = (featureBlock.SavingThrowType & Common.Bit2) != 0;
                splFeatureBlock2.SavingThrowType.Wands = (featureBlock.SavingThrowType & Common.Bit3) != 0;
                splFeatureBlock2.SavingThrowType.PetrifyPolymorph = (featureBlock.SavingThrowType & Common.Bit4) != 0;
                splFeatureBlock2.SavingThrowType.Bit5 = (featureBlock.SavingThrowType & Common.Bit5) != 0;
                splFeatureBlock2.SavingThrowType.Bit6 = (featureBlock.SavingThrowType & Common.Bit6) != 0;
                splFeatureBlock2.SavingThrowType.Bit7 = (featureBlock.SavingThrowType & Common.Bit7) != 0;
                splFeatureBlock2.SavingThrowType.Bit8 = (featureBlock.SavingThrowType & Common.Bit8) != 0;
                splFeatureBlock2.SavingThrowType.Bit9 = (featureBlock.SavingThrowType & Common.Bit9) != 0;
                splFeatureBlock2.SavingThrowType.IgnorePrimaryTarget = (featureBlock.SavingThrowType & Common.Bit10) != 0;
                splFeatureBlock2.SavingThrowType.IgnoreSecondaryTarget = (featureBlock.SavingThrowType & Common.Bit11) != 0;
                splFeatureBlock2.SavingThrowType.Bit12 = (featureBlock.SavingThrowType & Common.Bit12) != 0;
                splFeatureBlock2.SavingThrowType.Bit13 = (featureBlock.SavingThrowType & Common.Bit13) != 0;
                splFeatureBlock2.SavingThrowType.Bit14 = (featureBlock.SavingThrowType & Common.Bit14) != 0;
                splFeatureBlock2.SavingThrowType.Bit15 = (featureBlock.SavingThrowType & Common.Bit15) != 0;
                splFeatureBlock2.SavingThrowType.Bit16 = (featureBlock.SavingThrowType & Common.Bit16) != 0;
                splFeatureBlock2.SavingThrowType.Bit17 = (featureBlock.SavingThrowType & Common.Bit17) != 0;
                splFeatureBlock2.SavingThrowType.Bit18 = (featureBlock.SavingThrowType & Common.Bit18) != 0;
                splFeatureBlock2.SavingThrowType.Bit19 = (featureBlock.SavingThrowType & Common.Bit19) != 0;
                splFeatureBlock2.SavingThrowType.Bit20 = (featureBlock.SavingThrowType & Common.Bit20) != 0;
                splFeatureBlock2.SavingThrowType.Bit21 = (featureBlock.SavingThrowType & Common.Bit21) != 0;
                splFeatureBlock2.SavingThrowType.Bit22 = (featureBlock.SavingThrowType & Common.Bit22) != 0;
                splFeatureBlock2.SavingThrowType.Bit23 = (featureBlock.SavingThrowType & Common.Bit23) != 0;
                splFeatureBlock2.SavingThrowType.BypassMirrorImage = (featureBlock.SavingThrowType & Common.Bit24) != 0;
                splFeatureBlock2.SavingThrowType.IgnoreDifficulty = (featureBlock.SavingThrowType & Common.Bit25) != 0;
                splFeatureBlock2.SavingThrowType.Bit26 = (featureBlock.SavingThrowType & Common.Bit26) != 0;
                splFeatureBlock2.SavingThrowType.Bit27 = (featureBlock.SavingThrowType & Common.Bit27) != 0;
                splFeatureBlock2.SavingThrowType.Bit28 = (featureBlock.SavingThrowType & Common.Bit28) != 0;
                splFeatureBlock2.SavingThrowType.Bit29 = (featureBlock.SavingThrowType & Common.Bit29) != 0;
                splFeatureBlock2.SavingThrowType.Bit30 = (featureBlock.SavingThrowType & Common.Bit30) != 0;
                splFeatureBlock2.SavingThrowType.Bit31 = (featureBlock.SavingThrowType & Common.Bit31) != 0;
                splFeatureBlock2.TargetType = (SpellAbilityTarget)featureBlock.TargetType;
                splFeatureBlock2.TimingMode = (TimingMode)featureBlock.TimingMode;
                splFeatureBlock2.Unused2c = featureBlock.Unused2c;

                splFile.splFeatureBlocks.Add(splFeatureBlock2);
            }

            splFile.Checksum = HashGenerator.GenerateKey(splFile);
            return splFile;
        }
    }
}