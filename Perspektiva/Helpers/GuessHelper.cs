using Microsoft.AspNetCore.Mvc;
using Perspektiva.Models;
using Perspektiva.Areas.Admin.Models;
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
        public List<PerspectivaDataModel> GetPerspective(int id)
        {
            if (id == null)
                return null;

            string constr = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Perspektiva_db;Integrated Security=True";
            SqlDataReader reader;

            List<PerspectivaDataModel> perspektivaModel = new List<PerspectivaDataModel>();


            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT top(1) * from [Perspectives] order by TimeStamp desc ";
                
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            perspektivaModel.Add(new PerspectivaDataModel
                            {
                                ID = Convert.ToInt32(sdr["Id"]),
                                UserID = sdr["Id"].ToString(),
                                Latitude = sdr["Latitude"].ToString(),
                                Longitude = sdr["Longitude"].ToString(),
                                TimeStamp = DateTime.Parse(sdr["TimeStamp"].ToString()),
                                Description = sdr["Description"].ToString(),
                                Title = sdr["Title"].ToString(),
                                Difficulty = Convert.ToInt32(sdr["Difficulty"]),
                                PerspectivePicture = null
                            });
                        }
                    }
                    con.Close();
                }

            }

            return perspektivaModel;
        }



    }
}
