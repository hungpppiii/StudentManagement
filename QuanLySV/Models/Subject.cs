using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySV.Models
{
    public class Subject
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int NumberOfLesson { get; set; }
        public virtual float ComponentScoreRatio { get; set; }
        public virtual IEnumerable<SubjectResult> SubjectResults { get; set; }
    }
}