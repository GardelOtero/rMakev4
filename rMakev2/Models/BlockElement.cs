namespace rMakev2.Models
{
    public class BlockElement
    {
        public string id { get; set; }
        public string type { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string text { get; set; }
    }

    public class Root
    {
        public long time { get; set; }
        public List<BlockElement> blocks { get; set; }
        public string version { get; set; }
    }

}
