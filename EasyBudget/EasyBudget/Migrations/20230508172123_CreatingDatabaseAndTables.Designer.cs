﻿// <auto-generated />
using System;
using EasyBudget.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EasyBudget.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230508172123_CreatingDatabaseAndTables")]
    partial class CreatingDatabaseAndTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EasyBudget.Data.Models.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Created = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1582),
                            Name = "Transport",
                            Type = 2,
                            Updated = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1594)
                        },
                        new
                        {
                            Id = 2L,
                            Created = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1595),
                            Name = "Food",
                            Type = 2,
                            Updated = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1595)
                        },
                        new
                        {
                            Id = 3L,
                            Created = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1596),
                            Name = "Groceries",
                            Type = 2,
                            Updated = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1596)
                        },
                        new
                        {
                            Id = 4L,
                            Created = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1597),
                            Name = "Health",
                            Type = 2,
                            Updated = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1597)
                        },
                        new
                        {
                            Id = 5L,
                            Created = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1598),
                            Name = "Work",
                            Type = 2,
                            Updated = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1598)
                        },
                        new
                        {
                            Id = 6L,
                            Created = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1623),
                            Name = "Home",
                            Type = 2,
                            Updated = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1624)
                        },
                        new
                        {
                            Id = 7L,
                            Created = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1624),
                            Name = "Investments",
                            Type = 2,
                            Updated = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1625)
                        },
                        new
                        {
                            Id = 8L,
                            Created = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1625),
                            Name = "Others expenses",
                            Type = 2,
                            Updated = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1626)
                        },
                        new
                        {
                            Id = 9L,
                            Created = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1626),
                            Name = "Salary",
                            Type = 1,
                            Updated = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1627)
                        },
                        new
                        {
                            Id = 10L,
                            Created = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1627),
                            Name = "Investments",
                            Type = 1,
                            Updated = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1628)
                        },
                        new
                        {
                            Id = 11L,
                            Created = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1628),
                            Name = "Other incomes",
                            Type = 1,
                            Updated = new DateTime(2023, 5, 8, 14, 21, 23, 860, DateTimeKind.Local).AddTicks(1629)
                        });
                });

            modelBuilder.Entity("EasyBudget.Data.Models.Movement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Movements");
                });

            modelBuilder.Entity("EasyBudget.Data.Models.Movement", b =>
                {
                    b.HasOne("EasyBudget.Data.Models.Category", "Category")
                        .WithMany("Movements")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EasyBudget.Data.Models.Category", b =>
                {
                    b.Navigation("Movements");
                });
#pragma warning restore 612, 618
        }
    }
}