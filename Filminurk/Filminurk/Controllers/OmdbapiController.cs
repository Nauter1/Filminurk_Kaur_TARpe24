using Filminurk.Core.Dto.OmdbapiDTOs;
using Filminurk.Core.ServiceInterface;
using Filminurk.Models.Omdbapi;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class OmdbapiController : Controller
    {
        private readonly IOmdbapiServices _omdbapiServices;
        public OmdbapiController(IOmdbapiServices omdbapiServices)
        {
            _omdbapiServices = omdbapiServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FindMovie(OmdbapiSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Import", "Omdbapi", new { movie = model.Title });
            }
            return View(model);
        }
        
        [HttpGet]
        public IActionResult City(string title)
        {
            OmdbapiMovieResultDTO dto = new();
            dto.Title = title;
            _omdbapiServices.OmdbapiResult(dto);
            OmdbapiViewModel vm = new();

            /*vm.EffectiveDate = dto.EffectiveDate;
            vm.EffectiveEpochDate = dto.EffectiveEpochDate;
            vm.Severity = dto.Severity;
            vm.Text = dto.Text;
            vm.Category = dto.Category;
            vm.EndDate = dto.EndDate;
            vm.EndEpochDate = dto.EndEpochDate;
            vm.DailyForecastsEpochDate = dto.DailyForecastsEpochDate;
            vm.DailyForecastsDate = dto.DailyForecastsDate;

            vm.TempMinValue = dto.TempMinValue;
            vm.TempMinUnit = dto.TempMinUnit;
            vm.TempMinUnitType = dto.TempMinUnitType;

            vm.TempMaxValue = dto.TempMaxValue;
            vm.TempMaxUnit = dto.TempMaxUnit;
            vm.TempMaxUnitType = dto.TempMaxUnitType;

            vm.DayIcon = dto.DayIcon;
            vm.DayIconPhrase = dto.DayIconPhrase;
            vm.DayHasPrecipitation = dto.DayHasPrecipitation;
            vm.DayPrecipitationType = dto.DayPrecipitationType;
            vm.DayPrecipitationIntensity = dto.DayPrecipitationIntensity;

            vm.NightIcon = dto.NightIcon;
            vm.NightIconPhrase = dto.NightIconPhrase;
            vm.NightHasPrecipitation = dto.NightHasPrecipitation;
            vm.NightPrecipitationType = dto.NightPrecipitationType;
            vm.NightPrecipitationIntensity = dto.NightPrecipitationIntensity;

            vm.MobileLink = dto.MobileLink;
            vm.Link = dto.Link;       */
            return View(vm);
        }                                
    }
}
