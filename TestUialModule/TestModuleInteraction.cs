using System;
using Uial.Interactions;

namespace TestUialModule
{
    public class TestModuleInteraction : IInteraction
    {
        public string Name => "TestModuleInteraction";

        public void Do()
        {
            Console.WriteLine($"Hello from {nameof(TestModuleInteraction)}");
        }
    }
}
