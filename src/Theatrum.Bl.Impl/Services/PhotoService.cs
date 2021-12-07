using System;
using System.IO;
using System.Threading.Tasks;

using Mapster;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Theatrum.Bl.Abstract.IServices;
using Theatrum.Dal.Abstract.IRepository;
using Theatrum.Entities.Entities;
using Theatrum.Models.Models;
using Theatrum.Utils;

namespace Theatrum.Bl.Impl.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IHostingEnvironment _env;

        public PhotoService(
            IPhotoRepository photoRepository,
            IHostingEnvironment env)
        {
            _photoRepository = photoRepository;
            _env = env;
        }

        public async Task<Guid?> SaveOrUpdatePhoto(Guid? photoId, IFormFile photo)
        {

            if (photoId != null)
            {
                Photo photoEntity = await _photoRepository.GetByIdAsync((Guid)photoId);
                if (photoEntity != null)
                {
                    photoEntity.ContentType = photo.ContentType;
                    await _photoRepository.UpdateAsync(photoEntity);
                    using (Stream fileStream = new FileStream(Path.Combine(_env.ContentRootPath, Settings.UploadDirectory, photoEntity.Name), FileMode.OpenOrCreate))
                    {
                        await photo.CopyToAsync(fileStream);
                    }
                    return photoId;
                }
            }
            Photo saveResponse = await _photoRepository.AddAsync(
                new Photo()
                {
                    ContentType = photo.ContentType,
                    DateChanged = DateTimeOffset.UtcNow,
                    Name = Guid.NewGuid() + photo.FileName
                });

            string filePath = Path.Combine(_env.ContentRootPath, Settings.UploadDirectory, saveResponse.Name);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }
            return saveResponse.Id;
        }

        public async Task<PhotoModel> GetPhoto(Guid photoId)
        {
            Photo photo = await _photoRepository.GetByIdAsync(photoId);
            if (photo == null)
            {
                return null;
            }

            return photo.Adapt<Photo, PhotoModel>();
        }

        public async Task DeletePhoto(Guid? photoId)
        {
            if (photoId == null)
            {
                return;
            }
            Photo photo = await _photoRepository.GetByIdAsync((Guid)photoId);
            if (photo == null)
            {
                return;
            }
            string filePath = Path.Combine(_env.ContentRootPath, Settings.UploadDirectory, photo.Name);
            File.Delete(filePath);
            await _photoRepository.DeleteAsync(photo);
        }
    }
}
