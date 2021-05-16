using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExrizCorpPanel.Models.DB;

namespace ExrizCorpPanel.Controllers
{
    [Produces("application/json")]
    [Route("api/AreaQueues")]
    public class AreaQueuesController : Controller
    {
        private readonly ExrizKurumsalContext _context;

        public AreaQueuesController(ExrizKurumsalContext context)
        {
            _context = context;
        }

        // GET: api/AreaQueues
        [HttpGet]
        public IEnumerable<AreaQueue> GetAreaQueue()
        {
            return _context.AreaQueue;
        }

        // GET: api/AreaQueues/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAreaQueue([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var areaQueue = await _context.AreaQueue.SingleOrDefaultAsync(m => m.Id == id);

            if (areaQueue == null)
            {
                return NotFound();
            }

            return Ok(areaQueue);
        }

        // PUT: api/AreaQueues/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAreaQueue([FromRoute] int id, [FromBody] AreaQueue areaQueue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != areaQueue.Id)
            {
                return BadRequest();
            }

            _context.Entry(areaQueue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaQueueExists(id))
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

        // POST: api/AreaQueues
        [HttpPost]
        public async Task<IActionResult> PostAreaQueue([FromBody] AreaQueue areaQueue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AreaQueue.Add(areaQueue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAreaQueue", new { id = areaQueue.Id }, areaQueue);
        }

        // DELETE: api/AreaQueues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAreaQueue([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var areaQueue = await _context.AreaQueue.SingleOrDefaultAsync(m => m.Id == id);
            if (areaQueue == null)
            {
                return NotFound();
            }

            _context.AreaQueue.Remove(areaQueue);
            await _context.SaveChangesAsync();

            return Ok(areaQueue);
        }

        private bool AreaQueueExists(int id)
        {
            return _context.AreaQueue.Any(e => e.Id == id);
        }
    }
}