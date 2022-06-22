using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QRCoder;
using Theatrum.Bl.Abstract.IServices;
using Theatrum.Dal.Impl.Postgres;
using Theatrum.Entities.Entities;
using Theatrum.Models.Admin;
using Theatrum.Models.Models;
using Theatrum.Models.Settings;
using Theatrum.Utils;

namespace Theatrum.Web.Razor.Controllers
{
    [Route("show")]
    public class ShowsController : Controller
    {
        private readonly IShowService _showService;
        private readonly UserManager<AppUser> _userManager;
        private readonly PaginationConfig _paginationConfig;

        public ShowsController(IShowService showService,
            IOptions<PaginationConfig> paginationConfig,
            UserManager<AppUser> userManager)
        {
            _showService = showService;
            _userManager = userManager;
            _paginationConfig = paginationConfig.Value;
        }

        //public async Task<IActionResult> Shows(ShowFilteringAdminModel filteringAdminModel, [FromQuery] int page = 1)
        //{
        //    var result = await Shows(filteringAdminModel, page, nameof(Shows));
        //    return View(result);
        //}

        //private async Task<ShowFilteringAdminModel> Shows(ShowFilteringAdminModel filteringAdminModel, int page, string actionName)
        //{
        //    if (filteringAdminModel.FilteringSettings == null)
        //    {
        //        filteringAdminModel.FilteringSettings = new ShowFilteringSettingsAdminModel();
        //    }

        //    var count = await _showService.GetAllCount(filteringAdminModel.FilteringSettings);

        //    var shows =
        //        await _showService.GetAllPaginated(filteringAdminModel.FilteringSettings,
        //            _paginationConfig.PaginationAdminPageSize * (page - 1),
        //            _paginationConfig.PaginationAdminPageSize);

        //    ShowFilteringAdminModel showsFilteringAdminModel = new ShowFilteringAdminModel()
        //    {
        //        Shows = new GenericPaginatedModel<ShowModel>()
        //        {
        //            Models = shows,
        //            Pagination = new PaginationAdminModel(count, page,
        //                _paginationConfig.PaginationAdminPageSize, actionName),
        //        },
        //        FilteringSettings = filteringAdminModel.FilteringSettings,
        //    };
        //    return showsFilteringAdminModel;
        //}

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Details([FromRoute] Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var theatr = await _showService.GetById((Guid)id);
            return View(theatr);
        }


        [HttpGet]
        [Authorize]
        [Route("tickets/{id}")]
        public async Task<IActionResult> Tickets([FromRoute] Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<PlaceModel> places = await _showService.GetPlacesBySessionId((Guid)id);
            return View(places);
        }

        [Authorize]
        [Route("BuyTicket")]
        public async Task<IActionResult> BuyTickets(Guid sessionId, List<string> tickets)
        {
            Guid userId = (await _userManager.FindByNameAsync(User?.Identity?.Name)).Id;
            List<PlaceModel> result = await _showService.CreateTickets(tickets, userId, sessionId);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].QrCode = await GetQrCode(result[i].SecurityKey.ToString(), Color.Black);
            }
            return View(result);
        }

        public async Task<Byte[]> GetQrCode(string qrCodeText, Color color)
        {
            // using static method as instance method was just redirection to static one
            QRCodeData qrCodeData = QRCodeGenerator.GenerateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(4);
            if (color != Color.Black)
            {
                qrCodeImage = ChangeColor(qrCodeImage, color);
            }
            return BitmapToBytes(qrCodeImage);
        }

        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        private Bitmap ChangeColor(Bitmap scrBitmap, Color newColor)
        {
            // make an empty bitmap the same size as scrBitmap  
            Bitmap newBitmap = new Bitmap(scrBitmap.Width, scrBitmap.Height);
            for (int i = 0; i < scrBitmap.Width; i++)
            {
                for (int j = 0; j < scrBitmap.Height; j++)
                {
                    // get the pixel from the scrBitmap image  
                    var actualColor = scrBitmap.GetPixel(i, j);

                    // invert colors, since we want to tint the dark parts and not the bright ones
                    var invertedOriginalR = 255 - actualColor.R;
                    var invertedOriginalG = 255 - actualColor.G;
                    var invertedOriginalB = 255 - actualColor.B;

                    // multiply source by destination color (as float channels)
                    int r = (invertedOriginalR / 255) * (newColor.R / 255) * 255;
                    int g = (invertedOriginalG / 255) * (newColor.G / 255) * 255;
                    int b = (invertedOriginalB / 255) * (newColor.B / 255) * 255;
                    var tintedColor = Color.FromArgb(r, g, b);
                    newBitmap.SetPixel(i, j, tintedColor);
                }
            }
            return newBitmap;
        }
    }
}
