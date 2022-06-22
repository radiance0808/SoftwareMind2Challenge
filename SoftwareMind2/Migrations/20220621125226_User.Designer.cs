﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoftwareMind.Models;

namespace SoftwareMind2.Migrations
{
    [DbContext(typeof(BookingDbContext))]
    [Migration("20220621125226_User")]
    partial class User
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SoftwareMind.Models.Booking", b =>
                {
                    b.Property<int>("idBooking")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("endDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("idDesk")
                        .HasColumnType("int");

                    b.Property<int>("idUser")
                        .HasColumnType("int");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("datetime2");

                    b.HasKey("idBooking");

                    b.HasIndex("idDesk");

                    b.HasIndex("idUser");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("SoftwareMind.Models.Desk", b =>
                {
                    b.Property<int>("idDesk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("idLocation")
                        .HasColumnType("int");

                    b.HasKey("idDesk");

                    b.HasIndex("idLocation");

                    b.ToTable("Desks");
                });

            modelBuilder.Entity("SoftwareMind.Models.Location", b =>
                {
                    b.Property<int>("idLocation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("city")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("street")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("idLocation");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("SoftwareMind.Models.User", b =>
                {
                    b.Property<int>("idUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("RefreshTokenExpiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("firstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("lastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("login")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("idUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SoftwareMind.Models.Booking", b =>
                {
                    b.HasOne("SoftwareMind.Models.Desk", "desk")
                        .WithMany("Bookings")
                        .HasForeignKey("idDesk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareMind.Models.User", "user")
                        .WithMany("Bookings")
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("desk");

                    b.Navigation("user");
                });

            modelBuilder.Entity("SoftwareMind.Models.Desk", b =>
                {
                    b.HasOne("SoftwareMind.Models.Location", "Location")
                        .WithMany("Desks")
                        .HasForeignKey("idLocation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("SoftwareMind.Models.Desk", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("SoftwareMind.Models.Location", b =>
                {
                    b.Navigation("Desks");
                });

            modelBuilder.Entity("SoftwareMind.Models.User", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
