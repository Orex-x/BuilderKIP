using System;
using System.Collections.Generic;

namespace BuilderKIP.Models;

public enum ContractStatus
{
    NEW,
    ACCEPT,
    COMPLETED
}

public class Contract
{
    public int Id { get; set; }

    public string Number { get; set; }
    
    public string Address { get; set; }

    public DateTime DeadLine { get; set; }

    public int? ClientId { get; set; } = null;
    public virtual Client Client { get; set; }
    
    public int? EmployeeId { get; set; } = null;
    public virtual Employee? Employee { get; set; }

    public ContractStatus ContractStatus { get; set; }

    public DateTime? DateTimeNew { get; set; }
    public DateTime? DateTimeAccept { get; set; }
    public DateTime? DateTimeCompleted { get; set; }

    public BuildingServiceContract BuildingServiceContract { get; set; } = new();
    
    public int? ReceiptId { get; set; } = null;
    public virtual Receipt? Receipt { get; set; }
}