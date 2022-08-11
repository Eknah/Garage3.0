using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage3._0.Core
{
#nullable disable
    public class Membership
    {
        public int Id { get; set; }
        [Required]
        [StringLength(12, ErrorMessage = "Person number must be 10 characters", MinimumLength = 12)]
        public string PersonNumber { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Email Address")]
        public string Email { get; set; }
        [DisplayName("Membership registration date")]
        public DateTime RegistrationDate { get; set; }
        public Boolean Pro { get; set; }
        public ICollection<Vehicle>Vehicles { get; set; } = new List<Vehicle>();
    }
}
