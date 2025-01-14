﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using rMakev2.DTOs;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using System.Net.Http.Headers;

namespace rContentMan.Controllers
{
    [Route("[controller]")]
    [EnableCors]
    public class AssetsController : ControllerBase
    {

        private readonly IWebHostEnvironment env;
        private readonly ILogger<AssetsController> logger;

        public AssetsController(IWebHostEnvironment env, ILogger<AssetsController> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        // Post image
        [Consumes("multipart/form-data")]
        [HttpPost]
        public string UploadImage()
        {
            IFormFile file = Request.Form.Files.First();
            SaveDTO newroot = new SaveDTO();
            rMakev2.DTOs.File newfile = new rMakev2.DTOs.File();

            string objstr = "";

            var options = new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreReadOnlyProperties = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,

            };
            


            if (file == null)
            {

                newfile.url = "";

                newroot.success = 0;
                newroot.file = newfile;

                objstr = System.Text.Json.JsonSerializer.Serialize<SaveDTO>(newroot, options);

                return objstr;
            }


            var type = MediaTypeWithQualityHeaderValue.Parse(file.ContentType).MediaType;



            var homePath = env.WebRootPath + "/Assets/img/";

            var fileName = "";

            if(type == "image/svg+xml")
            {
                fileName = Path.GetFileName(Guid.NewGuid() + ".svg");

            }
            else
            {

                fileName = Path.GetFileName(Guid.NewGuid() + ".png");
            }


            string filePath = Path.Combine(homePath, fileName);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }


            var root = env.IsDevelopment() ? "https://localhost:7267/" : "https://rcontentman.azurewebsites.net/";

            newfile.url = root + "Assets/" + fileName;

            newroot.success = 1;
            newroot.file = newfile;


            objstr = System.Text.Json.JsonSerializer.Serialize<SaveDTO>(newroot, options);

            return objstr;
        }

        
        [HttpGet("{name}")]
        public IActionResult GetImage(string name)
        {

            var homePath = env.WebRootPath + "/Assets/img/";

            var filePath = Path.Combine(homePath, name);

            var type = "";

            if(name.Substring(name.LastIndexOf('.') + 1) == "svg")
            {
                type = "image/svg+xml";
            } 
            else
            {
                type = "image/*";
            }

            return PhysicalFile(filePath, type);


        }
    
    }
}
