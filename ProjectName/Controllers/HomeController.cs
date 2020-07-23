using Microsoft.AspNetCore.Mvc;
using ProjectName.Models;
using System.Collections.Generic;

namespace ProjectName.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

  }
}