﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecycLab.Models;

#nullable disable

namespace RecycLab.Migrations
{
    [DbContext(typeof(RecycLabContext))]
    [Migration("20240320154406_Migration4")]
    partial class Migration4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Person", b =>
                {
                    b.Property<int>("IdPerson")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.HasKey("IdPerson");

                    b.ToTable("People");

                    b.UseTptMappingStrategy();

                    b.HasData(
                        new
                        {
                            IdPerson = 1,
                            Address = "123 Main St",
                            BirthDate = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "john@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            PhoneNumber = "123-456-7890"
                        },
                        new
                        {
                            IdPerson = 2,
                            Address = "456 Elm St",
                            BirthDate = new DateTime(1995, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jane@example.com",
                            FirstName = "Jane",
                            LastName = "Smith",
                            PhoneNumber = "987-654-3210"
                        },
                        new
                        {
                            IdPerson = 3,
                            Address = "789 Oak St",
                            BirthDate = new DateTime(1985, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "alice@example.com",
                            FirstName = "Alice",
                            LastName = "Johnson",
                            PhoneNumber = "555-555-5555"
                        },
                        new
                        {
                            IdPerson = 4,
                            Address = "101 Pine St",
                            BirthDate = new DateTime(1975, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "bob@example.com",
                            FirstName = "Bob",
                            LastName = "Williams",
                            PhoneNumber = "444-444-4444"
                        });
                });

            modelBuilder.Entity("RecycLab.Models.Confirmation", b =>
                {
                    b.Property<int>("IdConfirmation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("ConfirmationDate")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdCollector")
                        .HasColumnType("int");

                    b.Property<int>("IdTransaction")
                        .HasColumnType("int");

                    b.HasKey("IdConfirmation");

                    b.HasIndex("IdCollector");

                    b.HasIndex("IdTransaction")
                        .IsUnique();

                    b.ToTable("Confirmations");
                });

            modelBuilder.Entity("RecycLab.Models.Dechet", b =>
                {
                    b.Property<int>("IdDechet")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("PrixUnitaire")
                        .HasColumnType("float");

                    b.Property<string>("TypeDechet")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IdDechet");

                    b.ToTable("Dechets");
                });

            modelBuilder.Entity("RecycLab.Models.Transaction", b =>
                {
                    b.Property<int>("IdTransaction")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Etat")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("IdDechet")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<float>("Quantity")
                        .HasColumnType("float");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<DateTime?>("TransactionDate")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("isConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("IdTransaction");

                    b.HasIndex("IdDechet");

                    b.HasIndex("IdUser");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("RecycLab.Models.Collector", b =>
                {
                    b.HasBaseType("Person");

                    b.Property<string>("Auto")
                        .HasColumnType("longtext");

                    b.Property<int>("PersonTempId1")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("TypeAuto")
                        .HasColumnType("longtext");

                    b.Property<bool>("isApproved")
                        .HasColumnType("tinyint(1)");

                    b.ToTable("Collectors", (string)null);
                });

            modelBuilder.Entity("RecycLab.Models.User", b =>
                {
                    b.HasBaseType("Person");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("RecycLab.Models.Confirmation", b =>
                {
                    b.HasOne("RecycLab.Models.Collector", "Collector")
                        .WithMany("Confirmations")
                        .HasForeignKey("IdCollector")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecycLab.Models.Transaction", "Transaction")
                        .WithOne("Confirmation")
                        .HasForeignKey("RecycLab.Models.Confirmation", "IdTransaction")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collector");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("RecycLab.Models.Transaction", b =>
                {
                    b.HasOne("RecycLab.Models.Dechet", "Dechet")
                        .WithMany("Transactions")
                        .HasForeignKey("IdDechet")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecycLab.Models.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dechet");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecycLab.Models.Collector", b =>
                {
                    b.HasOne("Person", "Person")
                        .WithMany("Collectors")
                        .HasForeignKey("IdPerson")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("RecycLab.Models.User", b =>
                {
                    b.HasOne("Person", "Person")
                        .WithMany("Users")
                        .HasForeignKey("IdPerson")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Person", b =>
                {
                    b.Navigation("Collectors");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("RecycLab.Models.Dechet", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("RecycLab.Models.Transaction", b =>
                {
                    b.Navigation("Confirmation")
                        .IsRequired();
                });

            modelBuilder.Entity("RecycLab.Models.Collector", b =>
                {
                    b.Navigation("Confirmations");
                });

            modelBuilder.Entity("RecycLab.Models.User", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
