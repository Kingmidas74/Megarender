﻿// <auto-generated />
using System;
using Megarender.StorageService.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace StorageService.Migrations
{
    [DbContext(typeof(StorageDbContext))]
    partial class StorageDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Megarender.StorageService.DAL.StorageSystem", b =>
                {
                    b.Property<int>("StorageSystemId")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("StorageSystemId");

                    b.ToTable("StorageSystem");

                    b.HasData(
                        new
                        {
                            StorageSystemId = 1,
                            Value = "FTP"
                        },
                        new
                        {
                            StorageSystemId = 2,
                            Value = "AZURE"
                        });
                });

            modelBuilder.Entity("Megarender.StorageService.DAL.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("StorageSystem")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
