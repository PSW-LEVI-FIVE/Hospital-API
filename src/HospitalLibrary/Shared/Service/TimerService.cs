using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using HospitalLibrary.Renovation.Interface;

namespace HospitalLibrary.Shared.Service
{
    public class TimerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        System.Timers.Timer reallocationTimer = new System.Timers.Timer();
      

        public TimerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            reallocationTimer.Elapsed += new ElapsedEventHandler(async(object source, ElapsedEventArgs e) =>
            {
                await GenerateMessage(source, e);
            });
            reallocationTimer.Interval = 30000; //number in miliseconds  
            reallocationTimer.Enabled = true;

            return Task.CompletedTask;
        }
        private async Task GenerateMessage(object source, ElapsedEventArgs e)
        {
            try
            {
                using IServiceScope scope=_serviceProvider.CreateScope();
                IEquipmentReallocationService equipmentReallocationService = scope.ServiceProvider.GetService<IEquipmentReallocationService>();
                List<EquipmentReallocation> realocations = await equipmentReallocationService.GetAllPendingForToday();
                foreach (EquipmentReallocation real in realocations) 
                {
                    if (real.EndAt < DateTime.Now) 
                    {
                        await equipmentReallocationService.InitiateReallocation(real);           
                    }
                }
                IRenovationService renovation = scope.ServiceProvider.GetService<IRenovationService>();

                List<Renovation.Model.Renovation> renovations = await renovation.GetAllPending();
                foreach (Renovation.Model.Renovation reno in renovations)
                {
                    if (reno.EndAt < DateTime.Now)
                    {
                        await renovation.initiateRenovation(reno);
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Something wrong with timer service");
            }
            
        }

    }
}
