using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Theatrum.Dal.Abstract.IRepository.Base;
using Theatrum.Entities.Entities;

namespace Theatrum.Dal.Abstract.IRepository
{
    public interface ISessionRepository : IGenericKeyRepository<Guid, Session>
    {
        Task<List<Session>> GetByShowId(Guid showId);
    }
}
