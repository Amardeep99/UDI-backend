using System;
using System.Collections.Generic;

namespace UDI_backend.Models2;

public partial class Reference
{
    public int ReferenceId { get; set; }

    public int ApplicationId { get; set; }

    public int? FormId { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual Form? Form { get; set; }

    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();
}
