﻿// <auto-generated />
using System;
using ATLANT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ATLANT.Migrations
{
    [DbContext(typeof(FitnesContext))]
    [Migration("20240505101315_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ATLANT.Models.Abonement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("money");

                    b.Property<int>("CountDays")
                        .HasColumnType("int");

                    b.Property<int>("CountVisits")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TypeService")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeTraining")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Abonement");
                });

            modelBuilder.Entity("ATLANT.Models.Client", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .HasColumnType("money");

                    b.HasKey("UserId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("ATLANT.Models.Coach", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("Coachs");
                });

            modelBuilder.Entity("ATLANT.Models.DayWeek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("DayWeek");
                });

            modelBuilder.Entity("ATLANT.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AbonementId")
                        .HasColumnType("int");

                    b.Property<int>("CountRemainTraining")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("date");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("date");

                    b.Property<bool>("IsValid")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AbonementId");

                    b.HasIndex("UserId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("ATLANT.Models.ServiceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NameService")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("ServiceType");
                });

            modelBuilder.Entity("ATLANT.Models.Shedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoachId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<int>("DayWeekId")
                        .HasColumnType("int");

                    b.Property<int>("MaxCount")
                        .HasColumnType("int");

                    b.Property<int>("ServiceTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeEnd")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("TimeStart")
                        .HasColumnType("datetime");

                    b.Property<int>("TypeTrainingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.HasIndex("DayWeekId");

                    b.HasIndex("ServiceTypeId");

                    b.HasIndex("TypeTrainingId");

                    b.ToTable("Shedule");
                });

            modelBuilder.Entity("ATLANT.Models.TimeTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CoachUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<int>("MaxCount")
                        .HasColumnType("int");

                    b.Property<int?>("ServiceTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeEnd")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("TimeStart")
                        .HasColumnType("datetime");

                    b.Property<int?>("TypeTrainingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CoachUserId");

                    b.HasIndex("ServiceTypeId");

                    b.HasIndex("TypeTrainingId");

                    b.ToTable("TimeTable");
                });

            modelBuilder.Entity("ATLANT.Models.TypeTraining", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NameType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("TypeTraining");
                });

            modelBuilder.Entity("ATLANT.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("date");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FIO")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nickname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ATLANT.Models.VisitRegister", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("PaymentId")
                        .HasColumnType("int");

                    b.Property<int?>("TimeTableId")
                        .HasColumnType("int");

                    b.Property<int>("VisitNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId");

                    b.HasIndex("TimeTableId");

                    b.ToTable("VisitRegister");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ATLANT.Models.Client", b =>
                {
                    b.HasOne("ATLANT.Models.User", "User")
                        .WithOne("Client")
                        .HasForeignKey("ATLANT.Models.Client", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ATLANT.Models.Coach", b =>
                {
                    b.HasOne("ATLANT.Models.User", "User")
                        .WithOne("Coach")
                        .HasForeignKey("ATLANT.Models.Coach", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ATLANT.Models.Payment", b =>
                {
                    b.HasOne("ATLANT.Models.Abonement", "Abonement")
                        .WithMany("Payment")
                        .HasForeignKey("AbonementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ATLANT.Models.Client", "Client")
                        .WithMany("Payment")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Abonement");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("ATLANT.Models.Shedule", b =>
                {
                    b.HasOne("ATLANT.Models.Coach", "Coach")
                        .WithMany("Shedule")
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ATLANT.Models.DayWeek", "DayWeek")
                        .WithMany("Shedule")
                        .HasForeignKey("DayWeekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ATLANT.Models.ServiceType", "ServiceType")
                        .WithMany("Shedule")
                        .HasForeignKey("ServiceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ATLANT.Models.TypeTraining", "TypeTraining")
                        .WithMany("Shedule")
                        .HasForeignKey("TypeTrainingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coach");

                    b.Navigation("DayWeek");

                    b.Navigation("ServiceType");

                    b.Navigation("TypeTraining");
                });

            modelBuilder.Entity("ATLANT.Models.TimeTable", b =>
                {
                    b.HasOne("ATLANT.Models.Coach", null)
                        .WithMany("TimeTable")
                        .HasForeignKey("CoachUserId");

                    b.HasOne("ATLANT.Models.ServiceType", null)
                        .WithMany("TimeTable")
                        .HasForeignKey("ServiceTypeId");

                    b.HasOne("ATLANT.Models.TypeTraining", null)
                        .WithMany("TimeTable")
                        .HasForeignKey("TypeTrainingId");
                });

            modelBuilder.Entity("ATLANT.Models.VisitRegister", b =>
                {
                    b.HasOne("ATLANT.Models.Payment", null)
                        .WithMany("VisitRegister")
                        .HasForeignKey("PaymentId");

                    b.HasOne("ATLANT.Models.TimeTable", null)
                        .WithMany("VisitRegister")
                        .HasForeignKey("TimeTableId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("ATLANT.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("ATLANT.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ATLANT.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("ATLANT.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ATLANT.Models.Abonement", b =>
                {
                    b.Navigation("Payment");
                });

            modelBuilder.Entity("ATLANT.Models.Client", b =>
                {
                    b.Navigation("Payment");
                });

            modelBuilder.Entity("ATLANT.Models.Coach", b =>
                {
                    b.Navigation("Shedule");

                    b.Navigation("TimeTable");
                });

            modelBuilder.Entity("ATLANT.Models.DayWeek", b =>
                {
                    b.Navigation("Shedule");
                });

            modelBuilder.Entity("ATLANT.Models.Payment", b =>
                {
                    b.Navigation("VisitRegister");
                });

            modelBuilder.Entity("ATLANT.Models.ServiceType", b =>
                {
                    b.Navigation("Shedule");

                    b.Navigation("TimeTable");
                });

            modelBuilder.Entity("ATLANT.Models.TimeTable", b =>
                {
                    b.Navigation("VisitRegister");
                });

            modelBuilder.Entity("ATLANT.Models.TypeTraining", b =>
                {
                    b.Navigation("Shedule");

                    b.Navigation("TimeTable");
                });

            modelBuilder.Entity("ATLANT.Models.User", b =>
                {
                    b.Navigation("Client");

                    b.Navigation("Coach");
                });
#pragma warning restore 612, 618
        }
    }
}