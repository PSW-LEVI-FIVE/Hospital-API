using HospitalAPI;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Buildings;
using HospitalLibrary.Floors;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Map;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.Allergens;
using HospitalLibrary.Doctors;
using HospitalLibrary.Medicines;
using HospitalLibrary.Patients;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Model;
using HospitalLibrary.Shared.Repository;
using HospitalLibrary.Therapies.Model;
using HospitalLibrary.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.Appointments;
using HospitalLibrary.Examination;
using HospitalLibrary.Shared.Model.ValueObjects;
using HospitalLibrary.Symptoms;

namespace HospitalTests.Setup;

public class TestDatabaseFactory<TStartup> : WebApplicationFactory<Startup>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            using var scope = BuildServiceProvider(services).CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<HospitalDbContext>();
            InitializeDatabase(db);
        });
    }


    private static ServiceProvider BuildServiceProvider(IServiceCollection services)
    {
        ServiceDescriptor? descriptor = services.FirstOrDefault(service => typeof(DbContextOptions<HospitalDbContext>) == service.ServiceType);

        services.Remove(descriptor);

        services.AddDbContext<HospitalDbContext>(option =>
        {
            option.UseNpgsql(CreateTestingConnectionString());
        });

        return services.BuildServiceProvider();
    }

    private static string CreateTestingConnectionString()
    {
        return "Host=localhost;Database=HospitalDbTest;Username=postgres;Password=ftn";
    }

    private static void InitializeDatabase(HospitalDbContext dbContext)
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        dbContext.Database.ExecuteSqlRaw(
            "CREATE OR REPLACE FUNCTION truncate_tables(username IN VARCHAR) RETURNS void AS $$ " +
            "DECLARE " +
            "statements CURSOR FOR " +
            "SELECT tablename FROM pg_tables " +
            "WHERE tableowner = username AND schemaname = 'public'; " +
            "BEGIN " +
            "    FOR stmt IN statements LOOP " +
            "EXECUTE 'TRUNCATE TABLE ' || quote_ident(stmt.tablename) || ' CASCADE;'; " +
            "END LOOP; " +
            "END; " +
            "    $$ LANGUAGE plpgsql; "
            );
        dbContext.Database.ExecuteSqlRaw("SELECT truncate_tables('postgres');");

        Speciality speciality1 = new Speciality(1, "INTERNAL_MEDICINE");
        Speciality speciality2 = new Speciality(2, "SURGERY");
        
        Doctor doctor = new Doctor()
        {
            Id = 4,
            Name = "Prvi",
            Surname = "Drugi",
            Address =  new Address("Srbija", "Novi Sad", "Sase Krstica", "4"),
            BirthDate = DateTime.Now,
            Email = "nekimail@gmail.com",
            PhoneNumber = new PhoneNumber("+1233555333"),
            SpecialityId = 2,
            Uid = "55557888",
            WorkingHours = new List<WorkingHours>()
        };

        BloodStorage bloodStorage = new BloodStorage()
        {
            BloodType = BloodType.A_NEGATIVE,
            Quantity = 5.0
        };

        Building building = new Building()
        {
            Id = 2,
            Address = "NEKA ADRESA",
            Name = "Neko ime"
        };

        MapBuilding mapBuilding = new MapBuilding()
        {
            Id = 2,
            BuildingId = building.Id,
            Height = 200,
            Width = 1000,
            XCoordinate = 50,
            YCoordinate = 50,
            RgbColour = "#FFFFFF"
        };

        Floor floor = new Floor(2, 100, 2);


        MapFloor mapFloor = new MapFloor()
        {
            Id = 2,
            FloorId = floor.Id,
            Height = 100,
            Width = 100,
            XCoordinate = 100,
            YCoordinate = 100,
            RgbColour = "#FFFFFF",
            MapBuildingId = mapBuilding.Id
        };

        Room room1 = new Room()
        {
            Id = 2,
            Area = 10,
            FloorId = 2,
            RoomNumber = "1",
            RoomType = RoomType.EXAMINATION_ROOM
        };
        
        Room room2 = new Room()
        {
            Id = 3,
            Area = 10,
            FloorId = 2,
            RoomNumber = "123",
            RoomType = RoomType.EXAMINATION_ROOM
        };

        RoomEquipment equipment = new Bed(1, 10, "Bed", 2, 1);
        RoomEquipment equipment2 = new Bed(2, 10, "Bed", 2, 1);

        MapRoom mapRoom = new MapRoom()
        {
            RoomId = room1.Id,
            Height = 10,
            Width = 10,
            XCoordinate = 10,
            YCoordinate = 10,
            MapFloorId = mapFloor.Id
        };

        Patient patient = new Patient()
        {
            Id = 1,
            Name = "Marko",
            Surname = "Markovic",
            Email = "asdasd1@gmail.coma",
            Uid = "67676767",
            PhoneNumber = new PhoneNumber("+123123123"),
            BirthDate = new DateTime(2000, 2, 2),
            Address =  new Address("Srbija", "Novi Sad", "Sase Krstica", "4"),
            BloodType = BloodType.A_NEGATIVE,
            ChoosenDoctor = doctor
        };

        User user = new User()
        {
            Username = "Mika",
            Password = new Password("plsradi123"),
            Role = Role.Patient,
            Id = 1
        };
        Allergen allergen1 = new Allergen(1,"Milk");
        List<Allergen> allergens = new List<Allergen>();
        allergens.Add(allergen1);
        
        Patient patient2 = new Patient()
        {
            Id = 2,
            Name = "Marko",
            Surname = "Markovic",
            Email = "asdasd2@gmail.com",
            Uid = "78787878",
            PhoneNumber = new PhoneNumber("+1233123123"),
            BirthDate = new DateTime(2000,2,3), 
            Address =  new Address("Srbija", "Novi Sad", "Sase Krstica", "4"), 
            BloodType = BloodType.A_NEGATIVE,
            ChoosenDoctor = doctor,
            Allergens = allergens,
        };

        Hospitalization hospitalization = new Hospitalization(10, 2, 2, DateTime.Now, HospitalizationState.ACTIVE);

        User user2 = new User()
        {
            Username = "Mika1",
            Password = new Password("plsradi123"),
            Role = Role.Doctor,
            Id = 2
        };
        
        Doctor doctor2 = new Doctor()
        {
            Id = 5,
            Name = "Prvi plus",
            Surname = "Drugi plus",
            Address = new Address("Srbija", "Novi Sad", "Sase Krstica", "4"),
            BirthDate = DateTime.Now,
            Email = "nekimail1@gmail.com",
            PhoneNumber = new PhoneNumber("+123123123"),
            SpecialityId = 1,
            Uid = "67867867",
            WorkingHours = new List<WorkingHours>()
        };

        MedicalRecord record = new MedicalRecord()
        {
            Id = 2,
            PatientId = 2
        };

        Medicine medicine = new Medicine(1, "MedicineOne", 12.0);
        Medicine medicine2 = new Medicine()
        {
            Id=2, 
            Name ="MedicineOne" ,
            Quantity = 12.0,
            Allergens = allergens,
        };
        User user3 = new User("Menjdjer", "nekakulsifra12", Role.Doctor,4,ActiveStatus.Active);
        Allergen allergen2 = new Allergen(2,"Cetirizine");
        Allergen allergen3 = new Allergen(3,"Budesonide");

        Therapy therapyBlo = new BloodTherapy(10, DateTime.Now, BloodType.A_NEGATIVE, 10, 4);
        Therapy therapyMed = new MedicineTherapy(10, DateTime.Now, 1, 10, 4);

        BloodTherapy bloodTherapy1 = new BloodTherapy(10, DateTime.Now, BloodType.A_NEGATIVE, 2.0, 4);
        BloodTherapy bloodTherapy2 = new BloodTherapy(10, DateTime.Now, BloodType.A_NEGATIVE, 3.0, 4);

        User user4 = new User("PacijentIpo", "nekakulsifra12", Role.Patient, 6, ActiveStatus.Pending);
        user4.ActivationCode = "asdasd";

        Patient patient4 = new Patient()
        {
            Id = 6,
            Name = "Marko",
            Surname = "Markovic",
            Email = "asdasd65@gmail.com",
            Uid = "78787899",
            PhoneNumber = new PhoneNumber("+123123123"),
            BirthDate = new DateTime(2000,2,3), 
            Address =  new Address("Srbija", "Novi Sad", "Sase Krstica", "4"),
            BloodType = BloodType.A_NEGATIVE,
            ChoosenDoctor = doctor
        };
        
        AnnualLeave annualLeave1 = new AnnualLeave(15, 4, null, "Some reason",
            new DateTime(2022, 02, 11, 00, 00, 00), new DateTime(2022, 03, 11, 00, 00, 00), AnnualLeaveState.PENDING,
            false);
        
        AnnualLeave annualLeave2 = new AnnualLeave(16, 4, null, "Razlog",
            new DateTime(2022, 05, 23, 00, 00, 00), new DateTime(2022, 09, 11, 00, 00, 00), AnnualLeaveState.PENDING,
            false);
        
        
        Symptom cough = new Symptom()
        {
            Id = 10,
            Name = "Cough"
        };
        Symptom blood = new Symptom()
        {
            Id = 11,
            Name = "Blood"
        };
        WorkingHours doctor1Wh1 = new WorkingHours()
        {
            DoctorId = 5,
            Day = 0,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor1Wh2 = new WorkingHours()
        {
            DoctorId = 5,
            Day = 1,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor1Wh3 = new WorkingHours()
        {
            DoctorId = 5,
            Day = 2,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor1Wh4 = new WorkingHours()
        {
            DoctorId = 5,
            Day = 3,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor1Wh5 = new WorkingHours()
        {
            DoctorId = 5,
            Day = 4,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor1Wh6 = new WorkingHours()
        {
            DoctorId = 5,
            Day = 5,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor1Wh7 = new WorkingHours()
        {
            DoctorId = 5,
            Day = 6,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor2Wh1 = new WorkingHours()
        {
            DoctorId = 4,
            Day = 0,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor2Wh2 = new WorkingHours()
        {
            DoctorId = 4,
            Day = 1,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor2Wh3 = new WorkingHours()
        {
            DoctorId = 4,
            Day = 2,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor2Wh4 = new WorkingHours()
        {
            DoctorId = 4,
            Day = 3,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor2Wh5 = new WorkingHours()
        {
            DoctorId = 4,
            Day = 4,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor2Wh6 = new WorkingHours()
        {
            DoctorId = 4,
            Day = 5,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor2Wh7 = new WorkingHours()
        {
            DoctorId = 4,
            Day = 6,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        var today = new DateTime();
        Appointment examination = new Appointment()
        {
            Id = 30,
            DoctorId = 4,
            PatientId = 1,
            RoomId = 2,
            State = AppointmentState.PENDING,
            Type = AppointmentType.EXAMINATION,
            StartAt = today,
            EndAt = today.AddDays(1)
        };
        
        Appointment examination76 = new Appointment()
        {
            Id = 89,
            DoctorId = 4,
            PatientId = 1,
            RoomId = 2,
            State = AppointmentState.PENDING,
            Type = AppointmentType.EXAMINATION,
            StartAt = DateTime.Now,
            EndAt = DateTime.Now.AddHours(1)
        };
        
        Appointment examination105 = new Appointment()
        {
            Id = 106,
            DoctorId = 4,
            PatientId = 6,
            RoomId = 2,
            State = AppointmentState.PENDING,
            Type = AppointmentType.EXAMINATION,
            StartAt = DateTime.Now.AddDays(1),
            EndAt = DateTime.Now.AddDays(1).AddHours(1)
        };

        Appointment examination1 = new Appointment()
        {
            Id = 500,
            DoctorId = 4,
            PatientId = 1,
            RoomId = 2,
            State = AppointmentState.PENDING,
            Type = AppointmentType.EXAMINATION,
            StartAt = today,
            EndAt = today.AddDays(1)
        };

        Appointment examinationDontTouch = new Appointment()
        {
            Id = 41,
            DoctorId = 4,
            PatientId = 1,
            RoomId = 2,
            State = AppointmentState.PENDING,
            Type = AppointmentType.EXAMINATION,
            StartAt = today,
            EndAt = today.AddDays(1)
        };

        
        DateTime timeBegin = today.AddDays(2);
        DateTime timeEnd = today.AddDays(2);
        TimeSpan timeSpanBegin = new TimeSpan(11, 35, 0);
        timeBegin = timeBegin.Date + timeSpanBegin;
        TimeSpan timeSpanEnd = new TimeSpan(12, 25, 0);
        timeEnd = timeEnd.Date + timeSpanEnd;
        
        Appointment appointment1 = new Appointment()
        {
            Id = 31,
            DoctorId = 4,
            PatientId = 1,
            RoomId = 2,
            State = AppointmentState.PENDING,
            Type = AppointmentType.EXAMINATION,
            StartAt = timeBegin,
            EndAt = timeEnd
        };
        timeBegin = today.AddDays(2);
        timeEnd = today.AddDays(2);
        timeSpanBegin = new TimeSpan(11, 35, 0);
        timeBegin = timeBegin.Date + timeSpanBegin;
        timeSpanEnd = new TimeSpan(12, 25, 0);
        timeEnd = timeEnd.Date + timeSpanEnd;
        Appointment appointment2 = new Appointment()
        {
            Id = 32,
            DoctorId = 5,
            PatientId = 1,
            RoomId = 2,
            State = AppointmentState.PENDING,
            Type = AppointmentType.EXAMINATION,
            StartAt = timeBegin,
            EndAt = timeEnd
        };


        ExaminationReport rp = new ExaminationReport()
        {
            Id = 10,
            Content = "Something test",
            Prescriptions = null,
            Symptoms = null,
            DoctorId = 4,
            ExaminationId = 41
        };
        
        dbContext.Specialities.Add(speciality1);
        dbContext.Specialities.Add(speciality2);
        dbContext.Buildings.Add(building);
        dbContext.MapBuildings.Add(mapBuilding);
        dbContext.Floors.Add(floor);
        dbContext.MapFloors.Add(mapFloor);
        dbContext.WorkingHours.Add(doctor1Wh1);
        dbContext.WorkingHours.Add(doctor1Wh2);
        dbContext.WorkingHours.Add(doctor1Wh3);
        dbContext.WorkingHours.Add(doctor1Wh4);
        dbContext.WorkingHours.Add(doctor1Wh5);
        dbContext.WorkingHours.Add(doctor1Wh6);
        dbContext.WorkingHours.Add(doctor1Wh7);
        dbContext.WorkingHours.Add(doctor2Wh1);
        dbContext.WorkingHours.Add(doctor2Wh2);
        dbContext.WorkingHours.Add(doctor2Wh3);
        dbContext.WorkingHours.Add(doctor2Wh4);
        dbContext.WorkingHours.Add(doctor2Wh5);
        dbContext.WorkingHours.Add(doctor2Wh6);
        dbContext.WorkingHours.Add(doctor2Wh7);
        dbContext.Rooms.Add(room1);
        dbContext.Rooms.Add(room2);
        dbContext.Doctors.Add(doctor);
        dbContext.Doctors.Add(doctor2);
        dbContext.AnnualLeaves.Add(annualLeave1);
        dbContext.AnnualLeaves.Add(annualLeave2);
        dbContext.Hospitalizations.Add(hospitalization);
        dbContext.MapRooms.Add(mapRoom);
        dbContext.RoomEquipment.Add(equipment);
        dbContext.RoomEquipment.Add(equipment2);
        dbContext.Patients.Add(patient);
        dbContext.Patients.Add(patient2);
        dbContext.Patients.Add(patient4);
        dbContext.MedicalRecords.Add(record);
        dbContext.BloodStorage.Add(bloodStorage);
        dbContext.Users.Add(user);
        dbContext.Users.Add(user2);
        dbContext.Users.Add(user3);
        dbContext.Users.Add(user4);
        dbContext.Allergens.Add(allergen1);
        dbContext.Allergens.Add(allergen2);
        dbContext.Allergens.Add(allergen3);
        dbContext.Medicines.Add(medicine);
        dbContext.Medicines.Add(medicine2);
        dbContext.Therapies.Add(therapyBlo);
        dbContext.Therapies.Add(therapyMed);
        dbContext.BloodStorage.Add(bloodStorage);
        dbContext.Therapies.Add(bloodTherapy1);
        dbContext.Therapies.Add(bloodTherapy2);
        dbContext.AnnualLeaves.Add(annualLeave1);
        dbContext.AnnualLeaves.Add(annualLeave2);
        dbContext.Symptoms.Add(cough);
        dbContext.Symptoms.Add(blood);
        dbContext.Appointments.Add(examination);
        dbContext.Appointments.Add(appointment1);
        dbContext.Appointments.Add(appointment2);
        dbContext.Appointments.Add(examination1);
        dbContext.Appointments.Add(examination76);
        dbContext.Appointments.Add(examination105);
        dbContext.Appointments.Add(examinationDontTouch);
        dbContext.ExaminationReports.Add(rp);
        dbContext.SaveChanges();


    }
}