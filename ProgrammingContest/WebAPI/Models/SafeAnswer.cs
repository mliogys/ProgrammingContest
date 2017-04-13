using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammingContest.WebAPI.Models
{
    public class SafeAnswer
    {
        public string TeamName { get; set; }
        public bool correct { get; set; }
        public bool reload { get; set; }
    }
}