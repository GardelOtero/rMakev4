﻿
namespace rMakev2.Models
{
    public class BlockElement
    {
        public BlockElement()
        {
            elementId = Guid.NewGuid().ToString();
        }

        public HashSet<string> Authors { get; set; } = new HashSet<string>();
        public string id { get; set; }
        public string type { get; set; }
        public Data data { get; set; }
        public string elementId { get; set; }
    }

    public class Data
    {
        public string text { get; set; }
        public int? level { get; set; }
        public string caption { get; set; }
        public string alignment { get; set; }
        public string url { get; set; }
        public File file { get; set; }
        public bool? withBorder { get; set; }
        public bool? withBackground { get; set; }
        public bool? stretched { get; set; }
        public string style { get; set; }
        public List<Item> items { get; set; }

        public string? link { get; set; }
        public Meta? meta { get; set; }
        
        public string code { get; set; }
        
        public bool? withHeadings { get; set; }
        public List<List<string>>? content { get; set;}
    }

    public class File
    {
        public string url { get; set; }
        public string? height { get; set; }
    }

    public class Item
    {
        public string content { get; set; }
        public List<object> items { get; set; }
        public string text { get; set; }
        public bool? @checked { get; set; }
    }

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


    public class Root
    {
        public long time { get; set; }
        public List<BlockElement> blocks { get; set; }
        public string version { get; set; }
    }



}
