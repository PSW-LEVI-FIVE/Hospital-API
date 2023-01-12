using System;
using HospitalLibrary.Infrastructure.EventSourcing.Events;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.SchedulingAppointments.Dtos;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.SchedulingAppointments
{
    public class SchedulingAppointmentStatistics : ISchedulingAppointmentStatistics
    {
        private IUnitOfWork _unitOfWork;
        
        public SchedulingAppointmentStatistics(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public TimesWatchedStepsDTO CalculateStepsAverageTime()
        {
            var dateAverage = _unitOfWork.SchedulingEvenetRepository
                .GetAverageTimeForStep(SchedulingAppointmentEventType.STARTED, SchedulingAppointmentEventType.PICKED_DATE);
            var specialityAverage = _unitOfWork.SchedulingEvenetRepository
                .GetAverageTimeForStep(SchedulingAppointmentEventType.PICKED_DATE, SchedulingAppointmentEventType.PICKED_SPECIALITY);
            var doctorAverage = _unitOfWork.SchedulingEvenetRepository
                .GetAverageTimeForStep(SchedulingAppointmentEventType.PICKED_SPECIALITY, SchedulingAppointmentEventType.PICKED_DOCTOR);
            var timeAverage = _unitOfWork.SchedulingEvenetRepository
                .GetAverageTimeForStep(SchedulingAppointmentEventType.PICKED_DOCTOR, SchedulingAppointmentEventType.PICKED_TIME);
            var scheduleAverage = _unitOfWork.SchedulingEvenetRepository
                .GetAverageTimeForStep(SchedulingAppointmentEventType.PICKED_TIME, SchedulingAppointmentEventType.FINISHED);
            return new TimesWatchedStepsDTO(dateAverage, specialityAverage,doctorAverage ,timeAverage,scheduleAverage);
        }

        public TimesWatchedStepsDTO GetTimesWatchedStep()
        {
            var dateStep = _unitOfWork.SchedulingEvenetRepository
                .GetTimesWatchedStep(SchedulingAppointmentEventType.PICKED_DATE);
            var specialityStep = _unitOfWork.SchedulingEvenetRepository
                .GetTimesWatchedStep(SchedulingAppointmentEventType.PICKED_SPECIALITY);
            var doctorStep = _unitOfWork.SchedulingEvenetRepository
                .GetTimesWatchedStep(SchedulingAppointmentEventType.PICKED_DOCTOR);
            var timeStep = _unitOfWork.SchedulingEvenetRepository
                .GetTimesWatchedStep(SchedulingAppointmentEventType.PICKED_TIME);
            var finishedStep = _unitOfWork.SchedulingEvenetRepository
                .GetTimesWatchedStep(SchedulingAppointmentEventType.FINISHED);
            return new TimesWatchedStepsDTO(dateStep, specialityStep,doctorStep ,timeStep,finishedStep);
        }

        public SchedulePerAgeDTO GetAverageTimeForSchedulePerAge(int fromAge, int toAge)
        {
            DateTime dateToAge = new DateTime(DateTime.Now.Year - fromAge, DateTime.Now.Month, DateTime.Now.Day);
            DateTime dateFromAge = new DateTime(DateTime.Now.Year - toAge, DateTime.Now.Month, DateTime.Now.Day);
            var dateStep = _unitOfWork.SchedulingEvenetRepository
                .GetAverageTimeForSchedulePerAge(dateFromAge,dateToAge);
            return new SchedulePerAgeDTO(dateStep);

        }
        public SchedulePerAgeDTO GetAverageTimeForSchedule()
        {
            var dateStep = _unitOfWork.SchedulingEvenetRepository
                .GetAverageTimeForSchedule();
            return new SchedulePerAgeDTO(dateStep);

        }
        public TimesWatchedStepsDTO GetHowManyTimesQuitOnStep()
        {
            var dateAverage = _unitOfWork.SchedulingEvenetRepository
                .GetHowManyTimesQuitOnStep(SchedulingAppointmentEventType.STARTED, SchedulingAppointmentEventType.PICKED_DATE);
            var specialityAverage = _unitOfWork.SchedulingEvenetRepository
                .GetHowManyTimesQuitOnStep(SchedulingAppointmentEventType.PICKED_DATE, SchedulingAppointmentEventType.PICKED_SPECIALITY);
            var doctorAverage = _unitOfWork.SchedulingEvenetRepository
                .GetHowManyTimesQuitOnStep(SchedulingAppointmentEventType.PICKED_SPECIALITY, SchedulingAppointmentEventType.PICKED_DOCTOR);
            var timeAverage = _unitOfWork.SchedulingEvenetRepository
                .GetHowManyTimesQuitOnStep(SchedulingAppointmentEventType.PICKED_DOCTOR, SchedulingAppointmentEventType.PICKED_TIME);
            var scheduleAverage = _unitOfWork.SchedulingEvenetRepository
                .GetHowManyTimesQuitOnStep(SchedulingAppointmentEventType.PICKED_TIME, SchedulingAppointmentEventType.FINISHED);
            return new TimesWatchedStepsDTO(dateAverage, specialityAverage,doctorAverage ,timeAverage,scheduleAverage);
        }
        public TimesWatchedStepsDTO GetLongTermedSteps()
        {
            var dateAverage = _unitOfWork.SchedulingEvenetRepository
                .GetLongTermedSteps(SchedulingAppointmentEventType.STARTED, SchedulingAppointmentEventType.PICKED_DATE);
            var specialityAverage = _unitOfWork.SchedulingEvenetRepository
                .GetLongTermedSteps(SchedulingAppointmentEventType.PICKED_DATE, SchedulingAppointmentEventType.PICKED_SPECIALITY);
            var doctorAverage = _unitOfWork.SchedulingEvenetRepository
                .GetLongTermedSteps(SchedulingAppointmentEventType.PICKED_SPECIALITY, SchedulingAppointmentEventType.PICKED_DOCTOR);
            var timeAverage = _unitOfWork.SchedulingEvenetRepository
                .GetLongTermedSteps(SchedulingAppointmentEventType.PICKED_DOCTOR, SchedulingAppointmentEventType.PICKED_TIME);
            var scheduleAverage = _unitOfWork.SchedulingEvenetRepository
                .GetLongTermedSteps(SchedulingAppointmentEventType.PICKED_TIME, SchedulingAppointmentEventType.FINISHED);
            return new TimesWatchedStepsDTO(dateAverage, specialityAverage,doctorAverage ,timeAverage,scheduleAverage);
        }


    }
}