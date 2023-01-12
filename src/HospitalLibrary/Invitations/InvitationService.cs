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
        private readonly ITimeIntervalValidationService _intervalValidation;

        public InvitationService(IUnitOfWork unitOfWork, IDoctorService doctorService,ITimeIntervalValidationService timeIntervalValidationService)
        {
            _unitOfWork = unitOfWork;
            _doctorService = doctorService;
            _intervalValidation = timeIntervalValidationService;
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
            _unitOfWork.InvitationRepository.Save();
            return invitation;
        }
        
        public Invitation DeclineInvitation(DeclineInvitationDto declineInvitationDto)
        {
            Invitation invitation =_unitOfWork.InvitationRepository.GetOne(declineInvitationDto.InvitationId);
            invitation.InvitationStatus = InvitationStatus.REJECTED;
            invitation.Reason = declineInvitationDto.Reason;
            _unitOfWork.InvitationRepository.Save();
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
                newInvitation.InvitationStatus = InvitationStatus.PENDING;
                invitations.Add(newInvitation);
                await _intervalValidation.ValidateTeamBuildingEventInvitation(newInvitation);
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
                newInvitation.InvitationStatus = InvitationStatus.PENDING;
                await _intervalValidation.ValidateTeamBuildingEventInvitation(newInvitation);
                invitations.Add(newInvitation);
                _unitOfWork.InvitationRepository.Add(newInvitation);
                _unitOfWork.InvitationRepository.Save();
                

            }

            return invitations;
        }


    }
}