using System.Collections.Generic;

namespace BLL.Interfaces.BLLEntities
{
    public class BLLUser
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IEnumerable<string> Roles { get; set; }
        
    }
}