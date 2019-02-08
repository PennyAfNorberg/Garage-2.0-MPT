using System.ComponentModel.DataAnnotations;

namespace Garage_2._0_MPT.Models
{
    public class TwoFactorModel
    {
        [Required]
        public string Token { get; set; }
    }
}
