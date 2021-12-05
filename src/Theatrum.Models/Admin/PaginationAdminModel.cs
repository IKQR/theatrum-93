using System;

namespace Theatrum.Models.Admin
{
    public class PaginationAdminModel
    {
        public int PageNumber { get; protected set; }
        public int Total { get; protected set; }
        public string Action { get; protected set; }

        private PaginationAdminModel()
        {

        }
        public PaginationAdminModel(int count, int pageNumber, int pageSize, string action)
        {
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            Total = (int)Math.Ceiling(count / (double)pageSize);
            Action = action;
        }

        public bool HasPrevious => (PageNumber > 1);
        public bool HasNext => (PageNumber < Total);

    }
}
