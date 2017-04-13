using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammingContest.WebAPI.Models
{
    public class Team
    {
        public int ID { get; private set; }
        public string Title { get; private set; }

        public Team(int ID, string Title)
        {
            this.ID = ID;
            this.Title = Title;
        }
    }
}