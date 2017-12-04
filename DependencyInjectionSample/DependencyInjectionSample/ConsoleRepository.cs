using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionSample
{
    public class ConsoleRepository : IRepository
    {
        public void DoSomething()
        {
            Console.WriteLine("Do something");
        }
    }
}
