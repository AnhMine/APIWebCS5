namespace APIWebCS5.DTOs.Order
{
    public class OrderDTO
    {
        public int? IdAccount { get; set; }

        public decimal? TotalAmount { get; set; }

        public DateTime? DateTime { get; set; }


        public byte? StatusOrder { get; set; }

        public byte? StatusDelivery { get; set; }

        public byte? MethodPayment { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
