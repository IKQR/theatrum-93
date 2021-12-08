using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace Theatrum.Models.Models
{
    public class ShowModel
    {
        public Guid? Id { get; set; }
        [DisplayName("Id театру")]  
        public Guid TheatrumId { get; set; }
        [Required]
        [DisplayName("Ім'я")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DisplayName("Опис")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        [DisplayName("Дата початку")]
        [DataType(DataType.Date)]
        public DateTimeOffset StartDate { get; set; }
        [Required]
        [DisplayName("Дата кінця")]
        [DataType(DataType.Date)]
        public DateTimeOffset EndDate { get; set; }
        [DisplayName("Фото")]
        public Guid? PhotoId { get; set; }
        [DisplayName("Фото")]
        public IFormFile ShowPhoto { get; set; }
        [Required]
        [DisplayName("Вікові обмеження")]
        public int AgeLimitation { get; set; }

        public List<SessionModel> Sessions { get; set; }
    }
}
