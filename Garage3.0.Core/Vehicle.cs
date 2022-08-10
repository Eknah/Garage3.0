namespace Garage3._0.Core
{
#nullable disable
	public class Vehicle
	{
		public int Id { get; set; }
		public int MembershipId { get; set; }
		public int VehicleTypeId { get; set; }
		public string Fuel { get; set; }
		public int Wheels { get; set; }
		public string Brand { get; set; }
		public string RegistrationNumber { get; set; }
		public string Colour { get; set; }

		public VehicleType VehicleType { get; set; }

	}
}