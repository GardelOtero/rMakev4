using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.SaveDTO
{

    public class SavePortfolioDTO

    {

        public string id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public List<SaveProjectDTO> Projects { get; set; }

        public DateTime CreationDate { get; set; }




    }


    public class SaveProjectDTO

    {

        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public string PortfolioId { get; set; }

        public List<string> ParentId { get; set; }

        public List<SaveDocumentDTO> Documents { get; set; }

        public string Author { get; set; }




    }

    public class SaveDocumentDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string ProjectId { get; set; }
        public int OrderId { get; set; }
        public List<string> ParentId { get; set; }
        public List<SaveElementDTO> Elements { get; set; }
        public string GitSha { get; set; }
        public string ImageLink { get; set; }
        
        public string Tag { get; set; }
    }

    public class SaveElementDTO
    {
        public string Id { get; set; }
        public BlockContent Block { get; set; }
        public DateTime CreationDate { get; set; }
        public string Author { get; set; }
        public string DocumentId { get; set; }
        public int OrderId { get; set; }
        public string ParentElement { get; set; }
        public string NestParent { get; set; }
        public string HashString { get; set; }


    }

    public class BlockContent
    {
        public string id { get; set; }
        public string type { get; set; }
        public Data data { get; set; }
    }



    public class Data
    {
        public string text { get; set; }
        public int? level { get; set; }
        public string? caption { get; set; }
        public string? alignment { get; set; }
        public string? url { get; set; }
        public File? file { get; set; }
        public bool? withBorder { get; set; }
        public bool? withBackground { get; set; }
        public bool? stretched { get; set; }
        public string? style { get; set; }
        public List<ItemDTO>? items { get; set; }
        public string? link { get; set; }
        public Meta? meta { get; set; }
        public string? code { get; set; }
        public bool? withHeadings { get; set; }
        public List<List<string>>? content { get; set; }
    }



    public class File
    {
        public string url { get; set; }
        public string? height { get; set; }
    }



    public class ItemDTO
    {
        public string content { get; set; }
        public List<ItemDTO> items { get; set; }
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
        public List<BlockContent> blocks { get; set; }
        public string version { get; set; }
    }

}