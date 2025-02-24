using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core
{
    public static class Common
    {
        public const int NewString = -1;

        public const Int32 Bit0 = 1;
        public const Int32 Bit1 = 2 << 0;
        public const Int32 Bit2 = 2 << 1;
        public const Int32 Bit3 = 2 << 2;
        public const Int32 Bit4 = 2 << 3;
        public const Int32 Bit5 = 2 << 4;
        public const Int32 Bit6 = 2 << 5;
        public const Int32 Bit7 = 2 << 6;
        public const Int32 Bit8 = 2 << 7;
        public const Int32 Bit9 = 2 << 8;
        public const Int32 Bit10 = 2 << 9;
        public const Int32 Bit11 = 2 << 10;
        public const Int32 Bit12 = 2 << 11;
        public const Int32 Bit13 = 2 << 12;
        public const Int32 Bit14 = 2 << 13;
        public const Int32 Bit15 = 2 << 14;
        public const Int32 Bit16 = 2 << 15;
        public const Int32 Bit17 = 2 << 16;
        public const Int32 Bit18 = 2 << 17;
        public const Int32 Bit19 = 2 << 18;
        public const Int32 Bit20 = 2 << 19;
        public const Int32 Bit21 = 2 << 20;
        public const Int32 Bit22 = 2 << 21;
        public const Int32 Bit23 = 2 << 22;
        public const Int32 Bit24 = 2 << 23;
        public const Int32 Bit25 = 2 << 24;
        public const Int32 Bit26 = 2 << 25;
        public const Int32 Bit27 = 2 << 26;
        public const Int32 Bit28 = 2 << 27;
        public const Int32 Bit29 = 2 << 28;
        public const Int32 Bit30 = 2 << 29;
        public const Int32 Bit31 = 2 << 30;

        public static Object ReadStruct(BinaryReader br, Type t)
        {
            var buff = br.ReadBytes(Marshal.SizeOf(t));
            var handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
            var s = (Object)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), t);
            handle.Free();
            return s;
        }

        public static byte[] WriteStruct(object anything)
        {
            var rawsize = Marshal.SizeOf(anything);
            var buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(anything, buffer, false);
            var rawdatas = new byte[rawsize];
            Marshal.Copy(buffer, rawdatas, 0, rawsize);
            Marshal.FreeHGlobal(buffer);
            return rawdatas;
        }

        public static string TryGetString(char[] chars)
        {
            if ((chars.Length > 0) && (chars[0] != '\0'))
            {
                return new string(chars);
            }
            return String.Empty;
        }

        public static IEString ReadString(Int32 strref, TlkFile tlkFile)
        {
            var stringInfo = new IEString();
            stringInfo.Strref = strref;

            if (tlkFile != null)
            {
                if ((strref <= tlkFile.Strings.Count) && (strref > -1))
                {
                    stringInfo.Flags = tlkFile.Strings[strref].Flags;
                    stringInfo.PitchVariance = tlkFile.Strings[strref].PitchVariance;
                    stringInfo.Sound = tlkFile.Strings[strref].Sound;
                    stringInfo.Text = tlkFile.Strings[strref].Text;
                    stringInfo.VolumeVariance = tlkFile.Strings[strref].VolumeVariance;
                }
            }
            return stringInfo;
        }

        public static int WriteString(IEString stringInfo, TlkFile tlkFile)
        {
            var strref = stringInfo.Strref;
            if (tlkFile != null)
            {
                if ((stringInfo.Strref != Common.NewString) && (stringInfo.Strref <= tlkFile.Strings.Count))
                {
                    tlkFile.Strings[stringInfo.Strref].Flags = stringInfo.Flags;
                    tlkFile.Strings[stringInfo.Strref].PitchVariance = stringInfo.PitchVariance;
                    tlkFile.Strings[stringInfo.Strref].Sound = stringInfo.Sound;
                    tlkFile.Strings[stringInfo.Strref].Text = stringInfo.Text;
                    tlkFile.Strings[stringInfo.Strref].VolumeVariance = stringInfo.VolumeVariance;
                }
                else
                {
                    // Re-use an existing strref if possible (based only on the text)
                    var existingEntry = tlkFile.Strings.Where(a => a.Text == stringInfo.Text).SingleOrDefault();
                    if (existingEntry != null)
                    {
                        strref = existingEntry.Strref;
                    }
                    else
                    {
                        strref = tlkFile.Strings.Count + 1;
                        var stringEntry = new StringEntry();
                        stringEntry.Flags = stringInfo.Flags;
                        stringEntry.PitchVariance = stringInfo.PitchVariance;
                        stringEntry.Sound = stringInfo.Sound;
                        stringEntry.Strref = strref;
                        stringEntry.Text = stringInfo.Text;
                        stringEntry.VolumeVariance = stringInfo.VolumeVariance;
                        tlkFile.Strings.Add(stringEntry);
                    }
                }
            }
            return strref;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array2
    {
        public char character1;
        public char character2;

        public override string ToString()
        {
            return String.Format("{0}{1}", character1, character2);
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array4
    {
        public char character1;
        public char character2;
        public char character3;
        public char character4;

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}", character1, character2, character3, character4);
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array6
    {
        public char character1;
        public char character2;
        public char character3;
        public char character4;
        public char character5;
        public char character6;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array8
    {
        public array8(string value)
        {
            if ((value ?? String.Empty).Length > 8)
            {
                throw new ArgumentException(value);
            }
            character1 = (value ?? String.Empty).Length >= 1 ? value[0] : '\0';
            character2 = (value ?? String.Empty).Length >= 2 ? value[1] : '\0';
            character3 = (value ?? String.Empty).Length >= 3 ? value[2] : '\0';
            character4 = (value ?? String.Empty).Length >= 4 ? value[3] : '\0';
            character5 = (value ?? String.Empty).Length >= 5 ? value[4] : '\0';
            character6 = (value ?? String.Empty).Length >= 6 ? value[5] : '\0';
            character7 = (value ?? String.Empty).Length >= 7 ? value[6] : '\0';
            character8 = (value ?? String.Empty).Length >= 8 ? value[7] : '\0';
        }

        public char character1;
        public char character2;
        public char character3;
        public char character4;
        public char character5;
        public char character6;
        public char character7;
        public char character8;

        public override string ToString()
        {
            var x = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", character1, character2, character3, character4, character5, character6, character7, character8);
            x = x.Substring(0, x.IndexOf('\0') >= 0 ? x.IndexOf('\0') : x.Length);
            return x;
        }

        public static bool IsNullOrEmpty(array8? value)
        {
            var x = value?.ToString();
            return x == null || x?.Length == 0;
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array24
    {
        public byte byte00;
        public byte byte01;
        public byte byte02;
        public byte byte03;
        public byte byte04;
        public byte byte05;
        public byte byte06;
        public byte byte07;
        public byte byte08;
        public byte byte09;
        public byte byte10;
        public byte byte11;
        public byte byte12;
        public byte byte13;
        public byte byte14;
        public byte byte15;
        public byte byte16;
        public byte byte17;
        public byte byte18;
        public byte byte19;
        public byte byte20;
        public byte byte21;
        public byte byte22;
        public byte byte23;

        public override string ToString()
        {
            var x = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}",
                byte00, byte01, byte02, byte03, byte04, byte05, byte06, byte07, byte08, byte09, byte10, byte11,
                byte12, byte13, byte14, byte15, byte16, byte17, byte18, byte19, byte20, byte21, byte22, byte23);
            //x = x.Replace("\0", "");
            return x;
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array32
    {
        public array32(string value)
        {
            if ((value ?? String.Empty).Length > 32)
            {
                throw new ArgumentException(value);
            }
            character01 = (value ?? String.Empty).Length >= 1 ? value[0] : '\0';
            character02 = (value ?? String.Empty).Length >= 2 ? value[1] : '\0';
            character03 = (value ?? String.Empty).Length >= 3 ? value[2] : '\0';
            character04 = (value ?? String.Empty).Length >= 4 ? value[3] : '\0';
            character05 = (value ?? String.Empty).Length >= 5 ? value[4] : '\0';
            character06 = (value ?? String.Empty).Length >= 6 ? value[5] : '\0';
            character07 = (value ?? String.Empty).Length >= 7 ? value[6] : '\0';
            character08 = (value ?? String.Empty).Length >= 8 ? value[7] : '\0';
            character09 = (value ?? String.Empty).Length >= 9 ? value[8] : '\0';
            character10 = (value ?? String.Empty).Length >= 10 ? value[9] : '\0';
            character11 = (value ?? String.Empty).Length >= 11 ? value[10] : '\0';
            character12 = (value ?? String.Empty).Length >= 12 ? value[11] : '\0';
            character13 = (value ?? String.Empty).Length >= 13 ? value[12] : '\0';
            character14 = (value ?? String.Empty).Length >= 14 ? value[13] : '\0';
            character15 = (value ?? String.Empty).Length >= 15 ? value[14] : '\0';
            character16 = (value ?? String.Empty).Length >= 16 ? value[15] : '\0';
            character17 = (value ?? String.Empty).Length >= 17 ? value[16] : '\0';
            character18 = (value ?? String.Empty).Length >= 18 ? value[17] : '\0';
            character19 = (value ?? String.Empty).Length >= 19 ? value[18] : '\0';
            character20 = (value ?? String.Empty).Length >= 20 ? value[29] : '\0';
            character21 = (value ?? String.Empty).Length >= 21 ? value[20] : '\0';
            character22 = (value ?? String.Empty).Length >= 22 ? value[21] : '\0';
            character23 = (value ?? String.Empty).Length >= 23 ? value[22] : '\0';
            character24 = (value ?? String.Empty).Length >= 24 ? value[23] : '\0';
            character25 = (value ?? String.Empty).Length >= 25 ? value[24] : '\0';
            character26 = (value ?? String.Empty).Length >= 26 ? value[25] : '\0';
            character27 = (value ?? String.Empty).Length >= 27 ? value[26] : '\0';
            character28 = (value ?? String.Empty).Length >= 28 ? value[27] : '\0';
            character29 = (value ?? String.Empty).Length >= 29 ? value[28] : '\0';
            character30 = (value ?? String.Empty).Length >= 30 ? value[29] : '\0';
            character31 = (value ?? String.Empty).Length >= 31 ? value[30] : '\0';
            character32 = (value ?? String.Empty).Length >= 32 ? value[31] : '\0';
        }

        public char character01;
        public char character02;
        public char character03;
        public char character04;
        public char character05;
        public char character06;
        public char character07;
        public char character08;
        public char character09;
        public char character10;
        public char character11;
        public char character12;
        public char character13;
        public char character14;
        public char character15;
        public char character16;
        public char character17;
        public char character18;
        public char character19;
        public char character20;
        public char character21;
        public char character22;
        public char character23;
        public char character24;
        public char character25;
        public char character26;
        public char character27;
        public char character28;
        public char character29;
        public char character30;
        public char character31;
        public char character32;

        public override string ToString()
        {
            var x = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}",
                character01, character02, character03, character04, character05, character06, character07, character08, character09, character10, character11,
                character12, character13, character14, character15, character16, character17, character18, character19, character20, character21, character22, character23,
                character24, character25, character26, character27, character28, character29, character30, character31, character32);
            //x = x.Replace("\0", "");
            return x;
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array34
    {
        public byte byte00;
        public byte byte01;
        public byte byte02;
        public byte byte03;
        public byte byte04;
        public byte byte05;
        public byte byte06;
        public byte byte07;
        public byte byte08;
        public byte byte09;
        public byte byte10;
        public byte byte11;
        public byte byte12;
        public byte byte13;
        public byte byte14;
        public byte byte15;
        public byte byte16;
        public byte byte17;
        public byte byte18;
        public byte byte19;
        public byte byte20;
        public byte byte21;
        public byte byte22;
        public byte byte23;
        public byte byte24;
        public byte byte25;
        public byte byte26;
        public byte byte27;
        public byte byte28;
        public byte byte29;
        public byte byte30;
        public byte byte31;
        public byte byte32;
        public byte byte33;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array36
    {
        public byte byte00;
        public byte byte01;
        public byte byte02;
        public byte byte03;
        public byte byte04;
        public byte byte05;
        public byte byte06;
        public byte byte07;
        public byte byte08;
        public byte byte09;
        public byte byte10;
        public byte byte11;
        public byte byte12;
        public byte byte13;
        public byte byte14;
        public byte byte15;
        public byte byte16;
        public byte byte17;
        public byte byte18;
        public byte byte19;
        public byte byte20;
        public byte byte21;
        public byte byte22;
        public byte byte23;
        public byte byte24;
        public byte byte25;
        public byte byte26;
        public byte byte27;
        public byte byte28;
        public byte byte29;
        public byte byte30;
        public byte byte31;
        public byte byte32;
        public byte byte33;
        public byte byte34;
        public byte byte35;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array38
    {
        public byte byte00;
        public byte byte01;
        public byte byte02;
        public byte byte03;
        public byte byte04;
        public byte byte05;
        public byte byte06;
        public byte byte07;
        public byte byte08;
        public byte byte09;
        public byte byte10;
        public byte byte11;
        public byte byte12;
        public byte byte13;
        public byte byte14;
        public byte byte15;
        public byte byte16;
        public byte byte17;
        public byte byte18;
        public byte byte19;
        public byte byte20;
        public byte byte21;
        public byte byte22;
        public byte byte23;
        public byte byte24;
        public byte byte25;
        public byte byte26;
        public byte byte27;
        public byte byte28;
        public byte byte29;
        public byte byte30;
        public byte byte31;
        public byte byte32;
        public byte byte33;
        public byte byte34;
        public byte byte35;
        public byte byte36;
        public byte byte37;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array40
    {
        public byte byte00;
        public byte byte01;
        public byte byte02;
        public byte byte03;
        public byte byte04;
        public byte byte05;
        public byte byte06;
        public byte byte07;
        public byte byte08;
        public byte byte09;
        public byte byte10;
        public byte byte11;
        public byte byte12;
        public byte byte13;
        public byte byte14;
        public byte byte15;
        public byte byte16;
        public byte byte17;
        public byte byte18;
        public byte byte19;
        public byte byte20;
        public byte byte21;
        public byte byte22;
        public byte byte23;
        public byte byte24;
        public byte byte25;
        public byte byte26;
        public byte byte27;
        public byte byte28;
        public byte byte29;
        public byte byte30;
        public byte byte31;
        public byte byte32;
        public byte byte33;
        public byte byte34;
        public byte byte35;
        public byte byte36;
        public byte byte37;
        public byte byte38;
        public byte byte39;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array48
    {
        public byte byte00;
        public byte byte01;
        public byte byte02;
        public byte byte03;
        public byte byte04;
        public byte byte05;
        public byte byte06;
        public byte byte07;
        public byte byte08;
        public byte byte09;
        public byte byte10;
        public byte byte11;
        public byte byte12;
        public byte byte13;
        public byte byte14;
        public byte byte15;
        public byte byte16;
        public byte byte17;
        public byte byte18;
        public byte byte19;
        public byte byte20;
        public byte byte21;
        public byte byte22;
        public byte byte23;
        public byte byte24;
        public byte byte25;
        public byte byte26;
        public byte byte27;
        public byte byte28;
        public byte byte29;
        public byte byte30;
        public byte byte31;
        public byte byte32;
        public byte byte33;
        public byte byte34;
        public byte byte35;
        public byte byte36;
        public byte byte37;
        public byte byte38;
        public byte byte39;
        public byte byte40;
        public byte byte41;
        public byte byte42;
        public byte byte43;
        public byte byte44;
        public byte byte45;
        public byte byte46;
        public byte byte47;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array56
    {
        public byte byte00;
        public byte byte01;
        public byte byte02;
        public byte byte03;
        public byte byte04;
        public byte byte05;
        public byte byte06;
        public byte byte07;
        public byte byte08;
        public byte byte09;
        public byte byte10;
        public byte byte11;
        public byte byte12;
        public byte byte13;
        public byte byte14;
        public byte byte15;
        public byte byte16;
        public byte byte17;
        public byte byte18;
        public byte byte19;
        public byte byte20;
        public byte byte21;
        public byte byte22;
        public byte byte23;
        public byte byte24;
        public byte byte25;
        public byte byte26;
        public byte byte27;
        public byte byte28;
        public byte byte29;
        public byte byte30;
        public byte byte31;
        public byte byte32;
        public byte byte33;
        public byte byte34;
        public byte byte35;
        public byte byte36;
        public byte byte37;
        public byte byte38;
        public byte byte39;
        public byte byte40;
        public byte byte41;
        public byte byte42;
        public byte byte43;
        public byte byte44;
        public byte byte45;
        public byte byte46;
        public byte byte47;
        public byte byte48;
        public byte byte49;
        public byte byte50;
        public byte byte51;
        public byte byte52;
        public byte byte53;
        public byte byte54;
        public byte byte55;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array60
    {
        public byte byte00;
        public byte byte01;
        public byte byte02;
        public byte byte03;
        public byte byte04;
        public byte byte05;
        public byte byte06;
        public byte byte07;
        public byte byte08;
        public byte byte09;
        public byte byte10;
        public byte byte11;
        public byte byte12;
        public byte byte13;
        public byte byte14;
        public byte byte15;
        public byte byte16;
        public byte byte17;
        public byte byte18;
        public byte byte19;
        public byte byte20;
        public byte byte21;
        public byte byte22;
        public byte byte23;
        public byte byte24;
        public byte byte25;
        public byte byte26;
        public byte byte27;
        public byte byte28;
        public byte byte29;
        public byte byte30;
        public byte byte31;
        public byte byte32;
        public byte byte33;
        public byte byte34;
        public byte byte35;
        public byte byte36;
        public byte byte37;
        public byte byte38;
        public byte byte39;
        public byte byte40;
        public byte byte41;
        public byte byte42;
        public byte byte43;
        public byte byte44;
        public byte byte45;
        public byte byte46;
        public byte byte47;
        public byte byte48;
        public byte byte49;
        public byte byte50;
        public byte byte51;
        public byte byte52;
        public byte byte53;
        public byte byte54;
        public byte byte55;
        public byte byte56;
        public byte byte57;
        public byte byte58;
        public byte byte59;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array64
    {
        public byte byte00;
        public byte byte01;
        public byte byte02;
        public byte byte03;
        public byte byte04;
        public byte byte05;
        public byte byte06;
        public byte byte07;
        public byte byte08;
        public byte byte09;
        public byte byte10;
        public byte byte11;
        public byte byte12;
        public byte byte13;
        public byte byte14;
        public byte byte15;
        public byte byte16;
        public byte byte17;
        public byte byte18;
        public byte byte19;
        public byte byte20;
        public byte byte21;
        public byte byte22;
        public byte byte23;
        public byte byte24;
        public byte byte25;
        public byte byte26;
        public byte byte27;
        public byte byte28;
        public byte byte29;
        public byte byte30;
        public byte byte31;
        public byte byte32;
        public byte byte33;
        public byte byte34;
        public byte byte35;
        public byte byte36;
        public byte byte37;
        public byte byte38;
        public byte byte39;
        public byte byte40;
        public byte byte41;
        public byte byte42;
        public byte byte43;
        public byte byte44;
        public byte byte45;
        public byte byte46;
        public byte byte47;
        public byte byte48;
        public byte byte49;
        public byte byte50;
        public byte byte51;
        public byte byte52;
        public byte byte53;
        public byte byte54;
        public byte byte55;
        public byte byte56;
        public byte byte57;
        public byte byte58;
        public byte byte59;
        public byte byte60;
        public byte byte61;
        public byte byte62;
        public byte byte63;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array66
    {
        public byte byte00;
        public byte byte01;
        public byte byte02;
        public byte byte03;
        public byte byte04;
        public byte byte05;
        public byte byte06;
        public byte byte07;
        public byte byte08;
        public byte byte09;
        public byte byte10;
        public byte byte11;
        public byte byte12;
        public byte byte13;
        public byte byte14;
        public byte byte15;
        public byte byte16;
        public byte byte17;
        public byte byte18;
        public byte byte19;
        public byte byte20;
        public byte byte21;
        public byte byte22;
        public byte byte23;
        public byte byte24;
        public byte byte25;
        public byte byte26;
        public byte byte27;
        public byte byte28;
        public byte byte29;
        public byte byte30;
        public byte byte31;
        public byte byte32;
        public byte byte33;
        public byte byte34;
        public byte byte35;
        public byte byte36;
        public byte byte37;
        public byte byte38;
        public byte byte39;
        public byte byte40;
        public byte byte41;
        public byte byte42;
        public byte byte43;
        public byte byte44;
        public byte byte45;
        public byte byte46;
        public byte byte47;
        public byte byte48;
        public byte byte49;
        public byte byte50;
        public byte byte51;
        public byte byte52;
        public byte byte53;
        public byte byte54;
        public byte byte55;
        public byte byte56;
        public byte byte57;
        public byte byte58;
        public byte byte59;
        public byte byte60;
        public byte byte61;
        public byte byte62;
        public byte byte63;
        public byte byte64;
        public byte byte65;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array124
    {
        public byte byte000;
        public byte byte001;
        public byte byte002;
        public byte byte003;
        public byte byte004;
        public byte byte005;
        public byte byte006;
        public byte byte007;
        public byte byte008;
        public byte byte009;
        public byte byte010;
        public byte byte011;
        public byte byte012;
        public byte byte013;
        public byte byte014;
        public byte byte015;
        public byte byte016;
        public byte byte017;
        public byte byte018;
        public byte byte019;
        public byte byte020;
        public byte byte021;
        public byte byte022;
        public byte byte023;
        public byte byte024;
        public byte byte025;
        public byte byte026;
        public byte byte027;
        public byte byte028;
        public byte byte029;
        public byte byte030;
        public byte byte031;
        public byte byte032;
        public byte byte033;
        public byte byte034;
        public byte byte035;
        public byte byte036;
        public byte byte037;
        public byte byte038;
        public byte byte039;
        public byte byte040;
        public byte byte041;
        public byte byte042;
        public byte byte043;
        public byte byte044;
        public byte byte045;
        public byte byte046;
        public byte byte047;
        public byte byte048;
        public byte byte049;
        public byte byte050;
        public byte byte051;
        public byte byte052;
        public byte byte053;
        public byte byte054;
        public byte byte055;
        public byte byte056;
        public byte byte057;
        public byte byte058;
        public byte byte059;
        public byte byte060;
        public byte byte061;
        public byte byte062;
        public byte byte063;
        public byte byte064;
        public byte byte065;
        public byte byte066;
        public byte byte067;
        public byte byte068;
        public byte byte069;
        public byte byte070;
        public byte byte071;
        public byte byte072;
        public byte byte073;
        public byte byte074;
        public byte byte075;
        public byte byte076;
        public byte byte077;
        public byte byte078;
        public byte byte079;
        public byte byte080;
        public byte byte081;
        public byte byte082;
        public byte byte083;
        public byte byte084;
        public byte byte085;
        public byte byte086;
        public byte byte087;
        public byte byte088;
        public byte byte089;
        public byte byte090;
        public byte byte091;
        public byte byte092;
        public byte byte093;
        public byte byte094;
        public byte byte095;
        public byte byte096;
        public byte byte097;
        public byte byte098;
        public byte byte099;
        public byte byte100;
        public byte byte101;
        public byte byte102;
        public byte byte103;
        public byte byte104;
        public byte byte105;
        public byte byte106;
        public byte byte107;
        public byte byte108;
        public byte byte109;
        public byte byte110;
        public byte byte111;
        public byte byte112;
        public byte byte113;
        public byte byte114;
        public byte byte115;
        public byte byte116;
        public byte byte117;
        public byte byte118;
        public byte byte119;
        public byte byte120;
        public byte byte121;
        public byte byte122;
        public byte byte123;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array128
    {
        public byte byte000;
        public byte byte001;
        public byte byte002;
        public byte byte003;
        public byte byte004;
        public byte byte005;
        public byte byte006;
        public byte byte007;
        public byte byte008;
        public byte byte009;
        public byte byte010;
        public byte byte011;
        public byte byte012;
        public byte byte013;
        public byte byte014;
        public byte byte015;
        public byte byte016;
        public byte byte017;
        public byte byte018;
        public byte byte019;
        public byte byte020;
        public byte byte021;
        public byte byte022;
        public byte byte023;
        public byte byte024;
        public byte byte025;
        public byte byte026;
        public byte byte027;
        public byte byte028;
        public byte byte029;
        public byte byte030;
        public byte byte031;
        public byte byte032;
        public byte byte033;
        public byte byte034;
        public byte byte035;
        public byte byte036;
        public byte byte037;
        public byte byte038;
        public byte byte039;
        public byte byte040;
        public byte byte041;
        public byte byte042;
        public byte byte043;
        public byte byte044;
        public byte byte045;
        public byte byte046;
        public byte byte047;
        public byte byte048;
        public byte byte049;
        public byte byte050;
        public byte byte051;
        public byte byte052;
        public byte byte053;
        public byte byte054;
        public byte byte055;
        public byte byte056;
        public byte byte057;
        public byte byte058;
        public byte byte059;
        public byte byte060;
        public byte byte061;
        public byte byte062;
        public byte byte063;
        public byte byte064;
        public byte byte065;
        public byte byte066;
        public byte byte067;
        public byte byte068;
        public byte byte069;
        public byte byte070;
        public byte byte071;
        public byte byte072;
        public byte byte073;
        public byte byte074;
        public byte byte075;
        public byte byte076;
        public byte byte077;
        public byte byte078;
        public byte byte079;
        public byte byte080;
        public byte byte081;
        public byte byte082;
        public byte byte083;
        public byte byte084;
        public byte byte085;
        public byte byte086;
        public byte byte087;
        public byte byte088;
        public byte byte089;
        public byte byte090;
        public byte byte091;
        public byte byte092;
        public byte byte093;
        public byte byte094;
        public byte byte095;
        public byte byte096;
        public byte byte097;
        public byte byte098;
        public byte byte099;
        public byte byte100;
        public byte byte101;
        public byte byte102;
        public byte byte103;
        public byte byte104;
        public byte byte105;
        public byte byte106;
        public byte byte107;
        public byte byte108;
        public byte byte109;
        public byte byte110;
        public byte byte111;
        public byte byte112;
        public byte byte113;
        public byte byte114;
        public byte byte115;
        public byte byte116;
        public byte byte117;
        public byte byte118;
        public byte byte119;
        public byte byte120;
        public byte byte121;
        public byte byte122;
        public byte byte123;
        public byte byte124;
        public byte byte125;
        public byte byte126;
        public byte byte127;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array156
    {
        public byte byte000;
        public byte byte001;
        public byte byte002;
        public byte byte003;
        public byte byte004;
        public byte byte005;
        public byte byte006;
        public byte byte007;
        public byte byte008;
        public byte byte009;
        public byte byte010;
        public byte byte011;
        public byte byte012;
        public byte byte013;
        public byte byte014;
        public byte byte015;
        public byte byte016;
        public byte byte017;
        public byte byte018;
        public byte byte019;
        public byte byte020;
        public byte byte021;
        public byte byte022;
        public byte byte023;
        public byte byte024;
        public byte byte025;
        public byte byte026;
        public byte byte027;
        public byte byte028;
        public byte byte029;
        public byte byte030;
        public byte byte031;
        public byte byte032;
        public byte byte033;
        public byte byte034;
        public byte byte035;
        public byte byte036;
        public byte byte037;
        public byte byte038;
        public byte byte039;
        public byte byte040;
        public byte byte041;
        public byte byte042;
        public byte byte043;
        public byte byte044;
        public byte byte045;
        public byte byte046;
        public byte byte047;
        public byte byte048;
        public byte byte049;
        public byte byte050;
        public byte byte051;
        public byte byte052;
        public byte byte053;
        public byte byte054;
        public byte byte055;
        public byte byte056;
        public byte byte057;
        public byte byte058;
        public byte byte059;
        public byte byte060;
        public byte byte061;
        public byte byte062;
        public byte byte063;
        public byte byte064;
        public byte byte065;
        public byte byte066;
        public byte byte067;
        public byte byte068;
        public byte byte069;
        public byte byte070;
        public byte byte071;
        public byte byte072;
        public byte byte073;
        public byte byte074;
        public byte byte075;
        public byte byte076;
        public byte byte077;
        public byte byte078;
        public byte byte079;
        public byte byte080;
        public byte byte081;
        public byte byte082;
        public byte byte083;
        public byte byte084;
        public byte byte085;
        public byte byte086;
        public byte byte087;
        public byte byte088;
        public byte byte089;
        public byte byte090;
        public byte byte091;
        public byte byte092;
        public byte byte093;
        public byte byte094;
        public byte byte095;
        public byte byte096;
        public byte byte097;
        public byte byte098;
        public byte byte099;
        public byte byte100;
        public byte byte101;
        public byte byte102;
        public byte byte103;
        public byte byte104;
        public byte byte105;
        public byte byte106;
        public byte byte107;
        public byte byte108;
        public byte byte109;
        public byte byte110;
        public byte byte111;
        public byte byte112;
        public byte byte113;
        public byte byte114;
        public byte byte115;
        public byte byte116;
        public byte byte117;
        public byte byte118;
        public byte byte119;
        public byte byte120;
        public byte byte121;
        public byte byte122;
        public byte byte123;
        public byte byte124;
        public byte byte125;
        public byte byte126;
        public byte byte127;
        public byte byte128;
        public byte byte129;
        public byte byte130;
        public byte byte131;
        public byte byte132;
        public byte byte133;
        public byte byte134;
        public byte byte135;
        public byte byte136;
        public byte byte137;
        public byte byte138;
        public byte byte139;
        public byte byte140;
        public byte byte141;
        public byte byte142;
        public byte byte143;
        public byte byte144;
        public byte byte145;
        public byte byte146;
        public byte byte147;
        public byte byte148;
        public byte byte149;
        public byte byte150;
        public byte byte151;
        public byte byte152;
        public byte byte153;
        public byte byte154;
        public byte byte155;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array168
    {
        public byte byte000;
        public byte byte001;
        public byte byte002;
        public byte byte003;
        public byte byte004;
        public byte byte005;
        public byte byte006;
        public byte byte007;
        public byte byte008;
        public byte byte009;
        public byte byte010;
        public byte byte011;
        public byte byte012;
        public byte byte013;
        public byte byte014;
        public byte byte015;
        public byte byte016;
        public byte byte017;
        public byte byte018;
        public byte byte019;
        public byte byte020;
        public byte byte021;
        public byte byte022;
        public byte byte023;
        public byte byte024;
        public byte byte025;
        public byte byte026;
        public byte byte027;
        public byte byte028;
        public byte byte029;
        public byte byte030;
        public byte byte031;
        public byte byte032;
        public byte byte033;
        public byte byte034;
        public byte byte035;
        public byte byte036;
        public byte byte037;
        public byte byte038;
        public byte byte039;
        public byte byte040;
        public byte byte041;
        public byte byte042;
        public byte byte043;
        public byte byte044;
        public byte byte045;
        public byte byte046;
        public byte byte047;
        public byte byte048;
        public byte byte049;
        public byte byte050;
        public byte byte051;
        public byte byte052;
        public byte byte053;
        public byte byte054;
        public byte byte055;
        public byte byte056;
        public byte byte057;
        public byte byte058;
        public byte byte059;
        public byte byte060;
        public byte byte061;
        public byte byte062;
        public byte byte063;
        public byte byte064;
        public byte byte065;
        public byte byte066;
        public byte byte067;
        public byte byte068;
        public byte byte069;
        public byte byte070;
        public byte byte071;
        public byte byte072;
        public byte byte073;
        public byte byte074;
        public byte byte075;
        public byte byte076;
        public byte byte077;
        public byte byte078;
        public byte byte079;
        public byte byte080;
        public byte byte081;
        public byte byte082;
        public byte byte083;
        public byte byte084;
        public byte byte085;
        public byte byte086;
        public byte byte087;
        public byte byte088;
        public byte byte089;
        public byte byte090;
        public byte byte091;
        public byte byte092;
        public byte byte093;
        public byte byte094;
        public byte byte095;
        public byte byte096;
        public byte byte097;
        public byte byte098;
        public byte byte099;
        public byte byte100;
        public byte byte101;
        public byte byte102;
        public byte byte103;
        public byte byte104;
        public byte byte105;
        public byte byte106;
        public byte byte107;
        public byte byte108;
        public byte byte109;
        public byte byte110;
        public byte byte111;
        public byte byte112;
        public byte byte113;
        public byte byte114;
        public byte byte115;
        public byte byte116;
        public byte byte117;
        public byte byte118;
        public byte byte119;
        public byte byte120;
        public byte byte121;
        public byte byte122;
        public byte byte123;
        public byte byte124;
        public byte byte125;
        public byte byte126;
        public byte byte127;
        public byte byte128;
        public byte byte129;
        public byte byte130;
        public byte byte131;
        public byte byte132;
        public byte byte133;
        public byte byte134;
        public byte byte135;
        public byte byte136;
        public byte byte137;
        public byte byte138;
        public byte byte139;
        public byte byte140;
        public byte byte141;
        public byte byte142;
        public byte byte143;
        public byte byte144;
        public byte byte145;
        public byte byte146;
        public byte byte147;
        public byte byte148;
        public byte byte149;
        public byte byte150;
        public byte byte151;
        public byte byte152;
        public byte byte153;
        public byte byte154;
        public byte byte155;
        public byte byte156;
        public byte byte157;
        public byte byte158;
        public byte byte159;
        public byte byte160;
        public byte byte161;
        public byte byte162;
        public byte byte163;
        public byte byte164;
        public byte byte165;
        public byte byte166;
        public byte byte167;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array214
    {
        public byte byte000;
        public byte byte001;
        public byte byte002;
        public byte byte003;
        public byte byte004;
        public byte byte005;
        public byte byte006;
        public byte byte007;
        public byte byte008;
        public byte byte009;
        public byte byte010;
        public byte byte011;
        public byte byte012;
        public byte byte013;
        public byte byte014;
        public byte byte015;
        public byte byte016;
        public byte byte017;
        public byte byte018;
        public byte byte019;
        public byte byte020;
        public byte byte021;
        public byte byte022;
        public byte byte023;
        public byte byte024;
        public byte byte025;
        public byte byte026;
        public byte byte027;
        public byte byte028;
        public byte byte029;
        public byte byte030;
        public byte byte031;
        public byte byte032;
        public byte byte033;
        public byte byte034;
        public byte byte035;
        public byte byte036;
        public byte byte037;
        public byte byte038;
        public byte byte039;
        public byte byte040;
        public byte byte041;
        public byte byte042;
        public byte byte043;
        public byte byte044;
        public byte byte045;
        public byte byte046;
        public byte byte047;
        public byte byte048;
        public byte byte049;
        public byte byte050;
        public byte byte051;
        public byte byte052;
        public byte byte053;
        public byte byte054;
        public byte byte055;
        public byte byte056;
        public byte byte057;
        public byte byte058;
        public byte byte059;
        public byte byte060;
        public byte byte061;
        public byte byte062;
        public byte byte063;
        public byte byte064;
        public byte byte065;
        public byte byte066;
        public byte byte067;
        public byte byte068;
        public byte byte069;
        public byte byte070;
        public byte byte071;
        public byte byte072;
        public byte byte073;
        public byte byte074;
        public byte byte075;
        public byte byte076;
        public byte byte077;
        public byte byte078;
        public byte byte079;
        public byte byte080;
        public byte byte081;
        public byte byte082;
        public byte byte083;
        public byte byte084;
        public byte byte085;
        public byte byte086;
        public byte byte087;
        public byte byte088;
        public byte byte089;
        public byte byte090;
        public byte byte091;
        public byte byte092;
        public byte byte093;
        public byte byte094;
        public byte byte095;
        public byte byte096;
        public byte byte097;
        public byte byte098;
        public byte byte099;
        public byte byte100;
        public byte byte101;
        public byte byte102;
        public byte byte103;
        public byte byte104;
        public byte byte105;
        public byte byte106;
        public byte byte107;
        public byte byte108;
        public byte byte109;
        public byte byte110;
        public byte byte111;
        public byte byte112;
        public byte byte113;
        public byte byte114;
        public byte byte115;
        public byte byte116;
        public byte byte117;
        public byte byte118;
        public byte byte119;
        public byte byte120;
        public byte byte121;
        public byte byte122;
        public byte byte123;
        public byte byte124;
        public byte byte125;
        public byte byte126;
        public byte byte127;
        public byte byte128;
        public byte byte129;
        public byte byte130;
        public byte byte131;
        public byte byte132;
        public byte byte133;
        public byte byte134;
        public byte byte135;
        public byte byte136;
        public byte byte137;
        public byte byte138;
        public byte byte139;
        public byte byte140;
        public byte byte141;
        public byte byte142;
        public byte byte143;
        public byte byte144;
        public byte byte145;
        public byte byte146;
        public byte byte147;
        public byte byte148;
        public byte byte149;
        public byte byte150;
        public byte byte151;
        public byte byte152;
        public byte byte153;
        public byte byte154;
        public byte byte155;
        public byte byte156;
        public byte byte157;
        public byte byte158;
        public byte byte159;
        public byte byte160;
        public byte byte161;
        public byte byte162;
        public byte byte163;
        public byte byte164;
        public byte byte165;
        public byte byte166;
        public byte byte167;
        public byte byte168;
        public byte byte169;
        public byte byte170;
        public byte byte171;
        public byte byte172;
        public byte byte173;
        public byte byte174;
        public byte byte175;
        public byte byte176;
        public byte byte177;
        public byte byte178;
        public byte byte179;
        public byte byte180;
        public byte byte181;
        public byte byte182;
        public byte byte183;
        public byte byte184;
        public byte byte185;
        public byte byte186;
        public byte byte187;
        public byte byte188;
        public byte byte189;
        public byte byte190;
        public byte byte191;
        public byte byte192;
        public byte byte193;
        public byte byte194;
        public byte byte195;
        public byte byte196;
        public byte byte197;
        public byte byte198;
        public byte byte199;
        public byte byte200;
        public byte byte201;
        public byte byte202;
        public byte byte203;
        public byte byte204;
        public byte byte205;
        public byte byte206;
        public byte byte207;
        public byte byte208;
        public byte byte209;
        public byte byte210;
        public byte byte211;
        public byte byte212;
        public byte byte213;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array240
    {
        public byte byte000;
        public byte byte001;
        public byte byte002;
        public byte byte003;
        public byte byte004;
        public byte byte005;
        public byte byte006;
        public byte byte007;
        public byte byte008;
        public byte byte009;
        public byte byte010;
        public byte byte011;
        public byte byte012;
        public byte byte013;
        public byte byte014;
        public byte byte015;
        public byte byte016;
        public byte byte017;
        public byte byte018;
        public byte byte019;
        public byte byte020;
        public byte byte021;
        public byte byte022;
        public byte byte023;
        public byte byte024;
        public byte byte025;
        public byte byte026;
        public byte byte027;
        public byte byte028;
        public byte byte029;
        public byte byte030;
        public byte byte031;
        public byte byte032;
        public byte byte033;
        public byte byte034;
        public byte byte035;
        public byte byte036;
        public byte byte037;
        public byte byte038;
        public byte byte039;
        public byte byte040;
        public byte byte041;
        public byte byte042;
        public byte byte043;
        public byte byte044;
        public byte byte045;
        public byte byte046;
        public byte byte047;
        public byte byte048;
        public byte byte049;
        public byte byte050;
        public byte byte051;
        public byte byte052;
        public byte byte053;
        public byte byte054;
        public byte byte055;
        public byte byte056;
        public byte byte057;
        public byte byte058;
        public byte byte059;
        public byte byte060;
        public byte byte061;
        public byte byte062;
        public byte byte063;
        public byte byte064;
        public byte byte065;
        public byte byte066;
        public byte byte067;
        public byte byte068;
        public byte byte069;
        public byte byte070;
        public byte byte071;
        public byte byte072;
        public byte byte073;
        public byte byte074;
        public byte byte075;
        public byte byte076;
        public byte byte077;
        public byte byte078;
        public byte byte079;
        public byte byte080;
        public byte byte081;
        public byte byte082;
        public byte byte083;
        public byte byte084;
        public byte byte085;
        public byte byte086;
        public byte byte087;
        public byte byte088;
        public byte byte089;
        public byte byte090;
        public byte byte091;
        public byte byte092;
        public byte byte093;
        public byte byte094;
        public byte byte095;
        public byte byte096;
        public byte byte097;
        public byte byte098;
        public byte byte099;
        public byte byte100;
        public byte byte101;
        public byte byte102;
        public byte byte103;
        public byte byte104;
        public byte byte105;
        public byte byte106;
        public byte byte107;
        public byte byte108;
        public byte byte109;
        public byte byte110;
        public byte byte111;
        public byte byte112;
        public byte byte113;
        public byte byte114;
        public byte byte115;
        public byte byte116;
        public byte byte117;
        public byte byte118;
        public byte byte119;
        public byte byte120;
        public byte byte121;
        public byte byte122;
        public byte byte123;
        public byte byte124;
        public byte byte125;
        public byte byte126;
        public byte byte127;
        public byte byte128;
        public byte byte129;
        public byte byte130;
        public byte byte131;
        public byte byte132;
        public byte byte133;
        public byte byte134;
        public byte byte135;
        public byte byte136;
        public byte byte137;
        public byte byte138;
        public byte byte139;
        public byte byte140;
        public byte byte141;
        public byte byte142;
        public byte byte143;
        public byte byte144;
        public byte byte145;
        public byte byte146;
        public byte byte147;
        public byte byte148;
        public byte byte149;
        public byte byte150;
        public byte byte151;
        public byte byte152;
        public byte byte153;
        public byte byte154;
        public byte byte155;
        public byte byte156;
        public byte byte157;
        public byte byte158;
        public byte byte159;
        public byte byte160;
        public byte byte161;
        public byte byte162;
        public byte byte163;
        public byte byte164;
        public byte byte165;
        public byte byte166;
        public byte byte167;
        public byte byte168;
        public byte byte169;
        public byte byte170;
        public byte byte171;
        public byte byte172;
        public byte byte173;
        public byte byte174;
        public byte byte175;
        public byte byte176;
        public byte byte177;
        public byte byte178;
        public byte byte179;
        public byte byte180;
        public byte byte181;
        public byte byte182;
        public byte byte183;
        public byte byte184;
        public byte byte185;
        public byte byte186;
        public byte byte187;
        public byte byte188;
        public byte byte189;
        public byte byte190;
        public byte byte191;
        public byte byte192;
        public byte byte193;
        public byte byte194;
        public byte byte195;
        public byte byte196;
        public byte byte197;
        public byte byte198;
        public byte byte199;
        public byte byte200;
        public byte byte201;
        public byte byte202;
        public byte byte203;
        public byte byte204;
        public byte byte205;
        public byte byte206;
        public byte byte207;
        public byte byte208;
        public byte byte209;
        public byte byte210;
        public byte byte211;
        public byte byte212;
        public byte byte213;
        public byte byte214;
        public byte byte215;
        public byte byte216;
        public byte byte217;
        public byte byte218;
        public byte byte219;
        public byte byte220;
        public byte byte221;
        public byte byte222;
        public byte byte223;
        public byte byte224;
        public byte byte225;
        public byte byte226;
        public byte byte227;
        public byte byte228;
        public byte byte229;
        public byte byte230;
        public byte byte231;
        public byte byte232;
        public byte byte233;
        public byte byte234;
        public byte byte235;
        public byte byte236;
        public byte byte237;
        public byte byte238;
        public byte byte239;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array180
    {
        public byte byte000;
        public byte byte001;
        public byte byte002;
        public byte byte003;
        public byte byte004;
        public byte byte005;
        public byte byte006;
        public byte byte007;
        public byte byte008;
        public byte byte009;
        public byte byte010;
        public byte byte011;
        public byte byte012;
        public byte byte013;
        public byte byte014;
        public byte byte015;
        public byte byte016;
        public byte byte017;
        public byte byte018;
        public byte byte019;
        public byte byte020;
        public byte byte021;
        public byte byte022;
        public byte byte023;
        public byte byte024;
        public byte byte025;
        public byte byte026;
        public byte byte027;
        public byte byte028;
        public byte byte029;
        public byte byte030;
        public byte byte031;
        public byte byte032;
        public byte byte033;
        public byte byte034;
        public byte byte035;
        public byte byte036;
        public byte byte037;
        public byte byte038;
        public byte byte039;
        public byte byte040;
        public byte byte041;
        public byte byte042;
        public byte byte043;
        public byte byte044;
        public byte byte045;
        public byte byte046;
        public byte byte047;
        public byte byte048;
        public byte byte049;
        public byte byte050;
        public byte byte051;
        public byte byte052;
        public byte byte053;
        public byte byte054;
        public byte byte055;
        public byte byte056;
        public byte byte057;
        public byte byte058;
        public byte byte059;
        public byte byte060;
        public byte byte061;
        public byte byte062;
        public byte byte063;
        public byte byte064;
        public byte byte065;
        public byte byte066;
        public byte byte067;
        public byte byte068;
        public byte byte069;
        public byte byte070;
        public byte byte071;
        public byte byte072;
        public byte byte073;
        public byte byte074;
        public byte byte075;
        public byte byte076;
        public byte byte077;
        public byte byte078;
        public byte byte079;
        public byte byte080;
        public byte byte081;
        public byte byte082;
        public byte byte083;
        public byte byte084;
        public byte byte085;
        public byte byte086;
        public byte byte087;
        public byte byte088;
        public byte byte089;
        public byte byte090;
        public byte byte091;
        public byte byte092;
        public byte byte093;
        public byte byte094;
        public byte byte095;
        public byte byte096;
        public byte byte097;
        public byte byte098;
        public byte byte099;
        public byte byte100;
        public byte byte101;
        public byte byte102;
        public byte byte103;
        public byte byte104;
        public byte byte105;
        public byte byte106;
        public byte byte107;
        public byte byte108;
        public byte byte109;
        public byte byte110;
        public byte byte111;
        public byte byte112;
        public byte byte113;
        public byte byte114;
        public byte byte115;
        public byte byte116;
        public byte byte117;
        public byte byte118;
        public byte byte119;
        public byte byte120;
        public byte byte121;
        public byte byte122;
        public byte byte123;
        public byte byte124;
        public byte byte125;
        public byte byte126;
        public byte byte127;
        public byte byte128;
        public byte byte129;
        public byte byte130;
        public byte byte131;
        public byte byte132;
        public byte byte133;
        public byte byte134;
        public byte byte135;
        public byte byte136;
        public byte byte137;
        public byte byte138;
        public byte byte139;
        public byte byte140;
        public byte byte141;
        public byte byte142;
        public byte byte143;
        public byte byte144;
        public byte byte145;
        public byte byte146;
        public byte byte147;
        public byte byte148;
        public byte byte149;
        public byte byte150;
        public byte byte151;
        public byte byte152;
        public byte byte153;
        public byte byte154;
        public byte byte155;
        public byte byte156;
        public byte byte157;
        public byte byte158;
        public byte byte159;
        public byte byte160;
        public byte byte161;
        public byte byte162;
        public byte byte163;
        public byte byte164;
        public byte byte165;
        public byte byte166;
        public byte byte167;
        public byte byte168;
        public byte byte169;
        public byte byte170;
        public byte byte171;
        public byte byte172;
        public byte byte173;
        public byte byte174;
        public byte byte175;
        public byte byte176;
        public byte byte177;
        public byte byte178;
        public byte byte179;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct array336
    {
        public byte byte000;
        public byte byte001;
        public byte byte002;
        public byte byte003;
        public byte byte004;
        public byte byte005;
        public byte byte006;
        public byte byte007;
        public byte byte008;
        public byte byte009;
        public byte byte010;
        public byte byte011;
        public byte byte012;
        public byte byte013;
        public byte byte014;
        public byte byte015;
        public byte byte016;
        public byte byte017;
        public byte byte018;
        public byte byte019;
        public byte byte020;
        public byte byte021;
        public byte byte022;
        public byte byte023;
        public byte byte024;
        public byte byte025;
        public byte byte026;
        public byte byte027;
        public byte byte028;
        public byte byte029;
        public byte byte030;
        public byte byte031;
        public byte byte032;
        public byte byte033;
        public byte byte034;
        public byte byte035;
        public byte byte036;
        public byte byte037;
        public byte byte038;
        public byte byte039;
        public byte byte040;
        public byte byte041;
        public byte byte042;
        public byte byte043;
        public byte byte044;
        public byte byte045;
        public byte byte046;
        public byte byte047;
        public byte byte048;
        public byte byte049;
        public byte byte050;
        public byte byte051;
        public byte byte052;
        public byte byte053;
        public byte byte054;
        public byte byte055;
        public byte byte056;
        public byte byte057;
        public byte byte058;
        public byte byte059;
        public byte byte060;
        public byte byte061;
        public byte byte062;
        public byte byte063;
        public byte byte064;
        public byte byte065;
        public byte byte066;
        public byte byte067;
        public byte byte068;
        public byte byte069;
        public byte byte070;
        public byte byte071;
        public byte byte072;
        public byte byte073;
        public byte byte074;
        public byte byte075;
        public byte byte076;
        public byte byte077;
        public byte byte078;
        public byte byte079;
        public byte byte080;
        public byte byte081;
        public byte byte082;
        public byte byte083;
        public byte byte084;
        public byte byte085;
        public byte byte086;
        public byte byte087;
        public byte byte088;
        public byte byte089;
        public byte byte090;
        public byte byte091;
        public byte byte092;
        public byte byte093;
        public byte byte094;
        public byte byte095;
        public byte byte096;
        public byte byte097;
        public byte byte098;
        public byte byte099;
        public byte byte100;
        public byte byte101;
        public byte byte102;
        public byte byte103;
        public byte byte104;
        public byte byte105;
        public byte byte106;
        public byte byte107;
        public byte byte108;
        public byte byte109;
        public byte byte110;
        public byte byte111;
        public byte byte112;
        public byte byte113;
        public byte byte114;
        public byte byte115;
        public byte byte116;
        public byte byte117;
        public byte byte118;
        public byte byte119;
        public byte byte120;
        public byte byte121;
        public byte byte122;
        public byte byte123;
        public byte byte124;
        public byte byte125;
        public byte byte126;
        public byte byte127;
        public byte byte128;
        public byte byte129;
        public byte byte130;
        public byte byte131;
        public byte byte132;
        public byte byte133;
        public byte byte134;
        public byte byte135;
        public byte byte136;
        public byte byte137;
        public byte byte138;
        public byte byte139;
        public byte byte140;
        public byte byte141;
        public byte byte142;
        public byte byte143;
        public byte byte144;
        public byte byte145;
        public byte byte146;
        public byte byte147;
        public byte byte148;
        public byte byte149;
        public byte byte150;
        public byte byte151;
        public byte byte152;
        public byte byte153;
        public byte byte154;
        public byte byte155;
        public byte byte156;
        public byte byte157;
        public byte byte158;
        public byte byte159;
        public byte byte160;
        public byte byte161;
        public byte byte162;
        public byte byte163;
        public byte byte164;
        public byte byte165;
        public byte byte166;
        public byte byte167;
        public byte byte168;
        public byte byte169;
        public byte byte170;
        public byte byte171;
        public byte byte172;
        public byte byte173;
        public byte byte174;
        public byte byte175;
        public byte byte176;
        public byte byte177;
        public byte byte178;
        public byte byte179;
        public byte byte180;
        public byte byte181;
        public byte byte182;
        public byte byte183;
        public byte byte184;
        public byte byte185;
        public byte byte186;
        public byte byte187;
        public byte byte188;
        public byte byte189;
        public byte byte190;
        public byte byte191;
        public byte byte192;
        public byte byte193;
        public byte byte194;
        public byte byte195;
        public byte byte196;
        public byte byte197;
        public byte byte198;
        public byte byte199;
        public byte byte200;
        public byte byte201;
        public byte byte202;
        public byte byte203;
        public byte byte204;
        public byte byte205;
        public byte byte206;
        public byte byte207;
        public byte byte208;
        public byte byte209;
        public byte byte210;
        public byte byte211;
        public byte byte212;
        public byte byte213;
        public byte byte214;
        public byte byte215;
        public byte byte216;
        public byte byte217;
        public byte byte218;
        public byte byte219;
        public byte byte220;
        public byte byte221;
        public byte byte222;
        public byte byte223;
        public byte byte224;
        public byte byte225;
        public byte byte226;
        public byte byte227;
        public byte byte228;
        public byte byte229;
        public byte byte230;
        public byte byte231;
        public byte byte232;
        public byte byte233;
        public byte byte234;
        public byte byte235;
        public byte byte236;
        public byte byte237;
        public byte byte238;
        public byte byte239;
        public byte byte240;
        public byte byte241;
        public byte byte242;
        public byte byte243;
        public byte byte244;
        public byte byte245;
        public byte byte246;
        public byte byte247;
        public byte byte248;
        public byte byte249;
        public byte byte250;
        public byte byte251;
        public byte byte252;
        public byte byte253;
        public byte byte254;
        public byte byte255;
        public byte byte256;
        public byte byte257;
        public byte byte258;
        public byte byte259;
        public byte byte260;
        public byte byte261;
        public byte byte262;
        public byte byte263;
        public byte byte264;
        public byte byte265;
        public byte byte266;
        public byte byte267;
        public byte byte268;
        public byte byte269;
        public byte byte270;
        public byte byte271;
        public byte byte272;
        public byte byte273;
        public byte byte274;
        public byte byte275;
        public byte byte276;
        public byte byte277;
        public byte byte278;
        public byte byte279;
        public byte byte280;
        public byte byte281;
        public byte byte282;
        public byte byte283;
        public byte byte284;
        public byte byte285;
        public byte byte286;
        public byte byte287;
        public byte byte288;
        public byte byte289;
        public byte byte290;
        public byte byte291;
        public byte byte292;
        public byte byte293;
        public byte byte294;
        public byte byte295;
        public byte byte296;
        public byte byte297;
        public byte byte298;
        public byte byte299;
        public byte byte300;
        public byte byte301;
        public byte byte302;
        public byte byte303;
        public byte byte304;
        public byte byte305;
        public byte byte306;
        public byte byte307;
        public byte byte308;
        public byte byte309;
        public byte byte310;
        public byte byte311;
        public byte byte312;
        public byte byte313;
        public byte byte314;
        public byte byte315;
        public byte byte316;
        public byte byte317;
        public byte byte318;
        public byte byte319;
        public byte byte320;
        public byte byte321;
        public byte byte322;
        public byte byte323;
        public byte byte324;
        public byte byte325;
        public byte byte326;
        public byte byte327;
        public byte byte328;
        public byte byte329;
        public byte byte330;
        public byte byte331;
        public byte byte332;
        public byte byte333;
        public byte byte334;
        public byte byte335;
    }

    public enum ItemType
    {
        Books = 0,
        AmuletsNecklaces = 1,
        Armor = 2,
        BeltsGirdles = 3,
        Boots = 4,
        Arrows = 5,
        BracersGauntlets = 6,
        HelmsHats = 7,
        Keys = 8,
        Potions = 9,
        Rings = 10,
        Scrolls = 11,
        Shields = 12,
        Food = 13,
        Bullets = 14,
        Bows = 15,
        Daggers = 16,
        Maces = 17,
        Slings = 18,
        ShortSwords = 19,
        LongsSwords = 20,
        Hammers = 21,
        MorningStars = 22,
        Flails = 23,
        Darts = 24,
        Axes = 25,
        Quarterstaffs = 26,
        Crossbows = 27,
        HandToHand = 28,
        Spears = 29,
        Halberds = 30,
        Bolts = 31,
        CloaksRobes = 32,
        Gold = 33,
        Gems = 34,
        Wands = 35,
        Bag_Eye_BrokenArmor = 36,
        BrokenShield_Bracelet = 37,
        BrokwnSword_Earring = 38,
        Tatoo = 39,
        Lens = 40,
        Buckler_Teeth = 41,
        Candle = 42,
        Unknown43 = 43,
        Club = 44,
        Unknown45 = 45,
        Unknown46 = 46,
        LargeShield = 47,
        Unknown48 = 48,
        MediumShield = 49,
        Notes = 50,
        Unknown51 = 51,
        Unknown52 = 52,
        SmallShield = 53,
        Unknown54 = 54,
        Telescope = 55,
        Drink = 56,
        GreatSword = 57,
        Container = 58,
        Fur_Pelt = 59,
        LeatherArmor = 60,
        StuddedLeatherArmor = 61,
        ChainMail = 62,
        SplintMail = 63,
        HalfPlate = 64,
        FullPlate = 65,
        HideArmor = 66,
        Robe = 67,
        Unknown68 = 68,
        BastardSword = 69,
        Scarf = 70,
        Food2 = 71,
        Hat = 72,
        Gauntlet = 73,
    }

    public enum IEFileType
    {
        Unknown,
        Bmp = 0x1,
        Mve = 0x2,
        Wav = 0x4,
        Wfx = 0x5,
        Plt = 0x6,
        Bam = 0x03e8,
        BamC = 0x03e8,
        Wed = 0x03e9,
        Chu = 0x03ea,
        Tis = 0x03eb,
        Mos = 0x03ec,
        MosC = 0x03ec,
        Itm = 0x03ed,
        Spl = 0x03ee,
        Bcs = 0x03ef,
        Ids = 0x03f0,
        Cre = 0x03f1,
        Are = 0x03f2,
        Dlg = 0x03f3,
        DimensionalArray = 0x03f4,
        Gam = 0x03f5,
        Sto = 0x03f6,
        Wmp = 0x03f7,
        Eff = 0x03f8,
        Bs = 0x03f9,
        Chr = 0x03fa,
        Vvc = 0x03fb,
        Vef = 0x03fc,
        Pro = 0x03fd,
        Bio = 0x03fe,
        Ba = 0x044c,
        Ini = 0x0802,
        Scr = 0x0803,
        Bif = 0x5000,  // custom
        Key = 0x5001,  // custom
        Tlk = 0x5002  // custom
    }

    [Serializable]
    public struct IEString
    {
        public IEString()
        {
            Flags = new StringEntryType();
        }

        public string Text { get; set; }
        public StringEntryType Flags { get; set; }
        public array8 Sound { get; set; }
        public Int32 VolumeVariance { get; set; }
        public Int32 PitchVariance { get; set; }
        public Int32 Strref { get; set; }
    }

    [Serializable]
    public class StringEntryType
    {
        public bool HasText { get; set; }
        public bool HasSound { get; set; }
        public bool HasToken { get; set; }
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
    public class SavingThrowType
    {
        public bool Spells { get; set; }
        public bool Breath { get; set; }
        public bool ParalyzePoisonDeath { get; set; }
        public bool Wands { get; set; }
        public bool PetrifyPolymorph { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
        public bool Bit8 { get; set; }
        public bool Bit9 { get; set; }
        public bool IgnorePrimaryTarget { get; set; }
        public bool IgnoreSecondaryTarget { get; set; }
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
        public bool BypassMirrorImage { get; set; }
        public bool IgnoreDifficulty { get; set; }
        public bool Bit26 { get; set; }
        public bool Bit27 { get; set; }
        public bool Bit28 { get; set; }
        public bool Bit29 { get; set; }
        public bool Bit30 { get; set; }
        public bool Bit31 { get; set; }
    }

    [Serializable]
    public class SpellFlags
    {
        public bool Bit0 { get; set; }
        public bool Bit1 { get; set; }
        public bool Bit2 { get; set; }
        public bool Bit3 { get; set; }
        public bool Bit4 { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
        public bool Bit8 { get; set; }
        public bool BreaksSanctuaryInvisibility { get; set; }
        public bool Hostile { get; set; }
        public bool NoLOSRequired { get; set; }
        public bool AllowSpotting { get; set; }
        public bool OutdoorsOnly { get; set; }
        public bool IgnoreWildSurgeDeadMagic { get; set; }
        public bool IgnoreWildSurge { get; set; }
        public bool NonCombatAbility { get; set; }
        public bool Bit17 { get; set; }
        public bool Bit18 { get; set; }
        public bool Bit19 { get; set; }
        public bool Bit20 { get; set; }
        public bool Bit21 { get; set; }
        public bool Bit22 { get; set; }
        public bool Bit23 { get; set; }
        public bool CanTargetInvisible { get; set; }
        public bool CastableWhenSilenced { get; set; }
        public bool Bit26 { get; set; }
        public bool Bit27 { get; set; }
        public bool Bit28 { get; set; }
        public bool Bit29 { get; set; }
        public bool Bit30 { get; set; }
        public bool Bit31 { get; set; }
    }

    [Serializable]
    public class Resistance
    {
        public bool DispellableAffectedByMagicResistance { get; set; }
        public bool IgnoreMagicResistance { get; set; }
        public bool Bit2 { get; set; }
        public bool Bit3 { get; set; }
        public bool Bit4 { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
    }

    public enum EffTargetType
    {
        None = 0,
        Self,
        ProjectileTarget,
        Party,
        Everyone,
        EveryoneExceptParty,
        CasterGroup,
        TargetGroup,
        EveryoneExceptSelf,
        OriginalCaster
    }
}