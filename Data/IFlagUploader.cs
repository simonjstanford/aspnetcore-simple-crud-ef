using Microsoft.AspNetCore.Http;

namespace GlobalCityManager.Data
{
    public interface IFlagUploader
    {
        string CreateFlag(string code, IFormFile nationalFlagFile);
    }
}
