using Microsoft.AspNetCore.Mvc;
using Perspektiva.Models;
using System.Diagnostics;
using Perspektiva.Helpers;
using System.Security.Claims;
using Newtonsoft.Json;
using Perspektiva.Areas.Admin.Models;
using GeoCoordinatePortable;
namespace Perspektiva.Controllers
{
    public class Student
    {
        public string? ID { get; set; }
        public string? Name { get; set; }
       public int? Age { get; set; }
    }
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Home/GetLocale  
        [HttpGet]
        public ActionResult GetGuessedLocation(string guesspost)
        {
            //chack if guesspost

            GuessHelper guessHelper = new GuessHelper();
            UserGuess guess =JsonConvert.DeserializeObject<UserGuess>(guesspost);

            int points = PonintifyGuess(guess);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            guess.UserID = userId;

            if (userId == null) return View("Index"); //TODO: return to loginpage

            guessHelper.SaveGuess(guess);
            
            //var user = ControllerContext.HttpContext.User;

            

          return View("Index");
        }


        public double GetDistanceGuessFromPerspectiveInMeters(UserGuess guess, PerspectivaDataModel perspektiva)
        {
            GuessHelper guessHelper = new GuessHelper();

            string lat  = guess.Latitude;

            //TODO: vrni relevantno perspektivo - datumsko odklenjena...
            List<PerspectivaDataModel> perspektivaList = guessHelper.GetPerspective(0);
            PerspectivaDataModel perspektiva = perspektivaList.FirstOrDefault();

            //TODO: porpavi modele na double

            double guessLat = StrToDouble(guess.Latitude);
            double guessLon = StrToDouble(guess.Longitude);
            
            double persLat = StrToDouble(perspektiva.Latitude);
            double perslon = StrToDouble(perspektiva.Longitude);

            
            var sCoord = new GeoCoordinate(guessLat, guessLon);
            var eCoord = new GeoCoordinate(persLat, perslon);

            double distanceInMeters = sCoord.GetDistanceTo(eCoord);

            return distanceInMeters;
        }
        public int PonintifyGuess(UserGuess guess)
        {
            GuessHelper guessHelper = new GuessHelper();

            //TODO: vrni relevantno perspektivo - datumsko odklenjena...
            List<PerspectivaDataModel> perspektivaList = guessHelper.GetPerspective(0);
            PerspectivaDataModel perspektiva = perspektivaList.FirstOrDefault();

            double distanceInMeters = GetDistanceGuessFromPerspectiveInMeters(guess, perspektiva);

            //TODO: determine points, dont forget about the difficulty

            return 0;
        }

        public double StrToDouble(string input)
        {
            return double.Parse(input, System.Globalization.CultureInfo.InvariantCulture); ;
        }
     

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    

    }
}