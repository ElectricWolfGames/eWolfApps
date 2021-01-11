using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace SystemTrayTools.Data
{
    public class ServerNameStatus
    {
        private static readonly List<ServerNameStatus> serverList = new List<ServerNameStatus>()
        {
            // Need to take this from a locla file - so we don't need to commit the names
        };

        public ServerNameStatus(string name, string address)
        {
            Name = name;
            Address = address;
            Status = "Unkonwn";
        }

        public static List<ServerNameStatus> ServerList => serverList;
        public string Address { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        internal string OutputData()
        {
            return $"<li>{Name} {Address}: {Status}</li>";
        }

        internal void UpdateStatus()
        {
            try
            {
                Ping ping = new Ping();
                PingReply pr = ping.Send(Address);
                Status = pr.Status.ToString();
            }
            catch
            {
                Status = "Failed to Connect";
            }
        }
    }
}