using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammingContest.WebAPI.Models
{
    public class StageInfo
    {
        public int stage { get; private set; }
        public int eventID { get; private set; }
        public int secondsToEvent { get; private set; }
        public int secondsAfterEvent { get; private set; }

        public StageInfo(int stage, int eventID, int secondsTo, int secondsAfter)
        {
            this.stage = stage;
            this.eventID = eventID;
            secondsToEvent = secondsTo + 2;
            secondsAfterEvent = secondsAfter;
        }
    }
}