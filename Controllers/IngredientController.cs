using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELabel.Data;
using ELabel.Models;
using ELabel.Extensions;
using ELabel.ViewModels;
using Ganss.Excel;

namespace ELabel.Controllers
{
    public class IngredientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IngredientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ingredient
        public async Task<IActionResult> Index()
        {
            if (_context.Ingredient == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Ingredient'  is null.");
            }

            var query = await _context.Ingredient
                                      .AsNoTracking()
                                      .OrderBy(i => i.Name)
                                      .ThenBy(i => i.Category)
                                      .ToListAsync();

            return View(query);
        }

        // GET: Ingredient/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Ingredient == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // GET: Ingredient/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingredient/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Category,Allergen")] Ingredient ingredient)
        {
            ingredient.Id = Guid.NewGuid();
            // ingredient.Custom = true; TODO: Uncomment in release

            if (ModelState.IsValid)
            {
                _context.Add(ingredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        // GET: Ingredient/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Ingredient == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredient.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredient/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Category,Allergen,Custom,Id")] Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // ingredient.Custom = true; TODO: Uncomment in release

                    _context.Update(ingredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientExists(ingredient.Id))
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
            return View(ingredient);
        }

        // GET: Ingredient/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Ingredient == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // POST: Ingredient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Ingredient == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Ingredient'  is null.");
            }
            var ingredient = await _context.Ingredient.FindAsync(id);
            if (ingredient != null)
            {
                _context.Ingredient.Remove(ingredient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientExists(Guid id)
        {
          return (_context.Ingredient?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private Guid? FindIngredientId(string name, IngredientCategory category)
        {
            return _context.Ingredient.Where(e => e.Name == name && e.Category == category).AsNoTracking().FirstOrDefault()?.Id;
        }

        // GET: Ingredient/Export
        public async Task<IActionResult> Export()
        {
            var ingredients = await _context.Ingredient
                                            .AsNoTracking()
                                            .OrderBy(i => i.Name)
                                            .ThenBy(i => i.Category)
                                            .ToListAsync();

            byte[] byteArray;
            var excelMapper = new ExcelMapper();
            excelMapper.IgnoreNestedTypes = true;

            using (MemoryStream stream = new MemoryStream())
            {
                excelMapper.Save(stream, ingredients, "Ingredients");

                byteArray = stream.ToArray();
            }

            return File(byteArray, "application/xlsx", $"e-label-ingredients.xlsx");
        }

        // GET: Ingredient/Import
        public IActionResult Import()
        {
            return View();
        }

        // POST: Ingredient/Import
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import([Bind("File")] ImportFileUpload importFileUpload)
        {
            if (!ModelState.IsValid)
            {
                return View(importFileUpload);
            }

            if (importFileUpload.File == null || importFileUpload.File.Length == 0)
            {
                ModelState.AddModelError("CustomError", "Empty file!");
                return View(importFileUpload);
            }

            IEnumerable<Ingredient> importedIngredients;

            using (var memoryStream = new MemoryStream())
            {
                await importFileUpload.File.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                try
                {
                    ExcelMapper excelMapper = new ExcelMapper(memoryStream);
                    excelMapper.IgnoreNestedTypes = true;
                    importedIngredients = excelMapper.Fetch<Ingredient>("Ingredients");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("CustomError", e.Message);
                    return View(importFileUpload);
                }
            }

            if (importedIngredients == null || !importedIngredients.Any())
            {
                ModelState.AddModelError("CustomError", "Empty excel!");
                return View(importFileUpload);
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                foreach (Ingredient importedIngredient in importedIngredients)
                {
                    if (importedIngredient.Id == Guid.Empty)
                    {
                        Guid? existingId = FindIngredientId(importedIngredient.Name, importedIngredient.Category);
    
                        importedIngredient.Id = (existingId == null ? Guid.NewGuid() : existingId.Value);
                    }

                    if (IngredientExists(importedIngredient.Id))
                    {
                        _context.Update(importedIngredient);
                    }
                    else
                    {
                        _context.Add(importedIngredient);
                    }

                    await _context.SaveChangesAsync();
                }

                // Commit transaction if all commands succeed, transaction will auto-rollback
                // when disposed if either commands fails
                transaction.Commit();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
