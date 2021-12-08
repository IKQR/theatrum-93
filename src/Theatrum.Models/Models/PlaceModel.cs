using System;

namespace Theatrum.Models.Models
{
    public class PlaceModel
    {
        public Guid SessionId { get; set; }
        public string PlaceId { get; set; }
        public string SecurityKey { get; set; }
    }
}
