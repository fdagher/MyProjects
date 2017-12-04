using Akka.Actor;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ActorSystem actorSystem = ActorSystem.Create("FastFoodRestaurant"))
            {
                IActorRef customer = actorSystem.ActorOf(Props.Create<Customer>(), "customer");
                customer.Tell(new MarkHungry());
                Console.ReadLine();
            };
        }
    }
}
