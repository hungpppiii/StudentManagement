using QuanLySV.Contants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySV.Models
{
    public class SubjectResult
    {
        public virtual int Id { get; set; }
        public virtual float ProcessScore { get; set; }
        public virtual float ComponentScore {  get; set; }
        public virtual Result Result { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}