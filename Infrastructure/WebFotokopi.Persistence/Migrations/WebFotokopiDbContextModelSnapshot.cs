﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebFotokopi.Persistence.Contexts;

#nullable disable

namespace WebFotokopi.Persistence.Migrations
{
    [DbContext(typeof(WebFotokopiDbContext))]
    partial class WebFotokopiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebFotokopi.Domain.Entities.City", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.CustomerAddress", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DistrictID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("DistrictID");

                    b.ToTable("CustomerAddresses");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.District", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("CityID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("CityID");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.File", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AppCustomerID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AppSellerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfPage")
                        .HasColumnType("int");

                    b.Property<bool>("SellerOwner")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("AppCustomerID");

                    b.HasIndex("AppSellerID");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.Identity.AppCustomer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CustomerAddressID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefleshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefleshTokenEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerAddressID")
                        .IsUnique();

                    b.ToTable("AppCustomers");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.Identity.AppSeller", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logo2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefleshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefleshTokenEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SellerAddressID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("View")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("SellerAddressID")
                        .IsUnique();

                    b.ToTable("AppSellers");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("SellerID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("SellerID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.Package", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ColorMode")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DuplexMode")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PaperSizeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PaperTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("SellerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("SheetsPerPageID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("View")
                        .HasColumnType("bit");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("PaperSizeID");

                    b.HasIndex("PaperTypeID");

                    b.HasIndex("SellerID");

                    b.HasIndex("SheetsPerPageID");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.PaperSize", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SizeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("PaperSizes");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.PaperType", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaperTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("PaperTypes");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerNote")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FileID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PackageID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("FileID");

                    b.HasIndex("OrderID");

                    b.HasIndex("PackageID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.SellerAddress", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DistrictID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("DistrictID");

                    b.ToTable("SellerAddresses");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.SheetsPerPage", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SheetsPerPageNumber")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("SheetsPerPages");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.CustomerAddress", b =>
                {
                    b.HasOne("WebFotokopi.Domain.Entities.District", "District")
                        .WithMany("CustomerAddresses")
                        .HasForeignKey("DistrictID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.District", b =>
                {
                    b.HasOne("WebFotokopi.Domain.Entities.City", "City")
                        .WithMany("Districts")
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.File", b =>
                {
                    b.HasOne("WebFotokopi.Domain.Entities.Identity.AppCustomer", "AppCustomer")
                        .WithMany()
                        .HasForeignKey("AppCustomerID");

                    b.HasOne("WebFotokopi.Domain.Entities.Identity.AppSeller", "AppSeller")
                        .WithMany("FileContents")
                        .HasForeignKey("AppSellerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppCustomer");

                    b.Navigation("AppSeller");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.Identity.AppCustomer", b =>
                {
                    b.HasOne("WebFotokopi.Domain.Entities.CustomerAddress", "CustomerAddress")
                        .WithOne("AppCustomer")
                        .HasForeignKey("WebFotokopi.Domain.Entities.Identity.AppCustomer", "CustomerAddressID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CustomerAddress");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.Identity.AppSeller", b =>
                {
                    b.HasOne("WebFotokopi.Domain.Entities.SellerAddress", "SellerAddress")
                        .WithOne("AppSeller")
                        .HasForeignKey("WebFotokopi.Domain.Entities.Identity.AppSeller", "SellerAddressID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SellerAddress");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.Order", b =>
                {
                    b.HasOne("WebFotokopi.Domain.Entities.Identity.AppCustomer", "AppCustomer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebFotokopi.Domain.Entities.Identity.AppSeller", "AppSeller")
                        .WithMany("Orders")
                        .HasForeignKey("SellerID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("AppCustomer");

                    b.Navigation("AppSeller");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.Package", b =>
                {
                    b.HasOne("WebFotokopi.Domain.Entities.PaperSize", "PaperSize")
                        .WithMany("ProductFeatures")
                        .HasForeignKey("PaperSizeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebFotokopi.Domain.Entities.PaperType", "PaperType")
                        .WithMany("ProductFeatures")
                        .HasForeignKey("PaperTypeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebFotokopi.Domain.Entities.Identity.AppSeller", "AppSeller")
                        .WithMany("Packages")
                        .HasForeignKey("SellerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebFotokopi.Domain.Entities.SheetsPerPage", "SheetsPerPage")
                        .WithMany("ProductFeatures")
                        .HasForeignKey("SheetsPerPageID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AppSeller");

                    b.Navigation("PaperSize");

                    b.Navigation("PaperType");

                    b.Navigation("SheetsPerPage");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.Product", b =>
                {
                    b.HasOne("WebFotokopi.Domain.Entities.File", "File")
                        .WithMany("Products")
                        .HasForeignKey("FileID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebFotokopi.Domain.Entities.Order", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebFotokopi.Domain.Entities.Package", "Package")
                        .WithMany("Products")
                        .HasForeignKey("PackageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");

                    b.Navigation("Order");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.SellerAddress", b =>
                {
                    b.HasOne("WebFotokopi.Domain.Entities.District", "District")
                        .WithMany("SellerAddress")
                        .HasForeignKey("DistrictID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.City", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.CustomerAddress", b =>
                {
                    b.Navigation("AppCustomer")
                        .IsRequired();
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.District", b =>
                {
                    b.Navigation("CustomerAddresses");

                    b.Navigation("SellerAddress");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.File", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.Identity.AppCustomer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.Identity.AppSeller", b =>
                {
                    b.Navigation("FileContents");

                    b.Navigation("Orders");

                    b.Navigation("Packages");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.Package", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.PaperSize", b =>
                {
                    b.Navigation("ProductFeatures");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.PaperType", b =>
                {
                    b.Navigation("ProductFeatures");
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.SellerAddress", b =>
                {
                    b.Navigation("AppSeller")
                        .IsRequired();
                });

            modelBuilder.Entity("WebFotokopi.Domain.Entities.SheetsPerPage", b =>
                {
                    b.Navigation("ProductFeatures");
                });
#pragma warning restore 612, 618
        }
    }
}
