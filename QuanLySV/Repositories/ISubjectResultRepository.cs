using QuanLySV.Dtos;
using QuanLySV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySV.Repositories
{
    public interface ISubjectResultRepository
    {
        SubjectResult GetSubjectResult(int id);
        SubjectResult GetSubjectResultWithReferences(int id);
        IEnumerable<SubjectResultDto> GetStudentSubjectResults(int studentId);
        SubjectResult CreateSubjectResult(SubjectResult newSubjectResult);
        void UpdateSubjectResult(SubjectResult subjectResult);
        void DeleteSubjectResult(SubjectResult subjectResult);
    }
}
