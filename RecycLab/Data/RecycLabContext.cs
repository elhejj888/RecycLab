using System;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace RecycLab.Models
{
	public class RecycLabContext : DbContext
    {
		public RecycLabContext(DbContextOptions options) : base(options) { }

		public DbSet<Collector> Collectors { get; set; }

        public DbSet<Dechet> Dechets { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Confirmation> Confirmations { get; set; }


        public string ConnectionString { get; set; }

		private MySqlConnection GetConnection()
		{
			return new MySqlConnection(ConnectionString);
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Collector>().ToTable("Collectors");

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.IdUser);

            modelBuilder.Entity<Transaction>()
                .HasOne(d => d.Dechet)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.IdDechet);

            
            modelBuilder.Entity<Confirmation>()
                .HasOne(c => c.Collector)
                .WithMany(col => col.Confirmations)
                .HasForeignKey(c => c.IdCollector);

            modelBuilder.Entity<Confirmation>()
                .HasOne(c => c.Transaction)
                .WithOne(d => d.Confirmation)
                .HasForeignKey<Confirmation>(k => k.IdTransaction);

            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    IdPerson = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Address = "123 Main St",
                    PhoneNumber = "123-456-7890",
                    Email = "john@example.com",
                    BirthDate = new DateTime(1990, 1, 1)
                },
                new Person
                {
                    IdPerson = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Address = "456 Elm St",
                    PhoneNumber = "987-654-3210",
                    Email = "jane@example.com",
                    BirthDate = new DateTime(1995, 5, 15)
                },
                // Add more seed data for additional Person entities
                new Person
                {
                    IdPerson = 3,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Address = "789 Oak St",
                    PhoneNumber = "555-555-5555",
                    Email = "alice@example.com",
                    BirthDate = new DateTime(1985, 8, 20)
                },
                new Person
                {
                    IdPerson = 4,
                    FirstName = "Bob",
                    LastName = "Williams",
                    Address = "101 Pine St",
                    PhoneNumber = "444-444-4444",
                    Email = "bob@example.com",
                    BirthDate = new DateTime(1975, 3, 10)
                }
            );

            base.OnModelCreating(modelBuilder);
        }

        
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                // Enable sensitive data logging
                optionsBuilder.EnableSensitiveDataLogging();

                // Configure the database provider and connection string
                optionsBuilder.UseMySql("server=localhost;port=3306;database=RecycLab;user=root;password=", ServerVersion.AutoDetect("server=localhost;port=3306;database=RecycLab;user=root;password="));
            }
        

    }
}

