using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnowledgeWebApp.Data;
using KnowledgeWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace KnowledgeWebApp.Controllers
{
    public class knowledgesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public knowledgesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: knowledges
        public async Task<IActionResult> Index()
        {
            return View(await _context.knowledge.ToListAsync());
        }

        // GET: knowledges/ShowSearchForm 
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // Post: knowledges/ShowSearchResults 
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index",await _context.knowledge.Where(x=>x.generalQuestion.Contains(SearchPhrase)).ToListAsync());
        }


        // GET: knowledges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowledge = await _context.knowledge
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowledge == null)
            {
                return NotFound();
            }

            return View(knowledge);
        }

        // GET: knowledges/Create

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: knowledges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,generalQuestion,generalAnswer")] knowledge knowledge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(knowledge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(knowledge);
        }

        // GET: knowledges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowledge = await _context.knowledge.FindAsync(id);
            if (knowledge == null)
            {
                return NotFound();
            }
            return View(knowledge);
        }

        // POST: knowledges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,generalQuestion,generalAnswer")] knowledge knowledge)
        {
            if (id != knowledge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knowledge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!knowledgeExists(knowledge.Id))
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
            return View(knowledge);
        }

        // GET: knowledges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowledge = await _context.knowledge
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowledge == null)
            {
                return NotFound();
            }

            return View(knowledge);
        }

        // POST: knowledges/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var knowledge = await _context.knowledge.FindAsync(id);
            if (knowledge != null)
            {
                _context.knowledge.Remove(knowledge);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool knowledgeExists(int id)
        {
            return _context.knowledge.Any(e => e.Id == id);
        }
    }
}
