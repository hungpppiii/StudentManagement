using QuanLySV.Services;
using System.Web.Mvc;

namespace QuanLySV.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: Students
        public ActionResult Index()
        {
            var students = _studentService.GetAllStudents();

            return View(students);
        }

        //GET: Students/Details/5
        public ActionResult Details(int id)
        {
            var student = _studentService.GetStudent(id);

            return View(student);
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
                var newStudentId = _studentService.CreateStudent(collection);

                return RedirectToAction($"Details/{newStudentId}");
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            var student = _studentService.GetStudent(id);

            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            _studentService.UpdateStudent(id, collection);

            return RedirectToAction($"Details/{id}");
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int id)
        {
            var student = _studentService.GetStudent(id);

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            _studentService.DeleteStudent(id);

            return RedirectToAction("Index");

        }
    }
}
