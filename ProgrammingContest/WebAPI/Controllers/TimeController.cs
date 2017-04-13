using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProgrammingContest.WebAPI.Repositories;
using ProgrammingContest.WebAPI.Models;

namespace ProgrammingContest.WebAPI.Controllers
{
    public class TimeController : ApiController
    {
        [HttpGet]
        [Route("api/times/eventstarting/{eventID}")]
        public IHttpActionResult GetStartingTime(int eventID)
        {
            return Ok((new TimeRepository()).GetSecondsTillStart(eventID));
        }

        [HttpGet]
        [Route("api/times/eventending/{eventID}")]
        public IHttpActionResult GetEndingTime(int eventID)
        {
            return Ok((new TimeRepository()).GetSecondsTillEnd(eventID));
        }
    }
}