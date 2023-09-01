
namespace rMakev2.DTOs
{
    public class File
    {
        public string url { get; set; } 
        public string height { get; set; }
    }

    public class SaveDTO
    {
        public int? success { get; set; }

        public File file { get; set; }
    }
}
