using IPSClient;
using System;

namespace IPSConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ApiClient(args[0], args[1]);
            var response = client.GetFiles(new IPSClient.Objects.Downloads.GetFilesRequest { categories = "41,42,43" }).Result;
            foreach (var item in response)
            {
                Console.WriteLine(item.title);
            }
        }
    }
}