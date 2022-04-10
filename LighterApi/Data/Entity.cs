using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LighterApi.Data
{
    public class Entity
    {
        [Key]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string TenantId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //自增的方式
        public DateTime Inserted { get; set; }
    }
}
