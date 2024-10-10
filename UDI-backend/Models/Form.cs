namespace UDI_backend.Models {
	public class Form {
		public int FormID { get; set; }
		public bool HasObjection { get; set; }
		public string? ObjectionReason { get; set; }

		public bool HasDebt { get; set; }

	}
}
