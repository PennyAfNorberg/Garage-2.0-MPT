using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Garage_2._0_MPT.Models;
namespace Garage_2._0_MPT
{
    public class GarageUser : IdentityUser
    {
        public string Locale { get; set; } = "sv-SE";
        public string Role { get; set; } = "user";
        public int MemberId { get; set; }
        public Member Member { get; set; }

    }
}
