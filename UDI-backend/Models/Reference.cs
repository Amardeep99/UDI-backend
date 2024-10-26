using System.ComponentModel.DataAnnotations;

namespace UDI_backend.Models;

public class Reference {

    [Key]
    public int ReferenceNumber { get; set; }

    public int ApplicationId { get; set; }

    public int? FormId { get; set; }

    public int OrganisationNr { get; set; }

    public DateTime Deadline { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual Form? Form { get; set; }
}
