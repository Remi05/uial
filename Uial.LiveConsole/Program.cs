using System;

namespace Uial.LiveConsole
{
    class Program
    {
        // Based on https://learn.microsoft.com/en-us/windows/win32/api/shellscalingapi/ne-shellscalingapi-process_dpi_awareness
        private const int PROCESS_PER_MONITOR_DPI_AWARE = 2;

        // Based on https://learn.microsoft.com/en-us/windows/win32/api/shellscalingapi/nf-shellscalingapi-setprocessdpiawareness
        [System.Runtime.InteropServices.DllImport("Shcore.dll")]
        private static extern bool SetProcessDpiAwareness(int dpiAwareness);

        static void Main(string[] args)
        {
            SetProcessDpiAwareness(PROCESS_PER_MONITOR_DPI_AWARE);

            var interpreter = new LiveInterpreter(Console.In, Console.Out, Console.Clear);
            interpreter.Run();
        }
    }
}
