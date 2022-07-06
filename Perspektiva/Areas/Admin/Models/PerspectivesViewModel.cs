using System.ComponentModel.DataAnnotations;

namespace Perspektiva.Areas.Admin.Models
{
    public class PerspectivesViewModel
    {
        public string? UserID { get; set; } 
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int Difficulty { get; set; }
        public IFormFile PerspectivePicture { set; get; }
    }
}
