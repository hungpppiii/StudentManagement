using NHibernate;
using NHibernate.Linq;
using QuanLySV.Contants;
using QuanLySV.Data;
using QuanLySV.Dtos;
using QuanLySV.Models;
using QuanLySV.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySV.Controllers
{
    public class SubjectResultsController : Controller
    {
        private readonly ISubjectResultService _subjectResultService;
        private readonly ISubjectService _subjectService;
        private readonly IStudentService _studentService;

        public SubjectResultsController(ISubjectResultService subjectResultService, ISubjectService subjectService, IStudentService studentService)
        {
            _subjectResultService = subjectResultService;
            _subjectService = subjectService;
            _studentService = studentService;
        }

        // GET: SubjectResults/?studentId=1
        public ActionResult Index(int studentId)
        {
            var results = _subjectResultService.GetStudentSubjectResults(studentId);

            var student = _studentService.GetStudent(studentId);

            ViewBag.Student = student;

            return View(results);
        }

        //GET: SubjectResults/Details/5
        public ActionResult Details(int id)
        {
            var subjectResult = _subjectResultService.GetSubjectResult(id);
            return View(subjectResult);
        }

        // GET: SubjectResults/RegisterSubject?studentId=1
        public ActionResult RegisterSubject(int studentId)
        {

            ViewBag.subjects = _subjectService.GetSubjectNotRegister(studentId);
            ViewBag.student = _studentService.GetStudent(studentId);

            return View();
        }

        // POST: SubjectResults/RegisterSubject?studentId=1
        [HttpPost]
        public ActionResult RegisterSubject(int studentId, FormCollection collection)
        {
            _subjectResultService.CreateSubjectResult(studentId, collection);

            return RedirectToAction("Index", new { studentId });

        }

        // GET: SubjectResults/Edit/1
        public ActionResult Edit(int id)
        {
            var subjectResult = _subjectResultService.GetSubjectResultWithReferences(id);

            return View(subjectResult);
        }

        // POST: SubjectResults/Edit/1
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var studentId = _subjectResultService.InputScore(id, collection);

            return RedirectToAction("Index", new { studentId });
        }

        // GET: SubjectResults/Delete/5
        public ActionResult Delete(int id)
        {
            var subjectResult = _subjectResultService.GetSubjectResultWithReferences(id);

            return View(subjectResult);
        }

        // POST: SubjectResults/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var subjectResult = _subjectResultService.GetSubjectResult(id);
            var studentId = subjectResult.Student.Id;

            _subjectResultService.DeleteSubjectResult(subjectResult);

            return RedirectToAction("Index", new { studentId });
        }
    }
}
