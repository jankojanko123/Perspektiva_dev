using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Perspektiva.Areas.Admin.Models
{
  public class PerspectivaDataModel
  {
    [Key]
    public int ID { get; set; }
    public string? UserID { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public DateTime TimeStamp { get; set; }
    public int Difficulty { get; set; }
    public IFormFile PerspectivePictureFile { set; get; }
    public byte[]? PerspectivePictureByte { set; get; }
  }
}
