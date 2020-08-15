using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ARoomInterior.Models;
using ARoomInterior.Models.DB;
using Syncfusion.EJ2.Linq;
using Newtonsoft.Json;

namespace ARoomInterior.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private System.IO.Ports.SerialPort port = new System.IO.Ports.SerialPort() { PortName = "COM4", BaudRate = 9600, ReadTimeout = 10000 };

        public ActionResult Index()
        {
            ViewBag.dataSource = db.Projects.Select(x => new { x.Name, x.Description, x.Customer.UserName, x.LawInfoContractNumber, LawDescription = x.LawInfo.Description }).ToList();
            return View();
        }

        public ActionResult Detail(string selectedName)
        {
            if (selectedName == null || selectedName == "")
                return Redirect("/Projects");
            var p = db.Projects.Select(x => new DetailProjectModel { IsCustomer = x.Customer.UserName == User.Identity.Name, LastName = x.Name, Name = x.Name, Description = x.Description, UserName = x.Customer.UserName, LawInfoContractNumber = x.LawInfoContractNumber, LawDescription = x.LawInfo.Description }).FirstOrDefault(x => x.Name == selectedName);
            ViewBag.Title = Resources.ProjectDetail.Title + " " + p.Name;
            return View(p);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detail(DetailProjectModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var p = db.Projects.First(x => x.Name == model.LastName);
            p.Name = model.Name;
            p.Description = model.Description;
            db.SaveChanges();

            return RedirectToAction("Detail", "Projects", new { selectedName = model.Name });// View(model.Name);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Project project)
        {
            if (project.Name != null)
            {
                try
                {
                    var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                    project.CustomerId = user.Id;
                    project.Customer = user;
                    db.Projects.Add(project);
                    db.SaveChanges();
                    return Redirect("/Projects");
                }
                catch (Exception) { }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string selectedName)
        {
            var p = db.Projects.FirstOrDefault(x => x.Name == selectedName && x.Customer.UserName == User.Identity.Name);
            if (p != null)
            {
                db.Projects.Remove(p);
                db.SaveChanges();
            }
            return Redirect("/Projects");
        }

        public ActionResult Model()
        {
            return Redirect("/Projects");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Model(string selectedName)
        {
            GetTemp();
            ViewBag.selectedName = selectedName;
            db.SpawnModels.ToList();
            db.ElementObjs.ToList();
            var proj = db.Projects.FirstOrDefault(x => x.Name == selectedName);
            ViewBag.dataSource = proj.SpawnModels.Select(x => new { x.ElementObj.Name, x.ElementObj.Description, x.ElementObj.Preview }).ToList();
            if (proj.RoomID == null || proj.RoomID == "")
            {
                proj.RoomID = GenerateRoomID();
                db.SaveChangesAsync();
            }
            ViewBag.roomID = proj.RoomID;
            return View();
        }

        private string GenerateRoomID()
        {
            var roomID = Membership.GeneratePassword(10, 0);
            return db.Projects.FirstOrDefault(x => x.RoomID == roomID) != null ? GenerateRoomID() : roomID;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeRoomID(string selectedName)
        {
            db.Projects.FirstOrDefault(x => x.Name == selectedName).RoomID = GenerateRoomID();
            db.SaveChanges();

            return RedirectToAction("Model", "Projects", new { selectedName });
        }

        [AllowAnonymous]
        [HttpPost]
        public string GetRoomData(string roomID)
        {
            try
            {
                return JsonConvert.SerializeObject(db.Projects.FirstOrDefault(x => x.RoomID == roomID).SpawnModels.ToList());
            }
            catch (Exception)
            {
                return "";
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public void SetRoomData(string roomID, string data)
        {
            try
            {
                db.Projects.FirstOrDefault(x => x.RoomID == roomID).SpawnModels = JsonConvert.DeserializeObject<List<SpawnModel>>(data);
                db.SaveChangesAsync();
            }
            catch (Exception) { }
        }

        public ActionResult AddElement(string selectedName)
        {
            if (selectedName == null)
                return Redirect("/Projects");
            ViewBag.selectedName = selectedName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddElement(string selectedName, ElementObj element)
        {
            ViewBag.selectedName = selectedName;
            if (element.Name == null)
                return View();
            try
            {
                var project = db.Projects.FirstOrDefault(x => x.Name == selectedName && x.Customer.UserName == User.Identity.Name);
                if (project == null)
                    return View();
                element = db.ElementObjs.Add(element);
                var d = db.SpawnModels.Add(new SpawnModel { ElementObj = element, ElementObjElementId = element.ElementId });
                project.SpawnModels.Add(d);
                db.SaveChanges();
                return RedirectToAction("Detail", "Projects", new { selectedName });
            }
            catch { }
            return View();
        }

        public void GetTemp()
        {
            try
            {
                port.Open();
                while (!port.IsOpen) { }
                SendTemp(port.ReadLine());
                if (port.IsOpen)
                    port.Close();
            }
            catch { }
        }

        public void SendTemp(string tempstr)
        {
            string[] strs = tempstr.Split(' ');
            var name = strs[0];
            var obj = db.ElementObjs.FirstOrDefault(x => x.Name == name);
            if (obj == null)
                return;
            obj.Description = "Humidity: " + strs[1] + "%\n" + "Temperature: " + strs[2] + "*C";
            db.SaveChanges();
        }
    }
}