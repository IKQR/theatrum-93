using System;
using Theatrum.Dal.Abstract.IRepository.Base;
using Theatrum.Entities.Entities;

namespace Theatrum.Dal.Impl.Postgres.Repository
{
    public interface ITheatrRepository : IGenericKeyRepository<Guid, Theatr>
    {

    }
}
