using APIWebCS5.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace APIWebCS5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryControllers : Controller
    {
        private readonly DogAndCatContext _context;
        public CategoryControllers(DogAndCatContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetCategory()
        {
            var cate = _context.Categories.Select(a => new
            {
                a.Name,
                a.Id,
                a.Products.Count,
            });
            return Ok(cate);
        }

        // Thêm loại sản phẩm
        [HttpPost]
        public IActionResult AddNewCategory([FromForm] string name)
        {
            Category category = new Category();
            category.Name = name;
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok(new SingleRespone { Code = 200, Message = " ok" });
        }

        // Cập nhật thông tin loại sản phẩm
        [HttpPut("Update/{id}")]
        public IActionResult UpdateCategory(int id, [FromForm] string newName)
        {
            var category = _context.Categories.FirstOrDefault(a => a.Id == id);
            category.Name = newName;
            _context.Categories.Update(category);
            _context.SaveChanges();
            return Ok(new SingleRespone { Code = 200, Message = " ok" });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            Category category = _context.Categories.FirstOrDefault(a => a.Id == id);
            if (category == null) return NotFound();
            else
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return Ok(new SingleRespone { Code = 200, Message = " ok" });
            }
        }

    }
}
