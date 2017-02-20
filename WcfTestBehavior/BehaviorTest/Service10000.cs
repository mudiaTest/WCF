using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace BehaviorTest
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, 
                     ConcurrencyMode = ConcurrencyMode.Multiple,                     
                     UseSynchronizationContext = false)]
    public class Service10000 : IService10000
    {
        public string GetData(int value)
        {
            throw new ArgumentNullException("composite");
            return string.Format("GetData: {0}", value);            
        }

        public string GetData2(int value)
        {
            Thread.Sleep(2000);
            return string.Format("GetData2: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
