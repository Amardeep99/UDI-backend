using UDI_backend.Models;
using Microsoft.EntityFrameworkCore;
using UDI_backend.Exceptions;
namespace UDI_backend.Database {
	public class DatabaseContext {

		private readonly IConfiguration _configuration;

		public DatabaseContext(IConfiguration configuration) {
			_configuration = configuration;
		}

		private UdiDatabase CreateDbContext() {
			return new UdiDatabase(_configuration);
		}

		public bool ReferenceExists(int id) {
			UdiDatabase db = CreateDbContext();


			return db.References.Any(r => r.Id == id);
		}

		public bool ReferenceHasFormId(int id) {
			UdiDatabase db = CreateDbContext();

			return db.References.FirstOrDefault(r => r.Id == id)?.FormId != null;
		}

		public Form? GetForm(int formId) {
			UdiDatabase db = CreateDbContext();
			Form? form = db.Forms.FirstOrDefault(f => f.Id == formId);

			if (form == null) throw new KeyNotFoundException("No form with this Id");

			return form;	
		}

		public int? FormIdOfReferenceOrNull(int id) {
			UdiDatabase db = CreateDbContext();
			return db.References.FirstOrDefault(r => r.Id == id)?.FormId;
		}

		public int CreateApplication(int dNumber, string travelDate) {
			UdiDatabase db = CreateDbContext();

			bool isValid = CheckIfApplicationValid(db, dNumber, travelDate);

			if(!isValid) throw new InvalidDataException("Data does not have valid format");

			Application application = new() { DNumber = dNumber, TravelDate = DateTime.Parse(travelDate) };
			db.Applications.Add(application);
			db.SaveChanges();

			return application.Id;
		}


		public int CreateReference(int applicationID) {
            Console.WriteLine(applicationID);
			UdiDatabase db = CreateDbContext();
			Application? application = db.Applications.FirstOrDefault(a => a.Id == applicationID);

			if (application == null ) throw new KeyNotFoundException("No application of this id");

			Reference reference = new() { ApplicationId = applicationID, Application = application};
			
			try {
				db.References.Add(reference);
				db.SaveChanges();
			} catch (DbUpdateException ex) {
				Console.WriteLine(ex.Message);
				throw;
			}

			return reference.Id;
		}

		public int CreateForm(int orgNr, int refId, bool hasObjection, string objectionReason, bool hasDebt,
			string orgName, string email, string phone, string contactName) {
			UdiDatabase db = CreateDbContext();

			if (!ReferenceExists(refId)) throw new KeyNotFoundException("No such reference exists");
			if (ReferenceHasFormId(refId)) throw new ReferenceAlreadyHasFormIdException();


			Form form = new() { ReferenceId = refId, HasObjection = hasObjection, ObjectionReason = objectionReason, HasDebt = hasDebt, 
				OrganisationNr = orgNr, OrganisationName = orgName, Email = email, Phone = phone, ContactName = contactName };

			db.Forms.Add(form);
			db.SaveChanges();
			SetFormIDToReference(form.ReferenceId, form.Id);

			return form.Id;
		}

		public bool SetFormIDToReference(int referenceID, int formID) {
			UdiDatabase db = CreateDbContext();
			Reference reference = db.References.First(r => r.Id == referenceID);


			reference.FormId = formID;
			db.SaveChanges();

			return true;
		}

		public bool CheckIfApplicationValid(UdiDatabase db, int dNumber, string travelDate) {
			DateTime parsedDate = new();
			try {
				parsedDate = DateTime.Parse(travelDate);
			} catch (Exception ex) {
				Console.WriteLine("Could not parse string to date");
				return false;
			}

			if (db.Applications.Any(a => a.DNumber == dNumber)) {
				throw new Exception("Person already has process ongoing");
			}
			return true;

		}

	}
}

