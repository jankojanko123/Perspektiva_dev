using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult Create()
        {/// zakaj negre v tale controller https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/areas?view=aspnetcore-6.0
            return View();
        }

        // POST: PerspectiveController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
