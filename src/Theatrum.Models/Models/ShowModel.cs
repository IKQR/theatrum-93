using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace Theatrum.Models.Models
{
    public class ShowModel
    {
        public Guid? Id { get; set; }
        public Guid TheatrumId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTimeOffset StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTimeOffset EndDate { get; set; }
        public Guid? PhotoId { get; set; }
        public IFormFile ShowPhoto { get; set; }
        [Required]
        public int AgeLimitation { get; set; }

        public List<SessionModel> Sessions { get; set; }
    }
}
