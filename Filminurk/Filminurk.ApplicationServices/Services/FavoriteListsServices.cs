using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.ApplicationServices.Services
{
    public class FavoriteListsServices : IFavoriteListsServices
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFilesServices _filesServices;

        public FavoriteListsServices(FilminurkTARpe24Context context, IFilesServices filesServices)
        {
            _context = context;
            _filesServices = filesServices;
        }
        public async Task<FavoriteList> DetailsAsync(Guid id)
        {
            var result = await _context.FavoriteLists.FirstOrDefaultAsync(x => x.FavoriteListID == id);
            return result;
        }
    }
}
