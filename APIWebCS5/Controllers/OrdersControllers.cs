using APIWebCS5.DTOs.Order;
using APIWebCS5.Mapper;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;

namespace APIWebCS5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersControllers : Controller
    {
        private readonly DogAndCatContext _context;
        public OrdersControllers(DogAndCatContext context)
        {
            _context = context;
        }
        [HttpGet("get-all-orders")]
        public IActionResult GetAllOrders()
        {
            var orders = _context.Orders.ToList();
            return Ok(orders);
        }
        [HttpGet("get-order-to-account/account/{id}/status/{type}")]
        public IActionResult GetOrderWithAccount(int id, int type)
        {
            var orders = _context.Orders.Where(a => a.AccountId == id && a.StatusDelivery == type).Select(a => new
            {
                a.Id,
                a.AccountId,
                a.Time,
                a.Total,
                a.StatusDelivery,
                detal = a.DetailOrders.Where(d => d.OrderId == a.Id)
                .Join(_context.Products, a => a.ProductId, p => p.Id, (a, p) => new
                {
                    a.Quantity,
                    p.Id,
                    p.Name,
                    p.Price,
                    link = p.Media
                            .Where(m => m.IsPrimary == true)
                            .Join(_context.Images, m => m.IdImage, i => i.Id, (m, i) => new
                            {
                                i.Link
                            })
                })
            }).OrderBy(p => p.StatusDelivery).ThenByDescending(a => a.Id);


            return Ok(orders);
        }
        [HttpGet("get-orders-by-type/type/{type}")]
        public IActionResult GetOrder(int type)
        {
            switch (type)
            {
                case 1:
                    var orderPending = _context.Orders.Where(a => a.StatusDelivery == 1).OrderByDescending(p => p.Id).ToList();
                    return Ok(orderPending);
                case 2:
                    var orderDelivery = _context.Orders.Where(a => a.StatusDelivery == 2).OrderByDescending(p => p.Id).ToList();
                    return Ok(orderDelivery);
                case 3:
                    var orderHistory = _context.Orders.Where(a => a.StatusDelivery == 3 || a.StatusDelivery == 4).OrderByDescending(p => p.Id).ToList();
                    return Ok(orderHistory);
                default:
                    return Ok("Không tìm thấy");
            }

        }
		[HttpGet("get-order-by-id/{id}")]
		public async Task<IActionResult> GetOrderWithIdAsync(int id)
		{
			var order = await _context.Orders
				.Where(a => a.Id == id)
				.Select(a => new
				{
					a.Id,
					a.AccountId,
					a.Time,
					a.Total,
					a.StatusDelivery,
					detal = a.DetailOrders
						.Join(_context.Products, a => a.ProductId, p => p.Id, (a, p) => new
						{
							a.Quantity,
							p.Id,
							p.Name,
							p.Price,
						})
						.Join(_context.Media, p => p.Id, m => m.IdProduct, (p, m) => new
						{
							p.Quantity,
							p.Id,
							p.Name,
							p.Price,
							m.IsPrimary,
							m.IdImage
						}).Where(a => a.IsPrimary == true)
						.Join(_context.Images, m => m.IdImage, i => i.Id, (m, i) => new
						{
							m.Quantity,
							m.Id,
							m.Name,
							m.Price,
							i.Link
						}).ToList()
				})
				.SingleOrDefaultAsync();

			if (order == null)
			{
				return NotFound();
			}
			return Ok(order);
		}

		[HttpPut("update-status-orders/{type}/id/{id}")]
        public IActionResult UpdateStatusDelivery(int id, int type)
        {
            var orders = _context.Orders.FirstOrDefault(a => a.Id == id);
            if (orders == null)
            {
                return Ok("Không tìm thấy!!");
            }
            else
            {
                switch (type)
                {
                    case 1:
                        orders.StatusDelivery = 2;
                        orders.Time = DateTime.Now;
                        _context.Orders.Update(orders);
                        _context.SaveChanges();
                        return Ok(new SingleRespone { Code = 200, Message = " ok" });
                    case 2:
                        orders.StatusDelivery = 3;
                        orders.Time = DateTime.Now;
                        _context.Orders.Update(orders);
                        _context.SaveChanges();
                        return Ok(new SingleRespone { Code = 200, Message = " ok" });
                    case 3:
                        orders.StatusDelivery = 4;
                        orders.Time = DateTime.Now;
                        _context.Orders.Update(orders);
                        _context.SaveChanges();
                        return Ok(new SingleRespone { Code = 200, Message = " ok" });
                    default:
                        return Ok("Không tìm thấy");

                }
            }
        }

        [HttpPost("orders")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDTO orderDto)
        {
            try
            {
                var order = new Order
                {
                    AccountId = (int)orderDto.IdAccount,
                    Total = (decimal)orderDto.TotalAmount,
                    Time = DateTime.Now,
                    StatusOrder = 0,
                    StatusDelivery = 1,
                    PayMentMethod = 0
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                var detalOrder = OrderMappers.MapToDetailOrder(orderDto.OrderItems, order.Id);


                foreach (var detailOrder in detalOrder)
                {
                    _context.DetailOrders.Add(detailOrder);
                    await _context.SaveChangesAsync();

                }
                return Ok(new SingleRespone { Code = 200, Message = " ok" });
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bug: {ex.Message}");
            }
        }



    }
}
