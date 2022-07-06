using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Perspektiva.Areas.Admin.Models;
using Perspektiva.Helpers;
using System.Security.Claims;

namespace Perspektiva.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class PerspectiveController : Controller
  {
    // GET: PerspectiveController

    public ActionResult Index()
    {
      return View();
    }

    // GET: PerspectiveController/Details/5
    public ActionResult Details(int id)
    {
      return View();
    }

    // GET: PerspectiveController/Create
    [Authorize(Roles = "SuperAdmin")]
    public ActionResult Create()
    {
      return View();
    }

    // POST: PerspectiveController/Create
    [HttpPost]
    [Authorize(Roles = "SuperAdmin")]
    [ValidateAntiForgeryToken]
    //public ActionResult Create(PerspectivaDataModel collection)


    public async Task<IActionResult> Create(PerspectivesViewModel collection)
    {
      IFormFile picFIle = collection.PerspectivePicture;
      byte[] bytePic = null;


      if (picFIle.Length > 0)
      {
        using (var ms = new MemoryStream())
        {
          picFIle.CopyTo(ms);
          bytePic = ms.ToArray();
          //string s = Convert.ToBase64String(fileBytes);
          // act on the Base64 data
        }
      }

      PerspectivaDataModel dataCollection = new PerspectivaDataModel();

      //try
      //{

      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

      dataCollection.Difficulty = collection.Difficulty;
      dataCollection.Latitude = collection.Latitude;
      dataCollection.Longitude = collection.Longitude;
      dataCollection.Latitude = collection.Latitude;
      dataCollection.Title = collection.Title;
      dataCollection.Description = collection.Description;
      dataCollection.UserID = userId;
      dataCollection.PerspectivePicture = bytePic;

      
      PerspectiveHelper perspectiveHelper = new PerspectiveHelper();
      perspectiveHelper.CreatePerspective(dataCollection);

      return RedirectToAction(nameof(Index));
      //}

      //catch
      //{
      //    return View();
      //}
    }

    // GET: PerspectiveController/Edit/5
    public ActionResult Edit(int id)
    {
      return View();
    }

    // POST: PerspectiveController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    // GET: PerspectiveController/Delete/5
    public ActionResult Delete(int id)
    {
      return View();
    }

    // POST: PerspectiveController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }
  }
}
