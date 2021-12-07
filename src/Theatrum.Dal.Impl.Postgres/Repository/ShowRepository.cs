using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Theatrum.Dal.Abstract.IRepository;
using Theatrum.Dal.Impl.Postgres.Repository.Base;
using Theatrum.Entities.Entities;
using Theatrum.Models.Admin;

namespace Theatrum.Dal.Impl.Postgres.Repository
{
    public class ShowRepository : GenericKeyRepository<Guid, Show, TheatrumDbContext>, IShowRepository
    {
        public ShowRepository(TheatrumDbContext context) : base(context)
        {
        }

        public async Task<int> GetAllCount(ShowFilteringSettingsAdminModel showFilteringSettingsAdminModel)
        {
            return await DbSet.Where(x => showFilteringSettingsAdminModel.TheatrId == null || x.TheatrumId == showFilteringSettingsAdminModel.TheatrId).CountAsync();
        }

        public async Task<List<Show>> GetAllPaginated(ShowFilteringSettingsAdminModel showFilteringSettingsAdminModel, int offset, int pageSize)
        {
            return await DbSet
                .Where(x => showFilteringSettingsAdminModel.TheatrId == null || x.TheatrumId == showFilteringSettingsAdminModel.TheatrId)
                .Skip(offset)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
