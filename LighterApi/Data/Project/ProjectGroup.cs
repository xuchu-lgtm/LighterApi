using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LighterApi.Data.Project
{
    public class ProjectGroup
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ProjectId { get; set; } //导航属性

        public Project Project { get; set; } //反向属性
    }
}
