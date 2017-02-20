using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyService" in both code and config file together.

    //Ten SB określa sposób utworzenia i utrzymania instancji service przez serwer - Per call, Per session, Single
    //https://www.codeproject.com/Articles/86007/ways-to-do-WCF-instance-management-Per-call-Per
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MyService : IMyService
    {
        public MyService()
        {
            int i = 0;
        }

        public delegate void MessageEventHandler(string message);
        public event MessageEventHandler outputMessage;

        [MyOperationBehavior]
        public string Add(int lp1, int lp2)
        {
            outputMessage(string.Format("[public event MessageEventHandler outputMessage] Wywołano '{0}' na obiekcie service '{1}'.",
                                        this.GetType().FullName + '.' + MethodBase.GetCurrentMethod().Name,
                                        this.GetHashCode().ToString()));
            return "Suma: " + (lp1 + lp2).ToString();            
        }        
    }

    class MyOperationBehavior: Attribute, IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
            Console.WriteLine("Inside {0}.{1}, operation {2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, operationDescription.Name);
        }
 
        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            Console.WriteLine("Inside {0}.{1}, operation {2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, operationDescription.Name);
        }
 
        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            Console.WriteLine("Inside {0}.{1}, operation {2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, operationDescription.Name);
        }
 
        public void Validate(OperationDescription operationDescription)
        {
            Console.WriteLine("Inside {0}.{1}, operation {2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, operationDescription.Name);
        }
    }
}
