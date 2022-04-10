using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LighterApi.Data.Project
{
    /// <summary>
    /// 多对多的中间实体
    /// </summary>
    public class SubjectProject : Entity
    {
        public string ProjectId { get; set; }
        public Project Project { get; set; }

        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
