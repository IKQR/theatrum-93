using System;

using Theatrum.Dal.Abstract.IRepository.Base;
using Theatrum.Entities.Entities;

namespace Theatrum.Dal.Abstract.IRepository
{
    public interface IPhotoRepository : IGenericKeyRepository<Guid, Photo>
    {

    }
}
