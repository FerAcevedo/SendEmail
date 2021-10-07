using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly DataContext _context;

        public EmailController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailInfo>>> GetEmails()
        {
            return await _context.Emails.ToListAsync();            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmailInfo>> GetEmail(int id)
        {
            return await _context.Emails.FindAsync(id);            
        }

        [HttpPost("send")]
        public async Task<ActionResult<EmailInfo>> PostEmail(EmailDto emailDto)
        {
            if (emailDto is null)
            {
                throw new ArgumentNullException(nameof(emailDto));
            }

            EmailInfo email = new()
            {
                EmailAddress = emailDto.EmailAddress,
                SLA = emailDto.SLA,
                RefNumber = emailDto.RefNumber,
                TranslationText = emailDto.TranslationText
            };
            await _context.AddAsync(email);
            await _context.SaveChangesAsync();

            return email; 
        }
    }
}