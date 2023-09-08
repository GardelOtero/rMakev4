using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Image
    {
        public string? url { get; set; }
    }

    public class Meta
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public Image? image { get; set; }
    }

    public class LinkDTO
    {
        public int success { get; set; }
        public Meta meta { get; set; }
    }
}
