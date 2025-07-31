using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppliNaissance.Data;
using WebAppliNaissance.Models;
using System.Threading.Tasks;

namespace WebAppliNaissance.Controllers
{
    public class DeclarantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeclarantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Declarant
        public async Task<IActionResult> Index()
        {
            var declarants = await _context.Declarants.ToListAsync();
            return View(declarants);
        }

        // GET: Declarant/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var declarant = await _context.Declarants.FirstOrDefaultAsync(m => m.Id == id);
            if (declarant == null) return NotFound();

            return View(declarant);
        }

        // GET: Declarant/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Declarant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nom,Prenom,Adresse,CNI,LienAvecEnfant")] Declarant declarant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(declarant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(declarant);
        }

        // GET: Declarant/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var declarant = await _context.Declarants.FindAsync(id);
            if (declarant == null) return NotFound();

            return View(declarant);
        }

        // POST: Declarant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Adresse,CNI,LienAvecEnfant")] Declarant declarant)
        {
            if (id != declarant.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(declarant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeclarantExists(declarant.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(declarant);
        }

        // GET: Declarant/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var declarant = await _context.Declarants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (declarant == null) return NotFound();

            return View(declarant);
        }

        // POST: Declarant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var declarant = await _context.Declarants.FindAsync(id);
            if (declarant != null)
            {
                _context.Declarants.Remove(declarant);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DeclarantExists(int id)
        {
            return _context.Declarants.Any(e => e.Id == id);
        }
    }
}
