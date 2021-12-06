using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Theatrum.Models.Admin;
using Theatrum.Models.Models;

namespace Theatrum.Bl.Abstract.IServices
{
    public interface ITheatrService
    {
        public Task<TheatrModel> GetById(Guid id);
        public Task DeleteById(Guid id);
        public Task CreateOrUpdate(TheatrModel theatrModel);
        public Task<int> GetAllCount(TheatrFilteringSettingsAdminModel theatrFilteringAdminModel);
        public Task<List<TheatrModel>> GetAllPaginated(TheatrFilteringSettingsAdminModel theatrFilteringAdminModel, int offset, int pageSize);
    }
}
