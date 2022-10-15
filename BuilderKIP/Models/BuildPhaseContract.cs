namespace BuilderKIP.Models;

public class BuildPhaseContract
{
    public int id { get; set; }
  
    public int BuildPhaseId { get; set; }
    public virtual BuildPhase BuildPhase { get; set; }
    
    public bool IsDone { get; set; }
    
    public int BuildingServiceContractId { get; set; }
    public virtual BuildingServiceContract BuildingServiceContract { get; set; }
}