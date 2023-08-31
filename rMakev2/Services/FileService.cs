using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenAI.GPT3.Interfaces;
using rMakev2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using rMakev2.DTOs;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace rMakev2.Services
{

   

    [ApiController]
    [Route("[controller]")]
    public class FileService : ControllerBase
    {

        private readonly IWebHostEnvironment env;
        private readonly ILogger<FileService> logger;

        public FileService(IWebHostEnvironment env, ILogger<FileService> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        [Consumes("multipart/form-data")]
        [HttpPost("Save")]
        public string SaveImage()
        {

           

            IFormFile file = Request.Form.Files.First();
            SaveDTO newroot = new SaveDTO();
            DTOs.File newfile = new DTOs.File();

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

            string root = env.WebRootPath;

            string path = Path.Combine(root, "assets", "img");

            string fileName = Path.GetFileName(file.FileName);

            string filePath = Path.Combine(path, fileName);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }


            newfile.url = "assets/img/" + fileName;

            newroot.success = 1;
            newroot.file = newfile;


            objstr = System.Text.Json.JsonSerializer.Serialize<SaveDTO>(newroot, options);

            return objstr;
        }


    }
}
