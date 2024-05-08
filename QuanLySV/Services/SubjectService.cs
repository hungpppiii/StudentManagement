using QuanLySV.Models;
using QuanLySV.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySV.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public Subject CreateSubject(FormCollection collection)
        {
            var newSubject = new Subject()
            {
                Name = collection["Name"],
                NumberOfLesson = int.Parse(collection["NumberOfLesson"]),
                ComponentScoreRatio = float.Parse(collection["ComponentScoreRatio"])
            };

            _subjectRepository.CreateSubject(newSubject);

            return newSubject;
        }

        public void DeleteSubject(int id)
        {
            var subject = _subjectRepository.GetSubject(id);
            _subjectRepository.DeleteSubject(subject);
        }

        public IEnumerable<Subject> GetSubjectNotRegister(int studentId)
        {
            return _subjectRepository.GetSubjectNotRegister(studentId);
        }

        public int UpdateSubject(int id, FormCollection collection)
        {
            var subject = _subjectRepository.GetSubject(id);

            subject.Name = collection["Name"];
            subject.NumberOfLesson = int.Parse(collection["NumberOfLesson"]);
            subject.ComponentScoreRatio = float.Parse(collection["ComponentScoreRatio"]);

            _subjectRepository.UpdateSubject(subject);

            return subject.Id;
        }
    }
}