using System.Collections.Generic;

namespace BuildingMaterials.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
