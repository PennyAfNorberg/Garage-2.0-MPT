using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Garage_2._0_MPT.Models
{
    public class Garage_2_0_MPTContext : DbContext
    {
        public Garage_2_0_MPTContext (DbContextOptions<Garage_2_0_MPTContext> options)
            : base(options)
        {
        }

        public DbSet<Garage_2._0_MPT.Models.ParkedVehicle> ParkedVehicle { get; set; }
    }
}
