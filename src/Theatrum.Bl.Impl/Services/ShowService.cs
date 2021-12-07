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

        public ShowService(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }
        public async Task<ShowModel> GetById(Guid id)
        {
            var theatr = await _showRepository.GetByIdAsync(id);
            return theatr.Adapt<Show, ShowModel>();
        }

        public async Task DeleteById(Guid id)
        {
            var theatr = await _showRepository.GetByIdAsync(id);
            await _showRepository.DeleteAsync(theatr);
        }

        public async Task CreateOrUpdate(ShowModel model)
        {
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
    }
}
