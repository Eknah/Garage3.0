using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage3._0.Core
{
#nullable disable
    public class Parking
    {
        public int Id { get; set; }
        public int? VehicleId { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
