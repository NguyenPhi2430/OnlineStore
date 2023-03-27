using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels.System
{
    public class UserViewModel
    {
        [Display(Name = "BirthDay")]
        public DateTime BirthDay { get; set; }
        [Display(Name = "First Name")]
        public string FirstName {  get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        public int Id { get; set; }
    }
}
