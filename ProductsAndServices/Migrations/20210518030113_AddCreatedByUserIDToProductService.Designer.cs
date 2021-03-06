﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductsAndServices.Context;

namespace ProductsAndServices.Migrations
{
    [DbContext(typeof(ProductsAndServicesContext))]
    [Migration("20210518030113_AddCreatedByUserIDToProductService")]
    partial class AddCreatedByUserIDToProductService
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProductsAndServices.Entity.ProductService", b =>
                {
                    b.Property<int>("ProductServiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedByUserID")
                        .HasColumnType("int");

                    b.Property<bool>("Exchangement")
                        .HasColumnType("bit");

                    b.Property<string>("ExchangementCondition")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsPriceChangeable")
                        .HasColumnType("bit");

                    b.Property<bool>("PriceAgreement")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProductServiceID");

                    b.ToTable("ProductServices");
                });

            modelBuilder.Entity("ProductsAndServices.Entity.ProductServicePicture", b =>
                {
                    b.Property<int>("PictureID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("Picture")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("ProductServiceID")
                        .HasColumnType("int");

                    b.HasKey("PictureID");

                    b.HasIndex("ProductServiceID");

                    b.ToTable("ProductServicePictures");
                });

            modelBuilder.Entity("ProductsAndServices.Entity.ProductServicePrice", b =>
                {
                    b.Property<int>("PriceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(9,2)");

                    b.Property<int>("ProductServiceID")
                        .HasColumnType("int");

                    b.HasKey("PriceID");

                    b.HasIndex("ProductServiceID");

                    b.ToTable("ProductServicePrices");
                });

            modelBuilder.Entity("ProductsAndServices.Entity.ProductServicePicture", b =>
                {
                    b.HasOne("ProductsAndServices.Entity.ProductService", "ProductService")
                        .WithMany("Pictures")
                        .HasForeignKey("ProductServiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductService");
                });

            modelBuilder.Entity("ProductsAndServices.Entity.ProductServicePrice", b =>
                {
                    b.HasOne("ProductsAndServices.Entity.ProductService", "ProductService")
                        .WithMany("Prices")
                        .HasForeignKey("ProductServiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductService");
                });

            modelBuilder.Entity("ProductsAndServices.Entity.ProductService", b =>
                {
                    b.Navigation("Pictures");

                    b.Navigation("Prices");
                });
#pragma warning restore 612, 618
        }
    }
}
