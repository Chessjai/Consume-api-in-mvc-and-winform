using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace S2Q2API.Models
{
    [Table("student2")]
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter name"), MaxLength(30)]
        [Display(Name = "Student Name")]
        public string Name { get; set; }

        [Range(1, 120, ErrorMessage = "Age must be between 1-120 in years.")]
        [Required(ErrorMessage = "Please enter Age")]
        public int Age { get; set; }

        public string Address { get; set; }

        //public List<Student> studentDStudentViewModels1 { get; set; } = new List<Student>();
    }
  

    public class StudentMasterDataViewmodel
    {
        public List<StudentData> studentDatas { get; set; } = new List<StudentData>();



    }

    public class StudentData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
  


}