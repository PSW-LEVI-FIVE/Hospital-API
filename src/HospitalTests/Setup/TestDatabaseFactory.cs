using HospitalAPI;
using HospitalLibrary.Buildings;
using HospitalLibrary.Patients;
using HospitalLibrary.Rooms;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;
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
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        Building building = new Building()
        {
            Id = 1,
            Address = "NEKA ADRESA",
            Name = "Neko ime"
        };
        dbContext.Buildings.Add(building);

        dbContext.SaveChanges();

    }
}