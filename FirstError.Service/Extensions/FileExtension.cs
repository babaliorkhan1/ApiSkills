using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstError.Service.Extensions
{
    public static class FileExtension
    {
        public static bool IsImage(this IFormFile formFile)
        {
            return formFile.ContentType.Contains("image");
        }
        public static bool IsSizeOk(this IFormFile formFile,int mb)
        {
            return formFile.Length / 1024 / 1024 <= mb;
        }
        public static string SaveFile(this IFormFile formFile, string root,string path)
        {
            string FileName= Guid.NewGuid().ToString() + formFile.FileName;
            string FullPath=Path.Combine(root,path,FileName);

            using (FileStream file=new FileStream(FullPath, FileMode.Create))
            {
                formFile.CopyTo(file);
            }
            return FileName;
        }
    }
}
