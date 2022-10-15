using System.Collections.Generic;

namespace BuilderKIP.Models;
public class Estimate
{
    public int Id { get; set; }
    public int Price { get; set; }
    public virtual ICollection<BuildingServiceContract> BuildingServices { get; set; }
    public virtual ICollection<Material> Materials { get; set; }
}