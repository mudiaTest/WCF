using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using Shared;

namespace Service2Library
{
    // Definicje metod z interface
    /*Aby dodać */

    public class MyService21 : IMyService2
    {
        /*event raportujący do serwera*/
        public delegate void MessageEventHandler(string message);
        public event MessageEventHandler outputMessage;

        public string Add2(int lp1, int lp2)
        {
            if (outputMessage != null)
                outputMessage(string.Format("[public event MessageEventHandler outputMessage] Wywołano '{0}' na obiekcie service '{1}'.",
                                            this.GetType().FullName + '.' + System.Reflection.MethodBase.GetCurrentMethod().Name,
                                            this.GetHashCode().ToString()));
            return "Suma21: " + (lp1 + lp2).ToString();
        }
    }

    [MyServiceBehaviorAttribute]
    public class MyService22 : IMyService2
    {
        /*event raportujący do serwera*/
        public delegate void MessageEventHandler(string message);
        public event MessageEventHandler outputMessage;

        public string Add2(int lp1, int lp2)
        {
            if (outputMessage != null) 
                outputMessage(string.Format("[public event MessageEventHandler outputMessage] Wywołano '{0}' na obiekcie service '{1}'.",
                                            this.GetType().FullName + '.' + System.Reflection.MethodBase.GetCurrentMethod().Name,
                                            this.GetHashCode().ToString()));
            return "Suma22: " + (lp1 + lp2).ToString();
        }
    }

    /*Customowy behavior 
     * Może być użyty poprzez dodanie go do hosta kodem
     *   MyBehavior behavior = new MyBehavior();
     *   behavior.Info = Info11;
     *   host.Description.Behaviors.Add(behavior);
     * rozszerza:
     * - IServiceBehavior(aby był behavior)
     * - IInstanceProvider (interfejs metod tworzenia instancji service przez servicehost)*/
    public class MyBehavior : IServiceBehavior, IInstanceProvider
    {
        public Action<string> Info;

        /*IServiceBehavior*/
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        /*Najczeście wykorzystywane miejsce do dostępu i modyfikacji servicehost i serviceDescription*/
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher cd in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher ed in cd.Endpoints)
                {
                    ed.DispatchRuntime.InstanceProvider = this;
                }
            }
        }

        /*Pozwala zajrzeć do servicehost i serviceDescription aby sprawdzić czy servicehost działa poprawnia.
          Rzucając wyjątek możemy zablokować tworzenie servicehost*/
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
           //Info(serviceHostBase.GetHashCode().ToString());
        }

        /*IInstanceProvider*/
        /*Metoda oddająca service*/
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return this.GetInstance(instanceContext);
        }

        /*Metoda oddająca service*/
        public object GetInstance(InstanceContext instanceContext)
        {
            MyService21 service = new MyService21();
            service.outputMessage +=  new MyService21.MessageEventHandler(Info);
            return service;
        }

        /*Metoda usuwająca service*/
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            //Info(instance.GetHashCode().ToString());
        }
    }

    /*Customowy behavior - podobnie jak wyżej
     * Dodatkowo dziedziczy po Attribute dzięki czemu może być dodany poprzez jako atrybut kodem:
     *   [MyServiceBehaviorAttribute]
     *   public class MyService22 : IMyService2
     */ 
    public class MyServiceBehaviorAttribute : Attribute, IServiceBehavior, IInstanceProvider
    {
        public Action<string> InfoDummy;

        /*IServiceBehavior*/
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        /*Najczeście wykorzystywane miejsce do dostępu i modyfikacji servicehost i serviceDescription*/
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher cd in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher ed in cd.Endpoints)
                {
                    ed.DispatchRuntime.InstanceProvider = this;
                }
            }
        }

        /*Pozwala zajrzeć do servicehost i serviceDescription aby sprawdzić czy servicehost działa poprawnia.
          Rzucając wyjątek możemy zablokować tworzenie servicehost*/
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            //Info(serviceHostBase.GetHashCode().ToString());
        }

        /*IInstanceProvider*/
        /*Metoda oddająca service*/
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return this.GetInstance(instanceContext);
        }

        /*Metoda oddająca service*/
        public object GetInstance(InstanceContext instanceContext)
        {
            MyService22 service = new MyService22();
            /*Dodanie jako atrybut powoduje że event musimy przekazać poprzez Servicehost. W tym celu budujemy MyServiceHost z  Action przechowującym Info*/
            service.outputMessage += new MyService22.MessageEventHandler( ((MyServiceHost)(instanceContext.Host)).Info );
            /*Alternatywnie można postąpić jak w przypadku MyBehavior, czyli:
            service.outputMessage += new MyService22.MessageEventHandler(InfoDummy);
            */
            return service;
        }

        /*Metoda usuwająca service*/
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            //Info(instance.GetHashCode().ToString());
        }
    }
}
