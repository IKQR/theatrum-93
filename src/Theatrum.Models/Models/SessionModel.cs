using System;

namespace Theatrum.Models.Models
{
    public class SessionModel
    {
        public Guid Id { get; set; }
        public Guid ShowId { get; set; }
        public int PlacesCount { get; set; }
        public DateTimeOffset StartDate { get; set; }
    }
}
