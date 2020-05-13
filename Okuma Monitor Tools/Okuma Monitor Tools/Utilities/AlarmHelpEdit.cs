using Okuma.Scout;
using Okuma_Monitor_Tools.Enums;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Okuma_Monitor_Tools.Utilities
{
    public class AlarmHelpEdit
    { 
        public static void WriteAlarmHelp(AlarmLevelEnum AlarmLevel, string AlarmString, string Message,
            string Object = "", string CharacterString = "", string Code = "", string FaultyLocation = "")
        {
       
        /* TODO ERROR: Skipped WarningDirectiveTrivia */
        var machineCode = DMC.MachineType;
        //var machineCode = OkMachine.GetMachineType == enumMachineType.Mill ? "M" : "L";
            /* TODO ERROR: Skipped WarningDirectiveTrivia */
            var alarmCode = Enum.GetName(typeof(AlarmLevelEnum), AlarmLevel);

            int alarmNumberStart = 2;
            if (AlarmLevel == AlarmLevelEnum.C)
                alarmNumberStart = 3;
            if (AlarmLevel == AlarmLevelEnum.D)
                alarmNumberStart = 4;

            var alarmFilePath = Path.Combine(string.Format(@"C:\OSP-P\P-MANUAL\{0}PA\ENG", machineCode),
                string.Format("ALARM-{0}.HTM", alarmCode));
            if (FileInUse(alarmFilePath))
                return;
            StringBuilder outputBuff = new StringBuilder();
            string buff = "";
            string dump = "";
            var lineSpace = "       ";
            var lineEnd = "<!--**-->";


            using (StreamReader sr = new StreamReader(alarmFilePath))
            {
                while (!sr.EndOfStream)
                {
                    buff = sr.ReadLine();
                    if (buff.Contains(string.Format("{0}127</FONT>", alarmNumberStart)))
                    {
                        if (buff.Contains("ThiNC Alarm"))
                            outputBuff.AppendLine(buff.Replace("ThiNC Alarm",
                                string.Format("<!--**AlarmString-->{0}<!--AlarmString**-->", AlarmString)));
                        else if (buff.Contains("THiNC Alarm")
                        )
                            outputBuff.AppendLine(buff.Replace("THiNC Alarm",
                                string.Format("<!--**AlarmString-->{0}<!--AlarmString**-->", AlarmString)));
                        // We found the first line
                        // Skip the next 3 lines
                        outputBuff.AppendLine(sr.ReadLine());
                        outputBuff.AppendLine(sr.ReadLine());
                        outputBuff.AppendLine(sr.ReadLine());
                        // Dump Message line
                        dump = sr.ReadLine();
                        outputBuff.AppendFormat("<!--**Message-->{0}{1}{2}", lineSpace, Message, lineEnd);
                        // Skip the next line
                        outputBuff.AppendLine(sr.ReadLine());
                        // Dump Object Line
                        dump = sr.ReadLine();
                        outputBuff.AppendLineFormat("<!--**Object--> {0}{1}{2}", lineSpace, Object, lineEnd);
                        // Skip the next line
                        outputBuff.AppendLine(sr.ReadLine());
                        // Dump CharacterString Line
                        dump = sr.ReadLine();
                        outputBuff.AppendLineFormat("<!--**CharacterString--> {0}{1}{2}", lineSpace, CharacterString,
                            lineEnd);
                        // Skip the next line
                        outputBuff.AppendLine(sr.ReadLine());
                        // Dump Code Line
                        dump = sr.ReadLine();
                        outputBuff.AppendLineFormat("<!--**Code--> {0}{1}{2}", lineSpace, Code, lineEnd);
                        // Skip the next line
                        outputBuff.AppendLine(sr.ReadLine());
                        // Dump FaultyLocation Line
                        dump = sr.ReadLine();
                        outputBuff.AppendLineFormat("<!--**FaultyLocation--> {0}{1}{2}", lineSpace, FaultyLocation,
                            lineEnd);
                    }
                    else
                        outputBuff.AppendLine(buff);
                }
            }

            File.WriteAllText(alarmFilePath, outputBuff.ToString());
        }

        public static void WriteExternalThincAlarmHelp(AlarmLevelEnum AlarmLevel, string AlarmString, string Message,
            string Object = "", string CharacterString = "", string Code = "", string FaultyLocation = "")
        {

            /* TODO ERROR: Skipped WarningDirectiveTrivia */
            var machineCode = DMC.MachineType;
            //var machineCode = OkMachine.GetMachineType == enumMachineType.Mill ? "M" : "L";
            /* TODO ERROR: Skipped WarningDirectiveTrivia */
            var alarmCode = Enum.GetName(typeof(AlarmLevelEnum), AlarmLevel);

            int alarmNumberStart = 1;
            if (AlarmLevel == AlarmLevelEnum.B)
                alarmNumberStart = 3;
            if (AlarmLevel == AlarmLevelEnum.C)
                alarmNumberStart = 3;
            if (AlarmLevel == AlarmLevelEnum.D)
                alarmNumberStart = 4;

            var alarmFilePath = Path.Combine(string.Format(@"C:\OSP-P\P-MANUAL\{0}PA\ENG", machineCode),
                string.Format("ALARM-{0}.HTM", alarmCode));
            if (FileInUse(alarmFilePath))
                return;
            StringBuilder outputBuff = new StringBuilder();
            string buff = "";
            string dump = "";
            var lineSpace = "       ";
            var lineEnd = "<!--**-->";


            using (StreamReader sr = new StreamReader(alarmFilePath))
            {
                while (!sr.EndOfStream)
                {
                    buff = sr.ReadLine();
                    if (buff.Contains(string.Format("{0}125</FONT>", alarmNumberStart)))
                    {
                        outputBuff.AppendLine(buff.Replace("THiNC Alarm",
                            string.Format("<!--**AlarmString-->{0}<!--AlarmString**-->", AlarmString)));
                        // We found the first line
                        // Skip the next 3 lines
                        outputBuff.AppendLine(sr.ReadLine());
                        outputBuff.AppendLine(sr.ReadLine());
                        outputBuff.AppendLine(sr.ReadLine());
                        // Dump Message line
                        dump = sr.ReadLine();
                        outputBuff.AppendLineFormat("<!--**Message-->{0}{1}{2}", lineSpace, Message, lineEnd);
                        // Skip the next line
                        outputBuff.AppendLine(sr.ReadLine());
                        // Dump Object Line
                        dump = sr.ReadLine();
                        outputBuff.AppendLineFormat("<!--**Object--> {0}{1}{2}", lineSpace, Object, lineEnd);
                        // Skip the next line
                        outputBuff.AppendLine(sr.ReadLine());
                        // Dump CharacterString Line
                        dump = sr.ReadLine();
                        outputBuff.AppendLineFormat("<!--**CharacterString--> {0}{1}{2}", lineSpace, CharacterString,
                            lineEnd);
                        // Skip the next line
                        outputBuff.AppendLine(sr.ReadLine());
                        // Dump Code Line
                        dump = sr.ReadLine();
                        outputBuff.AppendLineFormat("<!--**Code--> {0}{1}{2}", lineSpace, Code, lineEnd);
                        // Skip the next line
                        outputBuff.AppendLine(sr.ReadLine());
                        // Dump FaultyLocation Line
                        dump = sr.ReadLine();
                        outputBuff.AppendLineFormat("<!--**FaultyLocation--> {0}{1}{2}", lineSpace, FaultyLocation,
                            lineEnd);
                    }
                    else
                        outputBuff.AppendLine(buff);
                }
            }

            File.WriteAllText(alarmFilePath, outputBuff.ToString());
        }

        public static void RemoveAlarmHelp(AlarmLevelEnum AlarmLevel)
        {
            /* TODO ERROR: Skipped WarningDirectiveTrivia */
            var machineCode = DMC.MachineType;
            //var machineCode = OkMachine.GetMachineType == enumMachineType.Mill ? "M" : "L";
            /* TODO ERROR: Skipped WarningDirectiveTrivia */
            var alarmCode = Enum.GetName(typeof(AlarmLevelEnum), AlarmLevel);

            var alarmFilePath = Path.Combine(string.Format(@"C:\OSP-P\P-MANUAL\{0}PA\ENG", machineCode),
                string.Format("ALARM-{0}.HTM", alarmCode));
            if (FileInUse(alarmFilePath))
                return;
            Regex titleRemovalRegex = new Regex(@"\<\!\-\-\*\*AlarmString\-\-\>.*\<\!\-\-AlarmString\*\*\-\-\>",
                RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            Regex removalRegex = new Regex(@"\<\!\-\-\*\*\w{1,15}\-\-\>.*?\<\!\-\-\*\*\-\-\>",
                RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

            string buff = "";
            using (StreamReader sr = new StreamReader(alarmFilePath))
            {
                buff = sr.ReadToEnd();
                buff = titleRemovalRegex.Replace(buff, "ThiNC Alarm");
                buff = removalRegex.Replace(buff, "       Undefined");
            }

            File.WriteAllText(alarmFilePath, buff);
        }

        public static void RemoveExternalAlarmHelp(AlarmLevelEnum AlarmLevel)
        {
            /* TODO ERROR: Skipped WarningDirectiveTrivia */
            var machineCode = DMC.MachineType;
            //var machineCode = OkMachine.GetMachineType == enumMachineType.Mill ? "M" : "L";
            /* TODO ERROR: Skipped WarningDirectiveTrivia */
            var alarmCode = Enum.GetName(typeof(AlarmLevelEnum), AlarmLevel);

            var alarmFilePath = Path.Combine(string.Format(@"C:\OSP-P\P-MANUAL\{0}PA\ENG", machineCode),
                string.Format("ALARM-{0}.HTM", alarmCode));
            if (FileInUse(alarmFilePath))
                return;
            Regex titleRemovalRegex = new Regex(@"\<\!\-\-\*\*AlarmString\-\-\>.*\<\!\-\-AlarmString\*\*\-\-\>",
                RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            Regex removalRegex = new Regex(@"\<\!\-\-\*\*\w{1,15}\-\-\>.*?\<\!\-\-\*\*\-\-\>",
                RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

            string buff = "";
            using (StreamReader sr = new StreamReader(alarmFilePath))
            {
                buff = sr.ReadToEnd();
                buff = titleRemovalRegex.Replace(buff, "ThiNC Alarm");
                buff = removalRegex.Replace(buff, "       Undefined");
            }

            File.WriteAllText(alarmFilePath, buff);
        }

        private static bool FileInUse(string sFile)
        {
            bool thisFileInUse = false;
            if (File.Exists(sFile))
            {
                try
                {
                    using (System.IO.FileStream f =
                        new System.IO.FileStream(sFile, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                    {
                    }
                }
                catch
                {
                    thisFileInUse = true;
                }
            }

            return thisFileInUse;
        }
    }
}
