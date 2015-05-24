using System;
using System.Threading;
using System.Text.RegularExpressions;
// using System.Rundelay.InteropServices;

// http://debugmode.net/2010/05/18/shut-down-restart-log-off-and-forced-log-off-system-using-c/
namespace SixTaks
{
    class Program
    {
        // [DllImport("user32.dll")]
        // public static extern int ExitWindowsEx(int operationFlag, int operationReason);

        static void Main(string[] args)
        {   
            int delay = 0;
            bool l = false,
                 s = false,
                 r = false,
                 t = false;

            for (int i = 0; i < args.Length; i++)
            {
                // I don't like switch-case blocks, so let's use if-else :-)
                if (args[i].Equals("-l"))
                    l = true;
                else if (args[i].Equals("-s"))
                    s = true;
                else if (args[i].Equals("-r"))
                    r = true;
                else if (args[i].Equals("-t"))
                    t = true;
                else if (Regex.IsMatch(args[i], @"\d") && t && args[i - 1].Equals("-t"))
                    delay = int.Parse(args[i]) * 1000;
            }
            Thread.Sleep(delay);
            if (l)
                ExitWindowsEx(0, 0);
            if (s)
                ExitWindowsEx(1, 0);
            if (r)
                ExitWindowsEx(2, 0);
        }

        private static string[] operationsActionNames = new string[] {"LogOff", "ShutDown", "Restart"};

        private static void ExitWindowsEx(int operationFlag, int operationReason)
        {
            Console.WriteLine("operationFlag = " + operationFlag + " => " + operationsActionNames[operationFlag]);
        }
    }
}
