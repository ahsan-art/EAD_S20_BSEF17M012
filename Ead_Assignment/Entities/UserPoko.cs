using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserPoko
    {
        public int UserID { get; set; }

        public String Name { get; set; }

        public String Login { get; set; }

        public String Password { get; set; }

        public String Email { get; set; }

        public String Gender { get; set; }

        public String Address { get; set; }

        public int Age { get; set; }

        public String NIC { get; set; }

        public DateTime DOB { get; set; }

        public bool isCricket { get; set; }

        public bool isHockey { get; set; }

        public bool Chess { get; set; }

        public String ImageName { get; set; }

        public DateTime createdOn { get; set; }

        public String createdBy { get; set; }

        public DateTime modifiedOn { get; set; }

        public String modifiedBy { get; set; }

        public bool isActive { get; set; }
    }
}
