using Filminurk.Core.Domain;
using Filminurk.Core.Dto.OmdbapiDTOs;

namespace Filminurk.Models.Omdbapi
{
    public class OmdbapiViewModel
    {
        public string Title { get; set; }
        public string Released { get; set; }
        public Genre Genre { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string imdbRating { get; set; }
    }
}
