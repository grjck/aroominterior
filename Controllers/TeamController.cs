using System;
using System.Linq;
using System.Web.Mvc;
using ARoomInterior.Models;
using ARoomInterior.Models.DB;

namespace ARoomInterior.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.dataSource = db.Teams.Select(x => new { x.Name, x.Description, x.Customer.UserName }).ToList();
            return View();
        }

        public ActionResult Detail(string selectedName)
        {
            if (selectedName == null || selectedName == "")
                return Redirect("/Team");
            var p = db.Teams.Select(x => new DetailTeamModel { HasInvite = x.ApplicationUsers.Count(u => u.UserName == User.Identity.Name) > 0 || x.Invites.Count(u => u.Sender.UserName == User.Identity.Name) > 0, IsCustomer = x.Customer.UserName == User.Identity.Name, LastName = x.Name, Name = x.Name, Description = x.Description, Customer = x.Customer.UserName }).FirstOrDefault(x => x.Name == selectedName);
            ViewBag.Title = Resources.TeamDetail.Title + " " + p.Name;
            return View(p);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detail(DetailTeamModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var p = db.Teams.First(x => x.Name == model.LastName);
            p.Name = model.Name;
            p.Description = model.Description;
            db.SaveChanges();

            return RedirectToAction("Detail", "Team", new { selectedName = model.Name });// View(model.Name);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Team team)
        {
            if (team.Name != null)
            {
                try
                {
                    var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                    team.CustomerId = user.Id;
                    team.Customer = user;
                    db.Teams.Add(team);
                    db.SaveChanges();
                    return Redirect("/Team");
                }
                catch (Exception) { }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string selectedName)
        {
            var p = db.Teams.FirstOrDefault(x => x.Name == selectedName && x.Customer.UserName == User.Identity.Name);
            if (p != null)
            {
                db.Teams.Remove(p);
                db.SaveChanges();
            }
            return Redirect("/Team");
        }

        public ActionResult TeamDetail(string selectedName)
        {
            var v = db.Teams.FirstOrDefault(x => x.Name == selectedName && x.Customer.UserName == User.Identity.Name);
            if (v == null)
                return RedirectToAction("/Team");
            db.Users.ToList();
            ViewBag.dataSource = v.ApplicationUsers.Select(x => new { x.UserName, x.Email, x.Speciallization, x.Info }).ToList();
            ViewBag.selectedName = selectedName;
            return View();
        }

        public ActionResult RemoveUser(string selectedTeamName, string selectedName)
        {
            db.Users.ToList();
            //db.Teams.ToList();
            var p = db.Teams.FirstOrDefault(x => x.Name == selectedTeamName && x.Customer.UserName == User.Identity.Name);
            if (p != null)
            {
                db.Teams.FirstOrDefault(x => x.Name == selectedTeamName && x.Customer.UserName == User.Identity.Name).ApplicationUsers.Remove(p.ApplicationUsers.FirstOrDefault(x => x.UserName == selectedName));
                db.SaveChanges();
            }
            return Redirect("/Team/Detail?selectedName=" + selectedTeamName);
        }

        public ActionResult Invites(string selectedName)
        {
            var v = db.Teams.FirstOrDefault(x => x.Name == selectedName && x.Customer.UserName == User.Identity.Name);
            if (v == null)
                return RedirectToAction("/Team");
            ViewBag.selectedName = selectedName;
            db.Invites.ToList();
            db.Users.ToList();
            ViewBag.dataSource = v.Invites.Select(x => new { x.Sender.UserName, x.Sender.Email, x.Sender.Speciallization, x.Sender.Info }).ToList();
            return View();
        }

        public ActionResult AcceptInvite(string selectedName, string selectedUserName)
        {
            var v = db.Teams.FirstOrDefault(x => x.Name == selectedName && x.Customer.UserName == User.Identity.Name);
            if (v == null)
                return RedirectToAction("/Team");
            db.Invites.ToList();
            db.Users.ToList();
            var inv = v.Invites.FirstOrDefault(x => x.Sender.UserName == selectedUserName);
            if (inv == null)
                return RedirectToAction("/Team");
            v.ApplicationUsers.Add(inv.Sender);
            v.Invites.Remove(inv);
            db.SaveChanges();

            return Redirect("/Team/Detail?selectedName=" + selectedName);
        }

        public ActionResult SendInvite(string selectedName)
        {
            var v = db.Teams.FirstOrDefault(x => x.Name == selectedName && x.Customer.UserName != User.Identity.Name);
            if (v == null)
                return RedirectToAction("/Team");
            v.Invites.Add(new Invite() { Sender = db.Users.First(x => x.UserName == User.Identity.Name) });
            db.SaveChanges();
            return Redirect("/Team/Detail?selectedName=" + selectedName);
        }
    }
}