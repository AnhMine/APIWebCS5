using ASMCS4.Service;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace APIWebCS5.Service
{

    public class CustomImage
    {
        // Rendered size kích thước hình ảnh cho card product
        public static void AddImageCartProduct(string filePath, string uniqueFileName)
        {
            string outPath = PathImage.PathCart;
            if (!!Directory.Exists(outPath))
            {
                Directory.CreateDirectory(outPath);
            }
            string path = Path.Combine(outPath, uniqueFileName);

            using (var image = SixLabors.ImageSharp.Image.Load(filePath))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(450, 450),
                    Mode = ResizeMode.Max
                }));
                image.Save(path);
            }
        }

        // Rendered size kích thước hình ảnh cho avartar product
        public static void AddAvatarProduct(string filePath, string uniqueFileName)
        {
            string outPath = PathImage.PathAvtar;
            if (!!Directory.Exists(outPath))
            {
                Directory.CreateDirectory(outPath);
            }
            string path = Path.Combine(outPath, uniqueFileName);

            using (var image = SixLabors.ImageSharp.Image.Load(filePath))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(100, 100),
                    Mode = ResizeMode.Max
                }));
                image.Save(path);
            }
        }

        public static void DeleteImage(string fileName, string PathFile)
        {
            try
            {
                if (!!Directory.Exists(PathFile))
                {
                    Directory.CreateDirectory(PathFile);
                }
                string filePath = Path.Combine(PathFile, fileName);
                // Kiểm tra xem tệp có tồn tại hay không
                if (File.Exists(filePath))
                {
                    // Xóa tệp
                    File.Delete(filePath);
                }
            }
            catch (IOException exception)
            {
                Console.WriteLine($"Lỗi xóa tệp: {exception.Message}");
            }
        }
    }
}
