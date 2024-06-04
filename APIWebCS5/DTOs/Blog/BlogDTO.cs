namespace APIWebCS5.DTOs.Blog
{
    public class BlogDTO
    {
        public string? Healine { get; set; }
        public string? Content { get; set; }
        public IFormFile? formFile { get; set; }

        public int IdUser { get; set; };
    }
}
