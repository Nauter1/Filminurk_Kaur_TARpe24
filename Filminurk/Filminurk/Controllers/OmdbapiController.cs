using Filminurk.Core.Domain;
using Filminurk.Core.Dto.OmdbapiDTOs;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data.Migrations;
using Filminurk.Models.Omdbapi;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class OmdbapiController : Controller
    {
        private readonly IOmdbapiServices _omdbapiServices;
        public OmdbapiController(IOmdbapiServices omdbapiServices)
        {
            _omdbapiServices = omdbapiServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FindMovie(OmdbapiSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Import", "Omdbapi", new { movie = model.Title });
            }
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Import(string title)
        {
            OmdbapiMovieResultDTO dto = new();
            dto.Title = title;
            _omdbapiServices.OmdbapiResult(dto);
            OmdbapiViewModel vm = new();
            //vm.ID = movie.ID;
            vm.Title = dto.Title;
            vm.Released = dto.Released;
            if (Genre.IsDefined(typeof(Genre),dto.Genre))
            {
                vm.Genre = dto.Genre;
            }
            else
            {
                vm.Genre = "Other";
            }
            vm.imdbRating = dto.imdbRating;
            //vm.Warnings = movie.Warnings;
            vm.Actors = dto.Actors;
            //vm.EntryCreatedAt = movie.EntryCreatedAt;
            //vm.EntryModifiedAt = movie.EntryModifiedAt;
            vm.Director = dto.Director;
            //vm.Tagline = movie.Tagline;
            vm.Plot = dto.Plot;
            //vm.Images.AddRange(images);
            return View(vm);
        }                                
    }
}
