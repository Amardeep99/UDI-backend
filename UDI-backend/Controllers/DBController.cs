using Microsoft.AspNetCore.Mvc;
using UDI_backend.Database;

namespace UDI_backend.Controllers {

	[ApiController]
	[Route("api/v1")]
	public class DBController : ControllerBase {
		private readonly DatabaseContext _db;
		public DBController(DatabaseContext db) {
			_db = db;
		}

		[HttpPost("soknad")]
		public IActionResult CreateApplication([FromQuery] int dnr, [FromQuery] string date) {
			try {
				_db.CreateApplication(dnr, date);
			} catch (Exception ex) {
				return StatusCode(500);
			}

			return Ok();
		}

		[HttpPost("referanse")]
		public IActionResult CreateReference([FromQuery] int aID) {
			try {
				_db.CreateReference(aID);
			} catch (Exception ex) {
				return StatusCode(500, ex.Message);
			}

			return Ok();
		}

		[HttpPut("referanse")]
		public IActionResult AddFormID([FromQuery] int formId, [FromQuery] int refId) {
			try {
				bool couldAdd = _db.AddFormIDToReference(refId, formId);

				if (!couldAdd) return BadRequest("Reference ID does not exist");

			} catch (Exception ex) {
				return StatusCode(500, ex.Message);
			}

			return Ok();
		}

		[HttpPost("skjema")]
		public IActionResult CreateForm([FromQuery] int orgNr, [FromQuery] int refId,
			[FromQuery] bool hasObjection, [FromQuery] string objectionReason,
			[FromQuery] bool hasDebt, [FromQuery] string orgName, [FromQuery] string email,
			[FromQuery] string phone, string cName) {
			try {
				_db.CreateForm(orgNr, refId, hasObjection, objectionReason, hasDebt, orgName, email, phone, cName);
			} catch (Exception ex) {
				return StatusCode(500);
			}

			return Ok();
		}
	}

}
