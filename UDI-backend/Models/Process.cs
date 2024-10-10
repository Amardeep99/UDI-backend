using System;
using System.Collections.Generic;

namespace UDI_backend.Models;

public partial class Process
{
    public int RefrenceId { get; set; }

    public int OrganisationId { get; set; }

    public int FormId { get; set; }

    public int ApplicationId { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual Form Form { get; set; } = null!;

    public virtual Actor Organisation { get; set; } = null!;
}
