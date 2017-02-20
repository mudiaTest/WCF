using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;

namespace Service2Library
{

    [ServiceBehavior(/*InstanceContextMode - sposób zarządzania instancjami service:                      
                      * - PerCall - każdy call buduje nową instancję - pozwala na pełny multithread
                      * - PerSession - na każdą sesję jest bodowany nowy obiekt, więc 2 zapytania 
                      *   z tego samego klienta będą obsługiwane przez ten sam obiekt service - pozwala 
                      *   na multuthread dla róznych sesji, jednak 2 zapytania do tego samego service 
                      *   w ramach jednej sesji (np wywołane asynchronicznie) będą wywoływane w kolejności. 
                      * - Single - wszestkie zapytania niezależnie od klienta i sesji - żadne multithread nie działa
                      * ma bezpośredni związek z multitread  
                      */
                      /*ConcurrencyMode 
                       * - Single - tylko jeden wątek ma dostęp do jednej INSTANCJI service (nie mylić z serwerem). 
                       *   Instancja jest blokowana aż do całkowitego zakończenia działania metody service
                       * - Reentrant - podobnie jak Singla, ale instancja jest odblokowywana tuz przed zrobieniem callback.
                       *   Używane w przypadku komunikacji 2-kierunkowej
                       * - Multiple - wiele wątków może mieć dostęp do jednej instancji service - tu trzeba 
                       *   uważać, aby zabezpieczyć dostęp do zmiannych etc. - ręczna synchronizacja*/
                       /* - PerCall instancing + Single concurrency - typowy scenariusz. Mutlicalls dostępne, bo 
                        *   każdy działa na osobnej instancji service.
                        * - PerCall instancing + Multiple concurrency - działa jak wyżej.
                        * - PerSession instancing + Single concurrency - Multicall całkowicie zadziała w ramach
                        *   róznych sesji, ale wewnątrz jednej sesji będzie kolejka.
                        * - PerSession instancing + Multiple concurrency - Multicall całkowicie zadziała w ramach róznych sesji.
                        *   Wewnątrz jednej sesji też będzie działał multicall, ale trzeba zadbać o synchronizację dostępu do danych.
                        * - Single instancing + Single concurrency - Jedna kolejka będzie obowiązywała wszystkich klientów
                        * - Single instancing + Multiple concurrency - Multicall zadziała całkowicie, ale w wszystkie zapytania
                        *   będą używały jednego obiektu service, więc trzeba zadbać o synchroznizację*/
                       /*Poniżej jest przykład problemów. Obiekt service ma pole, które może być zmianiane przez różne metody aby 
                        *potem tę wartość czytanać. Jedna z metod czyta ten obiekt z opóźnieniem i jeśli w tym czasie zostania wywołana
                        *funkcja bez opóźnienia, to funkcja z późnieniem odda błędną wartość.
                        *Client1->Get32(v1)
                        *Client2->Get31(v2)
                        *Client2 otrzyma v2
                        *Client1 otrzyma v2 (zamiast v1)*/

                     InstanceContextMode = InstanceContextMode.Single,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     UseSynchronizationContext = false)]
    public class MyService3 : IMyService3
    {
        int lp1;
        public static Action<string> Info;
        public string Get31(int alp1)
        {
            lp1 = alp1;
            if (Info != null)
                Info(string.Format("[Action<string> Info] Wywołano '{0}' na obiekcie service '{1}'.", 
                                   this.GetType().FullName + '.' + System.Reflection.MethodBase.GetCurrentMethod().Name,
                                   this.GetHashCode().ToString()));
            return "Otrzymano: " + (lp1).ToString();
        }

        public string Get32(int alp1)
        {
            lp1 = alp1;
            if (Info != null)
                Info(string.Format("[Action<string> Info] Wywołano '{0}' na obiekcie service '{1}'.",
                                  this.GetType().FullName + '.' + System.Reflection.MethodBase.GetCurrentMethod().Name,
                                   this.GetHashCode().ToString()));
            System.Threading.Thread.Sleep(3 * 1000);
            return "Otrzymano: " + (lp1).ToString();
        }
    }
}
