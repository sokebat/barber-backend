using BarberApp.Application.Interface;
using BarberApp.Domain;
using Microsoft.AspNetCore.Mvc;
namespace BarberApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var appointments = await _appointmentService.GetAllAppointments();
                if (appointments == null || !appointments.Any())
                {
                    return NotFound(new { message = "No appointments found." });
                }
                return Ok(appointments);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving appointments.",
                    error = ex.Message
                });

            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid appointment ID." });
            }
            try
            {
                var appointment = await _appointmentService.GetAppointmentById(id);
                if (appointment == null)
                {
                    return NotFound(new { message = $"Appointment with ID {id} not found." });  // 404 - Not Found
                }
                return Ok(appointment);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"An error occurred while retrieving the appointment with ID {id}.",
                    error = ex.Message
                });

            }

        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromBody] Appointment appointment)
        {
            if (appointment == null)
            {
                return BadRequest(new { message = "Invalid appointment data." });  // 400 - Bad Request
            }
            if (appointment.id <= 0)
            {
                return BadRequest(new { message = "Specialist ID, Product ID required." });
            }
            try
            {
                await _appointmentService.AddAppointment(appointment);
                return CreatedAtAction(nameof(GetById), new { id = appointment.id }, appointment);

            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = "Duplicate appointment detected.", error = ex.Message });  // 409 - Conflict
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while adding the appointment.",
                    error = ex.InnerException?.Message ?? ex.Message
                });  // 500 - Internal Server Error
            }





        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment appointment)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid appointment ID." });  // 400 - Bad Request
            }

            if (appointment == null || id != appointment.id)
            {
                return BadRequest(new { message = "Appointment data is invalid or ID mismatch." });  // 400 - Bad Request
            }

            try
            {
                await _appointmentService.UpdateAppointment(appointment);
                return NoContent();  // 204 - No Content (successful update)
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });  // 404 - Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while updating the appointment.",
                    error = ex.InnerException?.Message ?? ex.Message
                });  // 500 - Internal Server Error
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid appointment ID." });  // 400 - Bad Request
            }

            try
            {
                await _appointmentService.DeleteAppointment(id);
                return NoContent();  // 204 - No Content (successful delete)
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });  // 404 - Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while deleting the appointment.",
                    error = ex.InnerException?.Message ?? ex.Message
                });  // 500 - Internal Server Error
            }
        }

    }
}
