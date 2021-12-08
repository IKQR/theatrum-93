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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<AppUserModel> GetById(Guid id)
        {
            var theatr = await _userRepository.GetByIdAsync(id);
            return theatr.Adapt<AppUser, AppUserModel>();
        }

        public async Task DeleteById(Guid id)
        {
            var theatr = await _userRepository.GetByIdAsync(id);
            await _userRepository.DeleteAsync(theatr);
        }

        public async Task<int> GetAllCount(UserFilteringSettingsAdminModel userFilteringSettingsAdminModel)
        {
            return await _userRepository.GetAllCount(userFilteringSettingsAdminModel);
        }

        public async Task<List<AppUserModel>> GetAllPaginated(UserFilteringSettingsAdminModel userFilteringSettingsAdminModel, int offset, int pageSize)
        {
            var users = await _userRepository.GetAllPaginated(userFilteringSettingsAdminModel, offset, pageSize);
            return users.Adapt<List<AppUser>, List<AppUserModel>>();
        }
    }
}
