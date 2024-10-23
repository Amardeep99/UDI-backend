namespace UDI_backend.Contracts {
	public class EditFormRequest {
		public bool HasObjection { get; set; }

		public bool HasDebt { get; set; }

		public string Email { get; set; } = null!;

		public string Phone { get; set; } = null!;

		public string ContactName { get; set; } = null!;
	}
}
