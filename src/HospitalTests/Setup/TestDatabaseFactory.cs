using HospitalAPI;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Buildings;
using HospitalLibrary.Floors;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.Patients;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;
using HospitalLibrary.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace HospitalTests.Setup;

public class TestDatabaseFactory<TStartup>: WebApplicationFactory<Startup>
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
        //dbContext.Database.EnsureDeleted();
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"BloodStorage\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Therapies\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Hospitalizations\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"AllergenMedicine\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Allergens\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Medicines\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"AnnualLeaves\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Appointments\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Beds\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Feedbacks\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"MedicalRecords\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"RoomEquipment\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"MapRooms\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Rooms\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"MapFloors\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Floors\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"MapBuildings\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Buildings\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"WorkingHours\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Users\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Doctors\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Patients\"");
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Persons\"");
        dbContext.Database.EnsureCreated();

        Building building = new Building()
        {
            Id = 1,
            Address = "NEKA ADRESA",
            Name = "Neko ime"
        };
        
        Floor floor = new Floor()
        {
            Id = 1,
            Area = 100,
            BuildingId = 1,
        };
        Room room = new Room()
        {
            Id = 1,
            Area = 10,
            FloorId = 1,
            RoomNumber= "1"
        };
        RoomEquipment equipment = new Bed(1, 10, "Bed", 1, 1);
        RoomEquipment equipment2 = new Bed(2, 10, "Bed", 1, 1);
        
        Patient patient = new Patient()
        {
            Id=1,
            Name = "Marko",
            Surname = "Markovic",
            Email = "asdasd1@gmail.coma",
            Uid = "67676767",
            PhoneNumber = "123123123",
            BirthDate = new DateTime(2000,2,2), 
            Address = "Mike", 
            BloodType = BloodType.A_NEGATIVE
        };
        
        User user = new User()
        {
            Username = "Mika",
            Password = "plsradi",
            Role = Role.Patient,
            Id = 1
        };
        
        Patient patient2 = new Patient()
        {
            Id=2,
            Name = "Marko",
            Surname = "Markovic",
            Email = "asdasd2@gmail.com",
            Uid = "78787878",
            PhoneNumber = "123123123",
            BirthDate = new DateTime(2000,2,3), 
            Address = "Zike", 
            BloodType = BloodType.A_NEGATIVE
        };
        User user2 = new User()
        {
            Username = "Mika1",
            Password = "plsradi",
            Role = Role.Patient,
            Id = 2
        };
        
        MedicalRecord record = new MedicalRecord()
        {
            Id = 2,
            PatientId = 2
        };
        
        Hospitalization hospitalization = new Hospitalization()
        {   
            Id = 10,
            BedId = 2,
            State = HospitalizationState.ACTIVE,
            StartTime = DateTime.Now,
            MedicalRecordId = 2,
        };
        
        dbContext.Buildings.Add(building);
        
        dbContext.Floors.Add(floor);
        dbContext.Rooms.Add(room);
        dbContext.RoomEquipment.Add(equipment);
        dbContext.RoomEquipment.Add(equipment2);
        dbContext.Patients.Add(patient);
        dbContext.Patients.Add(patient2);
        dbContext.Users.Add(user);
        dbContext.Users.Add(user2);
        dbContext.MedicalRecords.Add(record);
        dbContext.Hospitalizations.Add(hospitalization);
        dbContext.SaveChanges();

    }
}