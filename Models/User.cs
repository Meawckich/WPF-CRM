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
        public string Adress { get; set; }
        public virtual Role Role { get; set; }

        public User( string fullName, string login, string password, int roleId, string adress)
        {
            FullName = fullName;
            Login = login;
            Password = password;
            RoleId = roleId;
            Adress = adress;
        }

        public ICollection<Order> Orders { get; set; }
    }
}
