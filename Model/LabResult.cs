using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HCA_API.Model
{
    public class LabResult
    {
        [Key]
        public int LabID { get; set; }
        [Required]
        public Result ResultType { get; set; }
        [Required]
        public int DoctorID { get; set; }
        [Required]
        public DateTime ResultInTime { get; set; }
        [Required]
        public DateTime ResultOutTime { get; set; }
        [Required]
        public LabType Type { get; set; }

        
        public int PatientID { get; set; }



        public enum Result
        {
            Positive,
            Negative
        }

        public enum LabType
        {
            Blood,
            Urine,
            Stool
        }
    }
}
