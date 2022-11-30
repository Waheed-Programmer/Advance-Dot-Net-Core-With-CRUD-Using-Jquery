using Microsoft.AspNetCore.Mvc;
using ResumeManager.Data;
using ResumeManager.Models;

namespace ResumeManager.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetDepartmentlist()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult DepartmentList()
        {
            try
            {
                var list = _context.Departments.ToList();
                return Json(new JsonResponse() { IsSuccess = true, Data = list });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException!= null ? ex.InnerException.Message : ex.Message;
                return Json(new JsonResponse() { IsSuccess = false, Message = msg });
            }
        }

        [HttpGet]
        public IActionResult Edit(int did)
        {
            try
            {
                var obj = _context.Departments.Find(did);
                return Json(new JsonResponse() { IsSuccess = true, Data = obj });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new JsonResponse() { IsSuccess = false, Message = msg });
            }
        }


        [HttpPost]
        public IActionResult SaveDepartment(Department model)
        {
            try
            {
                if (model.Did > 0)
                {
                    _context.Departments.Update(model);
                    _context.SaveChanges();
                }
                else
                {
                    _context.Departments.Add(model);
                    _context.SaveChanges();
                }
                return Json(new JsonResponse() { IsSuccess = true, Message = "Record Save Successfully", Data = model });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException!= null ? ex.InnerException.Message : ex.Message;
                return Json(new JsonResponse() { IsSuccess = false, Message = msg });
            }
        }

    }
}
