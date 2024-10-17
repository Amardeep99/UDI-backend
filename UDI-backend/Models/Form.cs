using System;
using System.Collections.Generic;

namespace UDI_backend.Models;

public class Form {
    public int Id { get; set; }

    public int ReferenceId { get; set; }

    public bool HasObjection { get; set; }

    public string? ObjectionReason { get; set; }

    public bool HasDebt { get; set; }

	public string Email { get; set; } = null!;

	public string Phone { get; set; } = null!;

	public string ContactName { get; set; } = null!;

    public virtual Reference Reference { get; set; } = null!;

}
