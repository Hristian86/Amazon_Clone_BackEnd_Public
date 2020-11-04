namespace AkciqApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Message
    {
        public Message()
        {
        }

        public Message(string errors)
        {
            this.error = errors;
        }
        public string message { get; set; }
        public string error { get; set; }
    }
}
