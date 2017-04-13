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
    public class ContestantsController : ApiController
    {

        [HttpGet]
        [Route("api/contestants/{ID}")]
        public IHttpActionResult Get(int ID)
        {
            var data = (new ContestantsRepository()).GetContestantID(ID);

            if (data == null)
                return BadRequest();
            else
                return Ok(data);
        }
    }
}