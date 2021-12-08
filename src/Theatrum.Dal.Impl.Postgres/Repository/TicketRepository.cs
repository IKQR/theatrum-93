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
    public class TicketRepository : GenericKeyRepository<Guid, Ticket, TheatrumDbContext>, ITicketRepository
    {
        public TicketRepository(TheatrumDbContext context) : base(context)
        {
        }

        public async Task<List<Ticket>> GetAllBySessionId(Guid sessionId)
        {
            return await DbSet.Where(x => x.SessionId == sessionId).ToListAsync();
        }
    }
}
