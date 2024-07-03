﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShortSharing.DAL.Context;

#nullable disable

namespace ShortSharing.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240703141306_InitCreate")]
    partial class InitCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ShortSharing.DAL.Entities.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ShortSharing.DAL.Entities.RentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("EndRentDate")
                        .HasColumnType("date");

                    b.Property<Guid>("RenterId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("StartRentDate")
                        .HasColumnType("date");

                    b.Property<Guid>("ThingId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RenterId");

                    b.HasIndex("ThingId");

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("ShortSharing.DAL.Entities.ThingEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TypeId");

                    b.ToTable("Things");
                });

            modelBuilder.Entity("ShortSharing.DAL.Entities.TypeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("ShortSharing.DAL.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShortSharing.DAL.Entities.RentEntity", b =>
                {
                    b.HasOne("ShortSharing.DAL.Entities.UserEntity", "Renter")
                        .WithMany("Rents")
                        .HasForeignKey("RenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShortSharing.DAL.Entities.ThingEntity", "Thing")
                        .WithMany()
                        .HasForeignKey("ThingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Renter");

                    b.Navigation("Thing");
                });

            modelBuilder.Entity("ShortSharing.DAL.Entities.ThingEntity", b =>
                {
                    b.HasOne("ShortSharing.DAL.Entities.CategoryEntity", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShortSharing.DAL.Entities.UserEntity", "Owner")
                        .WithMany("Things")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShortSharing.DAL.Entities.TypeEntity", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Owner");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("ShortSharing.DAL.Entities.TypeEntity", b =>
                {
                    b.HasOne("ShortSharing.DAL.Entities.CategoryEntity", "Category")
                        .WithMany("Types")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ShortSharing.DAL.Entities.CategoryEntity", b =>
                {
                    b.Navigation("Types");
                });

            modelBuilder.Entity("ShortSharing.DAL.Entities.UserEntity", b =>
                {
                    b.Navigation("Rents");

                    b.Navigation("Things");
                });
#pragma warning restore 612, 618
        }
    }
}
