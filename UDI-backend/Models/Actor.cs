using System;
using System.Collections.Generic;

namespace UDI_backend.Models;

public partial class Actor
{
    public int OrganisationId { get; set; }

    public string OrganisationName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string ContactName { get; set; } = null!;

    public int? FormId { get; set; }

    public virtual Form? Form { get; set; }

    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();
}
