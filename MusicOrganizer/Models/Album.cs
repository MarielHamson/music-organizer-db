using System.Collections.Generic;
namespace MusicOrganizer.Models
{
  public class Album
  {
    public string AlbumTitle { get; set; }
    public int Id { get; }
    public Album(string albumTitle)
    {
      AlbumTitle = albumTitle;
    }
    /*     public static List<Album> GetAll()
        {

        } */
    public static void ClearAll()
    {

    }
    /*     public static Album Find(int searchId)
        {

        } */
  }
}