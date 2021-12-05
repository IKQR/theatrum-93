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
    public class TheatrRepository : GenericKeyRepository<Guid, Theatr, TheatrumDbContext>, ITheatrRepository
    {
        public TheatrRepository(TheatrumDbContext context) : base(context)
        {
        }

        public async Task<int> GetAllCount(TheatrFilteringSettingsAdminModel theatrFilteringAdminModel)
        {
            return await DbSet.Where(x => theatrFilteringAdminModel.Name == null || x.Name.Contains(theatrFilteringAdminModel.Name)).CountAsync();
        }

        public async Task<List<Theatr>> GetAllPaginated(TheatrFilteringSettingsAdminModel theatrFilteringAdminModel, int offset, int pageSize)
        {
            return await DbSet
                .Where(x => theatrFilteringAdminModel.Name == null || x.Name.Contains(theatrFilteringAdminModel.Name))
                .Skip(offset)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
