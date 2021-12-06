using System;

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
    }
}
