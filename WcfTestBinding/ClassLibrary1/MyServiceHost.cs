using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Shared
{
    public class MyServiceHost : ServiceHost
    {
        public MyServiceHost(Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses) { }
        public Action<string> Info;

        protected override void OnOpening()
        {
            base.OnOpening();
            //OperationContext.Current.InstanceContext.GetServiceInstance();
        }
    }
}
