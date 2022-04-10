using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LighterApi.Data.Project
{
    public class Project : Entity
    {
        public string Title { get; set; }
        public List<ProjectGroup> Groups { get; set; } // 导航属性，一对多的

        public List<SubjectProject> SubjectProjects { get; set; } //多对多的导航属性
    }
}
