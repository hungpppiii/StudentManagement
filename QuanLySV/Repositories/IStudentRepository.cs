using QuanLySV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySV.Repositories
{
    public interface IStudentRepository
    {
        Student GetStudent(int studentId);
        IEnumerable<Student> GetAllStudents();
        int CreateStudent(Student student);
        void UpdateStudent(Student studentData);
        void DeleteStudent(int studentId);
    }
}
