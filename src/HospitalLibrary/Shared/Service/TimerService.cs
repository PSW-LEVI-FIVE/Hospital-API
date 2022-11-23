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
    internal class TimerService : BackgroundService
    {
        System.Timers.Timer reallocationTimer = new System.Timers.Timer();
        public TimerService()
        {
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            reallocationTimer.Elapsed += new ElapsedEventHandler(GenerateMessage);
            reallocationTimer.Interval = 300000; //number in miliseconds  
            reallocationTimer.Enabled = true;

            return Task.CompletedTask;
        }
        private void GenerateMessage(object source, ElapsedEventArgs e)
        {
            //WriteToFile("Generate message at " + DateTime.Now);
            //Program.Messages.Add(new Message("generated text: " + System.Guid.NewGuid().ToString(), DateTime.Now));
        }

    }
}
