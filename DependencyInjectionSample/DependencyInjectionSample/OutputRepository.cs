using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionSample
{
    public class OutputRepository : IRepository
    {
        public void DoSomething()
        {
            Debug.WriteLine("Do something");
        }
    }
}
