﻿using AutoMapper;
using ELabel.Data;
using ELabel.Extensions;
using ELabel.Models;
using ELabel.ViewModels;
using Ganss.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELabel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize()]
    public class IngredientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public IngredientController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Ingredient
        public IActionResult Index(string filterText = "", string sortOrder = "")
        {
            if (_context.Ingredient == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Ingredient'  is null.");
            }

            ViewBag.SortParmName = string.IsNullOrEmpty(sortOrder) ? "-Name" : "";
            ViewBag.SortParmCategory = sortOrder == "Category" ? "-Category" : "Category";
            ViewBag.SortParmAllergen = sortOrder == "Allergen" ? "-Allergen" : "Allergen";
            ViewBag.SortParmCustom = sortOrder == "Custom" ? "-Custom" : "Custom";

            var query = _context.Ingredient
                                .AsNoTracking()
                                .OrderBy(i => i.Name)
                                .ThenBy(i => i.Category)
                                .AsQueryable()
                                .AsEnumerable();

            // Filter

            if (!string.IsNullOrWhiteSpace(filterText))
            {
                filterText = filterText.Trim();

                query = query.Where(t => t.Name.Contains(filterText, StringComparison.InvariantCultureIgnoreCase) ||
                                          EnumHelper.GetDisplayName(t.Category)?.ToLower() == filterText.ToLower() ||
                                          "allergen" == filterText.ToLower() && t.Allergen == true ||
                                          "custom" == filterText.ToLower() && t.Custom == true
                                         
                                   );
            }

            // Sort

            switch (sortOrder)
            {
                default: // "Name":
                    query = query.OrderBy(t => t.Name).ThenBy(t => t.Id);
                    break;
                case "-Name":
                    query = query.OrderByDescending(t => t.Name).ThenByDescending(t => t.Id);
                    break;
                case "Category":
                    query = query.OrderBy(t => t.Category).ThenBy(t => t.Name).ThenBy(t => t.Id);
                    break;
                case "-Category":
                    query = query.OrderByDescending(t => t.Category).ThenByDescending(t => t.Name).ThenByDescending(t => t.Id);
                    break;
                case "Allergen":
                    query = query.OrderBy(t => t.Allergen).ThenBy(t => t.Name).ThenBy(t => t.Id);
                    break;
                case "-Allergen":
                    query = query.OrderByDescending(t => t.Allergen).ThenByDescending(t => t.Name).ThenByDescending(t => t.Id);
                    break;
                case "Custom":
                    query = query.OrderBy(t => t.Custom).ThenBy(t => t.Name).ThenBy(t => t.Id);
                    break;
                case "-Custom":
                    query = query.OrderByDescending(t => t.Custom).ThenByDescending(t => t.Name).ThenByDescending(t => t.Id);
                    break;
            }

            ViewBag.FilterText = filterText;
            return View(query.ToList());
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
            ingredient.Custom = true;

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
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Category,Allergen,Custom,LocalizableStrings,Id")] Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // ingredient.Custom = true;

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
            var query = await _context.Ingredient
                                      .AsNoTracking()
                                      .OrderBy(i => i.Name)
                                      .ThenBy(i => i.Category)
                                      .ToListAsync();

            List<IngredientExcelDto> ingredients = _mapper.Map<List<IngredientExcelDto>>(query);

            byte[] byteArray;
            var excelMapper = new ExcelMapper();

            using (MemoryStream stream = new MemoryStream())
            {
                excelMapper.Save(stream, ingredients, "Ingredients");

                byteArray = stream.ToArray();
            }

            return File(byteArray, "application/xlsx", $"elabel-ingredients.xlsx");
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

            IEnumerable<IngredientExcelDto> importedIngredients;

            using (var memoryStream = new MemoryStream())
            {
                await importFileUpload.File.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                try
                {
                    ExcelMapper excelMapper = new ExcelMapper(memoryStream);
                    importedIngredients = excelMapper.Fetch<IngredientExcelDto>("Ingredients");
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
                foreach (IngredientExcelDto importedIngredient in importedIngredients)
                {
                    Ingredient ingredient = _mapper.Map<Ingredient>(importedIngredient);

                    if (ingredient.Id == Guid.Empty)
                    {
                        Guid? existingId = FindIngredientId(ingredient.Name, ingredient.Category);

                        ingredient.Id = existingId == null ? Guid.NewGuid() : existingId.Value;
                    }

                    if (IngredientExists(ingredient.Id))
                    {
                        _context.Update(ingredient);
                    }
                    else
                    {
                        _context.Add(ingredient);
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
