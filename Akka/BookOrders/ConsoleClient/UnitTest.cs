using BookOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.TestKit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Akka.TestKit.VsTest;

namespace ConsoleClient
{
    [TestClass]
    public class UnitTest : TestKit
    {
        [TestMethod]
        public void OrderProcessorActor_end_to_end()
        {
            var message = new PlaceOrder(12345, 10, 25, 5000);

            var orderProcessorActor = ActorOfAsTestActorRef(
                        () => new OrderProcessorActor(), TestActor);

            orderProcessorActor.Tell(message);

            Assert.IsTrue(ExpectMsg<AccountCharged>().Success);
        }
    }
}
