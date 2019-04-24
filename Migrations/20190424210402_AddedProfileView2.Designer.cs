﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SmartPool.Migrations
{
    [DbContext(typeof(PoolContext))]
    [Migration("20190424210402_AddedProfileView2")]
    partial class AddedProfileView2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SmartPool.Models.Carpool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Carpools");
                });

            modelBuilder.Entity("SmartPool.Models.Commute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ArriveBy");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("Day");

                    b.Property<string>("EndCity");

                    b.Property<string>("EndPt");

                    b.Property<string>("StartPt");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int?>("carpoolId");

                    b.Property<int?>("userId");

                    b.HasKey("Id");

                    b.HasIndex("carpoolId");

                    b.HasIndex("userId");

                    b.ToTable("Commutes");
                });

            modelBuilder.Entity("SmartPool.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Phone");

                    b.Property<string>("PwHash");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SmartPool.Models.Commute", b =>
                {
                    b.HasOne("SmartPool.Models.Carpool", "carpool")
                        .WithMany("Commutes")
                        .HasForeignKey("carpoolId");

                    b.HasOne("SmartPool.Models.User", "user")
                        .WithMany("Commutes")
                        .HasForeignKey("userId");
                });
#pragma warning restore 612, 618
        }
    }
}
