using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using TicketApi.Data;
using TicketApi.Models;

namespace TicketProcessor.Services
{
    public class TicketProcessingService
    {
        private readonly TicketDbContext _context;

        public TicketProcessingService(TicketDbContext context)
        {
            _context = context;
        }

        public async Task ProcessTicketsAsync()
        {
            var highPriorityTickets = await _context.Tickets
                .Where(t => t.Priority == "High" && !t.EmailSent)
                .ToListAsync();

            foreach (var ticket in highPriorityTickets)
            {
                if (SendEmail(ticket))
                {
                    ticket.EmailSent = true;
                    _context.Update(ticket);
                }
            }

            await _context.SaveChangesAsync();
        }

        private bool SendEmail(Ticket ticket)
        {
            try
            {
                var mailMessage = new MailMessage("emailtest7896@gmail.com", "emailtest7896@gmail.com")
                {
                    Subject = $"High Priority Ticket: {ticket.Title}",
                    Body = $"Ticket Details:\n\n" +
                           $"User: {ticket.UserId}\n" +
                           $"Module: {ticket.Module}\n" +
                           $"Description: {ticket.Description}\n" +
                           $"Order Id: {ticket.OrderId}\n" +
                           $"Ticket Reference: {ticket.TicketReference}"
                };

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("emailtest7896@gmail.com", "lyul eyqz daho gelc"),
                    EnableSsl = true,
                };

                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}