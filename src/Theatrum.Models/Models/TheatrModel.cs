using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Theatrum.Models.Models
{
    public class TheatrModel
    {
        public Guid? Id { get; set; }
        [DisplayName("Назва")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DisplayName("Опис")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayName("Місто")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [DisplayName("Адреса")]
        [DataType(DataType.Text)]
        public string Address { get; set; }
    }
}
