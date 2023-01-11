using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Invitations.Dtos;
using HospitalLibrary.Invitations.Interfaces;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Invitations
{
    public class InvitationService : IInvitationService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDoctorService _doctorService;
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

        public  async Task<IEnumerable<Invitation>> CreateEventForAll(Invitation invitation)
        {
            IEnumerable<Doctor> doctors = await  _doctorService.GetAll();
            List<Invitation> invitations = new List<Invitation>();
            foreach(Doctor doctor in doctors)
            {
                invitation.DoctorId = doctor.Id;
                invitations.Add(invitation);
                _unitOfWork.InvitationRepository.Add(invitation);
                _unitOfWork.InvitationRepository.Save();
                

            }

            return invitations;

        }

        public async Task<IEnumerable<Invitation>> CreateEventForSpeciality(Invitation invitation,int specialityId)
        {
            IEnumerable<Doctor> doctors = await _doctorService.GetAllDoctorsBySpecialization(specialityId);
            List<Invitation> invitations = new List<Invitation>();
            foreach(Doctor doctor in doctors)
            {
                invitation.DoctorId = doctor.Id;
                invitations.Add(invitation);
                _unitOfWork.InvitationRepository.Add(invitation);
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