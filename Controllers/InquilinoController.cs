using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Data;
using Inmobiliaria.Models;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria.Controllers
{
    public class InquilinosController : Controller
    {
        private readonly InmobiliariaContext _context;

        public InquilinosController(InmobiliariaContext context)
        {
            _context = context;
        }

        // GET: Inquilinos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Inquilinos.ToListAsync());
        }

        // GET: Inquilinos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var inquilino = await _context.Inquilinos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inquilino == null) return NotFound();

            return View(inquilino);
        }

        // GET: Inquilinos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inquilinos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Dni,Telefono,Email")] Inquilino inquilino)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inquilino);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inquilino);
        }

        // GET: Inquilinos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var inquilino = await _context.Inquilinos.FindAsync(id);
            if (inquilino == null) return NotFound();

            return View(inquilino);
        }

        // POST: Inquilinos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Dni,Telefono,Email")] Inquilino inquilino)
        {
            if (id != inquilino.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inquilino);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Inquilinos.Any(e => e.Id == inquilino.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(inquilino);
        }

        // GET: Inquilinos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var inquilino = await _context.Inquilinos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inquilino == null) return NotFound();

            return View(inquilino);
        }

        // POST: Inquilinos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inquilino = await _context.Inquilinos.FindAsync(id);
            if (inquilino != null)
            {
                _context.Inquilinos.Remove(inquilino);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
