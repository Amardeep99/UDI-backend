using System;
using System.Collections.Generic;

namespace UDI_backend.Models2;

public partial class Application
{
    public int ApplicationId { get; set; }

    public int DNumber { get; set; }

    public DateTime TravelDate { get; set; }

    public virtual ICollection<Reference> References { get; set; } = new List<Reference>();
}
