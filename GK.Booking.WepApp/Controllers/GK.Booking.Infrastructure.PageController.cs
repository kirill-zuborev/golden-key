using System.Web.Mvc;
using GK.Booking.Filters;

namespace GK.Booking.WepApp.Controllers
{
	public class PageController : Controller
	{
		public ActionResult Home()
		{
			Response.AddHeader("Content-Disposition", new System.Net.Mime.ContentDisposition { Inline = true, FileName = "index.html" }.ToString());
			return new FilePathResult(Server.MapPath("/wwwroot/GK.Booking.WebApp.Index.html"), "text/html");
		}

		public ActionResult Login()
		{
			Response.AddHeader("Content-Disposition", new System.Net.Mime.ContentDisposition { Inline = true, FileName = "index.html" }.ToString());
			return new FilePathResult(Server.MapPath("/wwwroot/GK.Booking.LoginPage.Index.html"), "text/html");
		}

		public ActionResult NotFound()
		{
			Response.AddHeader("Content-Disposition", new System.Net.Mime.ContentDisposition { Inline = true, FileName = "index.html" }.ToString());
			return new FilePathResult(Server.MapPath("/wwwroot/GK.Booking.NotFoundErrorPage.Index.html"), "text/html");
		}

		public ActionResult Error()
		{
			Response.AddHeader("Content-Disposition", new System.Net.Mime.ContentDisposition { Inline = true, FileName = "index.html" }.ToString());
			return new FilePathResult(Server.MapPath("/wwwroot/GK.Booking.ServerErrorPage.Index.html"), "text/html");
		}

		[Authorize]
		public ActionResult Link()
		{
			Response.AddHeader("Content-Disposition", new System.Net.Mime.ContentDisposition { Inline = true, FileName = "index.html" }.ToString());
			return new FilePathResult(Server.MapPath("/wwwroot/GK.Booking.LinkPage.Index.html"), "text/html");
		}

		[Authorize]
		public ActionResult Kitchen()
		{
			Response.AddHeader("Content-Disposition", new System.Net.Mime.ContentDisposition { Inline = true, FileName = "index.html" }.ToString());
			return new FilePathResult(Server.MapPath("/wwwroot/GK.Booking.KitchenScreen.Index.html"), "text/html");
		}

		[Authorize]
		public ActionResult Manager()
		{
			Response.AddHeader("Content-Disposition", new System.Net.Mime.ContentDisposition { Inline = true, FileName = "index.html" }.ToString());
			return new FilePathResult(Server.MapPath("/wwwroot/GK.Booking.ManagerScreen.Index.html"), "text/html");
		}

		[Authorize]
		public ActionResult Admin()
		{
			Response.AddHeader("Content-Disposition", new System.Net.Mime.ContentDisposition { Inline = true, FileName = "index.html" }.ToString());
			return new FilePathResult(Server.MapPath("/wwwroot/GK.Booking.AdminPage.Index.html"), "text/html");
		}
	}
}