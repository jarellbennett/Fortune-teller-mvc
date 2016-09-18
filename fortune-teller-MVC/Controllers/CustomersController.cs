using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fortune_teller_MVC.Models;

namespace fortune_teller_MVC.Controllers
{
    public class CustomersController : Controller
    {
        private FortunetellerMVCEntities db = new FortunetellerMVCEntities();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.BirthMonth).Include(c => c.Color).Include(c => c.Sibling);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            //AGE Even or Odd
            if (customer.Age % 2 == 0)
            {
                ViewBag.NumberofYears = 22;
            }
            else
            {
                ViewBag.NumberofYears = 35;
            }


            //Sibling Rivalry
            if(customer.SiblingID == 1)
            {
                ViewBag.locale = "Tropical Island Paradise";
            }
            else if(customer.SiblingID == 2)
            {
                ViewBag.locale = "a Haunted Mansion";
            }
            else if(customer.SiblingID == 3)
            {
                ViewBag.locale = "a Castle";
            }
            else
            {
                ViewBag.locale = "a Space Shuttle";
            }


            //Bank Account
            var firstLetter = customer.BirthMonth.MonthName[0];
            var secondLetter = customer.BirthMonth.MonthName[1];
            var thirdLetter = customer.BirthMonth.MonthName[2];
            string fullName = customer.FirstName + customer.LastName;

            if(fullName.Contains(firstLetter))
            {
                ViewBag.AmtofMulah = 300000;
            }
            else if(fullName.Contains(secondLetter))
            {
                ViewBag.AmtofMulah = 25000;
            }
            else if(fullName.Contains(thirdLetter))
            {
                ViewBag.AmtofMulah = 1500;
            }
            else
            {
                ViewBag.AmtofMulah = 300;
            }


            //Transportation

            if(customer.ColorID == 1)
            {
                ViewBag.transport = "Porsche";
            }
            else if (customer.ColorID == 2)
            {
                ViewBag.transport = "Speed Boat";
            }
            else if (customer.ColorID == 3)
            {
                ViewBag.transport = "Magic School Bus";
            }
            else if (customer.ColorID == 4)
            {
                ViewBag.transport = "Tank";
            }
            else if (customer.ColorID == 5)
            {
                ViewBag.transport = "Submarine";
            }
            else if (customer.ColorID == 6)
            {
                ViewBag.transport = "Dragon";
            }
            else 
            {
                ViewBag.transport = "Space Ship";
            }






            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "MonthName");
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName");
            ViewBag.SiblingID = new SelectList(db.Siblings, "SiblingID", "SiblingID");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonthID,ColorID,SiblingID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = customer.CustomerID });
            }

            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "MonthName", customer.BirthMonthID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", customer.ColorID);
            ViewBag.SiblingID = new SelectList(db.Siblings, "SiblingID", "SiblingID", customer.SiblingID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "MonthName", customer.BirthMonthID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", customer.ColorID);
            ViewBag.SiblingID = new SelectList(db.Siblings, "SiblingID", "SiblingID", customer.SiblingID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonthID,ColorID,SiblingID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "MonthName", customer.BirthMonthID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", customer.ColorID);
            ViewBag.SiblingID = new SelectList(db.Siblings, "SiblingID", "SiblingID", customer.SiblingID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
