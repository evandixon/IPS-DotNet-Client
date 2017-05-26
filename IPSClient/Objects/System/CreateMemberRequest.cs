using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.System
{
    /// <summary>
    /// Request to create a member
    /// </summary>
    public class CreateMemberRequest
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int group { get; set; }
        public object customFields { get; set; }
    }
}
