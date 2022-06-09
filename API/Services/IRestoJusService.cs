using API.Models;

namespace API.Services
{
    public interface IRestoJusService
    {
        Task<List<RestoJusModel>> List();
    }
}
 