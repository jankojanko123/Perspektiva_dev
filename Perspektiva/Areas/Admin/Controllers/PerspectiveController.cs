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
        public ActionResult Create(PerspectivesViewModel collection)
        {
            byte[] bytes = { 0, 0, 0, 25 }; //TODO: picture upload!
            //try
            //{
            //scollection.PerspectivePicture = [""];
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
                collection.UserID = userId;
                collection.PerspectivePicture = bytes;//temp

                PerspectiveHelper perspectiveHelper = new PerspectiveHelper();
                perspectiveHelper.CreatePerspective(collection);

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
