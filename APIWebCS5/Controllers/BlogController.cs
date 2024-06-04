using APIWebCS5.DTOs.Blog;
using APIWebCS5.Mapper;
using APIWebCS5.Service;
using ASMCS4.Service;
using Microsoft.AspNetCore.Mvc;

namespace APIWebCS5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogControllers : Controller
    {
        private readonly DogAndCatContext _context;
        public BlogControllers(DogAndCatContext context)
        {
            _context = context;
        }
        [HttpGet("get-all")]
        public IActionResult Index()
        {
            List<Blog> blogs = _context.Blogs.OrderByDescending(a=>a.Id).ToList();  
            return Ok(blogs);
        }
        [HttpGet("get-blog-by-id/{id}")]
        public IActionResult GetBlogById(int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            if(blog == null)
            {
                return BadRequest();
            }
            return Ok(blog);
        }
        [HttpPost("add-new-blog")]
        public async Task<IActionResult> AddNewBlog([FromForm]BlogDTO blog)
        {
            try
            {
                var blogs = BlogMappers.AddNewBlog(blog);
                _context.Add(blogs);
                await _context.SaveChangesAsync();
                return Ok(new SingleRespone { Code = 200, Message = " ok" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-blog/{id}")]
        public async Task<IActionResult> UpdateBlog(int id, [FromForm]BlogNoImage blog, IFormFile? img)
        {
            var blogList =  await _context.Blogs.Where(a=>a.Id == id).FirstOrDefaultAsync();
            if(blogList == null)
            {
                return BadRequest($"Blog {id} was not found.");
            }
            else
            {
                if(img == null)
                {
                    blogList = BlogMappers.MappresNotImages(blog, blogList);
                }
                else
                {
                    CustomImage.DeleteImage(blogList.Image, PathImage.PathDetal);
                    CustomImage.DeleteImage(blogList.Image, PathImage.PathAvtar);

                    blogList = BlogMappers.UpdateMappresImages(blog, blogList, img);

                    // Update new blog enter database
                    _context.Update(blogList);
                    await _context.SaveChangesAsync();
                }
                return Ok(new SingleRespone { Code = 200, Message = " ok" });
            }
        }
        [HttpDelete("remove-blog/{id}")]
        public async Task<IActionResult> RemoveBlog(int id)
        {
            Blog blogs = _context.Blogs.FirstOrDefault(b => b.Id == id);
            if(blogs == null)
            {
                return BadRequest();
            }
            else
            {
                await Task.Run(() => CustomImage.DeleteImage(blogs.Image, PathImage.PathDetal));
                //delete image in folder image
                await Task.Run(() => CustomImage.DeleteImage(blogs.Image, PathImage.PathAvtar));
                _context.Blogs.Remove(blogs);
                await _context.SaveChangesAsync();
                return Ok(new SingleRespone { Code = 200, Message = " ok" });
            }
        }
    }
}
