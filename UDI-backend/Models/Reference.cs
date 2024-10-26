using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UDI_backend.Models;

public class Reference {

    [Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public int ReferenceNumber { get; set; }

    public int ApplicationId { get; set; }

    public int? FormId { get; set; }

    public int OrganisationNr { get; set; }

    public DateTime Deadline { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual Form? Form { get; set; }
}
