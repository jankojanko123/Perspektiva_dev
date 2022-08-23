using GeoCoordinatePortable;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Perspektiva.Areas.Admin.Models;
using Perspektiva.Helpers;
using Perspektiva.Models;
using System.Security.Claims;
using System;
using System.IO;
using System.Web;
using System.Text;
using Newtonsoft.Json.Linq;

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

    // GET: Home/GetLocale  
    [HttpGet]
    public ActionResult GetBase64Photo(string image)
    {


      return View("Index");
    }

    [HttpPost]
    public IActionResult GetGuessedData()
    {

      var submitedGuessPictureFile = Request.Form.Files[0];
      byte[] bytePic = null;


      if (submitedGuessPictureFile.Length > 0)
      {
        using (var ms = new MemoryStream())
        {
          submitedGuessPictureFile.CopyTo(ms);
          bytePic = ms.ToArray();
          //string s = Convert.ToBase64String(fileBytes);
          // act on the Base64 data
        }
      }
      byte[] submitedGuessPictureByte = bytePic;
      string submitedGuessPicture64 = Convert.ToBase64String(bytePic);



      string userLatLonGuess = Request.Form.Keys.First(); // dumb
      string perspectiveIDReq = Request.Form.Keys.Skip(1).First(); // soooo dumb
       
      int perspectiveID = Int32.Parse(perspectiveIDReq);

      string decodedString = Uri.UnescapeDataString(userLatLonGuess);

      UserGuess ReviecedUserGuess = JsonConvert.DeserializeObject<UserGuess>(decodedString);

      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
      ReviecedUserGuess.UserID = userId;
      ReviecedUserGuess.PerspectiveID = perspectiveID;
      ReviecedUserGuess.PerspectiveGuessPictureByte = submitedGuessPictureByte;

      GuessHelper guessHelper = new GuessHelper();

      guessHelper.SaveGuess(ReviecedUserGuess);

      int points = PonintifyGuess(ReviecedUserGuess);

      return Json(new { success = true, responseText = "yee" });


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
      List<PerspectivaDataModel> perspektivaList = guessHelper.GetPerspective(guess.PerspectiveID);
      PerspectivaDataModel perspektiva = perspektivaList.FirstOrDefault();

      double distanceInMeters = GetDistanceGuessFromPerspectiveInMeters(guess, perspektiva);

      //TODO: determine points, dont forget about the difficulty

      int points = 0;

      switch (distanceInMeters)
      {
        case 0:
          points = 100;
          break;
        case < 50:
          points = 50;
          break;
        case < 100:
          points = 30;
          break;
        case < 180:
          points = 15;
          break;
        case < 270:
          points = 5;
          break;
        case < 500:
          points = 2;
          break;
        case > 500:
          points = 1;
          break;
      }

      //Point multiplyer
      points = points * perspektiva.Difficulty;


      return points;
    }

    public double StrToDouble(string input)
    {
      return double.Parse(input, System.Globalization.CultureInfo.InvariantCulture); ;
    }

  }
}
