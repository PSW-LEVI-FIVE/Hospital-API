﻿// <auto-generated />
using System;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HospitalLibrary.Migrations
{
    [DbContext(typeof(HospitalDbContext))]
    [Migration("20221101224248_Buildings_And_Floors")]
    partial class Buildings_And_Floors
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("HospitalLibrary.Appointments.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.Property<int>("RoomId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.HasIndex("RoomId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("HospitalLibrary.Buildings.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("HospitalLibrary.Feedbacks.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("AllowPublishment")
                        .HasColumnType("boolean");

                    b.Property<bool>("Anonimity")
                        .HasColumnType("boolean");

                    b.Property<string>("FeedbackContent")
                        .HasColumnType("text");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.Property<bool>("Published")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("HospitalLibrary.Floors.Floor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BuildingId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("Floors");
                });

            modelBuilder.Entity("HospitalLibrary.Rooms.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("FloorId")
                        .HasColumnType("integer");

                    b.Property<string>("RoomNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HospitalLibrary.Shared.Model.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<string>("Uid")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("HospitalLibrary.Shared.Model.WorkingHours", b =>
                {
                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<int>("Day")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("End")
                        .HasColumnType("interval");

                    b.Property<TimeSpan>("Start")
                        .HasColumnType("interval");

                    b.HasKey("DoctorId", "Day");

                    b.ToTable("WorkingHours");
                });

            modelBuilder.Entity("HospitalLibrary.Doctors.Doctor", b =>
                {
                    b.HasBaseType("HospitalLibrary.Shared.Model.Person");

                    b.Property<int>("SpecialtyType")
                        .HasColumnType("integer");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("HospitalLibrary.Patients.Patient", b =>
                {
                    b.HasBaseType("HospitalLibrary.Shared.Model.Person");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("HospitalLibrary.Appointments.Appointment", b =>
                {
                    b.HasOne("HospitalLibrary.Doctors.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospitalLibrary.Patients.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospitalLibrary.Rooms.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HospitalLibrary.Feedbacks.Feedback", b =>
                {
                    b.HasOne("HospitalLibrary.Patients.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("HospitalLibrary.Floors.Floor", b =>
                {
                    b.HasOne("HospitalLibrary.Buildings.Building", "Building")
                        .WithMany("Floors")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");
                });

            modelBuilder.Entity("HospitalLibrary.Rooms.Room", b =>
                {
                    b.HasOne("HospitalLibrary.Floors.Floor", "Floor")
                        .WithMany("Rooms")
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Floor");
                });

            modelBuilder.Entity("HospitalLibrary.Shared.Model.WorkingHours", b =>
                {
                    b.HasOne("HospitalLibrary.Doctors.Doctor", "Doctor")
                        .WithMany("WorkingHours")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("HospitalLibrary.Doctors.Doctor", b =>
                {
                    b.HasOne("HospitalLibrary.Shared.Model.Person", null)
                        .WithOne()
                        .HasForeignKey("HospitalLibrary.Doctors.Doctor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HospitalLibrary.Patients.Patient", b =>
                {
                    b.HasOne("HospitalLibrary.Shared.Model.Person", null)
                        .WithOne()
                        .HasForeignKey("HospitalLibrary.Patients.Patient", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HospitalLibrary.Buildings.Building", b =>
                {
                    b.Navigation("Floors");
                });

            modelBuilder.Entity("HospitalLibrary.Floors.Floor", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("HospitalLibrary.Doctors.Doctor", b =>
                {
                    b.Navigation("WorkingHours");
                });
#pragma warning restore 612, 618
        }
    }
}
