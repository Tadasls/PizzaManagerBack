﻿// <auto-generated />
using System;
using CompetitionEventsManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace PizzaManager.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20230505085350_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("CompetitionEventsManager.Models.LocalUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LocalUsers");
                });

            modelBuilder.Entity("CompetitionEventsManager.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("TotalAmount")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasMaxLength(20)
                        .HasColumnType("INTEGER");

                    b.HasKey("OrderID");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CompetitionEventsManager.Models.Pizza", b =>
                {
                    b.Property<int>("PizzaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OrderID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PizzaName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Price")
                        .HasColumnType("TEXT");

                    b.Property<string>("Size")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Toppings")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PizzaID");

                    b.HasIndex("OrderID");

                    b.HasIndex("UserId");

                    b.ToTable("Pizzas");

                    b.HasData(
                        new
                        {
                            PizzaID = 1,
                            PizzaName = "A Pizza",
                            Size = "small",
                            Toppings = 2
                        },
                        new
                        {
                            PizzaID = 2,
                            PizzaName = "B Pizza",
                            Size = "medium",
                            Toppings = 3
                        },
                        new
                        {
                            PizzaID = 3,
                            PizzaName = "C Pizza",
                            Size = "large",
                            Toppings = 4
                        });
                });

            modelBuilder.Entity("CompetitionEventsManager.Models.Order", b =>
                {
                    b.HasOne("CompetitionEventsManager.Models.LocalUser", "LocalUser")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("LocalUser");
                });

            modelBuilder.Entity("CompetitionEventsManager.Models.Pizza", b =>
                {
                    b.HasOne("CompetitionEventsManager.Models.Order", "Order")
                        .WithMany("Orders")
                        .HasForeignKey("OrderID");

                    b.HasOne("CompetitionEventsManager.Models.LocalUser", "LocalUser")
                        .WithMany("Pizzas")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("LocalUser");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("CompetitionEventsManager.Models.LocalUser", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Pizzas");
                });

            modelBuilder.Entity("CompetitionEventsManager.Models.Order", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}