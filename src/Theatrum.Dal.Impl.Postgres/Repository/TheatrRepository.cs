using System;
using Theatrum.Dal.Abstract.IRepository;
using Theatrum.Dal.Impl.Postgres.Repository.Base;
using Theatrum.Entities.Entities;

namespace Theatrum.Dal.Impl.Postgres.Repository
{
    public class TheatrRepository : GenericKeyRepository<Guid, Theatr, TheatrumDbContext>, ITheatrRepository
    {
        public TheatrRepository(TheatrumDbContext context) : base(context)
        {
        }
    }
}
