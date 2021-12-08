using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Theatrum.Bl.Abstract.IServices;
using Theatrum.Models.Models;
using Theatrum.Utils;

namespace Theatrum.Web.Razor.Controllers
{
    [Route("photo")]
    public class PhotoController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly IWebHostEnvironment _env;

        public PhotoController(IPhotoService photoService,
            IWebHostEnvironment env)
        {
            _photoService = photoService;
            _env = env;
        }

        [HttpGet]
        [Route("AdminPhoto/{photoId}")]
        public async Task<FileResult> AdminPhoto(Guid photoId)
        {
            if (photoId == Guid.Empty)
                return null;

            PhotoModel photo = await _photoService.GetPhoto(photoId);
            var filePath = Path.Combine(
                _env.ContentRootPath, Settings.UploadDirectory, photo.Name);
            return PhysicalFile(filePath, photo.ContentType);
        }
    }
}
