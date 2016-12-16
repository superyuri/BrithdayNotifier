using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApp.Domain;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            string[] collection = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.*", SearchOption.AllDirectories);
            StringBuilder sb = new StringBuilder();
            foreach (var item in collection)
            {
                sb.Append(item).Append("\r\n");
            }
            ViewBag.Message = "Data:"+ AppDomain.CurrentDomain.BaseDirectory + "\r\n" + sb.ToString();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}