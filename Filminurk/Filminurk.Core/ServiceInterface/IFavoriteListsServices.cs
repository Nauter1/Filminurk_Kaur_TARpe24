using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;

namespace Filminurk.Core.ServiceInterface
{
    public interface IFavoriteListsServices
    {
        Task<FavoriteList> DetailsAsync(Guid id);
    }
}
