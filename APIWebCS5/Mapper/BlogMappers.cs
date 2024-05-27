using APIWebCS5.DTOs.Blog;
using APIWebCS5.Service;
using ASMCS4.Service;

namespace APIWebCS5.Mapper
{
    public class BlogMappers
    {
        public static string SaveImageAndGetLink(IFormFile imageFile)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            var filePath = Path.Combine(PathImage.PathDetal, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }
            // Rendered size hỉnh ảnh cho card product
            CustomImage.AddImageCartProduct(filePath, uniqueFileName);

            CustomImage.AddAvatarProduct(filePath, uniqueFileName);

            return uniqueFileName;
        }
        public static Blog AddNewBlog(BlogDTO blogDTOs)
        {
            string imageUrl = SaveImageAndGetLink(blogDTOs.formFile); // Assuming SaveImageAndGetLink returns a string representing the image URL
            return new Blog
            {
                Headline = blogDTOs.Healine,
                DatePush = DateTime.Now,
                Content = blogDTOs.Content,
                Image = imageUrl
            };
        }

        // class mappers with class blogNotImageDTS
        public static Blog MappresNotImages(BlogNoImage model, Blog blogs)
        {
            blogs.Headline = model.Healine;
            blogs.Content = model.Content;
            blogs.DatePush = DateTime.Now;
            return blogs;
        }

        // class mappers with class blogNotImageDTS but have image
        public static Blog UpdateMappresImages(BlogNoImage model, Blog blogs, IFormFile image)
        { // Assuming SaveImageAndGetLink returns a string representing the image URL
            blogs.Headline = model.Healine;
            blogs.Content = model.Content;
            blogs.DatePush = DateTime.Now;
            blogs.Image = SaveImageAndGetLink(image);

            return blogs;
        }
    }
}
