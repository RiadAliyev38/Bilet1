using Bilet1.Models.Base;

namespace Bilet1.Models
{
    public class Member:BaseEntity
    {
        public string FullName { get; set; }
        public string ImageUrl { get; set; }

        public int DesignationId { get; set; } 
        public Designation Designation { get; set; }
    }
}
