﻿// <auto-generated />
using System;
using CarAuction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarAuction.Migrations
{
    [DbContext(typeof(AuctionDbContext))]
    partial class AuctionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CarAuction.Models.CurrentBind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VehicleId");

                    b.ToTable("CurrentBinds");
                });

            modelBuilder.Entity("CarAuction.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("AllDay")
                        .HasColumnType("bit");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("End")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Start")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("CarAuction.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("CarAuction.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CarAuction.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CarAuction.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("BidStatus")
                        .HasColumnType("bit");

                    b.Property<string>("BodyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreateById")
                        .HasColumnType("int");

                    b.Property<int>("CurrentBid")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Drive")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EngineCapacity")
                        .HasColumnType("int");

                    b.Property<int>("EngineOutput")
                        .HasColumnType("int");

                    b.Property<string>("Fuel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Highlights")
                        .HasColumnType("int");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<long>("MeterReadout")
                        .HasColumnType("bigint");

                    b.Property<string>("ModelGeneration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModelSpecifer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberKeys")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryDamage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Producer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegistrationYear")
                        .HasColumnType("int");

                    b.Property<bool>("SaleStatus")
                        .HasColumnType("bit");

                    b.Property<int>("SaleTerm")
                        .HasColumnType("int");

                    b.Property<bool>("SecondTireSet")
                        .HasColumnType("bit");

                    b.Property<string>("SecondaryDamage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ServiceManual")
                        .HasColumnType("bit");

                    b.Property<string>("Transmission")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Watch")
                        .HasColumnType("bit");

                    b.Property<int>("WinnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("CarAuction.Models.Watch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Watches");
                });

            modelBuilder.Entity("CarAuction.Models.CurrentBind", b =>
                {
                    b.HasOne("CarAuction.Models.User", "UserMany")
                        .WithMany("CurrentBinds")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarAuction.Models.Vehicle", "VehicleMany")
                        .WithMany("CurrentBinds")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserMany");

                    b.Navigation("VehicleMany");
                });

            modelBuilder.Entity("CarAuction.Models.User", b =>
                {
                    b.HasOne("CarAuction.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CarAuction.Models.Vehicle", b =>
                {
                    b.HasOne("CarAuction.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("CarAuction.Models.Watch", b =>
                {
                    b.HasOne("CarAuction.Models.User", "UserMany")
                        .WithMany("Observed")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarAuction.Models.Vehicle", "VehicleMany")
                        .WithMany("Bidders")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserMany");

                    b.Navigation("VehicleMany");
                });

            modelBuilder.Entity("CarAuction.Models.User", b =>
                {
                    b.Navigation("CurrentBinds");

                    b.Navigation("Observed");
                });

            modelBuilder.Entity("CarAuction.Models.Vehicle", b =>
                {
                    b.Navigation("Bidders");

                    b.Navigation("CurrentBinds");
                });
#pragma warning restore 612, 618
        }
    }
}
