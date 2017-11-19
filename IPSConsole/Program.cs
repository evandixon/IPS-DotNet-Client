using IPSClient;
using System;
using System.Collections.Generic;
using System.Linq;
using IPSClient.Objects.Pages;
using IPSClient.Objects.Downloads;
using System.IO;

namespace IPSConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var endpoint = args[0];
            var key = args[1];
            var action = args[2];

            var client = new ApiClient(endpoint, key);

            switch (action)
            {
                case "hello":
                    var systemHello = client.Hello().Result;
                    Console.WriteLine("Community name: " + systemHello.communityName);
                    Console.WriteLine("Community url: " + systemHello.communityUrl);
                    Console.WriteLine("IPS version: " + systemHello.ipsVersion);
                    break;

                case "file-upload":
                    {
                        var categoryId = args[3];
                        var authorId = args[4];
                        var title = args[6];
                        var description = args[5];

                        var request = new CreateFileRequest();
                        request.category = int.Parse(categoryId);
                        request.author = int.Parse(authorId);
                        request.title = title;
                        request.description = description;
                        for (int i = 7; i < args.Length; i++)
                        {
                            if (System.IO.File.Exists(args[i]))
                            {
                                request.AddFile(Path.GetFileName(args[i]), System.IO.File.ReadAllBytes(args[i]));
                            }
                            else if (System.IO.Directory.Exists(args[i]))
                            {
                                foreach (var filename in Directory.GetFiles(args[i], "*", SearchOption.TopDirectoryOnly))
                                {
                                    request.AddFile(Path.GetFileName(filename), System.IO.File.ReadAllBytes(filename));
                                }
                            }
                        }
                        client.CreateFile(request).Wait();
                    }
                    break;
                case "file-update":
                    {
                        var fileId = args[3];
                        var changelog = args[4];
                        var request = new NewFileVersionRequest();
                        request.changelog = changelog;
                        for (int i = 5; i < args.Length; i++)
                        {
                            if (System.IO.File.Exists(args[i]))
                            {
                                request.AddFile(Path.GetFileName(args[i]), System.IO.File.ReadAllBytes(args[i]));
                            }
                            else if (System.IO.Directory.Exists(args[i]))
                            {
                                foreach (var filename in Directory.GetFiles(args[i], "*", SearchOption.TopDirectoryOnly))
                                {
                                    request.AddFile(Path.GetFileName(filename), System.IO.File.ReadAllBytes(filename));
                                }
                            }                            
                        }

                        client.CreateFileVersion(int.Parse(fileId), request).Wait();
                    }
                    break;
                default:
                    Console.WriteLine("Unknown action " + action);
                    break;
            }
            //var client = new ApiClient(args[0], args[1]);

            // System

            // System/Groups
            //var allGroups = client.GetGroups().ToList();

            // CMS
            //var page = client.CreatePage(1, new CreatePageRequest { category = 1, author = 183, fields = new Dictionary<int, string> { { 1, "Test Content" }, { 2, "Test Data" } } } ).Result;
            //var page = client.CreatePage(1, new CreatePageRequest { category = 1, author = 183, fields = new Dictionary<int, string> { { 1, "Test Content" }, { 2, "Test Data" } } }).Result;
            //var pages = client.GetRecords(1, new IPSClient.Objects.System.GetContentItemsRequest { categories = "1" }).ToList();
            //var response = client.GetFiles(new IPSClient.Objects.System.GetContentItemsRequest { categories = "2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,56,57,61,62,65,66,67,68,69,70,71,72,73,74,75,76,77,78" });
            //foreach (var item in response.ToList())
            //{
            //    Console.WriteLine(item.title);
            //}

            // Gallery
            //var albums = client.GetAlbums(new IPSClient.Objects.Gallery.GetAlbumsRequest()).ToList();
            //var album = client.GetAlbum(324).Result;
            //var images = client.GetImages(new IPSClient.Objects.Gallery.GetImagesRequest { albums = "324" }).ToList();
            //var image = client.GetImage(15).Result;            
        }
    }
}