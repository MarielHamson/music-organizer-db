using System.Collections.Generic;
using MySql.Data.MySqlClient;

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

    public Album(string albumTitle, int id)
    {
      AlbumTitle = albumTitle;
      Id = id;
    }

    public static List<Album> GetAll()
    {
      List<Album> allAlbums = new List<Album> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM albums;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int albumId = rdr.GetInt32(0);
        string albumTitle = rdr.GetString(1);
        Album newAlbum = new Album(albumTitle, albumId);
        allAlbums.Add(newAlbum);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allAlbums;
    }
    public static void ClearAll()
    {

    }
    /*     public static Album Find(int searchId)
        {

        } */
  }
}