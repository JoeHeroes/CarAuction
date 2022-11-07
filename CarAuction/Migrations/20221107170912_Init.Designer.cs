﻿// <auto-generated />
using System;
using CarAuction.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarAuction.Migrations
{
    [DbContext(typeof(AuctionDbContext))]
    [Migration("20221107170912_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CarAuction.Authorization.Role", b =>
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

            modelBuilder.Entity("CarAuction.Authorization.User", b =>
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

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CarAuction.Entites.Bid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("bidStatus")
                        .HasColumnType("bit");

                    b.Property<int>("currentBid")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("idVehicle")
                        .HasColumnType("int");

                    b.Property<int>("location")
                        .HasColumnType("int");

                    b.Property<bool>("saleStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("timeLeft")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("CarAuction.Entites.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CreateById")
                        .HasColumnType("int");

                    b.Property<int>("bodyType")
                        .HasColumnType("int");

                    b.Property<string>("color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("drive")
                        .HasColumnType("int");

                    b.Property<int>("engineCapacity")
                        .HasColumnType("int");

                    b.Property<int>("engineOutput")
                        .HasColumnType("int");

                    b.Property<int>("fuel")
                        .HasColumnType("int");

                    b.Property<int>("idVehicle")
                        .HasColumnType("int");

                    b.Property<long>("meterReadout")
                        .HasColumnType("bigint");

                    b.Property<string>("modelGeneration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("modelSpecifer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("numberKeys")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("registrationYear")
                        .HasColumnType("int");

                    b.Property<bool>("secondTireSet")
                        .HasColumnType("bit");

                    b.Property<bool>("serviceManual")
                        .HasColumnType("bit");

                    b.Property<int>("transmission")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vehicle", (string)null);
                });

            modelBuilder.Entity("CarAuction.Models.InfoSell", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("VIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("highLights")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("idVehicle")
                        .HasColumnType("int");

                    b.Property<string>("modelGeneration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("modelSpecifer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("primaryDamage")
                        .HasColumnType("int");

                    b.Property<int>("registrationYear")
                        .HasColumnType("int");

                    b.Property<int>("saleTerm")
                        .HasColumnType("int");

                    b.Property<int>("secondaryDamage")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("InfoSells");
                });

            modelBuilder.Entity("CarAuction.Authorization.User", b =>
                {
                    b.HasOne("CarAuction.Authorization.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
