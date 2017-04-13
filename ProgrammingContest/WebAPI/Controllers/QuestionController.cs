using System.Web.Http;
using ProgrammingContest.WebAPI.Repositories;
using ProgrammingContest.WebAPI.Models;

namespace ProgrammingContest.WebAPI.Controllers
{
    public class QuestionController : ApiController
    {
        [HttpGet]
        [Route("api/questions/{ID}")]
        public IHttpActionResult GetTeamQuestions(int ID)
        {
            return Ok((new QuestionRepository()).getTeamQuestions(ID));
        }

        [HttpPost]
        [Route("api/questions/{ID}/answer")]
        public IHttpActionResult GetTeamQuestions(int ID, Answer answer)
        {

            return Ok(true);
        }

        [HttpGet]
        [Route("api/questions/{ID}/hint")]
        public IHttpActionResult GetQuestionHint(int ID)
        {
            return Ok((new QuestionRepository()).GetHint(ID));
        }

        [HttpGet]
        [Route("api/questions/safe")]
        public IHttpActionResult GetSafeQuestions()
        {

            return Ok((new QuestionRepository()).GetSafeQuestions());
        }
    }
}