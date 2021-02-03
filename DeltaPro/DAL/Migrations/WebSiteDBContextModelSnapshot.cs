﻿// <auto-generated />
using System;
using DAL.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(WebSiteDBContext))]
    partial class WebSiteDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.Entity("Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("LongDiscription")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(4000);

                    b.Property<int?>("OwnerId")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Picture1")
                        .HasColumnType("image");

                    b.Property<byte[]>("Picture2")
                        .HasColumnType("image");

                    b.Property<byte[]>("Picture3")
                        .HasColumnType("image");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,0)");

                    b.Property<string>("ShortDiscription")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(500);

                    b.Property<int>("State")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("UserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Models.Product", b =>
                {
                    b.HasOne("Models.User", "Owner")
                        .WithMany("ProductsToSell")
                        .HasForeignKey("OwnerId");

                    b.HasOne("Models.User", "Buyer")
                        .WithMany("ProductsToBuy")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}