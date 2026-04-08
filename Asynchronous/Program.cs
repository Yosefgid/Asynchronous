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
            var sayHello = Task.Run(async () =>
            {
                await Task.Delay(3000);
                Console.WriteLine("Hello");
            });

            var sayWorld = Task.Run(async () =>
            {
                await Task.Delay(3000);
                Console.WriteLine("World!");
            });

            await Task.WhenAll([sayHello, sayWorld]);
            stopwatch.Stop();
            Console.WriteLine($"It took {stopwatch.ElapsedMilliseconds}ms to execute code!");
        } 
    }
}
