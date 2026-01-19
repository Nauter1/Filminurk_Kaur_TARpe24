using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto.AccuWeatherDTOs;
using Filminurk.Core.Dto.OmdbapiDTOs;

namespace Filminurk.Core.ServiceInterface
{
    public interface IOmdbapiServices
    {
        Task<OmdbapiMovieResultDTO> OmdbapiResult(OmdbapiMovieResultDTO dto);
        Movie Create(OmdbapiMovieCreateDTO dto);
    }
}
