using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace the_greg_and_larry_show_api.Models
{
    public class Round
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public int Score { get; set; } = 0;
        public int Level { get; set; } = 0;
        public bool IsSaved { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}