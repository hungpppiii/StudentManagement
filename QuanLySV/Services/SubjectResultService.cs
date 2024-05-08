using QuanLySV.Contants;
using QuanLySV.Dtos;
using QuanLySV.Models;
using QuanLySV.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySV.Services
{
    public class SubjectResultService : ISubjectResultService
    {
        private readonly ISubjectResultRepository _subjectResultRepository;

        public SubjectResultService(ISubjectResultRepository subjectResultRepository)
        {
            _subjectResultRepository = subjectResultRepository;
        }

        public SubjectResult CreateSubjectResult(int studentId, FormCollection collection)
        {
            var newSubjectResult = new SubjectResult()
            {
                Student = new Student() { Id = studentId },
                Subject = new Subject() { Id = int.Parse(collection["subjectId"]) },
                Result = Result.Wait
            };

            return _subjectResultRepository.CreateSubjectResult(newSubjectResult);
        }

        public void DeleteSubjectResult(SubjectResult subjectResult)
        {
            _subjectResultRepository.DeleteSubjectResult(subjectResult);
        }

        public IEnumerable<SubjectResultDto> GetStudentSubjectResults(int studentId)
        {
            return _subjectResultRepository.GetStudentSubjectResults(studentId);
        }

        public SubjectResult GetSubjectResult(int id)
        {
            return _subjectResultRepository.GetSubjectResult(id);
        }

        public SubjectResult GetSubjectResultWithReferences(int id)
        {
            return _subjectResultRepository.GetSubjectResultWithReferences(id);
        }

        public int InputScore(int id, FormCollection collection)
        {
            Enum.TryParse<Gender>(collection["Gender"], out var gender);
            Enum.TryParse<Class>(collection["Class"], out var classEnum);
            DateTime.TryParse(collection["DateOfBirth"], out var dateOfBirth);

            var subjectResult = _subjectResultRepository.GetSubjectResultWithReferences(id);

            subjectResult.ProcessScore = float.Parse(collection["ProcessScore"]);
            subjectResult.ComponentScore = float.Parse(collection["ComponentScore"]);
            subjectResult.Result = (subjectResult.ComponentScore * subjectResult.Subject.ComponentScoreRatio)
                    + (subjectResult.ProcessScore * (1 - subjectResult.Subject.ComponentScoreRatio)) >= 4
                    ? Result.Pass : Result.Fail;

            _subjectResultRepository.UpdateSubjectResult(subjectResult);

            return subjectResult.Student.Id;
        }
    }
}