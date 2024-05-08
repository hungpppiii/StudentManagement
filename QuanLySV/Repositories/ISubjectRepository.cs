using QuanLySV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySV.Repositories
{
    public interface ISubjectRepository
    {
        Subject GetSubject(int id);
        IEnumerable<Subject> GetAllSubjects();
        IEnumerable<Subject> GetSubjects(Expression<Func<Subject, bool>> predicate);
        IEnumerable<Subject> GetSubjectNotRegister(int studentId);
        Subject CreateSubject(Subject newSubject);
        void UpdateSubject(Subject subject);
        void DeleteSubject(Subject subject);
    }
}
