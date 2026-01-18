using Bilet1.Models.Base;

namespace Bilet1.Models
{
    public class Designation:BaseEntity
    {
        public string Name { get; set; }

        public List<Member> Members { get; set; } = new List<Member>();
    }
}
