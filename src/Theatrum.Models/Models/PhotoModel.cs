using System;

namespace Theatrum.Models.Models
{
    public class PhotoModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public DateTimeOffset DateChanged { get; set; }
    }
}
