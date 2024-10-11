using Microsoft.AspNetCore.Hosting.Server;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System;
using UDI_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
namespace UDI_backend.Database {
	public class DatabaseContext {

		public int CreateApplication(int dNumber, string travelDate) {
			UdiMssqlDatabaseContext db = new();

			CheckIfApplicationValid(db, dNumber, travelDate);

			Application application = new() { DNumber = dNumber, TravelDate = DateTime.Parse(travelDate) };
			db.Applications.Add(application);
			db.SaveChanges();

			return application.ApplicationId;
		}


		public int CreateReference(int applicationID) {
            Console.WriteLine(applicationID);
			UdiMssqlDatabaseContext db = new();
			Application? application = db.Applications.FirstOrDefault(a => a.ApplicationId == applicationID);

			if (application == null ) throw new Exception("No application of this id");

			Reference reference = new() { ApplicationId = applicationID, Application = application};
			
			try {
				db.References.Add(reference);
				db.SaveChanges();
			} catch (DbUpdateException ex) {
				Console.WriteLine(ex.Message);
				throw;
			}

			return reference.ReferenceId;
		}

		public int CreateActor(int orgID, string orgName, string email, string phone, string contactName) {
			UdiMssqlDatabaseContext db = new();
			string spaceSeparatedName = contactName.Replace("-", " ");
			
			Actor actor = new() { OrganisationId = orgID, OrganisationName = orgName, Email = email, Phone = phone, ContactName = spaceSeparatedName };
			db.Actors.Add(actor);
			db.SaveChanges();

			return actor.OrganisationId;
		}

		public int CreateForm(int orgId, int refId, bool hasObjection, string objectionReason, bool hasDebt) {
			UdiMssqlDatabaseContext db = new();
			Form form = new() { OrganisationId = orgId, ReferenceId = refId, HasObjection = hasObjection, ObjectionReason = objectionReason, HasDebt = hasDebt };
			db.Forms.Add(form);
			db.SaveChanges();

			return form.FormId;
		}

		public bool AddFormIDToReference(int referenceID, int formID) {
			UdiMssqlDatabaseContext db = new();
			Reference? reference = db.References.First(r => r.ReferenceId == referenceID);

			if (reference == null) return false;

			reference.FormId = formID;
			db.SaveChanges();

			return true;
		}

		public bool AddFormIDToActor(int orgId, int formId) {
			UdiMssqlDatabaseContext db = new();
			Actor? actor = db.Actors.FirstOrDefault(a => a.OrganisationId == orgId);
			Form? form = db.Forms.FirstOrDefault(f => f.FormId == formId);

			if (form == null || actor == null) return false;

			actor.FormId = formId;
			db.SaveChanges();

			return true;
		}


		public bool CheckIfApplicationValid(UdiMssqlDatabaseContext db, int dNumber, string travelDate) {
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

