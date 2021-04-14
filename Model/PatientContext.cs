using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCA_API.Model;

namespace HCA_API.Model
{
    public class PatientContext: DbContext
    {
        public PatientContext(DbContextOptions<PatientContext> options):base(options)
        {

        }

        public DbSet<Patient> patients { get; set; }

        public DbSet<HCA_API.Model.LabResult> LabResult { get; set; }
    }
}
