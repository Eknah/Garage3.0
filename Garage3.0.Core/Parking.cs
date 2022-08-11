using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage3._0.Core
{
#nullable disable
    public class Parking
    {
        public int Id { get; set; }
        [DisplayName("Vehicle Identification")]
        public int? VehicleId { get; set; }
        [DisplayName("Parking started")]
        public DateTime ArrivalTime { get; set; } = DateTime.Now;
    }
}
