using Bilet1.Areas.Admin.ViewModels.Members;
using Bilet1.DAL;
using Bilet1.Models;
using Bilet1.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bilet1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MemberController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public MemberController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Member> members = await  _appDbContext.Members.Include(m=>m.Designation).ToListAsync();
            return View(members);
        }


        public async Task<IActionResult> Create()
        {
            List<Designation> designations = await _appDbContext.Designations.ToListAsync();

            CreateMemberVM createMemberVM = new CreateMemberVM()
            {
                Designations = designations
            };
            return View(createMemberVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberVM  createMemberVM)
        {
            if (!ModelState.IsValid)
            {
                createMemberVM.Designations = await _appDbContext.Designations.ToListAsync();
                return View(createMemberVM);
            }

            Member member =  new Member()
            {
                FullName=createMemberVM.FullName,
                DesignationId =createMemberVM.DesignationId,
                ImageUrl = await createMemberVM.ImageFile.CreateFile(_env.WebRootPath,"img")
            };

            await _appDbContext.Members.AddAsync(member);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            Member? member = await _appDbContext.Members.FirstOrDefaultAsync(m=> m.Id==id);
            if(member == null)
            {
                return NotFound();
            }

            UpdateMemberVM updateMemberVM = new UpdateMemberVM()
            {
                Id = member.Id,
                FullName = member.FullName,
                DesignationId = member.DesignationId,
                Designations=await _appDbContext.Designations.ToListAsync(),
            };



            return View(updateMemberVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id ,UpdateMemberVM updateMemberVM)
        {
            if (!ModelState.IsValid)
            {
                updateMemberVM.Designations= await _appDbContext.Designations.ToListAsync();
                return View(updateMemberVM);
            }

            Member? member = await _appDbContext.Members.FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            member.FullName = updateMemberVM.FullName;
            member.DesignationId = updateMemberVM.DesignationId;
            if(updateMemberVM.ImageFile != null)
            {
                member.ImageUrl= await updateMemberVM.ImageFile.CreateFile(_env.WebRootPath, "img");
            }
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        public IActionResult Details()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            return View();
        }



    }
}
