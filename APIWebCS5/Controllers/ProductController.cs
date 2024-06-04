using APIWebCS5.DTOs.Product;
using APIWebCS5.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace APIWebCS5.Controllers
{
    [ApiController]
    [Route("*api/[controller]")]
    public class ProductController : Controller
    {
        private readonly DogAndCatContext _context;
        public ProductController(DogAndCatContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetALlProduct()
        {
            var product = _context.Products.Select(a => new
            {
                a.Name,
                a.Price,
                a.Id,
                a.Status,
                a.Stock,
                a.Hair,
                a.CategoryId,
                a.Description,
                a.StatusHair,
                a.Popular,
                a.Size,
                a.Sex ,
                link = a.Media
                    .Where(m => m.IsPrimary == true)
                    .Join(_context.Images, m => m.IdImage, i => i.Id, (m, i) => new
                    {
                        i.Link
                    })

            }).OrderByDescending(a => a.Id);
            return Ok(product);
        }
        [HttpGet("idCategory/{idCategory}/page/{pageNumber}")]
        public IActionResult Page(int idCategory, string pageNumber)
        {
            if (!int.TryParse(pageNumber, out int pageNum))
            {
                return NotFound();
            }

            if (pageNum <= 0)
            {
                pageNum = 1;
            }

            try
            {
                int limit = 12; // số lượng sản phẩm trên mỗi trang

                int skip = (pageNum - 1) * limit;
                if (idCategory == 0)
                {
                    // nếu idCaregory == 0 thì phân trang như bình thường
                    var products = _context.Products.Where(a => a.Status == "còn hàng").Select(a => new
                    {
                        a.Name,
                        a.Price,
                        a.Id,
                        a.CategoryId,
                        link = a.Media
                        .Where(m => m.IsPrimary == true)
                        .Join(_context.Images, m => m.IdImage, i => i.Id, (m, i) => new
                        {
                            i.Link
                        })

                    })
                    .OrderByDescending(p => p.Id)
                    .Skip(skip)
                    .Take(limit)
                    .ToList();
                    return Ok(products);
                }
                else
                {
                    // Nếu idCategory != 0 thì phân trang theo loại sản phẩm
                    var products = _context.Products.Where(a => a.CategoryId == idCategory && a.Status == "còn hàng").Select(a => new
                    {
                        a.Name,
                        a.Price,
                        a.Id,
                        a.CategoryId,
                        link = a.Media
                        .Where(m => m.IsPrimary == true)
                        .Join(_context.Images, m => m.IdImage, i => i.Id, (m, i) => new
                        {
                            i.Link
                        })

                    })
                    .OrderByDescending(p => p.Id)
                    .Skip(skip)
                    .Take(limit)
                    .ToList();

                    return Ok(products);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var product = _context.Products.Where(a => a.Id == id).Select(a => new
            {
                a.Name,
                a.Price,
                a.Id,
                a.Status,
                a.Stock,
                a.Hair,
                a.CategoryId,
                a.Description,
                a.StatusHair,
                a.Popular,
                a.Size,
                a.Sex,
                a.Color,
                link = a.Media
                .Join(_context.Images, m => m.IdImage, i => i.Id, (m, i) => new
                {
                    i.Link
                })

            });
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // get value product using class dto and mappers
        [HttpGet("get-product")]
        public async Task<IActionResult> GetProduct()
        {
            List<Product> products = new List<Product>();
            await Task.Run(() =>
            {
                products = _context.Products.ToList();
            });
            List<Medium> sizes = new List<Medium>();
            foreach (var product in products)
            {
                Medium medium = _context.Media.FirstOrDefault(a => a.Id == (product.Id) && a.IsPrimary == true);
                sizes.Add(medium);
            }
            return Ok(sizes);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductWithImage([FromForm] ProductWithImageDTO model)
        {
            try
            {
                var product = ProductMappers.MapToProduct(model);
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                var imagesList = ProductMappers.MapToImageList(model);

                foreach (var image in imagesList)
                {
                    _context.Images.Add(image);
                    await _context.SaveChangesAsync();
                }

                var mediaList = ProductMappers.MaptoMedia(product.Id, imagesList);

                foreach (var media in mediaList)
                {
                    _context.Media.Add(media);
                    await _context.SaveChangesAsync();
                }

                return Ok(new SingleRespone { Code = 200, Message = " ok" });
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                return StatusCode(500, $"Bug: {errorMessage}");
            }

        }
        [HttpPut("product/{productId}")]
        public async Task<IActionResult> UpdateProductWithImages(int productId, [FromForm] ProductWithImageDTO model)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId);

                if (product == null)
                {
                    return NotFound("Product not found");
                }
                if (model.Images == null)
                {

                }    
                var prd = ProductMappers.UpdateProduct(model, product);
                _context.Products.Update(prd);
                var mediaList = await _context.Media.Where(m => m.IdProduct == productId).ToListAsync();
                _context.Media.RemoveRange(mediaList);
                await _context.SaveChangesAsync();
                var imagesList = ProductMappers.MapToImageList(model);

                foreach (var image in imagesList)
                {
                    _context.Images.Add(image);
                    await _context.SaveChangesAsync();
                }

                var mediaList2 = ProductMappers.MaptoMedia(product.Id, imagesList);

                foreach (var media in mediaList2)
                {
                    _context.Media.Add(media);
                    await _context.SaveChangesAsync();
                }

                return Ok(new SingleRespone { Code = 200,Message = " ok"}) ;// singlerespone



            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
