using Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Manage.Controllers
{
    public class UserController : Controller
    {
        private DatabaseContext dbContext;
        public UserController(DatabaseContext _dbContext)
        {
            dbContext = _dbContext;
        }



        public Users GetUserByRefferalCode(String code)
        {
            String data = code.ToUpper();
            var user = dbContext.Users.FirstOrDefault(u=>u.ReferralCode == code);
            if (user != null)
            {
                return user;
            }
            else { return null; }
        }

        public Users getDetailFindId(int id)
        {
            var user = dbContext.Users.Find(id);
            if (user != null)
            {
                return user;
            }
            else { return null; }
        }

        public List<Users> getDetailFindByRefferalId(int id)
        {
            var user = dbContext.Users.Where(u => u.ReferredBy == id).ToList();
            if (user != null)
            {
                var allRefferedUsers = new List<Users>(user);
                foreach (var item in user)
                {
                    allRefferedUsers.AddRange(getDetailFindByRefferalId(item.Id));
                }
                return allRefferedUsers;
            }
            else { return null; }
        }

        public IActionResult Index()
        {   
            var list = dbContext.Users.ToList();
            return View(list);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users users)
        {
            try
            {
                if (ModelState.IsValid) {
                    users.CreatedAt = DateTime.Now;
                    users.UpdatedAt = DateTime.Now;

                    if (users.ReferralCode != null)
                    {
                        var checkCodeUser = GetUserByRefferalCode(users.ReferralCode);
                        if (checkCodeUser == null)
                        {
                            return View(users);
                        }
                        else
                        {
                            users.ReferredBy = checkCodeUser.Id;
                        }
                    }
                    users.ReferralCode = Helper.GenerateRandomCode();

                    dbContext.Users.Add(users);
                    dbContext.SaveChanges();
                    return RedirectToAction("Index");
                } else
                {
                    return View(users);
                }
                
            }
            catch (Exception)
            {

                throw;
            }  
        }

        public IActionResult Detail(int id)
        {
            
            var user = getDetailFindId(id);
            var list = getDetailFindByRefferalId(id);
            
            ViewBag.user = user;
            return View(list);
        }
    }
}
