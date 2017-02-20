using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rashim.RND.AdhocDiscovery.Services
{
    public class MessageService:IMessageService
    {
        public string GetMessage(string submittedMsg)
        {
            return "Server Reply 1: " + submittedMsg;
        }
    }
}
