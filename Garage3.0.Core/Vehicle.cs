using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Garage3._0.Core
{
#nullable disable
	public class Vehicle
	{
		public int Id { get; set; }
		public int MembershipId { get; set; }
        [DisplayName("Type Vehicle Id")]
		public int VehicleTypeId { get; set; }
        [Required]
        [DisplayName("Fuel for Vehicle")]
		public string Fuel { get; set; }
        [Required]
        [Range(2,4)]
        [DisplayName("Total wheels of vehicle")]
		public int Wheels { get; set; }
        [Required]
        [StringLength(12, ErrorMessage = "{0} length must be between {2} & {0}.", MinimumLength = 2)]
		public string Brand { get; set; }
        [Required]
        [StringLength(6, ErrorMessage ="Length must be 6 chars long", MinimumLength = 6)]
        [DisplayName("Registration number")]
		public string RegistrationNumber { get; set; }
        [Required]
        [StringLength(12, ErrorMessage = "{0} length must be between {2} & {0}.", MinimumLength = 2)]
        [DisplayName("Color")]
		public string Color { get; set; }
        [Required]
        [DisplayName("Vehicle Type")]
		public VehicleType VehicleType { get; set; }

	}
}