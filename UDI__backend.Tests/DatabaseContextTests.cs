using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using UDI_backend.Database;
using UDI_backend.Models;
using Xunit;

namespace UDI_backend.Tests {
    public class DatabaseContextTests : IDisposable {
        private readonly UdiMssqlDatabaseContext _dbContext;

        public DatabaseContextTests() {
            _dbContext = CreateDatabaseContext();
        }

        private UdiMssqlDatabaseContext CreateDatabaseContext() {
            var options = new DbContextOptionsBuilder<UdiMssqlDatabaseContext>()
                .UseInMemoryDatabase("InMemoryDb") // Using InMemory database for tests
                .Options;

            return new UdiMssqlDatabaseContext(options);
        }

        public void Dispose() {
            // Clean up the database after each test
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Fact]
        public void CreateApplication_ValidData_ReturnsApplicationId() {
            // Arrange
            var databaseContext = new DatabaseContext();

            int uniqueDNumber = new Random().Next(1, 1000000);

            // Act
            int applicationId = databaseContext.CreateApplication(uniqueDNumber, "2024-10-11");

            // Assert
            Assert.True(applicationId > 0);
        }

        [Fact]
        public void CreateActor_ValidData_ReturnsActorId() {
            // Arrange
            var databaseContext = new DatabaseContext();

            int uniqueOrgId = new Random().Next(1, 1000000);

            // Act
            int actorId = databaseContext.CreateActor(uniqueOrgId, "Test Organization", "test@example.com", "1234567890", "John Doe");

            // Assert
            Assert.True(actorId > 0);
        }

        [Fact]
        public void CreateReference_ValidApplicationId_ReturnsReferenceId() {
            // Arrange
            var databaseContext = new DatabaseContext();

            int uniqueAppId = new Random().Next(1, 1000000);
            var application = new Application {
                ApplicationId = uniqueAppId,
                DNumber = 12345,
                TravelDate = DateTime.Now
            };
            _dbContext.Applications.Add(application);
            _dbContext.SaveChanges();

            // Act
            int referenceId = databaseContext.CreateReference(application.ApplicationId);

            // Assert
            Assert.True(referenceId > 0);
        }

        [Fact]
        public void CreateForm_ValidData_ReturnsFormId() {
            // Arrange
            var databaseContext = new DatabaseContext();

            int uniqueOrgId = new Random().Next(1, 1000000);
            int actorId = databaseContext.CreateActor(uniqueOrgId, "Test Organization", "test@example.com", "1234567890", "John Doe");

            int dummyReferenceId = CreateDummyReference(_dbContext);

            // Act
            int formId = databaseContext.CreateForm(actorId, dummyReferenceId, true, "Test objection reason", false);

            // Assert
            Assert.True(formId > 0);
        }

        [Fact]
        public void CheckIfApplicationValid_ExistingApplication_ThrowsException() {
            // Arrange
            var databaseContext = new DatabaseContext();

            int existingDNumber = 12345;
            _dbContext.Applications.Add(new Application { DNumber = existingDNumber, TravelDate = DateTime.Now });
            _dbContext.SaveChanges();

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => databaseContext.CheckIfApplicationValid(_dbContext, existingDNumber, DateTime.Now.ToString()));
            Assert.Equal("Person already has process ongoing", ex.Message);
        }

        private int CreateDummyReference(UdiMssqlDatabaseContext db) {
            var application = new Application {
                ApplicationId = new Random().Next(1, 1000000),
                DNumber = new Random().Next(10000, 99999),
                TravelDate = DateTime.Now
            };
            db.Applications.Add(application);
            db.SaveChanges();

            var reference = new Reference {
                ApplicationId = application.ApplicationId
            };
            db.References.Add(reference);
            db.SaveChanges();

            return reference.ReferenceId;
        }
    }
}
