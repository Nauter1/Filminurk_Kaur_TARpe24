using Filminurk.Data;
using Filminurk.Models.FavoriteLists;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class FavoriteListsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        // flservice add later
        // fileservice add later
        public FavoriteListsController( FilminurkTARpe24Context context )
        {
            _context = context;

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
    }
}
