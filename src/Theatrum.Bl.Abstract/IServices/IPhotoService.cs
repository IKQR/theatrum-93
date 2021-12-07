using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using Theatrum.Models.Models;

namespace Theatrum.Bl.Abstract.IServices
{
    public interface IPhotoService
    {
        Task<Guid?> SaveOrUpdatePhoto(Guid? photoId, IFormFile photo);
        Task<PhotoModel> GetPhoto(Guid photoId);
        Task DeletePhoto(Guid? photoId);
    }
}
