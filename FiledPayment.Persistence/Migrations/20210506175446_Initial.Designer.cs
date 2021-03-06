// <auto-generated />
using System;
using FiledPayment.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FiledPayment.Persistence.Migrations
{
    [DbContext(typeof(FiledPaymentDbContext))]
    [Migration("20210506175446_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FiledPayment.Domain.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Amount")
                        .IsRequired()
                        .HasColumnType("VARCHAR(64)");

                    b.Property<string>("CardHolder")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("CreditCardNumber")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentStateId")
                        .HasColumnType("int");

                    b.Property<string>("SecurityCode")
                        .HasColumnType("VARCHAR(3)");

                    b.HasKey("Id");

                    b.HasIndex("PaymentStateId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("FiledPayment.Domain.Entities.PaymentState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.HasKey("Id");

                    b.ToTable("PaymentStates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pending"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Processed"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Failed"
                        });
                });

            modelBuilder.Entity("FiledPayment.Domain.Entities.Payment", b =>
                {
                    b.HasOne("FiledPayment.Domain.Entities.PaymentState", "PaymentState")
                        .WithMany()
                        .HasForeignKey("PaymentStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentState");
                });
#pragma warning restore 612, 618
        }
    }
}
