using AutoMapper;
using ELabel.Data;
using ELabel.Extensions;
using ELabel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Net.Codecrete.QrCodeGenerator;
using System.Text;

namespace ELabel.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class ImageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Image/Download/5
        [ResponseCache(Duration = 60*60*24*30)]
        public async Task<IActionResult> Download(Guid? id)
        {
            if (id == null || _context.Image == null)
            {
                return NotFound();
            }

            var image = await _context.Image.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (image == null)
            {
                return NotFound();
            }

            byte[]? byteArray = ImageHelper.ConvertFromDataUrl(image.DataUrl);

            if (byteArray == null)
            {
                return NoContent();
            }

            string fileDownloadName = $"{image.Id}-{image.Width}x{image.Height}.{image.ContentType.Split('/').Last()}";

            return File(byteArray, image.ContentType, fileDownloadName);
        }

    }
}
