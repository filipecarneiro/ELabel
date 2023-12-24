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

        // GET: l/code
        [Route("l/{code?}")]
        public async Task<IActionResult> Page(string code)
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

            if (labelDto == null)
            {
                return NotFound();
            }

            if (labelDto.Portability != null && !string.IsNullOrWhiteSpace(labelDto.Portability.RedirectUrl))
            {
                return Redirect(labelDto.Portability.RedirectUrl);
            }

            ViewData["ProducerName"] = _producer.Name;
            return View(labelDto);
        }

        private Guid? FindProductId(string? code)
        {
            Guid? id = _context.Product.Where(p => p.Logistics.Sku == code).AsNoTracking().FirstOrDefault()?.Id;

            if (id is not null)
                return id;

            ulong ean;
            if (ulong.TryParse(code, out ean))
            { 
                id = _context.Product.Where(p => p.Logistics.Ean == ean).AsNoTracking().FirstOrDefault()?.Id;

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
