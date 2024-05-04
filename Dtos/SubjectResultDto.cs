using QuanLySV.Contants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySV.Dtos
{
    public class SubjectResultDto
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int NumberOfLesson { get; set; }
        public float ComponentScoreRatio { get; set; }
        public int ResultId { get; set; }
        public float ComponentScore { get; set; }
        public float ProcessScore { get; set; }
        public Result Result { get; set; }
    }
}