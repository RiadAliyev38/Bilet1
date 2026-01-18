using Bilet1.Models;
using Microsoft.Identity.Client;

namespace Bilet1.Areas.Admin.ViewModels.Members;

public class CreateMemberVM
{
    public string FullName { get; set; }
    public int DesignationId { get; set; }

    public IFormFile ImageFile { get; set; }
    public List<Designation> Designations { get; set; } = new List<Designation>();
}
