using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI_ConsoleClient
{
    public class ChatEntry
    {
        public string Owner { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public ChatEntry()
        {

        }
        public ChatEntry(string owner, string message)
        {
            this.Owner = owner;
            this.Message = message;
            this.Time = DateTime.Now;
        }
    }
}
