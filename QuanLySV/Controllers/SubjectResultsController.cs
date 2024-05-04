using NHibernate;
using NHibernate.Linq;
using QuanLySV.Contants;
using QuanLySV.Data;
using QuanLySV.Dtos;
using QuanLySV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySV.Controllers
{
    public class SubjectResultsController : Controller
    {
        private readonly ISessionFactory _sessionFactory;

        public SubjectResultsController(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        // GET: SubjectResults/?studentId=1
        public ActionResult Index(int studentId)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                Subject subject = null;
                SubjectResult subjectResult = null;

                var results = session.QueryOver<Subject>(() => subject)
                    .JoinQueryOver(s => s.SubjectResults, () => subjectResult)
                    //.Where(s => s.Id == 5)
                    //.JoinQueryOver(sr => sr.Student, () => student)
                    //.Where(st => st.Id == id)
                    .Where(sr => sr.Student.Id == studentId)
                    .Select(
                        _ => subject.Id,
                        _ => subject.Name,
                        _ => subject.NumberOfLesson,
                        _ => subject.ComponentScoreRatio,
                        _ => subjectResult.Id,
                        _ => subjectResult.ComponentScore,
                        _ => subjectResult.ProcessScore,
                        _ => subjectResult.Result
                        )
                    .List<object[]>()
                    .Select(row => new SubjectResultDto
                    {
                        SubjectId = (int)row[0],
                        SubjectName = (string)row[1],
                        NumberOfLesson = (int)row[2],
                        ComponentScoreRatio = (float)row[3],
                        ResultId = (int)row[4],
                        ComponentScore = (float)row[5],
                        ProcessScore = (float)row[6],
                        Result = (Result)Enum.Parse(typeof(Result), row[7].ToString()),
                    })
                    .ToList();

                var student = session.Query<Student>().FirstOrDefault(s => s.Id == studentId);

                ViewBag.Student = student;

                return View(results);
            }
        }

        //GET: SubjectResults/Details/5
        public ActionResult Details(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var subjectResult = session.Query<SubjectResult>().FirstOrDefault(s => s.Id == id);
                return View(subjectResult);
            }
        }

        // GET: SubjectResults/RegisterSubject?studentId=1
        public ActionResult RegisterSubject(int studentId)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var subjectIds = session.Query<SubjectResult>()
                    .Where(sr => sr.Student.Id == studentId)
                    .Select(sr => sr.Subject.Id)
                    .ToList();

                ViewBag.subjects = session.QueryOver<Subject>()
                                        .WhereRestrictionOn(s => s.Id)
                                        .Not.IsIn(subjectIds)
                                        .List();
                ViewBag.student = session.Get<Student>(studentId);

                return View();
            }
        }

        // POST: SubjectResults/RegisterSubject?studentId=1
        [HttpPost]
        public ActionResult RegisterSubject(int studentId, FormCollection collection)
        {
            try
            {
                using (var session = _sessionFactory.OpenSession())
                {
                    var newSubjectResult = new SubjectResult()
                    {
                        Student = new Student() { Id = studentId },
                        Subject = new Subject() { Id = int.Parse(collection["subjectId"]) },
                        Result = Result.Wait
                    };

                    session.Save(newSubjectResult);
                    return RedirectToAction("Index", new { studentId });
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: SubjectResults/Edit/1
        public ActionResult Edit(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var subjectResult = session.Query<SubjectResult>()
                    .Where(sr => sr.Id == id)
                    .Fetch(sr => sr.Subject)
                    .Fetch(sr => sr.Student)
                    .FirstOrDefault();

                return View(subjectResult);
            }
        }

        // POST: SubjectResults/Edit/1
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                using (var session = _sessionFactory.OpenSession())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        Enum.TryParse<Gender>(collection["Gender"], out var gender);
                        Enum.TryParse<Class>(collection["Class"], out var classEnum);
                        DateTime.TryParse(collection["DateOfBirth"], out var dateOfBirth);

                        var subjectResult = session.Query<SubjectResult>().FirstOrDefault(s => s.Id == id);

                        subjectResult.ProcessScore = float.Parse(collection["ProcessScore"]);
                        subjectResult.ComponentScore = float.Parse(collection["ComponentScore"]);
                        subjectResult.Result = (subjectResult.ComponentScore * subjectResult.Subject.ComponentScoreRatio)
                                + (subjectResult.ProcessScore * (1 - subjectResult.Subject.ComponentScoreRatio)) >= 4 
                                ? Result.Pass : Result.Fail;

                        session.Update(subjectResult);
                        tx.Commit();

                        return RedirectToAction("Index", new { studentId = subjectResult.Student.Id });
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: SubjectResults/Delete/5
        public ActionResult Delete(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var subjectResult = session.Query<SubjectResult>()
                    .Where(sr => sr.Id == id)
                    .Fetch(sr => sr.Subject)
                    .Fetch(sr => sr.Student)
                    .FirstOrDefault();
                return View(subjectResult);
            }
        }

        // POST: SubjectResults/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var session = _sessionFactory.OpenSession())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        var subjectResult = session.Get<SubjectResult>(id);
                        var studentId = subjectResult.Student.Id;
                        //session.Delete(new SubjectResult { Id = id }); Oke
                        session.Delete(subjectResult);
                        tx.Commit();

                        return RedirectToAction("Index", new { studentId = studentId });
                    }
                }
            }
            catch
            {
                return RedirectToAction("Delete", new {id});
            }
        }
    }
}
