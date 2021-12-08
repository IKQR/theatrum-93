using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Theatrum.Models.Admin;
using Theatrum.Models.Models;

namespace Theatrum.Bl.Abstract.IServices
{
    public interface IShowService
    {
        public Task<ShowModel> GetById(Guid id);
        public Task DeleteById(Guid id);
        public Task CreateOrUpdate(ShowModel showModel);
        public Task<int> GetAllCount(ShowFilteringSettingsAdminModel showFilteringSettingsAdminModel);
        public Task<List<ShowModel>> GetAllPaginated(ShowFilteringSettingsAdminModel showFilteringSettingsAdminModel, int offset, int pageSize);
        public Task<List<PlaceModel>> GetPlacesBySessionId(Guid sessionId);
    }
}
