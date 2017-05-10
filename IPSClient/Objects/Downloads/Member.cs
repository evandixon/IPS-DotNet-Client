using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Downloads
{
    public class Member
    {
        public int id { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string timezone { get; set; }
        public string formattedName { get; set; }
        public Group primaryGroup { get; set; }
        public List<Group> secondaryGroups { get; set; }
        public string email { get; set; }
        public DateTime joined { get; set; }
        public string registrationIpAddress { get; set; }
        public string photoUrl { get; set; }
        public bool validating { get; set; }
        public int posts { get; set; }
        public DateTime lastActivity { get; set; }
        public DateTime lastVisit { get; set; }
        public DateTime lastPost { get; set; }
        public int profileViews { get; set; }
        public string birthday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>The API documentation says this is an array of <see cref="FieldGroup"/>, but is instead its own class with each property being of type <see cref="FieldGroup"/></remarks>
        public object customFields { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
