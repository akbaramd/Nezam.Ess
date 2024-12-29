﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nezam.EES.Gateway;

#nullable disable

namespace Nezam.EES.Gateway.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Nezam.EES.Service.Identity.Domains.Roles.RoleEntity", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Nezam.EES.Service.Identity.Domains.Users.UserEntity", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Nezam.EES.Service.Identity.Domains.Users.UserTokenEntity", b =>
                {
                    b.Property<Guid>("TokenId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TypeId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("TokenId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokenEntity");
                });

            modelBuilder.Entity("Nezam.EES.Service.Identity.Domains.Users.UserVerificationTokenType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserVerificationTokenType");
                });

            modelBuilder.Entity("Nezam.EES.Slice.Secretariat.Domains.Documents.DocumentAggregateRoot", b =>
                {
                    b.Property<Guid>("DocumentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OwnerParticipantId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ReceiverParticipantId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("DocumentId");

                    b.HasIndex("OwnerParticipantId");

                    b.HasIndex("ReceiverParticipantId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Nezam.EES.Slice.Secretariat.Domains.Documents.DocumentAttachmentEntity", b =>
                {
                    b.Property<Guid>("DocumentAttachmentId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<long>("FileSize")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("TEXT");

                    b.HasKey("DocumentAttachmentId");

                    b.HasIndex("DocumentId");

                    b.ToTable("DocumentAttachments");
                });

            modelBuilder.Entity("Nezam.EES.Slice.Secretariat.Domains.Documents.DocumentReferralEntity", b =>
                {
                    b.Property<Guid>("DocumentReferralId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ParentReferralId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ReceiverUserId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReferralDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ReferrerUserId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RespondedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ResponseContent")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ViewedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("DocumentReferralId");

                    b.HasIndex("DocumentId");

                    b.HasIndex("ParentReferralId");

                    b.HasIndex("ReceiverUserId");

                    b.HasIndex("ReferrerUserId");

                    b.ToTable("DocumentReferrals");
                });

            modelBuilder.Entity("Nezam.EES.Slice.Secretariat.Domains.Participant.Participant", b =>
                {
                    b.Property<Guid>("ParticipantId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("ParticipantId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("UserRoles", b =>
                {
                    b.Property<string>("RolesRoleId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UsersUserId")
                        .HasColumnType("TEXT");

                    b.HasKey("RolesRoleId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Nezam.EES.Service.Identity.Domains.Users.UserEntity", b =>
                {
                    b.OwnsOne("Nezam.EEs.Shared.Domain.Identity.User.ValueObjects.UserEmailValue", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserEntityUserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("Email");

                            b1.HasKey("UserEntityUserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserEntityUserId");
                        });

                    b.OwnsOne("Nezam.EEs.Shared.Domain.Identity.User.ValueObjects.UserPasswordValue", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserEntityUserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("Password");

                            b1.HasKey("UserEntityUserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserEntityUserId");
                        });

                    b.OwnsOne("Nezam.EEs.Shared.Domain.Identity.User.ValueObjects.UserProfileValue", "Profile", b1 =>
                        {
                            b1.Property<Guid>("UserEntityUserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("LastName");

                            b1.HasKey("UserEntityUserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserEntityUserId");
                        });

                    b.Navigation("Email");

                    b.Navigation("Password")
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Nezam.EES.Service.Identity.Domains.Users.UserTokenEntity", b =>
                {
                    b.HasOne("Nezam.EES.Service.Identity.Domains.Users.UserVerificationTokenType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nezam.EES.Service.Identity.Domains.Users.UserEntity", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Nezam.EES.Slice.Secretariat.Domains.Documents.DocumentAggregateRoot", b =>
                {
                    b.HasOne("Nezam.EES.Slice.Secretariat.Domains.Participant.Participant", "OwnerParticipant")
                        .WithMany()
                        .HasForeignKey("OwnerParticipantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Nezam.EES.Slice.Secretariat.Domains.Participant.Participant", "ReceiverParticipant")
                        .WithMany()
                        .HasForeignKey("ReceiverParticipantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("OwnerParticipant");

                    b.Navigation("ReceiverParticipant");
                });

            modelBuilder.Entity("Nezam.EES.Slice.Secretariat.Domains.Documents.DocumentAttachmentEntity", b =>
                {
                    b.HasOne("Nezam.EES.Slice.Secretariat.Domains.Documents.DocumentAggregateRoot", null)
                        .WithMany("Attachments")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nezam.EES.Slice.Secretariat.Domains.Documents.DocumentReferralEntity", b =>
                {
                    b.HasOne("Nezam.EES.Slice.Secretariat.Domains.Documents.DocumentAggregateRoot", null)
                        .WithMany("Referrals")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nezam.EES.Slice.Secretariat.Domains.Documents.DocumentReferralEntity", null)
                        .WithMany()
                        .HasForeignKey("ParentReferralId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Nezam.EES.Slice.Secretariat.Domains.Participant.Participant", "ReceiverUser")
                        .WithMany()
                        .HasForeignKey("ReceiverUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Nezam.EES.Slice.Secretariat.Domains.Participant.Participant", "ReferrerUser")
                        .WithMany()
                        .HasForeignKey("ReferrerUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ReceiverUser");

                    b.Navigation("ReferrerUser");
                });

            modelBuilder.Entity("UserRoles", b =>
                {
                    b.HasOne("Nezam.EES.Service.Identity.Domains.Roles.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RolesRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nezam.EES.Service.Identity.Domains.Users.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nezam.EES.Service.Identity.Domains.Users.UserEntity", b =>
                {
                    b.Navigation("Tokens");
                });

            modelBuilder.Entity("Nezam.EES.Slice.Secretariat.Domains.Documents.DocumentAggregateRoot", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("Referrals");
                });
#pragma warning restore 612, 618
        }
    }
}
