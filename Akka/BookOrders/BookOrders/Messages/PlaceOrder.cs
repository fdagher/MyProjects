namespace BookOrders
{
    public class PlaceOrder
    {
        public int AccountId { get; }
        public int ItemId { get; }
        public int Quantity { get; }
        public int ExtPrice { get; }
        public PlaceOrder(int accountId, int itemId, int quantity, int extPrice)
        {
            AccountId = accountId;
            ItemId = itemId;
            Quantity = quantity;
            ExtPrice = extPrice;
        }
    }
}