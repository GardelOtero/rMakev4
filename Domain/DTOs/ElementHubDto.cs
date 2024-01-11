using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ElementHubDto
    {
        public Guid ElementId { get; set; }
        public Guid DocumentId { get; set; }
        public string Content { get; set; }
    }
}
