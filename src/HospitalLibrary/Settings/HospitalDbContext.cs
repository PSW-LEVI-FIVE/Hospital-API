﻿using EntityFramework.Exceptions.PostgreSQL;
using HospitalLibrary.Doctors;
using HospitalLibrary.Feedbacks;
using HospitalLibrary.Patients;
using HospitalLibrary.Rooms;
using HospitalLibrary.Shared.Model;
using HospitalLibrary.Appointments;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Settings
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<WorkingHours> WorkingHours { get; set; }
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkingHours>().HasKey(w => new { w.DoctorId, w.Day });
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
            modelBuilder.Entity<Feedback>().ToTable("Feedbacks");
        }
    }
}