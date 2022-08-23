namespace Perspektiva.Models
{
  public class UserGuess
  {
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string? UserID { get; set; }
    public int PerspectiveID { get; set; }
    public byte[]? PerspectiveGuessPictureByte { set; get; }
  }
}
