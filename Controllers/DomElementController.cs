using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SuciuBogdan_7946.Controllers
{
    public class DomElementController : Controller
    {
        private readonly Data.ApplicationDbContext _db;

        public DomElementController(Data.ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? projectId, int? pageId)
        {
            if (projectId != null)
            {
                ViewBag.projectId = projectId;
                ViewBag.projectName = _db.Project.Find(projectId).Name;
                IEnumerable<Models.Pages> pagesObj = _db.Pages.Where(p => p.ProjectId == projectId);
                ViewBag.pagesObj = pagesObj;
            }
            if (pageId != null)
            {
                ViewBag.pageId = pageId;
                ViewBag.pageName = _db.Pages.Find(pageId).Name;
                IEnumerable<Models.DomElement> domObj = _db.DomElement.Where(e => e.PageId == pageId);
                ViewBag.domObj = domObj;
            }
            IEnumerable<Models.Project> objList = _db.Project;
            return View(objList);
        }

        public IActionResult Create(int? projectId, int? pageId)
        {
            if (projectId == null || pageId == null)
            {
                return NotFound();
            }

            ViewBag.projectId = projectId;
            ViewBag.projectName = _db.Project.Find(projectId).Name;
            ViewBag.pageId = pageId;
            ViewBag.pageName = _db.Pages.Find(pageId).Name;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.DomElement domElement, int projectId, int pageId)
        {
            if (domElement.Name == null)
            {
                TempData["ErrorMessage"] = "Numele elementului este obligatoriu";
                return RedirectToAction("Create");
            }
            else
            if (domElement.LocatorValue == null)
            {
                TempData["ErrorMessage"] = "Valoarea identificatorului este obligatorie";
                return RedirectToAction("Create");
            }
            else
            {
                _db.Add(domElement);
                _db.SaveChanges();
                
                return RedirectToAction("Index", "DomElement", new { projectId  = projectId, pageId = pageId });
            }

        }

        public IActionResult Edit(int? id, int? projectId, int? pageId)
        {
            if (id == null || projectId == null || pageId == null)
            {
                return NotFound();
            }
            ViewBag.projectId = projectId;
            ViewBag.pageId = pageId;
            var elementObj = _db.DomElement.Find(id);

            if (elementObj == null)
            {
                return NotFound();
            }

            return View(elementObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Models.DomElement domElement, int? projectId, int? pageId)
        {
            if (projectId == null || pageId == null)
            {
                return NotFound();
            }
            else
            {
                _db.Update(domElement);
                _db.SaveChanges();

                return RedirectToAction("Index", "DomElement", new { projectId = projectId, pageId = pageId });
            }
        }

        public IActionResult Delete(int? id, int? projectId, int? pageId)
        {
            if (id == null || projectId == null || pageId == null)
            {
                return NotFound();
            }
            var domElement = _db.DomElement.Find(id);
            ViewBag.projectId = projectId;
            ViewBag.projectName = _db.Project.Find(projectId).Name;
            ViewBag.pageId = pageId;
            ViewBag.pageName = _db.Pages.Find(pageId).Name;
            if (domElement == null)
            {
                return NotFound();
            }
            return View(domElement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteElement(int? id, int? projectId, int? pageId)
        {
            if (id == null || projectId == null || pageId == null)
            {
                return NotFound();
            }
            var domElement = _db.DomElement.Find(id);
            ViewBag.projectId = projectId;
            ViewBag.pageId = pageId;
            if (domElement == null)
            {
                return NotFound();
            }
            else
            {
                _db.Remove(domElement);
                _db.SaveChanges();
                return RedirectToAction("Index", "DomElement", new { projectId = projectId, pageId = pageId });
            }

        }
    }
}
