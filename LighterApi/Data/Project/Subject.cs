using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LighterApi.Data.Project
{
    public class Subject : Entity
    {
        public string Title { get; set; }
        public string Context { get; set; }

        public List<SubjectProject> subjectProjects { get; set; } //多对多的导航属性
    }
}
