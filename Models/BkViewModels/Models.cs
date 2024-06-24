using System.ComponentModel.DataAnnotations;

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

    public class BK_LoginViewModel
    {
        [Required, DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }
    }

    public class BK_RegesterViewModel
    {
        [Required(ErrorMessage = "Enter the user name")]
        public string? UserName { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Enter the Email")]
        public string? Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Enter the Password")]
        public string? Password { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Enter the Comform Password"), Compare("Password")]
        public string? ComformPassword { get; set; }

    }
}
