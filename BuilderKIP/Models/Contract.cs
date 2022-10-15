using System;
using System.Collections.Generic;

namespace BuilderKIP.Models;


public class Contract
{
    public int Id { get; set; }
    
    public int ClientId { get; set; }
    public virtual Client Client { get; set; }
    
    public int EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
    

    public DateTime DateTime { get; set; }
    
    public virtual ICollection<BuildingServiceContract> BuildingServices { get; set; } 
    
    public int ReceiptId { get; set; }
    public virtual Receipt Receipt { get; set; }
}