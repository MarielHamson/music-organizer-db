using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MusicOrganizer.Models
{
  public class Artist
  {
    public string ArtistName { get; set; }
    public int Id { get; set; }
    public Artist(string artistName)
    {
      ArtistName = artistName;
    }

    public Artist(string artistName, int id)
    {
      ArtistName = artistName;
      Id = id;
    }

    public override bool Equals(System.Object otherArtist)
    {
      if (!(otherArtist is Artist))
      {
        return false;
      }
      {
        Artist newArtist = (Artist)otherArtist;
        bool artistNameEquality = (this.ArtistName == newArtist.ArtistName);
        bool idEquality = (this.Id == newArtist.Id);
        return (artistNameEquality && idEquality);
      }
    }

    public static void ClearAll()
    {
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM artists;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }
    }

    public static List<Artist> GetAll()
    {
      {
        List<Artist> allArtists = new List<Artist> { };
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM artists;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while (rdr.Read())
        {
          int artistId = rdr.GetInt32(0);
          string artistName = rdr.GetString(1);
          Artist newArtist = new Artist(artistName, artistId);
          allArtists.Add(newArtist);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allArtists;
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO artists (artistName) VALUES (@ArtistName);";
      MySqlParameter artistName = new MySqlParameter();
      artistName.ParameterName = "@ArtistName";
      artistName.Value = this.ArtistName;
      cmd.Parameters.Add(artistName);
      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Artist Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM artists WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = searchId;
      cmd.Parameters.Add(thisId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int artistId = 0;
      string artistName = "";
      while (rdr.Read())
      {
        artistId = rdr.GetInt32(0);
        artistName = rdr.GetString(1);
      }
      Artist foundArtist = new Artist(artistName, artistId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundArtist;
    }

    public List<Album> GetArtistAlbums()
    {
      List<Album> artistAlbums = new List<Album> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `albums` WHERE artistId = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = Id;
      cmd.Parameters.Add(thisId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        Album newAlbum = new Album(rdr.GetString(1), rdr.GetInt32(0), rdr.GetInt32(2));
        artistAlbums.Add(newAlbum);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return artistAlbums;
    }
  }
}