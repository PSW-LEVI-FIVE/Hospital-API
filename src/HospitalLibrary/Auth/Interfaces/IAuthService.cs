<<<<<<< HEAD
﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Allergens.Dtos;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Users.Dtos;

namespace HospitalLibrary.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<PatientDTO> RegisterPatient(CreatePatientDTO createPatientDto);
        Task<Users.User> RegisterPatient(Users.User user);
        Users.User UserExist(string username, string password);
        string Generate(UserDTO user);
        UserDTO Authenticate(UserDTO userDto);
    }
}