using S2Q2API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using S2Q2API.Dbclass;

namespace S2Q2API.Repository
{
    public class studentrepo:Istudent
    {
        
            Defaultdbcontext db = new Defaultdbcontext();

            private SqlConnection con;
            private void connection()
            {
                string str = ConfigurationManager.ConnectionStrings["MyConnectionString"].ToString();
                con = new SqlConnection(str);

            }
            public Student Add(Student model)
            {
                connection();
                SqlCommand cmd = new SqlCommand("stud_createQ2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", model.Name);
                cmd.Parameters.AddWithValue("@age", model.Age);
                cmd.Parameters.AddWithValue("@address", model.Address);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            return model;
            }

            public Student Delete(int Id, Student id)
            {
                throw new NotImplementedException();
            }

            public bool Delete(int id)
            {

                connection();
                SqlCommand cmd = new SqlCommand("Stud_delete_Q2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }

         

            public IEnumerable GetAll()
            {
                connection();
                List<Student> obj = new List<Student>();
                SqlCommand cmd = new SqlCommand("Stud_readQ2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    obj.Add(new Student
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Name = Convert.ToString(dr["name"]),
                        Age = Convert.ToInt32(dr["age"]),
                        Address = Convert.ToString(dr["address"])


                    });
                }
                return obj;
            }
        public Student Get(int ID)
        {
            connection();
            Student obj = new Student();
            SqlCommand cmd = new SqlCommand("proc_read_idQ2 ", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", ID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {


                obj.Id = Convert.ToInt32(dr["id"]);
                obj.Name = Convert.ToString(dr["name"]);
                obj.Age = Convert.ToInt32(dr["age"]);
                obj.Address = Convert.ToString(dr["address"]);



            }
            return obj;
            //common studentViewModels1 = new common();
            //try
            //{

            //    studentViewModels1.studentDStudentViewModels1 = db.Database.SqlQuery<Student>
            //        ("select * from tbl_student where Id=" + Convert.ToInt32(ID)).ToList();



            //}
            //catch (Exception ex)
            //{

            //}

        }

        public Student Update(int Id,  Student std)
            {
                connection();
                SqlCommand cmd = new SqlCommand("Stud_update_Q2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", std.Id);
                cmd.Parameters.AddWithValue("@name", std.Name);
                cmd.Parameters.AddWithValue("@age", std.Age);
                cmd.Parameters.AddWithValue("@address", std.Address);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return std;
            }

            //Student Istudent.Add(Student model)
            //{
            //    throw new NotImplementedException();
            //}

    }

    }
