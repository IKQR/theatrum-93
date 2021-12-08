using System;
using System.ComponentModel.DataAnnotations;

namespace Theatrum.Models.Models
{
    public class TheatrModel
    {
        public Guid? Id { get; set; }
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        public string City { get; set; }

        [DataType(DataType.Text)]
        public string Address { get; set; }
    }
}
