using HospitalAPI;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Buildings;
using HospitalLibrary.Floors;
using HospitalLibrary.Hospitalizations;
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


        Doctor doctor = new Doctor()
        {
            Id = 4,
            Name = "Prvi",
            Surname = "Drugi",
            Address = "Al bas daleko odavde",
            BirthDate = DateTime.Now,
            Email = "nekimail@gmail.com",
            PhoneNumber = "063555333",
            SpecialtyType = SpecialtyType.SURGERY,
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
        
        Hospitalization hospitalization = new Hospitalization()
        {   
            Id = 10,
            BedId = 2,
            State = HospitalizationState.FINISHED,
            StartTime = DateTime.Now,
            PdfUrl = "",
            MedicalRecordId = 2,
        };
        
        User user2 = new User()
        {
            Username = "Mika1",
            Password = "plsradi",
            Role = Role.Patient,
            Id = 2
        };
        Doctor doctor2 = new Doctor()
        {
            Id = 5,
            Name = "Prvi plus",
            Surname = "Drugi plus",
            Address = "Al bas daleko odavde",
            BirthDate = DateTime.Now,
            Email = "nekimail1@gmail.com",
            PhoneNumber = "063555333",
            SpecialtyType = SpecialtyType.ITERNAL_MEDICINE,
            Uid = "67867867",
            WorkingHours = new List<WorkingHours>()
        };
        
        MedicalRecord record = new MedicalRecord()
        {
            Id = 2,
            PatientId = 2
        };
        
        Medicine medicine = new Medicine(1, "MedicineOne", 12.0);
        User user3 = new User(4, "Menjdjer", "nekakulsifra", Role.Doctor);
        Allergen allergen1 = new Allergen(1,"Milk");
        Allergen allergen2 = new Allergen(2,"Cetirizine");
        Allergen allergen3 = new Allergen(3,"Budesonide");

        Therapy therapyBlo = new BloodTherapy(10,  DateTime.Now, BloodType.A_NEGATIVE, 10, 4);
        Therapy therapyMed = new MedicineTherapy(10, DateTime.Now, 1, 10, 4);
        
        BloodTherapy bloodTherapy1 = new BloodTherapy(10, DateTime.Now, BloodType.A_NEGATIVE, 2.0, 4);
        BloodTherapy bloodTherapy2 = new BloodTherapy(10, DateTime.Now, BloodType.A_NEGATIVE, 3.0, 4);
        
        dbContext.Buildings.Add(building);
        dbContext.Floors.Add(floor);
        dbContext.Rooms.Add(room);
        dbContext.Doctors.Add(doctor);
        dbContext.Doctors.Add(doctor2);
        dbContext.Hospitalizations.Add(hospitalization);
        dbContext.RoomEquipment.Add(equipment);
        dbContext.RoomEquipment.Add(equipment2);
        dbContext.Patients.Add(patient);
        dbContext.Patients.Add(patient2);
        dbContext.MedicalRecords.Add(record);
        dbContext.BloodStorage.Add(bloodStorage);
        dbContext.Users.Add(user);
        dbContext.Users.Add(user2);
        dbContext.Users.Add(user3);
        dbContext.Allergens.Add(allergen1);
        dbContext.Allergens.Add(allergen2);
        dbContext.Allergens.Add(allergen3);
        dbContext.Medicines.Add(medicine);
        dbContext.Therapies.Add(therapyBlo);
        dbContext.Therapies.Add(therapyMed);
        dbContext.BloodStorage.Add(bloodStorage);
        dbContext.Therapies.Add(bloodTherapy1);
        dbContext.Therapies.Add(bloodTherapy2);
        
        dbContext.SaveChanges();

    }
}