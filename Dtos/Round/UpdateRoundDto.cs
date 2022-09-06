using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace the_greg_and_larry_show_api.Dtos.Round
{
    public class UpdateRoundDto
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsSaved { get; set; } = false;
    }
}