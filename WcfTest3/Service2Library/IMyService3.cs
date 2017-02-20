using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service2Library
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMyService3" in both code and config file together.
    [ServiceContract]
    public interface IMyService3
    {
        [OperationContract]
        string Get31(int lp1);

        [OperationContract]
        string Get32(int lp1);
    }
}
