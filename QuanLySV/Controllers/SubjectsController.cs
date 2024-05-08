using NHibernate;
using QuanLySV.Models;
using QuanLySV.Repositories;
using QuanLySV.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySV.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectRepository subjectRepository, ISubjectService subjectService)
        {
            _subjectRepository = subjectRepository;
            _subjectService = subjectService;
        }

        // GET: Subjects
        public ActionResult Index()
        {
            var subjects = _subjectRepository.GetAllSubjects();
            return View(subjects);
        }

        //GET: Subjects/Details/5
        public ActionResult Details(int id)
        {
            var subject = _subjectRepository.GetSubject(id);
            return View(subject);
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var newSubject = _subjectService.CreateSubject(collection);
            return RedirectToAction($"Details/{newSubject.Id}");
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(int id)
        {
            var subject = _subjectRepository.GetSubject(id);
            return View(subject);
        }

        // POST: Subjects/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var updatedSubjectId = _subjectService.UpdateSubject(id, collection);
            return RedirectToAction($"Details/{updatedSubjectId}");
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int id)
        {
            var subject = _subjectRepository.GetSubject(id);
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            _subjectService.DeleteSubject(id);
            return RedirectToAction("Index");
        }
    }
}
