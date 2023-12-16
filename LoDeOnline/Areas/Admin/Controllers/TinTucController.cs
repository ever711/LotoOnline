using LoDeOnline.Data;
using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoDeOnline.Areas.Admin.Controllers
{
    public class TinTucController : Controller
    {
        // GET: Admin/TinTuc
        public ActionResult Index()
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            SqlCommand cmd = new SqlCommand("sp_TinTuc_Load", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            List<TinTuc> lst = new List<TinTuc>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TinTuc p = new TinTuc();
                p.MaTin = int.Parse(dt.Rows[i]["MaTin"].ToString());
                p.TieuDe = dt.Rows[i]["TieuDe"].ToString();
                p.NoiDung = dt.Rows[i]["NoiDung"].ToString();
                p.ThoiGian = DateTime.Now;
                lst.Add(p);
            }
            return View(lst);
        }

        // GET: Admin/TinTuc/Details/5
        public ActionResult Details(int id)
        {
            MyERPDbContext context = new MyERPDbContext();
            LoDeOnline.Domain.TinTuc table = context.TinTucs.Single(p => p.MaTin == id);
            LoDeOnline.Models.TinTuc table1 = new LoDeOnline.Models.TinTuc() { MaTin = table.MaTin, TieuDe = table.TieuDe, NoiDung = table.NoiDung, ThoiGian = table.ThoiGian };
            return View(table1);
        }

        // GET: Admin/TinTuc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TinTuc/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TinTuc tintuc)
        {
            try
            {
                tintuc.ThoiGian = DateTime.Now;
                MyERPDbContext context = new MyERPDbContext();
                context.TinTucs.Add(tintuc);
                context.SaveChanges();
                // return RedirectToAction("Index");
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/TinTuc/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/TinTuc/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TinTuc tintuc)
        {
            try
            {
                // TODO: Add update logic here
                MyERPDbContext context = new MyERPDbContext();
                TinTuc table= context.TinTucs.Single(p => p.MaTin == id);
                table.TieuDe = tintuc.TieuDe;
                table.NoiDung = tintuc.NoiDung;
                table.ThoiGian = DateTime.Now;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/TinTuc/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/TinTuc/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                MyERPDbContext context = new MyERPDbContext();
                var table= context.TinTucs.Single(p => p.MaTin == id);
                context.TinTucs.Remove(table);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
