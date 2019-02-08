using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Garage_2._0_MPT.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        public List<Member> Members { get; set; }
        public int MemberId { get; set; }
    }
}
