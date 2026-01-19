using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Microsoft.AspNetCore.Http;

namespace Filminurk.Core.Dto.OmdbapiDTOs
{
    public class OmdbapiMovieCreateDTO
    {
        public Guid? ID { get; set; }
        public string Title { get; set; }
        public DateOnly? FirstPublished { get; set; }
        public Genre Genre { get; set; }
        public string Director { get; set; }
        public List<string>? Actors { get; set; }
        public string? Description { get; set; }
        public double? CurrentRating { get; set; }
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}
