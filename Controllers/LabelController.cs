using AutoMapper;
using ELabel.Data;
using ELabel.Models;
using ELabel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
        public async Task<IActionResult> Product(Guid? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                                        .Include(p => p.Image)
                                        .Include(p => p.ProductIngredients.OrderBy(pi => pi.Order))
                                        .ThenInclude(pi => pi.Ingredient)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            LabelDto labelDto = _mapper.Map<LabelDto>(product);

            ViewData["ProducerName"] = _producer.Name;
            return View(labelDto);
        }

    }
}
