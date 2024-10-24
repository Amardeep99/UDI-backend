namespace UDI_backend.Contracts {
	public class CreateApplicationRequest {
		public int DNumber { get; set; }

		public string TravelDate { get; set; } = string.Empty;

		public string Name { get; set; } = null!;
	}
}
