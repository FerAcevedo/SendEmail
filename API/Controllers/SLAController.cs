using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SLAController : ControllerBase
    {
        private readonly DataContext _context;
        public SLAController(DataContext context)
        {
            _context = context;            
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SLA>>> GetSLAs()
        {
            return await _context.ServicesLA.ToListAsync();            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SLA>> GetSLA(int id)
        {
            return await _context.ServicesLA.FindAsync(id);            
        }
    }
}