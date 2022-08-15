using Garage3._0.Core;
using System.ComponentModel;

namespace Garage3._0.Web.Models.ViewModels
{
	public class VehicleViewViewModel
	{
		[DisplayName("Name")]
		public string Name { get; set; }
		[DisplayName("Last name")]
		public string LastName { get; set; }
		[DisplayName("Registration number")]
		public string RegistrationNumber { get; set; }

		[DisplayName("Vehicle type")]
		public VehicleType VehicleType{ get; set; }
		public int MembershipId { get; set; }
		[DisplayName("Arrival time")]
		public DateTime ArrivalTime { get; set; }
	}
}
