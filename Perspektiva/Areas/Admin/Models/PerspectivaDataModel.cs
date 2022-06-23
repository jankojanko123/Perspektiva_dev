namespace Perspektiva.Areas.Admin.Models
{
    public class PerspectivaDataModel
    {
        public int ID { get; set; }
        public string? UserID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Difficulty { get; set; }
        public byte[]? PerspectivePicture { get; set; }
    }
}
