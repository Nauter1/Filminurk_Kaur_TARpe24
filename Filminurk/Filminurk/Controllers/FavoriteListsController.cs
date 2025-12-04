using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Data.Migrations;
using Filminurk.Models.FavoriteLists;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class FavoriteListsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFavoriteListsServices _favoriteListsServices;
        // fileservice add later
        public FavoriteListsController( FilminurkTARpe24Context context, IFavoriteListsServices favoriteListsServices )
        {
            _context = context;
            _favoriteListsServices = favoriteListsServices;

        }
        public IActionResult Index()
        {
            var resultingLists = _context.FavoriteLists
                .OrderByDescending(y => y.ListCreatedAt)
                .Select(x => new FavoriteListsIndexViewModel
            {
                FavoriteListID = x.FavoriteListID,
                ListName = x.ListName,
                ListBelongsToUser = x.ListBelongsToUser,
                IsMovieOrActor = x.IsMovieOrActor,
                Description = x.Description,
                ListCreatedAt = x.ListCreatedAt,
                //ListDeletedAt = (DateTime)x.ListDeletedAt,
                Image = (List<FavoriteListsIndexImageViewModel>)_context.FilesToDatabase
                .Where(ml => ml.ListID == x.FavoriteListID)
                .Select(li => new FavoriteListsIndexImageViewModel
                {
                    ListID = li.ListID,
                    ImageID = li.ImageID,
                    ImageData = li.ImageData,
                    ImageTitle = li.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(li.ImageData)),
                })
            });
            return View(resultingLists);
        }
        /* create get, create post */
        [HttpGet]
        public IActionResult Create() 
        {
            var movies = _context.Movies.OrderBy(m=>m.Title).Select(mo => new MoviesIndexViewModel
            {
                ID = mo.ID,
                Title = mo.Title,
                CurrentRating = mo.CurrentRating,
                FirstPublished = mo.FirstPublished,
                Genre = mo.Genre,
            }).ToList();
            ViewData["allmovies"] = movies;
            ViewData["userHasSelected"] = new List<string>();
            //TODO IDENTIFY USER
            FavoriteListUserCreateViewModel vm = new();
            return View("UserCreate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> UserCreate(FavoriteListUserCreateViewModel vm, List<string> userHasSelected, List<MoviesIndexViewModel> movies)
        {
            List<Guid> tempParse = new();
            foreach (var stringID in userHasSelected)
            {
                tempParse.Add(Guid.Parse(stringID));
            }
            var newListDto = new FavoriteListDTO() { };
            newListDto.ListName = vm.ListName;
            newListDto.Description = vm.Description;
            newListDto.IsMovieOrActor = vm.IsMovieOrActor;
            newListDto.IsPrivate = vm.IsPrivate;
            newListDto.ListCreatedAt = DateTime.UtcNow;
            newListDto.ListBelongsToUser = "00000000-0000-0000-0000-000000000001";
            newListDto.ListModifiedAt = DateTime.UtcNow;
            newListDto.ListDeletedAt = vm.ListDeletedAt;

            var listofmoviestoadd = new List<Movie>();
            foreach (var movieId in tempParse)
            {
                var thismovie = _context.Movies.Where(tm => tm.ID == movieId).ToArray().First();
                listofmoviestoadd.Add((Movie)thismovie);
            }
            newListDto.ListOfMovies = listofmoviestoadd;
            
            /*
            List<Guid> convertedIDs = new List<Guid>();
            if (newListDto.ListOfMovies != null)
            {
                convertedIDs = MovieToId(newListDto.ListOfMovies);
            }
            */
            var newList = await _favoriteListsServices.Create(newListDto/* ,convertedIDs*/);
            if (newList == null)
            {
                return BadRequest();
            }
            return RedirectToAction("Index", vm);
        }
        [HttpGet]
        public async Task<IActionResult> UserDetails(Guid id, Guid thisuserid)
        {
            if (id == null || thisuserid == null)
            {
                return BadRequest();
            }

            var thisList = _context.FavoriteLists.Where(tl => tl.FavoriteListID == id && tl.ListBelongsToUser == thisuserid.ToString()).Select(stl => new FavoriteListUserDetailsViewModel
            {
                FavoriteListID = stl.FavoriteListID,
                ListBelongsToUser = stl.ListBelongsToUser,
                IsMovieOrActor = stl.IsMovieOrActor,
                Description = stl.Description,
                IsPrivate = stl.IsPrivate,
                ListOfMovies = stl.ListOfMovies,
                ListCreatedAt = stl.ListCreatedAt,
                IsReported = stl.IsReported,
                /*Image = _context.FilesToDatabase.Where(i => i.ListID == stl.FavoriteListID).Select(si => new FavoriteListsIndexImageViewModel
                {
                    ImageID = si.ImageID,
                    ListID = si.ListID,
                    ImageData = si.ImageData,
                    ImageTitle = si.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(si.ImageData)),
                }).ToList().First()*/
          
            }).First();
            // add viewdata attribute here later, to discern between user and admin
            return View("Details", (FavoriteListUserDetailsViewModel)thisList);
        }
        /*[HttpGet]
        public async Task<IActionResult> UserTogglePrivacy(Guid id, Guid thisuserid)
        {
            if (id == null || thisuserid == null)
            {
                return BadRequest();
            }

            var thisList = _context.FavoriteLists.Where(tl => tl.FavoriteListID == id && tl.ListBelongsToUser == thisuserid.ToString()).Select(stl => new FavoriteListUserDetailsViewModel
            {
                FavoriteListID = stl.FavoriteListID,
                ListBelongsToUser = stl.ListBelongsToUser,
                IsMovieOrActor = stl.IsMovieOrActor,
                Description = stl.Description,
                IsPrivate = stl.IsPrivate,
                ListOfMovies = stl.ListOfMovies,
                ListCreatedAt = stl.ListCreatedAt,
                IsReported = stl.IsReported,
                /*Image = _context.FilesToDatabase.Where(i => i.ListID == stl.FavoriteListID).Select(si => new FavoriteListsIndexImageViewModel
                {
                    ImageID = si.ImageID,
                    ListID = si.ListID,
                    ImageData = si.ImageData,
                    ImageTitle = si.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(si.ImageData)),
                }).ToList().First()

            }).First();         
            // add viewdata attribute here later, to discern between user and admin
            return View("UserTogglePrivacy", (FavoriteListUserDetailsViewModel)thisList);
        }     */
        [HttpPost]
        public async Task<IActionResult> UserTogglePrivacy(Guid id)
        {
            FavoriteList ThisList = await _favoriteListsServices.DetailsAsync(id);

            FavoriteListDTO UpdatedList = new FavoriteListDTO();
            UpdatedList.FavoriteListID = ThisList.FavoriteListID;
            UpdatedList.ListBelongsToUser = ThisList.ListBelongsToUser;
            UpdatedList.ListName = ThisList.ListName;
            UpdatedList.Description = ThisList.Description;
            UpdatedList.IsPrivate = !ThisList.IsPrivate;
            UpdatedList.ListOfMovies = ThisList.ListOfMovies;
            UpdatedList.IsMovieOrActor = ThisList.IsMovieOrActor;
            UpdatedList.ListCreatedAt = ThisList.ListCreatedAt;
            UpdatedList.ListModifiedAt = DateTime.Now;
            UpdatedList.ListDeletedAt = ThisList.ListDeletedAt;

            var result = await _favoriteListsServices.Update(UpdatedList, "Private");
            if (result == null)
            {
                return NotFound();
            }
            return RedirectToAction("UserDetails",result.FavoriteListID);
        }
        [HttpPost]
        public async Task<IActionResult> UserDelete(Guid id)
        {
            var deletedList = await _favoriteListsServices.DetailsAsync(id);

            var dto = new FavoriteListDTO();
            dto.FavoriteListID = deletedList.FavoriteListID;
            dto.ListBelongsToUser = deletedList.ListBelongsToUser;
            dto.ListName = deletedList.ListName;
            dto.Description = deletedList.Description;
            dto.IsPrivate = deletedList.IsPrivate;
            dto.ListOfMovies = deletedList.ListOfMovies;
            dto.IsMovieOrActor = deletedList.IsMovieOrActor;
            dto.ListCreatedAt = deletedList.ListCreatedAt;
            dto.ListModifiedAt = DateTime.Now;
            dto.ListDeletedAt = DateTime.Now;

            var result = await _favoriteListsServices.Update(dto, "Delete");
            if (result == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }



        private List<Guid> MovieToId(List<Movie> ListOfMovies)
        {
            var result = new List<Guid>();
            foreach (var movie in ListOfMovies)
            {
                result.Add((Guid)movie.ID);
            }
            return result;
        }
    }
}
