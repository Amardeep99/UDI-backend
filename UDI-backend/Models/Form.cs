using System;
using System.Collections.Generic;

namespace UDI_backend.Models;

public partial class Form
{
    public int FormId { get; set; }

    public bool HasObjection { get; set; }

    public string? ObjectionReason { get; set; }

    public bool HasDebt { get; set; }

    public virtual ICollection<Process> Processes { get; set; } = new List<Process>();
}
