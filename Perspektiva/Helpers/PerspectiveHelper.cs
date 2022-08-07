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

    public void CreatePerspective(PerspectivaDataModel perspective)
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
          cmd.Parameters.AddWithValue("@PerspectivePicture", perspective.PerspectivePictureByte);
          //guess.u = Convert.ToInt32(cmd.ExecuteScalar());
          cmd.ExecuteScalar();
          con.Close();
        }

      }
    }
    public List<PerspectivaDataModel> GetPerspectives(bool getAll = false)
    {
      

      string constr = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Perspektiva_db;Integrated Security=True";
      SqlDataReader reader;

      List<PerspectivaDataModel> perspektivaModel = new List<PerspectivaDataModel>();


      using (SqlConnection con = new SqlConnection(constr))
      {
        string query = "SELECT * from [Perspectives] order by TimeStamp desc ";

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
                PerspectivePictureByte = (byte[])sdr["PerspectivePicture"],
              });
            }
          }
          con.Close();
        }

      }

      return perspektivaModel;
    }
    public PerspectivaDataModel GetPerspective(int id)
    {


      string constr = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Perspektiva_db;Integrated Security=True";
      SqlDataReader reader;

      List<PerspectivaDataModel> perspektivaModel = new List<PerspectivaDataModel>();


      using (SqlConnection con = new SqlConnection(constr))
      {
        string query = "SELECT * from [Perspectives] where ID = @ID";

        using (SqlCommand cmd = new SqlCommand(query))
        {
          cmd.Connection = con;
          cmd.Parameters.AddWithValue("@ID", id);
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
                PerspectivePictureByte = (byte[])sdr["PerspectivePicture"],
              });
            }
          }
          con.Close();
        }

      }

      return perspektivaModel.First() ;
    }


    public void UpdatePerspective(PerspectivaDataModel perspective, int id)
    {
      if (perspective == null)
        return;

      string constr = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Perspektiva_db;Integrated Security=True";
      using (SqlConnection con = new SqlConnection(constr))
      {
        string query = "UPDATE [Perspectives] " +
          "set Latitude = @Latitude," +
          "Longitude = @Longitude," +
          "TimeStamp = @TimeStamp," +
          "Description = @Description," +
          "Title = @Title," +
          "Difficulty = @Difficulty," +
          "PerspectivePicture = @PerspectivePicture " +
          "WHERE Id = @ID ";
        query += " SELECT SCOPE_IDENTITY()";
        using (SqlCommand cmd = new SqlCommand(query))
        {
          cmd.Connection = con;
          con.Open();
          // ADD auto increment
          //cmd.Parameters.AddWithValue("@Id", 0);
          
          cmd.Parameters.AddWithValue("@Latitude", perspective.Latitude);
          cmd.Parameters.AddWithValue("@Longitude", perspective.Longitude);
          cmd.Parameters.AddWithValue("@TimeStamp", DateTime.Now);
          cmd.Parameters.AddWithValue("@Description", perspective.Description);
          cmd.Parameters.AddWithValue("@Title", perspective.Title);
          cmd.Parameters.AddWithValue("@Difficulty", perspective.Difficulty);
          cmd.Parameters.AddWithValue("@PerspectivePicture", perspective.PerspectivePictureByte);
          cmd.Parameters.AddWithValue("@ID", id);
          //guess.u = Convert.ToInt32(cmd.ExecuteScalar());
          cmd.ExecuteScalar();
          con.Close();
        }

      }
    }
  }
}

