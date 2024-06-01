using APIWebCS5.DTOs.Order;

namespace APIWebCS5.Mapper
{
    public class OrderMappers
    {
        public static List<DetailOrder> MapToDetailOrder(List<OrderItemDTO> orderItems, int id)
        {
            var list = new List<DetailOrder>();
            foreach (var item in orderItems)
            {
                var order = new DetailOrder();
                {
                    order.OrderId = id;
                    order.ProductId = item.IdProduct;
                    order.Quantity = item.Quantity;
                }
                list.Add(order);

            }
            return list;
        }
    }
}
