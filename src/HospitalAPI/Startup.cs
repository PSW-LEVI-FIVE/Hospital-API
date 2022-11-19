using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Interfaces;
using System.Text.Json.Serialization;
using HospitalAPI.Emails;
using HospitalAPI.ErrorHandling;
using Microsoft.Extensions.Hosting;
using HospitalAPI.Storage;
using HospitalAPI.Storage.Providers;
using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.BloodOrders;
using HospitalLibrary.BloodOrders.Interfaces;
using HospitalLibrary.Allergens;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.BloodStorages.Interfaces;
using HospitalLibrary.Auth;
using HospitalLibrary.Auth.Interfaces;
using HospitalLibrary.Buildings;
using HospitalLibrary.Buildings.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Feedbacks;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Floors;
using HospitalLibrary.Floors.Interfaces;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.Map;
using HospitalLibrary.Map.Interfaces;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.MedicalRecords.Interfaces;
using HospitalLibrary.Medicines;
using HospitalLibrary.Medicines.Interfaces;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.PDFGeneration;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;
using HospitalLibrary.Shared.Validators;
using HospitalLibrary.Therapies;
using HospitalLibrary.Therapies.Interfaces;
using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace HospitalAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.MaxDepth = 0;
                });
            services.AddDbContext<HospitalDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("HospitalDb")));

            services.AddControllers();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IEmailService, SendgridProvider>();
            services.AddScoped<IMapService, MapService>();
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IFloorService, FloorService>();
            services.AddScoped<ITimeIntervalValidationService, TimeIntervalValidationService>();
            services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            services.AddScoped<IHospitalizationService, HospitalizationService>();
            services.AddScoped<IHospitalizationValidator, HospitalizationValidator>();
            services.AddScoped<IAnnualLeaveService,AnnualLeaveService>();
            services.AddScoped<IAnnualLeaveValidator, AnnualLeaveValidator>();
            services.AddScoped<IAppointmentRescheduler, AppointmentRescheduler>();
            services.AddScoped<IBloodOrderService,BloodOrderService>();
            services.AddScoped<IAllergenService, AllergenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITherapyService, TherapyService>();
            services.AddScoped<IBloodStorageService, BloodStorageService>();
            services.AddScoped<IMedicineService, MedicineService>();
            services.AddScoped<IRegistrationValidationService, RegistrationValidationService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IStorage, SupabaseStorage>();
            services.AddScoped<IPDFGenerator, PdfGenerator>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GraphicalEditor", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HospitalAPI v1"));
            }
            

            app.UseRouting();

            app.UseAuthorization();

            app.ConfigureGlobalErrorHandling();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
