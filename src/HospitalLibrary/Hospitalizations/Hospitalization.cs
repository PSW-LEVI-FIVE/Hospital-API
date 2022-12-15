using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Model;
using HospitalLibrary.Therapies;
using HospitalLibrary.Therapies.Model;
using OpenQA.Selenium;
using NotFoundException = HospitalLibrary.Shared.Exceptions.NotFoundException;

namespace HospitalLibrary.Hospitalizations
{
    
    public enum HospitalizationState { ACTIVE, FINISHED }
    public class Hospitalization: BaseEntity
    {
        public int BedId { get; private set; }
        [ForeignKey("BedId")]
        public virtual Bed Bed  { get; private set; }

        public int MedicalRecordId { get; private set; }
        [ForeignKey("MedicalRecordId")]
        public virtual MedicalRecord MedicalRecord  { get; private set; }

        public HospitalizationState State { get; private set; }
        
        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }

        public string PdfUrl { get; private set; } = "";
        
        public virtual List<Therapy> Therapies { get; private set; }

        public Hospitalization(int bedId, int medicalRecordId, DateTime startTime, HospitalizationState state)
        {
            BedId = bedId;
            MedicalRecordId = medicalRecordId;
            StartTime = startTime;
            State = state;
        }
        
        public Hospitalization(int id, int bedId, int medicalRecordId, DateTime startTime, HospitalizationState state)
        {
            Id = id;
            BedId = bedId;
            MedicalRecordId = medicalRecordId;
            StartTime = startTime;
            State = state;
        }

        public Hospitalization(int id, int bedId, Bed bed, int medicalRecordId, MedicalRecord medicalRecord, HospitalizationState state, DateTime startTime, DateTime? endTime, string pdfUrl, List<Therapy> therapies)
        {
            Id = id;
            BedId = bedId;
            Bed = bed;
            MedicalRecordId = medicalRecordId;
            MedicalRecord = medicalRecord;
            State = state;
            StartTime = startTime;
            EndTime = endTime;
            PdfUrl = pdfUrl;
            Therapies = therapies;
        }


        public void Finish(DateTime time)
        {
            ThrowIfNotValidForFinish(time);
            State = HospitalizationState.FINISHED;
            EndTime = time;
        }

        public void UpdatePdfUrl(string url)
        {
            if (PdfUrl != null && UrlNotEmpty())
                throw new BadRequestException("Url already exists!");
            PdfUrl = url;
        }
        
        public void ValidateForPdfGeneration()
        {
            if (State != HospitalizationState.FINISHED) 
                throw new BadRequestException("Hospitalization should be finished!");
            if (UrlNotEmpty()) 
                throw new BadRequestException("Report already generated for given hospitalization!");
        }

        private void ThrowIfNotValidForFinish(DateTime time)
        {
            if (StartTime.CompareTo(time) >= 0)
                throw new BadRequestException("End Time should be after start time!");
            if (State == HospitalizationState.FINISHED)
                throw new BadRequestException("Hospitalization has already been finished!");
        }
        
        private bool UrlNotEmpty()
        {
            return !PdfUrl.Trim().Equals("");
        }

        public static void ThrowIfNull(Hospitalization hospitalization)
        {
            if (hospitalization == null)
                throw new NotFoundException("Hospitalization not found!");
        }
    }
}