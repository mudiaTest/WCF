using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Rashim.RND.AdhocDiscovery.Services
{
    [ServiceContract]
    public interface IMessageService
    {
        [OperationContract]
        string GetMessage(string submittedMsg);
    }
}
