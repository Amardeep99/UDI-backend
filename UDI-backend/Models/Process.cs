using System.ComponentModel.DataAnnotations.Schema;

namespace UDI_backend.Models {
	public class Process {
		public int RefrenceID { get; set; } // PK
		public int OrganisationID { get; set; } // FK
		public int FormID { get; set; } // FK
		public int ApplicationID { get; set; } // FK

		// Navigation Properties

		public Form Form { get; set; }
		public Application Application { get; set; }


		[ForeignKey("OrganisationID")]
		public Actor Actor { get; set; }

	}
}
