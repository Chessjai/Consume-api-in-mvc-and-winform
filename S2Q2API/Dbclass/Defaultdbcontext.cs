using S2Q2API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace S2Q2API.Dbclass
{
    public class Defaultdbcontext : DbContext

    {
        public Defaultdbcontext() : base("MyConnectionString")
        {
            Database.SetInitializer<Defaultdbcontext>(null);
        }
        public DbSet<Student> _student { get; set; }

        public System.Data.Entity.DbSet<S2Q2API.Models.StudentData> StudentDatas { get; set; }
    }

}