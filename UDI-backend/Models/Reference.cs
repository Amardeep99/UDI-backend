using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace UDI_backend.Models;

public class Reference {
    public int Id { get; set; }

    public int ApplicationId { get; set; }

    public int? FormId { get; set; }

    public int OrganisationNr { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual Form? Form { get; set; }
}
