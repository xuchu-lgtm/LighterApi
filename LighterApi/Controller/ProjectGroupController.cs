using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LighterApi.Data;
using LighterApi.Data.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LighterApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectGroupController : ControllerBase
    {
        private readonly LighterDbContext _lighterDbContext;
        public ProjectGroupController(LighterDbContext lighterDbContext)
        {
            _lighterDbContext = lighterDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectGroup projectGroup)
        {
            //if (!ModelState.IsValid)
            //{
            //    return ValidationProblem();
            //}
            _lighterDbContext.ProjectGroups.Add(projectGroup);
            await _lighterDbContext.SaveChangesAsync();

            return StatusCode((int)HttpStatusCode.Created, projectGroup);
        }
    }
}
