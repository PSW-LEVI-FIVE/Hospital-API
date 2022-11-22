using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Allergens
{
    public class AllergenService:IAllergenService
    {
        private IUnitOfWork _unitOfWork;

        public AllergenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<Allergen>> GetAll()
        {
            return _unitOfWork.AllergenRepository.GetAll();
        }
        public Allergen Create(Allergen allergen)
        {
            _unitOfWork.AllergenRepository.Add(allergen);
            _unitOfWork.AllergenRepository.Save();
            return allergen;
        }

        public Task<IEnumerable<Allergen>> GetAllergensWithNumberOfPatients()
        {
            return _unitOfWork.AllergenRepository.GetAllergensWithNumberOfPatients();
        }
    }
}