using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Theatrum.Models.Admin;
using Theatrum.Models.Models;

namespace Theatrum.Bl.Abstract.IServices
{
    public interface IUserService
    {
        public Task<AppUserModel> GetById(Guid id);
        public Task DeleteById(Guid id);
        public Task<int> GetAllCount(UserFilteringSettingsAdminModel userFilteringSettingsAdminModel);
        public Task<List<AppUserModel>> GetAllPaginated(UserFilteringSettingsAdminModel userFilteringSettingsAdminModel, int offset, int pageSize);
    }
}
