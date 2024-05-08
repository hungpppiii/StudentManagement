using NHibernate;
using QuanLySV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace QuanLySV.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ISessionFactory _sessionFactory;

        public SubjectRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public Subject CreateSubject(Subject newSubject)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                session.Save(newSubject);
                return newSubject;
            }
        }

        public void DeleteSubject(Subject subject)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Delete(subject);
                    tx.Commit();
                }
            }
        }

        public IEnumerable<Subject> GetAllSubjects()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var subject = session.Query<Subject>().ToList();
                return subject;
            }
        }

        public IEnumerable<Subject> GetSubjects(Expression<Func<Subject, bool>> predicate)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var subject = session.Query<Subject>().Where(predicate).ToList();
                return subject;
            }
        }

        public Subject GetSubject(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var subject = session.Query<Subject>().FirstOrDefault(s => s.Id == id);
                return subject;
            }
        }

        public void UpdateSubject(Subject subject)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Update(subject);
                    tx.Commit();
                }
            }
        }

        public IEnumerable<Subject> GetSubjectNotRegister(int studentId)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var subjectIds = session.Query<SubjectResult>()
                    .Where(sr => sr.Student.Id == studentId)
                    .Select(sr => sr.Subject.Id)
                .ToList();

                var subjects = session.QueryOver<Subject>()
                                        .WhereRestrictionOn(s => s.Id)
                                        .Not.IsIn(subjectIds)
                                        .List();

                return subjects;
            }
        }
    }
}