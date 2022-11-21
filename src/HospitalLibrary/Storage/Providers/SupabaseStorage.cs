using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Supabase.Interfaces;
using Supabase.Storage;

namespace HospitalAPI.Storage.Providers
{
    public class SupabaseStorage: IStorage
    {
        private Supabase.Client _client;

        public SupabaseStorage(IConfiguration configuration)
        {
            string apiKey = configuration["Supabase:Secret"];
            string url = configuration["Supabase:Url"];
            _client = new Supabase.Client(url, apiKey);
        }

        public async Task<string> UploadFile(byte[] file, string filename)
        {
            string fileAlias = filename + ".pdf";
            var bucket = _client.Storage.From("pdf-bucket");
            await bucket.Upload(file, fileAlias);
            string url =  bucket.GetPublicUrl(fileAlias);

            return url;
        }
    }
}