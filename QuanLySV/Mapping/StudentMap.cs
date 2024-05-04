using FluentNHibernate.Mapping;
using QuanLySV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySV.Mapping
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap() 
        {
            Table("Students");

            Id(x => x.Id).Column("Id");
            Map(x => x.Name).Column("Name");
            Map(x => x.Gender).Column("Gender");
            Map(x => x.DateOfBirth).Column("DateOfBirth");
            Map(x => x.Class).Column("Class");
            Map(x => x.AcademyYear).Column("AcademyYear");
            HasMany(x => x.SubjectResults)
              .KeyColumn("StudentId")
              .Inverse()
              .Cascade.AllDeleteOrphan();
        }
    }
}