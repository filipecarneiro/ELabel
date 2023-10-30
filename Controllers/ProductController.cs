using AutoMapper;
using ELabel.Data;
using ELabel.Models;
using ELabel.ViewModels;
using Ganss.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;

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
            var products = await _context.Product.AsNoTracking().OrderBy(p => p.Name).ToListAsync();
            var images = await _context.Image.AsNoTracking().OrderBy(p => p.Id).ToListAsync();

            byte[] byteArray;
            var excel = new ExcelMapper();
            excel.IgnoreNestedTypes = true;
            excel.Ignore<Image>(p => p.Content);

            using (MemoryStream stream = new MemoryStream())
            {
                excel.Save(Stream.Null, products, "Products");
                excel.Save(stream, images, "Images");

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

            const int MinWidth = 100;
            const int MaxWidth = 500;
            const int MinHeight = 100;
            const int MaxHeight = 500;
            const string mimeType = "image/webp";

            using (MemoryStream memoryStream = new MemoryStream())
            {
                await imageFileUpload.File.CopyToAsync(memoryStream);

                try
                {
                    // Resize image

                    memoryStream.Position = 0;
                    using (SKBitmap sourceBitmap = SKBitmap.Decode(memoryStream))
                    { 
                        int width = sourceBitmap.Width;
                        int height = sourceBitmap.Height;

                        if (width < MinWidth || height < MinHeight)
                        {
                            ModelState.AddModelError("CustomError", $"Image is too small ({width}x{height})! Chose an image with, at least, {MinWidth}x{MinHeight} pixels.");
                            return View(imageFileUpload);
                        }

                        if (width > MaxWidth)
                        {
                            double ratio = height / (double)width;
                            width = MaxWidth;
                            height = (int)(width * ratio);
                        }
                        if (height > MaxHeight)
                        {
                            double ratio = width / (double)height;
                            height = MaxHeight;
                            width = (int)(height * ratio);
                        }

                        using SKBitmap scaledBitmap = sourceBitmap.Resize(new SKImageInfo(width, height), SKFilterQuality.High);
                        using SKImage scaledImage = SKImage.FromBitmap(scaledBitmap);
                        using SKData data = scaledImage.Encode(SKEncodedImageFormat.Webp, 75); // mimeType

                        // Delete existing image

                        await _context.Image.Where(i => i.ProductId == id).ExecuteDeleteAsync();

                        // Add new image to database
                        
                        Image image = new Image()
                        {
                            Id = Guid.NewGuid(),
                            Content = data.ToArray(),
                            ContentType = mimeType,
                            Width = width,
                            Height = height,
                            ProductId = id
                        };

                        _context.Add(image);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("CustomError", e.Message);
                    return View(imageFileUpload);
                }
            }

            return RedirectToAction(nameof(Details), new { id });
        }

    }
}
