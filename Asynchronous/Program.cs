using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Threading.Tasks;

namespace Asynchronous
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await PrintHelloWorld();

            //string data = "85671 34262 92143 50984 24515 68356 77247 12348 56789 98760";
            //string story = "Mary had a little lamb, its fleece was white as snow.";
            //var words = story.Split(" ");

            //List<BigInteger> list = new();
            //list = data.Split(" ").Select(num => BigInteger.Parse(num)).ToList();

            //var printStory = Task.Run(async () =>
            //{
            //    foreach (var word in words)
            //    {
            //        Console.WriteLine(word);
            //        await Task.Delay(1000);
            //    }
            //});
            //var factorialNumbers = Task.Run(() =>
            //{
            //    var numbersFactorialed = list
            //    .Select(num => Exercises.CalculateFactorial(num))
            //    .ToList();
            //    return numbersFactorialed;
            //}).ContinueWith(t =>
            //{
            //    foreach (var numbers in t.Result)
            //    {
            //        Console.WriteLine("\n \n \n \n");
            //        Console.WriteLine(numbers);
            //    }
            //});
            //await Task.WhenAll([factorialNumbers, printStory]);
            AsyncFileManager fileManager = new();
            var filePath = "SuperSecretFile.txt";
            Console.WriteLine(await fileManager.ReadFile(filePath));


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
