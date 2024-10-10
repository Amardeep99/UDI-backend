using Microsoft.AspNetCore.Hosting.Server;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System;
using UDI_backend.Models;
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
			UdiMssqlDatabaseContext db = new();
			Reference reference = new();
			db.References.Add(reference);
			db.SaveChanges();

			return reference.ReferenceId;
		}

		public int CreateActor(string orgName, string email, string phone, string contactName) {
			UdiMssqlDatabaseContext db = new();
			Actor actor = new() { OrganisationName = orgName, Email = email, Phone = phone, ContactName = contactName };
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

