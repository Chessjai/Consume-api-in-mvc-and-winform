using S2Q2API.Dbclass;
using S2Q2API.Models;
using S2Q2API.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace S2Q2API.Controllers
{
    public class DefaultController : ApiController
    {
        
            Defaultdbcontext db = new Defaultdbcontext();
            Istudent student = new studentrepo();
            //getall
            [HttpGet]
            [Route("~/api/default/GetAllStudents")]
            public IHttpActionResult GetAllStudents()
            {
                Istudent Details = new studentrepo();
                var data = Details.GetAll();
                return Ok(data);
            }

        [HttpGet]
        [Route("~/api/default/GetByid")]
        public IHttpActionResult GetByid(int id)
        {
            Istudent Details = new studentrepo();
            var data = Details.Get(id);
            return Ok(data);
        }
        [HttpPost]
            [Route("~/api/default/Create")]
            public IHttpActionResult Create(Student std)
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid Data");
                using (var ctx = new Defaultdbcontext())
                {
                    ctx._student.Add(new Student()
                    {
                    
                        Name = std.Name,
                        Age = std.Age,
                        Address = std.Address
                    });
                    ctx.SaveChanges();
                }
                // var data = Student.Create(std);
                return Ok();
            }
            [HttpGet]
            //[Route("~/api/default/Update")]
            public IHttpActionResult Update(int id)
            {
                var std = db._student.Where(x => x.Id == id).FirstOrDefault();
                return Ok();
            }
            [HttpPut]
            [Route("~/api/default/Update")]
            public IHttpActionResult Update(Student std)
            {
          
            var data = student.Update(std.Id, std);
                return Ok();
            }
            //Delete
            [HttpDelete]
            [Route("~/api/default/Delete")]
            public IHttpActionResult Delete(int id)
            {
                studentrepo Det = new studentrepo();
                //var std = db.StudentModels.Where(x => x.Id == id).FirstOrDefault();
                Det.Delete(id);
                return Ok();
            }
        }

    }

