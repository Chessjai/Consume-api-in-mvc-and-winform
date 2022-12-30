using Newtonsoft.Json;
using S2Q2API.Models;
using S2Q2API.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Q2Winform
{
   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private async void GetAllStudents()
        {
            List<Student> list = new List<Student>();
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("http://localhost:62842/api/default/getallstudents"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();

                        dataGridView1.DataSource = JsonConvert.DeserializeObject<Student[]>(data).ToList();

                    }
                }
            }
        }
        private async void AddStudent()
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty)
            {
                Student p = new Student();
            p.Name = textBox1.Text;
            p.Age = Convert.ToInt32(textBox2.Text);
            p.Address = textBox3.Text;
            using (var client = new HttpClient())
            {
                var serializedProduct = JsonConvert.SerializeObject(p);
                var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("http://localhost:62842/api/default/Create", content);
            }
            GetAllStudents();
                MessageBox.Show("inserted student");
            }
            else
            {
                MessageBox.Show("name or age or address not empty");
            }
        }
        private async void UpdateStudent()
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty)
            {
                Student p = new Student();
                p.Id = tempid;
                p.Name = textBox1.Text;
                p.Age = Convert.ToInt32(textBox2.Text);
                p.Address = textBox3.Text;

                using (var client = new HttpClient())
                {
                    var serializedProduct = JsonConvert.SerializeObject(p);
                    var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                    var result = await client.PutAsync(String.Format("http://localhost:62842/api/default/Update", p.Id), content);
                }
                GetAllStudents();
            }
            else
            {
                MessageBox.Show("name or age or address not empty");
            }

        }
        private async void DeleteStudent(int id)
        {
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync("http://localhost:62842/api/default/Delete?id=" + id);
            }
            GetAllStudents();
        }





        private void button1_Click_1(object sender, EventArgs e)
        {
            GetAllStudents();
          
        }
  

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            AddStudent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateStudent();
         

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text!=string.Empty)
            {
                int id = Convert.ToInt32(textBox4.Text);
                DeleteStudent(id);
            }
            else
            {
                MessageBox.Show("id not empty");
            }


        }
        public int tempid;
      

        private void dataGridView1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            studentrepo std = new studentrepo();
            var id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            tempid = Convert.ToInt32(id);

            var Data = std.Get(tempid);
            textBox1.Text = Data.Name;
            textBox2.Text = Data.Age.ToString();
            textBox3.Text = Data.Address;

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                errorProvider1.SetError(textBox1, "Please Enter Name");
                errorProvider2.SetError(textBox1, "");
                errorProvider3.SetError(textBox1, "");
            }
            else
            {
                errorProvider1.SetError(textBox1, "");
                errorProvider2.SetError(textBox1, "");
                errorProvider3.SetError(textBox1, "Correct");
            }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
           
                Regex numberchk = new Regex(@"^([0-9]*|\d*)$");
                if (numberchk.IsMatch(textBox2.Text))
                {
                    errorProvider1.SetError(textBox2, "");
                    errorProvider2.SetError(textBox2, "");
                    errorProvider3.SetError(textBox2, "Correct");
                }
                else
                {
                    errorProvider1.SetError(textBox2, "");
                    errorProvider2.SetError(textBox2 , "Wrong format");
                    errorProvider3.SetError(textBox2, "");
                }
            
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (textBox3.Text == string.Empty)
            {
                errorProvider1.SetError(textBox3, "Please Enter Address");
                errorProvider2.SetError(textBox3, "");
                errorProvider3.SetError(textBox3, "");
            }
            else
            {
                errorProvider1.SetError(textBox3, "");
                errorProvider2.SetError(textBox3, "");
                errorProvider3.SetError(textBox3, "Correct");
            }

        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            if (textBox4.Text == string.Empty)
            {
                errorProvider1.SetError(textBox4, "Please Enter id");
                errorProvider2.SetError(textBox4, "");
                errorProvider3.SetError(textBox4, "");
            }
            else
            {
                errorProvider1.SetError(textBox4, "");
                errorProvider2.SetError(textBox4, "");
                errorProvider3.SetError(textBox4, "Correct");
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }



