using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SuciuBogdan_7946.Controllers
{
    public class ProjectController : Controller
    {
        private readonly Data.ApplicationDbContext _db;

        public ProjectController(Data.ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Models.Project> objList = _db.Project;
            return View(objList);

        }

        public IActionResult Create()
        {
            Models.Project objProject = new Models.Project();
            objProject.Name = "";
            return View(objProject);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Project.Find(id);

            if (obj == null)
            {
               return NotFound();
            } 
            
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Project.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.Project objProject)
        {
            if (objProject.Name == null)
            {
                TempData["ErrorMessage"] = "Numele proiectului este obligatoriu";
                return RedirectToAction("Create");
            }
            else
            {
                _db.Add(objProject);
                _db.SaveChanges();
                TempData["Message"] = objProject.Name;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Models.Project objProject)
        {
            if (objProject.Name == null)
            {
                TempData["ErrorMessage"] = "Numele proiectului este obligatoriu";
                return RedirectToAction("Create");
            }
            else
            {
                _db.Update(objProject);
                _db.SaveChanges();
               
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProject (int? id)
        {
            var objProject = _db.Project.Find(id);
            _db.Remove(objProject);
            _db.SaveChanges();

            return RedirectToAction("Index");
            
        }
    }
}
