using Filminurk.Data;
using Filminurk.Models.Movies;
using Filminurk.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using Filminurk.Core.ServiceInterface;

namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IMovieServices _movieServices;
        public MoviesController (FilminurkTARpe24Context context, IMovieServices movieServices)
        {
            _context = context;
            _movieServices = movieServices;
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
            var result = await _movieServices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
