using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2._0_MPT.Models
{
    public class Members
    {
        public int Id { get; set; }
        [MaxLength(25)]
        [Display (Name ="First Name")]
        public string FirstName { get; set; }
        [MaxLength(25)]
        [Display (Name ="Last Name")]
        public string LastName { get; set; }
        public Address Address { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public byte[] PassWord { get; set; }
    }

    [Owned]
    public class Address
    {
        [MaxLength(35)]
        [Display(Name = "Street Address")]
        public string Street { get; set; }
        [MaxLength(8)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [MaxLength(20)]
        public string City { get; set; }
        
    }
}
