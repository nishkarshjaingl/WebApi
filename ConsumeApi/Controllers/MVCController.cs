using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsumeApi.Controllers
{
    public class MVCController : Controller
    {
        // GET: MVCController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MVCController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MVCController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MVCController/Create
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

        // GET: MVCController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MVCController/Edit/5
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

        // GET: MVCController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MVCController/Delete/5
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
