using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Interfaces;
using System.Text.Json.Serialization;
using HospitalAPI.Emails;
using HospitalAPI.ErrorHandling;
using Microsoft.Extensions.Hosting;
using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Allergens;
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
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;
using HospitalLibrary.Shared.Validators;
using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                   options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve
                );
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
            services.AddScoped<IAllergenService, AllergenService>();
            services.AddScoped<IUserService, UserService>();
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
