using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Invitations.Dtos;
using HospitalLibrary.Invitations.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Invitations
{
    public class InvitationService : IInvitationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoctorService _doctorService;

        public InvitationService(IUnitOfWork unitOfWork, IDoctorService doctorService)
        {
            _unitOfWork = unitOfWork;
            _doctorService = doctorService;
        }

        public IEnumerable<Invitation> GetAllInvitations()
        {
            return _unitOfWork.InvitationRepository.GetAllInvitations();
        }

        public IEnumerable<Invitation> GetAllByDoctorId(int doctorId)
        {
            return _unitOfWork.InvitationRepository.GetAllByDoctorId(doctorId);
        }

        public Invitation AcceptInvitation(int invitationId)
        {
            Invitation invitation=_unitOfWork.InvitationRepository.GetOne(invitationId);
            invitation.InvitationStatus = InvitationStatus.ACCEPTED;
            _unitOfWork.InvitationRepository.Update(invitation);
            return invitation;
        }

        public  async Task<IEnumerable<Invitation>> CreateEventForAll(CreateInvitationDto createInvitationDto)
        {
            IEnumerable<Doctor> doctors = await  _doctorService.GetAll();
            List<Invitation> invitations = new List<Invitation>();
            foreach(Doctor doctor in doctors)
            {
                Invitation newInvitation = new Invitation(doctor.Id, createInvitationDto.Description,
                    createInvitationDto.Title,
                    "", createInvitationDto.Place, createInvitationDto.StartAt, createInvitationDto.EndAt,
                    createInvitationDto.InvitationStatus);
                newInvitation.DoctorId = doctor.Id;
                invitations.Add(newInvitation);
                _unitOfWork.InvitationRepository.Add(newInvitation);
               _unitOfWork.InvitationRepository.Save();
                

            }

            return invitations;

        }

        public async Task<IEnumerable<Invitation>> CreateEventForSpeciality(CreateInvitationDto createInvitationDto,int specialityId)
        {
            IEnumerable<Doctor> doctors = await _doctorService.GetAllDoctorsBySpecialization(specialityId);
            List<Invitation> invitations = new List<Invitation>();
            foreach(Doctor doctor in doctors)
            {
                Invitation newInvitation = new Invitation(doctor.Id, createInvitationDto.Description,
                    createInvitationDto.Title,
                    "", createInvitationDto.Place, createInvitationDto.StartAt, createInvitationDto.EndAt,
                    createInvitationDto.InvitationStatus);
                newInvitation.DoctorId = doctor.Id;
                invitations.Add(newInvitation);
                _unitOfWork.InvitationRepository.Add(newInvitation);
                _unitOfWork.InvitationRepository.Save();
                

            }

            return invitations;
        }

        public Invitation DeclineInvitation()
        {
            throw new System.NotImplementedException();
        }
    }
}