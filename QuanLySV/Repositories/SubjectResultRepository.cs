using NHibernate;
using NHibernate.Linq;
using QuanLySV.Contants;
using QuanLySV.Dtos;
using QuanLySV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySV.Repositories
{
    public class SubjectResultRepository : ISubjectResultRepository
    {
        private readonly ISessionFactory _sessionFactory;

        public SubjectResultRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public SubjectResult CreateSubjectResult(SubjectResult newSubjectResult)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                session.Save(newSubjectResult);
                return newSubjectResult;
            }
        }

        public void DeleteSubjectResult(SubjectResult subjectResult)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Delete(subjectResult);
                    tx.Commit();
                }
            }
        }

        public SubjectResult GetSubjectResult(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var subjectResult = session.Query<SubjectResult>().FirstOrDefault(s => s.Id == id);
                return subjectResult;
            }
        }

        public IEnumerable<SubjectResultDto> GetStudentSubjectResults(int studentId)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                Subject subject = null;
                SubjectResult subjectResult = null;

                var results = session.QueryOver<Subject>(() => subject)
                        .JoinQueryOver(s => s.SubjectResults, () => subjectResult)
                        //.Where(s => s.Id == 5)
                        //.JoinQueryOver(sr => sr.Student, () => student)
                        //.Where(st => st.Id == id)
                        .Where(sr => sr.Student.Id == studentId)
                        .Select(
                            _ => subject.Id,
                            _ => subject.Name,
                            _ => subject.NumberOfLesson,
                            _ => subject.ComponentScoreRatio,
                            _ => subjectResult.Id,
                            _ => subjectResult.ComponentScore,
                            _ => subjectResult.ProcessScore,
                            _ => subjectResult.Result
                            )
                        .List<object[]>()
                        .Select(row => new SubjectResultDto
                        {
                            SubjectId = (int)row[0],
                            SubjectName = (string)row[1],
                            NumberOfLesson = (int)row[2],
                            ComponentScoreRatio = (float)row[3],
                            ResultId = (int)row[4],
                            ComponentScore = (float)row[5],
                            ProcessScore = (float)row[6],
                            Result = (Result)Enum.Parse(typeof(Result), row[7].ToString()),
                        })
                        .ToList();

                return results;
            }
        }

        public void UpdateSubjectResult(SubjectResult subjectResult)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Update(subjectResult);
                    tx.Commit();
                }
            }
        }

        public SubjectResult GetSubjectResultWithReferences(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var subjectResult = session.Query<SubjectResult>()
                    .Where(sr => sr.Id == id)
                    .Fetch(sr => sr.Student)
                    .Fetch(sr => sr.Subject)
                    .FirstOrDefault();

                return subjectResult;
            }
        }
    }
}
