using Garage3._0.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Garage3._0.Web.Services
{
    public class VehicleTypeSelectListService : IVehicleTypeSelectListService
    {
        private readonly GarageContext db;
        public async Task<IEnumerable<SelectListItem>> GetVehicleTypesAsync() => await db.VehicleType.Select(v => new SelectListItem { Text = v.Name, Value = v.Id.ToString() }).ToListAsync();

        public VehicleTypeSelectListService(GarageContext db)
        {
            this.db = db;
        }
    }
}
