using System.ComponentModel;

namespace Garage3._0.Web.Models.ViewModels
{
	public class MembershipsOverviewViewModel
	{
		public int MembershipId { get; set; }
		[DisplayName("Name")]
		public string FullName { get; set; } = string.Empty;
		[DisplayName("Number of vehicles")]
		public int NumRegVehicles { get; set; } = 0;
		[DisplayName("Membership type")]
		public string MembershipType { get; set; } = String.Empty;
	}
}
