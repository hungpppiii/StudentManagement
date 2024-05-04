using FluentNHibernate.Mapping;
using QuanLySV.Models;

public class SubjectResultMap : ClassMap<SubjectResult>
{
    public SubjectResultMap()
    {
        Table("SubjectResults");

        Id(x => x.Id).Column("Id");
        Map(x => x.ProcessScore).Column("ProcessScore");
        Map(x => x.ComponentScore).Column("ComponentScore");
        Map(x => x.Result).Column("Result");
        References(x => x.Student, "StudentId");
        References(x => x.Subject, "SubjectId");
    }
}