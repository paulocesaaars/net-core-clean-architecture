using System.ComponentModel.DataAnnotations;

namespace Deviot.Administration.Api.ViewModel
{
    public class NewUserViewModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(150)]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
