using System.Diagnostics;
using System.Threading.Tasks;

namespace Asynchronous
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await PrintHelloWorld();
          
        }
        static async Task PrintHelloWorld()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await Task.Delay(2000);
            Console.WriteLine("Hello World!");
            stopwatch.Stop();
            Console.WriteLine($"It took {stopwatch.ElapsedMilliseconds}ms to execute code!");
        }
    }
}
