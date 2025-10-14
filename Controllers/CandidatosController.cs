using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CandidatosData;
using CandidatosModel;

namespace CandidatosUI.Controllers
{
    public class CandidatosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CandidatosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Candidatos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Candidatos.ToListAsync());
        }

        // GET: Candidatos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidatoModel = await _context.Candidatos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidatoModel == null)
            {
                return NotFound();
            }

            return View(candidatoModel);
        }

        // GET: Candidatos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidatos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Partido,Idade")] CandidatoModel candidatoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candidatoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(candidatoModel);
        }

        // GET: Candidatos/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidatoModel = await _context.Candidatos.FindAsync(id);
            if (candidatoModel == null)
            {
                return NotFound();
            }
            return View(candidatoModel);
        }

        // POST: Candidatos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nome,Partido,Idade")] CandidatoModel candidatoModel)
        {
            if (id != candidatoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidatoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatoModelExists(candidatoModel.Id))
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
            return View(candidatoModel);
        }

        // GET: Candidatos/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidatoModel = await _context.Candidatos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidatoModel == null)
            {
                return NotFound();
            }

            return View(candidatoModel);
        }

        // POST: Candidatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var candidatoModel = await _context.Candidatos.FindAsync(id);
            if (candidatoModel != null)
            {
                _context.Candidatos.Remove(candidatoModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidatoModelExists(string id)
        {
            return _context.Candidatos.Any(e => e.Id == id);
        }
    }
}
