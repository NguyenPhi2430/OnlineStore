using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.Data.Entities
{
    public class Role : IdentityRole<int>
    {
        public string Desc { get; set; }
    }
}
