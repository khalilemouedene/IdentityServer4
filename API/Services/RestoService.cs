using API.Models;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class RestoService : IRestoJusService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RestoService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<RestoJusModel>> List()
        {
            return await (from jus in _applicationDbContext.RestoJus
                   select new RestoJusModel()
                   {
                       Id = jus.Id,
                       Name = jus.Name,
                   }).ToListAsync();
        }
    }
}
