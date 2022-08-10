using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage3._0.Core
{
#nullable disable
    public class VehicleType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }  = new List<Vehicle>();
    }
}
