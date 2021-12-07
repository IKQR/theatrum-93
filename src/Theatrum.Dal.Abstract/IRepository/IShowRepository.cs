using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theatrum.Dal.Abstract.IRepository.Base;
using Theatrum.Entities.Entities;
using Theatrum.Models.Admin;

namespace Theatrum.Dal.Abstract.IRepository
{
    public interface IShowRepository : IGenericKeyRepository<Guid, Show>
    {
        Task<int> GetAllCount(ShowFilteringSettingsAdminModel showFilteringSettingsAdminModel);
        Task<List<Show>> GetAllPaginated(ShowFilteringSettingsAdminModel showFilteringSettingsAdminModel, int offset, int pageSize);
        Task<List<Show>> GetActualPaginated(ShowFilteringSettingsAdminModel showFilteringSettingsAdminModel, int offset, int pageSize);
    }
}
