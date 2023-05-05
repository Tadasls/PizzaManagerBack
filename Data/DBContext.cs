using CompetitionEventsManager.Data.InitialData;
using CompetitionEventsManager.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;


namespace CompetitionEventsManager.Data
{
    /// <summary>
    /// main duombaze
    /// </summary>
    public class DBContext : DbContext
    {
        public DBContext() { }
        /// <summary>
        /// main data dase
        /// </summary>
        /// <param name="options"></param>
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        /// <summary>
        /// basic of data conections comes to this
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }



       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LocalUser>().HasKey(d => d.Id);
            modelBuilder.Entity<LocalUser>().HasMany(ab => ab.Orders)
                .WithOne(ab => ab.LocalUser)
                .HasForeignKey(ab => ab.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>().HasKey(d => d.OrderID);
            //modelBuilder.Entity<Order>().HasData(PizzaInitialData.OrderDataSeed);

                   
         modelBuilder.Entity<LocalUser>().HasMany(ab => ab.Pizzas)
                .WithOne(ab => ab.LocalUser)
                .HasForeignKey(ab => ab.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Pizza>().HasKey(d => d.PizzaID);
            modelBuilder.Entity<Pizza>().HasData(PizzaInitialData.PizzaDataSeed);


            modelBuilder.Entity<Order>()
              .HasOne(ab => ab.LocalUser)
             .WithMany(ab => ab.Orders);



        }

    }
}




