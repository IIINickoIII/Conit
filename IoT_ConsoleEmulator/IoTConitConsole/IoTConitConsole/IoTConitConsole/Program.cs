using Newtonsoft.Json;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace IoTConitConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Select language please: ");
                Console.WriteLine("1 - English");
                Console.WriteLine("2 - Ukrainian");
                Console.WriteLine("3 - Russian");
                Console.Write("Your choice: ");
                if (int.TryParse(Console.ReadLine(), out int number))
                {

                    while (true)
                    {
                        AsyncContext.Run(() => MainAsync(number));
                    }
                }
                break;
            }
        }

        static async void MainAsync(int number)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.Clear();
            Console.Write(Language[number][0]);
            var input = Console.ReadLine();
            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine(Language[number][1]);
            }

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44397/api/parts/" + id.ToString());
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch
                {
                    Console.WriteLine(Language[number][2]);
                    Console.WriteLine();
                    Console.ReadLine();
                    return;
                }

                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    Console.WriteLine();

                    var part = JsonConvert.DeserializeObject<Part>(responseBody);

                    Console.WriteLine($"{Language[number][3]} {part.Id}\t {Language[number][4]} {part.Name}");

                }

            }
            Console.WriteLine();
            Console.ReadLine();
        }

        static List<List<string>> Language = new List<List<string>>()
        {
            new List<string>(),
            new List<string>
            {
                "Scan your part, please: ",
                "Try again!",
                "Not found 404",
                "Part Id:",
                "Part Name:"
            },
            new List<string>
            {
                "Відскануйте свою деталь, будь-ласка: ",
                "Спробуйте ще раз!",
                "Не знайдено 404",
                "Id:",
                "Name:"
            },
            new List<string>
            {
                "Отсканируйте свою деталь, пожалуйста: ",
                "Попробуйте еще раз!",
                "Не найдено 404",
                "Id:",
                "Name:"
            }
        };
    }
}
