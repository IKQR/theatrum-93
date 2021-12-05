using System.Collections.Generic;

namespace Theatrum.Models.Admin
{
    public class GenericPaginatedModel<TModel>
    {
        public List<TModel> Models { get; set; }
        public PaginationAdminModel Pagination { get; set; }
        public string NextDataUrl { get; set; }
    }
}
