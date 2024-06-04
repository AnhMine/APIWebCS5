using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;

namespace APIWebCS5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetImageController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public GetImageController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("get-image-detail/{imgName}")]
        public IActionResult GetImage(string imgName)
        {
            var webRoot = _webHostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRoot, "ImageDeltail", imgName); // Đường dẫn tới hình ảnh trong thư mục wwwroot

            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/jpeg"); // Trả về hình ảnh dưới dạng file stream
            }
            else
            {
                return NotFound(); // Không tìm thấy hình ảnh
            }
        }
        [HttpGet("get-images-cart/{imageName}")]
        public IActionResult ImageCart(string imageName)
        {
            var webRoot = _webHostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRoot, "ImageCart", imageName); // Đường dẫn tới hình ảnh trong thư mục wwwroot

            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/jpeg"); // Trả về hình ảnh dưới dạng file stream
            }
            else
            {
                return NotFound(); // Không tìm thấy hình ảnh
            }
        }
        [HttpGet("get-images-avatar/{imageName}")]
        public IActionResult ImageAvatar(string imageName)
        {
            var webRoot = _webHostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRoot, "ImageAvatar", imageName); // Đường dẫn tới hình ảnh trong thư mục wwwroot

            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/jpeg"); // Trả về hình ảnh dưới dạng file stream
            }
            else
            {
                return NotFound(); // Không tìm thấy hình ảnh
            }
        }

    }
}
