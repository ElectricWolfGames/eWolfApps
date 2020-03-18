using System;
using System.Diagnostics;

namespace CommandLineHelpers
{
    public static class CommandRunner
    {
        public static void ExecuteCommand(string command)
        {
            Console.WriteLine($"And the command is {command}");

            ProcessStartInfo ProcessInfo;
            Process Process;

            ProcessInfo = new ProcessStartInfo("cmd.exe", "/C " + command);
            ProcessInfo.WorkingDirectory = @"C:\Git\";
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = true;
            ProcessInfo.Verb = "runas";
            Process = Process.Start(ProcessInfo);
        }
    }
}