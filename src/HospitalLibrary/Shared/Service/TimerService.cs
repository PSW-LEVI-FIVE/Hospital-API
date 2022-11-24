using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace HospitalLibrary.Shared.Service
{
    public class TimerService : BackgroundService
    {
        //private readonly EquipmentReallocationService _equipmentReallocationService;
        private readonly IServiceProvider _serviceProvider;

        System.Timers.Timer reallocationTimer = new System.Timers.Timer();
       /* public TimerService()
        {
            
        }*/

        public TimerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            reallocationTimer.Elapsed += new ElapsedEventHandler(GenerateMessage);
            reallocationTimer.Interval = 30000; //number in miliseconds  
            reallocationTimer.Enabled = true;

            return Task.CompletedTask;
        }
        private async void GenerateMessage(object source, ElapsedEventArgs e)
        {
            using (IServiceScope scope=_serviceProvider.CreateScope())
            {
                IEquipmentReallocationService equipmentReallocationService = scope.ServiceProvider.GetService<IEquipmentReallocationService>();
                List<EquipmentReallocation> realocations = await equipmentReallocationService.getAllPendingForToday();
                foreach (EquipmentReallocation real in realocations) 
                {
                     if (real.EndAt < DateTime.Now) 
                     {
                         equipmentReallocationService.initiate(real);           
                     }
                }
            }

        }

    }
}
