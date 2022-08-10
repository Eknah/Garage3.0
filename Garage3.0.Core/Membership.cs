using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage3._0.Core
{
#nullable disable
    public class Membership
    {
        public int Id { get; set; }
        public int PersonNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Boolean Pro { get; set; }
        public ICollection<Vehicle>Vehicles { get; set; } = new List<Vehicle>();
    }
}
