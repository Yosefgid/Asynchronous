using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Threading.Tasks;
using System.Xml;

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


            //Read/Write

            AsyncFileManager fileManager = new();
            var filePath = "SuperSecretFile.txt";
            var encrypted = fileManager.ReadFile(filePath);
            //var decrypted = Decrypt(encrypted);

            //fileManager.WriteFile("DecryptedMessage.txt", decrypted);
            //Console.WriteLine(decrypted);

            //Concurrency
            var filePath1 = "ReallySuperSecretFile.txt";
            var filePath2 = "SuperTopSecretTextFile.txt";
            var encrypted1 = fileManager.ReadFile(filePath1);
            var encrypted2 = fileManager.ReadFile(filePath2);

            string[] combined = await Task.WhenAll(encrypted, encrypted1, encrypted2);

            //Now the decrypt 
            List<string> decryptedCombined = new();
            foreach(string curr in combined)
            {
                Console.WriteLine(Decrypt(curr));
                
                decryptedCombined.Add((Decrypt(curr)));
                
            }
            
            string finalCombined = string.Join( " ", decryptedCombined);

            fileManager.WriteFile("DecryptedMessage.txt", finalCombined);


        }


        static string Decrypt(string content)
        {

            var letters = "abcdefghijklmnopqrstuvwxyz";
            char[] contentR = content.ToCharArray();
            var lettersArr = letters.ToCharArray();
            List<char> result = new();

            foreach (char letter in contentR)
            {
                if (!char.IsLetter(letter))
                {

                    result.Add(letter);
                    continue;

                }
                int indexOf = letters.IndexOf(char.ToLower(letter));
                int newIndex = (indexOf + 1) % letters.Length;
                result.Add(letters[newIndex]);

            }
            result[0] = char.ToUpper(result[0]);
           

            return new string(result.ToArray());

            //for (int i = 0; i < contentR.Length; i++)
            //{
            //    char curr = contentR[i];
            //    if (char.IsLetter(curr))
            //    {
            //        if (curr == 'z')
            //        {
            //            contentR[i] = 'a';
            //        }
            //        else
            //        {

            //            contentR[i] = (char)(curr + 1);
            //        }

            //    }
            //}
            //return new string(contentR);

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
