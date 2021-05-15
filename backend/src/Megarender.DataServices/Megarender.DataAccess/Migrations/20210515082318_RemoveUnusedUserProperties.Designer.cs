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
    [Migration("20210515082318_RemoveUnusedUserProperties")]
    partial class RemoveUnusedUserProperties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Megarender.Domain.AccessGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

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

            modelBuilder.Entity("Megarender.Domain.MoneyTransactionStatus", b =>
                {
                    b.Property<int>("MoneyTransactionStatusId")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("MoneyTransactionStatusId");

                    b.ToTable("MoneyTransactionStatus");

                    b.HasData(
                        new
                        {
                            MoneyTransactionStatusId = 1,
                            Value = "Created"
                        },
                        new
                        {
                            MoneyTransactionStatusId = 2,
                            Value = "Approved"
                        },
                        new
                        {
                            MoneyTransactionStatusId = 3,
                            Value = "Deposited"
                        },
                        new
                        {
                            MoneyTransactionStatusId = 4,
                            Value = "Declined"
                        },
                        new
                        {
                            MoneyTransactionStatusId = 5,
                            Value = "Reversed"
                        },
                        new
                        {
                            MoneyTransactionStatusId = 6,
                            Value = "Refunded"
                        });
                });

            modelBuilder.Entity("Megarender.Domain.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("UniqueIdentifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UniqueIdentifier")
                        .IsUnique();

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Megarender.Domain.OrganizationProject", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.HasKey("ProjectId", "OrganizationId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("OrganizationProject");
                });

            modelBuilder.Entity("Megarender.Domain.PrivateMoneyTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MoneyTransactionStatus")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("PrivateMoneyTransactions");
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

            modelBuilder.Entity("Megarender.Domain.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedById")
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
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Megarender.Domain.Render", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("SceneId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("SceneId");

                    b.ToTable("Renders");
                });

            modelBuilder.Entity("Megarender.Domain.Scene", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ProjectId");

                    b.ToTable("Scenes");
                });

            modelBuilder.Entity("Megarender.Domain.SharedMoneyTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MoneyTransactionStatus")
                        .HasColumnType("integer");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("OrganizationId");

                    b.ToTable("SharedMoneyTransactions");
                });

            modelBuilder.Entity("Megarender.Domain.User", b =>
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

                    b.ToTable("UserOrganizations");
                });

            modelBuilder.Entity("Megarender.Domain.UserProject", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("ProjectId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProject");
                });

            modelBuilder.Entity("Megarender.Domain.AccessGroup", b =>
                {
                    b.HasOne("Megarender.Domain.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Megarender.Domain.Organization", "Organization")
                        .WithMany("AccessGroups")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Organization");
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

                    b.Navigation("AccessGroup");

                    b.Navigation("Privilege");
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

                    b.Navigation("AccessGroup");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Megarender.Domain.Organization", b =>
                {
                    b.HasOne("Megarender.Domain.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Megarender.Domain.OrganizationProject", b =>
                {
                    b.HasOne("Megarender.Domain.Organization", "Organization")
                        .WithMany("OrganizationProjects")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Megarender.Domain.Project", "Project")
                        .WithMany("ProjectOrganizations")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Megarender.Domain.PrivateMoneyTransaction", b =>
                {
                    b.HasOne("Megarender.Domain.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Megarender.Domain.Project", b =>
                {
                    b.HasOne("Megarender.Domain.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Megarender.Domain.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.Navigation("CreatedBy");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Megarender.Domain.Render", b =>
                {
                    b.HasOne("Megarender.Domain.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Megarender.Domain.Scene", "Scene")
                        .WithMany("Renders")
                        .HasForeignKey("SceneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Scene");
                });

            modelBuilder.Entity("Megarender.Domain.Scene", b =>
                {
                    b.HasOne("Megarender.Domain.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Megarender.Domain.Project", "Project")
                        .WithMany("Scenes")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Megarender.Domain.SharedMoneyTransaction", b =>
                {
                    b.HasOne("Megarender.Domain.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Megarender.Domain.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Organization");
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

                    b.Navigation("Organization");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Megarender.Domain.UserProject", b =>
                {
                    b.HasOne("Megarender.Domain.Project", "Project")
                        .WithMany("ProjectUsers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Megarender.Domain.User", "User")
                        .WithMany("UserProjects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Megarender.Domain.AccessGroup", b =>
                {
                    b.Navigation("AccessGroupPrivileges");

                    b.Navigation("AccessGroupUsers");
                });

            modelBuilder.Entity("Megarender.Domain.Organization", b =>
                {
                    b.Navigation("AccessGroups");

                    b.Navigation("OrganizationProjects");

                    b.Navigation("OrganizationUsers");
                });

            modelBuilder.Entity("Megarender.Domain.Project", b =>
                {
                    b.Navigation("ProjectOrganizations");

                    b.Navigation("ProjectUsers");

                    b.Navigation("Scenes");
                });

            modelBuilder.Entity("Megarender.Domain.Scene", b =>
                {
                    b.Navigation("Renders");
                });

            modelBuilder.Entity("Megarender.Domain.User", b =>
                {
                    b.Navigation("UserAcessGroups");

                    b.Navigation("UserOrganizations");

                    b.Navigation("UserProjects");
                });
#pragma warning restore 612, 618
        }
    }
}
