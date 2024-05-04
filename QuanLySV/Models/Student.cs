using QuanLySV.Contants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySV.Models
{
    public class Student
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual Class Class { get; set; }
        public virtual string AcademyYear {  get; set; }
        public virtual IEnumerable<SubjectResult> SubjectResults { get; set; }
    }
}