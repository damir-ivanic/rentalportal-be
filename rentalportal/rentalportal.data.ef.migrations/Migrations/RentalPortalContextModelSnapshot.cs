﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using rentalportal.data.ef;

namespace rentalportal.data.ef.migrations.Migrations
{
    [DbContext(typeof(RentalPortalContext))]
    partial class RentalPortalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("rentalportal.model.Domain.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("rentalportal.model.Domain.Note", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Message");

                    b.Property<Guid?>("RentalObjectId");

                    b.HasKey("Id");

                    b.HasIndex("RentalObjectId");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("rentalportal.model.Domain.RentalObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("RentalObject");
                });

            modelBuilder.Entity("rentalportal.model.Domain.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CustomerId");

                    b.Property<DateTime>("From");

                    b.Property<Guid>("RentalObjectId");

                    b.Property<DateTime>("To");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RentalObjectId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("rentalportal.model.Domain.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Chains");

                    b.Property<string>("Color");

                    b.Property<DateTime>("Insured");

                    b.Property<string>("Make");

                    b.Property<int>("Mileage");

                    b.Property<string>("Model");

                    b.Property<string>("Plates");

                    b.Property<DateTime>("Registered");

                    b.Property<string>("Sticker");

                    b.Property<string>("Tires");

                    b.Property<DateTime>("Warranty");

                    b.HasKey("Id");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("rentalportal.model.Domain.Note", b =>
                {
                    b.HasOne("rentalportal.model.Domain.RentalObject")
                        .WithMany("Notes")
                        .HasForeignKey("RentalObjectId");
                });

            modelBuilder.Entity("rentalportal.model.Domain.Reservation", b =>
                {
                    b.HasOne("rentalportal.model.Domain.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerId");

                    b.HasOne("rentalportal.model.Domain.RentalObject", "RentalObject")
                        .WithMany("Reservations")
                        .HasForeignKey("RentalObjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
