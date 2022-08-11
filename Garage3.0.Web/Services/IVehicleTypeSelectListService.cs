using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage3._0.Web.Services
{
    public interface IVehicleTypeSelectListService
    {
        Task<IEnumerable<SelectListItem>> GetVehicleTypesAsync();
    }

}
