using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Helpers
{
    public  class FileUpload
    {
        private static IHostingEnvironment hostingEnvironment;

        public FileUpload(IHostingEnvironment _hostingEnvironment)
        {
            hostingEnvironment = _hostingEnvironment;
        }

        public  string EnsureCorrectFilename(string filename,string wanted)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            var uzanti = filename.Split('.')[1];
            var name = wanted + "." + uzanti;

            return name;
        }

        public  string GetPathAndFilename(string filename,string _path)
        {
            string path = hostingEnvironment.WebRootPath + _path;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path + filename;
        }
    }
}
