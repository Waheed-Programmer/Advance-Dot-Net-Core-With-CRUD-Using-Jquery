using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResumeManager.Data;
using ResumeManager.Models;

namespace ResumeManager.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.UserApps.ToList());
        }
        [HttpGet]
        public IActionResult CreateOrEdit(int Id)
        {
            UserApp user = new UserApp();

            if (Id > 0)
            {
                user = _context.UserApps.Find(Id);
            }
            
            //City List for Dropdown
            
            var listItem = new SelectList(_context.Cities.ToList(), "CityId", "CityName");
            ViewBag.CityList = listItem;

            //Gender List for RadioButton

            user.Genderlist = new SelectList(_context.Genders.ToList(), "GenderId", "GenderName");

            //Interset List for CheckBox

            //user.Interestlist = new SelectList(_context.Interests.ToList(), "InterestId", "InterestName");

            List<SelectListItem> list = new List<SelectListItem>();

            var interestList = _context.Interests.ToList();

            var userInterestList = _context.UserInterests.Where(x=>x.UserId==Id).ToList();

            foreach (var item in interestList)
            {
            SelectListItem element = new SelectListItem() 
            { 
                Text= item.InterestName.ToString(),
                Value= item.InterestId.ToString(),
                Selected =userInterestList.Where(x=>x.InterestId==item.InterestId).Count()>0 ? true:false
            }; 

             list.Add(element);
                
            }
            user.Interestlist = list;

            return View(user);
        }

        [HttpPost]
        public IActionResult CreateOrEdit(UserApp model)
        {
            model.Interestlist = model.Interestlist.Where(x => x.Selected == true).ToList();
                if (model.UserId > 0)
                {
                    _context.UserApps.Update(model);
                    _context.SaveChanges();

                //remove CheckBox
                var nList = _context.UserInterests.Where(x=>x.UserId==model.UserId).ToList();
                _context.UserInterests.RemoveRange(nList);
                _context.SaveChanges();

                //Insert CheckBox
                List<UserInterest> uList = new List<UserInterest>();
                foreach (var item in model.Interestlist)
                {
                    uList.Add(new UserInterest() { UserId = model.UserId, InterestId = Convert.ToInt32(item.Value) });
                }
                _context.UserInterests.AddRange(uList);
                _context.SaveChanges();
            
                }
                else
                {
                    _context.UserApps.Add(model);
                    _context.SaveChanges();

                List<UserInterest> uList = new List<UserInterest>();
                foreach (var item in model.Interestlist)
                {
                    uList.Add(new UserInterest() { UserId = model.UserId, InterestId = Convert.ToInt32(item.Value) });
                }
                _context.UserInterests.AddRange(uList);
                _context.SaveChanges();
                }
           
            return RedirectToAction(nameof(Index));
        }

    }
}
