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
            var random = new Random();
            var cts = new CancellationTokenSource(5000);
            var token = cts.Token;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                Task<string> sayHello = Task.Run(async () =>
                {
                    int delay = random.Next(1000, 10000);
                    await Task.Delay(delay, token);
                    return "Hello...";
                }, token);

                Task<string> sayWorld = Task.Run(async () =>
                {
                    int delay = random.Next(1000, 10000);
                    await Task.Delay(delay, token);
                    return "...World";
                }, token);

                var combinedTask = await Task.WhenAll(sayHello, sayWorld);
                Console.WriteLine(combinedTask[0] + combinedTask[1]);
                stopwatch.Stop();
                Console.WriteLine($"It took {stopwatch.ElapsedMilliseconds}ms to execute code!");
            }
            catch(Exception delayedTooLongException)
            {
                Console.WriteLine("The task has been cancelled: It took more than 5 seconds: it took" + stopwatch.Elapsed);
            }
        } 
    }
}
