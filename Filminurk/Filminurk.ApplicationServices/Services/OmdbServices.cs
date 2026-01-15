using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto.OmdbapiDTOs;
using Filminurk.Core.ServiceInterface;

namespace Filminurk.ApplicationServices.Services
{
    public class OmdbServices : IOmdbapiServices
    {
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
                var response = httpClient.GetAsync($"?t={dto.Title}&apikey={apikey}").GetAwaiter().GetResult();
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
                

                /*dto.EffectiveDate = weatherRootDTO.Headline.EffectiveDate;
                dto.EffectiveEpochDate = weatherRootDTO.Headline.EffectiveEpochDate;
                dto.Severity = weatherRootDTO.Headline.Severity;
                dto.Text = weatherRootDTO.Headline.Text;
                dto.Category = weatherRootDTO.Headline.Category;
                dto.EndDate = weatherRootDTO.Headline.EndDate;
                dto.EndEpochDate = weatherRootDTO.Headline.EndEpochDate;

                dto.MobileLink = weatherRootDTO.Headline.MobileLink;
                dto.Link = weatherRootDTO.Headline.Link;

                dto.DailyForecastsDate = weatherRootDTO.DailyForecasts[0].Date;
                dto.DailyForecastsEpochDate = weatherRootDTO.DailyForecasts[0].EpochDate;

                dto.TempMinValue = weatherRootDTO.DailyForecasts[0].Temperature.Minimum.Value;
                dto.TempMinUnit = weatherRootDTO.DailyForecasts[0].Temperature.Minimum.Unit;
                dto.TempMinUnitType = weatherRootDTO.DailyForecasts[0].Temperature.Minimum.UnitType;

                dto.TempMaxValue = weatherRootDTO.DailyForecasts[0].Temperature.Maximum.Value;
                dto.TempMaxUnit = weatherRootDTO.DailyForecasts[0].Temperature.Maximum.Unit;
                dto.TempMaxUnitType = weatherRootDTO.DailyForecasts[0].Temperature.Maximum.UnitType;

                dto.DayIcon = weatherRootDTO.DailyForecasts[0].Day.Icon;
                dto.DayIconPhrase = weatherRootDTO.DailyForecasts[0].Day.IconPhrase;
                dto.DayHasPrecipitation = weatherRootDTO.DailyForecasts[0].Day.HasPrecipitation;
                dto.DayPrecipitationType = weatherRootDTO.DailyForecasts[0].Day.PrecipitationType;
                dto.DayPrecipitationIntensity = weatherRootDTO.DailyForecasts[0].Day.PrecipitationIntensity;


                dto.NightIcon = weatherRootDTO.DailyForecasts[0].Night.Icon;
                dto.NightIconPhrase = weatherRootDTO.DailyForecasts[0].Night.IconPhrase;
                dto.NightHasPrecipitation = weatherRootDTO.DailyForecasts[0].Night.HasPrecipitation;
                dto.NightPrecipitationType = weatherRootDTO.DailyForecasts[0].Night.PrecipitationType;
                dto.NightPrecipitationIntensity = weatherRootDTO.DailyForecasts[0].Night.PrecipitationIntensity;    */
            }
            return dto;
        }

    }
}