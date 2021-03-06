﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Downloads
{
    public class CreateFileRequest
    {
        public int category { get; set; }
        public int author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Dictionary<string, byte[]> files { get; set; }
        public string version { get; set; }
        public Dictionary<string, byte[]> screenshots { get; set; }
        public string prefix { get; set; }
        public string tags { get; set; }
        public DateTime? date { get; set; }
        public string ip_address { get; set; }
        public int? locked { get; set; }
        public int? hidden { get; set; }
        public int? pinned { get; set; }
        public int? featured { get; set; }

        public void AddFile(string name, byte[] content)
        {
            if (files == null)
            {
                files = new Dictionary<string, byte[]>();
            }
            files.Add(name, content);
        }

        public void AddScreenshot(string name, byte[] content)
        {
            if (screenshots == null)
            {
                screenshots = new Dictionary<string, byte[]>();
            }
            screenshots.Add(name, content);
        }
    }
}
