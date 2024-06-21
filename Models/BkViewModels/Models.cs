namespace WebApi_Project_Internal.Models.BkViewModels
{

    public class Bk_VideoViewModel
    {

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Categoery { get; set; }

        public IFormFile? VideoFile { get; set; }
        public IFormFile? ImageFile { get; set; }
    }

    public class Bk_ChennelViewModel
    {
        public int UserId { get; set; }
        public string? ChannelName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Categoery { get; set; }
    }
}
