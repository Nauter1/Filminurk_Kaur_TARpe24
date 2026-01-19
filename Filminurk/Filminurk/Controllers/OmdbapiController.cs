using Filminurk.ApplicationServices.Services;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.Dto.OmdbapiDTOs;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data.Migrations;
using Filminurk.Models.Omdbapi;
using Microsoft.AspNetCore.Mvc;
using Filminurk.Data;
using Filminurk.Models.Movies;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IO;
using static System.Net.WebRequestMethods;
using static Azure.Core.HttpHeader;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using System;

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
                return Import(model.Title);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Import(string title)
        {
            OmdbapiMovieResultDTO dto = new();
            dto.Title = title;
            _omdbapiServices.OmdbapiResult(dto);
            OmdbapiViewModel vm = new();
            //vm.ID = movie.ID;
            vm.Title = dto.Title;
            vm.Released = dto.Released;
            if (Genre.IsDefined(typeof(Genre), dto.Genre))
            {
                vm.Genre = (Genre)Enum.Parse(typeof(Genre), dto.Genre);
            }
            else
            {
                vm.Genre = Genre.Other;
            }
            vm.imdbRating = dto.imdbRating;
            //vm.Warnings = movie.Warnings;
            vm.Actors = dto.Actors;
            //vm.EntryCreatedAt = movie.EntryCreatedAt;
            //vm.EntryModifiedAt = movie.EntryModifiedAt;
            vm.Director = dto.Director;
            //vm.Tagline = movie.Tagline;
            vm.Plot = dto.Plot;
            //vm.Images.AddRange(images);
            return View("Import", vm);
        }
            [HttpPost]
            public IActionResult Import(OmdbapiViewModel vm)
            {
                if (ModelState.IsValid)
                {
                    var dto = new OmdbapiMovieCreateDTO()
                    {
                        ID = Guid.NewGuid(),
                        Title = vm.Title,
                        Description = vm.Plot,
                        Director = vm.Director,
                        Actors = vm.Actors.Split(new char[] { ',' }).ToList(),
                        CurrentRating = double.Parse(vm.imdbRating, System.Globalization.CultureInfo.InvariantCulture),
                        Genre = vm.Genre,
                        EntryCreatedAt = DateTime.Now,
                        EntryModifiedAt = DateTime.Now,
                    };

                if (vm.Released == "N/A" || vm.Released == null)
                {
                    dto.FirstPublished = DateOnly.MinValue;
                }
                else
                {
                    dto.FirstPublished = DateOnly.Parse(vm.Released);
                }


                    var result = _omdbapiServices.Create(dto);
                if (result == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
            }

                return RedirectToAction(nameof(Index));
            }

    }
}

