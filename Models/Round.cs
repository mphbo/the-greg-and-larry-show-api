using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace the_greg_and_larry_show_api.Models
{
    public class Round
    {
        public int Id { get; set; }
        public int Player_Id { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
    }
}