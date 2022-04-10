using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LighterApi.Data
{
    [NotMapped]  //排除类
    public class AuditLog : Entity
    {
    }
}
