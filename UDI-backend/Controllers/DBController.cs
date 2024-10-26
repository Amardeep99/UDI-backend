using Microsoft.AspNetCore.Mvc;
using UDI_backend.Clients;
using UDI_backend.Contracts;
using UDI_backend.Database;
using UDI_backend.Exceptions;
using UDI_backend.Models;

namespace UDI_backend.Controllers {

	[ApiController]
	[Route("api/v1")]
	public class DBController : ControllerBase {
		private readonly DatabaseContext _db;
		private readonly BronnoysundsRegClient _client;
		public DBController(DatabaseContext db, BronnoysundsRegClient client) {
			_db = db;
			_client = client;
		}

		[HttpGet("referanse/{refNr}")]
		public async Task<IActionResult> GetReference(int refNr) {
			try {
				Reference? reference = _db.GetReference(refNr);
				DateTime? travelDateTime = _db.GetTravelDate(refNr);
				DateOnly? travelDate = travelDateTime.HasValue ? DateOnly.FromDateTime(travelDateTime.Value) : null;
				string name = await _client.GetOrganisationDetails(reference.OrganisationNr) ?? "Ukjent organisasjon";

				var data = new {
					ReferenceExists = reference != null,
					reference?.FormId,
					travelDate,
					reference?.OrganisationNr,
					ApplicantName = reference?.Application.Name,
					OrganisationName = name,
					reference?.Deadline
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

		[HttpPost("referanse")]
		public IActionResult CreateReference([FromBody] CreateReferenceRequest request) {
			if (request == null) return BadRequest("Bad request body");

			try {
				int refNr = _db.CreateReference(request.ApplicationId, request.OrganisationNr);
				return Ok(refNr);

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
								form.ReferenceNumber, 
								form.HasObjection, 
								form.SuggestedTravelDate,
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
			catch (DebtTrueWhileObjectionFalseException dex) {
				return BadRequest(dex.Message);
			}
			catch (Exception e) {
				return StatusCode(500, e.Message);
			}

		}

		[HttpPut("skjema/{id}")]
		public IActionResult EditForm(int id, [FromBody] EditFormRequest form) {
			if (form == null) return BadRequest("Request body incorrectly formated");

			try {
				_db.EditForm(
					id,
					form.HasObjection,
					form.SuggestedTravelDate,
					form.HasDebt,
					form.Email,
					form.Phone,
					form.ContactName);
				return Ok();

			} catch (KeyNotFoundException keyex) {
				return BadRequest(keyex.Message);
			} catch (FormatException fex) {
				return BadRequest(fex.Message);
			} catch (DebtTrueWhileObjectionFalseException dex) {
				return BadRequest(dex.Message);
			} catch (Exception) {
				return StatusCode(500);
			}

		}

	}
}
