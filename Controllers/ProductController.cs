using AutoMapper;
using ELabel.Data;
using ELabel.Models;
using ELabel.ViewModels;
using Ganss.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELabel.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
            }

            var query = await _context.Product
                                      .AsNoTracking()
                                      .ToListAsync();

            return View(_mapper.Map<IEnumerable<WineProductDetailsDto>>(query));
                         
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<WineProductDetailsDto>(product));
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Volume,WineVintage,WineType,WineStyle,WineAppellation,Ean")] WineProductCreateDto wineProductCreateDto)
        {
            if (ModelState.IsValid)
            {
                Product product = _mapper.Map<Product>(wineProductCreateDto);
                product.Id = Guid.NewGuid();
                product.Kind = ProductKind.Wine;

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wineProductCreateDto);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            WineProductEditDto wineProductEditDto = _mapper.Map<WineProductEditDto>(product);

            return View(wineProductEditDto);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Volume,WineVintage,WineType,WineStyle,WineAppellation,Ean")] WineProductEditDto wineProductEditDto)
        {
            if (id != wineProductEditDto.Id)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _mapper.Map<WineProductEditDto,Product>(wineProductEditDto, product);

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<WineProductDetailsDto>(product));
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
          return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: Product/Export
        public async Task<IActionResult> Export()
        {
            var products = await _context.Product.OrderBy(e => e.Name).AsNoTracking().ToListAsync();

            byte[] byteArray;
            var excel = new ExcelMapper();
            using (MemoryStream stream = new MemoryStream())
            {
                excel.Save(stream, products, "Products");

                byteArray = stream.ToArray();
            }

            return File(byteArray, "application/xlsx", $"e-label-products.xlsx");
        }

        // GET: Product/Import
        public IActionResult Import()
        {
            return View();
        }

        // POST: Analysis/Import
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

            IEnumerable<Product> importedProducts;

            using (var memoryStream = new MemoryStream())
            {
                await importFileUpload.File.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                //StreamReader textReader = new StreamReader(memoryStream);

                try
                {
                    importedProducts = new ExcelMapper(memoryStream).Fetch<Product>();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("CustomError", e.Message);
                    return View(importFileUpload);
                }
            }

            if (importedProducts == null || !importedProducts.Any())
            {
                ModelState.AddModelError("CustomError", "Empty excel!");
                return View(importFileUpload);
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                foreach (Product importedProduct in importedProducts)
                {
                    if (importedProduct.Id != Guid.Empty && ProductExists(importedProduct.Id))
                        _context.Update(importedProduct);
                    else
                    {
                        importedProduct.Id = Guid.NewGuid();
                        _context.Add(importedProduct);
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
