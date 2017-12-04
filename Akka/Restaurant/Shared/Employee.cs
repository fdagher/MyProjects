using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Employee : TypedActor, IHandle<BurgerMenuRequest>, IHandle<SaladRequest>
    {
        public void Handle(BurgerMenuRequest message)
        {
            Console.WriteLine("Collect menu");

            Context.Parent.Tell(new Bill());
        }

        public void Handle(SaladRequest message)
        {
            throw new NotImplementedException();
        }
    }
}
