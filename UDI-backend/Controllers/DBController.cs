using Microsoft.AspNetCore.Mvc;
using UDI_backend.Contracts;
using UDI_backend.Database;
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
		public IActionResult ReferenceExists(int id) {
			try {
				bool exists = _db.ReferenceExists(id);
				return Ok(exists);

			} catch (Exception ex) {
				return StatusCode(500);
			}
		}

		[HttpPost("soknad")]
		public IActionResult CreateApplication([FromBody] CreateApplicationRequest application) {
			if (application == null) return BadRequest();
			 
			try {
				int id = _db.CreateApplication(application.DNumber, application.TravelDate);
				return Ok(id);

			} catch (Exception ex) {
				return StatusCode(500);
			}

			
		}

		[HttpPost("referanse/{aID}")]
		public IActionResult CreateReference(int aID) {
			try {
				int id = _db.CreateReference(aID);
				return Ok(id);

			} catch (Exception ex) {
				return StatusCode(500, ex.Message);
			}
		}

		// **** Denne kan kanskje fjernes, siden det å sette FormID til Reference kan ordnes fra _db.CreateForm() ****
		//[HttpPut("referanse")]
		//public IActionResult SetFormIDToReference([FromBody] SetFormIDToReferenceRequest request) {
		//	if (request == null) return BadRequest();

		//	try {
		//		bool couldAdd = _db.SetFormIDToReference(request.ReferenceID, request.FormID);

		//		if (!couldAdd) return BadRequest("Reference ID does not exist");

		//		return Ok();

		//	} catch (Exception ex) {
		//		return StatusCode(500, ex.Message);
		//	}

		//}

		[HttpPost("skjema")]
		public IActionResult CreateForm([FromBody] CreateFormRequest form) {

			if (form == null) return BadRequest();

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


			} catch (Exception ex) {
				return StatusCode(500);
			}

		}
	}

}
