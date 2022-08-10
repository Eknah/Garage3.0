using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage3._0.Core;

namespace Garage3._0.Data
{
    public class GarageContext : DbContext
    {
        public GarageContext (DbContextOptions<GarageContext> options)
            : base(options)
        {
        }

        public DbSet<Garage3._0.Core.Membership> Membership { get; set; } = default!;

        public DbSet<Garage3._0.Core.Vehicle>? Vehicle { get; set; }

        public DbSet<Garage3._0.Core.VehicleType>? VehicleType { get; set; }

        public DbSet<Parking>? Parkings { get; set; }
    }
}
