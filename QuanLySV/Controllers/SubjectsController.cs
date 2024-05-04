using NHibernate;
using QuanLySV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySV.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ISessionFactory _sessionFactory;

        public SubjectsController(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        // GET: Subjects
        public ActionResult Index()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var subjects = session.Query<Subject>().ToList();
                return View(subjects);
            }
        }

        //GET: Subjects/Details/5
        public ActionResult Details(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var subject = session.Query<Subject>().FirstOrDefault(s => s.Id == id);
                return View(subject);
            }
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
            try
            {
                using (var session = _sessionFactory.OpenSession())
                {
                    var newSubject = new Subject()
                    {
                        Name = collection["Name"],
                        NumberOfLesson = int.Parse(collection["NumberOfLesson"]),
                        ComponentScoreRatio = float.Parse(collection["ComponentScoreRatio"])
                    };

                    session.Save(newSubject);
                    return RedirectToAction($"Details/{newSubject.Id}");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var subject = session.Query<Subject>().FirstOrDefault(s => s.Id == id);
                return View(subject);
            }
        }

        // POST: Subjects/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                using (var session = _sessionFactory.OpenSession())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        var subject = session.Query<Subject>().FirstOrDefault(s => s.Id == id);

                        subject.Name = collection["Name"];
                        subject.NumberOfLesson = int.Parse(collection["NumberOfLesson"]);
                        subject.ComponentScoreRatio = float.Parse(collection["ComponentScoreRatio"]);

                        session.Update(subject);
                        tx.Commit();

                        return RedirectToAction($"Details/{subject.Id}");
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var subject = session.Get<Subject>(id);
                return View(subject);
            }
        }

        // POST: Subjects/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var session = _sessionFactory.OpenSession())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        var subject = session.Get<Subject>(id);

                        session.Delete(subject);
                        tx.Commit();

                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
