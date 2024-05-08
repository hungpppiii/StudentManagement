using QuanLySV.Dtos;
using QuanLySV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace QuanLySV.Services
{
    public interface ISubjectResultService
    {
        SubjectResult GetSubjectResult(int id);
        SubjectResult GetSubjectResultWithReferences(int id);
        IEnumerable<SubjectResultDto> GetStudentSubjectResults(int studentId);
        SubjectResult CreateSubjectResult(int studentId, FormCollection collection);
        int InputScore(int id, FormCollection collection);
        void DeleteSubjectResult(SubjectResult subjectResult);
    }
}
