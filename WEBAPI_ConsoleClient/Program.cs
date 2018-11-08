using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI_ConsoleClient
{

    class Program
    {
        static HttpClient client;
        static void Main(string[] args)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1479/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );

            DoIt().GetAwaiter().GetResult();

            Console.ReadLine();
        }

        static async Task<List<ChatEntry>> GetEntries()
        {
            List<ChatEntry> entries = null;
            HttpResponseMessage response = await client.GetAsync("Home/GetAll");
            if (response.IsSuccessStatusCode)
            {
                entries = await response.Content.ReadAsAsync<List<ChatEntry>>();
            }
            return entries;
        }

        static async Task AddEntry(ChatEntry ce)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync
                ("/Home/AddEntry", ce);
            response.EnsureSuccessStatusCode();
            //return Task.CompletedTask;
        }

        static async Task DoIt()
        {
            Console.Write("Add your name: ");
            string name = Console.ReadLine();
            Console.Write("Add your message: ");
            string message = Console.ReadLine();

            ChatEntry entry = new ChatEntry(name, message);
            await AddEntry(entry);

            Console.Clear();

            var entries = await GetEntries();
            foreach (var item in entries)
            {
                Console.WriteLine("[" + item.Time + "] " + item.Owner+
                    ": " + item.Message);
            }
        }
        
    }
}
