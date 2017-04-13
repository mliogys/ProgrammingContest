using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammingContest.WebAPI.ViewModels
{
    public class ContestantViewModel
    {
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string teamName { get; set; }
        public int teamID { get; set; }
        public int contestantID { get; set; }
    }
}