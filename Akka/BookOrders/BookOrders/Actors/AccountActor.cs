using System;
using Akka.Actor;
using System.Diagnostics;

namespace BookOrders
{
    public class AccountActor : ReceiveActor
    {
        public AccountActor()
        {
            Receive<ChargeCreditCard>(chargeCreditCard => ChargeCreditCardHandler(chargeCreditCard));
        }

        /// <summary>
        /// Event handler that runs once a chanrge credit card request is received. It will charge the account
        /// and inform the caller
        /// </summary>
        /// <param name="chargeCreditCard"></param>
        public void ChargeCreditCardHandler(ChargeCreditCard chargeCreditCard)
        {
            Debug.WriteLine("Charge Credit Card Request Received...");

            try
            {
                // access financial web service
                // code here
                if (chargeCreditCard.Amount < 0)
                {
                    throw new ApplicationException("Can't charge card a negative value");
                }
                // return result
                Context.Parent.Tell(new AccountCharged(chargeCreditCard, true));
            }
            catch (ApplicationException)
            {
                Context.Parent.Tell(new AccountCharged(chargeCreditCard, false));
                throw;
            }
        }
    }
}