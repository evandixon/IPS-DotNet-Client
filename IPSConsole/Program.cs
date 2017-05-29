using IPSClient;
using System;
using System.Collections.Generic;
using System.Linq;
using IPSClient.Objects.Pages;

namespace IPSConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ApiClient(args[0], args[1]);

            // System
            var systemHello = client.Hello().Result;
            // System/Groups
            //var allGroups = client.GetGroups().ToList();

            // CMS
            //var page = client.CreatePage(1, new CreatePageRequest { category = 1, author = 183, fields = new Dictionary<int, string> { { 1, "Test Content" }, { 2, "Test Data" } } } ).Result;
            //var page = client.CreatePage(1, new CreatePageRequest { category = 1, author = 183, fields = new Dictionary<int, string> { { 1, "Test Content" }, { 2, "Test Data" } } }).Result;
            var pages = client.GetRecords(1, new IPSClient.Objects.System.GetContentItemsRequest { categories = "1" }).ToList();
            //var response = client.GetFiles(new IPSClient.Objects.System.GetContentItemsRequest { categories = "2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,56,57,61,62,65,66,67,68,69,70,71,72,73,74,75,76,77,78" });
            //foreach (var item in response.ToList())
            //{
            //    Console.WriteLine(item.title);
            //}
        }
    }
}