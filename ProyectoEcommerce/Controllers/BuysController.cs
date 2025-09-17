using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoEcommerce.Data;
using ProyectoEcommerce.Models;

namespace ProyectoEcommerce.Controllers
{
    public class BuysController : Controller
    {
        private readonly ProyectoEcommerceContext _context;

        public BuysController(ProyectoEcommerceContext context)
        {
            _context = context;
        }

        // GET: Buys
        public async Task<IActionResult> Index()
        {
            var proyectoEcommerceContext = _context.Buys.Include(b => b.Customer).Include(b => b.Employee);
            return View(await proyectoEcommerceContext.ToListAsync());
        }

        // GET: Buys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buy = await _context.Buys
                .Include(b => b.Customer)
                .Include(b => b.Employee)
                .FirstOrDefaultAsync(m => m.BuyId == id);
            if (buy == null)
            {
                return NotFound();
            }

            return View(buy);
        }

        // GET: Buys/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name");
            return View();
        }

        // POST: Buys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BuyId,CustomerId,EmployeeId,Fecha,Subtotal,IVA,Total")] Buy buy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", buy.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", buy.EmployeeId);
            return View(buy);
        }

        // GET: Buys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buy = await _context.Buys.FindAsync(id);
            if (buy == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", buy.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", buy.EmployeeId);
            return View(buy);
        }

        // POST: Buys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BuyId,CustomerId,EmployeeId,Fecha,Subtotal,IVA,Total")] Buy buy)
        {
            if (id != buy.BuyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuyExists(buy.BuyId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", buy.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", buy.EmployeeId);
            return View(buy);
        }

        // GET: Buys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buy = await _context.Buys
                .Include(b => b.Customer)
                .Include(b => b.Employee)
                .FirstOrDefaultAsync(m => m.BuyId == id);
            if (buy == null)
            {
                return NotFound();
            }

            return View(buy);
        }

        // POST: Buys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buy = await _context.Buys.FindAsync(id);
            if (buy != null)
            {
                _context.Buys.Remove(buy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuyExists(int id)
        {
            return _context.Buys.Any(e => e.BuyId == id);
        }
    }
}
