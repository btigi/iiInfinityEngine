using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core
{
    public enum OverrideBehaviour
    {
        UseFirst,
        UseLast
    }

    // This rates as one of the top two worst classes I have ever written. Serious refactoring required.
    public class BcsDecompiler
    {
        private string[] objectIdsLines;
        private string[] alignmenIdsLines;
        private string[] genderIdsLines;
        private string[] specificIdsLines;
        private string[] classIdsLines;
        private string[] raceIdsLines;
        private string[] generalIdsLines;
        private string[] eaIdsLines;

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

            objectIdsLines = objectIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            alignmenIdsLines = alignmenIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            genderIdsLines = genderIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            specificIdsLines = specificIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            classIdsLines = classIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            raceIdsLines = raceIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            generalIdsLines = generalIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            eaIdsLines = eaIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public BcsDecompiler(List<IdsFile> idsFiles)
        {
            this.idsFiles = idsFiles;
            RefreshIdsFiles();
        }

        public List<string> Decompile(string filename, OverrideBehaviour overrideBehaviour = OverrideBehaviour.UseLast)
        {
            var lines = File.ReadAllLines(filename);
            var output = new List<string>();

            var idx = 1;
            var parsingScriptBlock = false;
            var parsingTriggerConditions = false;
            var parsingTrigger = false;
            var parsingResponseSet = false;
            var parsingAction = false;
            var nextStringUsage = String.Empty;
            var ORIndent = String.Empty;
            var ORCount = 0;
            if (lines.First() == "SC" && lines.Last() == "SC")
            {
                while (idx < lines.Count())
                {
                    var thisLine = lines[idx];

                    if (thisLine == "CR")
                    {
                        parsingScriptBlock = !parsingScriptBlock;
                        if (!parsingScriptBlock)
                        {
                            output.Add("END");
                            output.Add(String.Empty);
                        }
                    }

                    if (thisLine == "CO")
                    {
                        parsingTriggerConditions = !parsingTriggerConditions;
                        if (parsingTriggerConditions)
                        {
                            output.Add("IF");
                        }
                        else
                        {
                            output.Add("THEN");
                        }
                    }

                    if (thisLine == "TR")
                    {
                        parsingTrigger = !parsingTrigger;
                        if (parsingTrigger)
                        {
                            idx++;
                            var triggerLine = lines[idx];
                            idx++;
                            var objectLine = lines[idx];

                            var triggerIds = idsFiles.Where(a => a.Filename.ToLower() == "trigger.ids").Single();
                            var triggerLines = triggerIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                            var intIdx = 0;
                            var stringIdx = 0;
                            var triggerInfo = triggerLine.Split(' ');
                            var triggerIdDecimal = Convert.ToInt32(triggerInfo[0]);
                            var triggerIdHex = triggerIdDecimal.ToString("X");

                            var triggerDefinition = String.Empty;

                            //Note: Trigger IDS enties are assumed to be hex. BCS trigger references are assumed to be decimal
                            switch (overrideBehaviour)
                            {
                                case OverrideBehaviour.UseFirst:
                                    triggerDefinition = triggerLines.Where(a => a.IndexOf(' ') > 0 && Convert.ToInt32(a.Substring(0, a.IndexOf(' ')), 16) == Convert.ToInt32(triggerInfo[0])).First();
                                    break;
                                case OverrideBehaviour.UseLast:
                                    triggerDefinition = triggerLines.Where(a => a.IndexOf(' ') > 0 && Convert.ToInt32(a.Substring(0, a.IndexOf(' ')), 16) == Convert.ToInt32(triggerInfo[0])).Last();
                                    break;
                            }

                            var triggerName = triggerDefinition.Substring(triggerDefinition.IndexOf(' '), triggerDefinition.IndexOf('(') - triggerDefinition.IndexOf(' ')).Trim();
                            var parameters = triggerDefinition.Substring(triggerDefinition.IndexOf("(") + 1, triggerDefinition.Length - triggerDefinition.IndexOf("(") - 2).Split(',');

                            var outputLine = triggerName + "(";
                            if (parameters.Length > 0)
                            {
                                for (int i = 0; i < parameters.Length; i++)
                                {
                                    if (parameters[i] != String.Empty)
                                    {
                                        switch (parameters[i][0])
                                        {
                                            case 'I':
                                                var components = parameters[i].Split('*');
                                                if (components.Count() == 2)
                                                {
                                                    var ids = idsFiles.Where(a => a.Filename.ToLower() == String.Format("{0}.ids", components[1].ToLower())).SingleOrDefault();
                                                    switch (intIdx)
                                                    {
                                                        case 0:
                                                            if (ids != null)
                                                            {
                                                                var idsLines = ids.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                                                //TODO: Duplicate this 'check as hex if required' check elsewhere?
                                                                var text = idsLines.Where(a => a.IndexOf(' ') > 0 && (a.Substring(0, a.IndexOf(' ')).IndexOf('x') > 0 ? Convert.ToString(Convert.ToInt32(a.Substring(0, a.IndexOf(' ')), 16)) : a.Substring(0, a.IndexOf(' '))) == triggerInfo[1]).SingleOrDefault();
                                                                outputLine = outputLine + (text == null ? triggerInfo[1] : text.Substring(text.IndexOf(' '), text.Length - text.IndexOf(' ')).Trim()) + ',';
                                                            }
                                                            else
                                                            {
                                                                outputLine = outputLine + triggerInfo[1] + ',';
                                                                //Special handling for OR formatting
                                                                if (triggerName.ToUpper() == "OR")
                                                                {
                                                                    ORCount = Convert.ToInt32(triggerInfo[1]) + 2; // One for this trigger, and one because our count is 1-indexed
                                                                    ORIndent = "  ";
                                                                }
                                                            }
                                                            break;
                                                        case 1:
                                                            if (ids != null)
                                                            {
                                                                var idsLines = ids.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                                                var text = idsLines.Where(a => a.IndexOf(' ') > 0 && (a.Substring(0, a.IndexOf(' ')).IndexOf('x') > 0 ? Convert.ToString(Convert.ToInt32(a.Substring(0, a.IndexOf(' ')), 16)) : a.Substring(0, a.IndexOf(' '))) == triggerInfo[3]).SingleOrDefault();
                                                                outputLine = outputLine + (text == null ? triggerInfo[3] : text.Substring(text.IndexOf(' '), text.Length - text.IndexOf(' ')).Trim()) + ',';
                                                            }
                                                            else
                                                            {
                                                                outputLine = outputLine + triggerInfo[3] + ',';
                                                            }
                                                            break;
                                                        case 2:
                                                            if (ids != null)
                                                            {
                                                                var idsLines = ids.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                                                var text = idsLines.Where(a => a.IndexOf(' ') > 0 && (a.Substring(0, a.IndexOf(' ')).IndexOf('x') > 0 ? Convert.ToString(Convert.ToInt32(a.Substring(0, a.IndexOf(' ')), 16)) : a.Substring(0, a.IndexOf(' '))) == triggerInfo[4]).SingleOrDefault();
                                                                outputLine = outputLine + (text == null ? triggerInfo[4] : text.Substring(text.IndexOf(' '), text.Length - text.IndexOf(' ')).Trim()) + ',';
                                                            }
                                                            else
                                                            {
                                                                outputLine = outputLine + triggerInfo[4] + ',';
                                                            }
                                                            break;
                                                    }
                                                }
                                                intIdx++;
                                                break;
                                            case 'O':
                                                var objectResult = ParseObject(objectLine);
                                                outputLine = outputLine + objectResult + ',';
                                                break;
                                            case 'S':
                                                var parsedVariable = String.Empty;
                                                switch (stringIdx)
                                                {
                                                    case 0:
                                                        parsedVariable = triggerInfo[5];
                                                        if (triggerInfo[5].StartsWith("GLOBAL") && (triggerInfo[6] != "GLOBAL"))
                                                        {
                                                            parsedVariable = '"' + triggerInfo[5].Substring(6, triggerInfo[5].Length - 6);
                                                            nextStringUsage = "GLOBAL";
                                                        }
                                                        if (triggerInfo[5].StartsWith("LOCALS") && (triggerInfo[6] != "LOCALS"))
                                                        {
                                                            parsedVariable = '"' + triggerInfo[5].Substring(6, triggerInfo[5].Length - 6);
                                                            nextStringUsage = "LOCALS";
                                                        }

                                                        if (triggerInfo[5].StartsWith("\"GLOBAL") && (triggerInfo[6] != "\"GLOBAL"))
                                                        {
                                                            parsedVariable = '"' + triggerInfo[5].Substring(7, triggerInfo[5].Length - 7);
                                                            nextStringUsage = "\"GLOBAL\"";
                                                        }
                                                        if (triggerInfo[5].StartsWith("\"LOCALS") && (triggerInfo[5] != "\"LOCALS"))
                                                        {
                                                            parsedVariable = '"' + triggerInfo[5].Substring(7, triggerInfo[5].Length - 7);
                                                            nextStringUsage = "\"LOCALS\"";
                                                        }

                                                        if ((IsAreaVariable(triggerInfo[5])))
                                                        {
                                                            parsedVariable = '"' + triggerInfo[5].Substring(7, triggerInfo[5].Length - 7);
                                                            nextStringUsage = '"' + GetAreaVariable(triggerInfo[5]) + '"';
                                                        }

                                                        if (i == parameters.Length - 1)
                                                        {
                                                            if (nextStringUsage != String.Empty)
                                                            {
                                                                outputLine = outputLine + nextStringUsage + ',';
                                                            }
                                                            else
                                                            {
                                                                outputLine = outputLine + triggerInfo[5] + ',';
                                                            }
                                                            nextStringUsage = String.Empty;
                                                        }
                                                        else
                                                        {
                                                            outputLine = outputLine + parsedVariable + ',';
                                                        }
                                                        break;
                                                    case 1:
                                                        if (nextStringUsage != String.Empty)
                                                        {
                                                            outputLine = outputLine + nextStringUsage + ',';
                                                            nextStringUsage = String.Empty;
                                                            break;
                                                        }

                                                        if (triggerInfo[6].StartsWith("GLOBAL") && (triggerInfo[6] != "GLOBAL"))
                                                        {
                                                            parsedVariable = '"' + triggerInfo[6].Substring(6, triggerInfo[6].Length - 6);
                                                            nextStringUsage = "GLOBAL";
                                                        }
                                                        if (triggerInfo[6].StartsWith("LOCALS") && (triggerInfo[6] != "LOCALS"))
                                                        {
                                                            parsedVariable = '"' + triggerInfo[6].Substring(6, triggerInfo[6].Length - 6);
                                                            nextStringUsage = "LOCALS";
                                                        }

                                                        parsedVariable = triggerInfo[6];
                                                        if (triggerInfo[6].StartsWith("\"GLOBAL") && (triggerInfo[6] != "\"GLOBAL\""))
                                                        {
                                                            parsedVariable = '"' + triggerInfo[6].Substring(7, triggerInfo[6].Length - 7);
                                                            nextStringUsage = "\"GLOBAL\"";
                                                        }
                                                        if (triggerInfo[6].StartsWith("\"LOCALS") && (triggerInfo[6] != "\"LOCALS\""))
                                                        {
                                                            parsedVariable = '"' + triggerInfo[6].Substring(7, triggerInfo[6].Length - 7);
                                                            nextStringUsage = "\"LOCALS\"";
                                                        }

                                                        if ((IsAreaVariable(triggerInfo[6])))
                                                        {
                                                            parsedVariable = '"' + triggerInfo[5].Substring(7, triggerInfo[5].Length - 7);
                                                            nextStringUsage = '"' + GetAreaVariable(triggerInfo[6]) + '"';
                                                        }

                                                        if (i == parameters.Length - 1)
                                                        {
                                                            if (nextStringUsage != String.Empty)
                                                            {
                                                                outputLine = outputLine + nextStringUsage + ',';
                                                            }
                                                            else
                                                            {
                                                                outputLine = outputLine + triggerInfo[6] + ',';
                                                            }
                                                            nextStringUsage = String.Empty;
                                                        }
                                                        else
                                                        {
                                                            outputLine = outputLine + parsedVariable + ',';
                                                        }
                                                        break;
                                                }
                                                stringIdx++;
                                                break;
                                        }
                                    }
                                }
                            }
                            if (outputLine.EndsWith(","))
                            {
                                outputLine = outputLine.Substring(0, outputLine.Length - 1);
                            }
                            outputLine = outputLine + ")";

                            ORCount -= 1;
                            if (ORCount <= 0)
                            {
                                ORIndent = String.Empty;
                            }

                            var negation = triggerInfo[2] == "1" ? "!" : String.Empty;
                            output.Add((triggerName.ToUpper() == "OR" ? String.Empty : ORIndent) + "  " + negation + outputLine);
                        }
                    }

                    if (thisLine == "RE")
                    {
                        parsingResponseSet = !parsingResponseSet;
                        if (parsingResponseSet)
                        {
                            idx++;
                            var weightLine = lines[idx];
                            var weight = weightLine.Substring(0, weightLine.Length - 2);
                            output.Add("  " + String.Format("RESPONSE #{0}", weight));
                            parsingAction = true;
                        }
                    }

                    if (thisLine == "AC")
                    {
                        parsingAction = true;
                    }

                    if (parsingAction)
                    {
                        idx++;
                        var actionLine = lines[idx];

                        idx++;
                        var objectLine1 = lines[idx];
                        idx++;
                        idx++;
                        var objectLine2 = lines[idx];
                        idx++;
                        idx++;
                        var objectLine3 = lines[idx];
                        idx++;
                        var paramLine = lines[idx];


                        var actionIds = idsFiles.Where(a => a.Filename.ToLower() == "action.ids").Single();
                        var actionLines = actionIds.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                        var intIdx = 0;
                        var stringIdx = 0;
                        var objectIdx = 0;
                        var actionInfo = paramLine.Split(' ', '"');
                        var actionIdDecimal = Convert.ToInt32(actionLine.Substring(0, actionLine.Length - 2));

                        var actionDefinition = String.Empty;
                        switch (overrideBehaviour)
                        {
                            case OverrideBehaviour.UseFirst:
                                actionDefinition = actionLines.Where(a => (a.IndexOf(' ') > 0) && a.Substring(0, a.IndexOf(' ')).Trim() == actionIdDecimal.ToString()).First();
                                break;
                            case OverrideBehaviour.UseLast:
                                actionDefinition = actionLines.Where(a => (a.IndexOf(' ') > 0) && a.Substring(0, a.IndexOf(' ')).Trim() == actionIdDecimal.ToString()).Last();
                                break;
                        }
                        var actionName = actionDefinition.Substring(actionDefinition.IndexOf(' '), actionDefinition.IndexOf('(') - actionDefinition.IndexOf(' ')).Trim();
                        var parameters = actionDefinition.Substring(actionDefinition.IndexOf("(") + 1, actionDefinition.Length - actionDefinition.IndexOf("(") - 2).Split(',');

                        var objectReadCount = 0;
                        var outputLine = actionName + "(";
                        if (parameters.Length > 0)
                        {
                            for (int i = 0; i < parameters.Length; i++)
                            {
                                if (parameters[i] != String.Empty)
                                {
                                    switch (parameters[i][0])
                                    {
                                        case 'I':
                                            var components = parameters[i].Split('*');
                                            if (components.Count() == 2)
                                            {
                                                var ids = idsFiles.Where(a => a.Filename.ToLower() == String.Format("{0}.ids", components[1].ToLower())).SingleOrDefault();
                                                switch (intIdx)
                                                {
                                                    case 0:
                                                        if (ids != null)
                                                        {
                                                            var idsLines = ids.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                                            var text = idsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == actionInfo[0]).SingleOrDefault();
                                                            text = text == null ? text : text.Substring(text.IndexOf(' '), text.Length - text.IndexOf(' ')).Trim();
                                                            outputLine = outputLine + (text == null ? actionInfo[0] : text) + ',';
                                                        }
                                                        else
                                                        {
                                                            outputLine = outputLine + actionInfo[0] + ',';
                                                        }
                                                        break;
                                                    case 1:
                                                        if (ids != null)
                                                        {
                                                            var idsLines = ids.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                                            var text = idsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == actionInfo[3]).SingleOrDefault();
                                                            text = text == null ? text : text.Substring(text.IndexOf(' '), text.Length - text.IndexOf(' ')).Trim();
                                                            outputLine = outputLine + (text == null ? actionInfo[0] : text) + ',';
                                                        }
                                                        else
                                                        {
                                                            outputLine = outputLine + actionInfo[3] + ',';
                                                        }
                                                        break;
                                                    case 2:
                                                        if (ids != null)
                                                        {
                                                            var idsLines = ids.contents.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                                            var text = idsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == actionInfo[4]).SingleOrDefault();
                                                            text = text == null ? text : text.Substring(text.IndexOf(' '), text.Length - text.IndexOf(' ')).Trim();
                                                            outputLine = outputLine + (text == null ? actionInfo[0] : text) + ',';
                                                        }
                                                        else
                                                        {
                                                            outputLine = outputLine + actionInfo[4] + ',';
                                                        }
                                                        break;
                                                }
                                            }
                                            intIdx++;
                                            break;
                                        case 'O':
                                            var objectResult = String.Empty;
                                            // Note: Yes, it really seems we use the second object for the first object reference and vice-versa...!
                                            switch (objectIdx)
                                            {
                                                case 0:
                                                    objectResult = ParseObject(objectLine2);
                                                    objectReadCount = 1;
                                                    break;
                                                case 1:
                                                    objectResult = ParseObject(objectLine1);
                                                    objectReadCount = 2;
                                                    break;
                                                case 2:
                                                    objectResult = ParseObject(objectLine3);
                                                    objectReadCount = 3;
                                                    break;

                                            }
                                            objectIdx++;
                                            outputLine = outputLine + objectResult + ',';
                                            break;
                                        case 'S':
                                            var parsedVariable = String.Empty;
                                            switch (stringIdx)
                                            {
                                                case 0:
                                                    parsedVariable = '"' + actionInfo[5] + '"';
                                                    if (actionInfo[5].StartsWith("GLOBAL") && (actionInfo[5] != "GLOBAL"))
                                                    {
                                                        parsedVariable = '"' + actionInfo[5].Substring(6, actionInfo[5].Length - 6) + '"';
                                                        nextStringUsage = '"' + "GLOBAL" + '"';
                                                    }
                                                    if (actionInfo[5].StartsWith("LOCALS") && (actionInfo[5] != "LOCALS"))
                                                    {
                                                        parsedVariable = '"' + actionInfo[5].Substring(6, actionInfo[5].Length - 6) + '"';
                                                        nextStringUsage = '"' + "LOCALS" + '"';
                                                    }

                                                    if ((IsAreaVariable(actionInfo[5])))
                                                    {
                                                        if (actionInfo[5].StartsWith("\""))
                                                        {
                                                            parsedVariable = '"' + actionInfo[5].Substring(7, actionInfo[5].Length - 7) + '"';
                                                        }
                                                        else
                                                        {
                                                            parsedVariable = '"' + actionInfo[5].Substring(6, actionInfo[5].Length - 6) + '"';
                                                        }
                                                        nextStringUsage = '"' + GetAreaVariable(actionInfo[5]) + '"';
                                                    }

                                                    if (i == parameters.Length - 1)
                                                    {
                                                        if (nextStringUsage != String.Empty)
                                                        {
                                                            outputLine = outputLine + nextStringUsage + ',';
                                                        }
                                                        else
                                                        {
                                                            if (actionInfo[5].StartsWith("\""))
                                                            {
                                                                outputLine = outputLine + actionInfo[5] + ',';
                                                            }
                                                            else
                                                            {
                                                                outputLine = outputLine + "\"" + actionInfo[5] + "\"" + ',';
                                                            }
                                                        }
                                                        nextStringUsage = String.Empty;
                                                    }
                                                    else
                                                    {
                                                        outputLine = outputLine + parsedVariable + ',';
                                                    }
                                                    break;
                                                case 1:
                                                    if (nextStringUsage != String.Empty)
                                                    {
                                                        outputLine = outputLine + nextStringUsage + ',';
                                                        nextStringUsage = String.Empty;
                                                        break;
                                                    }

                                                    parsedVariable = '"' + actionInfo[6] + '"';
                                                    if (actionInfo[6].StartsWith("GLOBAL") && (actionInfo[6] != "GLOBAL"))
                                                    {
                                                        parsedVariable = '"' + actionInfo[6].Substring(6, actionInfo[6].Length - 6) + '"';
                                                        nextStringUsage = '"' + "GLOBAL" + '"';
                                                    }
                                                    if (actionInfo[6].StartsWith("LOCALS") && (actionInfo[6] != "LOCALS"))
                                                    {
                                                        parsedVariable = '"' + actionInfo[6].Substring(6, actionInfo[6].Length - 6) + '"';
                                                        nextStringUsage = '"' + "LOCALS" + '"';
                                                    }

                                                    if ((IsAreaVariable(actionInfo[6])))
                                                    {
                                                        if (actionInfo[5].StartsWith("\""))
                                                        {
                                                            parsedVariable = '"' + actionInfo[6].Substring(7, actionInfo[6].Length - 7) + '"';
                                                        }
                                                        else
                                                        {
                                                            parsedVariable = '"' + actionInfo[6].Substring(6, actionInfo[6].Length - 6) + '"';
                                                        }
                                                        nextStringUsage = '"' + GetAreaVariable(actionInfo[6]) + '"';
                                                    }

                                                    if (i == parameters.Length - 1)
                                                    {
                                                        if (nextStringUsage != String.Empty)
                                                        {
                                                            outputLine = outputLine + nextStringUsage + ',';
                                                        }
                                                        else
                                                        {
                                                            if (actionInfo[6].StartsWith("\""))
                                                            {
                                                                outputLine = outputLine + actionInfo[6] + ',';
                                                            }
                                                            else
                                                            {
                                                                outputLine = outputLine + "\"" + actionInfo[6] + "\"" + ',';
                                                            }
                                                        }
                                                        nextStringUsage = String.Empty;
                                                    }
                                                    else
                                                    {
                                                        outputLine = outputLine + parsedVariable + ',';
                                                    }
                                                    break;
                                            }

                                            stringIdx++;
                                            break;
                                        case 'P':
                                            outputLine = outputLine + String.Format("[{0}.{1}]", actionInfo[1], actionInfo[2]);
                                            break;
                                    }
                                }
                            }
                        }

                        if (objectReadCount != 3)
                        {
                            var obj = String.Empty;
                            switch (objectReadCount)
                            {
                                case 0:
                                    obj = ParseObject(objectLine1);
                                    break;
                                case 1:
                                    obj = ParseObject(objectLine1); // Yes, objectLine1. Object lines 1 and 2 are swapped in the BCS
                                    break;
                                case 2:
                                    obj = ParseObject(objectLine3);
                                    break;
                            }
                            if (obj != String.Empty)
                            {
                                if (obj.StartsWith("\""))
                                {
                                    outputLine = "ActionOverride(" + obj + ',' + outputLine.Trim(',') + ")";
                                }
                                else
                                {
                                    outputLine = "ActionOverride(" + "\"" + obj + "\"" + ',' + outputLine.Trim(',') + ")";
                                }

                                if (outputLine.EndsWith(","))
                                {
                                    outputLine = outputLine.Substring(0, outputLine.Length - 1);
                                }
                            }
                        }

                        if (outputLine.EndsWith(","))
                        {
                            outputLine = outputLine.Substring(0, outputLine.Length - 1);
                        }
                        outputLine = outputLine + ")";
                        output.Add("    " + outputLine);
                        parsingAction = false;
                    }
                    idx++;
                }
            }

            return output;
        }

        private string ParseObject(string objectLine)
        {
            var objectComponents = objectLine.Split(' ');

            // Default to string
            var objectResult = String.Empty;

            if (objectComponents[12].Substring(0, objectComponents[12].Length - 2) != "\"\"")
            {
                objectResult = objectComponents[12].Substring(0, objectComponents[12].Length - 2);
                return objectResult.Trim();
            }

            if (objectComponents[0] != "0" ||
                objectComponents[1] != "0" ||
                objectComponents[2] != "0" ||
                objectComponents[3] != "0" ||
                objectComponents[4] != "0" ||
                objectComponents[5] != "0" ||
                objectComponents[6] != "0")
            {
                //Build up object specifier
                var ea = objectComponents[0] == "0" ? "0" : eaIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == objectComponents[0]).SingleOrDefault();
                ea = string.IsNullOrEmpty(ea) ? eaIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == "0x" + objectComponents[0]).SingleOrDefault() : ea;
                var general = objectComponents[1] == "0" ? "0" : generalIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == objectComponents[1]).SingleOrDefault();
                general = string.IsNullOrEmpty(general) ? generalIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == "0x" + objectComponents[1]).SingleOrDefault() : general;
                var race = objectComponents[2] == "0" ? "0" : raceIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == objectComponents[2]).SingleOrDefault();
                race = string.IsNullOrEmpty(race) ? raceIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == "0x" + objectComponents[2]).SingleOrDefault() : race;
                var @class = objectComponents[3] == "0" ? "0" : classIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == objectComponents[3]).SingleOrDefault();
                @class = string.IsNullOrEmpty(@class) ? classIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == "0x" + objectComponents[3]).SingleOrDefault() : @class;
                var specific = objectComponents[4] == "0" ? "0" : specificIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == objectComponents[4]).SingleOrDefault();
                specific = string.IsNullOrEmpty(specific) ? specificIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == "0x" + objectComponents[4]).SingleOrDefault() : specific;
                var gender = objectComponents[5] == "0" ? "0" : genderIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == objectComponents[5]).SingleOrDefault();
                gender = string.IsNullOrEmpty(gender) ? genderIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == "0x" + objectComponents[5]).SingleOrDefault() : gender;
                var alignmen = objectComponents[6] == "0" ? "0" : alignmenIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == objectComponents[6]).SingleOrDefault();
                alignmen = string.IsNullOrEmpty(alignmen) ? alignmenIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == "0x" + objectComponents[6]).SingleOrDefault() : alignmen;

                ea = ea.IndexOf(' ') > 0 ? ea.Substring(ea.IndexOf(' '), ea.Length - ea.IndexOf(' ')).Trim() : ea.Trim();
                general = race.IndexOf(' ') > 0 ? general.Substring(general.IndexOf(' '), general.Length - general.IndexOf(' ')).Trim() : general.Trim();
                race = race.IndexOf(' ') > 0 ? race.Substring(race.IndexOf(' '), race.Length - race.IndexOf(' ')).Trim() : race.Trim();
                @class = @class.IndexOf(' ') > 0 ? @class.Substring(@class.IndexOf(' '), @class.Length - @class.IndexOf(' ')).Trim() : @class.Trim();
                specific = specific.IndexOf(' ') > 0 ? specific.Substring(specific.IndexOf(' '), specific.Length - specific.IndexOf(' ')).Trim() : specific.Trim();
                gender = gender.IndexOf(' ') > 0 ? gender.Substring(gender.IndexOf(' '), gender.Length - gender.IndexOf(' ')).Trim() : gender.Trim();
                alignmen = alignmen.IndexOf(' ') > 0 ? alignmen.Substring(alignmen.IndexOf(' '), alignmen.Length - alignmen.IndexOf(' ')).Trim() : alignmen.Trim();

                objectResult = String.Format("[{0}{1}{2}{3}{4}{5}{6}]", ea == "0" ? String.Empty : ea,
                    general == "0" ? String.Empty : "." + general,
                    race == "0" ? String.Empty : "." + race,
                    @class == "0" ? String.Empty : "." + @class,
                    specific == "0" ? String.Empty : "." + specific,
                    gender == "0" ? String.Empty : "." + gender,
                    alignmen == "0" ? String.Empty : "." + alignmen);
                return objectResult.Trim();
            }


            var @object1 = objectComponents[7] == "0" ? String.Empty : objectIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == objectComponents[7]).SingleOrDefault();
            @object1 = string.IsNullOrEmpty(@object1) ? objectIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == "0x" + objectComponents[7]).SingleOrDefault() : @object1;

            var @object2 = objectComponents[8] == "0" ? String.Empty : objectIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == objectComponents[8]).SingleOrDefault();
            @object2 = string.IsNullOrEmpty(@object2) ? objectIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == "0x" + objectComponents[8]).SingleOrDefault() : @object2;

            var @object3 = objectComponents[9] == "0" ? String.Empty : objectIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == objectComponents[9]).SingleOrDefault();
            @object3 = string.IsNullOrEmpty(@object3) ? objectIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == "0x" + objectComponents[9]).SingleOrDefault() : @object3;

            var @object4 = objectComponents[10] == "0" ? String.Empty : objectIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == objectComponents[10]).SingleOrDefault();
            @object4 = string.IsNullOrEmpty(@object4) ? objectIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == "0x" + objectComponents[10]).SingleOrDefault() : @object4;

            var @object5 = objectComponents[11] == "0" ? String.Empty : objectIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == objectComponents[11]).SingleOrDefault();
            @object5 = string.IsNullOrEmpty(@object5) ? objectIdsLines.Where(a => a.IndexOf(' ') > 0 && a.Substring(0, a.IndexOf(' ')) == "0x" + objectComponents[11]).SingleOrDefault() : @object5;

            @object1 = @object1 ?? String.Empty;
            @object2 = @object2 ?? String.Empty;
            @object3 = @object3 ?? String.Empty;
            @object4 = @object4 ?? String.Empty;
            @object5 = @object5 ?? String.Empty;

            @object1 = @object1.IndexOf(' ') > 0 ? @object1.Substring(@object1.IndexOf(' '), @object1.Length - @object1.IndexOf(' ')) : @object1;
            @object2 = @object2.IndexOf(' ') > 0 ? @object2.Substring(@object2.IndexOf(' '), @object2.Length - @object2.IndexOf(' ')) : @object2;
            @object3 = @object3.IndexOf(' ') > 0 ? @object3.Substring(@object3.IndexOf(' '), @object3.Length - @object3.IndexOf(' ')) : @object3;
            @object4 = @object4.IndexOf(' ') > 0 ? @object4.Substring(@object4.IndexOf(' '), @object4.Length - @object4.IndexOf(' ')) : @object4;
            @object5 = @object5.IndexOf(' ') > 0 ? @object5.Substring(@object5.IndexOf(' '), @object5.Length - @object5.IndexOf(' ')) : @object5;

            @object1 = @object1.Trim();
            @object2 = @object2.Trim();
            @object3 = @object3.Trim();
            @object4 = @object4.Trim();
            @object5 = @object5.Trim();

            @object5 = @object5 == String.Empty ? @object5 : @object5 + "(";
            @object4 = @object4 == String.Empty ? @object4 : @object4 + "(";
            @object3 = @object3 == String.Empty ? @object3 : @object3 + "(";
            @object2 = @object2 == String.Empty ? @object2 : @object2 + "(";
            @object1 = @object1 == String.Empty ? @object1 : @object1 + "(";

            var closeCount1 = @object1.IndexOf("(") > 1 ? 1 : 0;
            var closeCount2 = @object2.IndexOf("(") > 1 ? 1 : 0;
            var closeCount3 = @object3.IndexOf("(") > 1 ? 1 : 0;
            var closeCount4 = @object4.IndexOf("(") > 1 ? 1 : 0;
            var closeCount5 = @object5.IndexOf("(") > 1 ? 1 : 0;
            var closeCount = closeCount1 + closeCount2 + closeCount3 + closeCount4 + closeCount5;
            var closeString = String.Empty;
            for (int i = 0; i < closeCount; i++)
            {
                closeString = closeString + ")";
            }

            if (@object1 == String.Empty &&
                @object2 == String.Empty &&
                @object3 == String.Empty &&
                @object4 == String.Empty &&
                @object5 == String.Empty)
            {
                @object1 = "Myself";
            }

            objectResult = String.Format("{0}{1}{2}{3}{4}{5}", @object5, @object4, @object3, @object2, @object1, closeString);
            if (objectResult.EndsWith("()"))
            {
                objectResult = objectResult.Replace("()", String.Empty);
            }

            return objectResult.Trim();
        }

        private bool IsAreaVariable(string text)
        {
            if (text.ToUpper().StartsWith("\"AR"))
            {
                if (text.Length > 7)
                {
                    var text2 = text.Substring(3, 4);
                    var value = 0;
                    return int.TryParse(text2, out value);
                }
            }

            if (text.ToUpper().StartsWith("AR"))
            {
                if (text.Length > 7)
                {
                    var text2 = text.Substring(2, 4);
                    var value = 0;
                    return int.TryParse(text2, out value);
                }
            }
            return false;
        }

        private string GetAreaVariable(string text)
        {
            if (text.ToUpper().StartsWith("\"AR"))
            {
                if (text.Length > 7)
                {
                    var text2 = text.Substring(1, 6);
                    return text2;
                }
            }

            if (text.ToUpper().StartsWith("AR"))
            {
                if (text.Length > 7)
                {
                    var text2 = text.Substring(0, 6);
                    return text2;
                }
            }
            return String.Empty;
        }
    }
}