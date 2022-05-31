using Microsoft.AspNetCore.Mvc;
using Perspektiva.Models;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace Perspektiva.Helpers
{
    public class GuessHelper
    {
        private SqlConnection con;

        //To Handle connection related activities
     
        public void SaveGuess(UserGuess guess)
        {
            if (guess == null)
                return;

            string constr = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Perspektiva_db;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "INSERT INTO [UserGuess](UserID, Latitude,Longitude,TimeStamp) VALUES(@UserID, @Latitude,@Longitude,@TimeStamp)";
                query += " SELECT SCOPE_IDENTITY()";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    // ADD auto increment
                    //cmd.Parameters.AddWithValue("@Id", 0);
                    cmd.Parameters.AddWithValue("@UserID", guess.UserID);
                    cmd.Parameters.AddWithValue("@Latitude", guess.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", guess.Longitude);
                    cmd.Parameters.AddWithValue("@TimeStamp", DateTime.Now);
                    //guess.u = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.ExecuteScalar(); 
                    con.Close();
                }
               
            }

        }



    }
}
