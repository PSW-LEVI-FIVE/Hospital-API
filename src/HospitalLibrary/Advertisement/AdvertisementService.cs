using HospitalLibrary.Advertisement.Interfaces;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Advertisement
{
    public class AdvertisementService : IAdvertisementService
    {
        private IUnitOfWork _unitOfWork;

        public AdvertisementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Advertisement Create(Advertisement advertisement)
        {
            _unitOfWork.AdvertisementRepository.Add(advertisement);
            _unitOfWork.AdvertisementRepository.Save();
            return advertisement;
        }
        
    }
}