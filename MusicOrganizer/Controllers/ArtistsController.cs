using Microsoft.AspNetCore.Mvc;
using MusicOrganizer.Models;
using System.Collections.Generic;

namespace MusicOrganizer.Controllers
{
  public class ArtistsController : Controller
  {
    [HttpGet("/artists")]
    public ActionResult Index()
    {
      List<Artist> allArtists = Artist.GetAll();
      return View(allArtists);
    }

    [HttpGet("/artists/new")]
    public ActionResult New()
    {
      return View();
    }
    [HttpPost("/artists")]
    public ActionResult Create(string artistName)
    {
      Artist newArtist = new Artist(artistName);
      newArtist.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/artists/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Artist selectedArtist = Artist.Find(id);
      List<Album> artistAlbums = selectedArtist.GetArtistAlbums();
      model.Add("artist", selectedArtist);
      model.Add("albums", artistAlbums);
      return View(model);
    }

    [HttpPost("/artists/{artistId}/albums")]
    public ActionResult Create(int artistId, string albumDescription)
    {
      Dictionary<string, object> model = new Dictionary<string, object> { };
      Artist foundArtist = Artist.Find(artistId);
      Album newAlbum = new Album(albumDescription);
      newAlbum.Save();
      List<Album> artistAlbums = foundArtist.GetArtistAlbums();
      model.Add("artist", artistAlbums);
      model.Add("albums", foundArtist);
      return View("Show", model);
    }
  }
}
