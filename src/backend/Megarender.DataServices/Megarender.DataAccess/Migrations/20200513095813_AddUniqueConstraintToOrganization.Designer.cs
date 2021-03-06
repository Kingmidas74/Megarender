﻿// <auto-generated />
using System;
using Megarender.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Megarender.DataAccess.Migrations
{
    [DbContext(typeof(APIContext))]
    [Migration("20200513095813_AddUniqueConstraintToOrganization")]
    partial class AddUniqueConstraintToOrganization
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Megarender.Domain.AccessGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("AccessGroups");
                });

            modelBuilder.Entity("Megarender.Domain.AccessGroupPrivilege", b =>
                {
                    b.Property<Guid>("AccessGroupId")
                        .HasColumnType("uuid");

                    b.Property<int>("PrivilegeId")
                        .HasColumnType("integer");

                    b.HasKey("AccessGroupId", "PrivilegeId");

                    b.HasIndex("PrivilegeId");

                    b.ToTable("AccessGroupPrivilege");
                });

            modelBuilder.Entity("Megarender.Domain.AccessGroupUser", b =>
                {
                    b.Property<Guid>("AccessGroupId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("AccessGroupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("AccessGroupUser");
                });

            modelBuilder.Entity("Megarender.Domain.EntityStatus", b =>
                {
                    b.Property<int>("EntityStatusId")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("EntityStatusId");

                    b.ToTable("EntityStatus");

                    b.HasData(
                        new
                        {
                            EntityStatusId = 1,
                            Value = "Active"
                        },
                        new
                        {
                            EntityStatusId = 2,
                            Value = "Inactive"
                        });
                });

            modelBuilder.Entity("Megarender.Domain.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("UniqueIdentifier")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UniqueIdentifier")
                        .IsUnique();

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Megarender.Domain.Privilege", b =>
                {
                    b.Property<int>("PrivilegeId")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("PrivilegeId");

                    b.ToTable("Privilege");

                    b.HasData(
                        new
                        {
                            PrivilegeId = 0,
                            Value = "CanAuthorize"
                        },
                        new
                        {
                            PrivilegeId = 1,
                            Value = "CanSeeScenes"
                        },
                        new
                        {
                            PrivilegeId = 2,
                            Value = "CanSeeRenderTasks"
                        });
                });

            modelBuilder.Entity("Megarender.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("SecondName")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("SurName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Megarender.Domain.UserOrganization", b =>
                {
                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("OrganizationId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserOrganization");
                });

            modelBuilder.Entity("Megarender.Domain.AccessGroup", b =>
                {
                    b.HasOne("Megarender.Domain.Organization", "Organization")
                        .WithMany("AccessGroups")
                        .HasForeignKey("OrganizationId");
                });

            modelBuilder.Entity("Megarender.Domain.AccessGroupPrivilege", b =>
                {
                    b.HasOne("Megarender.Domain.AccessGroup", "AccessGroup")
                        .WithMany("AccessGroupPrivileges")
                        .HasForeignKey("AccessGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Megarender.Domain.Privilege", "Privilege")
                        .WithMany()
                        .HasForeignKey("PrivilegeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Megarender.Domain.AccessGroupUser", b =>
                {
                    b.HasOne("Megarender.Domain.AccessGroup", "AccessGroup")
                        .WithMany("AccessGroupUsers")
                        .HasForeignKey("AccessGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Megarender.Domain.User", "User")
                        .WithMany("UserAcessGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Megarender.Domain.UserOrganization", b =>
                {
                    b.HasOne("Megarender.Domain.Organization", "Organization")
                        .WithMany("OrganizationUsers")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Megarender.Domain.User", "User")
                        .WithMany("UserOrganizations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
