using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mapster;

using Theatrum.Bl.Abstract.IServices;
using Theatrum.Dal.Abstract.IRepository;
using Theatrum.Entities.Entities;
using Theatrum.Models.Admin;
using Theatrum.Models.Models;

namespace Theatrum.Bl.Impl.Services
{
    public class ShowService : IShowService
    {
        private readonly IShowRepository _showRepository;
        private readonly IPhotoService _photoService;
        private readonly ISessionRepository _sessionRepository;
        private readonly ITicketRepository _ticketRepository;

        public ShowService(IShowRepository showRepository, IPhotoService photoService, ISessionRepository sessionRepository, ITicketRepository ticketRepository)
        {
            _showRepository = showRepository;
            _photoService = photoService;
            _sessionRepository = sessionRepository;
            _ticketRepository = ticketRepository;
        }
        public async Task<ShowModel> GetById(Guid id)
        {
            var theatr = await _showRepository.GetByIdAsync(id);
            var m = theatr.Adapt<Show, ShowModel>();

            var ses = await _sessionRepository.GetByShowId(m.Id ?? Guid.Empty);

            m.Sessions = ses.Select(x =>
            {
                var t = x.Adapt<Session, SessionModel>();
                t.PlacesCount = x.Tickets.Count();
                return t;
            }).ToList();

            return m;
        }

        public async Task DeleteById(Guid id)
        {
            var theatr = await _showRepository.GetByIdAsync(id);
            await _showRepository.DeleteAsync(theatr);
        }

        public async Task CreateOrUpdate(ShowModel model)
        {
            //map photo
            if (model.ShowPhoto != null)
            {
                var photoId = await _photoService.SaveOrUpdatePhoto(model.PhotoId, model.ShowPhoto);
                model.PhotoId = photoId;
            }
            if (model.Id == null)
            {
                await _showRepository.AddAsync(model.Adapt<ShowModel, Show>());
            }
            else
            {
                var theatr = await _showRepository.GetByIdAsync((Guid)model.Id);
                await _showRepository.UpdateAsync(model.Adapt(theatr));
            }
        }

        public async Task<int> GetAllCount(ShowFilteringSettingsAdminModel showFilteringSettingsAdminModel)
        {
            return await _showRepository.GetAllCount(showFilteringSettingsAdminModel);
        }

        public async Task<List<ShowModel>> GetAllPaginated(ShowFilteringSettingsAdminModel showFilteringSettingsAdminModel, int offset, int pageSize)
        {
            var theatrs = await _showRepository.GetAllPaginated(showFilteringSettingsAdminModel, offset, pageSize);
            return theatrs.Adapt<List<Show>, List<ShowModel>>();
        }

        public async Task<List<PlaceModel>> GetPlacesBySessionId(Guid sessionId)
        {
            var tickets = await _ticketRepository.GetAllBySessionId(sessionId);

            return tickets.Select(x => x.Adapt<Ticket, PlaceModel>()).ToList();
        }

        public async Task<List<PlaceModel>> CreateTickets(List<string> tickets, Guid userId, Guid sessionId)
        {
            var entities = tickets.Select(x => new Ticket()
            {
                AppUserId = userId,
                PlaceId = x,
                SessionId = sessionId,
                SecurityKey = (Guid.NewGuid()).ToString(),
            }).ToList();
            await _ticketRepository.AddRangeAsync(entities);
            var securityKeys = entities.Select(x => x.SecurityKey).ToList();
            var result = await _ticketRepository.GetByAsync(x => x.SessionId == sessionId && x.AppUserId == userId && securityKeys.Contains(x.SecurityKey));
            return result.Adapt<List<Ticket>, List<PlaceModel>>();
        }

        public async Task<List<ShowModel>> GetActualPaginated(ShowFilteringSettingsAdminModel showFilteringSettingsAdminModel, int offset, int pageSize)
        {
            var theatrs = await _showRepository.GetActualPaginated(showFilteringSettingsAdminModel, offset, pageSize);
            return theatrs.Adapt<List<Show>, List<ShowModel>>();
        }
    }
}
