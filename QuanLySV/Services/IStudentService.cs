using QuanLySV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySV.Services
{
    public interface IStudentService
    {
        Student GetStudent(int studentId);
        IEnumerable<Student> GetAllStudents();
        int CreateStudent(FormCollection collection);
        void UpdateStudent(int studentId, FormCollection collection);
        void DeleteStudent(int studentId);
    }
}