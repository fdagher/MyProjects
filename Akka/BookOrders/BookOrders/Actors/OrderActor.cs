using System;
using Akka.Actor;
using System.Diagnostics;

namespace BookOrders
{
    public class OrderActor : ReceiveActor
    {
        public OrderActor()
        {
            Receive<PlaceOrder>(placeOrder => PlaceOrderHandler(placeOrder));
        }

        /// <summary>
        /// Event handler that runs once a place order request is received. It will place the order and inform the caller
        /// of the result
        /// </summary>
        /// <param name="placeOrder"></param>
        public void PlaceOrderHandler(PlaceOrder placeOrder)
        {
            Debug.WriteLine("Order Request Received...");

            Context.Parent.Tell(new OrderPlaced(DateTime.Now.Ticks.ToString(), placeOrder));
        }
    }
}