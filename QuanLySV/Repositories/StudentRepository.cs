using Dapper;
using Microsoft.Data.SqlClient;
using QuanLySV.Contants;
using QuanLySV.Helpers;
using QuanLySV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySV.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public int CreateStudent(Student student)
        {
            var insertStudentSQL = @"INSERT INTO Students (Name, Gender, AcademyYear, Class, DateOfBirth) 
                                        OUTPUT INSERTED.Id
                                        VALUES (@Name, @Gender, @AcademyYear, @Class, @DateOfBirth)";

            using (var connection = new SqlConnection(FiddleHelper.GetConnectionStringSQLServer()))
            {
                connection.Open();

                var newStudentId = connection.QuerySingle<int>(insertStudentSQL, student);

                return newStudentId;
            }
        }

        public void DeleteStudent(int studentId)
        {
            var deleteStudentSQL = "DELETE FROM Students WHERE Id = @StudentId";

            using (var connection = new SqlConnection(FiddleHelper.GetConnectionStringSQLServer()))
            {
                connection.Open();

                var affectedRows = connection.Execute(deleteStudentSQL, new
                {
                    StudentId = studentId
                });
            }
        }

        public IEnumerable<Student> GetAllStudents()
        {
            var selectListStudentSQL = "SELECT * FROM Students";

            using (var connection = new SqlConnection(FiddleHelper.GetConnectionStringSQLServer()))
            {
                connection.Open();

                var students = connection.Query<Student>(selectListStudentSQL).ToList();

                return students;
            }
        }

        public Student GetStudent(int studentId)
        {
            var selectStudentSQL = "SELECT * FROM Students s WHERE s.Id = @StudentId";

            using (var connection = new SqlConnection(FiddleHelper.GetConnectionStringSQLServer()))
            {
                connection.Open();

                var student = connection.QueryFirstOrDefault<Student>(selectStudentSQL, new { StudentId = studentId });

                return student;
            }
        }

        public void UpdateStudent(Student studentData)
        {
            var updateStudentSQL = @"UPDATE Students
                                    SET Name = @Name, Gender = @Gender, AcademyYear = @AcademyYear, Class = @Class, DateOfBirth = @DateOfBirth
                                    WHERE Id = @Id";
            using (var connection = new SqlConnection(FiddleHelper.GetConnectionStringSQLServer()))
            {
                connection.Open();

                var affectedRows = connection.Execute(updateStudentSQL, studentData);
            }
        }
    }
}