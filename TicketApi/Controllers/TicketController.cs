using Microsoft.AspNetCore.Mvc;
using TicketApi.Models;
using TicketApi.Data;
using System.Threading.Tasks;
using System;

namespace TicketApi.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketDbContext _context;

        public TicketController(TicketDbContext context)
        {
            _context = context;
        }

        [HttpPost("raiseticket")]
        public async Task<IActionResult> RaiseTicket([FromBody] Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest(new { success = false, message = "Invalid ticket data." });
            }

            ticket.TicketReference = DateTime.UtcNow.Ticks.ToString().Substring(0, 10); // Assuming you want a reference number
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = $"Ticket raised successfully. Ticket Reference #{ticket.TicketReference}" });
        }
    }
}