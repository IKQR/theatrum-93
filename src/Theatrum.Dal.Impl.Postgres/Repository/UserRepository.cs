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
    public class UserRepository : GenericKeyRepository<Guid, AppUser, TheatrumDbContext>, IUserRepository
    {
        public UserRepository(TheatrumDbContext context) : base(context)
        {

        }

        public async Task<int> GetAllCount(UserFilteringSettingsAdminModel userFilteringSettingsAdminModel)
        {
            return await DbSet.Where(x => userFilteringSettingsAdminModel.UserName == null || x.UserName.Contains(userFilteringSettingsAdminModel.UserName)).CountAsync();
        }

        public async Task<List<AppUser>> GetAllPaginated(UserFilteringSettingsAdminModel userFilteringSettingsAdminModel, int offset, int pageSize)
        {
            return await DbSet
                .Where(x => userFilteringSettingsAdminModel.UserName == null || x.UserName.Contains(userFilteringSettingsAdminModel.UserName))
                .Skip(offset)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
