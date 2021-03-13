using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTApplication.Data;
using IoTApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AplicatieLicentaIoT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetricsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MetricsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Metrics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Metric>>> Index()
        {
            return await _context.Metrics.ToListAsync();
        }

        // GET: api/Metrics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Metric>> GetMetric(int id)
        {
            var metric = await _context.Metrics.FindAsync(id);

            if (metric == null)
            {
                return NotFound();
            }

            return metric;
        }

        //// GET: Metrics/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Metrics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult<Metric>> PostMetric(Metric metric)
        {
            _context.Metrics.Add(metric);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostMetric", new { id = metric.Id }, metric);
        }

        // GET: Metrics/Edit/5
        public async Task<ActionResult<Metric>> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metric = await _context.Metrics.FindAsync(id);
            if (metric == null)
            {
                return NotFound();
            }
            return metric;
        }

        // POST: Metrics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Metric>> Edit(int id, [Bind("Id,Name,Dimensions")] Metric metric)
        {
            if (id != metric.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metric);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetricExists(metric.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return metric;
        }

        // GET: Metrics/Delete/5
        public async Task<ActionResult<Metric>> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metric = await _context.Metrics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metric == null)
            {
                return NotFound();
            }

            return metric;
        }

        // POST: Metrics/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult<Metric>> DeleteConfirmed(int id)
        //{
        //    var metric = await _context.Metrics.FindAsync(id);
        //    _context.Metrics.Remove(metric);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool MetricExists(int id)
        {
            return _context.Metrics.Any(e => e.Id == id);
        }
    }
}
