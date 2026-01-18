using Bilet1.Models;

namespace Bilet1.Areas.Admin.ViewModels.Members;

public class UpdateMemberVM
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int DesignationId { get; set; }
    public IFormFile? ImageFile { get; set; }
    public List<Designation> Designations { get; set; }= new List<Designation>();

}
