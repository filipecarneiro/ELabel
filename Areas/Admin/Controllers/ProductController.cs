using AutoMapper;
using ELabel.Data;
using ELabel.Extensions;
using ELabel.Models;
using ELabel.ViewModels;
using Ganss.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ELabel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize()]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly Producer _producer;

        public ProductController(ApplicationDbContext context, IMapper mapper, IOptions<Producer> producerConfiguration)
        {
            _context = context;
            _mapper = mapper;
            _producer = producerConfiguration.Value;
        }

        // GET: Product
        public IActionResult Index(string filterText = "", string sortOrder = "")
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
            }

            ViewBag.SortParmName = sortOrder == "Name" ? "-Name" : "Name";
            ViewBag.SortParmVolume = sortOrder == "Volume" ? "-Volume" : "Volume";
            ViewBag.SortParmWineVintage = sortOrder == "WineVintage" ? "-WineVintage" : "WineVintage";
            ViewBag.SortParmWineType = sortOrder == "WineType" ? "-WineType" : "WineType";
            ViewBag.SortParmWineSugarContent = sortOrder == "WineSugarContent" ? "-WineSugarContent" : "WineSugarContent";
            ViewBag.SortParmWineAppelation = sortOrder == "WineAppelation" ? "-WineAppelation" : "WineAppelation";
            ViewBag.SortParmSku = sortOrder == "Sku" ? "-Sku" : "Sku";

            var query = _context.Product
                                .AsNoTracking()
                                .OrderByDescending(i => i.WineInformation.Vintage)
                                .ThenBy(i => i.Name)
                                .ThenBy(i => i.Volume)
                                .AsQueryable()
                                .AsEnumerable();

            // Filter

            if (!string.IsNullOrWhiteSpace(filterText))
            {
                filterText = filterText.Trim().ToLower();
                float filterFloat;
                ushort filterUShort;

                query = query.Where(p => p.Name.Contains(filterText, StringComparison.InvariantCultureIgnoreCase) ||
                                          p.Volume != null && float.TryParse(filterText, out filterFloat) && p.Volume == filterFloat ||
                                          p.WineInformation.Vintage != null && ushort.TryParse(filterText, out filterUShort) && p.WineInformation.Vintage == filterUShort ||
                                          p.WineInformation.Type != null && EnumHelper.GetDisplayName(p.WineInformation.Type)?.ToLower() == filterText ||
                                          p.WineInformation.SugarContent != null && EnumHelper.GetDisplayName(p.WineInformation.SugarContent)?.ToLower() == filterText ||
                                          p.WineInformation.Appellation != null && p.WineInformation.Appellation.Contains(filterText, StringComparison.InvariantCultureIgnoreCase) ||
                                          p.Logistics.Sku != null && p.Logistics.Sku.Contains(filterText, StringComparison.InvariantCultureIgnoreCase)
                                         
                                   );
            }

            // Sort

            switch (sortOrder)
            {
                default:
                    query = query.OrderByDescending(p => p.WineInformation.Vintage).ThenBy(p => p.Name).ThenBy(p => p.Volume);
                    break;
                case "Name":
                    query = query.OrderBy(p => p.Name).ThenBy(p => p.Volume).ThenBy(p => p.WineInformation.Vintage);
                    break;
                case "-Name":
                    query = query.OrderByDescending(p => p.Name).ThenByDescending(p => p.Volume).ThenByDescending(p => p.WineInformation.Vintage);
                    break;
                case "Volume":
                    query = query.OrderBy(p => p.Volume).ThenBy(p => p.Name).ThenBy(p => p.Volume).ThenBy(p => p.WineInformation.Vintage);
                    break;
                case "-Volume":
                    query = query.OrderByDescending(p => p.Volume).ThenBy(p => p.Name).ThenBy(p => p.Volume).ThenBy(p => p.WineInformation.Vintage);
                    break;
                case "WineVintage":
                    query = query.OrderBy(p => p.WineInformation.Vintage).ThenBy(p => p.Name).ThenBy(p => p.Volume);
                    break;
                case "-WineVintage":
                    query = query.OrderByDescending(p => p.WineInformation.Vintage).ThenBy(p => p.Name).ThenBy(p => p.Volume);
                    break;
                case "WineType":
                    query = query.OrderBy(p => p.WineInformation.Type).ThenBy(p => p.Name).ThenBy(p => p.Volume).ThenBy(p => p.WineInformation.Vintage);
                    break;
                case "-WineType":
                    query = query.OrderByDescending(p => p.WineInformation.Type).ThenBy(p => p.Name).ThenBy(p => p.Volume).ThenBy(p => p.WineInformation.Vintage);
                    break;
                case "WineSugarContent":
                    query = query.OrderBy(p => p.WineInformation.SugarContent).ThenBy(p => p.Name).ThenBy(p => p.Volume).ThenBy(p => p.WineInformation.Vintage);
                    break;
                case "-WineSugarContent":
                    query = query.OrderByDescending(p => p.WineInformation.SugarContent).ThenBy(p => p.Name).ThenBy(p => p.Volume).ThenBy(p => p.WineInformation.Vintage);
                    break;
                case "WineAppelation":
                    query = query.OrderBy(p => p.WineInformation.Appellation).ThenBy(p => p.Name).ThenBy(p => p.Volume).ThenBy(p => p.WineInformation.Vintage);
                    break;
                case "-WineAppelation":
                    query = query.OrderByDescending(p => p.WineInformation.Appellation).ThenBy(p => p.Name).ThenBy(p => p.Volume).ThenBy(p => p.WineInformation.Vintage);
                    break;
                case "Sku":
                    query = query.OrderBy(p => p.Logistics.Sku).ThenBy(p => p.Name).ThenBy(p => p.Volume).ThenBy(p => p.WineInformation.Vintage);
                    break;
                case "-Sku":
                    query = query.OrderByDescending(p => p.Logistics.Sku).ThenBy(p => p.Name).ThenBy(p => p.Volume).ThenBy(p => p.WineInformation.Vintage);
                    break;
            }

            ViewBag.FilterText = filterText;

            ViewBag.UniqueWineVintages = _context.Product.AsNoTracking().Select(p => p.WineInformation.Vintage).Where(i => i.HasValue).Distinct().OrderByDescending(i => i).ToList();
            ViewBag.UniqueWineTypes = _context.Product.AsNoTracking().Select(p => p.WineInformation.Type).Where(i => i.HasValue).Distinct().OrderBy(i => i).ToList();
            ViewBag.UniqueWineSugarContents = _context.Product.AsNoTracking().Select(p => p.WineInformation.SugarContent).Where(i => i.HasValue).Distinct().OrderBy(i => i).ToList();
            ViewBag.UniqueWineAppellations = _context.Product.AsNoTracking().Select(p => p.WineInformation.Appellation).Where(i => !string.IsNullOrWhiteSpace(i)).Distinct().OrderBy(i => i).ToList();

            return View(_mapper.Map<IEnumerable<WineProductDetailsDto>>(query.ToList()));
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                                        .Include(p => p.Images.OrderBy(i => i.Width))
                                        .Include(p => p.ProductIngredients.OrderBy(pi => pi.Order))
                                        .ThenInclude(pi => pi.Ingredient)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            WineProductDetailsDto wineProductDetailsDto = _mapper.Map<WineProductDetailsDto>(product);

            string baseUrl = UrlHelper.GetBaseUrl(Request);
            string labelUrl = product.GetAbsoluteLabelUrl(baseUrl, useRedirectUrl: true, useExternalShortUrl: true);
            string filename = "QR Code " + product.GetTitle();

            QrCodeInfo qrCodeInfo = new QrCodeInfo( labelUrl, filename);
            wineProductDetailsDto.QrCodeInfo = qrCodeInfo;

            ViewBag.ProducerName = _producer.Name;

            return View(wineProductDetailsDto);
        }

        // GET: Product/Preview/5
        [AllowAnonymous]
        public async Task<IActionResult> Preview(Guid? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            string? url = product.GetRelativeLabelUrl();

            if (string.IsNullOrWhiteSpace(url))
            {
                return NotFound();
            }

            return Redirect(url);
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
        public async Task<IActionResult> Create([Bind("Name,Volume,Weight,Kind,WineInformation")] WineProductCreateDto wineProductCreateDto)
        {
            if (ModelState.IsValid)
            {
                Product product = _mapper.Map<Product>(wineProductCreateDto);
                product.Id = Guid.NewGuid();
                product.Kind = ProductKind.Wine;
                //product.NutritionInformation = new NutritionInformation();

                _context.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Edit), new { id = product.Id });
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

            var product = await _context.Product
                                        .Include(p => p.Images)
                                        .Include(p => p.ProductIngredients.OrderBy(pi => pi.Order))
                                        //.ThenInclude(pi => pi.Ingredient)
                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            WineProductEditDto wineProductEditDto = _mapper.Map<WineProductEditDto>(product);

            ViewBag.Ingredients = GetAvailableIngredientsList();
            ViewBag.ProducerName = _producer.Name;
            return View(wineProductEditDto);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Brand,Volume,Weight,Kind,WineInformation,ProductIngredients,PackagingGases,NutritionInformation,ResponsibleConsumption,Certifications,FoodBusinessOperator,Logistics,Portability")] WineProductEditDto wineProductEditDto)
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

            if (wineProductEditDto.Portability.ExternalShortUrl != null && !Uri.IsWellFormedUriString(wineProductEditDto.Portability.ExternalShortUrl, UriKind.Absolute))
            {
                ModelState.AddModelError("CustomError", "External short link is not an absolute url! Write a link with 'https://...'");
            }

            if (wineProductEditDto.Portability.RedirectUrl != null && !Uri.IsWellFormedUriString(wineProductEditDto.Portability.RedirectUrl, UriKind.Absolute))
            {
                ModelState.AddModelError("CustomError", "Redirect link is not an absolute url! Write a link with 'https://...'");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update Product

                    _mapper.Map(wineProductEditDto, product);

                    _context.Update(product);
                    await _context.SaveChangesAsync();

                    // Update Product Ingredients

                    var productIngredients = await _context.ProductIngredient
                                                           .Where(pi => pi.ProductId == product.Id)
                                                           .ToListAsync();

                    //_context.ChangeTracker.DetectChanges();
                    //Console.WriteLine(_context.ChangeTracker.DebugView.LongView);

                    productIngredients.ForEach(pi =>
                    {
                        _context.Entry(pi).State = EntityState.Deleted;
                    });

                    short order = 0;
                    foreach (ProductIngredientDto productIngredientDto in wineProductEditDto.ProductIngredients.OrderBy(pi => pi.Order))
                    {
                        bool duplicated = wineProductEditDto.ProductIngredients.Where(pi => pi.ToDelete == false && pi.IngredientId == productIngredientDto.IngredientId).Count() > 1;
                        if (duplicated)
                        {
                            ModelState.AddModelError("CustomError", "Duplicated ingredient! Please remove the repeated ingredient from the list.");

                            ViewBag.Ingredients = GetAvailableIngredientsList();
                            return View(wineProductEditDto);
                        }

                        ProductIngredient? auxProductIngredient = productIngredients
                                                                    .Where(pi => pi.Id == productIngredientDto.Id && pi.ProductId == product.Id).FirstOrDefault();

                        if (productIngredientDto.ToDelete)
                        {
                            if (auxProductIngredient == null)
                                continue;

                            _mapper.Map(productIngredientDto, auxProductIngredient);

                            _context.Entry(auxProductIngredient).State = EntityState.Deleted;
                            _context.Remove(auxProductIngredient);

                            continue;
                        }

                        if (auxProductIngredient == null)
                        {
                            auxProductIngredient = new ProductIngredient()
                            {
                                Id = Guid.NewGuid(),
                                ProductId = product.Id,
                                IngredientId = productIngredientDto.IngredientId
                            };

                            auxProductIngredient.Order = ++order;

                            _context.Entry(auxProductIngredient).State = EntityState.Added;
                            _context.Add(auxProductIngredient);

                        }
                        else
                        {
                            _mapper.Map(productIngredientDto, auxProductIngredient);

                            auxProductIngredient.Order = ++order;

                            _context.Entry(auxProductIngredient).State = EntityState.Modified;
                            _context.Update(auxProductIngredient);
                        }
                    }

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

            ViewBag.Ingredients = GetAvailableIngredientsList();
            return View(wineProductEditDto);
        }

        // GET: Product/Duplicate
        public async Task<IActionResult> Duplicate(Guid? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                                        .Include(p => p.Images)
                                        .Include(p => p.ProductIngredients.OrderBy(pi => pi.Order))
                                        //.ThenInclude(pi => pi.Ingredient)
                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            Product newProduct = product.DeepCopy();
            newProduct.Name = "Copy of " + product.Name;
            newProduct.Logistics.Sku = null;
            //newProduct.Logistics.Ean = null;
            newProduct.Portability.ExternalShortUrl = null;

            _context.Add(newProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = newProduct.Id });
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

        private Guid? FindProductId(string name, float? volume, ushort? wineVintage)
        {
            return _context.Product.Where(e => e.Name == name && e.Volume == volume && e.WineInformation.Vintage == wineVintage).AsNoTracking().FirstOrDefault()?.Id;
        }

        private SelectList GetAvailableIngredientsList()
        {
            var ingredients = _context.Ingredient
                                .AsNoTracking()
                                .Select(i => new
                                {
                                    i.Id,
                                    Name = i.Name + " (" + EnumHelper.GetDisplayName(i.Category) + ")"
                                })
                                .ToList();

            return new SelectList(ingredients, "Id", "Name");
        }

        private bool ImageExists(Guid id)
        {
            return (_context.Image?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: Product/Export
        public async Task<IActionResult> Export()
        {
            var query = await _context.Product
                                      .Include(p => p.Images.Where(i => i.PixelDensity == Image.ExportPixelDensity))
                                      .Include(p => p.ProductIngredients.OrderBy(pi => pi.Order))
                                        //.ThenInclude(pi => pi.Ingredient)
                                      .AsNoTracking()
                                      .OrderBy(p => p.Name)
                                      .ToListAsync();

            List<ProductExcelDto> products = _mapper.Map<List<ProductExcelDto>>(query);

            // TODO: Use UrlResolver, with Dependency Injection, to add the current Url
            string baseUrl = UrlHelper.GetBaseUrl(Request);
            foreach (ProductExcelDto product in products)
            {
                if(product.Code is not null)
                    product.LabelUrl = Product.GetAbsoluteLabelUrl(baseUrl, product.Code);
            }

            byte[] byteArray;
            var excel = new ExcelMapper();

            using (MemoryStream stream = new MemoryStream())
            {
                excel.Save(stream, products, "Products");

                byteArray = stream.ToArray();
            }

            return File(byteArray, "application/xlsx", $"elabel-products.xlsx");
        }

        // GET: Product/Import
        public IActionResult Import()
        {
            return View();
        }

        // POST: Product/Import
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

            IEnumerable<ProductExcelDto> importedProducts;

            using (var memoryStream = new MemoryStream())
            {
                await importFileUpload.File.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                try
                {
                    ExcelMapper excelMapper = new ExcelMapper(memoryStream);
                    importedProducts = excelMapper.Fetch<ProductExcelDto>("Products");
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
                foreach (ProductExcelDto importedProduct in importedProducts)
                {
                    Product product = _mapper.Map<Product>(importedProduct);
                    product.Images.Clear();
                    product.ProductIngredients.Clear();

                    if (product.Id == Guid.Empty)
                    {
                        Guid? existingId = FindProductId(product.Name, product.Volume, product.WineInformation.Vintage);

                        product.Id = existingId == null ? Guid.NewGuid() : existingId.Value;
                    }

                    if (ProductExists(product.Id))
                    {
                        _context.Update(product);
                    }
                    else
                    {
                        _context.Add(product);
                    }

                    await _context.SaveChangesAsync();

                    if(importedProduct.IngredientIdList != null)
                    {
                        // Delete existing Product Ingredients

                        await _context.ProductIngredient.Where(i => i.ProductId == product.Id).ExecuteDeleteAsync();

                        // Add Product Ingredients to database

                        short order = 0;
                        foreach (Guid ingredientId in importedProduct.IngredientIdList)
                        {
                            ProductIngredient productIngredient = new ProductIngredient()
                            {
                                Id = Guid.NewGuid(),
                                ProductId = product.Id,
                                IngredientId = ingredientId,
                                Order = ++order
                            };

                            _context.Add(productIngredient);
                        }

                        await _context.SaveChangesAsync();
                    }

                    if (importedProduct.ImageDataUrl != null && !string.IsNullOrEmpty(importedProduct.ImageDataUrl))
                    {
                        byte[]? imageByteBuffer = ImageHelper.ConvertFromDataUrl(importedProduct.ImageDataUrl);

                        if (imageByteBuffer == null)
                            continue;

                        int? biggerSideLenght = ImageHelper.GetBiggerSideLenght(imageByteBuffer);

                        if (biggerSideLenght == null || biggerSideLenght < Image.MinBiggerSideLenght)
                            continue;

                        OptimizedImage?[] optimizedImages = ImageHelper.OptimizedSet(imageByteBuffer, Image.DefaultSizes, Image.DefaultQuality);

                        if (optimizedImages == null)
                            continue;

                        // Delete existing images

                        await _context.Image.Where(i => i.ProductId == product.Id).ExecuteDeleteAsync();

                        // Add images to database

                        foreach(OptimizedImage? optimizedImage in optimizedImages)
                        {
                            if (optimizedImage == null)
                                continue;

                            Image image = new Image()
                            {
                                Id = Guid.NewGuid(),
                                ProductId = product.Id,
                                Content = optimizedImage.Content,
                                ContentType = optimizedImage.ContentType,
                                Width = optimizedImage.Width,
                                Height = optimizedImage.Height,
                                PixelDensity = optimizedImage.PixelDensity
                            };

                            _context.Add(image);
                        }

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
            ImageFileUpload imageFileUpload = new ImageFileUpload()
            {
                ProductId = id
            };

            ViewData["ProductTitle"] = _context.Product.Find(id)?.GetTitle();
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

            string? ProductTitle = _context.Product.Find(id)?.GetTitle();

            if (!ModelState.IsValid)
            {
                ViewData["ProductTitle"] = ProductTitle;
                return View(imageFileUpload);
            }

            if (imageFileUpload.File == null || imageFileUpload.File.Length == 0)
            {
                ModelState.AddModelError("CustomError", "Empty file!");
                ViewData["ProductTitle"] = ProductTitle;
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
                // Test image

                int? biggerSideLenght = ImageHelper.GetBiggerSideLenght(byteArray);

                if (biggerSideLenght == null)
                {
                    ModelState.AddModelError("CustomError", $"Invalid image format! Try another file.");
                    ViewData["ProductTitle"] = ProductTitle;
                    return View(imageFileUpload);
                }

                if (biggerSideLenght < Image.MinBiggerSideLenght)
                {
                    ModelState.AddModelError("CustomError", $"Image is too small (The bigger side has {biggerSideLenght}px)! Chose an image with, at least, {Image.MinBiggerSideLenght} pixels in width or height.");
                    ViewData["ProductTitle"] = ProductTitle;
                    return View(imageFileUpload);
                }

                // Resize image

                OptimizedImage?[] optimizedImages = ImageHelper.OptimizedSet(byteArray, Image.DefaultSizes, Image.DefaultQuality);

                // Delete existing images

                await _context.Image.Where(i => i.ProductId == id).ExecuteDeleteAsync();

                // Add images to database

                foreach(OptimizedImage? optimizedImage in optimizedImages)
                {
                    if (optimizedImage == null)
                        continue;

                    Image image = new Image()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = id,
                        Content = optimizedImage.Content,
                        ContentType = optimizedImage.ContentType,
                        Width = optimizedImage.Width,
                        Height = optimizedImage.Height,
                        PixelDensity = optimizedImage.PixelDensity
                    };

                    _context.Add(image);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("CustomError", e.Message);
                ViewData["ProductTitle"] = ProductTitle;
                return View(imageFileUpload);
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIngredient([Bind("ProductIngredients")] WineProductEditDto wineProductEditDto)
        {
            wineProductEditDto.ProductIngredients.Add(new ProductIngredientDto()
            {
                Id = Guid.Empty,
                ProductId = wineProductEditDto.Id,
                IngredientId = Guid.Empty,
                Order = (short)(wineProductEditDto.ProductIngredients.Count() + 1)
            });

            ViewBag.Ingredients = GetAvailableIngredientsList();
            return PartialView("ProductIngredients", wineProductEditDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveIngredient([Bind("ProductIngredients")] WineProductEditDto wineProductEditDto)
        {
            if (wineProductEditDto.ProductIngredients.Count == 0)
            {
                ViewBag.Ingredients = GetAvailableIngredientsList();
                return PartialView("ProductIngredients", wineProductEditDto);
            }

            wineProductEditDto.ProductIngredients.RemoveAt(wineProductEditDto.ProductIngredients.Count - 1);

            ViewBag.Ingredients = GetAvailableIngredientsList();
            return PartialView("ProductIngredients", wineProductEditDto);
        }

    }
}
