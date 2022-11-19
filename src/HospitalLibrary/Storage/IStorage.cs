using System.IO;
using System.Threading.Tasks;

namespace HospitalAPI.Storage
{
    public interface IStorage
    {
        Task<string> UploadFile(byte[] file, string filename);
    }
}