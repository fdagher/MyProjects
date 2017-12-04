using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrders
{
    public class OrderProcessorActor : ReceiveActor
    {
        public OrderProcessorActor()
        {
            Receive<PlaceOrder>(placeOrder => PlaceOrderHandler(placeOrder));
            Receive<OrderPlaced>(orderPlaced => OrderPlacedHandler(orderPlaced));
            Receive<AccountCharged>(accountCharged => AccountChargedHandler(accountCharged));
        }

        /// <summary>
        /// Event handler that runs once a place order request is received
        /// </summary>
        /// <param name="placeOrder">The order message</param>
        private void PlaceOrderHandler(PlaceOrder placeOrder)
        {
            Debug.WriteLine("Order Request Received to Order Processor...");

            var orderActor = Context.ActorOf<OrderActor>("orderActor" + DateTime.Now.Ticks);

            orderActor.Tell(placeOrder);
        }

        /// <summary>
        /// Event handler that runs once an order is placed. It will trigger the ChargeCreditCard Actor as a result
        /// </summary>
        /// <param name="orderPlaced">The placed order result message</param>
        private void OrderPlacedHandler(OrderPlaced orderPlaced)
        {
            Debug.WriteLine("Order placed successfully...");

            var accountActor = Context.ActorOf<AccountActor>("accountActor" + orderPlaced.OrderInfo.AccountId);

            accountActor.Tell(new ChargeCreditCard(orderPlaced.OrderInfo.ExtPrice));
        }

        /// <summary>
        /// Event handler that runs once the credit card account is charged. It will inform the parent of the result
        /// </summary>
        /// <param name="accountCharged">The </param>
        private void AccountChargedHandler(AccountCharged accountCharged)
        {
            if (accountCharged.Success)
            {
                Debug.WriteLine("Account Charged Successfully...");

                // Sends to TestActor (Test) or CustomerActor (Production)
                Context.Parent.Tell(accountCharged);
            }
            else
            {
                Debug.WriteLine("Failure! Account Not Charged...");

                // Sends to TestActor (Test) or CustomerActor (Production)
                Context.Parent.Tell(accountCharged);
            }
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                Decider.From(x => {
                    if (x is ApplicationException) return Directive.Resume;
                    return Directive.Restart;
                })
            );
        }
    }
}
