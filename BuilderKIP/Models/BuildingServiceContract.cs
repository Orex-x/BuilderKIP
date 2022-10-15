using System.Collections.Generic;

namespace BuilderKIP.Models;

public class BuildingServiceContract
{
    public int Id { get; set; }
    public int Grade { get; set; }
    public virtual ICollection<BuildPhaseContract> BuildPhaseContracts { get; set; }
    
    public int ContractId { get; set; }
    public virtual Contract Contract { get; set; }
    
    public int EstimateId { get; set; }
    public virtual Estimate Estimate { get; set; }
    
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
}