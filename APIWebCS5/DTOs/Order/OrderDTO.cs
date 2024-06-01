namespace APIWebCS5.DTOs.Order
{
    public class OrderDTO
    {
        public int IdAccount { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public byte StatusOrder { get; set; } = 1;

        public byte StatusDelivery { get; set; } = 1;

        public byte MethodPayment { get; set; } = 0;

        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}
