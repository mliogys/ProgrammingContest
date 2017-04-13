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
    public class ContestantsRepository
    {
        public object GetContestantID(int loginID)
        {
            object obj = null;

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ContestDB"].ConnectionString))
            {
                var cmd = new SqlCommand("select contestantID, Title from contestantView where loginID=@loginID", conn);
                cmd.Parameters.AddWithValue("@loginID", loginID);
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    obj = new
                    {
                        userID = Convert.ToInt32(reader[0]),
                        teamTitle = reader[1].ToString()
                    };
                }
                reader.Close();
            }

            return obj;
        }
    }
}