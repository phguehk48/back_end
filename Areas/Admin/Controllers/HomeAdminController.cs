using CHIC_CHARM2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CHIC_CHARM2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/Homeadmin")]
    public class HomeAdminController : Controller
    {
        ChicCharm2Context db = new ChicCharm2Context();
        [Route("")]
        [Route("Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [Route("TaikhoanAdmin")]
        public IActionResult TaikhoanAdmin()
        {
            return View();
        }

        [Route("Quanlysanpham")]
        public IActionResult Quanlysanpham()
        {
            var lstSanpham = db.Sanphams.ToList();
            return View(lstSanpham);
        }

        [Route ("Themsanpham")]
        [HttpGet]
        public IActionResult Themsanpham()
        {
            ViewBag.Madm = new SelectList(db.Danhmucsps.ToList(), "Madm", "Tendanhmuc");
           
            return View();
        }

        [Route("Themsanpham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Themsanpham(Sanpham Sanpham)
        {

            if(ModelState.IsValid)
            {
                db.Sanphams.Add(Sanpham);
                    db.SaveChanges();
                return RedirectToAction("Quanlysanpham");

            }
            return View(Sanpham);
           }

        [Route("Suasanpham")]
        [HttpGet]
        public IActionResult Suasanpham(string masp)
        {
            var Sanpham = db.Sanphams.Find(masp);
            ViewBag.Madm = new SelectList(db.Danhmucsps.ToList(), "Madm", "Tendanhmuc");
            return View(Sanpham);
        }

        [Route("Suasanpham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Suasanpham(Sanpham Sanpham)
        {

            if (ModelState.IsValid)
            {
                db.Entry(Sanpham).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Quanlysanpham", "HomeAdmin");

            }
            return View(Sanpham);
        }
        [Route("Xoasanpham")]
        [HttpGet]
        public IActionResult Xoasanpham(string Masp)
        {
            {
                var sanpham = db.Sanphams.Find(Masp);
                db.Database.ExecuteSqlRaw("delete from Sanpham where Masp = '" + Masp + "'");
               

                return RedirectToAction("Quanlysanpham", "HomeAdmin");
            }
        }
        }

}
