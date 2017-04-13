using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProgrammingContest.WebAPI.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace ProgrammingContest.WebAPI.Repositories
{
    public class QuestionRepository
    {
        public List<Question> getTeamQuestions(int teamID)
        {
            var list = new List<Question>();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContestDB"].ConnectionString))
            {
                var cmd = new SqlCommand("select * from dbo.getQuestions(@teamID)", conn);
                cmd.Parameters.AddWithValue("@teamID", teamID);
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(new Question()
                    {
                        orderNo = reader[0].ToString(),
                        questionID = Convert.ToInt32(reader[1]),
                        questionText = reader[2].ToString(),
                        isSolved = Convert.ToBoolean(reader[3]),
                        answer = Convert.ToBoolean(reader[3]) == true ? "Išspręsta" : "Neišspręsta",
                        Image = reader[4].ToString()
                    });

                reader.Close();
            }

            return list;
        }

        public object GetHint(int questionID)
        {
            object hint = null;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContestDB"].ConnectionString))
            {
                var cmd = new SqlCommand("select input, [image] from Answer where questionID = @questionID", conn);
                cmd.Parameters.AddWithValue("@questionID", questionID);
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    hint = new { hint = reader[0].ToString(), image = reader[1].ToString() };               
            }

            return hint;
        }

        public List<object> GetSafeQuestions()
        {
            List<object> hint = new List<object>();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContestDB"].ConnectionString))
            {
                var cmd = new SqlCommand("select * from dbo.getSafeQuestions()", conn);
                conn.Open();
                var reader = cmd.ExecuteReader();
                string taskNo = "";
                while (reader.Read())
                {
                    if (Convert.ToInt32(reader[0]) == 1)
                        taskNo = "A1";
                    else if (Convert.ToInt32(reader[0]) == 2)
                        taskNo = "A2";
                    else if (Convert.ToInt32(reader[0]) == 3)
                        taskNo = "A3";
                    else if (Convert.ToInt32(reader[0]) == 4)
                        taskNo = "A4";
                    else if (Convert.ToInt32(reader[0]) == 5)
                        taskNo = "A5";
                    else if (Convert.ToInt32(reader[0]) == 6)
                        taskNo = "A6";
                    hint.Add(new { orderNo = taskNo, questionText = reader[1].ToString(), Image = reader[2].ToString() });
                }                    
            }

            return hint;
        }
    }
}