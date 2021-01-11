using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace SuciuBogdan_7946.Controllers
{
    public class PageController : Controller
    {
        private readonly Data.ApplicationDbContext _db;

        public PageController(Data.ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            
            IEnumerable<Models.Pages> obj = _db.Pages.Include(c => c.Project);
            
            return View(obj);
        }

        // CREATE - GET
        public IActionResult Create()
        {
            ViewBag.Projects = new SelectList(_db.Project, "Id", "Name");
            return View();
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.Pages objPages)
        {
            var verifyPage = _db.Pages;

            List<String> list = verifyPage
                .Where(p => p.Name == objPages.Name && objPages.ProjectId == p.ProjectId)
                .Select(x => x.Name).ToList<String>();
            
            if (objPages.Name == null)
            {
                TempData["ErrorMessage"] = "Numele paginii este obligatoriu";
                return RedirectToAction("Create");
            }
            else
            if (list.Count > 0)
            {
                TempData["ErrorMessage"] = $"Pagina {objPages.Name} deja exista pentru proiectul ales.";
                return RedirectToAction("Create");
            }
            else
            {
                _db.Add(objPages);
                _db.SaveChanges();
                TempData["SuccessMessage"] = $"Pagina {objPages.Name} creata cu success";
                return RedirectToAction("Index");
            }
        }

        // EDIT - GET
        public IActionResult Edit(int? id)
        {
            var objPage = _db.Pages.Find(id);
            ViewBag.Projects = new SelectList(_db.Project, "Id", "Name");

            return View(objPage);
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Models.Pages pageObj)
        {
            if (pageObj.Name == null) 
            {
                TempData["ErrorMessage"] = "Numele paginii este obligatoriu";
                return RedirectToAction("Index");
            }
            else
            {
                _db.Update(pageObj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // DELETE - GET
        public IActionResult Delete(int? id)
        {
            var objPage = _db.Pages.Find(id);
            ViewBag.Projects = new SelectList(_db.Project, "Id", "Name");

            return View(objPage);
        }

        // DELETE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePage(int? id)
        {
           if (id == null)
            {
                return NotFound();
            }
            var pageObj = _db.Pages.Find(id);
            _db.Remove(pageObj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
