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

		[HttpGet("referanse/{id}")]
		public IActionResult GetReference(int id) {
			try {
				bool refExists = _db.ReferenceExists(id);
				int? formId = _db.FormIdOfReferenceOrNull(id);

				var data = new { ReferenceExists = refExists, FormID = formId};
				return Ok(data);

			} catch (Exception) {
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
				int id = _db.CreateApplication(application.DNumber, application.TravelDate);
				return Ok(id);

			} catch (InvalidDataException dataex) {
				return BadRequest(dataex.Message);
			} 
			catch (Exception) {
				return StatusCode(500);
			}

			
		}

		[HttpPost("referanse/{aID}")]
		public IActionResult CreateReference(int aID) {
			try {
				int id = _db.CreateReference(aID);
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
								form.OrganisationNr, 
								form.ReferenceId, 
								form.HasObjection, 
								form.ObjectionReason, 
								form.HasDebt, 
								form.OrganisationName, 
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
					form.OrganisationNr,
					form.HasObjection,
					form.ObjectionReason,
					form.HasDebt,
					form.OrganisationName,
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
