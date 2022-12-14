namespace ResumeManager.HelperFunction
{
    public static class ObjectStorageHelper
    {
        //Save --> Put

        public static bool PutObject(string filePath, IFormFile file)
        {
            bool imageUpload = false;
            string basePath = "C://ObjectStorage/";
            string fullPath = Path.Combine(basePath, filePath);

            string folder = Path.GetDirectoryName(fullPath);
            if(!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            if (!File.Exists(filePath))
            {
                //save image in local drive
                using(FileStream outputFileStream = new FileStream(fullPath, FileMode.Create))
                {
                    var fileBytes = ConvertToBytes(file);
                    outputFileStream.Write(fileBytes, 0, fileBytes.Length);
                    imageUpload = true;

                }
            }

            return imageUpload;
        }

        private static byte[] ConvertToBytes(IFormFile file)
        {
            byte[] bytes = null;

            BinaryReader reader = new BinaryReader(file.OpenReadStream());
            bytes = reader.ReadBytes((int)file.Length);
            return bytes;
        }

       public static ObjectStorageModel GetObject(string filePath)
        {
            string basePath = "C://ObjectStorage/";
            string fullPath = Path.Combine(basePath, filePath);

            ObjectStorageModel obj = new ObjectStorageModel();

            if (File.Exists(fullPath))
            {
                FileStream stream = System.IO.File.OpenRead(fullPath);
                obj.fileBytes = new byte[stream.Length];
                stream.Read(obj.fileBytes, 0, obj.fileBytes.Length);  
                stream.Close();

                var info = new System.IO.FileInfo(fullPath);
                if(info != null)
                {
                    obj.fileSize = (info.Length / 1000000);
                }
            }
            return obj;
        }
    }

    public class ObjectStorageModel
    {
        public byte[] fileBytes { get; set;}
        public long fileSize { get; set;}
    }
}
