using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LighterApi.Data;
using LighterApi.Data.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LighterApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly LighterDbContext _lighterDbContext;
        public ProjectController(LighterDbContext lighterDbContext)
        {
            _lighterDbContext = lighterDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetListAsync(CancellationToken cancellationToken)
        {
            await _lighterDbContext.Query<AuditLog>().ToListAsync(); // 操作隐式的方式

            return await _lighterDbContext.Projects.ToListAsync(cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> CreateAsync([FromBody] Project project, CancellationToken cancellationToken)
        {
            project.Id = Guid.NewGuid().ToString();
            _lighterDbContext.Projects.Add(project);
            await _lighterDbContext.SaveChangesAsync();

            return StatusCode((int)HttpStatusCode.Created, project);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Project>> GetAsync(string id, CancellationToken cancellationToken)
        {
            var project = await _lighterDbContext.Projects.Include(p => p.Groups).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            await _lighterDbContext.Entry(project).Collection(p => p.Groups).LoadAsync(); //集合的导航属性
            await _lighterDbContext.Entry(project).Reference(p => p.Groups).LoadAsync();//单一的导航属性


            await _lighterDbContext.Projects.FromSqlRaw("select * from ").FirstOrDefaultAsync(); //查询原生的sql
            return Ok(project);
        }

        [HttpPatch]//对部分数据进行更新
        [Route("{id}/title")]
        public async Task<ActionResult<Project>> SetTitleAsync(string id, [FromQuery] string title, CancellationToken cancellationToken)
        {
            var project = await _lighterDbContext.Projects.Include(p => p.Groups).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            var projectGroup = await _lighterDbContext.ProjectGroups.Where(p => p.ProjectId == id).ToListAsync();

            project.Title = title;

            foreach (var group in projectGroup)
            {
                group.Name = $"{title} {group.Name}";
            }

            await _lighterDbContext.SaveChangesAsync();

            return project;
        }

        [HttpPatch]//对部分数据进行更新
        [Route("{id}/title")]
        public async Task<ActionResult<Project>> SetAsync(string id, CancellationToken cancellationToken)
        {
            var project = await _lighterDbContext.Projects.Include(p => p.Groups).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            var properties = _lighterDbContext.Entry(project).Properties.ToList();

            foreach (var query in HttpContext.Request.Query)
            {
                var property = properties.FirstOrDefault(p => p.Metadata.Name == query.Key);
                if (property == null) continue;

                var currentType = Convert.ChangeType(query.Value.ToString(), property.Metadata.ClrType);

                _lighterDbContext.Entry(project).Property(query.Key).CurrentValue = currentType;
                _lighterDbContext.Entry(project).Property(query.Key).IsModified = true;
            }

            await _lighterDbContext.SaveChangesAsync();

            return project;
        }

        [HttpPut]//对整个数据进行更新
        public async Task Test1()
        {

        }
    }
}
