using Theatrum.Models.Models;

namespace Theatrum.Models.Admin
{
    public class ShowFilteringAdminModel
    {
        public GenericPaginatedModel<ShowModel> Shows { get; set; }
        public ShowFilteringSettingsAdminModel FilteringSettings { get; set; }
    }
}
