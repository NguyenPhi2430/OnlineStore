using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public User User { get; set; }
    }
}
