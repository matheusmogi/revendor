// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Revendor.Infrastructure.Persistence;

namespace Revendor.Infrastructure.Migrations
{
    [DbContext(typeof(RevendorContext))]
    [Migration("20211006112024_improvingcolumns")]
    partial class improvingcolumns
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Revendor.Domain.Entities.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("Birthday")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Revendor.Domain.Entities.Tenant", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tenant");
                });

            modelBuilder.Entity("Revendor.Domain.Entities.Customer", b =>
                {
                    b.HasOne("Revendor.Domain.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");

                    b.OwnsMany("Revendor.Domain.ValueObjects.EmailAddress", "Emails", b1 =>
                        {
                            b1.Property<string>("CustomerId")
                                .HasColumnType("nvarchar(50)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Email")
                                .HasColumnType("nvarchar(50)")
                                .HasMaxLength(50);

                            b1.Property<string>("Label")
                                .HasColumnType("nvarchar(20)")
                                .HasMaxLength(20);

                            b1.HasKey("CustomerId", "Id");

                            b1.ToTable("EmailAddress");

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
#pragma warning restore 612, 618
        }
    }
}
