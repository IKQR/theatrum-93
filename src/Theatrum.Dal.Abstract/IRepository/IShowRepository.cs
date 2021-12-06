using System;

using Theatrum.Dal.Abstract.IRepository.Base;
using Theatrum.Entities.Entities;

namespace Theatrum.Dal.Abstract.IRepository
{
    public interface IShowRepository : IGenericKeyRepository<Guid, Show>
    {

    }
}
