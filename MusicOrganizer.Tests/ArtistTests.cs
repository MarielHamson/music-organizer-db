using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicOrganizer.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MusicOrganizer.Tests
{
  [TestClass]
  public class ArtistTests : IDisposable
  {
    public void Dispose()
    {
      Artist.ClearAll();
    }
    public void ArtistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=epicodus;port=3306;database=music_organizer_test;";
    }
    [TestMethod]
    public void ArtistConstructor_CreatesInstanceOfArtist_Artist()
    {
      Artist newArtist = new Artist("test");
      Assert.AreEqual(typeof(Artist), newArtist.GetType());
    }

    [TestMethod]
    public void GetArtistName_ReturnsArtistName_String()
    {
      //Arrange
      string name = "Prince";

      //Act
      Artist newArtist = new Artist(name);
      string result = newArtist.ArtistName;

      //Assert
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void SetArtistName_SetArtistName_String()
    {
      //Arrange
      string name = "testArtist";
      Artist newArtist = new Artist(name);

      //Act
      string updatedName = "newTestArtist";
      newArtist.ArtistName = updatedName;
      string result = newArtist.ArtistName;

      //Assert
      Assert.AreEqual(updatedName, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyListFromDatabase_ArtistList()
    {
      //Arrange
      List<Artist> newList = new List<Artist> { };

      //Act
      List<Artist> result = Artist.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ArtistList()
    {
      Artist testArtist = new Artist("Prince");
      testArtist.Save();
      List<Artist> result = Artist.GetAll();
      List<Artist> testList = new List<Artist> { testArtist };
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsArtists_ArtistList()
    {
      Artist newArtist1 = new Artist("Radiohead");
      Artist newArtist2 = new Artist("Prince");
      newArtist1.Save();
      newArtist2.Save();
      List<Artist> newList = new List<Artist> { newArtist1, newArtist2 };
      List<Artist> result = Artist.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectArtistFromDatabase_Artist()
    {
      Artist newArtist1 = new Artist("Radiohead");
      Artist newArtist2 = new Artist("Prince");
      newArtist1.Save();
      newArtist2.Save();
      Artist foundArtist = Artist.Find(newArtist2.Id);
      Assert.AreEqual(newArtist2, foundArtist);
    }

    [TestMethod]
    public void GetArtistAlbums_ReturnsAlbumsForArtistFromDatabase_List()
    {
      Artist newArtist1 = new Artist("Radiohead");
      Artist newArtist2 = new Artist("Prince");
      newArtist1.Save();
      newArtist2.Save();
      Album newAlbum1 = new Album("OK Computer");
      Album newAlbum2 = new Album("The Bends");
      Album newAlbum3 = new Album("Purple Rain");
      newAlbum1.ArtistId = newArtist1.Id;
      newAlbum2.ArtistId = newArtist1.Id;
      newAlbum3.ArtistId = newArtist2.Id;
      newAlbum1.Save();
      newAlbum2.Save();
      newAlbum3.Save();
      List<Album> newList = new List<Album> { newAlbum1, newAlbum2 };
      List<Album> results = newArtist1.GetArtistAlbums();
      Console.WriteLine(newArtist1.Id);
      Console.WriteLine(newAlbum1.ArtistId);
      Console.WriteLine(newList.Count);
      Console.WriteLine(results.Count);
      CollectionAssert.AreEqual(newList, results);
    }
  }
}
