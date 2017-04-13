using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ProgrammingContest.WebAPI.Models;
using System.Data.SqlClient;
using System.Data;

namespace ProgrammingContest.WebAPI.Repositories
{
    public class TeamRepository
    {
        public Team GetTeam(int passwd)
        {
            Team team = null;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContestDB"].ConnectionString))
            {
                var cmd = new SqlCommand("select ID, title from Team where passwd=@passwd", conn);
                cmd.Parameters.AddWithValue("@passwd", passwd);
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    team = new Team(Convert.ToInt32(reader[0]), reader[1].ToString());
                reader.Close();
            }

            return team;
        }

        public int GetTeamPoints(int teamID)
        {
            int points = 0;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContestDB"].ConnectionString))
            {
                var cmd = new SqlCommand("select totalScore from TeamScore where teamID=@teamID", conn);
                cmd.Parameters.AddWithValue("@teamID", teamID);
                conn.Open();
                var reader = cmd.ExecuteScalar();
                if (reader != null)
                    points = Convert.ToInt32(reader);
            }

            return points;
        }

        public List<Team> GetTeams()
        {
            var list = new List<Team>();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContestDB"].ConnectionString))
            { 
                var cmd = new SqlCommand("select ID, title from Team", conn);
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(new Team(Convert.ToInt32(reader[0]), reader[1].ToString()));
                reader.Close();
            }

            return list;
        }

        public int AddContestant(int teamID, string name, string surname)
        {
            int userID = 0;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContestDB"].ConnectionString))
            {
                var cmd = new SqlCommand("contestantRegistration", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@teamID", teamID);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@surname", surname);
                cmd.Parameters.Add("@userID", SqlDbType.Int).Direction = ParameterDirection.Output;
                conn.Open();
                cmd.ExecuteNonQuery();
                userID = Convert.ToInt32(cmd.Parameters["@userID"].Value);
            }

            return userID;
        }

        public List<Contestant> GetContestants(int teamID)
        {
            var list = new List<Contestant>();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContestDB"].ConnectionString))
            {
                var cmd = new SqlCommand("select [name], surname from Contestant where teamID=@teamID", conn);
                cmd.Parameters.AddWithValue("@teamID", teamID);
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(new Contestant() { Name = reader[0].ToString() + " " + reader[1].ToString() });
                reader.Close();
            }

            return list;
        }

        public StageInfo GetStage(int teamID)
        {
            var eventID = 1;
            StageInfo info = new StageInfo(1, 1, -100, -100);

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContestDB"].ConnectionString))
            {
                var cmd = new SqlCommand("select stage, eventID, secondsToEvent, secondsAfterEvent from dbo.getStageInfo(@teamID)", conn);
                cmd.Parameters.AddWithValue("@teamID", teamID);
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    eventID = reader[1] is DBNull ? 1 : Convert.ToInt32(reader[1]);
                    info = new StageInfo(Convert.ToInt32(reader[0]), eventID, Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]));
                }

                reader.Close();
            }

            return info;
        }

        public object checkAnswer(int teamID, int questionID, string text)
        {
            object obj = null;
            if (hacked(questionID))
            {
                return new { correct = false, reload = false };
            }

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContestDB"].ConnectionString))
            {
                var cmd = new SqlCommand("select output from Answer where questionID=@questionID", conn);
                cmd.Parameters.AddWithValue("@questionID", questionID);
                var result = "";
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    result = reader[0].ToString();
                reader.Close();

                if (!result.Equals(text))
                {
                    obj = new { correct = false, reload = false };
                }
                else
                {
                    cmd = new SqlCommand("dbo.answerRegistration", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@teamID", teamID);
                    cmd.Parameters.AddWithValue("@questionID", questionID);
                    cmd.Parameters.Add("@reload", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    bool _reload = Convert.ToBoolean(cmd.Parameters["@reload"].Value);
                    obj = new { correct = true, reload = _reload };
                }
            }

            return obj;
        }

        private bool hacked(int questionID)
        {
            bool isHacked = true;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContestDB"].ConnectionString))
            {
                var cmd = new SqlCommand("select endTime from questionView where ID=@questionID", conn);
                cmd.Parameters.AddWithValue("@questionID", questionID);
                conn.Open();
                var endTime = cmd.ExecuteScalar();
                if (endTime != null)
                {
                    var startTime = DateTime.Now;
                    TimeSpan diff = Convert.ToDateTime(endTime) - startTime;
                    if (diff.TotalSeconds >= -60)
                        isHacked = false;
                }
            }
            return isHacked;
        }

        public SafeAnswer checkSafe(int teamID, string code)
        {
            SafeAnswer obj = null;
            if (hacked())
            {
                return new SafeAnswer() { correct = false, reload = false, TeamName = null };
            }

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContestDB"].ConnectionString))
            {
                var cmd = new SqlCommand("dbo.safeCrackTrialRegistration", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@teamID", teamID);
                cmd.Parameters.AddWithValue("@code", code);
                cmd.Parameters.Add("@cracked", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@teamName", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                conn.Open();
                cmd.ExecuteNonQuery();
                bool cracked = Convert.ToBoolean(cmd.Parameters["@cracked"].Value);
                string teamName = cmd.Parameters["@teamName"].Value.ToString();
                obj = new SafeAnswer() { correct = cracked, reload = cracked, TeamName = teamName };
            }

            return obj;
        }

        private bool hacked()
        {
            bool isHacked = true;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContestDB"].ConnectionString))
            {
                var cmd = new SqlCommand("select endTime from EventConfig where ID=2", conn);
                conn.Open();
                var endTime = cmd.ExecuteScalar();
                if (endTime != null)
                {
                    var startTime = DateTime.Now;
                    TimeSpan diff = Convert.ToDateTime(endTime) - startTime;
                    if (diff.TotalSeconds >= -60)
                        isHacked = false;
                }
            }
            return isHacked;
        }
    }
}