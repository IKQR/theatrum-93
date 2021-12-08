using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Theatrum.Dal.Abstract.IRepository.Base;
using Theatrum.Entities.Entities;
using Theatrum.Models.Admin;

namespace Theatrum.Dal.Abstract.IRepository
{
    public interface IUserRepository : IGenericKeyRepository<Guid, AppUser>
    {
        Task<int> GetAllCount(UserFilteringSettingsAdminModel userFilteringSettingsAdminModel);
        Task<List<AppUser>> GetAllPaginated(UserFilteringSettingsAdminModel userFilteringSettingsAdminModel, int offset, int pageSize);
    }
}

