using QuanLySV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace QuanLySV.Services
{
    public interface ISubjectService
    {
        IEnumerable<Subject> GetSubjectNotRegister(int studentId);
        Subject CreateSubject(FormCollection collection);
        int UpdateSubject(int id, FormCollection collection);
        void DeleteSubject(int id);
    }
}
