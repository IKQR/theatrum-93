using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theatrum.Entities.Entities
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        [ForeignKey(nameof(AppUserId))]
        public AppUser AppUser { get; set; }
        public Guid SessionId { get; set; }
        [ForeignKey(nameof(SessionId))]
        public Session Session { get; set; }
        public string SecurityKey { get; set; }
        public bool IsChecked { get; set; }
    }
}
