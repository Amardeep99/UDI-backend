using Microsoft.AspNetCore.Hosting.Server;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System;
using UDI_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
namespace UDI_backend.Database {
	public class DatabaseContext {

		public bool ReferenceExists(int id) {
			UdiDatabase db = new UdiDatabase();

			return db.References.Any(r => r.Id == id);
		}

		public int CreateApplication(int dNumber, string travelDate) {
			UdiDatabase db = new();

			CheckIfApplicationValid(db, dNumber, travelDate);

			Application application = new() { DNumber = dNumber, TravelDate = DateTime.Parse(travelDate) };
			db.Applications.Add(application);
			db.SaveChanges();

			return application.Id;
		}


		public int CreateReference(int applicationID) {
            Console.WriteLine(applicationID);
			UdiDatabase db = new();
			Application? application = db.Applications.FirstOrDefault(a => a.Id == applicationID);

			if (application == null ) throw new Exception("No application of this id");

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
			UdiDatabase db = new();
			Form form = new() { ReferenceId = refId, HasObjection = hasObjection, ObjectionReason = objectionReason, HasDebt = hasDebt, 
				OrganisationNr = orgNr, OrganisationName = orgName, Email = email, Phone = phone, ContactName = contactName };

			db.Forms.Add(form);
			db.SaveChanges();
			SetFormIDToReference(form.ReferenceId, form.Id);

			return form.Id;
		}

		public bool SetFormIDToReference(int referenceID, int formID) {
			UdiDatabase db = new();
			Reference? reference = db.References.First(r => r.Id == referenceID);

			if (reference == null) return false;

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

