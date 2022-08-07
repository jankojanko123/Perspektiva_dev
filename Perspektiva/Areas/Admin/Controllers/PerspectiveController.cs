using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Perspektiva.Areas.Admin.Models;
using Perspektiva.Helpers;
using System.Drawing;
using System.Security.Claims;

namespace Perspektiva.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class PerspectiveController : Controller
  {
    // GET: PerspectiveController

    public ActionResult Index()
    {


      PerspectiveHelper perspectiveHelper = new PerspectiveHelper();
      List<PerspectivaDataModel> perspectivesList =  perspectiveHelper.GetPerspectives();
      /*pridobi listo perspektiv - vseh*/

      return View(perspectivesList);
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
      IFormFile picFIle = collection.PerspectivePictureFile;
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
      dataCollection.PerspectivePictureByte = bytePic;

      
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
      PerspectiveHelper perspectiveHelper = new PerspectiveHelper();
      PerspectivaDataModel perspective = perspectiveHelper.GetPerspective(id);

      PerspectivesViewModel viewCollection = new PerspectivesViewModel();

      //try
      //{

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

    // POST: PerspectiveController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit( PerspectivesViewModel collection, int id)
    {
      /* TODO: picture edit*/
      try
      {

        byte[] bytePic = null;
        if (collection.PerspectivePictureFile != null)
        {
          IFormFile picFIle = collection.PerspectivePictureFile;
          
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
        }
        else
        {
          bytePic = collection.PerspectivePictureByte;
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
        dataCollection.PerspectivePictureByte = bytePic;

        dataCollection.ID = id;

        PerspectiveHelper perspectiveHelper = new PerspectiveHelper();
        perspectiveHelper.UpdatePerspective(dataCollection, id);

        /////
        collection.ID = id;
        collection.PerspectivePictureByte = bytePic;

        return View(collection);
      }
      catch
      {
        return View();
      }
    }

    public Image byteArrayToImage(byte[] byteArrayIn)
    {
      using (MemoryStream mStream = new MemoryStream(byteArrayIn))
      {
        return Image.FromStream(mStream);
      }
    }
    //another easy way to convert image to bytearray
    public static byte[] imgToByteConverter(Image inImg)
    {
      ImageConverter imgCon = new ImageConverter();
      return (byte[])imgCon.ConvertTo(inImg, typeof(byte[]));
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
