using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BehaviorTest;
using System.ServiceModel.Description;

namespace Service
{
    class ServiceInitializer
    {
        private Action<string> Info;
        
        public ServiceInitializer(Action<string> aInfo)
        {
            Info = aInfo;
        }

        internal void InitService10000()
        {
            ServiceHost host;
            try
            {
                /*Przekazanie precedury eventu z użyciem behavior dodany jako Atrybut, za pośrednictwem customowego Servicehost*/
                host = new ServiceHost(typeof(Service10000));
                //host.Description.Behaviors.
                host.Open();
                Info("Service10000 online.");
            }
            catch (Exception ex)
            {
                host = null;
                Info(string.Format("There is an issue with Service10000: '" + ex.Message + "'"));
            }
        }
    }
}
