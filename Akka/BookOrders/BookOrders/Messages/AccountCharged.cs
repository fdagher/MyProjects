namespace BookOrders
{
    public class AccountCharged
    {
        public ChargeCreditCard ChargeInfo { get; }

        public bool Success { get; }

        public AccountCharged(ChargeCreditCard chargeInfo, bool success)
        {
            ChargeInfo = chargeInfo;
            Success = success;
        }
    }
}