using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;

namespace DependencyInjectionSample
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.LoadConfiguration();

            IRepository repo = container.Resolve<IRepository>();

            repo.DoSomething();

            Console.ReadLine();
        }
    }
}
