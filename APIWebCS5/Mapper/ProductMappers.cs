using APIWebCS5.DTOs.Product;
using APIWebCS5.Service;
using ASMCS4.Service;

namespace APIWebCS5.Mapper
{
    public class ProductMappers
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

            // Rendered size hình ảnh cho form admin
            CustomImage.AddAvatarProduct(filePath, uniqueFileName);

            return uniqueFileName;
        }

        public static Product MapToProduct(ProductWithImageDTO product)
        {
            return new Product
            {
                Name = product.Name,
                Price = product.Price,
                Status = "còn hàng",
                Stock = product.Stock,
                Description = product.Description,
                CategoryId = product.IdCategory,
                Hair = product.Hair,
                StatusHair = product.StatusHair,
                Sex = product.Sex,
                Size   = product.Size,
                Color = product.Color,
                Popular = product.Popular,
            };
        }
        public static List<Image> MapToImageList(ProductWithImageDTO model)
        {
            var imageList = new List<Image>();
            foreach (var imageFile in model.Images)
            {
                var image = new Image
                {
                    Link = SaveImageAndGetLink(imageFile)
                };
                imageList.Add(image);
            }
            return imageList;
        }
        public static List<Medium> MaptoMedia(int productId, List<Image> imagesList)
        {
            var mediaList = new List<Medium>();
            bool isFirstimage = true;
            foreach (var image in imagesList)
            {
                var media = new Medium
                {
                    IdProduct = productId,
                    IdImage = image.Id,
                    IsPrimary = isFirstimage,
                };
                mediaList.Add(media);
                isFirstimage = false;

            }
            return mediaList;
        }
        public static Product UpdateProduct(ProductWithImageDTO model, Product product)
        {
            product.Name = model.Name;
            product.Price = model.Price;
            product.Status = model.Status;
            product.Stock = product.Stock;
            product.Description = model.Description;
            product.Color = model.Color;
            product.Hair = model.Hair;  
            product.Popular = model.Popular;
            product.Sex = model.Sex;
            product.Size = model.Size;
            product.StatusHair = model.StatusHair;
            product.CategoryId = model.IdCategory;
            return product;
        }
    }
}
