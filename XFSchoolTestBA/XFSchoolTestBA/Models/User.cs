using System;
using System.Collections.Generic;
using System.Text;

namespace XFSchoolTestBA.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FullName { get; set; }
        public int Roles { get; set; }
        public string Password { get; set; }
    }
}
