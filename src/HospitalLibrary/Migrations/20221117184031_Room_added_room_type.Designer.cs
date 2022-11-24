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
    [Migration("20221117184031_Room_added_room_type")]
    partial class Room_added_room_type
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("AllergenMedicine", b =>
                {
                    b.Property<int>("AllergensId")
                        .HasColumnType("integer");

                    b.Property<int>("MedicinesId")
                        .HasColumnType("integer");

                    b.HasKey("AllergensId", "MedicinesId");

                    b.HasIndex("MedicinesId");

                    b.ToTable("AllergenMedicine");
                });

            modelBuilder.Entity("HospitalLibrary.Allergens.Allergen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Allergens");
                });

            modelBuilder.Entity("HospitalLibrary.AnnualLeaves.AnnualLeave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsUrgent")
                        .HasColumnType("boolean");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("AnnualLeaves");
                });

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

            modelBuilder.Entity("HospitalLibrary.BloodOrders.BloodOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Arrival")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("BloodType")
                        .HasColumnType("integer");

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("Quantity")
                        .HasColumnType("double precision");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("BloodOrders");
                });

            modelBuilder.Entity("HospitalLibrary.BloodStorages.BloodStorage", b =>
                {
                    b.Property<int>("BloodType")
                        .HasColumnType("integer");

                    b.Property<double>("Quantity")
                        .HasColumnType("double precision");

                    b.HasKey("BloodType");

                    b.ToTable("BloodStorage");
                });

            modelBuilder.Entity("HospitalLibrary.Buildings.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

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

                    b.Property<float>("Area")
                        .HasColumnType("real");

                    b.Property<int>("BuildingId")
                        .HasColumnType("integer");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("Floors");
                });

            modelBuilder.Entity("HospitalLibrary.Hospitalizations.Hospitalization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BedId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MedicalRecordId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BedId");

                    b.HasIndex("MedicalRecordId");

                    b.ToTable("Hospitalizations");
                });

            modelBuilder.Entity("HospitalLibrary.Map.MapBuilding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BuildingId")
                        .HasColumnType("integer");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<string>("RgbColour")
                        .HasColumnType("text");

                    b.Property<float>("Width")
                        .HasColumnType("real");

                    b.Property<float>("XCoordinate")
                        .HasColumnType("real");

                    b.Property<float>("YCoordinate")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("MapBuildings");
                });

            modelBuilder.Entity("HospitalLibrary.Map.MapFloor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("FloorId")
                        .HasColumnType("integer");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<string>("RgbColour")
                        .HasColumnType("text");

                    b.Property<float>("Width")
                        .HasColumnType("real");

                    b.Property<float>("XCoordinate")
                        .HasColumnType("real");

                    b.Property<float>("YCoordinate")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.ToTable("MapFloors");
                });

            modelBuilder.Entity("HospitalLibrary.Map.MapRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<int>("MapFloorId")
                        .HasColumnType("integer");

                    b.Property<string>("RbgColour")
                        .HasColumnType("text");

                    b.Property<int>("RoomId")
                        .HasColumnType("integer");

                    b.Property<float>("Width")
                        .HasColumnType("real");

                    b.Property<float>("XCoordinate")
                        .HasColumnType("real");

                    b.Property<float>("YCoordinate")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("MapFloorId");

                    b.HasIndex("RoomId");

                    b.ToTable("MapRooms");
                });

            modelBuilder.Entity("HospitalLibrary.MedicalRecords.MedicalRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("MedicalRecords");
                });

            modelBuilder.Entity("HospitalLibrary.Medicines.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("HospitalLibrary.Rooms.Model.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<float>("Area")
                        .HasColumnType("real");

                    b.Property<int>("FloorId")
                        .HasColumnType("integer");

                    b.Property<string>("RoomNumber")
                        .HasColumnType("text");

                    b.Property<int>("RoomType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HospitalLibrary.Rooms.Model.RoomEquipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("RoomId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomEquipment");
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

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Uid")
                        .IsUnique();

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

            modelBuilder.Entity("HospitalLibrary.Therapies.Model.Therapy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("GivenAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("HospitalizationId")
                        .HasColumnType("integer");

                    b.Property<string>("therapy_type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HospitalizationId");

                    b.ToTable("Therapies");

                    b.HasDiscriminator<string>("therapy_type").HasValue("Therapy");
                });

            modelBuilder.Entity("HospitalLibrary.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HospitalLibrary.Rooms.Model.Bed", b =>
                {
                    b.HasBaseType("HospitalLibrary.Rooms.Model.RoomEquipment");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.ToTable("Beds");
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

                    b.Property<int>("BloodType")
                        .HasColumnType("integer");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("HospitalLibrary.Therapies.Model.BloodTherapy", b =>
                {
                    b.HasBaseType("HospitalLibrary.Therapies.Model.Therapy");

                    b.Property<int>("BloodType")
                        .HasColumnType("integer");

                    b.Property<double>("Quantity")
                        .HasColumnType("double precision")
                        .HasColumnName("BloodTherapy_Quantity");

                    b.HasDiscriminator().HasValue("blood");
                });

            modelBuilder.Entity("HospitalLibrary.Therapies.Model.MedicineTherapy", b =>
                {
                    b.HasBaseType("HospitalLibrary.Therapies.Model.Therapy");

                    b.Property<int>("MedicineId")
                        .HasColumnType("integer");

                    b.Property<double>("Quantity")
                        .HasColumnType("double precision");

                    b.HasIndex("MedicineId");

                    b.HasDiscriminator().HasValue("medicine");
                });

            modelBuilder.Entity("AllergenMedicine", b =>
                {
                    b.HasOne("HospitalLibrary.Allergens.Allergen", null)
                        .WithMany()
                        .HasForeignKey("AllergensId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospitalLibrary.Medicines.Medicine", null)
                        .WithMany()
                        .HasForeignKey("MedicinesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HospitalLibrary.AnnualLeaves.AnnualLeave", b =>
                {
                    b.HasOne("HospitalLibrary.Doctors.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
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

                    b.HasOne("HospitalLibrary.Rooms.Model.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HospitalLibrary.BloodOrders.BloodOrder", b =>
                {
                    b.HasOne("HospitalLibrary.Doctors.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
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

            modelBuilder.Entity("HospitalLibrary.Hospitalizations.Hospitalization", b =>
                {
                    b.HasOne("HospitalLibrary.Rooms.Model.Bed", "Bed")
                        .WithMany("Hospitalizations")
                        .HasForeignKey("BedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospitalLibrary.MedicalRecords.MedicalRecord", "MedicalRecord")
                        .WithMany("Hospitalizations")
                        .HasForeignKey("MedicalRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bed");

                    b.Navigation("MedicalRecord");
                });

            modelBuilder.Entity("HospitalLibrary.Map.MapBuilding", b =>
                {
                    b.HasOne("HospitalLibrary.Buildings.Building", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");
                });

            modelBuilder.Entity("HospitalLibrary.Map.MapFloor", b =>
                {
                    b.HasOne("HospitalLibrary.Floors.Floor", "Floor")
                        .WithMany()
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Floor");
                });

            modelBuilder.Entity("HospitalLibrary.Map.MapRoom", b =>
                {
                    b.HasOne("HospitalLibrary.Map.MapFloor", "MapFloor")
                        .WithMany("MapRooms")
                        .HasForeignKey("MapFloorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospitalLibrary.Rooms.Model.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MapFloor");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HospitalLibrary.MedicalRecords.MedicalRecord", b =>
                {
                    b.HasOne("HospitalLibrary.Patients.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("HospitalLibrary.Rooms.Model.Room", b =>
                {
                    b.HasOne("HospitalLibrary.Floors.Floor", "Floor")
                        .WithMany("Rooms")
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Floor");
                });

            modelBuilder.Entity("HospitalLibrary.Rooms.Model.RoomEquipment", b =>
                {
                    b.HasOne("HospitalLibrary.Rooms.Model.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
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

            modelBuilder.Entity("HospitalLibrary.Therapies.Model.Therapy", b =>
                {
                    b.HasOne("HospitalLibrary.Hospitalizations.Hospitalization", "Hospitalization")
                        .WithMany("Therapies")
                        .HasForeignKey("HospitalizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hospitalization");
                });

            modelBuilder.Entity("HospitalLibrary.Users.User", b =>
                {
                    b.HasOne("HospitalLibrary.Shared.Model.Person", "Person")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("HospitalLibrary.Rooms.Model.Bed", b =>
                {
                    b.HasOne("HospitalLibrary.Rooms.Model.RoomEquipment", null)
                        .WithOne()
                        .HasForeignKey("HospitalLibrary.Rooms.Model.Bed", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("HospitalLibrary.Therapies.Model.MedicineTherapy", b =>
                {
                    b.HasOne("HospitalLibrary.Medicines.Medicine", "Medicine")
                        .WithMany()
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("HospitalLibrary.Buildings.Building", b =>
                {
                    b.Navigation("Floors");
                });

            modelBuilder.Entity("HospitalLibrary.Floors.Floor", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("HospitalLibrary.Hospitalizations.Hospitalization", b =>
                {
                    b.Navigation("Therapies");
                });

            modelBuilder.Entity("HospitalLibrary.Map.MapFloor", b =>
                {
                    b.Navigation("MapRooms");
                });

            modelBuilder.Entity("HospitalLibrary.MedicalRecords.MedicalRecord", b =>
                {
                    b.Navigation("Hospitalizations");
                });

            modelBuilder.Entity("HospitalLibrary.Rooms.Model.Bed", b =>
                {
                    b.Navigation("Hospitalizations");
                });

            modelBuilder.Entity("HospitalLibrary.Doctors.Doctor", b =>
                {
                    b.Navigation("WorkingHours");
                });
#pragma warning restore 612, 618
        }
    }
}
