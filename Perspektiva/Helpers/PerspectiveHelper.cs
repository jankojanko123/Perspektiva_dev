using Microsoft.AspNetCore.Mvc;
using Perspektiva.Models;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Perspektiva.Areas.Admin.Models;

namespace Perspektiva.Helpers
{

    public class PerspectiveHelper
    {
        private SqlConnection con;

        public void CreatePerspective(PerspectivesViewModel perspective)
        {
            if (perspective == null)
                return;

            string constr = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Perspektiva_db;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "INSERT INTO [Perspectives](UserID, Latitude,Longitude,TimeStamp, Description, Title, Difficulty, PerspectivePicture) " +
                    "VALUES(@UserID, @Latitude,@Longitude,@TimeStamp, @Description, @Title, @Difficulty, @PerspectivePicture)";
                query += " SELECT SCOPE_IDENTITY()";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    // ADD auto increment
                    //cmd.Parameters.AddWithValue("@Id", 0);
                    cmd.Parameters.AddWithValue("@UserID", perspective.UserID);
                    cmd.Parameters.AddWithValue("@Latitude", perspective.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", perspective.Longitude);
                    cmd.Parameters.AddWithValue("@TimeStamp", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Description", perspective.Description);
                    cmd.Parameters.AddWithValue("@Title", perspective.Title);
                    cmd.Parameters.AddWithValue("@Difficulty", perspective.Difficulty);
                    cmd.Parameters.AddWithValue("@PerspectivePicture", perspective.PerspectivePicture);
                    //guess.u = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.ExecuteScalar();
                    con.Close();
                }

            }

        }

    }
}
