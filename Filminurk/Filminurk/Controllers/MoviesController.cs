using Filminurk.Data;
using Filminurk.Models.Movies;
using Filminurk.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using Filminurk.Core.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using Filminurk.Core.Domain;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Logging.Abstractions;

namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IMovieServices _movieServices;
        public MoviesController(FilminurkTARpe24Context context, IMovieServices movieServices)
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
            MoviesCreateUpdateViewModel result = new();
            return View("CreateUpdate", result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MoviesCreateUpdateViewModel vm)
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

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var movie = await _movieServices.DetailsAsync(id);
            if(movie==null)
            {
                return NotFound();
            }
            var vm = new MoviesDetailsViewModel();
            vm.ID = movie.ID;
            vm.Title = movie.Title;
            vm.FirstPublished = movie.FirstPublished;
            vm.Genre = movie.Genre;
            vm.CurrentRating = movie.CurrentRating;
            vm.Warnings = movie.Warnings;
            vm.Actors = movie.Actors;
            vm.EntryCreatedAt = movie.EntryCreatedAt;
            vm.EntryModifiedAt = movie.EntryModifiedAt;
            vm.Director = movie.Director;
            vm.Tagline = movie.Tagline;
            vm.Description = movie.Description;
            return View(vm);
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var movie = await _movieServices.DetailsAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            var vm = new MoviesCreateUpdateViewModel();

            vm.ID = movie.ID;
            vm.Title = movie.Title;
            vm.FirstPublished = movie.FirstPublished;
            vm.Genre = movie.Genre;
            vm.CurrentRating = movie.CurrentRating;
            vm.Warnings = movie.Warnings;
            vm.Actors = movie.Actors;
            vm.EntryCreatedAt = movie.EntryCreatedAt;
            vm.EntryModifiedAt = movie.EntryModifiedAt;
            vm.Director = movie.Director;
            vm.Tagline = movie.Tagline;
            vm.Description = movie.Description;
            return View("CreateUpdate",vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(MoviesCreateUpdateViewModel vm)
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
            var result = await _movieServices.Update(dto);
            if(result == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid ID)
        {
            var movie = await _movieServices.DetailsAsync(ID);
            if (movie == null)
            {
                return NotFound();
            }
            var vm = new MovieDeleteViewModel();

            vm.ID = movie.ID;
            vm.Title = movie.Title;
            vm.FirstPublished = movie.FirstPublished;
            vm.Genre = movie.Genre;
            vm.CurrentRating = movie.CurrentRating;
            vm.Warnings = movie.Warnings;
            vm.Actors = movie.Actors;
            vm.EntryCreatedAt = movie.EntryCreatedAt;
            vm.EntryModifiedAt = movie.EntryModifiedAt;
            vm.Director = movie.Director;
            vm.Tagline = movie.Tagline;
            vm.Description = movie.Description;

            return View(vm);
            
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid ID)
        {
            var movie = await _movieServices.Delete(ID);
            if (movie == null) { return NotFound(); }
            return RedirectToAction(nameof(Index));

        } 
    }
}
