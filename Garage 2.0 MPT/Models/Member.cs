using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2._0_MPT.Models
{
    public class Member
    {
        public int Id { get; set; }
        [MaxLength(25)]
        [Display (Name ="First Name")]
        public string FirstName { get; set; }
        [MaxLength(25)]
        [Display (Name ="Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(35)]
        [Display(Name = "Street Address")]
        public string Street { get; set; }
        [MaxLength(8)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [MaxLength(20)]
        public string City { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public byte[] PassWord { get; set; }
        [NotMapped]
        public string Message { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }

        public ICollection<ParkedVehicle> ParkedVehicles { get; set; }
    }

    
}
