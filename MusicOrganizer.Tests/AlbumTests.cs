using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicOrganizer.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MusicOrganizer.Tests
{
  [TestClass]
  public class AlbumTests : IDisposable
  {
    public void Dispose()
    {

    }
    public void AlbumTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=epicodus;port=3306;database=music_organizer_test;";
    }
    [TestMethod]
    public void AlbumConstructor_CreatesInstanceOfAlbum_Album()
    {
      Album newAlbum = new Album("test");
      Assert.AreEqual(typeof(Album), newAlbum.GetType());
    }

    [TestMethod]
    public void GetAlbumTitle_ReturnsAlbumTitle_String()
    {
      //Arrange
      string title = "Walk the dog.";

      //Act
      Album newAlbum = new Album(title);
      string result = newAlbum.AlbumTitle;

      //Assert
      Assert.AreEqual(title, result);
    }

    [TestMethod]
    public void SetAlbumTitle_SetAlbumTitle_String()
    {
      //Arrange
      string title = "testAlbum";
      Album newAlbum = new Album(title);

      //Act
      string updatedTitle = "newTestAlbum";
      newAlbum.AlbumTitle = updatedTitle;
      string result = newAlbum.AlbumTitle;

      //Assert
      Assert.AreEqual(updatedTitle, result);
    }

    /* [TestMethod]
    public void GetAll_ReturnsEmptyListFromDatabase_AlbumList()
    {
      //Arrange
      List<Album> newList = new List<Album> { };

      //Act
      List<Album> result = Album.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    } */
  }
}
