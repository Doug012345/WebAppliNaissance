using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppliNaissance.Data;
using WebAppliNaissance.Models;

namespace WebAppliNaissance.Controllers
{
    [Authorize]

    public class NaissanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NaissanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Naissance
        [Authorize(Roles = "Admin,Agent")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Naissances.Include(n => n.Declarant).ToListAsync());
        }

        // GET: Naissance/Details/5
        [Authorize(Roles = "Admin,Agent")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naissance = await _context.Naissances
                .Include(n => n.Declarant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (naissance == null)
            {
                return NotFound();
            }

            return View(naissance);
        }

        // GET: Naissance/Create
        [Authorize(Roles = "Admin,Agent")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Naissance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Agent")]
        public async Task<IActionResult> Create([Bind("Id,NomEnfant,PrenomEnfant,DateNaissance,HeureNaissance,LieuNaissance,Sexe,NomPere,PrenomPere,NomMere,PrenomMere,Declarant")] Naissance naissance)
        {
            if (ModelState.IsValid)
            {
                // Générer le numéro d'acte
                var year = naissance.DateNaissance.Year;
                var count = await _context.Naissances.CountAsync(n => n.DateNaissance.Year == year) + 1;
                naissance.NumeroActe = $"{year}-{count.ToString().PadLeft(4, '0')}";

                _context.Add(naissance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(naissance);
        }

        // GET: Naissance/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naissance = await _context.Naissances.FindAsync(id);
            if (naissance == null)
            {
                return NotFound();
            }
            return View(naissance);
        }

        // POST: Naissance/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomEnfant,PrenomEnfant,DateNaissance,HeureNaissance,LieuNaissance,Sexe,NomPere,PrenomPere,NomMere,PrenomMere,NumeroActe")] Naissance naissance)
        {
            if (id != naissance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(naissance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NaissanceExists(naissance.Id))
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
            return View(naissance);
        }

        // GET: Naissance/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naissance = await _context.Naissances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (naissance == null)
            {
                return NotFound();
            }

            return View(naissance);
        }

        // POST: Naissance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var naissance = await _context.Naissances.FindAsync(id);
            _context.Naissances.Remove(naissance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NaissanceExists(int id)
        {
            return _context.Naissances.Any(e => e.Id == id);
        }

        // GET: Naissance/Certificat/5
        [Authorize(Roles = "Admin,Agent")]
        public async Task<IActionResult> Certificat(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naissance = await _context.Naissances
                .Include(n => n.Declarant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (naissance == null)
            {
                return NotFound();
            }

            return View(naissance);
        }

        // GET: Naissance/PrintCertificat/5
        [Authorize(Roles = "Admin,Agent")]
        public async Task<IActionResult> PrintCertificat(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naissance = await _context.Naissances
                .Include(n => n.Declarant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (naissance == null)
            {
                return NotFound();
            }

            return View(naissance);
        }
    }
}
