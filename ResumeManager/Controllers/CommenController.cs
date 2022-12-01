using Microsoft.AspNetCore.Mvc;
using ResumeManager.Data;
using ResumeManager.HelperFunction;
using ResumeManager.Models;

namespace ResumeManager.Controllers
{

    public class CommenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommenController(ApplicationDbContext context)
        {
            _context = context;
        }

        public JsonResult BulkSaveUpload()
        {
            ObjectVM objVM = new ObjectVM();
            var file = Request.Form.Files[0];
            
            var filename = file.FileName;
            var extention = Path.GetExtension(filename);
            var fileSize = file.Length;

            string[] validImageExtension = { ".png", ".jpg", ".jpeg" };
            bool isImageValid = true;

            if (!validImageExtension.Contains(extention))            
                isImageValid = false;

            string filePath = "2/1/" +"Images/" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-ff") + "_" + filename;
            string basePath = "C://ObjectStorage/";            
            string fullPath = Path.Combine(basePath, filePath);

            //save image in folder

            bool imageUpload = ObjectStorageHelper.PutObject(filePath, file);

            //save image in database
            if (imageUpload)
            {
                Image imgObj = new Image() { Name = filename, Extension = extention, FileSize = fileSize, FilePath = filePath, FileType = "Image" };
                _context.Images.Add(imgObj);
                _context.SaveChanges();

                var objImage = ObjectStorageHelper.GetObject(filePath);
                if (objImage != null)
                {
                    objVM.ImageId = imgObj.ImageId;
                    objVM.FilePath = Convert.ToBase64String(objImage.fileBytes);
                }
               // ImgId = imgObj.ImageId;
            }
            return Json(objVM);
        }
    }
}
