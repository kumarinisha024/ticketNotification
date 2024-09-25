using System;
using System.ComponentModel.DataAnnotations;

namespace TicketApi.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Priority { get; set; }

        [Required]
        public string Module { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string OrderId { get; set; }

        [Required]
        public string Description { get; set; }

        public string TicketReference { get; set; }

        public bool EmailSent { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}