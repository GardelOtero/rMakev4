using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using rContentMan.Models;
using HtmlAgilityPack;
using Domain.DTOs;
using System.Text.Json.Serialization;
using System.Text.Json;
using rMakev2.DTOs;
using System.Net.NetworkInformation;

namespace rContentMan.Controllers
{

    [Route("[controller]")]
    [EnableCors]
    public class LinkController : ControllerBase
    {


        [HttpGet]
        public string GetUrlData(string url)
        {


            var webGet = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

            LinkDTO dto = new LinkDTO();

            Meta metadto = new Meta();

            var options = new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreReadOnlyProperties = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,

            };

            try
            {
                document = webGet.Load(url);

            } catch
            {

                dto.success = 0;
                dto.meta = metadto;


                string errorstr = System.Text.Json.JsonSerializer.Serialize(dto, options);

                return errorstr;

            }

            var metaTags = document.DocumentNode.SelectNodes("//meta");
            var title = document.DocumentNode.SelectSingleNode("//title");
            MetaInformation metaInfo = new MetaInformation(url);




            if (metaTags != null)
            {
                int matchCount = 0;
                foreach (var tag in metaTags)
                {
                    var tagName = tag.Attributes["name"];
                    var tagContent = tag.Attributes["content"];
                    var tagProperty = tag.Attributes["property"];
                    if (tagName != null && tagContent != null)
                    {
                        switch (tagName.Value.ToLower())
                        {
                            case "title":
                                metaInfo.Title = tagContent.Value;
                                matchCount++;
                                break;
                            case "description":
                                metaInfo.Description = tagContent.Value;
                                matchCount++;
                                break;
                            case "twitter:title":
                                metaInfo.Title = string.IsNullOrEmpty(metaInfo.Title) ? tagContent.Value : metaInfo.Title;
                                matchCount++;
                                break;
                            case "twitter:description":
                                metaInfo.Description = string.IsNullOrEmpty(metaInfo.Description) ? tagContent.Value : metaInfo.Description;
                                matchCount++;
                                break;
                            case "keywords":
                                metaInfo.Keywords = tagContent.Value;
                                matchCount++;
                                break;
                            case "twitter:image":
                                metaInfo.ImageUrl = string.IsNullOrEmpty(metaInfo.ImageUrl) ? tagContent.Value : metaInfo.ImageUrl;
                                matchCount++;
                                break;
                        }
                    }
                    else if (tagProperty != null && tagContent != null)
                    {
                        switch (tagProperty.Value.ToLower())
                        {
                            case "og:title":
                                metaInfo.Title = string.IsNullOrEmpty(metaInfo.Title) ? tagContent.Value : metaInfo.Title;
                                matchCount++;
                                break;
                            case "og:description":
                                metaInfo.Description = string.IsNullOrEmpty(metaInfo.Description) ? tagContent.Value : metaInfo.Description;
                                matchCount++;
                                break;
                            case "og:image":
                                metaInfo.ImageUrl = string.IsNullOrEmpty(metaInfo.ImageUrl) ? tagContent.Value : metaInfo.ImageUrl;
                                matchCount++;
                                break;
                        }
                    }
                }
                metaInfo.HasData = matchCount > 0;
            }

            

            dto.success = 1;

            metadto.title = title.InnerText;
            metadto.description = metaInfo.Description;

            metadto.image = new Image();
            metadto.image.url = metaInfo.ImageUrl;

            dto.meta = metadto;


            string objstr = System.Text.Json.JsonSerializer.Serialize(dto, options);

            return objstr;

        }




    }
}
