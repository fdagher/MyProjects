using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Customer : TypedActor, IHandle<MarkHungry>, IHandle<Bill>
    {
        public void Handle(MarkHungry message)
        {
            Console.WriteLine("Customer is hungry, let's order some food!");
            IActorRef employee = Context.ActorOf<Employee>("employee");
            employee.Tell(new BurgerMenuRequest());
            Console.WriteLine("Ordered food");
        }

        public void Handle(Bill message)
        {
            Console.WriteLine("Great! Menu is ready. Let's pay");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(3, TimeSpan.FromSeconds(5), ex =>
            {
                if (ex is ApplicationException)
                {
                    return Directive.Restart;
                }

                return Directive.Escalate;
            });
        }
    }
}
