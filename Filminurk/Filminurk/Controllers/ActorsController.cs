using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.Actors;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class ActorsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IActorsServices _actorsServices;
        private readonly IFilesServices _fileServices;
        public ActorsController(FilminurkTARpe24Context context, IActorsServices actorsServices, IFilesServices fileServices)
        {
            _context = context;
            _actorsServices = actorsServices;
            _fileServices = fileServices;
        }
        public IActionResult Index()
        {
            var result = _context.Actors.Select(x => new ActorsIndexViewModel
            {
                ID = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                NickName = x.NickName,
                ActorRating = x.ActorRating,
                Gender = x.Gender,
                FavoriteGenre = x.FavoriteGenre
            });
            return View(result);
        }
    }
}
