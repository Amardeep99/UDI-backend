using System;
using System.Collections.Generic;

namespace UDI_backend.Models;

public class Application {
    public int Id { get; set; }

    public int DNumber { get; set; }

    public DateTime TravelDate { get; set; }

    public virtual ICollection<Reference>? References { get; set; } 
}
