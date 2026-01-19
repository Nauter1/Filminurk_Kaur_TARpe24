using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto.OmdbapiDTOs;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.ApplicationServices.Services
{
    public class OmdbServices : IOmdbapiServices
    {
        private readonly FilminurkTARpe24Context _context;
        public OmdbServices(FilminurkTARpe24Context context)
        {
            _context = context;
        }


        public async Task<OmdbapiMovieResultDTO> OmdbapiResult(OmdbapiMovieResultDTO dto)
        {
            string apikey = Filminurk.Data.Environment.omdbapikey;
            var baseUrl = "http://www.omdbapi.com/";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
                var response = httpClient.GetAsync($"?apikey={apikey}&t={dto.Title}").GetAwaiter().GetResult();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                try
                {
                    List<Root> omdbData = JsonSerializer.Deserialize<List<Root>>(jsonResponse);             // Root needs to be renamed shouldnt it
                    dto.Title = omdbData[0].Title;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            string omdbResponse = baseUrl + $"?t={dto.Title}&apikey={apikey}";

            using (var clientWeather = new HttpClient())
            {
                var httpResponseOmdb = clientWeather.GetAsync(omdbResponse).GetAwaiter().GetResult();
                string jsonOmdb = await httpResponseOmdb.Content.ReadAsStringAsync();

                Root omdbRootDTO = JsonSerializer.Deserialize<Root>(jsonOmdb);

                dto.Title = omdbRootDTO.Title;
                dto.Released = omdbRootDTO.Released;
                dto.Genre = omdbRootDTO.Genre;
                dto.imdbRating = omdbRootDTO.imdbRating;
                dto.Actors = omdbRootDTO.Actors;
                dto.Director = omdbRootDTO.Director;
                dto.Plot = omdbRootDTO.Plot;
            }
            return dto;
        }

        public Movie Create(OmdbapiMovieCreateDTO dto)
        {
            Movie movie = new Movie();
            movie.ID = (Guid)dto.ID;
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.FirstPublished = (DateOnly)dto.FirstPublished;
            movie.CurrentRating = dto.CurrentRating;
            movie.Actors = dto.Actors;
            movie.Genre = dto.Genre;
            movie.Director = dto.Director;
            movie.EntryCreatedAt = DateTime.Now;
            movie.EntryModifiedAt = DateTime.Now;

            _context.Movies.AddAsync(movie);
            _context.SaveChangesAsync();
            return movie;
        }

    }
}