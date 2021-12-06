using System;

namespace Theatrum.Models.Models
{
    public class TheatrModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
