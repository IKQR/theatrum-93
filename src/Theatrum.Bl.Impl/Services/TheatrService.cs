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
    public class TheatrService : ITheatrService
    {
        private readonly ITheatrRepository _theatrRepository;

        public TheatrService(ITheatrRepository theatrRepository)
        {
            _theatrRepository = theatrRepository;
        }
        public async Task<TheatrModel> GetById(Guid id)
        {
            var theatr = await _theatrRepository.GetByIdAsync(id);
            return theatr.Adapt<Theatr, TheatrModel>();
        }

        public async Task DeleteById(Guid id)
        {
            var theatr = await _theatrRepository.GetByIdAsync(id);
            await _theatrRepository.DeleteAsync(theatr);
        }

        public async Task CreateOrUpdate(TheatrModel theatrModel)
        {
            if (theatrModel.Id == null)
            {
                await _theatrRepository.AddAsync(theatrModel.Adapt<TheatrModel, Theatr>());
            }
            else
            {
                var theatr = await _theatrRepository.GetByIdAsync((Guid)theatrModel.Id);
                await _theatrRepository.UpdateAsync(theatrModel.Adapt(theatr));
            }
        }

        public async Task<int> GetAllCount(TheatrFilteringSettingsAdminModel theatrFilteringAdminModel)
        {
            return await _theatrRepository.GetAllCount(theatrFilteringAdminModel);
        }

        public async Task<List<TheatrModel>> GetAllPaginated(TheatrFilteringSettingsAdminModel theatrFilteringAdminModel, int offset, int pageSize)
        {
            var theatrs = await _theatrRepository.GetAllPaginated(theatrFilteringAdminModel, offset, pageSize);
            return theatrs.Adapt<List<Theatr>, List<TheatrModel>>();
        }
    }
}
