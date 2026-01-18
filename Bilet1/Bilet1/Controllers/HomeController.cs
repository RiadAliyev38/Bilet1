using Bilet1.DAL;
using Bilet1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bilet1.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Member> members = await _appDbContext.Members.Include(m=>m.Designation).ToListAsync();

            return View(members);
        }

        
    }
}
