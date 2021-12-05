using System;
using Theatrum.Dal.Abstract.IRepository;
using Theatrum.Dal.Impl.Postgres.Repository.Base;
using Theatrum.Entities.Entities;

namespace Theatrum.Dal.Impl.Postgres.Repository
{
    public class PhotoRepository : GenericKeyRepository<Guid, Photo, TheatrumDbContext>, IPhotoRepository
    {
        public PhotoRepository(TheatrumDbContext context) : base(context)
        {

        }
    }
}