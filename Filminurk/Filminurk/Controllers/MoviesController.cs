using Filminurk.Data;
using Filminurk.Models.Movies;
using Filminurk.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        public MoviesController (FilminurkTARpe24Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var result = _context.Movies.Select(x => new MoviesIndexViewModel
            {
                ID = x.ID,
                Title = x.Title,
                FirstPublished = x.FirstPublished,
                Genre = x.Genre,
                CurrentRating = x.CurrentRating,
                Warnings = x.Warnings,
                
            });
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            MoviesCreateViewModel result = new();
            return View("Create",result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MoviesCreateViewModel vm)
        {
            var dto = new MoviesDTO()
            {
                ID = vm.ID,
                Title = vm.Title,
                FirstPublished = vm.FirstPublished,
                Genre = vm.Genre,
                CurrentRating = vm.CurrentRating,
                Warnings = vm.Warnings,
                Actors = vm.Actors,
                EntryCreatedAt = vm.EntryCreatedAt,
                EntryModifiedAt = vm.EntryModifiedAt,
                Director = vm.Director,
                Tagline = vm.Tagline,
                Description = vm.Description,
            };
            var result = await _context.Create(dto);
            if (result == null)
            {
                RedirectToAction(nameof(Index));
            }
            RedirectToAction(nameof(Index));
        }
    }
}
