using IPSClient;
using System;

namespace IPSConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ApiClient(args[0], args[1]);
            var helloResponse = client.Hello().Result;
            Console.WriteLine(helloResponse.communityName);
        }
    }
}