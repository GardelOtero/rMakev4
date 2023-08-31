using Newtonsoft.Json;

namespace rMakev2.DTOs
{
    public class File
    {
        [JsonProperty("url")]
        public string url { get; set; } 
    }

    public class SaveDTO
    {
        [JsonProperty("success")]
        public int? success { get; set; }

        [JsonProperty("file")]
        public File file { get; set; }
    }
}
