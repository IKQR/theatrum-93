using System;
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
    }
}
