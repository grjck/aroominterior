using ARoomInterior.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace ARoomInterior.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly List<string> languages = new string[] { "en", "ru" }.ToList();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangeLanguage()
        {
            var cultureIndex = languages.FindIndex(x => x == CultureInfo.CurrentCulture.Name);
            string newCulture;
            if (cultureIndex != languages.Count - 1)
                newCulture = languages[cultureIndex + 1];
            else
                newCulture = languages[0];

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(newCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(newCulture);

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = newCulture;
            Response.Cookies.Add(cookie);

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
