using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyStore
{
    
    internal class GlobalData
    {
        private bool adminPrivilege = false;
        public GlobalData() { }

        public bool AdminPrivilege {  get { return adminPrivilege; } 
            set { adminPrivilege = value; }
        }
    }

}
