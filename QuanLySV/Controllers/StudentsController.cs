using Castle.Core.Resource;
using Castle.Windsor;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using NHibernate;
using QuanLySV.Common;
using QuanLySV.Contants;
using QuanLySV.Data;
using QuanLySV.Dtos;
using QuanLySV.Helpers;
using QuanLySV.Models;
using QuanLySV.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using static QuanLySV.MvcApplication;
using static System.Net.Mime.MediaTypeNames;
namespace QuanLySV.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ISessionFactory _sessionFactory;
        private SqlConnection _connection;

        public StudentsController(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _connection = new SqlConnection(FiddleHelper.GetConnectionStringSQLServer());
        }

        // GET: Students
        public ActionResult Index()
        {
            var selectListStudentSQL = "SELECT * FROM Students";

            _connection.Open();

            var students = _connection.Query<Student>(selectListStudentSQL).ToList();

            _connection.Close();

            return View(students);

            //using (var session = _sessionFactory.OpenSession())
            //{
            //    var students = session.Query<Student>().ToList();
            //    return View(students);
            //}
        }

        //GET: Students/Details/5
        public ActionResult Details(int id)
        {
            var selectStudentSQL = "SELECT * FROM Students s WHERE s.Id = @studentId";

            _connection.Open();

            var students = _connection.QueryFirstOrDefault<Student>(selectStudentSQL, new { studentId = id });

            _connection.Close();

            return View(students);

            //using (var session = _sessionFactory.OpenSession())
            //{
            //    var student = session.Query<Student>().FirstOrDefault(s => s.Id == id);
            //    return View(student);
            //}
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                var insertStudentSQL = @"INSERT INTO Students (Name, Gender, AcademyYear, Class, DateOfBirth) 
                                        OUTPUT INSERTED.Id
                                        VALUES (@Name, @Gender, @AcademyYear, @Class, @DateOfBirth)";
                Enum.TryParse<Gender>(collection["Gender"], out var gender);
                Enum.TryParse<Class>(collection["Class"], out var classEnum);
                DateTime.TryParse(collection["DateOfBirth"], out var dateOfBirth);

                _connection.Open();

                var newStudentId = _connection.QuerySingle<int>(insertStudentSQL, new
                {
                    Name = collection["Name"],
                    Gender = gender,
                    AcademyYear = collection["AcademyYear"],
                    Class = classEnum,
                    DateOfBirth = dateOfBirth
                });

                _connection.Close();

                return RedirectToAction($"Details/{newStudentId}");

                //using (var session = _sessionFactory.OpenSession())
                //{
                //    Enum.TryParse<Gender>(collection["Gender"], out var gender);
                //    Enum.TryParse<Class>(collection["Class"], out var classEnum);
                //    DateTime.TryParse(collection["DateOfBirth"], out var dateOfBirth);

                //    var newStudent = new Student()
                //{
                //    Name = collection["Name"],
                //        Gender = gender,
                //        AcademyYear = collection["AcademyYear"],
                //        Class = classEnum,
                //        DateOfBirth = dateOfBirth,
                //    };

                //    session.Save(newStudent);
                //    return RedirectToAction($"Details/{newStudent.Id}");
                //}
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var student = session.Query<Student>().FirstOrDefault(s => s.Id == id);
                //ViewData["Gender"] = student.Gender.ToSelectList();
                //ViewData["Class"] = student.Gender.ToSelectList();
                return View(student);
            }
        }

        // POST: Students/Edit/5
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

                        var student = session.Query<Student>().FirstOrDefault(s => s.Id == id);

                        student.Name = collection["Name"];
                        student.Gender = gender;
                        student.AcademyYear = collection["AcademyYear"];
                        student.Class = classEnum;
                        student.DateOfBirth = dateOfBirth;

                        session.Update(student);
                        tx.Commit();

                        return RedirectToAction($"Details/{student.Id}");
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var student = session.Get<Student>(id);
                return View(student);
            }
        }

        // POST: Students/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var session = _sessionFactory.OpenSession())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        var student = session.Get<Student>(id);

                        session.Delete(student);
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
