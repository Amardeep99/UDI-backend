using Microsoft.AspNetCore.Mvc;
using UDI_backend.Contracts;
using UDI_backend.Database;
using UDI_backend.Exceptions;
using UDI_backend.Models;

namespace UDI_backend.Controllers {

	[ApiController]
	[Route("api/v1")]
	public class DBController : ControllerBase {
		private readonly DatabaseContext _db;
		public DBController(DatabaseContext db) {
			_db = db;
		}

		[HttpGet("referanse/{refid}")]
		public IActionResult GetReference(int refId) {
			try {
				Reference? reference = _db.GetReference(refId);
				DateTime? travelDateTime = _db.GetTravelDate(refId);
				DateOnly? travelDate = travelDateTime.HasValue ? DateOnly.FromDateTime(travelDateTime.Value) : (DateOnly?)null;

				var data = new {
					ReferenceExists = reference != null,
					reference?.FormId,
					travelDate,
					reference?.OrganisationNr,
					reference?.Application.Name
				};

				return Ok(data);

			} catch (KeyNotFoundException keyex) {
				return BadRequest(keyex.Message);
			}
			catch (Exception) {
				return StatusCode(500);
			}
		}

		[HttpGet("skjema/{formId}")]
		public IActionResult GetForm(int formId) {
			try {
				Form form = _db.GetForm(formId)!;
				return Ok(form);	
			} catch (KeyNotFoundException keyex) {
				return NotFound(keyex.Message);
			}
		}

		[HttpPost("soknad")]
		public IActionResult CreateApplication([FromBody] CreateApplicationRequest application) {
			if (application == null) return BadRequest();
			 
			try {
				int id = _db.CreateApplication(application.DNumber, application.TravelDate, application.Name);
				return Ok(id);

			} catch (InvalidDataException dataex) {
				return BadRequest(dataex.Message);
			} 
			catch (Exception) {
				return StatusCode(500);
			}

			
		}

		// TODO: Create CreateReferenceRequest
		[HttpPost("referanse")]
		public IActionResult CreateReference([FromBody] CreateReferenceRequest request) {
			if (request == null) return BadRequest("Bad request body");

			try {
				int id = _db.CreateReference(request.ApplicationId, request.OrganisationNr);
				return Ok(id);

			} catch(KeyNotFoundException keyex) {
				return NotFound(keyex.Message);
			}
			catch (Exception) {
				return StatusCode(500);
			}
		}

		[HttpPost("skjema")]
		public IActionResult CreateForm([FromBody] CreateFormRequest form) {

			if (form == null) return BadRequest("Request body incorrectly formated");

			try {
				int id = _db.CreateForm(
								form.ReferenceId, 
								form.HasObjection, 
								form.ObjectionReason, 
								form.HasDebt, 
								form.Email, 
								form.Phone, 
								form.ContactName);
				return Ok(id);


			} 
			catch (KeyNotFoundException keyex) {
				return BadRequest(keyex.Message);
			} 
			catch (ReferenceAlreadyHasFormIdException refex) {
				return BadRequest(refex.Message);
			}
			catch (Exception) {
				return StatusCode(500);
			}

		}

		[HttpPut("skjema/{id}")]
		public IActionResult EditForm(int id, [FromBody] EditFormRequest form) {
			if (form == null) return BadRequest("Request body incorrectly formated");

			try {
				_db.EditForm(
					id,
					form.HasObjection,
					form.ObjectionReason,
					form.HasDebt,
					form.Email,
					form.Phone,
					form.ContactName);
				return Ok();

			} catch (KeyNotFoundException keyex) {
				return BadRequest(keyex.Message);
			} catch (Exception) {
				return StatusCode(500);
			}

		}

	}
}
