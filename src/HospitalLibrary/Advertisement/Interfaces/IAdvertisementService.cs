using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Advertisement.Interfaces
{
    public interface IAdvertisementService
    {
        public Advertisement Create(Advertisement advertisement);
        public Task<IEnumerable<Advertisement>> GetAll();
    }
}