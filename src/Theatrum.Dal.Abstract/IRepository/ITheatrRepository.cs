using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Theatrum.Dal.Abstract.IRepository.Base;
using Theatrum.Entities.Entities;
using Theatrum.Models.Admin;

namespace Theatrum.Dal.Abstract.IRepository
{
    public interface ITheatrRepository : IGenericKeyRepository<Guid, Theatr>
    {
        Task<int> GetAllCount(TheatrFilteringSettingsAdminModel theatrFilteringAdminModel);
        Task<List<Theatr>> GetAllPaginated(TheatrFilteringSettingsAdminModel theatrFilteringAdminModel, int offset, int pageSize);
        Task<List<Tuple<Guid, string>>> GetAllForSelect();
    }
}

