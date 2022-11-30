using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResumeManager.Data;
using ResumeManager.Models;

namespace ResumeManager.Controllers
{

    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetEmployeelist()
        {
            var list = _context.Departments.ToList();
            ViewBag.departmentList = new SelectList(list, "Did", "Name");
            return PartialView();
        }

        [HttpGet]
        public IActionResult EmployeeList()
        {
            try
            {
                var list = _context.Employees.Include(m => m.Departments).Select(x => new
                {
                    EmpId = x.EmpId,
                    FullName = x.FullName,
                    Email = x.Email,
                    Contact = x.Contact,
                    Address = x.Address,
                    Department = x.Departments.Name
                }).ToList();
                return Json(new JsonResponse() { IsSuccess = true, Data = list });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new JsonResponse() { IsSuccess = false, Message = msg });
            }
        }

        [HttpGet]
        public IActionResult Edit(int empId)
        {
            try
            {
                var obj = _context.Departments.Find(empId);
                return Json(new JsonResponse() { IsSuccess = true, Data = obj });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new JsonResponse() { IsSuccess = false, Message = msg });
            }
        }


        [HttpPost]
        public IActionResult SaveEmployee(Employee model)
        {
            try
            {
                if (model.Did > 0)
                {
                    _context.Employees.Update(model);
                    _context.SaveChanges();
                }
                else
                {
                    _context.Employees.Add(model);
                    _context.SaveChanges();
                }
                return Json(new JsonResponse() { IsSuccess = true, Message = "Record Save Successfully", Data = new
                {
                    EmpId = model.EmpId,
                    FullName = model.FullName,
                    Email = model.Email,
                    Contact = model.Contact,
                    Address = model.Address,
                    Department = _context.Departments.Where(x=>x.Did==model.Did).FirstOrDefault().Name
                }
                });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new JsonResponse() { IsSuccess = false, Message = msg });
            }
        }
    }
}
