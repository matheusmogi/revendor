﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Revendor.Infrastructure.Persistence;

namespace Revendor.Infrastructure.Migrations
{
    [DbContext(typeof(RevendorContext))]
    partial class RevendorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Revendor.Domain.Entities.Brand", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ClusterId")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("TenantId");

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("Revendor.Domain.Entities.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2")
                        .HasMaxLength(10);

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Nickname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ClusterId")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("TenantId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Revendor.Domain.Entities.InventoryMovement", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ClusterId")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("ProductId");

                    b.HasIndex("TenantId");

                    b.ToTable("InventoryMovement");
                });

            modelBuilder.Entity("Revendor.Domain.Entities.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("BrandId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<decimal>("CostPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CurrentInventory")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("EAN")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<decimal>("ProfitMargin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("ShowInStore")
                        .HasColumnType("bit");

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("ValidUntil")
                        .HasColumnType("datetime2");

                    b.HasKey("Id")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("BrandId");

                    b.HasIndex("ClusterId")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("TenantId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Revendor.Domain.Entities.Tenant", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ClusterId")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("Tenant");

                    b.HasData(
                        new
                        {
                            Id = "d93c7c90-e5e5-42b6-b29d-b25c9e55ec8d",
                            ClusterId = 0,
                            Name = "Test Tenant"
                        });
                });

            modelBuilder.Entity("Revendor.Domain.Entities.TenantBrands", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BrandId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("BrandId");

                    b.HasIndex("TenantId");

                    b.ToTable("TenantBrands");
                });

            modelBuilder.Entity("Revendor.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("ClusterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ClusterId")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("TenantId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = "655ecf50-191a-4322-8a24-decec9f92116",
                            Active = true,
                            ClusterId = 0,
                            Password = "$2a$11$ckpeXAuckb7iNaMImlT.B.LMFiMJMIeoPh43rAnugHPObXV9g5b0O",
                            Role = 1,
                            Username = "revendor.admin"
                        },
                        new
                        {
                            Id = "4ff9fe1a-4d14-41c4-a03b-cf351ed9f512",
                            Active = true,
                            ClusterId = 0,
                            Password = "$2a$11$ckpeXAuckb7iNaMImlT.B.LMFiMJMIeoPh43rAnugHPObXV9g5b0O",
                            Role = 2,
                            TenantId = "d93c7c90-e5e5-42b6-b29d-b25c9e55ec8d",
                            Username = "user"
                        });
                });

            modelBuilder.Entity("Revendor.Domain.Entities.Brand", b =>
                {
                    b.HasOne("Revendor.Domain.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("Revendor.Domain.Entities.Customer", b =>
                {
                    b.HasOne("Revendor.Domain.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");

                    b.OwnsOne("Revendor.Domain.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<string>("CustomerId")
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("AddressLine")
                                .HasColumnName("AddressLine")
                                .HasColumnType("nvarchar(100)")
                                .HasMaxLength(100);

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Complement")
                                .HasColumnName("Complement")
                                .HasColumnType("nvarchar(100)")
                                .HasMaxLength(100);

                            b1.Property<string>("Neighbourhood")
                                .HasColumnName("Neighbourhood")
                                .HasColumnType("nvarchar(100)")
                                .HasMaxLength(100);

                            b1.Property<string>("State")
                                .HasColumnName("State")
                                .HasColumnType("nvarchar(2)")
                                .HasMaxLength(2);

                            b1.Property<string>("StreetNumber")
                                .HasColumnName("StreetNumber")
                                .HasColumnType("nvarchar(10)")
                                .HasMaxLength(10);

                            b1.Property<string>("ZipCode")
                                .HasColumnName("ZipCode")
                                .HasColumnType("nvarchar(10)")
                                .HasMaxLength(10);

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.OwnsOne("Revendor.Domain.ValueObjects.Gender", "Gender", b1 =>
                        {
                            b1.Property<string>("CustomerId")
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("Name")
                                .HasColumnName("Gender")
                                .HasColumnType("nvarchar(20)")
                                .HasMaxLength(20);

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.OwnsMany("Revendor.Domain.ValueObjects.Phone", "PhoneNumbers", b1 =>
                        {
                            b1.Property<string>("CustomerId")
                                .HasColumnType("nvarchar(50)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Label")
                                .HasColumnType("nvarchar(20)")
                                .HasMaxLength(20);

                            b1.Property<string>("PhoneNumber")
                                .HasColumnType("nvarchar(15)")
                                .HasMaxLength(15);

                            b1.HasKey("CustomerId", "Id");

                            b1.ToTable("Phone");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });
                });

            modelBuilder.Entity("Revendor.Domain.Entities.InventoryMovement", b =>
                {
                    b.HasOne("Revendor.Domain.Entities.Product", "Product")
                        .WithMany("Movements")
                        .HasForeignKey("ProductId");

                    b.HasOne("Revendor.Domain.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("Revendor.Domain.Entities.Product", b =>
                {
                    b.HasOne("Revendor.Domain.Entities.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Revendor.Domain.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");

                    b.OwnsMany("Revendor.Domain.ValueObjects.Image", "Images", b1 =>
                        {
                            b1.Property<string>("ProductId")
                                .HasColumnType("nvarchar(50)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(100)")
                                .HasMaxLength(100);

                            b1.Property<string>("Path")
                                .HasColumnType("nvarchar(500)")
                                .HasMaxLength(500);

                            b1.HasKey("ProductId", "Id");

                            b1.ToTable("Image");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });
                });

            modelBuilder.Entity("Revendor.Domain.Entities.TenantBrands", b =>
                {
                    b.HasOne("Revendor.Domain.Entities.Brand", "Brand")
                        .WithMany("Tenants")
                        .HasForeignKey("BrandId");

                    b.HasOne("Revendor.Domain.Entities.Tenant", "Tenant")
                        .WithMany("Brands")
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("Revendor.Domain.Entities.User", b =>
                {
                    b.HasOne("Revendor.Domain.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });
#pragma warning restore 612, 618
        }
    }
}
