namespace UDI_backend.Contracts {
	public class CreateFormRequest {
		public int ReferenceId { get; set; }

		public bool HasObjection { get; set; }

		public string ObjectionReason { get; set; } = "";

		public bool HasDebt { get; set; }

		public int OrganisationNr { get; set; }

		public string OrganisationName { get; set; } = null!;

		public string Email { get; set; } = null!;

		public string Phone { get; set; } = null!;

		public string ContactName { get; set; } = null!;

	}
}
