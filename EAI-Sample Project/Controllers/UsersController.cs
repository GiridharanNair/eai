using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EAI_Sample_Project.EFFactory;
using EAI_Sample_Project.ViewModel;
using System.Data.Entity.Core.Objects;

namespace EAI_Sample_Project.Controllers
{
    public class UsersController : Controller
    {
        private EAIDemoEntities db = new EAIDemoEntities();

        // GET: Users
        public ActionResult Index()
        {


            return View(db.Users.ToList());
        }



        // GET: Users
        public ActionResult ListUsers()
        {
            List<UsersBuildingCount> userBuildingsMap = new List<UsersBuildingCount>();
            UsersBuildingCount building;
            foreach (User item in db.Users)
            {
                building = new UsersBuildingCount();
                var countOfBuildings = item.Buildings.Count();
                building.ID = item.ID;
                building.Name = item.Name;
                building.Email = item.Email;
                building.CountBuildings = countOfBuildings;
                userBuildingsMap.Add(building);

            }

            return View(userBuildingsMap.ToList());
        }



        // GET: Users
        public ActionResult ListUsersFromProc()
        {
            ObjectResult<sp_GetBuildingCountforUsers_Result> result;
            result = db.sp_GetBuildingCountforUsers();

            return View(result.ToList());
         }

        // GET: Users
        public ActionResult ListBuildings(int userID)
        {
            List<BuildingAccess> buildingItems = new List<BuildingAccess>();
            BuildingAccess item;
            foreach (Building building in db.Buildings)
            {
                item = new BuildingAccess();
                
                var access = building.Users.Where(x => x.ID==userID).Count();

                item.BuildingID = building.ID;
                item.Name = building.Name;
                item.Access = access > 0 ? true : false;
                buildingItems.Add(item);
            
            }
            return View(buildingItems.ToList());
        }


        // GET: Users
        public ActionResult ListAllAssets()
        {
                  
            return View(db.Buildings.ToList());
        }

        // GET: Users
        public ActionResult MapResources()
        {
            ViewBag.Users = db.Users;
            return View(db.Buildings.ToList());
        }


        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
