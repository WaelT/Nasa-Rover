using CodeTest.Nasa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodeTest.Nasa.Helpers;
using CodeTest.Nasa.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration.CommandLine;

namespace CodeTest.Nasa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<SelectListItem> _rovers;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private void InitRovers()
        {
            _rovers = new List<SelectListItem>
            {
                new SelectListItem() {Value = "curiosity", Text = "Curiosity"},
                new SelectListItem() {Value = "opportunity", Text = "Opportunity"},
                new SelectListItem() {Value = "spirit", Text = "Spirit"}
            };
        }

        public IActionResult Index()
        {
            InitRovers();
            return View(_rovers);
        }
        public IActionResult GetImages(string roverName)
        {
            
            NasaWebServices nasaWebServices = new NasaWebServices();
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\dates.txt");

            DateHelper dateHelper = new DateHelper();
            var datesList = dateHelper.ValidateDatesFromFile(path);
            ResponseViewModel responseViewModel = new ResponseViewModel();

            CallResponse result = new CallResponse();

            foreach (var date in datesList)
            {
                if (!date.HasError)
                {
                    var response = nasaWebServices.GetImagesOnDate(date.StringDate, roverName);
                    var destFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Data\\Photos\\{roverName}\\{date.StringDate}");
                    nasaWebServices.DownloadImages(response.photos, destFolder);
                    result.photos.AddRange(response.photos);
                    responseViewModel.ResponseList.Add(response);
                }
                else
                {
                    responseViewModel.ErrorMessage += date.ErrorMessage + "\n";
                    responseViewModel.HasError = true;

                }
            }
            return PartialView("_ImagesPartialView", responseViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult DateError(string date)
        {
            return View($"Unable to parse {date} from date.txt file");
        }



    }
}
