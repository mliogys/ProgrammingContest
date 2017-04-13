using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammingContest.WebAPI.Models
{
    public class Question
    {
        public string orderNo { get; set; }
        public int questionID { get; set; }
        public string questionText { get; set; }
        public bool isSolved { get; set; }
        public string answer { get; set; }
        public string Image { get; set; }
    }
}