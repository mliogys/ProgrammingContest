using System;
using System.Web.Mvc;
using ProgrammingContest.WebAPI.Repositories;
using ProgrammingContest.WebAPI.Models;
using System.Web;

namespace ProgrammingContest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.Cookies["TeamCookie"] != null)
            {
                var teamTitle = Server.HtmlEncode(Request.Cookies["TeamCookie"]["team"]);
                var data = teamTitle.Split(':');
                ViewBag.TeamTitle = data[1];
                ViewBag.Title = "Konkursas";

                StageInfo stage = (new TeamRepository()).GetStage(Convert.ToInt32(data[0]));

                if (stage.secondsToEvent <= 0 && stage.secondsAfterEvent < 0)
                {
                    if (stage.eventID == 1)
                    {
                        ViewBag.StageTitle = stage.stage + "-as sprintas";
                        return View("Content");
                    }
                    else
                    {
                        return View("Safe");
                    }
                }
                else if (stage.secondsToEvent > 0 && stage.secondsAfterEvent < 0)
                {
                    ViewBag.timeValue = stage.secondsToEvent.ToString();
                    return View("Waiting");
                }
                else
                {
                    if (Request.Cookies["TeamCookie"] != null)
                    {
                        HttpCookie myCookie = new HttpCookie("TeamCookie");
                        myCookie.Expires = DateTime.Now.AddDays(-1d);
                        Response.Cookies.Add(myCookie);
                    }
                    return View("Finished");
                }
            }
            else
            {
                return View("Index");
            }
        }


        public ActionResult Waiting()
        {
            if (Request.Cookies["TeamCookie"] == null)
            {
                return View("Index");
            }
            else
            {
                return View("Waiting");
            }
        }

        public ActionResult Content()
        {
            if (Request.Cookies["TeamCookie"] == null)
            {
                return View("Index");
            }
            else
            {
                return View("Content");
            }
        }

        public ActionResult Safe()
        {
            if (Request.Cookies["TeamCookie"] == null)
            {
                return View("Index");
            }
            else
            {
                return View("Safe");
            }
        }

        public ActionResult Finished()
        {
            if (Request.Cookies["TeamCookie"] == null)
            {
                return View("Index");
            }
            else
            {
                return View("Finished");
            }
        }
    }
}