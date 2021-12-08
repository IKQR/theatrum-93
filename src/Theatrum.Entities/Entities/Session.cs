using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theatrum.Entities.Entities
{
    public class Session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public Guid ShowId { get; set; }
        [ForeignKey(nameof(ShowId))]
        public Show Show { get; set; }
        
        public List<Ticket> Tickets { get; set; }
    }
}
