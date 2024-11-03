﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nezam.Modular.ESS.Identity.infrastructure.Data;

#nullable disable

namespace Nezam.Modular.ESS.Identity.infrastructure.Migrations
{
    [DbContext(typeof(IdentityDbContext))]
    [Migration("20241103062825_Init2")]
    partial class Init2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Nezam.Modular.ESS.Identity.Domain.Employer.EmployerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Employers");
                });

            modelBuilder.Entity("Nezam.Modular.ESS.Identity.Domain.Engineer.EngineerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Engineers");
                });

            modelBuilder.Entity("Nezam.Modular.ESS.Identity.Domain.Roles.RoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Nezam.Modular.ESS.Identity.Domain.User.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("DeletedDate");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("ModifiedDate");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Nezam.Modular.ESS.Identity.Domain.User.UserVerificationTokenEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserVerificationToken");
                });

            modelBuilder.Entity("UserRoles", b =>
                {
                    b.Property<Guid>("RolesId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("TEXT");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Nezam.Modular.ESS.Identity.Domain.Employer.EmployerEntity", b =>
                {
                    b.HasOne("Nezam.Modular.ESS.Identity.Domain.User.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Nezam.Modular.ESS.Identity.Domain.Engineer.EngineerEntity", b =>
                {
                    b.HasOne("Nezam.Modular.ESS.Identity.Domain.User.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Nezam.Modular.ESS.Identity.Domain.User.UserEntity", b =>
                {
                    b.OwnsOne("Bonyan.UserManagement.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserEntityId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("EmailAddress");

                            b1.Property<bool>("IsVerified")
                                .HasColumnType("INTEGER")
                                .HasColumnName("EmailIsVerified");

                            b1.HasKey("UserEntityId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserEntityId");
                        });

                    b.OwnsOne("Bonyan.UserManagement.Domain.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserEntityId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("HashedPassword")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("PasswordHash");

                            b1.Property<byte[]>("Salt")
                                .IsRequired()
                                .HasColumnType("BLOB")
                                .HasColumnName("PasswordSalt");

                            b1.HasKey("UserEntityId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserEntityId");
                        });

                    b.OwnsOne("Bonyan.UserManagement.Domain.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("UserEntityId")
                                .HasColumnType("TEXT");

                            b1.Property<bool>("IsVerified")
                                .HasColumnType("INTEGER")
                                .HasColumnName("PhoneNumberIsVerified");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("UserEntityId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserEntityId");
                        });

                    b.Navigation("Email");

                    b.Navigation("Password")
                        .IsRequired();

                    b.Navigation("PhoneNumber");
                });

            modelBuilder.Entity("Nezam.Modular.ESS.Identity.Domain.User.UserVerificationTokenEntity", b =>
                {
                    b.HasOne("Nezam.Modular.ESS.Identity.Domain.User.UserEntity", "User")
                        .WithMany("VerificationTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserRoles", b =>
                {
                    b.HasOne("Nezam.Modular.ESS.Identity.Domain.Roles.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nezam.Modular.ESS.Identity.Domain.User.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nezam.Modular.ESS.Identity.Domain.User.UserEntity", b =>
                {
                    b.Navigation("VerificationTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
