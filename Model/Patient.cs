using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HCA_API.Model
{
    public class Patient
    {
        public Patient()
        {

        }
        [Key]
        public int PatientID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]    
        public DateTime DOB { get; set; }
        [Required]
        public Gender PatientGender { get; set; }

        public List<LabResult> LabResult { get; set; }


    public enum Gender
    {
        Male,
        Female
    }


    }
}
