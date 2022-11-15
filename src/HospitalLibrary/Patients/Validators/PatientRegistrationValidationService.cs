using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HospitalLibrary.Patients;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Shared.Validators
{
    public class PatientRegistrationValidationService: IPatientRegistrationValidationService
    {
        private IUnitOfWork _unitOfWork;
        
        public PatientRegistrationValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ValidatePatient(Patient patient)
        {
            if (await _unitOfWork.PatientRepository.GetOneByUid(patient.Uid) != null)
                throw new BadRequestException("Uid is already taken");
            if (await _unitOfWork.PatientRepository.GetOneByEmail(patient.Email) != null)
                throw new BadRequestException("Email is already taken");

            ThrowIfNameOrSurnameNotValid(patient.Name,"Name input not valid");
            ThrowIfNameOrSurnameNotValid(patient.Surname,"Surname input not valid");
            ThrowIfUidNotValid(patient.Uid);
            ThrowIfEmailNotValid(patient.Email);
            ThrowIfPhoneNumberNotValid(patient.PhoneNumber);
            ThrowIfAddressNotValid(patient.Address);
            ThrowIfBirthDateNotValid(patient.BirthDate);
        }
        private void ThrowIfNameOrSurnameNotValid(string patientInput,string throwMessage)
        {
            if (!new Regex( @"^[A-Z][a-z0-9_-]{3,19}$").IsMatch(patientInput))
                throw new BadRequestException(throwMessage);
        }
        private void ThrowIfUidNotValid(string uid)
        {
            if (!new Regex(@"^\d{8}$").IsMatch(uid))
                throw new BadRequestException("Uid input not valid");
        }
        private void ThrowIfEmailNotValid(string email)
        {
            if (!new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").IsMatch(email))
                throw new BadRequestException("Email input not valid");
        }
        private void ThrowIfPhoneNumberNotValid(string phoneNumber)
        {
            if (!new Regex(@"^[+]*[0-9-]+$").IsMatch(phoneNumber))
                throw new BadRequestException("Phone number input not valid");
        }
        private void ThrowIfAddressNotValid(string address)
        {
            if (!new Regex(@"^[A-Z][A-Za-z0-9( )]+$").IsMatch(address))
                throw new BadRequestException("Address input not valid");
        }
        
        private void ThrowIfBirthDateNotValid(DateTime birthDate)
        {
            if (birthDate >= DateTime.Now)
                throw new BadRequestException("Birth date cant be in the future");
        }
    }
}