using AutoMapper;
using ELabel.Data;
using ELabel.Extensions;
using ELabel.Models;
using ELabel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Net.Codecrete.QrCodeGenerator;
using System.Text;

namespace ELabel.Controllers
{
    [AllowAnonymous]
    public class LabelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly Producer _producer;

        public LabelController(ApplicationDbContext context, IMapper mapper, IOptions<Producer> producerConfiguration)
        {
            _context = context;
            _mapper = mapper;
            _producer = producerConfiguration.Value;
        }

        // GET: Label/Product/5
        [Route("Label/Product/{id?}")]
        public async Task<IActionResult> Product(Guid? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            LabelDto? labelDto = await GetLabelAsync(id.Value);

            if (labelDto == null)
            {
                return NotFound();
            }

            ViewData["ProducerName"] = _producer.Name;
            return View(labelDto);
        }

        // GET: l/code
        [Route("l/{code?}")]
        [Route("Label/ProductCode/{code?}")]
        public async Task<IActionResult> ProductCode(string code)
        {
            if (code == null || _context.Product == null)
            {
                return NotFound();
            }

            Guid? id = FindProductId(code);

            if (id == null)
            {
                return NotFound();
            }

            LabelDto? labelDto = await GetLabelAsync(id.Value);

            ViewData["ProducerName"] = _producer.Name;
            return View("Product", labelDto);
        }

        // GET: Label/QRCode/5
        [Route("Label/QRCode/{code?}")]
        public IActionResult QRCode([FromRoute] string code, [FromQuery] string format = "svg")
        {
            if (code == null || _context.Product == null)
            {
                return NotFound();
            }

            string baseUrl = UrlHelper.GetBaseUrl(Request);
            var content = UrlHelper.GetQrCodeUrl(baseUrl, code, format);

            // Generate QrCode
            var qr = QrCode.EncodeText(content, QrCode.Ecc.Medium);

            byte[] byteArray;

            // PNG

            if (format.ToLower() == "png")
            {
                byteArray = qr.ToPng(10, 4);

                return File(byteArray, "image/png", $"qrcode-{code}.png");
            }

            // SVG

            byteArray = Encoding.UTF8.GetBytes(qr.ToSvgString(4));

            return File(byteArray, "text/svg", $"qrcode-{code}.svg");
        }

        private Guid? FindProductId(string? code)
        {
            Guid? id = _context.Product.Where(p => p.Sku == code).AsNoTracking().FirstOrDefault()?.Id;

            if (id is not null)
                return id;

            ulong ean;
            if (ulong.TryParse(code, out ean))
            { 
                id = _context.Product.Where(p => p.Ean == ean).AsNoTracking().FirstOrDefault()?.Id;

                if (id is not null)
                    return id;
            }

            Guid guid;
            if (Guid.TryParse(code, out guid))
            {
                id = _context.Product.Where(p => p.Id == guid).AsNoTracking().FirstOrDefault()?.Id;

                if (id is not null)
                    return id;
            }

            return null;
        }

        private async Task<LabelDto?> GetLabelAsync(Guid id)
        {
            var product = await _context.Product
                            .Include(p => p.Image)
                            .Include(p => p.ProductIngredients.OrderBy(pi => pi.Order))
                            .ThenInclude(pi => pi.Ingredient)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return null;
            }

            LabelDto labelDto = _mapper.Map<LabelDto>(product);

            return labelDto;
        }

    }
}
