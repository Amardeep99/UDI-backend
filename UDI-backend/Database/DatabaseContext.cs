using UDI_backend.Models;
using Microsoft.EntityFrameworkCore;
using UDI_backend.Exceptions;
namespace UDI_backend.Database {
	public class DatabaseContext {

		private readonly UdiDatabase _db;

		public DatabaseContext(UdiDatabase db) {
			_db = db;
		}
		public bool ReferenceExists(int id) {
			return _db.References.Any(r => r.Id == id);
		}

		public Reference GetReference(int id) { 
			Reference? reference = _db.References.Include(r => r.Application).FirstOrDefault(r => r.Id == id);
			if (reference == null) throw new KeyNotFoundException("No reference with that id");

			return reference;
		}

		public bool ReferenceHasFormId(int id) {
			return _db.References.FirstOrDefault(r => r.Id == id)?.FormId != null;
		}

		public Form? GetForm(int formId) {
			Form? form = _db.Forms.FirstOrDefault(f => f.Id == formId);
			if (form == null) throw new KeyNotFoundException("No form with this id");

			return form;	
		}

		public DateTime? GetTravelDate(int refId) {
			DateTime? travelDate = _db.References
										.Include(r => r.Application)
										.Where(r => r.Id == refId)
										.Select(r => r.Application.TravelDate)
										.FirstOrDefault();

			if (travelDate == null) throw new KeyNotFoundException("Reference has no application");

			return travelDate;
		}

		public int? FormIdOfReferenceOrNull(int id) {
			return _db.References.FirstOrDefault(r => r.Id == id)?.FormId;
		}

		public int CreateApplication(int dNumber, string travelDate, string name) {

			bool isValid = CheckIfApplicationValid(_db, dNumber, travelDate);

			if(!isValid) throw new InvalidDataException("Data does not have valid format");

			Application application = new() { DNumber = dNumber, TravelDate = DateTime.Parse(travelDate), Name = name };
			_db.Applications.Add(application);
			_db.SaveChanges();

			return application.Id;
		}


		public int CreateReference(int applicationID, int orgNr) {
			Application? application = _db.Applications.FirstOrDefault(a => a.Id == applicationID);

			if (application == null ) throw new KeyNotFoundException("No application of this id");

			Reference reference = new() { ApplicationId = applicationID, Application = application, OrganisationNr = orgNr};
			
			try {
				_db.References.Add(reference);
				_db.SaveChanges();
			} catch (DbUpdateException) {
				throw;
			}

			return reference.Id;
		}

		public int CreateForm(int refId, bool hasObjection, 
			bool hasDebt, string email, string phone, string contactName) {

			if (!ReferenceExists(refId)) throw new KeyNotFoundException("No such reference exists");
			if (ReferenceHasFormId(refId)) throw new ReferenceAlreadyHasFormIdException();


			Form form = new() { ReferenceId = refId, HasObjection = hasObjection, HasDebt = hasDebt, 
									Email = email, Phone = phone, ContactName = contactName };

			_db.Forms.Add(form);
			_db.SaveChanges();
			SetFormIDToReference(form.ReferenceId, form.Id);

			return form.Id;
		}

		public void EditForm(int id, bool hasObjection,
			bool hasDebt, string email, string phone, string contactName) {

			Form? form = _db.Forms.FirstOrDefault(f => f.Id == id);

			if (form == null) throw new KeyNotFoundException("No form with this id");
			
			form.HasObjection = hasObjection;	
			form.HasDebt = hasDebt;
			form.Email = email;
			form.Phone = phone;
			form.ContactName = contactName;

			_db.Update(form);
			_db.SaveChanges();
		}

		public bool SetFormIDToReference(int referenceID, int formID) {
			Reference reference = _db.References.First(r => r.Id == referenceID);


			reference.FormId = formID;
			_db.SaveChanges();

			return true;
		}

		public bool CheckIfApplicationValid(UdiDatabase db, int dNumber, string travelDate) {
			DateTime parsedDate = new();
			try {
				parsedDate = DateTime.Parse(travelDate);
			} catch (Exception) {
				return false;
			}

			if (db.Applications.Any(a => a.DNumber == dNumber)) {
				throw new Exception("Person already has process ongoing");
			}
			return true;

		}

	}
}

