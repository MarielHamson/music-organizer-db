using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MusicOrganizer.Models
{
  public class Album
  {
    public string AlbumTitle { get; set; }
    public int Id { get; set; }
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
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM albums;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO albums (albumTitle) VALUES (@AlbumTitle);";
      MySqlParameter albumTitle = new MySqlParameter();
      albumTitle.ParameterName = "@AlbumTitle";
      albumTitle.Value = this.AlbumTitle;
      cmd.Parameters.Add(albumTitle);
      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherAlbum)
    {
      if (!(otherAlbum is Album))
      {
        return false;
      }
      {
        Album newAlbum = (Album)otherAlbum;
        bool albumTitleEquality = (this.AlbumTitle == newAlbum.AlbumTitle);
        bool idEquality = (this.Id == newAlbum.Id);
        return (albumTitleEquality && idEquality);
      }
    }
    public static Album Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM albums WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = searchId;
      cmd.Parameters.Add(thisId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int albumId = 0;
      string albumTitle = "";
      while (rdr.Read())
      {
        albumId = rdr.GetInt32(0);
        albumTitle = rdr.GetString(1);
      }
      Album foundAlbum = new Album(albumTitle, albumId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundAlbum;
    }
  }
}