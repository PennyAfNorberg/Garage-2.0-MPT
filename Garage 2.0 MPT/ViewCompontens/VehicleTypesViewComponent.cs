using Garage_2._0_MPT.Models;
using Garage_2._0_MPT.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2._0_MPT.ViewCompontens
{
    public class VehicleTypesViewComponent : ViewComponent
    {
        private Garage_2_0_MPTContext db;

        public VehicleTypesViewComponent(Garage_2_0_MPTContext context)
        {
            this.db = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var res = await db.VehicleTyp.ToArrayAsync();
            return View(res);
        }
    }
}
