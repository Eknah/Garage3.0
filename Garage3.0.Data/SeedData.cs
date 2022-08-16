using Bogus;
using Garage3._0.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage3._0.Data
{
	public class SeedData
	{
		private static Faker faker = null!;

		public static async Task InitAsync(GarageContext db)
		{
			if (await db.Membership.AnyAsync())
				return;

			faker = new Faker("sv");

			var memberships = GenerateMemberships(5);
			await db.AddRangeAsync(memberships);

			var vehicleTypes = GenerateVehicleTypes();
			await db.AddRangeAsync(vehicleTypes);

			await db.SaveChangesAsync();

			var vehicles = GenerateVehicles(5, memberships, vehicleTypes);
			await db.AddRangeAsync(vehicles);

			await db.SaveChangesAsync();

			var parkings = GenerateParkings(5, vehicles);
			await db.AddRangeAsync(parkings);

			await db.SaveChangesAsync();
		}

		private static IEnumerable<Parking> GenerateParkings(int numberOfParkings, IEnumerable<Vehicle> vehicles)
		{
			var parkings = new List<Parking>();

			for (var i = 0; i < numberOfParkings; i++)
			{
				var randomVehicle = vehicles.ElementAt(new Random().Next(vehicles.Count()));
				var vehicleId = randomVehicle.Id;

				var arrivalTime = faker.Date.Recent();

				parkings.Add(new Parking()
				{
					VehicleId = vehicleId,
					ArrivalTime = arrivalTime
				});
			}

			return parkings;
		}

		private static IEnumerable<Vehicle> GenerateVehicles(int numberOfVehicles, IEnumerable<Membership> memberships, IEnumerable<VehicleType> vehicleTypes)
		{
			var vehicles = new List<Vehicle>();

			for (var i = 0; i < numberOfVehicles; i++)
			{
				var randomMember = memberships.ElementAt(new Random().Next(memberships.Count()));
				var membershipId = randomMember.Id;

				var randomVehicleType = vehicleTypes.ElementAt(new Random().Next(vehicleTypes.Count()));
				var vehicleTypeId = randomVehicleType.Id;

				string[] brands =
				{
					"Volvo",
					"Volkswagen",
					"Toyota",
					"Honda",
					"Peugeot"
				};

				string[] colors =
				{
					"Red",
					"Green",
					"Blue",
					"Yellow",
					"Black"
				};

				var regNumber = new string(faker.Random.Chars('A', 'Z', 3)) + new string(faker.Random.Chars('0', '9', 3));

				vehicles.Add(new Vehicle()
				{
					MembershipId = membershipId,
					VehicleTypeId = vehicleTypeId,
					Fuel = new Random().Next(2) % 2 == 0 ? "Diesel" : "Gasoline",
					Wheels = new Random().Next(3) + 2,
					Brand = brands[new Random().Next(brands.Length)],
					RegistrationNumber = regNumber,
					Color = colors[new Random().Next(colors.Length)]
				});
			}

			return vehicles;
		}

		private static IEnumerable<VehicleType> GenerateVehicleTypes()
		{
			var vehicleTypes = new List<VehicleType>();

			vehicleTypes.Add(new() { Name = "Car", Size = 1 });
			vehicleTypes.Add(new() { Name = "Bicycle", Size = 1 });
			vehicleTypes.Add(new() { Name = "Motorcycle", Size = 1 });
			vehicleTypes.Add(new() { Name = "Truck", Size = 1 });

			return vehicleTypes;
		}

		private static IEnumerable<Membership> GenerateMemberships(int numberOfMemberships)
		{
			var memberships = new List<Membership>();

			for (int i = 0; i < numberOfMemberships; i++)
			{
				var p = new Person();

				var firstName = p.FirstName;
				var lastName = p.LastName;

				var dateOfBirth = p.DateOfBirth;

				var years = dateOfBirth.Year.ToString();
				var month = dateOfBirth.Month.ToString("00");
				var day = dateOfBirth.Day.ToString("00");
				var last4ArrDigits = p.Random.Digits(4, 0, 9);

				string last4 = String.Empty;

				foreach (var digit in last4ArrDigits!)
					last4 += digit;

				var personNumber = $"{years}{month}{day}{last4}";

				var email = p.Email;

				var registrationDate = faker.Date.Recent();

				var pro = faker.Random.Bool();

				memberships.Add(new Membership()
				{
					PersonNumber = personNumber,
					Name = firstName,
					LastName = lastName,
					Email = email,
					RegistrationDate = registrationDate,
					Pro = pro
				});
			}

			return memberships;
		}
	}
}
