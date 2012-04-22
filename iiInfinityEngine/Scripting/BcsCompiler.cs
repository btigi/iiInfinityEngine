using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core
{
    // This rates as one of the top two worst classes I have ever written. Serious refactoring required.
    // And the addion of ActionOverride handling has just made it laughable
    public class BcsCompiler
    {
        private string[] objectIdsLines;
        private string[] alignmenIdsLines;
        private string[] genderIdsLines;
        private string[] specificIdsLines;
        private string[] classIdsLines;
        private string[] raceIdsLines;
        private string[] generalIdsLines;
        private string[] eaIdsLines;
        private string[] triggerIdsLines;
        private string[] actionIdsLines;

        public List<IdsFile> idsFiles { get; set; }

        public void RefreshIdsFiles()
        {
            var objectIds = idsFiles.Where(a => a.Filename.ToLower() == "object.ids").SingleOrDefault();
            var alignmenIds = idsFiles.Where(a => a.Filename.ToLower() == "alignmen.ids").SingleOrDefault();
            var genderIds = idsFiles.Where(a => a.Filename.ToLower() == "gender.ids").SingleOrDefault();
            var specificIds = idsFiles.Where(a => a.Filename.ToLower() == "specific.ids").SingleOrDefault();
            var classIds = idsFiles.Where(a => a.Filename.ToLower() == "class.ids").SingleOrDefault();
            var raceIds = idsFiles.Where(a => a.Filename.ToLower() == "race.ids").SingleOrDefault();
            var generalIds = idsFiles.Where(a => a.Filename.ToLower() == "general.ids").SingleOrDefault();
            var eaIds = idsFiles.Where(a => a.Filename.ToLower() == "ea.ids").SingleOrDefault();
            var triggerIds = idsFiles.Where(a => a.Filename.ToLower() == "trigger.ids").SingleOrDefault();
            var actionIds = idsFiles.Where(a => a.Filename.ToLower() == "action.ids").SingleOrDefault();

            objectIdsLines = objectIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            alignmenIdsLines = alignmenIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            genderIdsLines = genderIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            specificIdsLines = specificIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            classIdsLines = classIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            raceIdsLines = raceIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            generalIdsLines = generalIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            eaIdsLines = eaIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            triggerIdsLines = triggerIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            actionIdsLines = actionIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public BcsCompiler(List<IdsFile> idsFiles)
        {
            this.idsFiles = idsFiles;
            RefreshIdsFiles();
        }

        public List<string> Compile(string filename)
        {
            var outputLines = new List<string>();
            var fileText = File.ReadAllText(filename);

            var startPosition = fileText.IndexOf("/*");
            while (startPosition > -1)
            {
                var endPosition = fileText.IndexOf("*/");
                if (endPosition > -1)
                {
                    fileText = fileText.Remove(startPosition, (endPosition - startPosition) + 2);
                }
                startPosition = fileText.IndexOf("/*");
            }

            if (fileText.IndexOf("/*") > -1)
            {
                throw new Exception("Unbalanced comment");
            }

            var lines = fileText.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            for (int i = 0; i < lines.Count(); i++)
            {
                lines[i] = lines[i].IndexOf("//") > -1 ? lines[i].Substring(0, lines[i].IndexOf("//")) : lines[i];
            }

            lines.RemoveAll(a => a.Trim() == String.Empty);

            var idx = 0;
            outputLines.Add("SC");
            while (idx < lines.Count())
            {
                var thisLine = lines[idx];

                if (thisLine == "IF")
                {
                    outputLines.Add("CR");
                    outputLines.Add("CO");

                    idx++;
                    var triggerLine = String.Empty;
                    while ((lines[idx].Trim() != "THEN") && (idx < lines.Count))
                    {
                        triggerLine = lines[idx];

                        var actualParameters = triggerLine.Substring(triggerLine.IndexOf('(') + 1, triggerLine.Length - triggerLine.IndexOf('(') - 2).Split(',');

                        var triggerName = triggerLine.Substring(0, triggerLine.IndexOf('('));
                        var negated = triggerName.Trim().StartsWith("!");
                        triggerName = triggerName.Trim(new char[] { '!', ' ' });
                        var ids = triggerIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(a.IndexOf(' '), a.IndexOf('(') - a.IndexOf(' ')).Trim() == triggerName.Trim());

                        var output = String.Empty;
                        var @object = "0 0 0 0 0 0 0 0 0 0 0 0 \"\"";
                        foreach (var def in ids)
                        {
                            var triggerId = Convert.ToInt16(def.Substring(0, def.IndexOf(' ')), 16).ToString();
                            var expectedParameters = def.Substring(def.IndexOf('(') + 1, def.Length - def.IndexOf('(') - 2).Split(',');

                            if (expectedParameters.Count() == actualParameters.Count())
                            {
                                var intIdx = 0;
                                var strIdx = 0;
                                var i0 = 0;
                                var i1 = 0;
                                var i2 = 0;
                                var s0 = String.Empty;
                                var s1 = String.Empty;
                                @object = "0 0 0 0 0 0 0 0 0 0 0 0 \"\"";

                                for (int i = 0; i < expectedParameters.Count(); i++)
                                {
                                    if (expectedParameters[i].Length > 0)
                                    {
                                        switch (expectedParameters[i][0])
                                        {
                                            case 'O':
                                                @object = ParseObject(actualParameters[i]);
                                                break;
                                            case 'S':
                                                if (!(actualParameters[i].Trim().StartsWith("\"")) && (actualParameters[i].Trim().EndsWith("\"")))
                                                {
                                                    throw new Exception("Invalid string param");
                                                }
                                                switch (strIdx)
                                                {
                                                    case 0:
                                                        s0 = actualParameters[i].Trim(new char[] { ')', ' ' });
                                                        break;
                                                    case 1:
                                                        s1 = actualParameters[i].Trim(new char[] { ')', ' ' });
                                                        break;
                                                }
                                                strIdx++;
                                                break;
                                            case 'P':
                                                var x = actualParameters[i].Trim(new char[] { '[', ']' });
                                                var coords = x.Split('.');
                                                if (coords.Length != 2)
                                                {
                                                    throw new Exception("Invalid coordinate param");
                                                }
                                                output = output + coords[0] + " " + coords[1];
                                                break;
                                            case 'I':
                                                var temp = 0;
                                                var ap = actualParameters[i].Trim(')', ' ');
                                                if (!Int32.TryParse(ap, out temp))
                                                {
                                                    var boolValidIdsEntry = false;
                                                    var p = expectedParameters[i].Split('*');
                                                    if (p.Count() == 2)
                                                    {
                                                        var thisIds = idsFiles.Where(a => a.Filename.ToLower() == p[1].ToLower() + ".ids").SingleOrDefault();
                                                        if (thisIds != null)
                                                        {
                                                            var thisIdsLines = thisIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                                            var thisIdsLine = thisIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(a.IndexOf(' '), a.Length - a.IndexOf(' ')).ToLower().Trim() == ap.ToLower()).SingleOrDefault();
                                                            if (thisIdsLine != null)
                                                            {
                                                                var thisIdsValue = thisIdsLine.Substring(0, thisIdsLine.IndexOf(' '));
                                                                if (thisIdsValue.IndexOf('x') > -1)
                                                                {
                                                                    temp = Convert.ToInt32(thisIdsValue, 16);
                                                                    boolValidIdsEntry = true;
                                                                }
                                                                else
                                                                {
                                                                    temp = Convert.ToInt32(thisIdsValue);
                                                                    boolValidIdsEntry = true;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    if (!boolValidIdsEntry)
                                                    {
                                                        throw new Exception("Invalid int param");
                                                    }
                                                }
                                                switch (intIdx)
                                                {
                                                    case 0:
                                                        i0 = temp;
                                                        break;
                                                    case 1:
                                                        i1 = temp;
                                                        break;
                                                    case 2:
                                                        i2 = temp;
                                                        break;
                                                }
                                                intIdx++;
                                                break;
                                        }
                                    }
                                }

                                if ((s1 == "\"GLOBAL\"") || (s1 == "\"LOCALS\"") || (IsAreaVariable(s1)))
                                {
                                    if ((triggerId != "16448") && (triggerId != "16566")) // WTF??
                                    {
                                        s0 = s1.Replace("\"", String.Empty) + s0.Replace("\"", String.Empty);
                                        s1 = String.Empty;
                                    }
                                }

                                output = String.Format("{0} {1} {2} {3} {4} {5} {6} OB", triggerId, i0, negated ? "1" : "0", i1, i2, Quote(s0), Quote(s1));
                            }
                        }

                        if (output == String.Empty)
                        {
                            throw new Exception();
                        }

                        outputLines.Add("TR");
                        outputLines.Add(output);
                        outputLines.Add(@object + "OB");
                        outputLines.Add("TR");

                        Trace.WriteLine(triggerLine + " " + ids);
                        idx++;
                    }
                    outputLines.Add("CO");
                    thisLine = "THEN";
                }

                if (thisLine == "THEN")
                {
                    outputLines.Add("RS");
                    outputLines.Add("RE");
                    idx++;

                    var responseLine = lines[idx];
                    var responseWeight = responseLine.Replace("RESPONSE #", String.Empty).Trim();

                    var ActionOverride = false;
                    var @nextobject1 = String.Empty;

                    idx++;
                    var actionLine = String.Empty;
                    while ((lines[idx].Trim() != "END") && (idx < lines.Count))
                    {
                    jmp:

                        if (!ActionOverride)
                        {
                            actionLine = lines[idx];
                        }

                        if (actionLine.Trim().ToUpper().StartsWith("RESPONSE"))
                        {
                            responseWeight = actionLine.Replace("RESPONSE #", String.Empty).Trim();
                            outputLines.Add("RE"); // close existing block
                            outputLines.Add("RE"); // start new block
                            idx++;
                            actionLine = lines[idx];
                        }

                        var actualParameters = actionLine.Substring(actionLine.IndexOf('(') + 1, actionLine.Length - actionLine.IndexOf('(') - 2).Split(',');

                        var actionName = actionLine.Substring(0, actionLine.IndexOf('('));
                        actionName = actionName.Trim();
                        var ids = actionIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(a.IndexOf(' '), a.IndexOf('(') - a.IndexOf(' ')).Trim() == actionName.Trim());

                        var output = String.Empty;
                        var @object1 = "0 0 0 0 0 0 0 0 0 0 0 0 \"\"";
                        var @object2 = "0 0 0 0 0 0 0 0 0 0 0 0 \"\"";
                        var @object3 = "0 0 0 0 0 0 0 0 0 0 0 0 \"\"";
                        foreach (var def in ids)
                        {
                            var actionNumber = def.Substring(0, def.IndexOf(' '));
                            var actionId = actionNumber.IndexOf('x') > -1 ? Convert.ToInt16(def.Substring(0, def.IndexOf(' ')), 16).ToString() : actionNumber.Trim();
                            var expectedParameters = def.Substring(def.IndexOf('(') + 1, def.Length - def.IndexOf('(') - 2).Split(',');

                            if (expectedParameters.Count() == actualParameters.Count())
                            {
                                var intIdx = 0;
                                var strIdx = 0;
                                var objIdx = 0;
                                var i0 = 0;
                                var i1 = 0;
                                var i2 = 0;
                                var c0 = 0;
                                var c1 = 0;
                                var s0 = String.Empty;
                                var s1 = String.Empty;
                                @object1 = "0 0 0 0 0 0 0 0 0 0 0 0 \"\"";
                                @object2 = "0 0 0 0 0 0 0 0 0 0 0 0 \"\"";
                                @object3 = "0 0 0 0 0 0 0 0 0 0 0 0 \"\"";

                                for (int i = 0; i < expectedParameters.Count(); i++)
                                {
                                    if (expectedParameters[i].Length > 0)
                                    {
                                        switch (expectedParameters[i][0])
                                        {
                                            case 'O':
                                                // Note: Yes, it really seems we use the second object for the first object reference and vice-versa...!
                                                switch (objIdx)
                                                {
                                                    case 0:
                                                        if (@nextobject1 == String.Empty)
                                                        {
                                                            @object2 = ParseObject(actualParameters[i]);
                                                        }
                                                        else
                                                        {
                                                            @object1 = nextobject1;
                                                            @object2 = ParseObject(actualParameters[i]);
                                                        }
                                                        break;
                                                    case 1:
                                                        if (@nextobject1 == String.Empty)
                                                        {
                                                            @object1 = ParseObject(actualParameters[i]);
                                                        }
                                                        else
                                                        {
                                                            @object3 = ParseObject(actualParameters[i]);
                                                        }
                                                        break;
                                                    case 2:
                                                        @object3 = ParseObject(actualParameters[i]);
                                                        break;
                                                }
                                                objIdx++;
                                                break;
                                            case 'S':
                                                if (!(actualParameters[i].Trim().StartsWith("\"")) && (actualParameters[i].Trim().EndsWith("\"")))
                                                {
                                                    throw new Exception("Invalid string param");
                                                }
                                                switch (strIdx)
                                                {
                                                    case 0:
                                                        s0 = actualParameters[i].Trim(new char[] { ')', ' ' });
                                                        break;
                                                    case 1:
                                                        s1 = actualParameters[i].Trim(new char[] { ')', ' ' });
                                                        break;
                                                }
                                                strIdx++;
                                                break;
                                            case 'P':
                                                var x = actualParameters[i].Trim(new char[] { '[', ']' });
                                                var coords = x.Split('.');
                                                if (coords.Length != 2)
                                                {
                                                    throw new Exception("Invalid coordinate param");
                                                }
                                                c0 = Convert.ToInt32(coords[0]);
                                                c1 = Convert.ToInt32(coords[1]);
                                                break;
                                            case 'I':
                                                var temp = 0;
                                                var ap = actualParameters[i].Trim(')', ' ');
                                                if (!Int32.TryParse(ap, out temp))
                                                {
                                                    var boolValidIdsEntry = false;
                                                    var p = expectedParameters[i].Split('*');
                                                    if (p.Count() == 2)
                                                    {
                                                        var thisIds = idsFiles.Where(a => a.Filename.ToLower() == p[1].ToLower() + ".ids").SingleOrDefault();
                                                        if (thisIds != null)
                                                        {
                                                            var thisIdsLines = thisIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                                            var thisIdsLine = thisIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(a.IndexOf(' '), a.Length - a.IndexOf(' ')).ToLower().Trim() == ap.ToLower()).SingleOrDefault();
                                                            if (thisIdsLine != null)
                                                            {
                                                                var thisIdsValue = thisIdsLine.Substring(0, thisIdsLine.IndexOf(' '));
                                                                if (thisIdsValue.IndexOf('x') > -1)
                                                                {
                                                                    temp = Convert.ToInt32(thisIdsValue, 16);
                                                                    boolValidIdsEntry = true;
                                                                }
                                                                else
                                                                {
                                                                    temp = Convert.ToInt32(thisIdsValue);
                                                                    boolValidIdsEntry = true;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    if (!boolValidIdsEntry)
                                                    {
                                                        throw new Exception("Invalid int param");
                                                    }
                                                }
                                                switch (intIdx)
                                                {
                                                    case 0:
                                                        i0 = temp;
                                                        break;
                                                    case 1:
                                                        i1 = temp;
                                                        break;
                                                    case 2:
                                                        i2 = temp;
                                                        break;
                                                }
                                                intIdx++;
                                                break;
                                            case 'A':
                                                if (actualParameters[i - 1].StartsWith("\""))
                                                {
                                                    @nextobject1 = "0 0 0 0 0 0 0 0 0 0 0 0 " + actualParameters[i - 1];
                                                }
                                                else
                                                {
                                                    @nextobject1 = "0 0 0 0 0 0 0 0 0 0 0 0 \"" + actualParameters[i - 1] + "\"";
                                                }
                                                actionLine = actualParameters[i];
                                                ActionOverride = true;
                                                goto jmp;
                                        }
                                    }
                                    else
                                    {
                                        if (ActionOverride)
                                        {
                                            @object1 = nextobject1;
                                        }
                                    }
                                    ActionOverride = false;
                                }

                                if ((s1 == "\"GLOBAL\"") || (s1 == "\"LOCALS\"") || (IsAreaVariable(s1)))
                                {
                                    s0 = "\"" + s1.Replace("\"", String.Empty) + s0.Replace("\"", String.Empty) + "\"";
                                    s1 = String.Empty;
                                }

                                output = responseWeight + "AC" + System.Environment.NewLine;
                                output += actionId + "OB" + System.Environment.NewLine;
                                output += @object1 + "OB" + System.Environment.NewLine;
                                output += "OB" + System.Environment.NewLine;
                                output += @object2 + "OB" + System.Environment.NewLine;
                                output += "OB" + System.Environment.NewLine;
                                output += @object3 + "OB" + System.Environment.NewLine;
                                output += String.Format("{0} {1} {2} {3} {4}{5} {6}", i0, c0, c1, i1, i2, Quote(s0), Quote(s1));
                                output += " AC";
                                responseWeight = String.Empty;
                                nextobject1 = String.Empty;
                            }
                        }


                        if (output == String.Empty)
                        {
                            throw new Exception();
                        }

                        outputLines.Add(output);

                        Trace.WriteLine(actionName);

                        idx++;
                    }


                    outputLines.Add("RE");
                    outputLines.Add("RS");
                    outputLines.Add("CR");
                }
                idx++;
            }
            outputLines.Add("SC");


            return outputLines;
        }

        private string ParseObject(string o)
        {
            if (o.StartsWith("\""))
            {
                return "0 0 0 0 0 0 0 0 0 0 0 0 " + o.Trim(')', ' ');
            }

            if (o.StartsWith("["))
            {
                // object spec
                var parsed = o.Trim(new char[] { '[', ']' }).Split('.');
                var eaLine = eaIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(a.IndexOf(' '), a.Length - a.IndexOf(' ')).Trim() == parsed[0].Trim()).SingleOrDefault();
                eaLine = eaLine.IndexOf(' ') > -1 ? eaLine.Substring(0, eaLine.IndexOf(' ')).Trim() : eaLine;
                var generalLine = parsed.Length > 1 ? eaIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(a.IndexOf(' '), a.Length - a.IndexOf(' ')).Trim() == parsed[1].Trim()).SingleOrDefault() : "0";
                generalLine = generalLine.IndexOf(' ') > -1 ? generalLine.Substring(0, generalLine.IndexOf(' ')).Trim() : generalLine;
                var raceLine = parsed.Length > 2 ? eaIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(a.IndexOf(' '), a.Length - a.IndexOf(' ')).Trim() == parsed[2].Trim()).SingleOrDefault() : "0";
                raceLine = raceLine.IndexOf(' ') > -1 ? raceLine.Substring(0, raceLine.IndexOf(' ')).Trim() : raceLine;
                var classLine = parsed.Length > 3 ? eaIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(a.IndexOf(' '), a.Length - a.IndexOf(' ')).Trim() == parsed[3].Trim()).SingleOrDefault() : "0";
                classLine = classLine.IndexOf(' ') > -1 ? classLine.Substring(0, classLine.IndexOf(' ')).Trim() : classLine;
                var specificLine = parsed.Length > 4 ? eaIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(a.IndexOf(' '), a.Length - a.IndexOf(' ')).Trim() == parsed[4].Trim()).SingleOrDefault() : "0";
                specificLine = specificLine.IndexOf(' ') > -1 ? specificLine.Substring(0, specificLine.IndexOf(' ')).Trim() : specificLine;
                var genderLine = parsed.Length > 5 ? eaIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(a.IndexOf(' '), a.Length - a.IndexOf(' ')).Trim() == parsed[5].Trim()).SingleOrDefault() : "0";
                genderLine = genderLine.IndexOf(' ') > -1 ? genderLine.Substring(0, genderLine.IndexOf(' ')).Trim() : genderLine;
                var alignmentLine = parsed.Length > 6 ? eaIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(a.IndexOf(' '), a.Length - a.IndexOf(' ')).Trim() == parsed[6].Trim()).SingleOrDefault() : "0";
                alignmentLine = alignmentLine.IndexOf(' ') > -1 ? alignmentLine.Substring(0, alignmentLine.IndexOf(' ')).Trim() : alignmentLine;

                return String.Format("{0} {1} {2} {3} {4} {5} {6} 0 0 0 0 0 ", eaLine, generalLine, raceLine, classLine, specificLine, genderLine, alignmentLine) + "\"" + "\"";
            }

            // ids identifier
            var objectLine = objectIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(a.IndexOf(' '), a.Length - a.IndexOf(' ')).Trim() == o.Trim()).SingleOrDefault();
            if (objectLine != null)
            {
                return "0 0 0 0 0 0 0 " + objectLine.Substring(0, objectLine.IndexOf(' ')).Trim() + " 0 0 0 0 " + "\"" + "\"";
            }

            return "0 0 0 0 0 0 0 0 0 0 0 0 \"\"";
        }

        private string Quote(string input)
        {
            var data = input.StartsWith("\"") ? input : '"' + input;
            data = input.EndsWith("\"") ? data : data + '"';
            return data;
        }

        private bool IsAreaVariable(string text)
        {
            if (text.ToUpper().StartsWith("\"AR"))
            {
                var text2 = text.Substring(3, text.Length - 3).Trim('"');
                var value = 0;
                return int.TryParse(text2, out value);
            }
            return false;
        }
    }
}