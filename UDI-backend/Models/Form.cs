using System;
using System.Collections.Generic;

namespace UDI_backend.Models;

public partial class Form
{
    public int FormId { get; set; }

    public int OrganisationId { get; set; }

    public int? ReferenceId { get; set; }

    public bool HasObjection { get; set; }

    public string? ObjectionReason { get; set; }

    public bool HasDebt { get; set; }

    public virtual Actor Organisation { get; set; } = null!;

    public virtual Reference? Reference { get; set; }

    public virtual ICollection<Reference> References { get; set; } = new List<Reference>();
}
