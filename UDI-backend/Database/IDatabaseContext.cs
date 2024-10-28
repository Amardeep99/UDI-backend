using UDI_backend.Models;

namespace UDI_backend.Database;

public interface IDatabaseContext {
	// Reference related methods
	bool ReferenceExists(int refNr);
	Reference GetReference(int refNr);
	bool ReferenceHasFormId(int refNr);
	int? FormIdOfReferenceOrNull(int refNr);
	DateTime? GetTravelDate(int refNr);
	bool SetFormIDToReference(int refNr, int formID);

	// Form related methods
	Form? GetForm(int formId);
	int CreateForm(int refNr, bool hasObjection, string? suggestedTravelDate,
		bool hasDebt, string email, string phone, string contactName);
	void EditForm(int id, bool hasObjection, string? suggestedTravelDate,
		bool hasDebt, string email, string phone, string contactName);

	// Application related methods
	int CreateApplication(int dNumber, string travelDate, string name);
	int CreateReference(int applicationID, int orgNr);

	// Validation methods
	bool CheckValidHasObjectionAndHasDebt(bool hasObjection, bool hasDebt);
	bool CheckValidDateOnlyOrNull(string? date);
	bool CheckIfApplicationValid(IUdiDatabase db, int dNumber, string travelDate);
	int CreateUniqueReferenceNumber();
}