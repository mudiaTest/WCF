using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WcfServiceLibrarySimpleTest
{
    class MyServiceHost : ServiceHost
    {
        public Func<string> Info;
    }
}
