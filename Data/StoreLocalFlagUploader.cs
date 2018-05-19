using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace GlobalCityManager.Data
{
    public class StoreLocalFlagUploader : IFlagUploader
    {
        private IHostingEnvironment environment;

        public StoreLocalFlagUploader(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        public string CreateFlag(string code, IFormFile nationalFlagFile)
        {
            string relativePath = string.Empty;
            if (nationalFlagFile?.Length > 0)
            {
                var targetFileName = $"{code}{Path.GetExtension(nationalFlagFile.FileName)}";
                relativePath = Path.Combine("images", targetFileName);
                var absolutePath = Path.Combine(environment.WebRootPath, relativePath);
                using (var stream = new FileStream(absolutePath, FileMode.Create))
                {
                    nationalFlagFile.CopyTo(stream);
                }
            }
            return relativePath;
        }
    }
}
