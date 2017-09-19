using ShipBob.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBob.DAL
{
    public class ShipBobContext : DbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ShipBobContext() : base("ShipBobConext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
