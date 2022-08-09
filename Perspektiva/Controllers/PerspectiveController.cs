using GeoCoordinatePortable;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Perspektiva.Areas.Admin.Models;
using Perspektiva.Helpers;
using Perspektiva.Models;
using System.Security.Claims;

namespace Perspektiva.Controllers
{
  public class PerspectiveController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult SubmitGuess(int id)
    { // user submit

      PerspectiveHelper perspectiveHelper = new PerspectiveHelper();
      PerspectivaDataModel perspective = perspectiveHelper.GetPerspective(id);

      PerspectivesViewModel viewCollection = new PerspectivesViewModel();

      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

      viewCollection.ID = perspective.ID;
      viewCollection.Difficulty = perspective.Difficulty;
      viewCollection.Latitude = perspective.Latitude;
      viewCollection.Longitude = perspective.Longitude;
      viewCollection.Latitude = perspective.Latitude;
      viewCollection.Title = perspective.Title;
      viewCollection.Description = perspective.Description;
      viewCollection.UserID = userId;
      viewCollection.PerspectivePictureByte = perspective.PerspectivePictureByte;

      return View(viewCollection);
    }

    // GET: Home/GetLocale  
    [HttpGet]
    public ActionResult GetGuessedLocation(string guesspost)
    {
      //chack if guesspost

      GuessHelper guessHelper = new GuessHelper();
      UserGuess guess = JsonConvert.DeserializeObject<UserGuess>(guesspost);

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

      string lat = guess.Latitude;

      //TODO: vrni relevantno perspektivo - datumsko odklenjena...
      List<PerspectivaDataModel> perspektivaList = guessHelper.GetPerspective(0);
      //PerspectivaDataModel perspektiva = perspektivaList.FirstOrDefault();

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

  }
}
