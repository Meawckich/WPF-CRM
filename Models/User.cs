using System.Collections.Generic;

namespace BuildingMaterials.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Login{ get; set; }
        public string Password { get; set; }
        public int RoleId{ get; set; }

        public virtual Role Role { get; set; }
    }
}
