using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class MessageService :IMessageServices
    {
        public string GetMessage(string submittedMessages)
        {
            if(!string.IsNullOrEmpty(submittedMessages))
            {
                return "Reply from server " + submittedMessages;
            }
            return string.Empty;
        }
    }
}
