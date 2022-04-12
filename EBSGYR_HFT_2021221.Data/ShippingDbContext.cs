using PKMXEN.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace PKMXEN.Data
{
    public class ShippingDbContext : DbContext
    {
        /// <summary>
        /// Creating Database Tables using DbSet
        /// </summary>
        public virtual DbSet<Carrier> Carriers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Parcel> Parcels { get; set; }

        /// <summary>
        /// Checks existance of database tables - if NOT, creates them
        /// </summary>
        public ShippingDbContext()
        {
            Database.EnsureCreated();
        }

        // Configuring database, setting connectionString
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder == null)
            {
                throw new ArgumentException(nameof(optionsBuilder));
            }
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\HFT_Database.mdf;Integrated Security=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Carrier>(entity =>
            //{
            //    entity
            //    .HasMany<Order>(o => o.Orders)
            //    .WithOne(o => o.Carriers)
            //    .OnDelete(DeleteBehavior.Cascade);
            //});

            //modelBuilder.Entity<Order>(entity =>
            //{
            //    entity
            //    .HasOne(o => o.Carriers)
            //    .WithMany(c => c.Orders)
            //    .HasForeignKey(o => o.CarrierID)
            //    .OnDelete(DeleteBehavior.Cascade);

            //});

            //modelBuilder.Entity<Parcel>(entity =>
            //{
            //    entity
            //    .HasOne(p => p.Orders)
            //    .WithMany(o => o.Parcel)
            //    .HasForeignKey(p => p.OrderID)
            //    .OnDelete(DeleteBehavior.Cascade);
            //});

            // Creating Sample Order Data

            Order order1 = new() { OrderID = 1, OrderDescription = "2021 MacBook Pro", OrderValue = 3219.99, OrderDate = new DateTime(2021, 10, 11), CarrierID = 2 };
            Order order2 = new() { OrderID = 2, OrderDescription = "Oneplus 9 Pro 128GB ", OrderValue = 999.99, OrderDate = new DateTime(2021, 09, 10), CarrierID = 3 };
            Order order3 = new() { OrderID = 3, OrderDescription = "Apple Watch Series 6", OrderValue = 450.99, OrderDate = new DateTime(2020, 12, 10), CarrierID = 1 };
            Order order4 = new() { OrderID = 4, OrderDescription = "LG Cinebeam Projector 4K", OrderValue = 2789.99, OrderDate = new DateTime(2019, 07, 24), CarrierID = 2 };
            Order order5 = new() { OrderID = 5, OrderDescription = "10x Lenovo Wireless Mouse for PC", OrderValue = 150.99, OrderDate = new DateTime(2020, 02, 27), CarrierID = 5 };

            // Creating Sample Carrier Data
            Carrier carrier1 = new() { CarrierID = 1, Name = "Peter Adam", Age = 28, Salary = 2400, TotalNumberOfParcels = 77 };
            Carrier carrier2 = new() { CarrierID = 2, Name = "Mark Johnson", Age = 41, Salary = 1780, TotalNumberOfParcels = 50 };
            Carrier carrier3 = new() { CarrierID = 3, Name = "Eva Sandler", Age = 36, Salary = 2120, TotalNumberOfParcels = 82 };
            Carrier carrier4 = new() { CarrierID = 4, Name = "Mack Brandy", Age = 20, Salary = 1090, TotalNumberOfParcels = 48 };
            Carrier carrier5 = new() { CarrierID = 5, Name = "Steve Harlem", Age = 55, Salary = 2510, TotalNumberOfParcels = 101 };

            // Creating Sample Parcel Data

            Parcel parcel1 = new() { TrackingID = 1, OrderID = 3, Weight = 2.7, COD = false, ShippingDate = new DateTime(2021, 12, 12), CustomerName = "Jeana M.Avery", Country = "USA", Address = "1941 Frank Avenue, Springfield, MA 01103" };
            Parcel parcel2 = new() { TrackingID = 2, OrderID = 2, Weight = 1.87, COD = true, ShippingDate = new DateTime(2021, 10, 01), CustomerName = "Joseph G. Cacho", Country = "USA", Address = "4874 Penn Street, Oran, MO 63771" };
            Parcel parcel3 = new() { TrackingID = 3, OrderID = 4, Weight = 12.65, COD = false, ShippingDate = new DateTime(2019, 08, 11), CustomerName = "Paul C. Light", Country = "UK", Address = "70 Nith Street, GLENBRECK, ML12 5XN" };
            Parcel parcel4 = new() { TrackingID = 4, OrderID = 1, Weight = 4.2, COD = true, ShippingDate = new DateTime(2021, 10, 13), CustomerName = "Marie T. Schulte", Country = "FR", Address = "36 Boulevard de Normandie, Forbach, 57600" };
            Parcel parcel5 = new() { TrackingID = 5, OrderID = 5, Weight = 3.0, COD = false, ShippingDate = new DateTime(2020, 03, 02), CustomerName = "Danko Mozes", Country = "HU", Address = "2 Belgrad rkp., Kismakfa, 9800" };

            modelBuilder.Entity<Order>().HasData(order1, order2, order3, order4, order5);
            modelBuilder.Entity<Carrier>().HasData(carrier1, carrier2, carrier3, carrier4, carrier5);
            modelBuilder.Entity<Parcel>().HasData(parcel1, parcel2, parcel3, parcel4, parcel5);
        }
    }
}
