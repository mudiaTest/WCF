using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Service2Library;
using Shared;
using BehaviorTest;
using System.ServiceModel.Description;

namespace Service
{
    class ServiceInitializer
    {
        private Action<string> Info;
        private Action<string> Info11;
        private Action<string> Info12;
        
        public ServiceInitializer(Action<string> aInfo, Action<string> aInfo11, Action<string> aInfo12)
        {
            Info = aInfo;
            Info11 = aInfo11;
            Info12 = aInfo12;
        }

        /*Inicjalizacja service zawartej w innym projekcie
         * - KONIECZNE JEST dodanie do referncji .dll z definicją serwisu
         *Standarowe wywołanie servicehost z typem
         *Dodanie customowego Behavior w runtime, który spowoduje przekazanie funcji do eventu.
         *W ten sposób InstanceContextMode może mieć dowolną wartość, bo behavior wypełni event na obiekcie service 
         */
        internal void InitService21()
        {
            ServiceHost host;
            try
            {
                host = new ServiceHost(typeof(MyService21));
                /*Przekazanie precedury eventu przez behavior dodany w runtime*/
                MyBehavior behavior = new MyBehavior();
                behavior.Info = Info11;
                host.Description.Behaviors.Add(behavior);
                host.Open();
                Info("Service21 online.");
            }
            catch (Exception ex)
            {
                host = null;
                Info(string.Format("There is an issue with MyService21: '" + ex.Message + "'"));
            }
        }

        /*Jak wyżej, ale behavior dodany jako atrybut*/
        internal void InitService22()
        {
            MyServiceHost host;
            try
            {
                /*Przekazanie precedury eventu z użyciem behavior dodany jako Atrybut, za pośrednictwem customowego Servicehost*/
                host = new MyServiceHost(typeof(MyService22));
                host.Info = Info11;
                host.Open();
                Info("Service22 online.");
            }
            catch (Exception ex)
            {
                host = null;
                Info(string.Format("There is an issue with MyService22: '" + ex.Message + "'"));
            }
        }

        /*Inicjalizacja service zawartej w serwerze 
           - NIE MA POTRZEBY dodawania niczego do references 
          Wywołanie Servicehost z obiektem service zamiast typeof(service)
           - to podejście wymaga [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
           - serice jest single, czyli zawsze jedno
           - umożliwia ustawienie w obiekcie service eventów, zmiannych, metod          
             - umożliwia logowanie*/
        internal void InitService1()
        {
            ServiceHost host;
            try
            {
                /*W tym przypadku tworzymy obiekt service i przekazujemy go do konstruktora hosta*/
                MyService service = new MyService();

                /*Mając obiekt service możemy dodać do niego eventy, które umożliwią np logowanie call w serwerze*/
                service.outputMessage += new MyService.MessageEventHandler(Info11);
                service.outputMessage += new MyService.MessageEventHandler(Info12);
                host = new ServiceHost(service);
                host.Open();
                Info("Service online.");
            }
            catch (Exception ex)
            {
                host = null;
                Info(string.Format("There is an issue with MyService: '" + ex.Message + "'"));
            }
        }

        /*Inicjalizacja service zawartej w w innym projekcie
           - KONIECZNE JEST dodanie do referncji .dll z definicją serwisu
          Wywołanie service ze static Action 
           - to podejście NIE wymaga [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
           - umożliwia ustawienie klasy service static metody
             - umożliwia logowanie
             - UWAGA! Jeśli w ustawionej metodzie NIE MOŻEMY odwoływać się do kontrolek, bo te 
               zostały utworzone w innym wątku. Do takiej akcji powinniśmy używać eventów - patrz InitService2*/
        internal void InitService3()
        {
            ServiceHost host;
            try
            {
                host = new ServiceHost(typeof(MyService3));
                //MyService3.Info = Info;                
                host.Open();
                Info("Service3 online.");
            }
            catch (Exception ex)
            {
                host = null;
                Info(string.Format("There is an issue with MyService3: '" + ex.Message + "'"));
            }
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
                Info(string.Format("There is an issue with MyService22: '" + ex.Message + "'"));
            }
        }
    }
}
