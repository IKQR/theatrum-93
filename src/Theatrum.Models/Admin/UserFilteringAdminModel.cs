using Theatrum.Models.Models;

namespace Theatrum.Models.Admin
{
    public class UserFilteringAdminModel
    {
        public GenericPaginatedModel<AppUserModel> Users { get; set; }
        public UserFilteringSettingsAdminModel FilteringSettings { get; set; }
    }
}
