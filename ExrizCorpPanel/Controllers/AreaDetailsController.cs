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
    [Route("api/AreaDetails")]
    public class AreaDetailsController : Controller
    {
        private readonly ExrizKurumsalContext _context;

        public AreaDetailsController(ExrizKurumsalContext context)
        {
            _context = context;
        }

        // GET: api/AreaDetails
        [HttpGet]
        public IEnumerable<AreaDetail> GetAreaDetail()
        {
            return _context.AreaDetail;
        }

        // GET: api/AreaDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAreaDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var areaDetail = await _context.AreaDetail.SingleOrDefaultAsync(m => m.Id == id);

            if (areaDetail == null)
            {
                return NotFound();
            }

            return Ok(areaDetail);
        }

        // PUT: api/AreaDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAreaDetail([FromRoute] int id, [FromBody] AreaDetail areaDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != areaDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(areaDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaDetailExists(id))
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

        // POST: api/AreaDetails
        [HttpPost]
        public async Task<IActionResult> PostAreaDetail([FromBody] AreaDetail areaDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AreaDetail.Add(areaDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAreaDetail", new { id = areaDetail.Id }, areaDetail);
        }

        // DELETE: api/AreaDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAreaDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var areaDetail = await _context.AreaDetail.SingleOrDefaultAsync(m => m.Id == id);
            if (areaDetail == null)
            {
                return NotFound();
            }

            _context.AreaDetail.Remove(areaDetail);
            await _context.SaveChangesAsync();

            return Ok(areaDetail);
        }

        private bool AreaDetailExists(int id)
        {
            return _context.AreaDetail.Any(e => e.Id == id);
        }
    }
}