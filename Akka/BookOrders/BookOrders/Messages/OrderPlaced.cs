namespace BookOrders
{
    public class OrderPlaced
    {
        public string OrderId { get; }

        public PlaceOrder OrderInfo { get; set; }

        public OrderPlaced(string orderId, PlaceOrder orderInfo)
        {
            OrderId = orderId;
            OrderInfo = orderInfo;
        }
    }
}