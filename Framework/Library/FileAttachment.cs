using System.IO;

namespace Common.Tools
{
    public class FileAttachment
    {
        public string Name { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public static FileAttachment FromFile(string name, string filePath, string contentType = null)
        {
            return new FileAttachment
            {
                Name = name,
                FileName = Path.GetFileName(filePath),
                ContentType = contentType,
                Content = File.ReadAllBytes(filePath)
            };
        }
    }
}