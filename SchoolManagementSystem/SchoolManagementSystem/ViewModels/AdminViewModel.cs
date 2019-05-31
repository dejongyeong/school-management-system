using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.ViewModels
{
    public class AdminViewModel : RootViewModel
    {
        public Admin Admin { get; set; }

        public AdminViewModel()
        {
            // Initialise entitiy or inserts will fail
            Admin = new Admin
            {
                username = "admin",
                password = "admin123"
            };
        }
    }
}
