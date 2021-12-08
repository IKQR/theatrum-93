using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Theatrum.Dal.Abstract.IRepository;
using Theatrum.Dal.Impl.Postgres.Repository.Base;
using Theatrum.Entities.Entities;

namespace Theatrum.Dal.Impl.Postgres.Repository
{
    public class SessionRepository : GenericKeyRepository<Guid, Session, TheatrumDbContext>, ISessionRepository
    {
        public SessionRepository(TheatrumDbContext context) : base(context)
        {
        }

        public async Task<List<Session>> GetByShowId(Guid showId)
        {
            return await DbSet.Where(x => x.ShowId == showId).Include(x => x.Tickets).ToListAsync();
        }
    }
}
