using System;

namespace TempoTaskScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");
            var test = new Test();
            test.Run().Wait();
            Console.WriteLine("Done");
            Console.ReadKey();

        }
    }
}
