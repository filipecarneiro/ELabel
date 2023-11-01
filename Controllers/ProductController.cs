using AutoMapper;
using ELabel.Data;
using ELabel.Extensions;
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
                                        .Include(p => p.Image)
                                        .AsNoTracking() 
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            WineProductDetailsDto wineProductDetailsDto = _mapper.Map<WineProductDetailsDto>(product);

            return View(wineProductDetailsDto);
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
        public async Task<IActionResult> Create([Bind("Name,Volume,WineVintage,WineType,WineStyle,WineAppellation,Country,Sku,Ean")] WineProductCreateDto wineProductCreateDto)
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Volume,WineVintage,WineType,WineStyle,WineAppellation,Country,Sku,Ean")] WineProductEditDto wineProductEditDto)
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

        private bool ImageExists(Guid id)
        {
            return (_context.Image?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: Product/Export
        public async Task<IActionResult> Export()
        {
            var query = await _context.Product
                                      .Include(p => p.Image)
                                      .AsNoTracking()
                                      .OrderBy(p => p.Name)
                                      .ToListAsync();

            List<WineProductDetailsDto> products = _mapper.Map<List<WineProductDetailsDto>>(query);

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

            IEnumerable<WineProductDetailsDto> importedProducts;

            using (var memoryStream = new MemoryStream())
            {
                await importFileUpload.File.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                try
                {
                    ExcelMapper excelMapper = new ExcelMapper(memoryStream);
                    importedProducts = excelMapper.Fetch<WineProductDetailsDto>("Products");
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
                foreach (WineProductDetailsDto importedProduct in importedProducts)
                {
                    Product product = _mapper.Map<Product>(importedProduct);

                    if (product.Id != Guid.Empty && ProductExists(product.Id))
                    {
                        _context.Update(product);
                    }
                    else
                    {
                        product.Id = Guid.NewGuid();
                        _context.Add(product);
                    }

                    await _context.SaveChangesAsync();

                    if(importedProduct.Image != null && !String.IsNullOrEmpty(importedProduct.Image.DataUrl))
                    {
                        bool newImage = false;
                        var image = await _context.Image.FirstOrDefaultAsync(m => m.ProductId == product.Id);

                        if (image == null)
                        {
                            newImage = true;
                            image = new Image() {
                                Id = Guid.NewGuid(),
                                ProductId = product.Id,
                                ContentType = String.Empty,
                                Content = new byte[0]
                            };
                        }

                        byte[]? imageByteBuffer = ImageHelper.ConvertFromDataUrl(importedProduct.Image.DataUrl);

                        if (imageByteBuffer == null)
                            continue;

                        OptimizedImage? optimizedImage = ImageHelper.Optimize(imageByteBuffer, ImageFileUpload.MaxWidth, ImageFileUpload.MaxHeight, ImageFileUpload.Quality);

                        if (optimizedImage == null || optimizedImage.Width < ImageFileUpload.MinWidth || optimizedImage.Height < ImageFileUpload.MinHeight)
                            continue;

                        image.ContentType = optimizedImage.ContentType;
                        image.Content = optimizedImage.Content;
                        image.Width = optimizedImage.Width;
                        image.Height = optimizedImage.Height;

                        if (newImage)
                            _context.Add(image);
                        else
                            _context.Update(image);
                        await _context.SaveChangesAsync();
                    }
                }

                // Commit transaction if all commands succeed, transaction will auto-rollback
                // when disposed if either commands fails
                transaction.Commit();
            }

            return RedirectToAction(nameof(Index));
        }

        // Get: Product/DeleteImage/5
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            if (_context.Image == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Image'  is null.");
            }

            await _context.Image.Where(i => i.ProductId == id).ExecuteDeleteAsync();

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: Product/ChangeImage
        public IActionResult ChangeImage(Guid id)
        {
            ImageFileUpload imageFileUpload = new ImageFileUpload(){
                ProductId = id
            };

            return View(imageFileUpload);
        }

        // POST: Analysis/ChangeImage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeImage(Guid id, [Bind("File,ProductId")] ImageFileUpload imageFileUpload)
        {
            if (id != imageFileUpload.ProductId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(imageFileUpload);
            }

            if (imageFileUpload.File == null || imageFileUpload.File.Length == 0)
            {
                ModelState.AddModelError("CustomError", "Empty file!");
                return View(imageFileUpload);
            }

            byte[] byteArray;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                await imageFileUpload.File.CopyToAsync(memoryStream);

                memoryStream.Position = 0;
                byteArray = memoryStream.ToArray();
            }

            try
            {
                // Resize image

                OptimizedImage? optimizedImage = ImageHelper.Optimize(byteArray, ImageFileUpload.MaxWidth, ImageFileUpload.MaxHeight, ImageFileUpload.Quality);

                if(optimizedImage == null)
                {
                    ModelState.AddModelError("CustomError", $"Invalid image format! Try another file.");
                    return View(imageFileUpload);
                }

                if (optimizedImage.Width < ImageFileUpload.MinWidth || optimizedImage.Height < ImageFileUpload.MinHeight)
                {
                    ModelState.AddModelError("CustomError", $"Image is too small ({optimizedImage.Width}x{optimizedImage.Height})! Chose an image with, at least, {ImageFileUpload.MinWidth}x{ImageFileUpload.MinHeight} pixels.");
                    return View(imageFileUpload);
                }

                // Delete existing image

                await _context.Image.Where(i => i.ProductId == id).ExecuteDeleteAsync();

                // Add new image to database
                        
                Image image = new Image()
                {
                    Id = Guid.NewGuid(),
                    Content = optimizedImage.Content,
                    ContentType = optimizedImage.ContentType,
                    Width = optimizedImage.Width,
                    Height = optimizedImage.Height,
                    ProductId = id
                };

                _context.Add(image);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("CustomError", e.Message);
                return View(imageFileUpload);
            }

            return RedirectToAction(nameof(Details), new { id });
        }

    }
}
