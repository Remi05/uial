using System;

namespace Uial.LiveConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var interpreter = new LiveInterpreter(Console.In, Console.Out, Console.Clear);
            interpreter.Run();
        }
    }
}
