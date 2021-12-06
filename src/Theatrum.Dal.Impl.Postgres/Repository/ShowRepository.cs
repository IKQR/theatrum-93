using System;

using Theatrum.Dal.Abstract.IRepository;
using Theatrum.Dal.Impl.Postgres.Repository.Base;
using Theatrum.Entities.Entities;

namespace Theatrum.Dal.Impl.Postgres.Repository
{
    public class ShowRepository : GenericKeyRepository<Guid, Show, TheatrumDbContext>, IShowRepository
    {
        public ShowRepository(TheatrumDbContext context) : base(context)
        {
        }
    }
}
