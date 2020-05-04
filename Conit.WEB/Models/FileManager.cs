using System;
using System.IO;

namespace Conit.WEB.Models
{
    public class FileManager
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }

        public string GeneratePictureName(string directory)
        {
            var guid = Guid.NewGuid();
            var pictureName = guid.ToString() + ".png";
            var path = directory + pictureName;
            return path;
        }
    }
}