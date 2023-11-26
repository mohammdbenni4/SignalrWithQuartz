
using Microsoft.AspNetCore.SignalR.Client;
using System.Net;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                 .WithUrl("https://localhost:7284/not-hub")
                 .Build();

            connection.StartAsync().Wait();
            connection.On("RecieveMessage",(string message) =>
            {
                Console.WriteLine(message);
            });

            Console.ReadKey();
        }
    }
}

