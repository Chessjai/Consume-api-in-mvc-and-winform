using Newtonsoft.Json;
using S2Q2API.Dbclass;
using S2Q2API.Models;
using S2Q2API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{

    public class HomeMVCController : Controller
    {
        Defaultdbcontext db = new Defaultdbcontext();
        HttpClient client = new HttpClient();
        public ActionResult Index()
        {
            List<Student> EmpInfo = new List<Student>();
            
                client.BaseAddress = new Uri("http://localhost:62842/api/default/getallstudents");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync("getallstudents");
                response.Wait();
                var test=response.Result;
                if (test.IsSuccessStatusCode)
                {
                    var disply = test.Content.ReadAsAsync<List<Student>>();
                    disply.Wait();
                    EmpInfo = disply.Result;
                }
                return View(EmpInfo);
            
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student std)
        {
            client.BaseAddress = new Uri("http://localhost:62842/api/default/Create");
            var response = client.PostAsJsonAsync("Create", std);
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");

        }
   
        public ActionResult Details(int id)
        {
            Student s = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62842/api/default/getbyid");
                //HTTP GET
                var responseTask = client.GetAsync("getbyid?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Student>();
                    readTask.Wait();
                    s = readTask.Result;
                }
            }

            return View(s);
        }

        public ActionResult Delete(int Id)
        {

            var std = db._student.Where(x => x.Id == Id).FirstOrDefault();
            return View(std);

        }
        [HttpPost]
        public ActionResult Delete(int id, Student std)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62842/api/default/Delete");
                //HTTP GET
                var responseTask = client.DeleteAsync("Delete?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit( int Id)
        {
            var std = db._student.Where(x => x.Id == Id).FirstOrDefault();
            return View(std);
            //Student s = null;
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:62842/api/default");
            //    //HTTP GET
            //    var responseTask = client.GetAsync("Update?id=" + Id.ToString());
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsAsync<Student>();
            //        readTask.Wait();
            //        s = readTask.Result;
            //    }
            //}
            //return View(s);
        }
        [HttpPost]
        public ActionResult Edit(Student std)
        {
           using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62842/api/default/Update");
                //HTTP GET
                var responseTask = client.PutAsJsonAsync<Student>("Update" ,std);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(std);
        }
     

    




    }
}