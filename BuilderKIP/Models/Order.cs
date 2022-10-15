using System;
using System.Collections.Generic;

namespace BuilderKIP.Models;

public class Order
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public DateTime DateTime { get; set; }
    
    public int ClientId { get; set; }
    public virtual Client Client { get; set; }
    
    public virtual ICollection<BuildingServiceContract> BuildingServices { get; set; }
}