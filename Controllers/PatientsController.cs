using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HCA_API.Model;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace HCA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientContext _context;
        private IMemoryCache _cache;
       

        public PatientsController(PatientContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        /// <summary>
        /// Returns all patients
        /// </summary>
        /// <returns></returns>

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> Getpatients()
        {
            //Setting up inmemory cache
            _cache.Set("PatientsKey", _context.patients);
            return await _context.patients.ToListAsync();
        }

        /// <summary>
        /// Returns patient with specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        /// <summary>
        /// Give all patients that have had a certain type of lab resport in a datetime range
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

    
      
        //public void GetAllPatients()
        //{
        //    var patientslist = _context.patients.Join(_context.LabResult, patient => patient.PatientID,
        //        labResult => labResult.PatientID,
        //        (patient, labresult) => new
        //        {
        //            PatientName = patient.Name,
        //            ResultType = labresult.Type,
        //            BeginDate = labresult.ResultInTime,
        //            Enddate = labresult.ResultOutTime
        //        });

        //}

        /// <summary>
        /// Edit patient information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patient"></param>
        /// <returns></returns>
        // PUT: api/Patients/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, Patient patient)
        {
            if (id != patient.PatientID)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates patient information
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        // POST: api/Patients
       
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.patients.Add(patient);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPatient", new { id = patient.PatientID }, patient);
            }
            else
            {
                return BadRequest();
            }
            
        }

        /// <summary>
        /// Delete the patient by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(int id)
        {
            return _context.patients.Any(e => e.PatientID == id);
        }
    }
}
