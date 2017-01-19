using System;
using System.Collections.Generic;
using System.Linq;
using CMS.Domain;
using CMS.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resource = CMS.WebUI.ViewModels.Resource;

namespace CMS.WebUI.Controllers
{
    [Authorize("Professor")]
    public class ProfessorController : Controller
    {
        private readonly CmsContext _context;
        private readonly IEnumerable<Subject> _subjects;

        public ProfessorController()
        {
            _context = new CmsContext();

            _subjects = _context.Subjects;
        }

        public IActionResult Courses()
        {
            ViewBag.Subjects = _subjects.Where(s => s.teacherName.Equals(User.Identity.Name)).ToList(); ;
            return View();
        }

        public IActionResult Course(string id = null)
        {
            var course = _subjects.First(s => s.Id.ToString().Equals(id));
            return View(course);
        }

        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCourse(ViewModels.Subject newSubject, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(newSubject);
            }
            _context.Subjects.Add(new Subject()
            {
                Id = Guid.NewGuid(),
                subjectName = newSubject.SubjectName,
                teacherName = newSubject.TeacherName

            });
            _context.SaveChanges();

            return RedirectToAction("Courses", "Professor");
        }

        public IActionResult AddResource()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddResource(Resource resource, string returnUrl = null)
        {
            if (!ModelState.IsValid) return View(resource);

            var subject = _context.Subjects.First(s => s.subjectName.Equals(resource.CourseName));
            var persistentResource = new Domain.Models.Resource
            {
                Subject = subject,
                SubjectNo = subject.Id,
                type = resource.Type,
                path = resource.File.Name
            };

            _context.Resources.Add(persistentResource);

            _context.SaveChanges();

            return RedirectToAction("Course", "Professor",new {id=subject.Id});
        }
    }
}