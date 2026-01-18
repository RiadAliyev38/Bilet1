using Bilet1.Areas.Admin.ViewModels.Designations;
using Bilet1.DAL;
using Bilet1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bilet1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DesignationController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public DesignationController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Designation> designation =  await _appDbContext.Designations.ToListAsync();
            return View(designation);
        }

        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDesignationVM designationVM)
        {
            if(!ModelState.IsValid)
            {
                return View(designationVM);
            }

            Designation designation=new Designation()
            {
                Name = designationVM.Name,
            };

            await _appDbContext.AddAsync(designation);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Update(int? id)
        {
            if(id==null || id < 0)
            {
                return BadRequest();
            }
            var designation = await  _appDbContext.Designations.FirstOrDefaultAsync(d=>d.Id==id);

            if(designation == null)
            {
                return NotFound();
            }

            UpdateDesignationVM designationVM = new UpdateDesignationVM()
            {
                Name = designation.Name,
            };


            return View(designationVM);

        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateDesignationVM designationVM)
        {
            if (!ModelState.IsValid) 
            {
                return View(designationVM);
            } 
            var dbdesignation = await _appDbContext.Designations.FirstOrDefaultAsync(d => d.Id == id);

            if(dbdesignation == null)
            {
                return NotFound();
            }

            if(await _appDbContext.Designations.AnyAsync(d => d.Name == designationVM.Name && d.Id != dbdesignation.Id))
            {
                ModelState.AddModelError("Name", "Bu ad artiq isledilir.");
                return View(designationVM);
            }

            dbdesignation.Name = designationVM.Name;

            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int id)
        {
            var designation = await _appDbContext.Designations.Include(d=>d.Members).FirstOrDefaultAsync(d=>d.Id==id);

            if (designation == null)
            {
                return NotFound();
            }

            

            return View(designation);

        }


        public async Task<IActionResult> Delete(int id)
        {
            var designation = await _appDbContext.Designations.FirstOrDefaultAsync(d=>d.Id==id);
            if(designation == null )
            {

                return NotFound();
            }

            _appDbContext.Designations.Remove(designation);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
