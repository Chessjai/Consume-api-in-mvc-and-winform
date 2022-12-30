using S2Q2API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2Q2API.Repository
{
    public interface Istudent
    {

        IEnumerable GetAll();
        Student Get(int id);
        Student Add(Student model);
        Student Update(int Id, Student id);
        Student Delete(int Id, Student id);
        bool Delete(int id);

    }
}

