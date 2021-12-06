using Theatrum.Models.Models;

namespace Theatrum.Models.Admin
{
    public class TheatrFilteringAdminModel
    {
        public GenericPaginatedModel<TheatrModel> Theatrs { get; set; }
        public TheatrFilteringSettingsAdminModel FilteringSettings { get; set; }
    }
}
