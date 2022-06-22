using System;

namespace Theatrum.Models.Models
{
    public class PlaceModel
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public string PlaceId { get; set; }
        public string SecurityKey { get; set; }
        public byte[] QrCode { get; set; }
    }
}
