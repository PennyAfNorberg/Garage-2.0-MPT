using System.ComponentModel.DataAnnotations;

namespace Garage_2._0_MPT.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
