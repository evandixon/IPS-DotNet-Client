using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Downloads
{
    public class NewFileVersionRequest
    {
        public Dictionary<string, byte[]> files { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string version { get; set; }
        public string changelog { get; set; }
        public int? save { get; set; }
        public Dictionary<string, byte[]> screenshots { get; set; }

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
