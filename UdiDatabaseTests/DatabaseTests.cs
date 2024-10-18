using Microsoft.EntityFrameworkCore;
using UDI_backend.Database;
using UDI_backend.Models;

namespace UDI_backend.Tests {
	public class DatabaseContextTests : IDisposable {
		private readonly UdiDatabase _dbContext;
		private readonly DatabaseContext _databaseContext;

		public DatabaseContextTests() {
			_dbContext = CreateDatabaseContext();
			_databaseContext = new DatabaseContext(_dbContext);
		}

		private UdiDatabase CreateDatabaseContext() {
			var options = new DbContextOptionsBuilder<UdiDatabase>()
				.UseInMemoryDatabase(databaseName: $"InMemoryDb_{Guid.NewGuid()}")
				.Options;

			return new UdiDatabase(options);
		}

		public void Dispose() {
			_dbContext.Database.EnsureDeleted();
			_dbContext.Dispose();
		}

		[Theory]
		[InlineData(1000001, "2023-11-15", "Test Person 1")]
		[InlineData(1000002, "2023-12-01", "Test Person 2")]
		public void CreateApplication_ValidData_ReturnsApplicationId(int dNumber, string travelDate, string name) {
			// Act
			int applicationId = _databaseContext.CreateApplication(dNumber, travelDate, name);

			// Assert
			Assert.True(applicationId > 0);
			var savedApplication = _dbContext.Applications.FirstOrDefault(a => a.Id == applicationId);
			Assert.NotNull(savedApplication);
			Assert.Equal(dNumber, savedApplication.DNumber);
			Assert.Equal(name, savedApplication.Name);
			Assert.Equal(DateTime.Parse(travelDate), savedApplication.TravelDate);
		}

		[Theory]
		[InlineData(1000001, "2023-11-15", "Test Person 1", 2000001)]
		[InlineData(1000002, "2023-12-01", "Test Person 2", 2000002)]
		public void CreateReference_ValidApplicationId_ReturnsReferenceId(int dNumber, string travelDate, string name, int orgNr) {
			// Arrange
			int applicationId = _databaseContext.CreateApplication(dNumber, travelDate, name);

			// Act
			int referenceId = _databaseContext.CreateReference(applicationId, orgNr);

			// Assert
			Assert.True(referenceId > 0);
			var savedReference = _dbContext.References.FirstOrDefault(r => r.Id == referenceId);
			Assert.NotNull(savedReference);
			Assert.Equal(applicationId, savedReference.ApplicationId);
			Assert.Equal(orgNr, savedReference.OrganisationNr);
		}

		[Theory]
		[InlineData(1000001, "2023-11-15", "Test Person 1", 2000001, true, "Test objection reason", false, "test1@example.com", "1234567890", "John Doe")]
		[InlineData(1000002, "2023-12-01", "Test Person 2", 2000002, false, "", true, "test2@example.com", "0987654321", "Jane Smith")]
		public void CreateForm_ValidData_ReturnsFormId(int dNumber, string travelDate, string name, int orgNr,
													   bool hasObjection, string objectionReason, bool hasDebt,
													   string email, string phone, string contactName) {
			// Arrange
			int applicationId = _databaseContext.CreateApplication(dNumber, travelDate, name);
			int referenceId = _databaseContext.CreateReference(applicationId, orgNr);

			// Act
			int formId = _databaseContext.CreateForm(referenceId, hasObjection, objectionReason, hasDebt, email, phone, contactName);

			// Assert
			Assert.True(formId > 0);
			var savedForm = _dbContext.Forms.FirstOrDefault(f => f.Id == formId);
			Assert.NotNull(savedForm);
			Assert.Equal(referenceId, savedForm.ReferenceId);
			Assert.Equal(hasObjection, savedForm.HasObjection);
			Assert.Equal(objectionReason, savedForm.ObjectionReason);
			Assert.Equal(hasDebt, savedForm.HasDebt);
			Assert.Equal(email, savedForm.Email);
			Assert.Equal(phone, savedForm.Phone);
			Assert.Equal(contactName, savedForm.ContactName);
		}

		[Theory]
		[InlineData(1000001, "2023-11-15", "Test Person 1", 2000001, true, "Initial reason", false, "initial@example.com", "1234567890", "John Doe",
					false, "Updated reason", true, "updated@example.com", "0987654321", "Jane Smith")]
		public void EditForm_ValidData_UpdatesForm(int dNumber, string travelDate, string name, int orgNr,
												   bool initialHasObjection, string initialObjectionReason, bool initialHasDebt,
												   string initialEmail, string initialPhone, string initialContactName,
												   bool newHasObjection, string newObjectionReason, bool newHasDebt,
												   string newEmail, string newPhone, string newContactName) {
			// Arrange
			int applicationId = _databaseContext.CreateApplication(dNumber, travelDate, name);
			int referenceId = _databaseContext.CreateReference(applicationId, orgNr);
			int formId = _databaseContext.CreateForm(referenceId, initialHasObjection, initialObjectionReason, initialHasDebt, initialEmail, initialPhone, initialContactName);

			// Act
			_databaseContext.EditForm(formId, newHasObjection, newObjectionReason, newHasDebt, newEmail, newPhone, newContactName);

			// Assert
			var updatedForm = _dbContext.Forms.FirstOrDefault(f => f.Id == formId);
			Assert.NotNull(updatedForm);
			Assert.Equal(newHasObjection, updatedForm.HasObjection);
			Assert.Equal(newObjectionReason, updatedForm.ObjectionReason);
			Assert.Equal(newHasDebt, updatedForm.HasDebt);
			Assert.Equal(newEmail, updatedForm.Email);
			Assert.Equal(newPhone, updatedForm.Phone);
			Assert.Equal(newContactName, updatedForm.ContactName);
		}
	}
}