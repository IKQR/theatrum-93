using System;
using Microsoft.AspNetCore.Http;

namespace Theatrum.Models.Models
{
    public class ShowModel
    {
        public Guid? Id { get; set; }
        public Guid TheatrumId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public Guid? PhotoId { get; set; }
        public IFormFile ShowPhoto { get; set; }
        public int AgeLimitation { get; set; }
    }
}
