using FluentNHibernate.Mapping;
using QuanLySV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySV.Mapping
{
    public class SubjectMap : ClassMap<Subject>
    {
        public SubjectMap() 
        {
            Table("Subjects");

            Id(x => x.Id).Column("Id");
            Map(x => x.Name).Column("Name");
            Map(x => x.NumberOfLesson).Column("NumberOfLesson");
            Map(x => x.ComponentScoreRatio).Column("ComponentScoreRatio");
            HasMany(x => x.SubjectResults)
              .KeyColumn("SubjectId")
              .Inverse()
              .Cascade.AllDeleteOrphan();
        }
    }
}