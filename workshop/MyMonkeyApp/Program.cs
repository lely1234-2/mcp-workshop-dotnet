using System;
using System.Threading.Tasks;
using MyMonkeyApp.Helpers;
using MyMonkeyApp.Models;
using System.Collections.Generic;

namespace MyMonkeyApp
{
    class Program
    {
        static readonly string MCP_API_URL = "http://localhost:5000/api/monkeys"; // 실제 MCP 서버 주소로 변경
        static readonly List<string> AsciiArts = new List<string>
        {
            @"  (o.o)  ",
            @"  (:'  ",
            @"  (\_/)",
            @"  (='.'=)",
            @"  (')_(')"
        };

        static async Task Main(string[] args)
        {
            await MonkeyHelper.LoadMonkeysFromServerAsync(MCP_API_URL);
            var running = true;
            var rand = new Random();
            while (running)
            {
                Console.Clear();
                // 랜덤 ASCII 아트 출력
                Console.WriteLine(AsciiArts[rand.Next(AsciiArts.Count)]);
                Console.WriteLine("\nMonkey App Menu:");
                Console.WriteLine("1. List all monkeys");
                Console.WriteLine("2. Get details for a specific monkey by name");
                Console.WriteLine("3. Get a random monkey");
                Console.WriteLine("4. Exit app");
                Console.Write("Select an option: ");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        DisplayMonkeyTable(MonkeyHelper.GetAllMonkeys());
                        break;
                    case "2":
                        Console.Write("Enter monkey name: ");
                        var name = Console.ReadLine();
                        var monkey = MonkeyHelper.FindMonkeyByName(name ?? "");
                        if (monkey != null)
                            DisplayMonkeyDetails(monkey);
                        else
                            Console.WriteLine("Monkey not found.");
                        break;
                    case "3":
                        var randomMonkey = MonkeyHelper.GetRandomMonkey();
                        if (randomMonkey != null)
                        {
                            DisplayMonkeyDetails(randomMonkey);
                            Console.WriteLine($"Random pick count: {MonkeyHelper.GetRandomPickCount()}");
                        }
                        else
                            Console.WriteLine("No monkeys available.");
                        break;
                    case "4":
                        running = false;
                        continue;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void DisplayMonkeyTable(List<Monkey> monkeys)
        {
            Console.WriteLine("| Name      | Species   | Age | Favorite Food | Description");
            Console.WriteLine("|-----------|-----------|-----|--------------|------------");
            foreach (var m in monkeys)
            {
                Console.WriteLine($"| {m.Name,-9} | {m.Species,-9} | {m.Age,3} | {m.FavoriteFood,-12} | {m.Description}");
            }
        }

        static void DisplayMonkeyDetails(Monkey m)
        {
            Console.WriteLine($"Name: {m.Name}\nSpecies: {m.Species}\nAge: {m.Age}\nFavorite Food: {m.FavoriteFood}\nDescription: {m.Description}");
        }
    }
}
