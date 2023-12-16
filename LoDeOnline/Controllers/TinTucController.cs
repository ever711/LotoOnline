using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoDeOnline.Data;
//using LoDeOnline.Domain;
using LoDeOnline.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Reflection;

namespace LoDeOnline.Controllers
{
    
    public class TinTucController : Controller
    {
        // GET: TinTuc
        
        public ActionResult Index()
        {
            
            //load
           // MyERPDbContext context = new MyERPDbContext();
           // var table = context.TinTucs;
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

        // GET: TinTuc/Details/5
        public ActionResult Details(int id)
        {
            MyERPDbContext context = new MyERPDbContext();
            LoDeOnline.Domain.TinTuc table = context.TinTucs.Single(p => p.MaTin == id);
            LoDeOnline.Models.TinTuc table1 = new LoDeOnline.Models.TinTuc() { MaTin = table.MaTin, TieuDe = table.TieuDe, NoiDung = table.NoiDung, ThoiGian = table.ThoiGian };
            return View(table1);
        }

        // GET: TinTuc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TinTuc/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TinTuc/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TinTuc/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TinTuc/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TinTuc/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
