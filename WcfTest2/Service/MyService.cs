using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyService" in both code and config file together.

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MyService : IMyService
    {
        public Action<string> Info;
        public string Add(int lp1, int lp2)
        {
            Info("Wywołano 'Add'.");
            return "Suma: " + (lp1 + lp2).ToString();            
        }
    }
}
