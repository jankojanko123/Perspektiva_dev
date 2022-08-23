using Microsoft.AspNetCore.Mvc;
using Perspektiva.Models;
using System.Diagnostics;
using Perspektiva.Helpers;
using System.Security.Claims;
using Newtonsoft.Json;
using Perspektiva.Areas.Admin.Models;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize(Roles = "Basic, Admin, SuperAdmin")]
    public IActionResult MyPerspectives()
    {

      PerspectiveHelper perspectiveHelper = new PerspectiveHelper();
      List<PerspectivaDataModel> perspectivesList = perspectiveHelper.GetPerspectives();
      /*pridobi listo perspektiv - vseh*/

      return View(perspectivesList);
      
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