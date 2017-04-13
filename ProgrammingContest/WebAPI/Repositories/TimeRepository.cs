using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ProgrammingContest.WebAPI.ViewModels;
using System.Data.SqlClient;
using System.Data;

namespace ProgrammingContest.WebAPI.Repositories
{
    public class TimeRepository
    {
        public int GetSecondsTillEnd(int eventID)
        {
            int seconds = 0;

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContestDB"].ConnectionString))
            {
                var cmd = new SqlCommand("select DATEDIFF(ss,GetDate(), endTime) as diff from EventConfig where ID=@eventID", conn);
                cmd.Parameters.AddWithValue("@eventID", eventID);
                conn.Open();
                var endTime = cmd.ExecuteScalar();
                if (endTime != null)
                    seconds = (int)endTime; 
            }

            return seconds;
        }

        public int GetSecondsTillStart(int eventID)
        {
            int seconds = 0;

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContestDB"].ConnectionString))
            {
                var cmd = new SqlCommand("select DATEDIFF(ss,GetDate(), startTime) as diff from EventConfig where ID=@eventID", conn);
                cmd.Parameters.AddWithValue("@eventID", eventID);
                conn.Open();
                var endTime = cmd.ExecuteScalar();
                if (endTime != null)
                    seconds = (int)endTime;
            }

            return seconds;
        }


    }
}