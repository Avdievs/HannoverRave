using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API;
using Shared.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DJsController : ControllerBase
    {
        private readonly RaveContext _context;

        public DJsController(RaveContext context)
        {
            _context = context;
        }

        // GET: api/DJs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DJ>>> GetDJs()
        {
            return await _context.DJs.ToListAsync();
        }

        // GET: api/DJs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DJ>> GetDJ(int id)
        {
            var dJ = await _context.DJs.FindAsync(id);

            if (dJ == null)
            {
                return NotFound();
            }

            return dJ;
        }

        // PUT: api/DJs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDJ(int id, DJ dJ)
        {
            if (id != dJ.DJId)
            {
                return BadRequest();
            }

            _context.Entry(dJ).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DJExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DJs
        [HttpPost]
        public async Task<ActionResult<DJ>> PostDJ(DJ dJ)
        {
            _context.DJs.Add(dJ);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDJ", new { id = dJ.DJId }, dJ);
        }

        // DELETE: api/DJs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DJ>> DeleteDJ(int id)
        {
            var dJ = await _context.DJs.FindAsync(id);
            if (dJ == null)
            {
                return NotFound();
            }

            _context.DJs.Remove(dJ);
            await _context.SaveChangesAsync();

            return dJ;
        }

        private bool DJExists(int id)
        {
            return _context.DJs.Any(e => e.DJId == id);
        }
    }
}
