using UDI_backend.Models;
using Microsoft.EntityFrameworkCore;
using UDI_backend.Exceptions;
using Microsoft.Extensions.Configuration;

namespace UDI_backend.Database {
	public class DatabaseContext {
		private readonly IDbContextFactory<UdiDatabase> _contextFactory;

		public DatabaseContext(IDbContextFactory<UdiDatabase> contextFactory) {
			_contextFactory = contextFactory;
		}

		public bool ReferenceExists(int id) {
			using var db = _contextFactory.CreateDbContext();
			return db.References.Any(r => r.Id == id);
		}

		public bool ReferenceHasFormId(int id) {
			using var db = _contextFactory.CreateDbContext();
			return db.References.FirstOrDefault(r => r.Id == id)?.FormId != null;
		}

		public Form? GetForm(int formId) {
			using var db = _contextFactory.CreateDbContext();
			Form? form = db.Forms.FirstOrDefault(f => f.Id == formId);

			if (form == null) throw new KeyNotFoundException("No form with this Id");

			return form;
		}

		public int? FormIdOfReferenceOrNull(int id) {
			using var db = _contextFactory.CreateDbContext();
			return db.References.FirstOrDefault(r => r.Id == id)?.FormId;
		}

		public int CreateApplication(int dNumber, string travelDate) {
			using var db = _contextFactory.CreateDbContext();

			if (!CheckIfApplicationValid(dNumber, travelDate))
				throw new InvalidDataException("Data does not have valid format");

			Application application = new() { DNumber = dNumber, TravelDate = DateTime.Parse(travelDate) };
			db.Applications.Add(application);
			db.SaveChanges();

			return application.Id;
		}

		public int CreateReference(int applicationID) {
			using var db = _contextFactory.CreateDbContext();
			Application? application = db.Applications.FirstOrDefault(a => a.Id == applicationID);

			if (application == null) throw new KeyNotFoundException("No application of this id");

			Reference reference = new() { ApplicationId = applicationID, Application = application };

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
			using var db = _contextFactory.CreateDbContext();

			if (!ReferenceExists(refId)) throw new KeyNotFoundException("No such reference exists");
			if (ReferenceHasFormId(refId)) throw new ReferenceAlreadyHasFormIdException();

			Form form = new() {
				ReferenceId = refId,
				HasObjection = hasObjection,
				ObjectionReason = objectionReason,
				HasDebt = hasDebt,
				OrganisationNr = orgNr,
				OrganisationName = orgName,
				Email = email,
				Phone = phone,
				ContactName = contactName
			};

			db.Forms.Add(form);
			db.SaveChanges();
			SetFormIDToReference(form.ReferenceId, form.Id);

			return form.Id;
		}

		public bool SetFormIDToReference(int referenceID, int formID) {
			using var db = _contextFactory.CreateDbContext();
			Reference reference = db.References.First(r => r.Id == referenceID);

			reference.FormId = formID;
			db.SaveChanges();

			return true;
		}

		private bool CheckIfApplicationValid(int dNumber, string travelDate) {
			using var db = _contextFactory.CreateDbContext();

			if (!DateTime.TryParse(travelDate, out _)) {
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