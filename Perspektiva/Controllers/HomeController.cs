using Microsoft.AspNetCore.Mvc;
using Perspektiva.Models;
using System.Diagnostics;
using Perspektiva.Helpers;
using System.Security.Claims;
using Newtonsoft.Json;

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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            guess.UserID = userId;

            if (userId == null) return View("Index"); //TODO: return to loginpage

            guessHelper.SaveGuess(guess);
            
            //var user = ControllerContext.HttpContext.User;

            

          return View("Index");
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