using Dapper;
using Microsoft.Data.SqlClient;
using QuanLySV.Contants;
using QuanLySV.Helpers;
using QuanLySV.Models;
using QuanLySV.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySV.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public int CreateStudent(FormCollection collection)
        {
            Enum.TryParse<Gender>(collection["Gender"], out var gender);
            Enum.TryParse<Class>(collection["Class"], out var classEnum);
            DateTime.TryParse(collection["DateOfBirth"], out var dateOfBirth);

            var newStudentId = _studentRepository.CreateStudent(new Student
            {
                Name = collection["Name"],
                Gender = gender,
                AcademyYear = collection["AcademyYear"],
                Class = classEnum,
                DateOfBirth = dateOfBirth
            });

            return newStudentId;

        }

        public void DeleteStudent(int studentId)
        {
            _studentRepository.DeleteStudent(studentId);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _studentRepository.GetAllStudents();
        }

        public Student GetStudent(int studentId)
        {
            return _studentRepository.GetStudent(studentId);
        }

        public void UpdateStudent(int studentId, FormCollection collection)
        {
            Enum.TryParse<Gender>(collection["Gender"], out var gender);
            Enum.TryParse<Class>(collection["Class"], out var classEnum);
            DateTime.TryParse(collection["DateOfBirth"], out var dateOfBirth);

            _studentRepository.UpdateStudent(new Student
            {
                Id = studentId,
                Name = collection["Name"],
                Gender = gender,
                AcademyYear = collection["AcademyYear"],
                Class = classEnum,
                DateOfBirth = dateOfBirth
            });
        }
    }
}