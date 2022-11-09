using System.Collections.Generic;

namespace BuilderKIP.Models;

public class BuildingServiceContract
{
    public int Id { get; set; }
    public int? Grade { get; set; }

    public int? TypeClimaticConditionId { get; set; } = null;
    public TypeClimaticCondition? TypeClimaticCondition { get; set; }

    public int? TypeGroundId { get; set; } = null;
    public TypeGround? TypeGround { get; set; }

    public int? TypeReliefId { get; set; } = null;
    public TypeRelief? TypeRelief { get; set; }


    public int BuildingServiceId { get; set; }
    public virtual BuildingService? BuildingService { get; set; }

    public int ContractId { get; set; }
    public virtual Contract? Contract { get; set; }

    public int PriceEstimate { get; set; }

    public virtual ICollection<BuildingServiceMaterial> Materials { get; set; } = new List<BuildingServiceMaterial>();
    public virtual ICollection<Stage> Stages { get; set; } = new List<Stage>();
}