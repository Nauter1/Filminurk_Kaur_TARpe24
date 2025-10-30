using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;

namespace Filminurk.Core.ServiceInterface
{
    public interface IActorsServices
    {
        Task<Movie> Create(ActorsDTO dto);
        Task<Movie> Update(ActorsDTO dto);
        Task<Movie> Delete(Guid id);
        Task<Movie> DetailsAsync(Guid id);
    }
}
