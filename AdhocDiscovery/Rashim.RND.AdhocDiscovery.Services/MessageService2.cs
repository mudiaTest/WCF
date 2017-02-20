using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Rashim.RND.AdhocDiscovery.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class MessageService2 : IMessageService2
    {
        public string GetMessage2(string submittedMsg)
        {
            return "Server Reply 2: " + submittedMsg;
        }
    }
}
