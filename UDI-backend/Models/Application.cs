﻿using System;
using System.Collections.Generic;

namespace UDI_backend.Models;

public partial class Application
{
    public int ApplicationId { get; set; }

    public int DNumber { get; set; }

    public DateTime TravelDate { get; set; }

    public virtual ICollection<Process> Processes { get; set; } = new List<Process>();
}
