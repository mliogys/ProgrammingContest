using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProgrammingContest.WebAPI.Repositories;
using ProgrammingContest.WebAPI.Models;
using ProgrammingContest.App.SignalR;
using Microsoft.AspNet.SignalR;

namespace ProgrammingContest.WebAPI.Controllers
{
    public class TeamController : ApiController
    {

        [HttpGet]
        [Route("api/teams")]
        public IHttpActionResult Get()
        {
            return Ok((new TeamRepository()).GetTeams());
        }

        [HttpGet]
        [Route("api/teams/{ID}/points")]
        public IHttpActionResult GetPoints(int ID)
        {
            return Ok((new TeamRepository()).GetTeamPoints(ID));
        }

        [HttpGet]
        [Route("api/teams/{ID}")]
        public IHttpActionResult GetTeam(int ID)
        {
            var data = (new TeamRepository()).GetTeam(ID);
            if (data == null)
                return BadRequest();
            else
                return Ok(data);
        }

        [HttpPost]
        [Route("api/teams/{ID}/contestants")]
        public IHttpActionResult AddContestant(int ID, Contestant contestant)
        {
            return Ok((new TeamRepository()).AddContestant(ID, contestant.Name, contestant.Surname));
        }

        [HttpGet]
        [Route("api/teams/{ID}/contestants")]
        public IHttpActionResult GetContestants(int ID)
        {
            return Ok((new TeamRepository()).GetContestants(ID));
        }

        [HttpPost]
        [Route("api/teams/{teamID}/questions/{questionID}/answer")]
        public IHttpActionResult CheckAnswer(int teamID, int questionID, Answer answer)
        {
            return Ok((new TeamRepository()).checkAnswer(teamID, questionID, answer.Text));
        }

        [HttpPost]
        [Route("api/teams/{teamID}/safe")]
        public IHttpActionResult CheckSafe(int teamID, Answer code)
        {
            SafeAnswer answer = (new TeamRepository()).checkSafe(teamID, code.Text);
            if (answer.TeamName != null)
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<SafeHub>();
                context.Clients.All.hello(answer.correct, answer.TeamName);
            }

            return Ok(new { correct = answer.correct, reload = answer.reload });
        }

    }
}