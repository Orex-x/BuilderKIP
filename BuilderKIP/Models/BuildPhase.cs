namespace BuilderKIP.Models;

public class BuildPhase
{
    public int id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public int BuildingServiceId { get; set; }
    public BuildingService BuildingService { get; set; }
}